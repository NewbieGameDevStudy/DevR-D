﻿using NetworkSocket;
using Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient {
    public partial class Client {
        UserToken userToken;

        public Client(UserToken userToken) {
            this.userToken = userToken;
        }
    }
}
