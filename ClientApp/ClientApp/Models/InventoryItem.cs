namespace ClientApp.Models.InventoryControl
{
    public class Item
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Quantity { get; set; }
        public string? Category { get; set; }
        public int DaysSinceLastOrder { get; set; }

        public override string ToString()
        {
            return this.Id + ", "
                + this.Name + ", "
                + this.Category + ", "
                + "quantity: " + this.Quantity + ", "
                + "last ordered " + this.DaysSinceLastOrder + " days ago";
        }
    }
}
