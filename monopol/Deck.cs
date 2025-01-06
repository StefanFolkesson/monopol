using System.ComponentModel.DataAnnotations;

namespace monopol {
    internal abstract class Deck {
        private List<Card> cards = new List<Card>();
        public Deck() {
        }

        public void AddCard(Card card) {
            cards.Add(card);
        }
        public Card DrawCard() {
            // remove the first element and return it
            if(cards.Count == 0) {
                return null;
            }
            Card card = cards.First();
            cards.Remove(card);
            return card;
        }
        public void shuffle() {
            Random rnd = new Random();
            cards = cards.OrderBy(x => rnd.Next()).ToList();
        }
    }
}