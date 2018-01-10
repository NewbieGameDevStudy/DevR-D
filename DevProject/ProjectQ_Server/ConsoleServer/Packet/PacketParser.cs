using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public static class PacketParser {

        #region Deserializer
        public static int Deserializer_GetTotalLength(byte[] buffer) {
            return BitConverter.ToInt32(buffer, 0);
        }

        public static int Deserializer_GetPacketID(byte[] buffer) {
            return BitConverter.ToInt32(buffer, sizeof(Int32));
        }
        #endregion

        #region Serializer

        public static byte[] Serializer_ConvertByte(int length, int packetID) {
            var lengthBytes = BitConverter.GetBytes(length);
            var msgBytes = BitConverter.GetBytes(packetID);

            var ret = new byte[lengthBytes.Length + msgBytes.Length];

            Array.Copy(lengthBytes, 0, ret, 0, lengthBytes.Length);
            Array.Copy(msgBytes, 0, ret, lengthBytes.Length, msgBytes.Length);
            return ret;
        }

        #endregion
    }
}
