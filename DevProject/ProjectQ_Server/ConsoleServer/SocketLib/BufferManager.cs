using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib {
    public class BufferManager {

        private byte[] buffer;
        private int totalByteSize;
        private int currentIndex;
        private int bufferSize;

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

            e.SetBuffer(buffer, bufferSize * currentIndex, bufferSize);
            currentIndex++;
        }

        //TODO : close해서 버퍼 반납시에 회수하는것도 만들어야한다.
    }
}
