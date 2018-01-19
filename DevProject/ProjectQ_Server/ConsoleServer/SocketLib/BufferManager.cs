using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSocket {
    public class BufferManager {

        byte[] buffer;
        int totalByteSize;
        int currentIndex;
        int bufferSize;

        public BufferManager(int totalByteSize, int bufferSize) {
            this.totalByteSize = totalByteSize;
            this.bufferSize = bufferSize;
            currentIndex = 0;
        }

        public void InitBuffer() {
            buffer = new byte[totalByteSize];
        }

        public void SetBuffer(SocketAsyncEventArgs e) {
            if (e == null)
                return;

            e.SetBuffer(buffer, currentIndex, bufferSize);
            currentIndex += bufferSize;
        }
    }
}
