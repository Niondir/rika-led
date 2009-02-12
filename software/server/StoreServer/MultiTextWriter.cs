using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StoreServer
{
    /// <summary>
    /// Wirite text to multiple targets
    /// </summary>
    public class MultiTextWriter : TextWriter
    {
        private List<TextWriter> m_Streams;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streams">The targets to write to</param>
        public MultiTextWriter(params TextWriter[] streams)
        {
            m_Streams = new List<TextWriter>(streams);

            if (m_Streams.Count < 0)
                throw new ArgumentException("You must specify at least one stream.");
        }

        /// <summary>
        /// Add a stream
        /// </summary>
        /// <param name="tw"></param>
        public void Add(TextWriter tw)
        {
            m_Streams.Add(tw);
        }

        /// <summary>
        /// Remnove a stream
        /// </summary>
        /// <param name="tw"></param>
        public void Remove(TextWriter tw)
        {
            m_Streams.Remove(tw);
        }

        /// <summary>
        /// Write to all streams
        /// </summary>
        /// <param name="ch"></param>
        public override void Write(char ch)
        {
            for (int i = 0; i < m_Streams.Count; i++)
                m_Streams[i].Write(ch);
        }

        /// <summary>
        /// Write to all streams
        /// </summary>
        /// <param name="line"></param>
        public override void WriteLine(string line)
        {
            for (int i = 0; i < m_Streams.Count; i++)
                m_Streams[i].WriteLine(line);
        }

        /// <summary>
        /// Write to all streams
        /// </summary>
        /// <param name="line"></param>
        /// <param name="args"></param>
        public override void WriteLine(string line, params object[] args)
        {
            WriteLine(String.Format(line, args));
        }

        /// <summary>
        /// The encoding
        /// </summary>
        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }
    }
}
