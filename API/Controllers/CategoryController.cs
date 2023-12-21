using API.DAL;
using API.DTOs.CatagoryDtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _appDb;

    public CategoriesController(AppDbContext appDb)
    {
        this._appDb = appDb;
    }

    [HttpGet("")]
    public IActionResult GetAll()
    {
        List<Category> categories = _appDb.Catagories.ToList();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var catagory = _appDb.Catagories.Find(id);

        if (catagory is null) return NotFound();


        return Ok(catagory);
    }

    [HttpPost]
    public IActionResult Create([FromForm]CatagoryCreateDto dto)
    {
        Category catagory = new Category
        {
            Name = dto.Name,
        };
        catagory.CreatedDate = DateTime.UtcNow.AddHours(4);
        catagory.UpdatedDate = DateTime.UtcNow.AddHours(4);
        catagory.IsDeleted = false;
        _appDb.Catagories.Add(catagory);
        _appDb.SaveChanges();

        return StatusCode(201);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CatagoryUpdateDto updateDto)
    {
        var categoryToUpdate = _appDb.Catagories.FirstOrDefault(x => x.Id == id);

        if (categoryToUpdate != null)
        {

            categoryToUpdate.Name = updateDto.Name;
            categoryToUpdate.CreatedDate = DateTime.UtcNow.
                AddHours(4);
            categoryToUpdate.UpdatedDate = DateTime.UtcNow.
                AddHours(4);
            _appDb.SaveChanges();
            return Ok(categoryToUpdate);
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var catagoryToDelete = _appDb.Catagories.FirstOrDefault(x => x.Id == id);

        if (catagoryToDelete != null)
        {
            _appDb.Remove(catagoryToDelete);
            _appDb.SaveChanges();
            return Ok("Catagory deleted");
        }

        return NotFound();
    }
    [HttpPatch("{id}")]
    public IActionResult SoftDelete(int id)
    {
        var catagoryToSoftDelete = _appDb.Catagories.FirstOrDefault(x => x.Id == id);

        if (catagoryToSoftDelete != null)
        {   
            catagoryToSoftDelete.IsDeleted = true;
            _appDb.SaveChanges();
            return Ok("Successfully soft deleted ");
        }

        return NotFound();
    }
}
