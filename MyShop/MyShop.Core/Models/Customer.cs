using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }

        [StringLength(50)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        [Display(Name = "Ulica, nr domu/lokalu")]
        public string Street { get; set; }

        [StringLength(100)]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [StringLength(100)]
        [Display(Name = "Województwo")]
        public string State { get; set; }

        [StringLength(6)]
        [Display(Name = "Kod")]
        public string ZipCode { get; set; }

    }
}
