using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace ConsoleApp1Tests.CellPhoneServiceMock
{
    /// <summary>
    /// 상속받는 이유 CellphoneMmsSender객체에서 참조할때
    /// 같은 유형의 객체이어야 동작하지 떄문이다.
    /// </summary>
    public class CellPhoneServiceMock : CellPhoneService
    {
        bool isSendMMSCalled = false;
        string sendMsg = "";

        public override void sendMMS(string msg)
        {
            isSendMMSCalled = true;
           
        }

        public bool isSendMMSCall()
        {
            return isSendMMSCalled;
        }

        public string getSendMsg()
        {
            return sendMsg;
        }
    }
}
