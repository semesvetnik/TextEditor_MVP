using System;

namespace TextEditor.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с представлением файла.
    /// </summary>
    public interface IFileViewService
    {
        void ContentChanged(object sender, EventArgs e);
        void Open(object sender, EventArgs e);
        void Save(object sender, EventArgs e);
    }
}