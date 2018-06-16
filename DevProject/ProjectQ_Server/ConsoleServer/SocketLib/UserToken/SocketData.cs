using Packet;
using System;
using System.Collections.Generic;
using System.IO;


namespace NetworkSocket
{
    public class SocketData : ICloneable
    {
        public int PacketId { get; private set; }
        public long MsLength {
            get {
                return Ms.Length;
            }
        }

        public MemoryStream Ms { get; private set; }

        const int packetSize = 1024;

        byte[] m_buffer = new byte[packetSize];
        byte[] m_packetBuffer = new byte[packetSize];

        int m_headerSize = sizeof(Int32) * 2;
        
        int m_totalPacketLength;            //패킷의 전체 바이트 길이
        int m_remainTotalPacketLength;      //패킷전체중에서 앞으로 받을 남을량

        public SocketData()
        {
            Ms = new MemoryStream();
        }

        //TODO : 내부적으로 모든 복사를 진행하지 않아서 문제가 될 수도 있다.
        protected SocketData(SocketData socketData)
        {
            PacketId = socketData.PacketId;
            //m_currentLength = socketData.m_currentLength;
            m_totalPacketLength = socketData.m_totalPacketLength;
            Ms = new MemoryStream();
            Ms.Write(socketData.Ms.GetBuffer(), 0, (int)socketData.Ms.Length);
            Ms.Seek(0, SeekOrigin.Begin);
        }

        public object Clone()
        {
            return new SocketData(this);
        }


        //패킷 {해더 : 전체길이, packetId(int32), 바디 : 패킷 내용}
        public bool ReceiveBuffer(byte[] buffer, int offset, int totalBytes)
        {
            int receiveRemainLength = totalBytes;
            int receiveCurrentOffset = 0;

            if (receiveRemainLength == 0)
                return false;

            //받은 최대치 1024를 기준으로 복사함 1024 >= totalbytes
            Array.Copy(buffer, offset, m_buffer, 0, totalBytes);

            //처음에 받음을 시작하는경우
            if (m_remainTotalPacketLength <= 0) {
                if (totalBytes < PacketParser.HeaderSize)
                    return false;

                m_totalPacketLength = PacketParser.Deserializer_GetTotalLength(m_buffer);
                PacketId = PacketParser.Deserializer_GetPacketID(m_buffer);

                receiveCurrentOffset = m_headerSize;

                //총 남은량을 저장해둔다
                m_remainTotalPacketLength = m_totalPacketLength;

                //메모리 스트림 준비
                Ms.SetLength(m_totalPacketLength);
            }

            int requireLength = 0;
            int receiveCurrentLength = 0;

            //TODO : 만약에 한번에 받을수있는 1024자체가 끊겨서 올경우??
            //이론상으로는 그럴리는 없는것 같은데..우선 그 예외처리를 없음
            while (receiveRemainLength > 0) {
                requireLength = receiveRemainLength - receiveCurrentLength;

                if (receiveRemainLength >= requireLength) {
                    
                    //처음이라면
                    if (m_remainTotalPacketLength == m_totalPacketLength) {
                        requireLength -= m_headerSize;
                        receiveRemainLength -= m_headerSize;
                    }

                    Buffer.BlockCopy(m_buffer, receiveCurrentOffset, m_packetBuffer, receiveCurrentLength, requireLength);

                    receiveCurrentLength += requireLength;
                    receiveCurrentOffset += requireLength;
                    receiveRemainLength -= requireLength;

                    //총 남은길이 갱신
                    m_remainTotalPacketLength -= receiveCurrentLength;

                    //연속적인 패킷 또는 일반 패킷모두 받은상황이라면
                    if (m_remainTotalPacketLength <= 0) {
                        Ms.Write(m_packetBuffer, 0, m_remainTotalPacketLength + receiveCurrentLength);
                        Ms.Seek(0, SeekOrigin.Begin);

                        m_totalPacketLength = 0;
                        m_remainTotalPacketLength = 0;
                        Array.Clear(m_packetBuffer, 0, m_packetBuffer.Length);
                        Array.Clear(m_buffer, 0, m_buffer.Length);
                        break;
                    } else {
                        Ms.Write(m_packetBuffer, 0, requireLength);
                        Array.Clear(m_packetBuffer, 0, m_packetBuffer.Length);
                        Array.Clear(m_buffer, 0, m_buffer.Length);
                        return false;
                    }
                } 
            }
            return true;
        }

        //public void ReceiveBuffer(byte[] buffer, int offset, int totalBytes)
        //{
        //    int requireLength = 0;          //앞으로 받아야할 바이트
        //    int remainLength = totalBytes;  //현재 받아야할 남은 바이트

