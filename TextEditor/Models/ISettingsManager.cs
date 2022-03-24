namespace TextEditor.Models
{
    internal interface ISettingsManager
    {
        Settings Load(string configUrl);

        void Save(string configUrl, Settings settings);
    }
}
