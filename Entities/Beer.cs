namespace WebApi.Entities
{
    public class Beer
    {
        public int BeerId { get; set; }
        public int UserId { get; set; }
        public string BeerName { get; set; }
        public string BeerDescription { get; set; }
        public int BeerRating { get; set; }
        public string BeerImageLink { get; set; }
    }
}