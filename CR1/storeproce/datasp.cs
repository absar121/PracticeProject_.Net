using Google.Protobuf.WellKnownTypes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CR1.storeproce
{
    public class datasp
       
    {
        //public IConfiguration _config;

        public void CallController()
        {
            //string cs = "server=localhost,user=root,database=user,password=rootpassword,port=3307";
             string cs = "server= DESKTOP-KTJQFPG; database=userdb;Trusted_Connection=True";
            //string cs = _config.GetConnectionString("user");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("TEST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var response = new List<Value>();
                
                // SqlParameter param1 = new SqlParameter
                //{
                // ParameterName = "@id",
                // SqlDbType = SqlDbType.Int,
                //Value=3,
                //Direction=ParameterDirection.Input

                //};
                // cmd.Parameters.Add(param1);
                //await con.OpenAsync();
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                   Console.WriteLine(rd[0] + "," + rd[1] + "," + rd[2] + "," + rd[3]);
                   var x = rd.GetValue(0);
                }
            }
        }
       
    }
}
