using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product
    {
        [Key]
        public String Id { get; set; }

        [StringLength(20)]
        [Display(Name="Product name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Display(Name = "Cena")]
        public decimal Price{ get; set; }
        public string Category{ get; set; }
        public string Image { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
