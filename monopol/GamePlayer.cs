
namespace monopol {
    public class GamePlayer {

        private string name;
        private int money;
        private int position;
        private bool getOutOfJail;
        private int jailTurns;

        public GamePlayer(string name, int money, int position) {
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

        internal void GoToJail() {
            JailTurns = 3;
        }


        /*   public void RecieveCard(Card card) {
               throw new System.NotImplementedException();
           }*/
    }
}