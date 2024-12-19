using System.Diagnostics;
using System.Xml;

namespace monopol {
    internal class Board {
        private Deck chancedeck = new ChanceDeck();
        private Deck communitydeck = new CommunityDeck();
        private List<BoardObject> board = new List<BoardObject>();

        public Board() {
            loadBoard("boardspaces.xml");
            Debug.WriteLine($"Board created {board.Count}");
        }
        public void DrawCommunityCard(GamePlayer player) {
            // Handle the cards
            Card card = communitydeck.DrawCard();
            switch (card.Action) {
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
                    if (card.Number == 5) {
                        // Railroad
                        int[] railroadpositions = { 5, 15, 25, 35 };
                        foreach (int railroad in railroadpositions) {
                            if (player.Position < railroad) {
                                player.Position = railroad;
                                break;
                            }
                        }
                    }
                    if (card.Number == 8) {
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
        public void DrawChanceCard(GamePlayer player) {
            // Handle the cards
            Card card = chancedeck.DrawCard();
            switch (card.Action) {
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
                    if (card.Number == 5) {
                        // Railroad
                        int[] railroadpositions = { 5, 15, 25, 35 };
                        foreach (int railroad in railroadpositions) {
                            if (player.Position < railroad) {
                                player.Position = railroad;
                                break;
                            }
                        }
                    }
                    if (card.Number == 8) {
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

        public void loadBoard(string XmlFileName) {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFileName);
            XmlNodeList squares = doc.GetElementsByTagName("Space");
            int counter = 0;
            foreach (XmlNode square in squares) {
                string name = "";
                string color = "";
                string type = "";
                int rent = 0;
                int price = 0;
                int housecost = 0;
                int hotelcost = 0;
                int mortgage = 0;
                int position = 0;
                XmlNode nameNode = square.SelectSingleNode("Name");
                if (nameNode != null) {
                    name = nameNode.InnerText;
                }

                XmlNode colorNode = square.SelectSingleNode("Color");
                if (colorNode != null) {
                    color = colorNode.InnerText;
                }

                XmlNode typeNode = square.SelectSingleNode("Type");
                if (typeNode != null) {
                    type = typeNode.InnerText;
                }

                XmlNode rentNode = square.SelectSingleNode("Rent");
                if (rentNode != null) {
                    rent = int.Parse(rentNode.InnerText);
                }

                XmlNode priceNode = square.SelectSingleNode("Price");
                if (priceNode != null) {
                    price = int.Parse(priceNode.InnerText);
                }

                XmlNode housecostNode = square.SelectSingleNode("HouseCost");
                if (housecostNode != null) {
                    housecost = int.Parse(housecostNode.InnerText);
                }

                XmlNode hotelcostNode = square.SelectSingleNode("HotelCost");
                if (hotelcostNode != null) {
                    hotelcost = int.Parse(hotelcostNode.InnerText);
                }

                XmlNode mortgageNode = square.SelectSingleNode("Mortgage");
                if (mortgageNode != null) {
                    mortgage = int.Parse(mortgageNode.InnerText);
                }

                XmlNode positionNode = square.SelectSingleNode("Position");
                if (positionNode != null) {
                    position = int.Parse(positionNode.InnerText);
                }
                Debug.WriteLine($"{counter++} Name: {name}, Color: {color}, Type: {type}, Rent: {rent}, Price: {price}, HouseCost: {housecost}, HotelCost: {hotelcost}, Mortgage: {mortgage}, Position: {position}");
                if (type == "Street") {
                    board.Add(new Street(name, position, color, price, rent, housecost, hotelcost, mortgage));
                } else if (type == "TrainStation") {
                    board.Add(new TrainStation(name, position, price, rent));
                } else if (type == "Utility") {
                    board.Add(new Utility(name, position, price, rent));
                } else if (type == "Chance") {
                    board.Add(new ChancePosition(name, position));
                } else if (type == "CommunityChest") {
                    board.Add(new CommunityChestPosition(name, position));
                } else if (type == "Tax") {
                    board.Add(new TaxPosition(name, position, price));
                } else if (type == "Jail") {
                    board.Add(new SpecialPosition(name, position, type));
                } else if (type == "GoToJail") {
                    board.Add(new SpecialPosition(name, position, type));
                } else if (type == "FreeParking") {
                    board.Add(new SpecialPosition(name, position, type));
                } else if (type == "Go") {
                    board.Add(new SpecialPosition(name, position, type));
                }

            }

        }
        internal BoardObject GetSpace(int position) {
            return board[position];
        }
    }
}