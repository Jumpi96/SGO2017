using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using ExcelDataReader;
using System.IO;
using System.Data;

namespace SGO
{
    public class ScrapperService
    {
        private readonly Models.SGOContext _context;
        private readonly Models.Usuario usuario;

        public ScrapperService(Models.SGOContext context, Models.Usuario usuario)
        {
            _context = context;
            this.usuario = usuario;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }


        public int ProcesarDocumento(String origen)
        {
            IExcelDataReader reader = null;
            FileStream stream = File.Open(origen, FileMode.Open, FileAccess.Read);
            if (System.IO.Path.GetExtension(origen).Equals(".xls"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }

            if (reader != null)
            {
                System.Data.DataSet result = reader.AsDataSet();
                DataTable hoja = result.Tables[0];
                var obra = IniciarObra(hoja);
                AnalizarPrecios(hoja, obra, GetCeroYExcel(hoja));

                return 1;
            }
            else
            {
                return -1; // Error -1: No se encuentra el archivo.
            }
        }

        private Models.Obra IniciarObra(DataTable hoja)
        {
            int i = GetCeroYExcel(hoja);

            string rawNombre = hoja.Rows[i-2][4].ToString();
            string obraNombre = rawNombre.Substring(rawNombre.IndexOf(":")+2);
            string rawCliente = hoja.Rows[i-4][4].ToString();
            string obraCliente = rawCliente.Substring(rawCliente.IndexOf(":") + 2);
            double obraCR = GetCR(hoja);

            Models.Obra obra = new Models.Obra
            {
                ID = 0,
                Nombre = obraNombre,
                Cliente = obraCliente,
                Coeficiente = obraCR,
                InsFecha = DateTime.Now,
                ModUsuario = usuario,
                ModFecha = DateTime.Now,
                Finalizada = false
            };
            Controllers.ObrasController contObra = new Controllers.ObrasController(_context);

            obra = contObra.Insertar(obra);
            return obra;
        }

        private int GetCeroYExcel(DataTable hoja)
        {
            for (int a = 0; a < 25; a++)
                if (hoja.Rows[a][4].ToString().Equals("ANÁLISIS DE PRECIOS"))
                    return a;
            return -1;
        }

        private double GetCR(DataTable hoja)
        {
            for (int a = 0; a < 25; a++)
                if (hoja.Rows[a][15].ToString().Equals("CR"))
                    return Double.Parse(hoja.Rows[a][16].ToString());
            return -1;
        }

        private void AnalizarPrecios(DataTable hoja, Models.Obra obra, int i)
        {
            i = GetCeroYPrecio(hoja, i);
            while (i != -1)
                i = GetCeroYPrecio(hoja, ProcesarPrecio(hoja, i));


            
        }

        private int ProcesarPrecio(DataTable hoja, int i)
        {
            var rubro = GetRubro(hoja, i);
            var subRubro = GetSubRubro(hoja, ++i, rubro);
            var idItem = GetItem(hoja, ++i, subRubro);

            return i;
        }

        private int GetCeroYPrecio(DataTable hoja, int i)
        {
            for (int a = i; a < i+10; a++)
                if (hoja.Rows[a][4].ToString().Equals("Rubro:"))
                    return a;
            return -1;
        }

        private Models.Rubro GetRubro(DataTable hoja, int i)
        {
            string nombreRubro = hoja.Rows[i][6].ToString();
            Controllers.RubroesController cont = new Controllers.RubroesController(_context);
            Models.Rubro rubro = cont.RubroByNombre(nombreRubro);

            if (rubro == null)
            {
                rubro = new Models.Rubro { ID = 0, Nombre = nombreRubro, Numeracion = "" };
                rubro = cont.Insertar(rubro);
            }

            return rubro;
        }

        private Models.SubRubro GetSubRubro(DataTable hoja, int i, Models.Rubro rubro)
        {
            string nombreSubRubro = hoja.Rows[i][6].ToString();
            Controllers.SubRubroesController cont = new Controllers.SubRubroesController(_context);
            Models.SubRubro subRubro = cont.SubrubroByNombreYRubro(nombreSubRubro,rubro);

            if (subRubro == null)
            {
                subRubro = new Models.SubRubro { ID = 0, Nombre = nombreSubRubro, Rubro=rubro };
                subRubro = cont.Insertar(subRubro);
            }

            return subRubro;
        }

        private Models.Item GetItem(DataTable hoja, int i, Models.SubRubro subRubro)
        {
            string nombreItem = hoja.Rows[i][6].ToString();
            Controllers.ItemsController cont = new Controllers.ItemsController(_context);
            Models.Item item = cont.ItemByNombreYSubrubro(nombreItem, subRubro);
            
            if (item == null)
            {
                string nombreUnidad = hoja.Rows[i + 1][5].ToString();
                Controllers.UnidadsController contUnidad = new Controllers.UnidadsController(_context);
                Models.Unidad unidad = contUnidad.UnidadByNombre(nombreUnidad);

                if(unidad == null)
                {
                    unidad = new Models.Unidad { ID = 0, Nombre = nombreUnidad, Descripcion = "" };
                    unidad = contUnidad.Insertar(unidad);
                }

                item = new Models.Item { ID = 0, Nombre = nombreItem, SubRubro = subRubro, Unidad = unidad };
                item = cont.Insertar(item);
            }

            return item;
        }
    }
}
