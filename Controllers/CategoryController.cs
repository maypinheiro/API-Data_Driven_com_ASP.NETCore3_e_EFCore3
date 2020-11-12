using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shoop.Data;
using Shoop.Models;
using Microsoft.AspNetCore.Authorization;


//https:// localhost:5001/categories
[Route("v1/categories")]
public class CategoryController : ControllerBase
{

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
    //[ResponseCache(VaryByHeader="User-Agent",Location= ResponseCacheLocation.Any,Duration=30)]
    public async Task<ActionResult<List<Category>>> Get(
        [FromServices] DataContext context
    )

    {
        var categories = await context.Categories.AsNoTracking().ToListAsync();
        return Ok(categories);
    }

    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Category>> GetById(
        int id,
    [FromServices] DataContext context)
    {
        var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return category;
    }


    [HttpPost]
    [Route("")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Post([FromBody] Category model,
    [FromServices] DataContext context
    )
    {

        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            context.Categories.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch
        {
            return BadRequest(new { message = "Não foi possivel criar uma nova Categria" });
        }
    }


    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Put(
    int id,
     [FromBody] Category model,
     [FromServices] DataContext context)
    {

        // Verifica se o ID é o mesmo do modelo 
        if (id != model.Id)
            return NotFound(new { message = "Categoria não encontrada verifique o ID" });

        // Verifica se os dados são validos.
        if (!ModelState.IsValid)
            return BadRequest(ModelState);  // ASSIM MANDA OS ERROS
                                            // return BadRequest(new { message = "Categoria não encontrada" });  ASSIM MANDA UMA MSG try

        try
        {
            context.Entry<Category>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(new { message = "Não foi possivel atualizar a categoria, categoria já atualizada...." });
            // este é um erro de concorrencia quando estamos tentando atualizar e outra pessoa esta atualizando ao mesmo tempo.

        }
        catch (Exception)
        {
            return BadRequest(new { message = "Não foi possivel atualizar a categoria" });

        }

    }


    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Delete(int id,
    [FromServices] DataContext context
    )
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        //o FirstOrDefaultAsync busca uma categoria dada uma  expressão , se ela encontrar mais de uma categoria traz a primeira, se encontrar 1 traz ela s não encontrar nada traz nullo.
        if (category == null)
            return BadRequest(new { message = "Categoria não encontrada" });


        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            // return Ok (category);
            //podemos tbm passar uma mensagem 
            return Ok(new { messagem = "Categoria removida com Sucesso!" });
        }
        catch (Exception)
        {
            return BadRequest(new { message = " Não foi ossivel remover a categoria" });
        }
    }

}