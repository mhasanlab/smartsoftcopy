using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace work_01.Models
{
    public class Car
    {
        public int CarId { get; set; }
        [Required, StringLength(30, ErrorMessage = "The field is required!!"), Display(Name = "Car Name")]
        public string CarName { get; set; }
        [ForeignKey("Customer"),Display(Name ="Customer")]
        public int CustomerId { get; set; }
        [Required, Display(Name = "Make")]
        public int Make { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Record Date")]
        public DateTime PurchaseDate { get; set; }
        [Required, Column(TypeName ="money")]
        public decimal Price { get; set; }
        [Display(Name = "Available")]
        public bool isAvailable { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public class Customer 
    {
        public int CustomerId { get; set; }
        [Required, StringLength(50), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Mobile { get; set; }
        public virtual ICollection<Car> Cars{ get; set; }
    }

    public class CarRentDbContext : DbContext
    {
        public CarRentDbContext(DbContextOptions<CarRentDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
