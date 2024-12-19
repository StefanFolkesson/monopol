namespace monopol {
    internal class SpecialPosition:BoardObject {
        private string type = "";

        public SpecialPosition(string name, int position, string type) : base(name, position) {
            Type = type;
        }

        public string Type { get => type; set => type = value; }
        public override void DisplayInfo() {
            // show data in output window

            Console.WriteLine($"Chans: {Name}");
            Console.WriteLine($"Position: {Position}");
        }
    }
}