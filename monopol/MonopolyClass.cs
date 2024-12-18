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
            // Create all the squares on the board and add the swedish names to the streets and add them to the list




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