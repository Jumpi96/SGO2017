using System;
using SGO.Models;
using SGO.Controllers;
using System.Text;
using ExcelDataReader;
using System.IO;
using System.Data;

namespace SGO
{
    public class ScraperService
    {
        private Entities db;
        private readonly Usuario usuario;

        public ScraperService(Usuario usuario)
        {
            db = new Entities();
            this.usuario = usuario;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }


        public int ProcesarDocumento(String origen, int idCliente, int idDepartamento)
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
                var obra = IniciarObra(hoja, idCliente, idDepartamento);
                AnalizarPrecios(hoja, obra, GetCeroYExcel(hoja));

                return 1;
            }
            else
            {
                return -1; // Error -1: No se encuentra el archivo.
            }
        }

        private Obra IniciarObra(DataTable hoja, int idCliente, int idDepartamento)
        {
            int i = GetCeroYExcel(hoja);

            string rawNombre = hoja.Rows[i-2][4].ToString();
            string obraNombre = rawNombre.Substring(rawNombre.IndexOf(":")+2);
            string rawCliente = hoja.Rows[i-4][4].ToString();
            string obraCliente = rawCliente.Substring(rawCliente.IndexOf(":") + 2);
            double obraCR = GetCR(hoja);

            Obra obra = new Obra
            {
                ID = 0,
                Nombre = obraNombre,
                ClienteID = idCliente,
                Coeficiente = obraCR,
                InsFecha = DateTime.Now,
                ModUsuarioID = 1,//usuario.ID,
                ModFecha = DateTime.Now,
                Finalizada = false,
                DepartamentoID = idDepartamento
            };
            ObrasController contObra = new ObrasController();

            obra = contObra.Insertar(db, obra);
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

        private void AnalizarPrecios(DataTable hoja, Obra obra, int i)
        {
            i = GetCeroYPrecio(hoja, i);
            while (i != -1)
                i = GetCeroYPrecio(hoja, ProcesarPrecio(hoja, obra, i));
        }

        private int ProcesarPrecio(DataTable hoja, Obra obra, int i)
        {
            var rubro = GetRubro(hoja, i);
            var subRubro = GetSubRubro(hoja, ++i, rubro);
            var item = GetItem(hoja, ++i, subRubro);

            i = i + 3;

            i = GetSubItemDeTipoItem("A", hoja, obra, item, i);
            i = GetSubItemDeTipoItem("B", hoja, obra, item, i);
            i = GetSubItemDeTipoItem("C", hoja, obra, item, i);

            return i;
        }

        private int GetSubItemDeTipoItem(string caracterTipoItem, DataTable hoja, Obra obra, Item item, int i)
        {
            TipoItemsController contTipoItem = new TipoItemsController();
            var tipoItem = contTipoItem.TipoItemByCaracter(db, caracterTipoItem);

            string tope = tipoItem.Nombre.ToUpper();

            i++;

            while (hoja.Rows[i][4].ToString() != "-" && !hoja.Rows[i][4].ToString().Contains(tope))
            {
                string nombreSubItem = hoja.Rows[i][4].ToString();
                double cantidad = Double.Parse(hoja.Rows[i][5].ToString());
                string nombreUnidad = hoja.Rows[i][6].ToString();
                double precioUnitario = Double.Parse(hoja.Rows[i][7].ToString());
                var subItemDeItem = GetSubItemDeItem(item, tipoItem, nombreSubItem, nombreUnidad, precioUnitario);

                RegistrarDetalleSubItem(obra, subItemDeItem, cantidad, precioUnitario);

                i++;
            }
            
            while (!hoja.Rows[i][4].ToString().Contains(tope))
            {
                i++;
            }

            return ++i;
        }

        private void RegistrarDetalleSubItem(Obra obra, SubItemDeItem subItemDeItem, double cantidad, double precioUnitario)
        {
            DetalleSubItemsController cont = new DetalleSubItemsController();
            DetalleSubItem detalleSubItem = new DetalleSubItem { ID = 0, Cantidad = cantidad, Obra = obra, PrecioUnitario = precioUnitario, SubItemDeItem = subItemDeItem };
            cont.Insertar(db, detalleSubItem);
        }

        private SubItemDeItem GetSubItemDeItem(Item item, TipoItem tipoItem, string nombre, string nombreUnidad, double precio)
        {
            SubItemsController contSubItem = new SubItemsController();
            SubItem subItem = contSubItem.SubItemByNombre(db, nombre);

            if (subItem == null)
            {
                UnidadsController contUnidad = new UnidadsController();
                Unidad unidad = contUnidad.UnidadByNombre(db, nombreUnidad);

                if (unidad == null)
                {
                    unidad = new Unidad { ID = 0, Descripcion = "", Nombre = nombreUnidad };
                    unidad = contUnidad.Insertar(db, unidad);
                }

                subItem = new SubItem { ID = 0, Nombre=nombre, PrecioUnitario=precio,TipoItem=tipoItem, Unidad = unidad };
            }
            //else --> actualizar precio unitario del subitem (solo sirve de referencia)

            SubItemDeItemsController contSubItemDeItem = new SubItemDeItemsController();
            SubItemDeItem subItemDeItem = contSubItemDeItem.SubItemDeItemByItemSubItem(db, item, subItem);

            if (subItemDeItem == null)
            {
                subItemDeItem = new SubItemDeItem { ID = 0, Item = item, SubItem = subItem };
                subItemDeItem = contSubItemDeItem.Insertar(db, subItemDeItem);
            }

            return subItemDeItem;
        }

        private int GetCeroYPrecio(DataTable hoja, int i)
        {
            for (int a = i; a < i+15; a++)
                if (hoja.Rows[a][4].ToString().Equals("Rubro:"))
                    return a;
            return -1;
        }

        private Rubro GetRubro(DataTable hoja, int i)
        {
            string nombreRubro = hoja.Rows[i][6].ToString();
            RubroesController cont = new RubroesController();
            Rubro rubro = cont.RubroByNombre(db, nombreRubro);

            if (rubro == null)
            {
                rubro = new Rubro { ID = 0, Nombre = nombreRubro, Numeracion = "" };
                rubro = cont.Insertar(db, rubro);
            }

            return rubro;
        }

        private SubRubro GetSubRubro(DataTable hoja, int i, Rubro rubro)
        {
            string nombreSubRubro = hoja.Rows[i][6].ToString();
            SubRubroesController cont = new SubRubroesController();
            SubRubro subRubro = cont.SubrubroByNombreYRubro(db, nombreSubRubro,rubro);

            if (subRubro == null)
            {
                subRubro = new SubRubro { ID = 0, Nombre = nombreSubRubro, Rubro=rubro };
                subRubro = cont.Insertar(db, subRubro);
            }

            return subRubro;
        }

        private Item GetItem(DataTable hoja, int i, SubRubro subRubro)
        {
            string nombreItem = hoja.Rows[i][6].ToString();
            ItemsController cont = new ItemsController();
            Item item = cont.ItemByNombreYSubrubro(db, nombreItem, subRubro);
            
            if (item == null)
            {
                string nombreUnidad = hoja.Rows[i + 1][5].ToString();
                UnidadsController contUnidad = new UnidadsController();
                Unidad unidad = contUnidad.UnidadByNombre(db, nombreUnidad);

                if(unidad == null)
                {
                    unidad = new Unidad { ID = 0, Nombre = nombreUnidad, Descripcion = "" };
                    unidad = contUnidad.Insertar(db, unidad);
                }

                item = new Item { ID = 0, Nombre = nombreItem, SubRubro = subRubro, Unidad = unidad };
                item = cont.Insertar(db, item);
            }

            return item;
        }
    }
}
