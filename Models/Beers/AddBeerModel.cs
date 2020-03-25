using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Beers
{
    public class AddBeerModel
    {
        [Required]
        public int UserId { get; set; }
        public string BeerName { get; set; }
        public string BeerDescription { get; set; }
        public int BeerRating { get; set; }
        public string BeerImageLink { get; set; }
    }
}