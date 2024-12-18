namespace monopol {
    internal class Card {
        private string description;
        private int number;
        private string action;
        private int destination;
        private int amount;
        public Card(string description, int number, string action, int destination = 0, int amount = 0) {
            this.description = description;
            Number = number;
            Action = action;
            Destination = destination;
            Amount = amount;
        }

        public int Number { get => number; set => number = value; }

        public string Action { get => action; set => action = value; }
        public int Destination { get => destination; set => destination = value; }
        public int Amount { get => amount; set => amount = value; }

        public string DisplayInfo() {
            return $"Description: {description} Number: {Number}";
        }
    }

}