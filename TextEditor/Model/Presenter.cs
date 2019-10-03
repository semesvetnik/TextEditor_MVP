using System;
using TextEditor.Interfaces;
using TextEditorLibrary.Interfaces;

namespace TextEditorLibrary.Model
{
    /// <summary>
    /// Управляющий код. Управляет моделью и графическим интерфейсом.
    /// </summary>
    public class Presenter : IPresenterService
    {
        public IMainForm _mainForm { get; set; }
        public IFileManagerService _fileManagerService { get; set; }
        public IMessageService _messageService { get; set; }
        public IFileViewService _fileViewService { get; set; }

        public string _currentFilePath;

        /// <summary>
        /// Инициализация.
        /// </summary>
        /// <param name="view">Форма.</param>
        /// <param name="manager">Сервис менеджера файла.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="fileViewService">Сервис файла.</param>
        public Presenter(IMainForm view, IFileManagerService manager, IMessageService messageService,
            IFileViewService fileViewService)
        {
            _mainForm = view;
            _fileManagerService = manager;
            _messageService = messageService;
            _fileViewService = fileViewService;

            _mainForm.SetSymbolCount(0);

            _mainForm.ContentChanged += new EventHandler(ContentChanged);
            _mainForm.FileOpenClick += new EventHandler(Open);
            _mainForm.FileSaveClick += new EventHandler(Save);

            //_mainForm.ContentChanged += new EventHandler(_fileViewService.ContentChanged);
            //_mainForm.FileOpenClick += new EventHandler(_fileViewService.Open);
            //_mainForm.FileSaveClick += new EventHandler(_fileViewService.Save);
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
                    if (filePath == null || filePath == "")
                    {
                        _messageService.ShowExclamation("Требуется указать имя файла.");
                        return;
                    }

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
