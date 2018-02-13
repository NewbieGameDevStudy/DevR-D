using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Player.Component
{
    public abstract class BaseComponent
    {
        public PlayerObject Parent { get; private set; }
        public BaseComponent(PlayerObject player)
        {
            Parent = player;
        }

        public abstract void Update(double deltaTime);
    }
}
