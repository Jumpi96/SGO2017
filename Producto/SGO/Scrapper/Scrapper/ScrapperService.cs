using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using ExcelDataReader;
using System.IO;
using System.Data;

namespace Scrapper
{
    public class ScrapperService
    {
        //private readonly Models.SGOContext _context;

        //public Scrapper(Models.SGOContext context)
        public ScrapperService()
        {
            //_context = context;
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
                string aaa = result.Tables[0].Rows[0][0].ToString();
                IniciarObra(hoja);
                AnalizarPrecios(hoja,GetCeroYExcel(hoja));

                return 1;
            }
            else
            {
                return -1; // Error -1: No se encuentra el archivo.
            }
        }

        private int IniciarObra(DataTable hoja)
        {
            int i = 4;
            int j = GetCeroYExcel(hoja);

            string rawNombre = hoja.Rows[i][j - 2].ToString();
            string obraNombre = rawNombre.Substring(rawNombre.IndexOf(":") + 1);
            string rawCliente = hoja.Rows[i][j - 4].ToString();
            string obraCliente = rawCliente.Substring(rawCliente.IndexOf(":") + 1);
            double obraCR = GetCR(hoja);

            /*Models.Obra obra = new Models.Obra
            {
                ID = 0,
                Nombre = obraNombre,
                Cliente = obraCliente,
                Coeficiente = obraCR,
                InsFecha = DateTime.Now,
                ModUsuario = 0,
                ModFecha = DateTime.Now,
                Finalizada = 0
            };
            Controllers.ObrasController contObra = new Controllers.ObrasController(_context);
            contObra.Insert(obra);*/
            return 0; //debe devolver ID de Obra
        }

        private int GetCeroYExcel(DataTable hoja)
        {
            for (int a = 0; a < 25; a++)
                if (hoja.Rows[4][a].ToString().Equals("ANÁLISIS DE PRECIOS"))
                    return a;
            return -1;
        }

        private double GetCR(DataTable hoja)
        {
            for (int a = 0; a < 25; a++)
                if (hoja.Rows[15][a].ToString().Equals("CR"))
                    return Double.Parse(hoja.Rows[16][a].ToString());
            return -1;
        }

        private void AnalizarPrecios(DataTable hoja, int j)
        {
            j = GetCeroYPrecio(hoja, j);
            while (j != -1)
                j = GetCeroYPrecio(hoja, ProcesarPrecio(hoja, j));


            
        }

        private int ProcesarPrecio(DataTable hoja, int j)
        {
            int idRubro = GetRubro(hoja,j);
            int idSubRubro = GetSubRubro(hoja, j+1, idRubro);
            int idItem = GetItem(hoja, j+2, idSubRubro);
            int idUnidad = GetUnidad(hoja, j+3);

            return j;
        }

        private int GetCeroYPrecio(DataTable hoja, int i)
        {
            for (int a = i; a < i+10; a++)
                if (hoja.Rows[4][a].ToString().Equals("Rubro:"))
                    return a;
            return -1;
        }

        private int GetRubro(DataTable hoja, int j)
        {
            string nombreRubro = hoja.Rows[6][j].ToString();
            //Controllers.RubroesController cont = new Controllers.RubroesController(_context);
            //string idRubro = cont.RubroByNombre(nombreRubro);
            /*if (idRubro == 0)
            {
                Models.Rubro rubro = new Models.Rubro { ID = 0, Nombre = "Nombre", Numeracion = "A" };
                cont.Insert()
            }
            rawCliente.Substring(rawCliente.IndexOf(":") + 1);
            return idRubro;
            */
            
            return -1;
        }

        public void MasOMenosAsi()
        {
            //Models.Rubro rubro = new Models.Rubro { ID = 0, Nombre = "Nombre", Numeracion = "A" };
            //Controllers.RubroesController cont = new Controllers.RubroesController(_context);
            //cont.Insert(rubro);
        }
    }
}
