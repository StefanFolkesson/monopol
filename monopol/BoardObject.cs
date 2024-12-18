namespace monopol {
    public abstract class BoardObject {
        // Gemensamma egenskaper för alla objekt på brädet

        public string Name { get; set; }           // Namnet på objektet
        public int Position { get; set; }         // Positionen på brädet
        public string Owner { get; set; }         // Namnet på ägaren (null om ingen äger)

        // Konstruktor
        protected BoardObject(string name, int position) {
            Name = name;
            Position = position;
            Owner = null;
        }

        // Metod som kan implementeras olika i subklasser
        public abstract void DisplayInfo();

        // Gemensam funktion för att byta ägare
        public void ChangeOwner(string newOwner) {
            Owner = newOwner;
        }
    }

}