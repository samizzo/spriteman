using Newtonsoft.Json;
using System;
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

        public string GetImageFullPath(string image)
        {
            return Path.Combine(Path.GetDirectoryName(Filename), image);
        }

        public void AddImage(string image)
        {
            // Make path relative to the path of the project.
            var projDir = Path.GetDirectoryName(Filename) + "\\";
            var root = new Uri(projDir, UriKind.Absolute);
            var imageFullPath = new Uri(image, UriKind.Absolute);
            var imageRelativePath = root.MakeRelativeUri(imageFullPath).ToString();
            Images.Add(imageRelativePath.Replace('/', '\\').ToLower());
        }

        public void RemoveImage(string image)
        {
            Images.Remove(image);

            // Remove sprites that reference the image.
            var removeList = Sprites.Where(sprite => sprite.Image == image).ToList();
            foreach (var sprite in removeList)
                RemoveSprite(sprite);
        }

        public Sprite AddSprite(string image, string name, Rectangle rectangle, int insertIndex = -1)
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
            if (insertIndex != -1)
            {
                Sprites.Insert(insertIndex, sprite);
            }
            else
            {
                Sprites.Add(sprite);
            }
            return sprite;
        }

        public void RemoveSprite(Sprite sprite)
        {
            Sprites.Remove(sprite);
        }

        public void MoveUp(Sprite sprite)
        {
            int index = Sprites.IndexOf(sprite);
            if (index > 0)
            {
                Sprites.Remove(sprite);
                Sprites.Insert(index - 1, sprite);
            }
        }

        public void MoveDown(Sprite sprite)
        {
            int index = Sprites.IndexOf(sprite);
            if (index < Sprites.Count - 1)
            {
                Sprites.Remove(sprite);
                Sprites.Insert(index + 1, sprite);
            }
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
