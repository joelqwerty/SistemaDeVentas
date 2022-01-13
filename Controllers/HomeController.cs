using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeVentas.Models;
using System.Data;

namespace SistemaDeVentas.Controllers
{
    public class HomeController : Controller
    {

        LogicaVentas ven = new LogicaVentas();
        LogicaInventario inv = new LogicaInventario();
        LogicaAltaBaja obj = new LogicaAltaBaja();
        

        public ActionResult VistaInventario()
        {
            DataTable dt = new DataTable();
            dt = inv.ReadInventario();

            return View(dt);
        }
        public ActionResult VistaAltaBaja()
        {
            DataTable dt = new DataTable();
            dt = obj.ReadInventarioAlta();

            return View(dt);
        }
        public ActionResult VistaVentas()
        {
            DataTable dt = new DataTable();
            dt = ven.ReadVentas();

            return View(dt);
        }


        public ActionResult VentasPost(string Codigo, string Precio, int Piezas = 0)
        {



            if (ven.ValidarCajasVacias(Codigo, Precio, Piezas) == true)
            {

                TempData["MensajeCampos"] = "Todos los campos son obligatorios";

            }

            else 
            {

                ven.CreateVentas(Codigo, Precio, Piezas);
                obj.RestarInventario(Piezas, Codigo);
                TempData["mensaje"] = "Ultima Venta: " + Piezas + " Piezas De " + Codigo;


            }
           

                return View("VistaVentas", ven.ReadVentas());
            
           
        }

        public ActionResult BuscarPost(string Codigo)
        {


            if (inv.ValidarCajasVacias(Codigo) == true)
            {

                TempData["MensajeCampos"] = "El Codigo es Obligatorio";

            }

            else
            {
                inv.BuscarCodigo(Codigo);
            }

                return View("VistaInventario", inv.BuscarCodigo(Codigo));
            
            
        }



        public ActionResult IngresarCodigo(string Codigo, string Descripcion, string Precio, int Piezas = 0)
        {

            if (obj.ValidarCajasVaciasAlta(Codigo, Descripcion, Precio, Piezas) == true)
            {

                TempData["MensajeCampos"] = "Todos los campos son obligatorios";

            }

            else
            {
                obj.Insertar(Codigo, Descripcion, Precio, Piezas);
                TempData["Mensaje"] = "Se ingreso el Codigo " + Codigo;
            }

           return View("VistaAltaBaja", obj.ReadInventarioAlta());
                                     
        }   

        public ActionResult EliminarCodigo(string Codigo)
        {

            if (obj.ValidarCajasVaciasAlta2(Codigo) == true)
            {

                TempData["MensajeCamposEliminar"] = "Es idispensable el codigo";

            }

            else
            {
                obj.Eliminar(Codigo);
                TempData["Mensaje2"] = "Se eliminó el codigo " + Codigo;
            }
            
                return View("VistaAltaBaja", obj.ReadInventarioAlta());         
            
        }

        public ActionResult AgregarPiezas( string Codigo, int Piezas= 0)
        {

            if (obj.ValidarCajasVaciasAlta3(Codigo, Piezas) == true)
            {

                TempData["MensajeCamposAgregar"] = "Los Campos son Indispensables";

            }

            else
            {
                obj.SumarInventario(Piezas, Codigo);
                TempData["Mensaje3"] = "Se agrego " + Piezas + "Piezas";
               
            }

                return View("VistaAltaBaja", obj.ReadInventarioAlta());
             
        }






        public ActionResult EliminarPiezas(string Codigo, int Piezas = 0)
        {



            if (obj.ValidarCajasVaciasAlta3(Codigo, Piezas) == true)
            {

                TempData["MensajeEliminarP"] = "Los Campos son Indispensables";

            }

            else
            {
                obj.RestarInventario(Piezas, Codigo);
                TempData["Mensaje4"] = "Se Eliminó" + Piezas + "Piezas";

            }

                return View("VistaAltaBaja", obj.ReadInventarioAlta());
            
        }
    }
}