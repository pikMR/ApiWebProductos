﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiWebClientes.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<ClienteViewModel> Clientes { get; set; }
    }
}