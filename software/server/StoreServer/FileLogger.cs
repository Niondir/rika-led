using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StoreServer
{
    /// <summary>
    /// A TextWriter to write a logfile
    /// </summary>
    public class FileLogger : TextWriter, IDisposable
    {
        private string m_FileName;
        private bool m_NewLine;

        /// <summary>
        /// Format of the date string
        /// </summary>
        public const string DateFormat = "[MMMM dd hh:mm:ss.f tt]: ";

        /// <summary>
        /// Filename of the Logfile
        /// </summary>
        public string FileName { get { return m_FileName; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public FileLogger(string file)
            : this(file, false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="append"></param>
        public FileLogger(string file, bool append)
        {
            file = Path.Combine(Program.BaseDirectory, file);
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

        /// <summary>
        /// Write a char
        /// </summary>
        /// <param name="ch"></param>
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

        /// <summary>
        /// Write a string
        /// </summary>
        /// <param name="str"></param>
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

        /// <summary>
        /// Write a line
        /// </summary>
        /// <param name="line"></param>
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

        /// <summary>
        /// Enconding of the logfile, readonly
        /// </summary>
        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }
}
