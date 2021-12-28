using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaDeVentas.Models
{
    public class LogicaVentas
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString);

        public void CreateVentas(string Codigo, string Precio, int Piezas)
        { 
                SqlCommand cmd = new SqlCommand($"Insert into Ventas values('{Codigo}','{Precio}',{Piezas},GETDATE())", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
        }


        public void RestarInventario(int Piezas, string Codigo)
        {                   
                SqlCommand cmd = new SqlCommand($"Update Inventario set Piezas = Piezas - {Piezas} where Codigo = '{Codigo}' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();      
        }


        public DataTable ReadVentas()
        {      
                SqlDataAdapter da = new SqlDataAdapter("Select id, Codigo, Precio, Piezas, Fecha from Ventas ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;      
        }


        public bool ValidarCajasVacias(string Codigo, string Precio, int Piezas)
        {      
                bool vacio = false;
                if (Codigo == "" || Precio == "" || Piezas == 0)
                {
                    vacio = true;
                }

                return vacio;           
        }
    }
}