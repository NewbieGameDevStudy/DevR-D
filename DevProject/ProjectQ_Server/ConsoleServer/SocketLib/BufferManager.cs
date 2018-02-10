using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSocket
{
    public class BufferManager
    {
        byte[] m_buffer;
        int m_totalByteSize;
        int m_currentIndex;
        int m_bufferSize;

        public BufferManager(int totalByteSize, int bufferSize)
        {
            m_totalByteSize = totalByteSize;
            m_bufferSize = bufferSize;
            m_currentIndex = 0;
        }

        public void InitBuffer()
        {
            m_buffer = new byte[m_totalByteSize];
        }

        public void SetBuffer(SocketAsyncEventArgs e)
        {
            if (e == null)
                return;

            e.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
            m_currentIndex += m_bufferSize;
        }
    }
}
