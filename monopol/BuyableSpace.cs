using System.Diagnostics;
using System.Windows;

namespace monopol {
    public abstract class BuyableSpace:BoardSpace {

        private int price;
        public int Price { get => price; set => price = value; }
        private bool isMortgaged;
        public bool IsMortgaged { get => isMortgaged; set => isMortgaged = value; }
        private GamePlayer owner;
        public GamePlayer Owner { get => owner; set => owner = value; }
        private int baseRent;
        public int BaseRent { get => baseRent; set => baseRent = value; }


        public BuyableSpace(string name, int position, int price, int baserent):base(name,position) {
            Price = price;
            baseRent = baserent;
            IsMortgaged = false;
            owner = null;
        }

        public void Mortgage() {
            IsMortgaged = true;
        }

        public void Unmortgage() {
            IsMortgaged = false;
        }

        public void ChangeOwner(GamePlayer newOwner) {
            owner = newOwner;
        }

        public void HandleAction(GamePlayer currentPlayer,int rent) {
            if (Owner == null) {
                // Offer to buy the street
                Debug.WriteLine($"{currentPlayer.Name} can buy {Name} for {Price}.");
                ChangeOwner(currentPlayer);
                currentPlayer.Money -= Price;
            } else if (Owner != currentPlayer) {
                // Pay rent
                Debug.WriteLine($"{currentPlayer.Name} pays rent to {Owner}.");
                currentPlayer.Money -= rent;
                Owner.Money += rent;
                // Handle bankrupcy with mortgaging and selling houses
            }

        }

    }
}