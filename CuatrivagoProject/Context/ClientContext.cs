using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Context
{
    public class ClientContext
    {

        public ClientContext() { }


        public int verifyClient(string conn, string email)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Verify_Client2";
            SqlCommand comm = new SqlCommand(sqlStoredProcedure, connection);
            comm.CommandType = CommandType.StoredProcedure;


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@email";
            parameter.SqlDbType = SqlDbType.Char;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = email;
            comm.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@return";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Output;
            comm.Parameters.Add(parameter2);
            comm.Connection.Open();
            comm.ExecuteNonQuery();
            int returnValue = int.Parse(parameter2.Value.ToString());
            comm.Connection.Close();
            return returnValue;
        }
        public int returnClientId(string conn, string email)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_Client_By_Mail";
            SqlCommand comm = new SqlCommand(sqlStoredProcedure, connection);
            comm.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@email_";
            parameter.SqlDbType = SqlDbType.Char;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = email;
            comm.Parameters.Add(parameter);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@return";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Output;
            comm.Parameters.Add(parameter2);
            comm.Connection.Open();
            comm.ExecuteNonQuery();
            int returnValue = int.Parse(parameter2.Value.ToString());
            comm.Connection.Close();
            return returnValue;
        }

        public int insertClient(string conn, Client client)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Create_Client";
            SqlCommand comm = new SqlCommand(sqlStoredProcedure, connection);
            comm.CommandType = CommandType.StoredProcedure;



            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@Name";
            p1.SqlDbType = SqlDbType.Char;
            p1.Direction = ParameterDirection.Input;
            p1.Value = client.name;
            comm.Parameters.Add(p1);


            SqlParameter p11 = new SqlParameter();
            p11.ParameterName = "@LastName";
            p11.SqlDbType = SqlDbType.Char;
            p11.Direction = ParameterDirection.Input;
            p11.Value = client.lastName;
            comm.Parameters.Add(p11);
            //comm.Parameters.Add(p11);


            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@Email";
            p2.SqlDbType = SqlDbType.Char;
            p2.Value = client.email;
            p2.Direction = ParameterDirection.Input;
            comm.Parameters.Add(p2);


            SqlParameter P3 = new SqlParameter();
            P3.ParameterName = "@Phone";
            P3.SqlDbType = SqlDbType.Char;
            P3.Direction = ParameterDirection.Input;
            P3.Value = client.phone;
            comm.Parameters.Add(P3);



            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@CreditCard";
            p4.SqlDbType = SqlDbType.Char;
            p4.Direction = ParameterDirection.Input;
            string identificadorUnico = Guid.NewGuid().ToString();
            p4.Value = identificadorUnico;
            //p4.Value = client.creditCard;
            comm.Parameters.Add(p4);



            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@IdCard";
            p5.SqlDbType = SqlDbType.Char;
            p5.Direction = ParameterDirection.Input;
            string identificadorUnico5 = Guid.NewGuid().ToString();
            p5.Value = identificadorUnico5;
            //p5.Value = client.idCard;
            comm.Parameters.Add(p5);



            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@R";
            p6.SqlDbType = SqlDbType.Int;
            p6.Direction = ParameterDirection.Output;
            comm.Parameters.Add(p6);



            comm.Connection.Open();
            comm.ExecuteNonQuery();
            int returnValue = int.Parse(p6.Value.ToString());
            comm.Connection.Close();
            return returnValue;
        }


        public Client returnClient(string conn, string email)
        {

            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_Client_By_Email";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@email", email);

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            Client client = new Client();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            //List<Image> imageList = new List<Image>();
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        Image image = new Image();
            //        image.id = Int32.Parse(reader.GetString(0));
            //        image.description = reader.GetString(1);
            //        image.path = reader.GetString(2);
            //        imageList.Add(image);
            //    }
            //}
            reader.Read();
            Debug.WriteLine(reader.GetString(0));
            client.id = int.Parse(reader.GetString(0));
            client.name = reader.GetString(1);
            client.lastName = reader.GetString(2);
            //client.phone = Int32.Parse(reader.GetString(3));
            //client.creditCard = int.Parse(reader.GetString(4));
            //client.idCard = int.Parse(reader.GetString(5));
            client.creditCard = 0;
            client.idCard = 0;
            client.phone = 0;

            sqlCommand.Connection.Close();
            return client;
        }
    }
}