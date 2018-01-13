using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSocket {
    public class SocketAsyncEventArgsPool {

        private Stack<SocketAsyncEventArgs> pool;

        public SocketAsyncEventArgsPool(int numConnection) {
            pool = new Stack<SocketAsyncEventArgs>(numConnection);
        }

        public void Push(SocketAsyncEventArgs e) {
            if (e == null)
                throw new ArgumentNullException("socketAsyncEventArgs is null");

            lock (pool) {
                pool.Push(e);
            }
        }

        public SocketAsyncEventArgs Pop() {
            lock (pool) {
                return pool.Pop();
            }
        }

        public int Count {
            get {
                return pool.Count;
            }
        }
    }
}
