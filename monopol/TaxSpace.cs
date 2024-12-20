using System.Xml.Linq;

namespace monopol {
    internal class TaxSpace : BoardSpace {

        private int price;
        public int Price { get => price; set => price = value; }
        public TaxSpace(string name, int position, int price) : base(name, position) {
            Price = price;
        }
        public override void DisplayInfo() {
            // show data in output window

            Console.WriteLine($"Chans: {Name}");
            Console.WriteLine($"Position: {Position}");
        }
    }
}