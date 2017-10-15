using ApiWebClientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
namespace ApiWebClientes.Controllers
{
    public class MVCProductoController : Controller
    {
        private String URL_DELETE = "api/producto/";
        // GET: MVCProducto
        public ActionResult Index()
        {
            IList<ProductoViewModel> productos = null;
            
            using (var producto = new HttpClient())
            {
                String strPathAndQuery = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
                producto.BaseAddress = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/") + URL_DELETE);
                //HTTP GET
                var responseTask = producto.GetAsync("producto");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductoViewModel>>();
                    readTask.Wait();

                    productos = readTask.Result;
                }
            }

            return View(productos);
        }

        // GET: MVCProducto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MVCProducto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVCProducto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MVCProducto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MVCProducto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MVCProducto/Delete/5
        public ActionResult Delete(int id)
        {
            using (var producto = new HttpClient())
            {
                producto.BaseAddress = new Uri("http://localhost:64189/api/");

                //HTTP DELETE
                var deleteTask = producto.DeleteAsync("producto/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        // POST: MVCProducto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
