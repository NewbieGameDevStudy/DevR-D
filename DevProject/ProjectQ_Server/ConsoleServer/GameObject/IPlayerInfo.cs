using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public interface IPlayerInfo
    {
        float Xpos { get; }
        float Ypos { get; }
        int Level { get; }
        int Exp { get; }
    }
}
