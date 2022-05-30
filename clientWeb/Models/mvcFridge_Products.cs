namespace ClientWeb.Models
{
    public class mvcFridge_Product
    {
        public int Id { get; set; }

        public int FridgeId { get; set; }
        public virtual mvcFridge? Fridge { get; set; }

        public int ProductId { get; set; }
        public virtual mvcProduct? Product { get; set; }

        public int Quantity { get; set; }
    }
}
