using System.Diagnostics;

namespace monopol {
    internal class ChancePosition : BoardObject {
        
        public ChancePosition(string name, int position) : base(name, position) {
        }

        public void giveCard(GamePlayer player,ChanceDeck chance) {
            // Draw a card and act accordingly
            Card card = chance.DrawCard();
            Debug.WriteLine($"{card.DisplayInfo()}");

            switch (card.Action){
                case "directadvance":
                        // Go to a specific position
                    player.Position = card.Destination;
                    break;
                        // Go to a relatove position
                        
                 case "specialadvance":
                    if (card.Number == 4) {
                        // Utility
                        int[] utilitypositions = { 12, 28 };
                        foreach (int utility in utilitypositions) {
                            if (player.Position < utility) {
                                player.Position = utility;
                                break;
                            }
                        }

                    }
                    if(card.Number == 5) {
                        // Railroad
                        int[] railroadpositions = { 5, 15, 25, 35 };
                        foreach (int railroad in railroadpositions) {
                            if (player.Position < railroad) {
                                player.Position = railroad;
                                break;
                            }
                        }
                    }
                    if(card.Number == 8) {
                        // Back 3 spaces
                        player.Position -= 3;
                    }

                    player.Position += card.Destination;
                    break;
                case "credit":
                    player.Money += card.Amount;
                    break;
                case "debit":
                    player.Money -= card.Amount;
                    break;
                case "getOutOfJail":
                    player.GetOutOfJail = true;
                    break;

                default:
                    break;
            }
        }
        public override void DisplayInfo() {
            // show data in output window

            Console.WriteLine($"Chans: {Name}");
            Console.WriteLine($"Position: {Position}");
        }
    }
}