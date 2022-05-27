namespace ClientWeb.Models
{
    public class mvcFridge
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Owner_name { get; set; }

        public virtual List<mvcFridge_Product>? Fridge_Products { get; set; }

        public int FridgeModelId { get; set; }
        public virtual mvcFridgeModel? FridgeModel { get; set; }
    }
}
