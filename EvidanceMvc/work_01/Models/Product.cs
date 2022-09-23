namespace work_01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        public int productId { get; set; }
        [Required,StringLength(50,ErrorMessage ="product name is required"),Display(Name ="Product Name")]
        public string productName { get; set; }
        [Display(Name ="Category")]
        public Nullable<int> catId { get; set; }
        [Display(Name ="Price")]
        public decimal price { get; set; }
        [Display(Name ="Store Date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public System.DateTime storeDate { get; set; }
        [Display(Name ="Picture")]
        public string picturePath { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
