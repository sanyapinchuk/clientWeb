namespace ClientWeb.Models
{
    public class mvcProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Default_quantity { get; set; }

        public virtual List<mvcFridge_Product> Fridge_Products { get; set; }
    }
}
