using Google.Protobuf.WellKnownTypes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Mysqlx.Crud.Order.Types;
using Microsoft.AspNetCore.Builder;
using CR1.Model;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;

namespace CR1.storeproce
{
    public class storeproc
    {
        private readonly string _connectionString;
        public storeproc(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("user");
        }

        public async Task<List<Value>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TEST", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   // var response = new List<Value>();
                    List<Value> response = new List<Value>();
                    await sql.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Value obj = new Value();
                          
                            obj.Id = (int)reader["Id"];
                            obj.rollno = (int)reader["rollno"];
                            obj.name = reader["name"].ToString();
                            obj.department = reader["department"].ToString();
                            response.Add(obj);
                          
                        }
                    }
                    return response;
                }
            }
        }

        public async Task insert(users user)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("insertrecord", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
               
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@Cnic", user.Cnic);
                    cmd.Parameters.AddWithValue("@dob", user.dob);
                    await sql.OpenAsync();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public async Task update(int id,users user)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("updaterecord", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@Id", user));
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@Cnic", user.Cnic);
                    cmd.Parameters.AddWithValue("@dob", user.dob);
                    await sql.OpenAsync();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public async Task<ActionResult<IEnumerable<users>>> Getuser()
            
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getuser", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    List<users> response = new List<users>();
                    await sql.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users obj = new users();
                      
                            obj.userid = (int)reader["userid"];
                            obj.Age = (int)reader["Age"];
                            obj.Name = reader["Name"].ToString();
                            obj.Cnic = (int)reader["Age"];
                            obj.dob = (DateTime)reader["dob"];
                            response.Add(obj);
                      
                        }
                    }

                    return response;
                }
            }
        }

        //private Value MapToValue(SqlDataReader reader)
        //{
        //    return new Value()
        //    {
        //        Id = (int)reader["Id"],
        //        rollno = (int)reader["rollno"],
        //        name = reader["name"].ToString(),
        //        department = reader["department"].ToString()
        //    };
        //}

        public class Value
        {
            public int Id { get; set; }
            public int rollno { get; set; }
            public string name { get; set; }
            public string department { get; set; }
            // Add other properties as needed
        }
        public class users
        {
            public int userid { get; set; }
            public int Age { get; set; }
            public string Name { get; set; }
            public int Cnic { get; set; }
            public DateTime dob { get; set; }
            // Add other properties as needed
        }

        public async Task<List<users>> GetById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getuserbyid", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    //Value response = null;
                    List<users> response = new List<users>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users obj = new users();

                            obj.userid = (int)reader["userid"];
                            obj.Age = (int)reader["Age"];
                            obj.Name = reader["Name"].ToString();
                            obj.Cnic = (int)reader["Age"];
                            obj.dob = (DateTime)reader["dob"];
                            response.Add(obj);
                            // response = MapToValue(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task delete(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("deleteuser", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    await sql.OpenAsync();
                    cmd.ExecuteNonQuery();
                    
                }
            }
        }

    }
}
