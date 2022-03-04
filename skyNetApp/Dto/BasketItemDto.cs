using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Dto
{
    public class BasketItemDto
    {
        [Required]
        public int id { get; set; }

       [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(1,double.MaxValue,ErrorMessage ="quantity must be large than zero")]
        public int Quantity { get; set; }
        [Required]
        [Range(.1, double.MaxValue, ErrorMessage = "Price must be large than zero")]
        public decimal Price { get; set; }
        [Required]

        public string PictureUrl { get; set; }
       
        [Required]
        public string Type { get; set; }
        [Required]
        public string Brand { get; set; }



    }
}
