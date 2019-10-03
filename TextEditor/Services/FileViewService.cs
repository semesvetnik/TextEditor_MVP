using System;
using TextEditor.Interfaces;
using TextEditorLibrary.Interfaces;
using TextEditorLibrary.Services;

namespace TextEditor.Services
{
    /// <summary>
    /// Сервис для работы с представлением файла.
    /// </summary>
    public class FileViewService : IFileViewService
    {
        public IMainForm _mainForm { get; set; }
        public IFileManagerService _fileManagerService { get; set; }
        public IMessageService _messageService { get; set; }

        public string _currentFilePath;

        public FileViewService(IMainForm mainForm, IFileManagerService fileManagerService, IMessageService messageService)
        {
            _mainForm = mainForm;
            _fileManagerService = fileManagerService;
            _messageService = messageService;
            //_view = new MainForm();
            //_manager = new FileManagerService();
            //_messageService = new MessageService();
        }

        /// <summary>
        /// Сохраняет файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Save(object sender, EventArgs e)
        {
            try
            {
                string content = _mainForm.Content;
                _fileManagerService.SaveContent(content, _currentFilePath);
                _messageService.ShowMessage("Файл успешно сохранен.");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Открывает файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Open(object sender, EventArgs e)
        {
            try
            {
                string filePath = _mainForm.FilePath;
                bool isExist = _fileManagerService.IsExist(filePath);

                if (!isExist)
                {
                    _messageService.ShowExclamation("Выбранный файл не существует.");
                    return;
                }

                _currentFilePath = filePath;

                string content = _fileManagerService.GetContent(filePath);
                int count = _fileManagerService.GetSymbolCount(content);

                // Установка данных на форме.
                _mainForm.Content = content;
                _mainForm.SetSymbolCount(count);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Изменяет содержимое файла.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ContentChanged(object sender, EventArgs e)
        {
            string content = _mainForm.Content;
            int count = _fileManagerService.GetSymbolCount(content);
            _mainForm.SetSymbolCount(count);
        }
    }
}
