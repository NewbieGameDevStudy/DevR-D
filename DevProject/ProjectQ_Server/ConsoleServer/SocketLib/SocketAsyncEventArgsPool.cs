using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NetworkSocket
{
    public class SocketAsyncEventArgsPool
    {

        private Stack<SocketAsyncEventArgs> m_pool;

        public SocketAsyncEventArgsPool(int numConnection)
        {
            m_pool = new Stack<SocketAsyncEventArgs>(numConnection);
        }

        public void Push(SocketAsyncEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("socketAsyncEventArgs is null");

            lock (m_pool) {
                m_pool.Push(e);
            }
        }

        public SocketAsyncEventArgs Pop()
        {
            lock (m_pool) {
                return m_pool.Pop();
            }
        }

        public int Count {
            get {
                return m_pool.Count;
            }
        }
    }
}
