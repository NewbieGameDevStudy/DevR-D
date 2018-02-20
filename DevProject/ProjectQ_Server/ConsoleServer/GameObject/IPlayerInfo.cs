using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public interface IPlayerInfo
    {
        double Xpos { get; }
        double Ypos { get; }
        int Level { get; }
        int Exp { get; }
    }
}
