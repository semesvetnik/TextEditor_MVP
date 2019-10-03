using System;
using System.Drawing;
using System.Windows.Forms;
using TextEditor.Interfaces;

namespace TextEditor
{
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();

            btnOpenFile.Click += new EventHandler(btnOpenFile_Click);
            btnSaveFile.Click += new EventHandler(btnSaveFile_Click);
            tbContent.TextChanged += new EventHandler(tbContent_TextChanged);
            btnSelectFile.Click += btnSelectFile_Click;
            udFont.ValueChanged += udFont_ValueChange;
        }

        //public MainForm Get()
        //{
        //    InitializeComponent();

        //    btnOpenFile.Click += new EventHandler(btnOpenFile_Click);
        //    btnSaveFile.Click += new EventHandler(btnSaveFile_Click);
        //    tbContent.TextChanged += new EventHandler(tbContent_TextChanged);
        //    btnSelectFile.Click += btnSelectFile_Click;
        //    udFont.ValueChanged += udFont_ValueChange;

        //    return this;
        //}

        #region Проброс событий
        void btnOpenFile_Click(object sender, EventArgs e)
        {
            //if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
            FileOpenClick?.Invoke(this, EventArgs.Empty);
        }

        void btnSaveFile_Click(object sender, EventArgs e)
        {
            FileSaveClick?.Invoke(this, EventArgs.Empty);
        }

        void tbContent_TextChanged(object sender, EventArgs e)
        {
            ContentChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region IMainForm

        public string FilePath
        {
            get { return tbFilePath.Text; }
        }

        public string Content
        {
            get { return tbContent.Text; }
            set { tbContent.Text = value; }
        }

        public void SetSymbolCount(int count)
        {
            stlSymbolCount.Text = count.ToString();
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;

        #endregion

        #region Обработка внутренних элементов управления

        /// <summary>
        /// Вызывает диалог открытия файла, указывает для него необходимые фильтры.
        /// По умолчанию отображается .txt. Можно отобразить все типы файлов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Текстовые файлы|*.txt|Все файлы|*.*"
            };

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = ofd.FileName;

                FileOpenClick?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Выбор шрифта.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void udFont_ValueChange(object sender, EventArgs e)
        {
            tbContent.Font = new Font("Calibri", (float)udFont.Value);
        }
        #endregion
    }
}
