using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public static class PacketParser {
        private static Dictionary<int, MethodInfo> genericParsers;

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

        public static void InitGenericParseMethod() {
            genericParsers = new Dictionary<int, MethodInfo>();

            foreach(var type in PacketList.GetTypes()) {
                genericParsers.Add(type.Key, typeof(PacketParser).GetMethod("Parser", new[] {
                    typeof(MemoryStream) }).MakeGenericMethod(type.Value));
            }
        }

        public static PK_BASE Parser(int id, MemoryStream ms) {
            if (!genericParsers.ContainsKey(id))
                Console.WriteLine("Not GenericParser");

            var genericParser = genericParsers[id];
            return genericParser?.Invoke(null, new object[] { ms }) as PK_BASE;
        }

        public static PK Parser<PK>(MemoryStream ms) where PK : PK_BASE {
            var packet = ProtoBuf.Serializer.Deserialize<PK>(ms);
            return packet;
        }
    }
}
