using Microsoft.AspNetCore.Mvc;
using OnlineShop.Connection;
using OnlineShop.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OnlineShop.Data
{
    public class ProductData
    {
        ConnectionDB cn = new ConnectionDB();

        public async Task<List<productModel>> listProducts()
        {
            var list = new List<productModel>();
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                using (var cmd = new SqlCommand("listProducts", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            var product = new productModel((string)reader["name"], (string)reader["description"], (decimal)reader["price"]);
                            list.Add(product);
                        }
                        return list;
                    }
                }
            }
        }

        public async Task insertProduct(productModel parameters)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                using (var cmd = new SqlCommand("insertProduct", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", parameters.name);
                    cmd.Parameters.AddWithValue("description", parameters.description);
                    cmd.Parameters.AddWithValue("price", parameters.price);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateProduct(productModel parameters)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                using (var cmd = new SqlCommand("updateProduct", sql)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", parameters.id);
                    cmd.Parameters.AddWithValue("name", parameters.name);
                    cmd.Parameters.AddWithValue("description", parameters.description);
                    cmd.Parameters.AddWithValue("price", parameters.price);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteProduct(int id)
        {
            using(var sql = new SqlConnection(cn.ConnectionString()))
            {
                using (var cmd = new SqlCommand("deleteProduct", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<productModel>> listProductById(int id)
        {
            var list = new List<productModel>();
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                using (var cmd = new SqlCommand("listProductById", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var product = new productModel((string)reader["name"], (string)reader["description"], (decimal)reader["price"]);
                            list.Add(product);
                        }
                        return list;
                    }
                }
            }
        }
    }
}
