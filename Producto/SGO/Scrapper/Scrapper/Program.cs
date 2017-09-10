using System;


namespace Scrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            ScrapperService s = new ScrapperService();
            s.ProcesarDocumento("E:/SWAP/SGO/Producto/SGO/Scrapper/Scrapper/CopyReferenciaLicitacion.xls");

        }
    }
}
