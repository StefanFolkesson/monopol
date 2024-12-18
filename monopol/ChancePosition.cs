namespace monopol {
    internal class ChancePosition : BoardObject {
        
        public ChancePosition(string name, int position) : base(name, position) {
        }

        public void giveCard(GamePlayer player,ChanceDeck chance) {
            player.RecieveCard(chance.DrawCard());
        }
        public override void DisplayInfo() {
            Console.WriteLine($"Chans: {Name}");
            Console.WriteLine($"Position: {Position}");
        }
    }
}