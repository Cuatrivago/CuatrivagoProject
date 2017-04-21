using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CuatrivagoProject.Context;
using System.Web.Configuration;
using CuatrivagoProject.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace CuatrivagoUnitTest
{
    [TestClass]
    public class RoomTest
    {

        private RoomContext rc = new RoomContext();
        string conn = "workstation id=CuatriVago.mssql.somee.com;packet size=4096;user id=cuatrivago;pwd=Cu@Tr0_V@G05;data source=CuatriVago.mssql.somee.com;persist security info=False;initial catalog=CuatriVago";
        //POR TIPO Y ENTRE FECHAS
        [TestMethod]
        public void habitacionTIPOFECHAS()
        {
            List<Room> list = rc.getRoomsAvailable(conn, "2000-04-20", "2090-04-21", 1);
            foreach (var element in list)
            {
                Debug.WriteLine(element.description_ + "\n");
                Debug.WriteLine(element.idRoom + "\n");
                Debug.WriteLine("\n");
            }
        }
    }
}
