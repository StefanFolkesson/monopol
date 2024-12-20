using System.Diagnostics;
using System.Numerics;

namespace monopol {
    internal class SpecialSpace:BoardSpace {
        private string type = "";

        public SpecialSpace(string name, int position, string type) : base(name, position) {
            Type = type;
        }

        public string Type { get => type; set => type = value; }
        public override void DisplayInfo() {
            // show data in output window

            Console.WriteLine($"Chans: {Name}");
            Console.WriteLine($"Position: {Position}");
        }

        public override void HandleAction(GamePlayer currentPlayer) {

            switch (currentPlayer.Position) {
                case 0:
                    Debug.WriteLine($"{currentPlayer.Name} is on go.");
                    currentPlayer.Money += 200;
                    break;
                case 10:
                    Debug.WriteLine($"{currentPlayer.Name} is visiting jail.");
                    break;
                case 20:
                    Debug.WriteLine($"{currentPlayer.Name} is in free parking.");
                    break;
                case 30:
                    Debug.WriteLine($"{currentPlayer.Name} is in jail.");
                    if (currentPlayer.GetOutOfJail != true) {
                        currentPlayer.Position = 10;
                        currentPlayer.GoToJail();
                    }
                    break;
            }
        }
    }
}