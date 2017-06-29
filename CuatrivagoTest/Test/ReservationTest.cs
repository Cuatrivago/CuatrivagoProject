using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CuatrivagoProject.Context;
using System.Diagnostics;

namespace CuatrivagoUnitTest.Test
{

    [TestClass]
    public class ReservationTest
    {

    private ReservationContext rc = new ReservationContext();
    string conn = "workstation id=CuatriVago.mssql.somee.com;packet size=4096;user id=cuatrivago;pwd=Cu@Tr0_V@G05;data source=CuatriVago.mssql.somee.com;persist security info=False;initial catalog=CuatriVago";


        [TestMethod]
        public void reservation()
        {
            int a = rc.isAvailable(conn, "2017-07-28", "2017-07-29", 1);
         
            Debug.WriteLine("Test" +  a + "\n");

            Debug.WriteLine(a + "\n");

            Debug.WriteLine(a + "\n");

        }
    }
}
