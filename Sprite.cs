using System.Collections.Generic;

namespace spriteman
{
    internal class Sprite
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }
        public int BottomRightX { get; set; }
        public int BottomRightY { get; set; }
        public Dictionary<string, string> Kvps { get; set; }

        public Sprite()
        {
            Kvps = new Dictionary<string, string>();
        }
    }
}
