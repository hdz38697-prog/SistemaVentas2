using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CDProducto
    {
        public int Idproducto { get; set; }
        public string Codigo { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fingreso { get; set; }
        public DateTime Fvencimiento { get; set; }
        public Double Pcompra { get; set; }
        public Double Pventa { get; set; }

        public int Stock { get; set; }
        public string Estado { get; set; }
        public int Idcategoria { get; set; }
        public string Buscar { get; set; }



        public DataTable Listar()
        {
            DataTable resul = new DataTable("producto");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = Conexion.Conn;
                SqlCommand Cmd = new SqlCommand("splistar_producto", conexion);
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
        public string Guardar(CDProducto prod)
        {
            string resul = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = Conexion.Conn;
                conexion.Open();

                SqlCommand Cmd = new SqlCommand("SPGuardar_producto", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                //
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@idproducto";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Direction = ParameterDirection.Output;
                Cmd.Parameters.Add(ParId);

                Cmd.Parameters.AddWithValue("@codigo", prod.Codigo);
                Cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                Cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion);
                Cmd.Parameters.AddWithValue("@f_ingreso", prod.Fingreso);
                Cmd.Parameters.AddWithValue("@f_vencimiento", prod.Fvencimiento);
                Cmd.Parameters.AddWithValue("@precio_compra", prod.Pcompra);
                Cmd.Parameters.AddWithValue("@precio_venta", prod.Pventa);
                Cmd.Parameters.AddWithValue("@stock", prod.Stock);
                Cmd.Parameters.AddWithValue("@estado", prod.Estado);
                Cmd.Parameters.AddWithValue("@idcategoria", prod.Idcategoria);

                resul = Cmd.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo insertar el registro";
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
        //metodo para EDITAR cliente
        public string Editar(CDProducto prod)
        {
            string resul = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = Conexion.Conn;
                conexion.Open();

                SqlCommand Cmd = new SqlCommand("SPEditar_Producto", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

           
                SqlParameter ParId = new SqlParameter();
                ParId.ParameterName = "@idproducto";
                ParId.SqlDbType = SqlDbType.Int;
                ParId.Value = prod.Idproducto;
                ParId.Direction = ParameterDirection.InputOutput;
                Cmd.Parameters.Add(ParId);

                Cmd.Parameters.AddWithValue("@codigo", prod.Codigo);
                Cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                Cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion);
                Cmd.Parameters.AddWithValue("@f_ingreso", prod.Fingreso);
                Cmd.Parameters.AddWithValue("@f_vencimiento", prod.Fvencimiento);
                Cmd.Parameters.AddWithValue("@precio_compra", prod.Pcompra);
                Cmd.Parameters.AddWithValue("@precio_venta", prod.Pventa);
                Cmd.Parameters.AddWithValue("@stock", prod.Stock);
                Cmd.Parameters.AddWithValue("@estado", prod.Estado);
                Cmd.Parameters.AddWithValue("@idcategoria", prod.Idcategoria);

                

                resul = Cmd.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo editar el registro";
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
        public string Eliminar(CDProducto prod)
        {
            string resul = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = Conexion.Conn;
                conexion.Open();
                SqlCommand Cmd = new SqlCommand("speliminar_producto", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@idproducto", prod.Idproducto);

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

        // metodo para buscar por cliente nombre
        public DataTable BuscarNombre(CDProducto prod)
        {
            DataTable resul = new DataTable("producto");
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = Conexion.Conn;
                SqlCommand Cmd = new SqlCommand("spbuscar_producto_nombre", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Nombre", prod.Buscar); //REVISAR

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


        public DataTable BuscarCodigo(CDProducto prod)
        {
            DataTable resul = new DataTable("producto");
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = Conexion.Conn;
                SqlCommand Cmd = new SqlCommand("spbuscar_producto_codigo", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@codigo", prod.Buscar); //REVISAR

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


    }
}
