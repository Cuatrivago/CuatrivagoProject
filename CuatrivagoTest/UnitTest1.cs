using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CuatrivagoProject.Context;
using System.Diagnostics;

namespace CuatrivagoTest
{
    [TestClass]
    public class UnitTest1
    {
        private ReservationContext rc = new ReservationContext();
        string conn = "workstation id=CuatriVago.mssql.somee.com;packet size=4096;user id=cuatrivago;pwd=Cu@Tr0_V@G05;data source=CuatriVago.mssql.somee.com;persist security info=False;initial catalog=CuatriVago";


        [TestMethod]
        public void TestMethod1()
        {
            //string a = rc.insertReservation(conn, "2017-01-01", "2017-01-04", 1, 1);


            //Debug.WriteLine(a + "\n");

            //Debug.WriteLine(a + "\n");

            //Debug.WriteLine(a + "\n");

        }
    }
}
