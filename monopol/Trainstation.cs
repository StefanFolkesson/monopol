﻿namespace monopol {
    public class TrainStation : BuyableObject {
    

        // Konstruktor
        public TrainStation(string name, int position, int price, int baseRent)
            : base(name, position,  price,  baseRent) {
        }

        // Beräkna hyra baserat på antal stationer ägda av samma ägare
        public int CalculateRent() {
            int ownedStations = 0; // Hur får vi dera på detta?
            return BaseRent * (int)Math.Pow(2, ownedStations - 1); // Dubbel hyra för varje extra station
        }

        public override void DisplayInfo() {
            Console.WriteLine($"Tågstation: {Name}");
            Console.WriteLine($"Pris: {Price} kr");
            Console.WriteLine($"Grundhyra: {BaseRent} kr");
            Console.WriteLine($"Ägare: {(Owner==null? "Ingen":Owner.Name)}");
        }

    }

}