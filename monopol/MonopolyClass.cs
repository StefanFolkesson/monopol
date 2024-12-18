using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace monopol {
    internal class MonopolyClass {
        private Deck chancedeck = new ChanceDeck();
        private Deck communitydeck = new CommunityDeck();
        private List<GamePlayer> gameplayers = new List<GamePlayer>();
        private int currentPlayer = 0;

        // public enum card {Chance, CommunityChest};
        public MonopolyClass() {
            List<BoardObject> board = new List<BoardObject>();
            board.Add(new Street("Hamngatan", 1, "Röd", 3000, 250, 1000));
            board.Add(new TrainStation("Centralstationen", 5, 2000, 500));
            board.Add(new ChancePosition("Chansen", 7));
            board.Add(new Street("Kungsgatan", 9, "Röd", 3000, 250, 1000));
            board.Add(new Street("Stureplan", 11, "Gul", 3500, 350, 1500));
            board.Add(new Street("Götgatan", 13, "Gul", 3500, 350, 1500));
            board.Add(new Street("Odenplan", 15, "Gul", 4000, 400, 2000));
            board.Add(new TrainStation("Södra Station", 17, 2000, 500));
            board.Add(new Street("Ringvägen", 19, "Grön", 4500, 450, 2000));
            board.Add(new Street("Sveavägen", 21, "Grön", 4500, 450, 2000));

            gameplayers.Add(new GamePlayer("Spelare 1", 1, 10000));
            gameplayers.Add(new GamePlayer("Spelare 2", 1, 10000));
            gameplayers[0].Position = 7;
            foreach (BoardObject square in board) {
                if(square is ChancePosition)
                    ((ChancePosition)square).giveCard(gameplayers[currentPlayer], (ChanceDeck)chancedeck);
                Debug.WriteLine($"{square.Name}");
            }
        }
    }
}