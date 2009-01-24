using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StoreServer
{
    public class FileLogger : TextWriter, IDisposable
    {
        private string m_FileName;
        private bool m_NewLine;
        public const string DateFormat = "[MMMM dd hh:mm:ss.f tt]: ";

        public string FileName { get { return m_FileName; } }

        public FileLogger(string file)
            : this(file, false)
        {
        }

        public FileLogger(string file, bool append)
        {
            m_FileName = file;
            string dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            /*
            if (!File.Exists(file))
            {
                File.Create(file);
            }*/

            using (StreamWriter writer = new StreamWriter(new FileStream(m_FileName, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                writer.WriteLine(">>>Logging started on {0}.", DateTime.Now.ToString("f")); //f = Tuesday, April 10, 2001 3:51 PM 
            }
            m_NewLine = true;
        }

        public override void Write(char ch)
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(m_FileName, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                if (m_NewLine)
                {
                    writer.Write(DateTime.Now.ToString(DateFormat));
                    m_NewLine = false;
                }
                writer.Write(ch);
            }
        }

        public override void Write(string str)
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(m_FileName, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                if (m_NewLine)
                {
                    writer.Write(DateTime.Now.ToString(DateFormat));
                    m_NewLine = false;
                }
                writer.Write(str);
            }
        }

        public override void WriteLine(string line)
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(m_FileName, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                if (m_NewLine)
                    writer.Write(DateTime.Now.ToString(DateFormat));
                writer.WriteLine(line);
                m_NewLine = true;
            }
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }
}
