using System.Diagnostics;

namespace monopol {
    internal class CommunityChestPosition : BoardObject {
        public CommunityChestPosition(string name, int position) : base(name, position) {
        }
        public override void DisplayInfo() {
            // show data in output window

            Console.WriteLine($"Chans: {Name}");
            Console.WriteLine($"Position: {Position}");
        }

    }
}