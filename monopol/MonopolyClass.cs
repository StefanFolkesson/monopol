using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace monopol {
    internal class MonopolyClass {
        private List<GamePlayer> gameplayers = new List<GamePlayer>();
        private Board board = new Board();
        private int currentPlayer = 0;
        private Random random = new Random();

        
        // Lägga till en bank
        
        // Get my ownership på boardklassen
        // Funktionen köpa gata på street, utility station. Fixa till ett till arv.
        // Köpa hus på street
        // Köpa hotell på street
        // Sälja hus på street


        public MonopolyClass() {
            // Create all the squares on the board and add the swedish names to the streets and add them to the list

            gameplayers.Add(new GamePlayer("Spelare 1", 1, 10000));
            gameplayers.Add(new GamePlayer("Spelare 2", 1, 10000));

            StartGameLoop();
        }

        // Create a turnbased gameloop for monopoly


        private void StartGameLoop() {
            bool gameRunning = true;

            while (gameRunning) {
                GamePlayer player = gameplayers[currentPlayer];
                Debug.WriteLine($"{player.Name}'s turn.");

                // Roll dice
                int diceRoll = RollDice();
                Debug.WriteLine($"{player.Name} rolled a {diceRoll}.");

                // Move player
                player.Position = (player.Position + diceRoll) % 40;
                BoardSpace currentSpace = board.GetSpace(player.Position);
                Debug.WriteLine($"{player.Name} landed on {currentSpace.Name}.");

                // Handle the space the player landed on
                // HandleAction skall kanske sitta i board
                board.HandleAction(player);
                

                // Check for game-ending conditions
                gameRunning = CheckGameEnd();

                // Move to the next player
                currentPlayer = (currentPlayer + 1) % gameplayers.Count;
            }

            Debug.WriteLine("Game over!");
        }

        private int RollDice() {
            return random.Next(1, 7) + random.Next(1, 7);
        }

        private bool CheckGameEnd() {
            // Implement game-ending conditions
            // For example, check if a player is bankrupt
            foreach (var player in gameplayers) {
                if (player.Money <= 0) {
                    Debug.WriteLine($"{player.Name} is bankrupt!");
                    return true;
                }
            }
            return false;
        }

    }
}