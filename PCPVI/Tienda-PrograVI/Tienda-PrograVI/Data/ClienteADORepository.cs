using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Tienda_PrograVI.Models;

namespace Tienda_PrograVI.Data
{
    public class ClienteADORepository
    { 
     private readonly string _connectionString = "Server=.;Database=TIenda_Ropa;User Id=sa;Password=Alejandra25;TrustServerCertificate=True;";
    public List<Cliente> GetAll()
    {
        var lista = new List<Cliente>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("ListarCliente", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    Id_cliente = (int)reader["IdCliente"],
                    Nombre = reader["Nombre"].ToString(),
                    Correo = reader["Correo"].ToString(),
                    Contraseña = reader["Contraseña"].ToString(),
                    Direccion = reader["Direccion"].ToString()
                });
            }
        }
        return lista;
    }
    public void Insert(Cliente cliente)
    {
        int newId = 0;
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
                string sql = "EXEC GuardarCliente @Nombre, @Correo,@Contraseña,@Direccion; SELECT MAX(ID) FROM Cliente; ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))

            {
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@Contraseña", cliente.Contraseña);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);

                conn.Open();
                object result = cmd.ExecuteScalar();
                newId = Convert.ToInt32(result);
            }
        }
    }

    public Cliente GetById(int id)
    {
        Cliente est = null;
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("ListarClientePorId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                est = new Cliente
                {
                    Id_cliente = (int)reader["IdCliente"],
                    Nombre = reader["Nombre"].ToString(),
                    Correo = reader["Correo"].ToString(),
                    Contraseña = reader["Contraseña"].ToString(),
                    Direccion = reader["Direccion"].ToString()
                };
            }
        }
        return est;
    }
    public void Update(Cliente clientes)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("ActualizarCliente", conn);
            cmd.Parameters.AddWithValue("@Nombre", clientes.Nombre);
            cmd.Parameters.AddWithValue("@Correo", clientes.Correo);
            cmd.Parameters.AddWithValue("@Contraseña", clientes.Contraseña);
            cmd.Parameters.AddWithValue("@Direccion", clientes.Direccion);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int Id_cliente)
    {
        Cliente clientes = null;
        clientes = GetById(Id_cliente);
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("EliminarCliente", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_cliente", Id_cliente);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
}