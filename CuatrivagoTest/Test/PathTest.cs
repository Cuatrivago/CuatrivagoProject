using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CuatrivagoUnitTest.Test
{
    [TestClass]
    public class PathTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string server = "/Carpeta/Subcarpeta/Archivo.pendejo";

            Console.WriteLine(server.Substring(1));
        }
    }
}
