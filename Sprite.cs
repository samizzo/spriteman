using System.Collections.Generic;

namespace spriteman
{
    internal class Sprite
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<string, string> Kvps { get; set; }

        public Sprite()
        {
            Kvps = new Dictionary<string, string>();
        }
    }
}
