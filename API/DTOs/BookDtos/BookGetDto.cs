namespace API.DTOs.BookDtos
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CatagoryId { get; set; }
    }
}
