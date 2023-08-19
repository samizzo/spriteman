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

        public void AddSprite(string image, string name, int x, int y, int width, int height)
        {
            Debug.Assert(Images.Contains(image));
            var sprite = new Sprite()
            {
                Image = image,
                Name = name,
                X = x,
                Y = y,
                Width = width,
                Height = height
            };
            Sprites.Add(sprite);
        }
    }
}
