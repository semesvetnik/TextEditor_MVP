using System.Text;

namespace TextEditorLibrary.Interfaces
{
    public interface IFileManagerService
    {
        /// <summary>
        /// Получает содержимое файла в кодировке по умолчанию.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Строка в кодировке по умолчанию.</returns>
        string GetContent(string filePath);

        /// <summary>
        /// Получает содержимое файла в указанной кодировке.
        /// </summary>
        /// <param name="filePath">Путь к искомому файлу.</param>
        /// <param name="encoding">Кодировка.</param>
        /// <returns>Строка в указанной кодировке.</returns>
        string GetContent(string filePath, Encoding encoding);

        /// <summary>
        /// Возвращает количество символов строки.
        /// </summary>
        /// <param name="content">Строка.</param>
        /// <returns>Количество символов.</returns>
        int GetSymbolCount(string content);

        /// <summary>
        /// Проверяет, существует ли заданный файл.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <returns>Признак наличия файла.</returns>
        bool IsExist(string filePath);

        /// <summary>
        /// Сохраняет строку в кодировке по умолчанию.
        /// </summary>
        /// <param name="content">Строка.</param>
        /// <param name="filePath">Размещение нового файла.</param>
        void SaveContent(string content, string filePath);

        /// <summary>
        /// Сохраняет строку в указанной кодировке в файл.
        /// </summary>
        /// <param name="content">Строка.</param>
        /// <param name="filePath">Размещение нового файла.</param>
        /// <param name="encoding">Кодировка.</param>
        void SaveContent(string content, string filePath, Encoding encoding);
    }
}