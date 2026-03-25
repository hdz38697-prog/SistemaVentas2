
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace CapaNegocio
{
    public class CNCategoria
    {
        public static DataTable Listar()
        {
            CDCategoria Datos = new CDCategoria();
            return Datos.Listar();
        }
        public static DataTable ListarCmbCategoria()
        {
            CDCategoria datos = new CDCategoria();
            return datos.Listar();
        }

        //METODO PARA GUARDAR CLIENTE
        public static string Guardar(string descripcion)
        {
            CDCategoria Datos = new CDCategoria();
            Datos.Descripcion = descripcion;
            return Datos.Guardar(Datos);
        }

        //METODO PARA EDITAR CLIENTE
        public static string Editar(int idcategoria, string descripcion)
        {
            CDCategoria Datos = new CDCategoria();
            Datos.IdCategoria = idcategoria;   
            Datos.Descripcion = descripcion;
            return Datos.Editar(Datos);
        }

        //metodo para eliminar cliente
        public static string Eliminar(int idcategoria)
        {
            CDCategoria Datos = new CDCategoria();
            Datos.IdCategoria = idcategoria;
            return Datos.Eliminar(Datos);
        }
        //metodo buscar por nombre 
        public static DataTable BuscarNombre(string textobuscar)
        {
            CDCategoria Datos = new CDCategoria();
            Datos.Buscar = textobuscar;
            return Datos.BuscarNombre(Datos);
        }
        
    }
}
