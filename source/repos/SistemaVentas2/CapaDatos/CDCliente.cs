using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDCliente
    {
        public int Idcliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Rfc { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }

        public string Buscar { get; set; }

        // metodo para listar clientes
        public DataTable Listar()
        {
            DataTable resul = new DataTable("cliente");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = Conexion.Conn;
                SqlCommand Cmd = new SqlCommand("splistar_cliente", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(Cmd);
                SqlDat.Fill(resul);
            }
            catch (Exception ex)
            {
                resul = null;
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resul;
        }
        //metodo para guardar cliente
        public string Guardar(CDCliente cli)
        {
            string resul = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = Conexion.Conn;
                conexion.Open();
                SqlCommand Cmd = new SqlCommand("spguardar_cliente", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@idcliente", SqlDbType.Int).Direction = ParameterDirection.Output;
                Cmd.Parameters.AddWithValue("@nombre", cli.Nombre);
                Cmd.Parameters.AddWithValue("@apellidos", cli.Apellidos);
                Cmd.Parameters.AddWithValue("@dni", cli.Dni);
                Cmd.Parameters.AddWithValue("@rfc", cli.Rfc);
                Cmd.Parameters.AddWithValue("@telefono", cli.Telefono);
                Cmd.Parameters.AddWithValue("@estado", cli.Estado);

                resul = Cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo insertar el registro";
            }
            catch (Exception ex)
            {
                resul = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resul;
        }

        //metodo para EDITAR cliente
        public string Editar(CDCliente cli)
        {
            string resul = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = Conexion.Conn;
                conexion.Open();

                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = conexion;
                Cmd.CommandText = "speditar_cliente";
                Cmd.CommandType = CommandType.StoredProcedure;
                    
                Cmd.Parameters.AddWithValue("@idcliente", cli.Idcliente);
                Cmd.Parameters.AddWithValue("@nombre", cli.Nombre);
                Cmd.Parameters.AddWithValue("@apellidos", cli.Apellidos);
                Cmd.Parameters.AddWithValue("@dni", cli.Dni);
                Cmd.Parameters.AddWithValue("@rfc", cli.Rfc);
                Cmd.Parameters.AddWithValue("@telefono", cli.Telefono);
                Cmd.Parameters.AddWithValue("@estado", cli.Estado);

                int FilasAfectadas = Cmd.ExecuteNonQuery();

                if (FilasAfectadas == 1)
                    resul = "OK";
                else
                    resul = "No se actualizó ningún registro";
            }
            catch (Exception ex)
            {
                resul = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return resul;
        }

        //metodo para eliminar cliente
        public string Eliminar(CDCliente cli)
        {
            string resul = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = Conexion.Conn;
                conexion.Open();
                SqlCommand Cmd = new SqlCommand("speliminar_cliente", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@idcliente", cli.Idcliente);

                resul = Cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo insertar el registro";

            }
            catch (Exception ex)
            {
                resul = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return resul;

        }
        //metodo para buscar por cliente nombre
        // metodo para buscar por cliente nombre
        public DataTable BuscarNombre(CDCliente cli)
        {
            DataTable resul = new DataTable("cliente");
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = Conexion.Conn;
                conexion.Open();

                SqlCommand Cmd = new SqlCommand("spbuscar_cliente_nombre", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@nombre", cli.Buscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(Cmd);
                SqlDat.Fill(resul);
            }
            catch (Exception ex)
            {
                resul = null;
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return resul;
        }

        //metodo para buscar por dni

        public DataTable BuscarDni(CDCliente cli)
        {
          DataTable resul = new DataTable("cliente");
          SqlConnection conexion = new SqlConnection();
          try
             {
              conexion.ConnectionString = Conexion.Conn;
              SqlCommand Cmd = new SqlCommand("spbuscar_cliente_dni", conexion);
              Cmd.CommandType = CommandType.StoredProcedure;
              Cmd.Parameters.AddWithValue("@dni", cli.Buscar);
              SqlDataAdapter sqlDat = new SqlDataAdapter(Cmd);
              sqlDat.Fill(resul);
              }
              catch (Exception ex)
              {
                  resul = null;
                  throw ex;
              }
              finally
              {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
              }
          return resul;
        }
    }
}