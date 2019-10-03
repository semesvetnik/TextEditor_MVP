using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditorLibrary.Interfaces;

namespace TextEditorLibrary.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class FileManagerService : IFileManagerService
    {
        /// <summary>
        /// Кодировка по умолчанию.
        /// </summary>
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);

        /// <summary>
        /// Проверяет, существует ли заданный файл.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <returns>Признак наличия файла.</returns>
        public bool IsExist(string filePath)
        {
            bool isExist = File.Exists(filePath);
            return isExist;
        }

        /// <summary>
        /// Возвращает количество символов заданной строки.
        /// </summary>
        /// <param name="content">Строка.</param>
        /// <returns>Количество символов.</returns>
        public int GetSymbolCount(string content)
        {
            int count = content.Length;
            return count;
        }

        /// <summary>
        /// Получает содержимое файла в указанной кодировке.
        /// </summary>
        /// <param name="filePath">Путь к искомому файлу.</param>
        /// <param name="encoding">Кодировка.</param>
        /// <returns>Строка в указанной кодировке.</returns>
        public string GetContent(string filePath, Encoding encoding)
        {
            string content = File.ReadAllText(filePath, encoding);
            return content;
        }

        /// <summary>
        /// Получает содержимое файла в кодировке по умолчанию.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Строка в кодировке по умолчанию.</returns>
        public string GetContent(string filePath)
        {
            string content = GetContent(filePath, _defaultEncoding);
            return content;
        }

        /// <summary>
        /// Сохраняет строку в указанной кодировке в файл.
        /// </summary>
        /// <param name="content">Строка.</param>
        /// <param name="filePath">Размещение нового файла.</param>
        /// <param name="encoding">Кодировка.</param>
        public void SaveContent(string content, string filePath, Encoding encoding)
        {
            File.WriteAllText(filePath, content, encoding);
        }

        /// <summary>
        /// Сохраняет строку в кодировке по умолчанию.
        /// </summary>
        /// <param name="content">Строка.</param>
        /// <param name="filePath">Размещение нового файла.</param>
        public void SaveContent(string content, string filePath)
        {
            SaveContent(content, filePath, _defaultEncoding);
        }
    }
}
