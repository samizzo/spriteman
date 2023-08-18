using System.Collections.Generic;

namespace spriteman
{
    internal class Sprite
    {
        public string Name { get; set; }
        public Dictionary<string, string> Kvps { get; set; }

        public Sprite()
        {
            Kvps = new Dictionary<string, string>();
        }
    }
}
