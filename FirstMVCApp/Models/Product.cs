using System.ComponentModel;
using System.ComponentModel.DataAnnotations; 

namespace FirstMVCApp.Models
{
    public class Product
    {
        [DisplayName("Product Id")]
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is required.")]
        [MaxLength(25, ErrorMessage ="Product name cannot exceed 25 chars")]
        [MinLength(4, ErrorMessage ="Product name should be 4 or more chars")]
        public string ProductName { get; set; }

        [DisplayName("Stock Level")]
        [Required(ErrorMessage = "Stock Level should be entered")]
        [Range(minimum:0, maximum:5000, ErrorMessage ="Stock Level should be 0 to 5000")]
        public short UnitsInStock { get; set; }

        [DisplayName("Item Price")]
        [Required(ErrorMessage = "Item Price is required.")]
        [Range(minimum: 0, maximum: 5000, ErrorMessage = "Item Price should be 0 to 5000")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Is Discontinued?")]
        public bool Discontinued { get; set; }
    }
    //Northwind DB - Products Table 
}
