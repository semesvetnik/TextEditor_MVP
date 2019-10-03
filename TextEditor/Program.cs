using System;
using Autofac;
using System.Windows.Forms;
using TextEditor.Interfaces;
using TextEditor.Services;
using TextEditorLibrary.Interfaces;
using TextEditorLibrary.Model;
using TextEditorLibrary.Services;

namespace TextEditor
{
    static class Program
    {
        static IMainForm _mainForm { get; set; }
        static IFileManagerService _fileManagerService { get; set; }
        static IFileViewService _fileViewService { get; set; }
        static IMessageService _messageService { get; set; }
        static IPresenterService _presenterService { get; set; }

        static IContainer Container { get; set; }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();

            builder.RegisterType<FileManagerService>().As<IFileManagerService>();
            builder.RegisterType<FileViewService>().As<IFileViewService>();
            builder.RegisterType<MainForm>().As<IMainForm>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<Presenter>().As<IPresenterService>();

            builder.Register(c => new FileManagerService()).As<IFileManagerService>();
            builder.Register(c => new FileViewService(c.Resolve<IMainForm>(), c.Resolve<IFileManagerService>(),
                c.Resolve<IMessageService>())).As<IFileViewService>();
            builder.Register(c => new MainForm()).As<IMainForm>();
            builder.Register(c => new MessageService()).As<IMessageService>();
            builder.Register(c => new Presenter(c.Resolve<IMainForm>(), c.Resolve<IFileManagerService>(),
                c.Resolve<IMessageService>(), c.Resolve<IFileViewService>())).As<IPresenterService>();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                _mainForm = scope.Resolve<IMainForm>();
                _messageService = scope.Resolve<IMessageService>();
                _fileManagerService = scope.Resolve<IFileManagerService>();
                _fileViewService = scope.Resolve<IFileViewService>();
                _presenterService = scope.Resolve<IPresenterService>();
            }

            //MainForm form = _mainForm.Get();
            MainForm form = new MainForm();
            MessageService messageService = new MessageService();
            FileManagerService fileManagerService = new FileManagerService();
            FileViewService fileService = new FileViewService(_mainForm, _fileManagerService, _messageService);

            //Presenter presenter = new Presenter(form, _fileManagerService, _messageService, _fileViewService);
            Presenter presenter = new Presenter(form, fileManagerService, messageService, fileService);

            Application.Run(form);
            //Application.Run(form);
        }
    }
}
