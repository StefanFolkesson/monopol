namespace monopol {
    public abstract class BuyableObject:BoardObject {

        private int price;
        public int Price { get => price; set => price = value; }
        private bool isMortgaged;
        public bool IsMortgaged { get => isMortgaged; set => isMortgaged = value; }
        private GamePlayer owner;
        public GamePlayer Owner { get => owner; set => owner = value; }
        private int baseRent;
        public int BaseRent { get => baseRent; set => baseRent = value; }


        public BuyableObject(string name, int position, int price, int baserent):base(name,position) {
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

    }
}