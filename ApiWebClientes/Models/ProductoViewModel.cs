﻿namespace ApiWebClientes.Models
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int UnidadesStock { get; set; }
        public int? CategoriaPrincipal { get; set; }
    }
}