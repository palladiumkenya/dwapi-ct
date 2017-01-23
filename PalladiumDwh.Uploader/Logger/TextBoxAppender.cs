using System;
using System.Windows.Forms;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace PalladiumDwh.Uploader.Logger
{
    public class TextBoxAppender : IAppender
    {
        private TextBox _textBox;
        private readonly object _lockObj = new Object();

        public TextBoxAppender(TextBox textBox)
        {
            var form = textBox.FindForm();
            if (form == null) return;
            form.FormClosing += delegate { Close(); };
            _textBox = textBox;
            Name = "TextBoxAppender";
        }

        public string Name { get; set; }

        public static void ConfigureTextBoxAppender(TextBox textBox)
        {
            var hierarchy = (Hierarchy) LogManager.GetRepository();
            var appender = new TextBoxAppender(textBox);
            hierarchy.Root.AddAppender(appender);
        }

        public void Close()
        {
            try
            {
                lock (_lockObj)
                {
                    _textBox = null;
                }
                var hierarchy = (Hierarchy) LogManager.GetRepository();
                hierarchy.Root.RemoveAppender(this);
            }
            catch
            {
                // ignored
            }
        }

        public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
        {
            if (_textBox == null)
                return;
            var message = string.Concat(loggingEvent.RenderedMessage, "\r\n");
            lock (_lockObj)
            {
                if (_textBox == null)
                    return;
                var del = new Action<string>(s => _textBox.AppendText(s));
                _textBox.BeginInvoke(del, message);
            }
        }
    }
}
