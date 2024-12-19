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
            /*            gameplayers[0].Position = 7;
                        foreach (BoardObject square in board) {
                            if(square is ChancePosition)
                                ((ChancePosition)square).giveCard(gameplayers[currentPlayer], (ChanceDeck)chancedeck);
                            Debug.WriteLine($"{square.Name}");
                        }*/
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
                BoardObject currentSpace = board.GetSpace(player.Position);
                Debug.WriteLine($"{player.Name} landed on {currentSpace.Name}.");

                // Handle the space the player landed on
                HandleSpace(player, currentSpace);

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

        private void HandleSpace(GamePlayer player, BoardObject space) {
            // Implement logic for handling different types of spaces
            if (space is Street street) {
                if (street.Owner == null) {
                    // Offer to buy the street
                    Debug.WriteLine($"{player.Name} can buy {street.Name} for {street.Price}.");
                    street.ChangeOwner(player);
                    player.Money -= street.Price;
                } else if (street.Owner != player) {
                    // Pay rent
                    Debug.WriteLine($"{player.Name} pays rent to {street.Owner}.");
                    player.Money -= street.CalculateRent();
                    street.Owner.Money += street.CalculateRent();
                    // Handle bankrupcy with mortgaging and selling houses
                }
            } else if (space is Utility utility) {
                if (utility.Owner == null) {
                    // Offer to buy the street
                    Debug.WriteLine($"{player.Name} can buy {utility.Name} for {utility.Price}.");
                    utility.ChangeOwner(player);
                    player.Money -= utility.Price;
                } else if (utility.Owner != player) {
                    // Pay rent
                    Debug.WriteLine($"{player.Name} pays rent to {utility.Owner}.");
                    player.Money -= utility.CalculateRent();
                    utility.Owner.Money += utility.CalculateRent();
                    // Handle bankrupcy with mortgaging and selling houses
                }
            } else if (space is TrainStation station) {
                if (station.Owner == null) {
                    // Offer to buy the street
                    Debug.WriteLine($"{player.Name} can buy {station.Name} for {station.Price}.");
                    station.ChangeOwner(player);
                    player.Money -= station.Price;
                } else if (station.Owner != player) {
                    // Pay rent
                    Debug.WriteLine($"{player.Name} pays rent to {station.Owner}.");
                    player.Money -= station.CalculateRent();
                    station.Owner.Money += station.CalculateRent();
                    // Handle bankrupcy with mortgaging and selling houses
                }

            } else if (space is ChancePosition) {
                // Draw a chance card
                Debug.WriteLine($"{player.Name} draws a chance card.");
                board.DrawChanceCard(player);

            } else if (space is CommunityChestPosition) {
                // Draw a community chest card
                Debug.WriteLine($"{player.Name} draws a community chest card.");
                board.DrawCommunityCard(player);

                // Implement community chest card logic
            } else if (space is TaxPosition tax) {
                // Pay tax
                Debug.WriteLine($"{player.Name} pays {tax.Price} in taxes.");
                player.Money -= tax.Price;

            } else if (space is SpecialPosition special) {
                // Handle special positions like "Go to Jail"
                Debug.WriteLine($"{player.Name} landed on {special.Name}.");

                if (special.Type == "GoToJail") {
                    if (player.GetOutOfJail != true) {
                        player.Position = 10;
                        player.GoToJail();
                    }
                }
                if (special.Type == "Jail") {
                    Debug.WriteLine($"{player.Name} is visiting jail.");
                }
                if (special.Type == "FreeParking") {
                    Debug.WriteLine($"{player.Name} is in free parking.");
                }
                if (special.Type == "Go") {
                    Debug.WriteLine($"{player.Name} passed go and gets 200.");
                    player.Money += 200;
                }
            }
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