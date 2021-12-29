using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDeVentas.Models
{
    public class LogicaInventario
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString);
        public void CreateInventario(string Codigo, string Precio, int Piezas)
        {     
                SqlCommand cmd = new SqlCommand($"Insert into Inventario values('{Codigo}','{Precio}',{Piezas},GETDATE())", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();              
        }

        public DataTable ReadInventario()
        {         
                SqlDataAdapter da = new SqlDataAdapter("Select id, Codigo, Precio, Piezas, Fecha from Inventario", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;        
        }

        public DataTable BuscarCodigo(string Codigo)
        {         
                SqlDataAdapter da = new SqlDataAdapter($"Select * From Inventario where Codigo = '{Codigo}'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;               
        }


    }
}