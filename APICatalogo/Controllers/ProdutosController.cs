using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using APICatalogo.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
    }
}
