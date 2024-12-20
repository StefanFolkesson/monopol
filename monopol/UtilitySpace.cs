using System.Diagnostics;
using System.Windows;

namespace monopol {
    public class UtilitySpace : BuyableSpace {
        // Konstruktor
        public UtilitySpace(string name, int position, int price, int baseRent)
            : base(name, position, baseRent, price) {

        }

        // Beräkna hyra baserat på antal stationer ägda av samma ägare
        public int CalculateRent() {
            int ownedUtilities = 0; // Hur får vi dera på detta?
            return BaseRent * (int)Math.Pow(2, ownedUtilities - 1); // Dubbel hyra för varje extra station
        }

        public override void DisplayInfo() {
            Console.WriteLine($"{Name}");
            Console.WriteLine($"Pris: {Price} kr");
            Console.WriteLine($"Grundhyra: {BaseRent} kr");
            Console.WriteLine($"Ägare: {(Owner == null ? "Ingen" : Owner.Name)}");
        }

        public override void HandleAction(GamePlayer currentPlayer) {
            // Logik för att betala hyra
            base.HandleAction(currentPlayer, CalculateRent());
            if (Owner == currentPlayer) {
                // Du äger inget händer
            }

        }
    }

}