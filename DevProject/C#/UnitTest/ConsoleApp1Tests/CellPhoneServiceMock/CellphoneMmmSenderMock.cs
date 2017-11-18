using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace ConsoleApp1Tests.CellPhoneServiceMock
{
    public class CellphoneMmmSenderMock : CellphoneMmmSender
    {
        private CellPhoneServiceMock mockPhoneService;
        public CellphoneMmmSenderMock(CellPhoneServiceMock service) : base(service)
        {
            mockPhoneService = service;
        }

        public void send(string msg)
        {
            base.send(msg);
        }
    }
}
