using System.Collections.Generic;

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
