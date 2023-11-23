using System.ComponentModel;

namespace ClientApp.Models.PriceControl
{
    public class Item : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }

        private decimal price;
        public decimal Price
        {
            get => price;
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
