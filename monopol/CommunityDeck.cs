using System.Diagnostics;
using System.Xml;
namespace monopol {
    internal class CommunityDeck : Deck {
        public CommunityDeck() {
            LoadCards("communitydeckcards.xml");
            Debug.WriteLine("CommunityDeck created");
        }
        private void LoadCards(string XmlFileName) {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFileName);
            XmlNodeList cards = doc.GetElementsByTagName("communitydeckcard");
            foreach (XmlNode card in cards) {
                string description = "";
                int number = 0;
                string action = "";
                int destination = 0;
                int amount = 0;
                XmlNode descriptionNode = card.SelectSingleNode("description");
                if (descriptionNode != null) {
                    description = descriptionNode.InnerText;
                }

                XmlNode numberNode = card.SelectSingleNode("id");
                if (numberNode != null) {
                    number = int.Parse(numberNode.InnerText);
                }

                XmlNode actionNode = card.SelectSingleNode("action");
                if (actionNode != null) {
                    action = actionNode.InnerText;
                }

                XmlNode destinationNode = card.SelectSingleNode("destination");
                if (destinationNode != null) {
                    destination = int.Parse(destinationNode.InnerText);
                }

                XmlNode amountNode = card.SelectSingleNode("amount");
                if (amountNode != null) {
                    amount = int.Parse(amountNode.InnerText);
                }

                AddCard(new Card(description, number, action, destination, amount));


            }

        }
    }
}