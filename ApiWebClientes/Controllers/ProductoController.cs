using ApiWebClientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ApiWebClientes
{
    
    public class ProductoController : ApiController 
    {
        
         public IHttpActionResult GetAllProductos()
        {
            
            System.Diagnostics.Debug.WriteLine("SystraceMethod:GetAllProductoS()");
            List<ProductoViewModel> productos = new List<ProductoViewModel>();
            
            using (var ctx = new bdclientesEntities())
            {

                productos =
                ctx.Producto.Select(p => new ProductoViewModel()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CategoriaPrincipal = p.CategoriaPrincial
                }).ToList<ProductoViewModel>();

                if (productos.Count() == 0)
                    return NotFound();

                return Ok(productos);
            }
            
        }

        public IHttpActionResult GetAllProductosCat(int cat)
        {

            System.Diagnostics.Debug.WriteLine("SystraceMethod:GetAllProductoS()");
            List<ProductoViewModel> productos = new List<ProductoViewModel>();

            using (var ctx = new bdclientesEntities())
            {

                productos =
                ctx.Producto.Select(p => new ProductoViewModel()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CategoriaPrincipal = p.CategoriaPrincial,
                    UnidadesStock = p.UnidadesStock,
                    Precio = p.Precio
                }).Where(c => c.CategoriaPrincipal == cat).ToList<ProductoViewModel>();

                if (productos.Count() == 0)
                    return NotFound();

                return Ok(productos);
            }

        }

        public IHttpActionResult GetProducto(int id)
        {
            ProductoViewModel producto = new ProductoViewModel();
            using (var ctx = new bdclientesEntities())
            {
                producto =
                ctx.Producto.Select(p => new ProductoViewModel()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CategoriaPrincipal = p.CategoriaPrincial
                }).Where(d => d.Id == id).FirstOrDefault();

                if (producto==null)
                    return NotFound();

                return Ok(producto);
            }

        }


        public IHttpActionResult AddNewProducto(ProductoViewModel producto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dato no conocido.");

            using (var ctx = new bdclientesEntities())
            {
                ICollection<Categoria> categorias = new List<Categoria>();
                categorias.Add(ctx.Categoria.Find(producto.CategoriaPrincipal));
                ctx.Producto.Add(new Producto()
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    FechaAlta = DateTime.Now,
                    Precio = producto.Precio,
                    CategoriaPrincial = producto.CategoriaPrincipal,
                    Categoria = categorias
                });

                ctx.SaveChanges();
            }
            return Ok();
        }

        public IHttpActionResult SetProducto(ProductoViewModel producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dato no conocido.");
            }

            using (var ctx = new bdclientesEntities())
            {
                Producto nuevoProd = ctx.Producto.Find(producto.Id);

                if (producto != null)
                {
                    nuevoProd.Nombre = producto.Nombre;
                    nuevoProd.Precio = producto.Precio;
                    nuevoProd.UnidadesStock = producto.UnidadesStock;
                }
                else
                {
                    return NotFound();
                }

                return Ok(producto);
            }

        }

        public IHttpActionResult DelProducto(int id)
        {
            if (id <= 0)
                return BadRequest("No es un producto Valido");

            using (var ctx = new bdclientesEntities())
            {
                var producto = ctx.Producto.Where(s => s.Id == id).FirstOrDefault();
                ctx.Entry(producto).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }

    }
}
