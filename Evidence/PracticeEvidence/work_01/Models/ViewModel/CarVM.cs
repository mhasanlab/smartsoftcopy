using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace work_01.Models.ViewModel
{
    public class CarVM
    {
        public int CarId { get; set; }
        [Required, StringLength(30, ErrorMessage = "The field is required!!"), Display(Name = "Car Name")]
        public string CarName { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Required, Display(Name = "Make")]
        public int Make { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Record Date")]
        public DateTime PurchaseDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Display(Name = "Available")]
        public bool isAvailable { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        public IFormFile Pictures { get; set; }
    }
}
