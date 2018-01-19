using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public static class PacketParser {
        private static Dictionary<int, MethodInfo> genericDeserializer = new Dictionary<int, MethodInfo>();
        private static Dictionary<Type, MethodInfo> genericSerializer = new Dictionary<Type, MethodInfo>();

        public static int HeaderSize => sizeof(Int32) * 2;

        public static void InitGenericParseMethod() {
            foreach (var type in PacketList.GetTypes()) {
                genericDeserializer.Add(type.Key, typeof(PacketParser).GetMethod("Deserializer_Packet", new[] {
                    typeof(MemoryStream) }).MakeGenericMethod(type.Value));
            }

            foreach (var type in PacketList.GetTypes()) {
                genericSerializer.Add(type.Value, typeof(PacketParser).GetMethod("Serializer_Packet", new[] {
                    typeof(MemoryStream), typeof(PK_BASE) }).MakeGenericMethod(type.Value));
            }
        }

        #region Deserializer
        public static int Deserializer_GetTotalLength(byte[] buffer) {
            return BitConverter.ToInt32(buffer, 0);
        }

        public static int Deserializer_GetPacketID(byte[] buffer) {
            return BitConverter.ToInt32(buffer, sizeof(Int32));
        }

        public static PK Deserializer_Packet<PK>(MemoryStream ms) where PK : PK_BASE {
            var packet = ProtoBuf.Serializer.Deserialize<PK>(ms);
            return packet;
        }

        public static PK_BASE Deserializer_Parser(int id, MemoryStream ms) {
            if (!genericDeserializer.ContainsKey(id)) {
                Console.WriteLine("Not GenericParser");
                return null;
            }

            var genericParser = genericDeserializer[id];
            return genericParser?.Invoke(null, new object[] { ms }) as PK_BASE;
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

        public static MemoryStream Serializer_Packet<PK>(MemoryStream ms, PK_BASE pks) where PK : PK_BASE {
            var packet = pks as PK;
            ProtoBuf.Serializer.Serialize(ms, packet);
            return ms;
        }

        public static MemoryStream Serializer_Parser(PK_BASE pks) {
            if (!genericSerializer.ContainsKey(pks.GetType())) {
                Console.WriteLine("Not GenericParser");
                return null;
            }

            MemoryStream ms = new MemoryStream();
            var genericParser = genericSerializer[pks.GetType()];
            return genericParser?.Invoke(null, new object[] { ms, pks }) as MemoryStream;
        }

        #endregion

    }
}
