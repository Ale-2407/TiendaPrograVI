using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Tienda_PrograVI.Models;

namespace Tienda_PrograVI.Data
{
    public class ProductoADORepository
    {
        private readonly string _connectionString = "Server=.;Database=TIenda_Ropa;User Id=sa;Password=Alejandra25;TrustServerCertificate=True;";
        public List<Producto> GetAll()
        {
            var lista = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListarPruducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Producto
                    {
                        Id_producto = (int)reader["IdProducto"],
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = (int)reader["Precio"],
                        Stock = (int)reader["Stock"],
                        Id_categoria = (int)reader["IdCategoria"]

                    });
                }
            }
            return lista;
        }
        public void Insert(Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GuardarProducto", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = producto.Nombre;
                    cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 250).Value = producto.Descripcion;
                    cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = producto.Precio;
                    cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = producto.Stock;
                    cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = producto.Id_categoria;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al guardar el producto.", ex);
                }
            }
        }



        public Producto GetById(int id)
        {
            Producto est = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListarProductoPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    est = new Producto
                    {
                        Id_producto = (int)reader["IdProducto"],
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = (int)reader["Precio"],
                        Stock = (int)reader["Stock"],
                        Id_categoria = (int)reader["IdCategoria"]

                    };
                }
            }
            return est;
        }
        public void Update(Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ActualizarProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.Parameters.AddWithValue("@IdCategoria", producto.Id_categoria);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int Id_producto)
        {
            Producto producto = null;
            producto = GetById(Id_producto);
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("EliminarProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", Id_producto);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}