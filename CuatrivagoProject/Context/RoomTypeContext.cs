using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Context
{
    public class RoomTypeContext
    {
        public List<RoomType> getAllRoomType(string conn)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Retrieve_Room_By_Type";
            SqlCommand cmdRoomType = new SqlCommand(sqlStoredProcedure, connection);

            cmdRoomType.CommandType = CommandType.StoredProcedure;

            cmdRoomType.Connection.Open();
            cmdRoomType.ExecuteNonQuery();

            SqlDataReader reader = cmdRoomType.ExecuteReader();

            List<RoomType> allRoomType = new List<RoomType>();

            cr.fi.bccr.indicadoreseconomicos.wsIndicadoresEconomicos client = new cr.fi.bccr.indicadoreseconomicos.wsIndicadoresEconomicos();
            DataSet typeChange = client.ObtenerIndicadoresEconomicos("317", DateTime.Today.ToString("d"), DateTime.Today.ToString("d"), "Cuatrivago", "n");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RoomType roomType = new RoomType();
                    roomType.idRoomType = reader.GetString(0);
                    roomType.name = reader.GetString(1);
                    roomType.description_ = reader.GetString(2);
                    roomType.quantity = reader.GetString(3);
                    roomType.priceColon = "$" + reader.GetString(4);
                    double colon  = int.Parse(reader.GetString(4)) * double.Parse(typeChange.Tables[0].Rows[0].ItemArray[2].ToString());

                    roomType.priceDolar = "₡" + colon;
                    roomType.path = reader.GetString(5);

                    allRoomType.Add(roomType);
                }
            }

            cmdRoomType.Connection.Close();

            return allRoomType;

        }
    }

}