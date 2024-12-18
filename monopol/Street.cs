﻿namespace monopol {
    public class Street : BoardObject {
        public string ColorGroup { get; set; }   // Färggruppen
        public int Price { get; set; }           // Kostnad för att köpa gatan
        public int Rent { get; set; }            // Grundhyra
        public int HouseCost { get; set; }       // Kostnad för att bygga hus
        public int Houses { get; private set; }  // Antal hus
        public bool HasHotel { get; private set; }

        // Konstruktor
        public Street(string name, int position, string colorGroup, int price, int rent, int houseCost)
            : base(name, position) {
            ColorGroup = colorGroup;
            Price = price;
            Rent = rent;
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

        // Beräkna hyra
        public int CalculateRent() {
            if (HasHotel)
                return Rent * 5; // Exempel: hotell ger fem gånger grundhyran
            return Rent + (Houses * (Rent / 2)); // Varje hus ökar hyran
        }

        public override void DisplayInfo() {
            Console.WriteLine($"Gata: {Name}");
            Console.WriteLine($"Färggrupp: {ColorGroup}");
            Console.WriteLine($"Pris: {Price} kr");
            Console.WriteLine($"Grundhyra: {Rent} kr");
            Console.WriteLine($"Antal hus: {Houses}, Hotell: {HasHotel}");
            Console.WriteLine($"Ägare: {(Owner ?? "Ingen")}");
        }
    }

}