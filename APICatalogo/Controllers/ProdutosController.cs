using APICatalogo.Context;
using APICatalogo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // [HttpGet("primeiro")]
        // [HttpGet("teste")]
        // [HttpGet("/primeiro")]
        [HttpGet("{valor:alpha:length(5)}")]
        public async Task<ActionResult<Produto>> Get2Async()
        {
            return await _context.Produtos.FirstOrDefaultAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            if (produtos is null)
            {
                return NotFound();
            }
            return produtos;
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetAsync([FromQuery] int id)
        {
            var produto = await _context
                .Produtos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult(
                "ObterProduto",
                new { id = produto.ProdutoId },
                produto
            );
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
