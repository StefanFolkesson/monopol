namespace monopol {
    public abstract class BoardSpace {
        // Gemensamma egenskaper för alla objekt på brädet

        public string Name { get; set; }           // Namnet på objektet
        public int Position { get; set; }         // Positionen på brädet

        // Konstruktor
        protected BoardSpace(string name, int position) {
            Name = name;
            Position = position;
        }

        // Metod som kan implementeras olika i subklasser
        public abstract void DisplayInfo();

        public virtual void HandleAction(GamePlayer currentPlayer) {
            // Default action, e.g., do nothing
            Console.WriteLine($"{currentPlayer.Name} landed on {Name}. No action.");
        }

    }

}