using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SistemaDeVentas.Models
{
    public class LogicaAltaBaja
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ConnectionString);


        public bool ValidarCajasVaciasAlta(string Codigo, string Descripcion, string Precio, int Piezas)
        {

            bool vacio = false;

            if (Codigo == "" || Descripcion == "" || Precio == "" || Piezas == 0 )
            {
                vacio = true;
            }

            return vacio;

        }



        public void Insertar(string Codigo, string Descripcion, string Precio, int Piezas)
        {
           
                SqlCommand cmd = new SqlCommand($"Insert into Inventario values('{Codigo}','{Descripcion}','{Precio}',{Piezas},GETDATE())", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
           
        }

        public void Eliminar(string Codigo)
        {
           
                SqlCommand cmd = new SqlCommand($"Delete Inventario where Codigo = '{Codigo}'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
         
           
        }


        public DataTable ReadInventarioAlta()
        {
           
                SqlDataAdapter da = new SqlDataAdapter("Select id, Codigo, Descripcion, Precio, Piezas, Fecha from Inventario", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
          
            
        }

        public void RestarInventario(int Piezas, string Codigo)
        {

       
                SqlCommand cmd = new SqlCommand($"Update Inventario set Piezas = Piezas - {Piezas} where Codigo = '{Codigo}' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            
        }

        public void SumarInventario(int Piezas, string Codigo)
        {

                SqlCommand cmd = new SqlCommand($"Update Inventario set Piezas = Piezas + {Piezas} where Codigo = '{Codigo}' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            
        }
    }
}