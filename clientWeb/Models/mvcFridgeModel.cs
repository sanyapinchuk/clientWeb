namespace ClientWeb.Models
{
    public class mvcFridgeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int? Year { get; set; }

        public virtual List<mvcFridge> Fridges { get; set; }
    }
}
