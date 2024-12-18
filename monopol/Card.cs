namespace monopol {
    internal class Card {
        string description;
        int number;
        public Card(string description, int number) {
            this.description = description;
            this.number = number;
        }

        public void DisplayInfo() {
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"Number: {number}");
        }

}