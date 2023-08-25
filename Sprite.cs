using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace spriteman
{
    internal class Sprite
    {
        public class Kvp : INotifyPropertyChanged
        {
            private string key;
            public string Key
            {
                get => key;
                set
                {
                    if (key != value)
                    {
                        key = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            private string value;
            public string Value
            {
                get => value;
                set
                {
                    if (this.value != value)
                    {
                        this.value = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }
        public int BottomRightX { get; set; }
        public int BottomRightY { get; set; }
        public BindingList<Kvp> Kvps { get; set; }

        public Sprite()
        {
            Kvps = new BindingList<Kvp>();
        }
    }
}