        //    if (remainLength == 0)
        //        return;

        //    Array.Copy(buffer, offset, m_buffer, 0, 1024);

        //    //패킷은 {해더 : 전체길이, packetId(int32), 바디 : 패킷 내용}
        //    //받아야할 남은 바이트가 있다면 계속진행

        //    //가장 처음이라면 먼저 패킷의 헤더를 확인한다.
        //    if (m_currentLength == 0) {
        //        if (totalBytes < PacketParser.HeaderSize)
        //            return;

        //        m_bufferOffset = offset;

        //        m_totalPacketLength = PacketParser.Deserializer_GetTotalLength(buffer, m_bufferOffset);
        //        PacketId = PacketParser.Deserializer_GetPacketID(buffer, m_bufferOffset);

        //        m_currentBufferOffset = m_headerSize;
        //        remainLength -= m_currentBufferOffset;
        //    } else {
        //        m_currentBufferOffset = m_currentLength;
        //    }

        //    while (remainLength > 0) {
        //        //앞으로 받아야할 바이트
        //        requireLength = m_totalPacketLength - m_currentLength;

        //        if (remainLength >= requireLength) {
        //            //처음에는 헤더사이즈를 뺸 나머지를 복사한다.
        //            Buffer.BlockCopy(buffer, m_bufferOffset + m_currentBufferOffset, m_packetBuffer, m_currentLength, requireLength);

        //            m_currentBufferOffset += requireLength;
        //            m_currentLength += requireLength;
        //            remainLength -= m_currentLength;

        //            //모두 받은거라면
        //            if (m_totalPacketLength == m_currentLength) {

        //                //메모리 스트림에 기록해두고 나머진 초기화
        //                Ms.SetLength(m_totalPacketLength);
        //                Ms.Write(m_packetBuffer, 0, m_totalPacketLength);
        //                Ms.Seek(0, SeekOrigin.Begin);

        //                m_currentBufferOffset = 0;
        //                m_currentLength = 0;
        //                m_totalPacketLength = 0;
        //                remainLength = 0;
        //            }
        //        } else {
        //            Buffer.BlockCopy(buffer, m_currentBufferOffset, m_packetBuffer, m_currentLength, remainLength);
        //            m_currentLength += remainLength;
        //            remainLength = 0;
        //        }
        //    }
        //}

        public List<byte[]> GetSendBuffer(PK_BASE pks)
        {
            //패킷 {해더 : 전체길이, packetId(int32), 바디 : 패킷 내용}
            var packetId = PacketList.GetPacketID(pks.GetType());
            var ms = PacketParser.Serializer_Parser(pks);
            var length = (int)ms.Length;

            byte[] headerByte = PacketParser.Serializer_ConvertByte(length, packetId);

            //1024바이트 이상일 경우 잘게 나눈다.
            List<byte[]> byteList = new List<byte[]>();
            var totalLength = headerByte.Length + length;
            if (totalLength <= packetSize) {
                byte[] buffer = new byte[totalLength];
                Array.Copy(headerByte, 0, buffer, 0, headerByte.Length);
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, headerByte.Length, length);

                byteList.Add(buffer);
                return byteList;
            }

            var remainLength = totalLength;
            while (remainLength >= 0) {
                var bufferLength = (remainLength - packetSize) >= 0 ? packetSize : remainLength;
                var divideBuffer = new byte[bufferLength];
                
                if(remainLength == totalLength) {
                    Array.Copy(headerByte, 0, divideBuffer, 0, headerByte.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(divideBuffer, headerByte.Length, bufferLength - headerByte.Length);
                } else {
                    ms.Read(divideBuffer, 0, bufferLength);
                }

                byteList.Add(divideBuffer);
                remainLength -= packetSize;
            }

            return byteList;

            //var packetId = PacketList.GetPacketID(pks.GetType());
            //var ms = PacketParser.Serializer_Parser(pks);
            //var length = (int)ms.Length;

            //byte[] buffer = null;
            //byte[] headerByte = PacketParser.Serializer_ConvertByte(length, packetId);

            ////1024바이트 이상일 경우 잘게 나눈다.
            //buffer = new byte[headerByte.Length + length];

            //Array.Copy(headerByte, 0, buffer, 0, headerByte.Length);
            //ms.Seek(0, SeekOrigin.Begin);
            //ms.Read(buffer, headerByte.Length, length);

            //return buffer;
        }

        public void ReadBuffer(byte[] buffer)
        {
            Ms.Seek(0, SeekOrigin.Begin);
            Ms.Read(buffer, 0, (int)Ms.Length);
        }

    }
}
