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
            try
            {
                ven.CreateVentas(Codigo, Precio, Piezas);
                obj.RestarInventario(Piezas, Codigo);
                TempData["mensaje"] = "Ultima Venta: " + Piezas + " Piezas De " + Codigo;

                return View("VistaVentas", ven.ReadVentas());
            }
            catch (Exception)
            {
                throw new Exception("Campos Requeridos");
            }
           
        }

        public ActionResult BuscarPost(string Codigo)
        {
            try
            {
                return View("VistaInventario", inv.BuscarCodigo(Codigo));
            }
            catch (Exception)
            {
                throw new Exception("Codigo requerido");
            }          
        }



        public ActionResult IngresarCodigo(string Codigo, string Descripcion, string Precio, int Piezas = 0)
        {
           try
           {
           obj.Insertar(Codigo, Descripcion, Precio, Piezas);
           TempData["mensaje"] = "Se ingreso el Codigo " + Codigo;
           return View("VistaAltaBaja", obj.ReadInventarioAlta());
              }
              catch (Exception)
              {

              throw new Exception("Codigo requerido");
              }                                 
        }   

        public ActionResult EliminarCodigo(string Codigo)
        {
            try
            {
                obj.Eliminar(Codigo);
                TempData["mensaje"] = "Se eliminó el codigo " + Codigo;
                return View("VistaAltaBaja", obj.ReadInventarioAlta());
            }
            catch (Exception)
            {
                throw new Exception("Codigo requerido");
            }
        }

        public ActionResult AgregarPiezas( string Codigo, int Piezas= 0)
        {
            try
            {
                obj.SumarInventario(Piezas, Codigo);
                TempData["mensaje"] = "Se agrego " + Piezas + "Piezas";
                return View("VistaAltaBaja", obj.ReadInventarioAlta());
            }
            catch (Exception)
            {
                throw new Exception("Codigo requerido");
            }      
        }

        public ActionResult EliminarPiezas(string Codigo, int Piezas = 0)
        {
            try
            {
                obj.RestarInventario(Piezas, Codigo);
                TempData["mensaje"] = "Se Eliminó" + Piezas + "Piezas";
                return View("VistaAltaBaja", obj.ReadInventarioAlta());
            }
            catch (Exception)
            {
                throw new Exception("Codigo requerido");
            }
        }
    }
}