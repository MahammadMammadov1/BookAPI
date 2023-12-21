using API.DAL;
using API.DTOs.BookDtos;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _appDb;

        public BooksController(AppDbContext appDb)
        {
            this._appDb = appDb;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<Book> books = _appDb.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _appDb.Books.Find(id);

            if (book is null) return NotFound();

            BookGetDto dto = new BookGetDto()
            {
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                CatagoryId = book.CatagoryId
            };

            return Ok(dto);

        }

        [HttpPost]
        public IActionResult Create(BookCreateDto dto)
        {
            Book book = new Book
            {
                Name = dto.Name,
                Price = dto.Price,
                CostPrice = dto.CostPrice,
                CatagoryId = dto.CatagoryId
            };


            book.CreatedDate = DateTime.UtcNow.AddHours(4);
            book.UpdatedDate = DateTime.UtcNow.AddHours(4);
            book.IsDeleted = false;
            _appDb.Books.Add(book);
            _appDb.SaveChanges();

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookUpdateDto updateDto)
        {
            var bookToUpdate = _appDb.Books.FirstOrDefault(x => x.Id == id);

            if (bookToUpdate != null)
            {
                bookToUpdate.Name = updateDto.Name;
                bookToUpdate.Price = updateDto.Price;
                bookToUpdate.CostPrice = updateDto.CostPrice;
                bookToUpdate.CatagoryId = updateDto.CatagoryId;

                _appDb.SaveChanges(); 
                return Ok(bookToUpdate); 
            }

            return NotFound(); 
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookToDelete = _appDb.Books.FirstOrDefault(x => x.Id == id);

            if (bookToDelete != null)
            {
                _appDb.Remove(bookToDelete);
                _appDb.SaveChanges();
                return Ok("Successfully Delete");
            }

            return NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult SoftDelete(int id)
        {
            var bookToSoftDelete = _appDb.Books.FirstOrDefault(x => x.Id == id);

            if (bookToSoftDelete != null)
            {
                bookToSoftDelete.IsDeleted = true;
                _appDb.SaveChanges();
                return Ok("Successfully soft deleted ");
            }

            return NotFound();
        }





    }
}
