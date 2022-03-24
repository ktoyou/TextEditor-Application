using Newtonsoft.Json;

namespace TextEditor.Models
{
    internal class Settings : BaseModel
    {
        [JsonProperty("fontSize")] public int FontSize
        {
            get => _fontSize;
            set
            {
                if (value < 0) return;
                _fontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }

        public static Settings Default() => new Settings
        {
            FontSize = 15
        };

        private int _fontSize;
    }
}
