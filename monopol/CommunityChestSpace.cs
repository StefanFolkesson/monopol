using System.Diagnostics;
using System.Numerics;

namespace monopol {
    internal class CommunityChestSpace : BoardSpace {
        public CommunityChestSpace(string name, int position) : base(name, position) {
        }
        public override void DisplayInfo() {
            // show data in output window

            Console.WriteLine($"Chans: {Name}");
            Console.WriteLine($"Position: {Position}");
        }

        public void HandleAction(GamePlayer currentPlayer, Card card) {
            // Dra ett kort från chanshögen
            Card drawnCard = card;
            Console.WriteLine($"{currentPlayer.Name} drew a Community Deck card:");
            switch (card.Action) {
                case "directadvance":
                    // Go to a specific position
                    currentPlayer.Position = card.Destination;
                    break;
                // Go to a relatove position

                case "specialadvance":
                    if (card.Number == 4) {
                        // Utility
                        int[] utilitypositions = { 12, 28 };
                        foreach (int utility in utilitypositions) {
                            if (currentPlayer.Position < utility) {
                                currentPlayer.Position = utility;
                                break;
                            }
                        }
                    }
                    if (card.Number == 5) {
                        // Railroad
                        int[] railroadpositions = { 5, 15, 25, 35 };
                        foreach (int railroad in railroadpositions) {
                            if (currentPlayer.Position < railroad) {
                                currentPlayer.Position = railroad;
                                break;
                            }
                        }
                    }
                    if (card.Number == 8) {
                        // Back 3 spaces
                        currentPlayer.Position -= 3;
                    }

                    currentPlayer.Position += card.Destination;
                    break;
                case "credit":
                    currentPlayer.Money += card.Amount;
                    break;
                case "debit":
                    currentPlayer.Money -= card.Amount;
                    break;
                case "getOutOfJail":
                    currentPlayer.GetOutOfJail = true;
                    break;

                default:
                    break;
            }

        }

    }
}