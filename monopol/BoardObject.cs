namespace monopol {
    public abstract class BoardObject {
        // Gemensamma egenskaper för alla objekt på brädet

        public string Name { get; set; }           // Namnet på objektet
        public int Position { get; set; }         // Positionen på brädet

        // Konstruktor
        protected BoardObject(string name, int position) {
            Name = name;
            Position = position;
        }

        // Metod som kan implementeras olika i subklasser
        public abstract void DisplayInfo();

    }

}