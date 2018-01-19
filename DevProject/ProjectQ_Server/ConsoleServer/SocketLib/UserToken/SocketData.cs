using Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSocket {
    public class SocketData : ICloneable {
        public int PacketId { get; private set; }
        public long MsLength {
            get {
                return Ms.Length;
            }
        }

        public MemoryStream Ms { get; private set; }
        byte[] buffer = new byte[1024];
        byte[] packetBuffer = new byte[1024];

        int headerSize = sizeof(Int32) * 2;
        int currentLength;          //현재까지 누적된 바이트
        int totalPacketLength;      //패킷의 전체 바이트 길이
        int currentBufferOffset;
        int bufferOffset;

        public SocketData() {
            Ms = new MemoryStream();
        }

        //TODO : 내부적으로 모든 복사를 진행하지 않아서 문제가 될 수도 있다.
        protected SocketData(SocketData socketData) {
            this.PacketId = socketData.PacketId;
            this.currentLength = socketData.currentLength;
            this.totalPacketLength = socketData.totalPacketLength;
            this.Ms = new MemoryStream();
            this.Ms.Write(socketData.Ms.GetBuffer(), 0, (int)socketData.Ms.Length);
        }

        public object Clone() {
            return new SocketData(this);
        }

        public void ReceiveBuffer(byte[] buffer, int offset, int totalBytes) {
            int requireLength = 0;          //앞으로 받아야할 바이트
            int remainLength = totalBytes;  //현재 받아야할 남은 바이트

            if (remainLength == 0)
                return;

            Array.Copy(buffer, offset, this.buffer, 0, 1024);

            //패킷은 {해더 : 전체길이, packetId(int32), 바디 : 패킷 내용}
            //받아야할 남은 바이트가 있다면 계속진행

            //가장 처음이라면 먼저 패킷의 헤더를 확인한다.
            if (currentLength == 0) {
                if (totalBytes < PacketParser.HeaderSize)
                    return;

                bufferOffset = offset;

                totalPacketLength = PacketParser.Deserializer_GetTotalLength(buffer);
                PacketId = PacketParser.Deserializer_GetPacketID(buffer);

                currentBufferOffset = headerSize;
                remainLength -= currentBufferOffset;
            } else {
                currentBufferOffset = currentLength;
            }

            while (remainLength > 0) {
                //앞으로 받아야할 바이트
                requireLength = totalPacketLength - currentLength;

                if (remainLength >= requireLength) {
                    //처음에는 헤더사이즈를 뺸 나머지를 복사한다.
                    Buffer.BlockCopy(buffer, currentBufferOffset, packetBuffer, currentLength, requireLength);

                    currentBufferOffset += requireLength;
                    currentLength += requireLength;
                    remainLength -= currentLength;

                    //모두 받은거라면
                    if (totalPacketLength == currentLength) {

                        //메모리 스트림에 기록해두고 나머진 초기화
                        Ms.SetLength(totalPacketLength);
                        Ms.Write(packetBuffer, 0, packetBuffer.Length);
                        Ms.Seek(0, SeekOrigin.Begin);

                        currentBufferOffset = 0;
                        currentLength = 0;
                        totalPacketLength = 0;
                        remainLength = 0;
                    }
                } else {
                    Buffer.BlockCopy(buffer, currentBufferOffset, packetBuffer, currentLength, remainLength);
                    currentLength += remainLength;
                    remainLength = 0;
                }
            }
        }

        //TODO : 풀링가능?
        public byte[] SendBuffer(PK_BASE pks) {
            var packetId = PacketList.GetPacketID(pks.GetType());
            var ms = PacketParser.Serializer_Parser(pks);
            var length = (int)ms.Length;

            byte[] buffer = null;
            byte[] headerByte = PacketParser.Serializer_ConvertByte(length, packetId);

            buffer = new byte[headerByte.Length + length];

            Array.Copy(headerByte, 0, buffer, 0, headerByte.Length);
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(buffer, headerByte.Length, length);

            return buffer;
        }

        public void ReadBuffer(byte[] buffer) {
            Ms.Seek(0, SeekOrigin.Begin);
            Ms.Read(buffer, 0, (int)Ms.Length);
        }

    }
}
