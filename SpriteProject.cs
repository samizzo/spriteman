using System.Collections.Generic;

namespace spriteman
{
    internal class SpriteProject
    {
        public bool Dirty { get; set; }
        public string Filename { get; set; }

        public List<string> Images { get; }
        public List<Sprite> Sprites { get; }

        public SpriteProject()
        {
            Images = new List<string>();
            Sprites = new List<Sprite>();
            Dirty = true;
        }

        public void Save()
        {
        }

        public static SpriteProject Load()
        {
            return new SpriteProject();
        }
    }
}
