using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public static class PacketList {
        readonly static Dictionary<int, Type> packetTypeList = new Dictionary<int, Type>();
        readonly static Dictionary<Type, int> reversePacketTypeList = new Dictionary<Type, int>();

        public static void InitPacketList() {

            var packets = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assm => assm.GetTypes())
                .Where(t => t.BaseType == typeof(PK_BASE))
                .OrderBy(t => t.FullName)
                .ToArray();

            int index = 0;
            foreach(var packet in packets) {

                packetTypeList.Add(index, packet);
                reversePacketTypeList.Add(packet, index);

                index++;
            }
        }

        public static int GetPacketID(Type packetType) {
            if (reversePacketTypeList.ContainsKey(packetType))
                return reversePacketTypeList[packetType];

            return -1;
        }

        public static Dictionary<int, Type> GetTypes() {
            return packetTypeList;
        }
    }

    public class PacketMethod {

        Dictionary<int, MethodInfo> methodList = new Dictionary<int, MethodInfo>();

        public bool SetMethod(Type typeClass, string funcName) {
            var methods = new List<MethodInfo>();

            foreach(var method in typeClass.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)) {
                if(method.Name == funcName) {
                    methods.Add(method);
                }
            }

            foreach (var method in methods) {
                var parameters = method.GetParameters();
                if (parameters.Length != 1) {
                    continue;
                }

                var pksId = PacketList.GetPacketID(parameters[0].ParameterType);

                if (pksId == -1)
                    continue;

                methodList.Add(pksId, method);
            }

            return true;
        }

        public MethodInfo MethodDispatch(int packetID) {
            if (!methodList.ContainsKey(packetID)) {
                Console.WriteLine("패킷함수가 없음");
                return null;
            }

            return methodList[packetID];
        }
    }
}
