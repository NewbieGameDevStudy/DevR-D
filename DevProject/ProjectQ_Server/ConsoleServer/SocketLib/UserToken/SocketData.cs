using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib {
    public class SocketData {

        MemoryStream ms;
        byte[] packetBuffer;

        int headerSize = sizeof(Int32) * 2;
        int packetId;
        int currentLength;          //현재까지 누적된 바이트
        int totalPacketLength;      //패킷의 전체 바이트 길이

        public SocketData() {
            ms = new MemoryStream();
        }

        public void ReceiveBuffer(byte[] buffer, int totalBytes) {
            int offset = 0;                 //원본 buffer로부터의 offset 위치
            int requireLength = 0;          //앞으로 받아야할 바이트
            int remainLength = totalBytes;  //현재 받아야할 남은 바이트

            if (remainLength == 0)
                return;

            //패킷은 {해더 : 전체길이, packetId(int32), 바디 : 패킷 내용}
            //받아야할 남은 바이트가 있다면 계속진행

            //가장 처음이라면 먼저 패킷의 헤더를 확인한다.
            if (currentLength == 0) {
                totalPacketLength = BitConverter.ToInt32(buffer, sizeof(Int32));
                packetId = BitConverter.ToInt32(buffer, sizeof(Int32));
                packetBuffer = new byte[totalPacketLength];

                offset = headerSize;
                remainLength -= offset;
                currentLength += offset;
            } else {
                offset = currentLength;
            }

            while (remainLength > 0) {
                //앞으로 받아야할 바이트
                requireLength = totalPacketLength - currentLength;

                if (remainLength >= requireLength) {
                    //헤더사이즈를 뺸 나머지를 구한다
                    Buffer.BlockCopy(buffer, offset, packetBuffer, currentLength - headerSize, requireLength);

                    offset += requireLength;
                    currentLength += requireLength;
                    remainLength -= currentLength;

                    //모두 받은거라면
                    if (totalPacketLength == currentLength) {

                        //메모리 스트림에 기록해두고 나머진 초기화
                        ms.SetLength(totalPacketLength - headerSize);
                        ms.Write(packetBuffer, 0, packetBuffer.Length);
                        ms.Seek(0, SeekOrigin.Begin);

                        packetBuffer = null;
                        currentLength = 0;
                        totalPacketLength = 0;
                        remainLength = 0;
                    }
                } else {
                    Buffer.BlockCopy(buffer, offset, packetBuffer, currentLength - headerSize, remainLength);
                    currentLength += remainLength;
                    remainLength = 0;
                }
            }
        }
    }
}
