using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Interfaces
{
    public interface IMainForm
    {
        //MainForm Get();

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Содержимое файла.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Задает количество символов.
        /// </summary>
        /// <param name="count">Количество символов.</param>
        void SetSymbolCount(int count);

        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }
}
