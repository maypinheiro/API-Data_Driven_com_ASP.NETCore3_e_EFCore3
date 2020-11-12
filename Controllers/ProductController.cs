using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoop.Models;
using Shoop.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Shoop.controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await
            context.
            Products.
            Include(x => x.Category) // se quisermos apenas o Id não precisa deste include, toda vez que usamos um include ele da umjoim em duas tabelas e ist é mas custoso // podemos ter mais de um include , ele é composto de uma função.       Ex : Include(x=> x.aquiVemTodasAsFuncoes)
            .AsNoTracking().
            ToListAsync();
            return products;

        }
        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]


        public async Task<ActionResult<Product>> GetId([FromServices] DataContext context, int id)
        {
            var product = await context
               .Products
               .Include(x => x.Category)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == id);
            return product;

        }
        [HttpGet]
        [Route("categories/{id:int}")]  //products/categories/1
        [AllowAnonymous]

        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context
                .Products
                .Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .ToListAsync();
            return products;

        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]

        public async Task<ActionResult<Product>> Post(
            [FromServices] DataContext contex,
            [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                contex.Products.Add(model);
                await contex.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}