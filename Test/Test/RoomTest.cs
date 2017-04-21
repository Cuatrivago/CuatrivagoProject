using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CuatrivagoProject.Models;
using System.Web.Configuration;

namespace Test
{
   
    [TestClass]
    public class RoomTest
    {
        ModelForRoomList roomList = new ModelForRoomList();
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();

        [TestMethod]
        public void habitacionesPorTipoyFechas()
        {
            roomList.roomList = roomContext.getRoomsAvailable(conn, arrival, departure, roomType);
        }
    }
}
