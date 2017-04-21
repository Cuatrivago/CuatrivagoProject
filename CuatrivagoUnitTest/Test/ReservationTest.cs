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
          string a = rc.insertReservation(conn,"2017-01-01","2017-01-04",1,1);


            Debug.WriteLine(a + "\n");

            Debug.WriteLine(a + "\n");

            Debug.WriteLine(a + "\n");

        }
    }
}
