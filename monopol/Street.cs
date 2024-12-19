namespace monopol {
    public class Street : BuyableObject {
        public string ColorGroup { get; set; }   // Färggruppen
        public int HouseCost { get; set; }       // Kostnad för att bygga hus
        public int Houses { get; private set; }  // Antal hus
        public bool HasHotel { get; private set; }

        // Konstruktor
        public Street(string name, int position, string colorGroup, int price, int baseRent, int houseCost, int hotelcost, int mortgage)
            : base(name, position,price,baseRent) {
            ColorGroup = colorGroup;
            HouseCost = houseCost;
            Houses = 0;
            HasHotel = false;
        }

        // Bygg hus
        public void BuildHouse() {
            if (Houses < 4 && !HasHotel) {
                Houses++;
            } else if (Houses == 4 && !HasHotel) {
                HasHotel = true;
                Houses = 0;
            }
        }

        public void SellHouse() {
            if (HasHotel) {
                HasHotel = false;
                Houses = 4;
            } else if (Houses > 0) {
                Houses--;
            }
        }



        // Beräkna hyra
        public int CalculateRent() {
            if (HasHotel)
                return BaseRent * 5; // Exempel: hotell ger fem gånger grundhyran
            return BaseRent + (Houses * (BaseRent / 2)); // Varje hus ökar hyran
        }

        public override void DisplayInfo() {
            Console.WriteLine($"Gata: {Name}");
            Console.WriteLine($"Färggrupp: {ColorGroup}");
            Console.WriteLine($"Pris: {Price} kr");
            if (IsMortgaged) {
                Console.WriteLine($"Pantvärde: {Price} kr");
                Console.WriteLine($"Hyra: {0} kr");
                Console.WriteLine($"Antal hus: {Houses}, Hotell: {HasHotel}");
            }
            Console.WriteLine($"Hyra: {CalculateRent()} kr");
            Console.WriteLine($"Antal hus: {Houses}, Hotell: {HasHotel}");
            Console.WriteLine($"Ägare: {(Owner==null?"Ingen":Owner.Name)}");
        }
    }

}