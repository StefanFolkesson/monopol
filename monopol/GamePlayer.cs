namespace monopol {
    public class GamePlayer {

        private string name;
        private int money;
        private int position;

        public GamePlayer(string name, int money, int position) {
            Name = name;
            Money = money;
            Position = position;
        }
        public string Name { get => name; set => name = value; }
        public int Money { get => money; set => money = value; }
        public int Position { get => position; set => position = value; }
        public bool GetOutOfJail { get; internal set; }


        /*   public void RecieveCard(Card card) {
               throw new System.NotImplementedException();
           }*/
    }
}