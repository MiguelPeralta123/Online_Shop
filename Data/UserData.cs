using OnlineShop.Connection;
using OnlineShop.Models;
using System.Data;
using System.Data.SqlClient;

namespace OnlineShop.Data
{
    public class UserData
    {
        ConnectionDB cn = new ConnectionDB();

        public async Task<List<userModel>> GetUsers()
        {
            var list = new List<userModel>();
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                using(var cmd = new SqlCommand("getUsers", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await sql.OpenAsync();
                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new userModel((string)reader["username"], (string)reader["password"]);
                            list.Add(user);
                        }
                        return list;
                    }
                }
            }
        }

        public async Task postUser(userModel parameters) 
        {
            using (var sql = new SqlConnection(cn.ConnectionString())) 
            {
                using(var cmd = new SqlCommand("postUser", sql)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("username", parameters.username);
                    cmd.Parameters.AddWithValue("password", parameters.password);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task putUser(userModel parameters) 
        {
            using(var sql = new SqlConnection(cn.ConnectionString())) 
            {
                using(var cmd = new SqlCommand("putUser", sql)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", parameters.id);
                    cmd.Parameters.AddWithValue("username", parameters.username);
                    cmd.Parameters.AddWithValue("password", parameters.password);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteUser(int id) 
        {
            using(var sql = new SqlConnection(cn.ConnectionString())) 
            {
                using(var cmd = new SqlCommand("deleteUser", sql)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<userModel>> getUserById(int id)
        {
            var list = new List<userModel>();
            using(var sql = new SqlConnection(cn.ConnectionString()))
            {
                using(var cmd = new SqlCommand("getUserById", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    await sql.OpenAsync();
                    using(var reader = await cmd.ExecuteReaderAsync()) 
                    {
                        while(await reader.ReadAsync()) 
                        {
                            var user = new userModel((string)reader["username"], (string)reader["password"]);
                            list.Add(user);
                        }
                        return list;
                    }
                }
            }
        }
    }
}
