using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace spriteman
{
    internal class SpriteProject
    {
        [JsonIgnore]
        public bool Dirty { get; set; }

        [JsonIgnore]
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
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(Filename, json);
            Dirty = false;
        }

        public static SpriteProject Load(string filename)
        {
            var json = File.ReadAllText(filename);
            var spriteProject = JsonConvert.DeserializeObject<SpriteProject>(json);
            spriteProject.Filename = filename;
            spriteProject.Dirty = false;
            return spriteProject;
        }
    }
}
