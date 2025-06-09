using e_commerce.Models;

namespace e_commerce.Dto
{
    public class CartViewModel
    {  
        public List<CartItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}

