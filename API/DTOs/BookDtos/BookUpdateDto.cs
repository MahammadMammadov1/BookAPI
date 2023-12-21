namespace API.DTOs.BookDtos
{
    public class BookUpdateDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double CostPrice { get; set; }
        public int CatagoryId { get; set; }
    }
}
