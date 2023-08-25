using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace spriteman
{
    internal class SpriteProject
    {
        [JsonIgnore]
        public bool Dirty { get; set; }

        [JsonIgnore]
        public string Filename { get; set; }

        public BindingList<string> Images { get; }
        public BindingList<Sprite> Sprites { get; }

        public SpriteProject()
        {
            Images = new BindingList<string>();
            Images.ListChanged += ListChanged;
            Sprites = new BindingList<Sprite>();
            Sprites.ListChanged += ListChanged;
            Dirty = true;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(Filename, json);
            Dirty = false;
        }

        public static SpriteProject Load(string filename)
        {
            var json = File.ReadAllText(filename);
            var spriteProject = JsonConvert.DeserializeObject<SpriteProject>(json);
            spriteProject.Filename = filename;

            foreach (var sprite in spriteProject.Sprites)
            {
                sprite.Kvps.ListChanged += spriteProject.ListChanged;
                foreach (var kvp in sprite.Kvps)
                    kvp.PropertyChanged += spriteProject.Kvp_PropertyChanged;
            }

            spriteProject.Dirty = false;

            return spriteProject;
        }

        public void AddImage(string image)
        {
            Images.Add(image);
        }

        public void RemoveImage(string image)
        {
            Images.Remove(image);

            // Remove sprites that reference the image.
            var removeList = Sprites.Where(sprite => sprite.Image == image).ToList();
            foreach (var sprite in removeList)
                RemoveSprite(sprite);
        }

        public Sprite AddSprite(string image, string name, Rectangle rectangle)
        {
            var sprite = new Sprite()
            {
                Image = image,
                Name = name,
                TopLeftX = rectangle.X,
                TopLeftY = rectangle.Y,
                BottomRightX = rectangle.X + rectangle.Width - 1,
                BottomRightY = rectangle.Y + rectangle.Height - 1,
            };
            sprite.Kvps.ListChanged += KvpListChanged;
            Sprites.Add(sprite);
            return sprite;
        }

        public void RemoveSprite(Sprite sprite)
        {
            Sprites.Remove(sprite);
        }

        private void ListChanged(object sender, ListChangedEventArgs e)
        {
            Dirty = true;
        }

        private void KvpListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                var kvp = (Sprite.Kvp)sender;
                kvp.PropertyChanged += Kvp_PropertyChanged;
            }
            Dirty = true;
        }

        private void Kvp_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Dirty = true;
        }
    }
}
