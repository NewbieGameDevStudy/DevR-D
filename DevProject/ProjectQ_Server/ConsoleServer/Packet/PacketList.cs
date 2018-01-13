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
    }

    public class PacketPaser {

    }
}
