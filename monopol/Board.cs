using System.Diagnostics;
using System.Xml;

namespace monopol {
    internal class Board {
        private Deck chancedeck = new ChanceDeck();
        private Deck communitydeck = new CommunityDeck();
        private List<BoardSpace> board = new List<BoardSpace>();

        public Board() {
            loadBoard("boardspaces.xml");
            Debug.WriteLine($"Board created {board.Count}");
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
                    board.Add(new StreetSpace(name, position, color, price, rent, housecost, hotelcost, mortgage));
                } else if (type == "TrainStation") {
                    board.Add(new TrainStationSpace(name, position, price, rent));
                } else if (type == "Utility") {
                    board.Add(new UtilitySpace(name, position, price, rent));
                } else if (type == "Chance") {
                    board.Add(new ChanceSpace(name, position));
                } else if (type == "CommunityChest") {
                    board.Add(new CommunityChestSpace(name, position));
                } else if (type == "Tax") {
                    board.Add(new TaxSpace(name, position, price));
                } else if (type == "Jail") {
                    board.Add(new SpecialSpace(name, position, type));
                } else if (type == "GoToJail") {
                    board.Add(new SpecialSpace(name, position, type));
                } else if (type == "FreeParking") {
                    board.Add(new SpecialSpace(name, position, type));
                } else if (type == "Go") {
                    board.Add(new SpecialSpace(name, position, type));
                }

            }

        }


        internal BoardSpace GetSpace(int position) {
            return board[position];
        }

        internal void HandleAction(GamePlayer player) {
            if(board[player.Position] is ChanceSpace) 
                ((ChanceSpace)board[player.Position]).HandleAction(player, chancedeck.DrawCard());
            else if (board[player.Position] is CommunityChestSpace)
                ((CommunityChestSpace)board[player.Position]).HandleAction(player, communitydeck.DrawCard());
            else
                board[player.Position].HandleAction(player); // Street , train, utility, special

        }
    }
}