using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace work_01.Models
{
    public class ProductViewModel
    {
        public int productId { get; set; }
        [Required, StringLength(50, ErrorMessage = "product name is required"), Display(Name = "Product Name")]
        public string productName { get; set; }
        [Display(Name = "Category")]
        public Nullable<int> catId { get; set; }
        [Display(Name = "Price"), Column(TypeName = "money")]
        public decimal price { get; set; }
        [Display(Name = "Store Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime storeDate { get; set; }
        [Display(Name = "Picture")]
        public string picturePath { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}