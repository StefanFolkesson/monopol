
using System.Xml;

namespace monopol {
    internal class ChanceDeck :Deck{
        public ChanceDeck() {
            
        }

        public new void DrawCard() {
            Card card = base.DrawCard();
            card.DisplayInfo();
        }

        public void LoadCards(string XmlFileName) {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFileName);
            XmlNodeList cards = doc.GetElementsByTagName("chancecard");
            foreach (XmlNode card in cards) {
                string description = card.SelectSingleNode("description").InnerText;
                int number = int.Parse(card.SelectSingleNode("id").InnerText);
                AddCard(new Card(description, number));
            }

        }


        
}