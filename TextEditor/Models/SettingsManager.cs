using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace TextEditor.Models
{
    internal class SettingsManager : ISettingsManager
    {
        public Settings Load(string configUrl)
        {
            if (!File.Exists(configUrl)) return Settings.Default();

            string json;
            using(Stream stream = File.OpenRead(configUrl))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                json = Encoding.UTF8.GetString(buffer);
            }

            return JsonConvert.DeserializeObject<Settings>(json);
        }

        public void Save(string configUrl, Settings settings)
        {
            using(FileStream stream = File.OpenWrite(configUrl))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(settings));
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
