using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Context
{
    public class OfferContext
    {
        public List<Offer> createOffer(string conn, DateTime dateIn, DateTime dateOut, int discount, int type)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Create_Offer";
            SqlCommand cmdOffert = new SqlCommand(sqlStoredProcedure, connection);

            cmdOffert.CommandType = CommandType.StoredProcedure;

            SqlParameter currentDateIn = new SqlParameter();
            currentDateIn.ParameterName = "@StartDate";
            currentDateIn.SqlDbType = SqlDbType.Date;
            currentDateIn.Direction = ParameterDirection.Input;
            currentDateIn.Value = dateIn;

            SqlParameter currentDateOut = new SqlParameter();
            currentDateOut.ParameterName = "@FinishDate";
            currentDateOut.SqlDbType = SqlDbType.Date;
            currentDateOut.Direction = ParameterDirection.Input;
            currentDateOut.Value = dateOut;

            SqlParameter currentDiscount = new SqlParameter();
            currentDiscount.ParameterName = "@Discount";
            currentDiscount.SqlDbType = SqlDbType.Int;
            currentDiscount.Direction = ParameterDirection.Input;
            currentDiscount.Value = discount;

            SqlParameter currentType = new SqlParameter();
            currentType.ParameterName = "@RoomType";
            currentType.SqlDbType = SqlDbType.Int;
            currentType.Direction = ParameterDirection.Input;
            currentType.Value = type;

            cmdOffert.Parameters.Add(currentDateIn);
            cmdOffert.Parameters.Add(currentDateOut);
            cmdOffert.Parameters.Add(currentDiscount);
            cmdOffert.Parameters.Add(currentType);


            cmdOffert.Connection.Open();

            SqlDataReader reader = cmdOffert.ExecuteReader();

            List<Offer> offer = new List<Offer>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Offer offerTemp = new Offer();
                    offerTemp.dateIn = reader.GetDateTime(1);
                    offerTemp.dateOut = reader.GetDateTime(2);
                    offerTemp.discount = reader.GetInt32(3);
                    offerTemp.roomType = reader.GetString(4);
                    offer.Add(offerTemp);
                }
            }

            cmdOffert.Connection.Close();

            return offer;
        }

        public List<Offer> getOffers(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Retrieve_All_Offer";
            SqlCommand cmdOffert = new SqlCommand(sqlStoredProcedure, connection);

            cmdOffert.CommandType = CommandType.StoredProcedure;

            cmdOffert.Connection.Open();

            cmdOffert.ExecuteNonQuery();

            SqlDataReader reader = cmdOffert.ExecuteReader();

            List<Offer> offer = new List<Offer>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Offer offerTemp = new Offer();
                    offerTemp.id = reader.GetInt32(0);
                    offerTemp.dateIn = reader.GetDateTime(1);
                    offerTemp.dateOut = reader.GetDateTime(2);
                    offerTemp.discount = reader.GetInt32(3);
                    offerTemp.roomType = reader.GetString(4);
                    offerTemp.active = reader.GetInt32(5);
                    offer.Add(offerTemp);
                }
            }
            else
            {
                Offer offerTemp = new Offer();
                offerTemp.id = -1;
                offerTemp.dateIn = DateTime.Today;
                offerTemp.dateOut = DateTime.Today;
                offerTemp.discount = 10;
                offerTemp.roomType = "No hay ofertas";
                offerTemp.active = 1;
                offer.Add(offerTemp);
            }

            cmdOffert.Connection.Close();

            return offer;
        }

        public List<Offer> getOffer(string conn, int id)
        {
            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "";
            SqlCommand cmdOffert = new SqlCommand(sqlStoredProcedure, connection);

            cmdOffert.CommandType = CommandType.StoredProcedure;

            SqlParameter currentId = new SqlParameter();
            currentId.ParameterName = "@id";
            currentId.SqlDbType = SqlDbType.Int;
            currentId.Direction = ParameterDirection.Input;
            currentId.Value = id;

            cmdOffert.Parameters.Add(currentId);


            cmdOffert.Connection.Open();

            SqlDataReader reader = cmdOffert.ExecuteReader();

            List<Offer> offer = new List<Offer>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Offer offerTemp = new Offer();
                    offerTemp.id = reader.GetInt32(0);
                    offerTemp.dateIn = reader.GetDateTime(1);
                    offerTemp.dateOut = reader.GetDateTime(2);
                    offerTemp.discount = reader.GetInt32(3);
                    offerTemp.roomType = reader.GetString(4);
                    offer.Add(offerTemp);
                }
            }

            cmdOffert.Connection.Close();

            return offer;
        }

        public void deleteOffer(string conn, int id)
        {
            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_DELETE_OFFER";
            SqlCommand cmdOffert = new SqlCommand(sqlStoredProcedure, connection);

            cmdOffert.CommandType = CommandType.StoredProcedure;

            SqlParameter currentId = new SqlParameter();
            currentId.ParameterName = "@id";
            currentId.SqlDbType = SqlDbType.Int;
            currentId.Direction = ParameterDirection.Input;
            currentId.Value = id;

            SqlParameter r = new SqlParameter("@R", " ");
            r.SqlDbType = SqlDbType.Char;
            r.Size = 1;
            r.Direction = ParameterDirection.Output;
            cmdOffert.Parameters.Add(r);
            cmdOffert.Parameters.Add(currentId);

            cmdOffert.Connection.Open();

            cmdOffert.ExecuteReader();

            cmdOffert.Connection.Close();

        }

        /// <summary>
        /// Este metodo llama "por debajo de la mesa" a un sp que activa o elimina ofertas segun sea necesario sin devolver valores.
        /// </summary>
        /// <param name="conn">Conection String para base de datos</param>
        public void callForCheckOffer(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Verify_Offer";

            SqlCommand cmdVerifyOffer = new SqlCommand(sqlStoredProcedure, connection);

            cmdVerifyOffer.Connection.Open();
            cmdVerifyOffer.ExecuteNonQuery();
            cmdVerifyOffer.Connection.Close();

        }

    }
}