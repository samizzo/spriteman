using System.Collections.Generic;

namespace spriteman
{
    internal class Sprite
    {
        public class Kvp
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }
        public int BottomRightX { get; set; }
        public int BottomRightY { get; set; }
        public List<Kvp> Kvps { get; set; }

        public Sprite()
        {
            Kvps = new List<Kvp>();
        }
    }
}
