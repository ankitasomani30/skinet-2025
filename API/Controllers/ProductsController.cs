using System;
using API.RequestHelper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);


        return await CreatePageResult(repository, spec, specParams.PageIndex, specParams.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.Add(product);

        if(await repository.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct", new {id = product.Id}, product);
        }

        return BadRequest("Failed to create product.");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if(product.Id != id || !repository.Exists(id))
        {
            return BadRequest("Cannot update this product.");
        }
        
        repository.Update(product);

        if(await repository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Failed to update product.");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product =  await repository.GetByIdAsync(id);
        if(product == null)
        {
            return NotFound();
        }
        repository.Remove(product);

        if(await repository.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Failed to delete product.");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var brands = new BrandListSpecification();

        return Ok(await repository.ListAsync(brands));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var typeSpecification = new TypeListSpecification();

        return Ok(await repository.ListAsync(typeSpecification));
    }

}
