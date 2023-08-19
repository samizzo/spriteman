using System.Collections.Generic;
using System.Diagnostics;

namespace spriteman
{
    internal class SpriteProject
    {
        public List<string> Images { get; }
        public List<Sprite> Sprites { get; }

        public SpriteProject()
        {
            Images = new List<string>();
            Sprites = new List<Sprite>();
        }
    }
}
