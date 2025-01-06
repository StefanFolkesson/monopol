

namespace monopol {
    public class GamePlayer {

        private string name;
        private int money;
        private int position;
        private bool getOutOfJail;
        private int jailTurns;

        public GamePlayer(string name, int position, int money) {
            Name = name;
            Money = money;
            Position = position;
            GetOutOfJail = false;
        }
        public string Name { get => name; set => name = value; }
        public int Money { get => money; set => money = value; }
        public int Position { get => position; set => position = value; }
        public bool GetOutOfJail { get; internal set; }

        public int JailTurns { get => jailTurns; set => jailTurns = value; }

        public void DisplayInfo() {
            // show data in output window
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Money: {Money}");
            Console.WriteLine($"Position: {Position}");
        }

        internal void checkMoney(BoardSpace[] mySpaces ) {
            if(Money < 0) {
                // Sell houses, mortgage properties
                foreach (BoardSpace space in mySpaces) {
                    if (space is BuyableSpace) {
                        BuyableSpace buyableSpace = (BuyableSpace)space;
                        if (buyableSpace.Owner == this && Money<0) {
                            buyableSpace.Mortgage();
                        }
                    }
                }

                // Remove player from game
            }
        }

        internal void GoToJail() {
            JailTurns = 3;
        }
    }
}