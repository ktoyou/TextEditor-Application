using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows.Input;
using TextEditor.Commands;
using TextEditor.Models;

namespace TextEditor.ViewModels
{
    internal class EditorViewModel : BaseViewModel
    {

        #region properties
        public string FileTitle
        {
            get => _fileTitle;
            set
            {
                _fileTitle = value;
                OnPropertyChanged(nameof(FileTitle));
            }
        }

        public string FileContent
        {
            get => _fileContent;
            set
            {
                _fileContent = value;
                OnPropertyChanged(nameof(FileContent));
            }
        }

        public Settings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChanged(nameof(Settings));
            }
        }
        #endregion

        #region commands
        public ICommand OpenFileCommand { get; set; }

        public ICommand CreateFileCommand { get; set; }

        public ICommand PlusScaleCommand { get; set; }

        public ICommand MinusScaleCommand { get; set; }

        public ICommand SaveFileCommand { get; set; }

        public ICommand SaveHowCommand { get; set; }
        #endregion

        public EditorViewModel(ISettingsManager settings)
        {
            settingsManager = settings;
            Settings = settingsManager.Load(_config);

            OpenFileCommand = new RelayCommand(OpenFile);
            CreateFileCommand = new RelayCommand(CreateFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveHowCommand = new RelayCommand(SaveHow);
            PlusScaleCommand = new RelayCommand(param => Settings.FontSize += 1);
            MinusScaleCommand = new RelayCommand(param => Settings.FontSize -= 1);

            FileContent = string.Empty;
            FileTitle = "Без имени";
        }

        #region command methods
        private void SaveHow(object obj)
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = _filter };
            dialog.ShowDialog();

            if (dialog.FileName.Length == 0) return;

            using (FileStream stream = File.Create(dialog.FileName))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(FileContent);
                stream.Write(buffer, 0, buffer.Length);
            }

            _fullPathToFile = dialog.FileName;
            FileTitle = dialog.SafeFileName;
        }

        private void OpenFile(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = _filter };
            dialog.ShowDialog();

            if(dialog.FileName.Length == 0) return;
            
            using(Stream stream = dialog.OpenFile())
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                FileContent = Encoding.UTF8.GetString(buffer);
            }

            _fullPathToFile = dialog.FileName;
            FileTitle = dialog.SafeFileName;
        }

        private void SaveFile(object obj)
        {
            if (_fullPathToFile == null)
            {
                SaveHow(null);
                return;
            }
            File.WriteAllText(_fullPathToFile, FileContent);
        }

        private void CreateFile(object obj)
        {
            FileTitle = obj as string;
            FileContent = string.Empty;
        }
        #endregion

        #region private variables
        private string _fileTitle;

        private string _fileContent;

        private string _fullPathToFile;

        private Settings _settings;

        private ISettingsManager settingsManager;
        #endregion

        #region static variables
        private static string _config = "config.json";

        private static string _filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        #endregion

        ~EditorViewModel()
        {
            settingsManager.Save(_config, Settings);
        }
    }
}
