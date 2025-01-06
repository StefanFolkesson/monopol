using System.Diagnostics;
using System.Xml;

namespace monopol {
    internal class Board {
        private Deck chancedeck = new ChanceDeck();
        private Deck communitydeck = new CommunityDeck();
        public List<BoardSpace> boardSpaces = new List<BoardSpace>();

        public Board() {
            
            Debug.WriteLine($"Board created {boardSpaces.Count}");
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
                    boardSpaces.Add(new StreetSpace(name, position, color, price, rent, housecost, hotelcost, mortgage));
                } else if (type == "TrainStation") {
                    boardSpaces.Add(new TrainStationSpace(name, position, price, rent));
                } else if (type == "Utility") {
                    boardSpaces.Add(new UtilitySpace(name, position, price, rent));
                } else if (type == "Chance") {
                    boardSpaces.Add(new ChanceSpace(name, position));
                } else if (type == "CommunityChest") {
                    boardSpaces.Add(new CommunityChestSpace(name, position));
                } else if (type == "Tax") {
                    boardSpaces.Add(new TaxSpace(name, position, price));
                } else if (type == "Jail") {
                    boardSpaces.Add(new SpecialSpace(name, position, type));
                } else if (type == "GoToJail") {
                    boardSpaces.Add(new SpecialSpace(name, position, type));
                } else if (type == "FreeParking") {
                    boardSpaces.Add(new SpecialSpace(name, position, type));
                } else if (type == "Go") {
                    boardSpaces.Add(new SpecialSpace(name, position, type));
                }

            }

        }

        internal BoardSpace[] GetMySpaces(GamePlayer player) {
            // return all spaces owned by player
            List<BoardSpace> myspaces = new List<BoardSpace>();
            foreach (BoardSpace space in boardSpaces) {
                if (space is BuyableSpace) {
                    if (((BuyableSpace)space).Owner == player) {
                        myspaces.Add(space);
                    }
                }
            }
            return myspaces.ToArray();
        }


        internal BoardSpace GetSpace(int position) {
            return boardSpaces[position];
        }

        internal void HandleAction(GamePlayer player) {
            if (boardSpaces[player.Position] is ChanceSpace) {
                Card card = chancedeck.DrawCard();
                if (card != null) {
                    ((ChanceDeck)chancedeck).LoadCards();
                    card = chancedeck.DrawCard();
                }
                ((ChanceSpace)boardSpaces[player.Position]).HandleAction(player, chancedeck.DrawCard());
            } else if (boardSpaces[player.Position] is CommunityChestSpace) {
                Card card = communitydeck.DrawCard();
                if (card != null) {
                    ((CommunityDeck)communitydeck).LoadCards();
                    card = communitydeck.DrawCard();
                }
                ((CommunityChestSpace)boardSpaces[player.Position]).HandleAction(player, communitydeck.DrawCard());
            } else
                boardSpaces[player.Position].HandleAction(player); // Street , train, utility, special

        }
    }
}