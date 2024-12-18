namespace monopol {
    public class TrainStation : BoardObject {
        public int Price { get; set; }   // Kostnad för att köpa stationen
        public int BaseRent { get; set; } // Grundhyran för en station

        // Konstruktor
        public TrainStation(string name, int position, int price, int baseRent)
            : base(name, position) {
            Price = price;
            BaseRent = baseRent;
        }

        // Beräkna hyra baserat på antal stationer ägda av samma ägare
        public int CalculateRent(int ownedStations) {
            return BaseRent * (int)Math.Pow(2, ownedStations - 1); // Dubbel hyra för varje extra station
        }

        public override void DisplayInfo() {
            Console.WriteLine($"Tågstation: {Name}");
            Console.WriteLine($"Pris: {Price} kr");
            Console.WriteLine($"Grundhyra: {BaseRent} kr");
            Console.WriteLine($"Ägare: {(Owner ?? "Ingen")}");
        }
    }

}