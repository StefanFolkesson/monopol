using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace monopol {
    internal class MonopolyClass {
        private Deck chancedeck = new ChanceDeck();
        private Deck communitydeck = new CommunityDeck();
        private List<GamePlayer> gameplayers = new List<GamePlayer>();
        private int currentPlayer = 0;
        private List<BoardObject> board = new List<BoardObject>();
        //TODO: gör board till en klass
        // Flytta korten dit
        // Lägga till en bank
        // Funktion för kasta tärningar i monopolklassen
        // Get my ownership på boardklassen
        // Funktionen köpa gata på street, utility station. Fixa till ett till arv.
        // Köpa hus på street
        // Köpa hotell på street
        // Sälja hus på street


        public MonopolyClass() {
            // Create all the squares on the board and add the swedish names to the streets and add them to the list

            loadBoard("boardspaces.xml");
            gameplayers.Add(new GamePlayer("Spelare 1", 1, 10000));
            gameplayers.Add(new GamePlayer("Spelare 2", 1, 10000));
/*            gameplayers[0].Position = 7;
            foreach (BoardObject square in board) {
                if(square is ChancePosition)
                    ((ChancePosition)square).giveCard(gameplayers[currentPlayer], (ChanceDeck)chancedeck);
                Debug.WriteLine($"{square.Name}");
            }*/
        }
        public void loadBoard(string XmlFileName) {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFileName);
            XmlNodeList squares = doc.GetElementsByTagName("Space");
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

                if (type == "Street") {
                    board.Add(new Street(name, position, color, price, rent, housecost, hotelcost, mortgage));
                } else if (type == "Chance") {
                    board.Add(new ChancePosition(name, position));
                } else if (type == "CommunityChest") {
                    board.Add(new CommunityChestPosition(name, position));
                } else if (type == "Tax") {
                    board.Add(new TaxPosition(name, position, price));
                } else if (type == "Jail") {
                    board.Add(new JailPosition(name, position));
                } else if (type == "GoToJail") {
                    board.Add(new GoToJailPosition(name, position));
                } else if (type == "FreeParking") {
                    board.Add(new FreeParkingPosition(name, position));
                } else if (type == "Go") {
                    board.Add(new GoPosition(name, position));
                }

        }

    }
}