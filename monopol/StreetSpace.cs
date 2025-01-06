using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows;

namespace monopol {
    public class StreetSpace : BuyableSpace {
        public string ColorGroup { get; set; }   // Färggruppen
        public int HouseCost { get; set; }       // Kostnad för att bygga hus
        public int Houses { get; private set; }  // Antal hus
        public bool HasHotel { get; private set; }

        // Konstruktor
        public StreetSpace(string name, int position, string colorGroup, int price, int baseRent, int houseCost, int hotelcost, int mortgage)
            : base(name, position,price,baseRent) {
            ColorGroup = colorGroup;
            HouseCost = houseCost;
            Houses = 0;
            HasHotel = false;
        }

        // Bygg hus
        public void BuildHouse() {
            if(Owner.Money < HouseCost) {
                Debug.WriteLine("Not enough money to build house.");
                return;
            }
            if (Houses < 4 && !HasHotel) {
                Owner.Money -= HouseCost;
                Houses++;
                Debug.WriteLine($"Built house on {Name}.");
            } else if (Houses == 4 && !HasHotel) {
                if(Owner.Money < HouseCost*4) {
                    Debug.WriteLine("Not enough money to build hotel.{HouseCost*4}");
                    return;
                }
                Owner.Money -= HouseCost * 4;
                HasHotel = true;
                Houses = 0;
                Debug.WriteLine($"Built hotel on {Name}.");
            }
        }

        public void SellHouse() {
            if (HasHotel) {
                Owner.Money += HouseCost * 2;
                HasHotel = false;
                Houses = 4;
                Debug.WriteLine($"Sold hotel on {Name}.");
            } else if (Houses > 0) {
                Owner.Money += HouseCost / 2;
                Houses--;
                Debug.WriteLine($"Sold house on {Name}.");
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
        public override void HandleAction(GamePlayer currentPlayer) {
            if(Owner == null) {
                // Erbjud att köpa gatan
                Debug.WriteLine($"{currentPlayer.Name} kan köpa {Name} för {Price}.");
                ChangeOwner(currentPlayer);
                currentPlayer.Money -= Price;
            } else if (Owner != currentPlayer) {
                // Betala hyra
                Debug.WriteLine($"{currentPlayer.Name} betalar hyra till {Owner}.");
                currentPlayer.Money -= CalculateRent();
                Owner.Money += CalculateRent();
                // Hantera bankrutt med pant och försäljning av hus
            }

            else if (Owner == currentPlayer) {
                if (IsMortgaged) {
                    // Player owns the street but it is mortgaged
                    // Offer to unmortgage
                    Debug.WriteLine($"{currentPlayer.Name} owns {Name} but it is mortgaged.");
                    Debug.WriteLine($"Unmortgage for {Price * 1.1}?");
                    if(Owner.Money >= Price * 1.1)
                        Unmortgage();
                } else {
                    // Player owns the street
                    // Offer to build houses
                    Debug.WriteLine($"Build house for {HouseCost}?");
                    Debug.WriteLine($"Sell house for {HouseCost / 2}?");
                    Debug.WriteLine($"{currentPlayer.Name} owns {Name}.");
                    if(currentPlayer.Money >= HouseCost)
                        BuildHouse();
                }
            }
        }
    }

}