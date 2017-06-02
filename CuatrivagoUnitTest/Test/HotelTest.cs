using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CuatrivagoProjectAdmin.Models;
using CuatrivagoProjectAdmin.Context;

namespace CuatrivagoUnitTest.Test
{
    [TestClass]
    public class HotelTest
    {

        private HotelContext rc = new HotelContext();
        string conn = "workstation id=CuatriVago.mssql.somee.com;packet size=4096;user id=cuatrivago;pwd=Cu@Tr0_V@G05;data source=CuatriVago.mssql.somee.com;persist security info=False;initial catalog=CuatriVago";


        [TestMethod]
        public void TestMethod1()
        {
            Hotel newHotel = new Hotel();
            newHotel.idHotel = 1;
            newHotel.name = "Cuatrivago";
            newHotel.address_ = "Costa Rica";
            newHotel.phone = "22345678";
            newHotel.email = "cuatri@cuatrivago.com";
            newHotel.postMail = "catrivago@cuatrivago.com";
            newHotel.aboutUs = "Durante años hemos trabajado en estrecha colaboración con organizaciones gubernamentales para implementar programas que beneficien a nuestro medio ambiente natural y nos permitan colaborar en la protección de la belleza natural de la región. Ejemplo de ello es la reconocida Certificación para la Sostenibilidad Turística.";
            newHotel.homeInformation = "Como líder global en el negocio de la hospitalidad, en el Hotel Cuatrivago  reconocemos nuestra responsabilidad de proteger el medio ambiente en la ejecución de nuestra labor.  Estamos comprometidos a realizar todas las acciones necesarias para ser un hotel sostenible y responsable con nuestra comunidad.";
            rc.updateHotel(newHotel, conn);
        }
}
}
