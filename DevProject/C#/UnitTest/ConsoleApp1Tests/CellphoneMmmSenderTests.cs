using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1Tests.CellPhoneServiceMock;

namespace ConsoleApp1.Tests
{
    [TestClass()]
    public class CellphoneMmmSenderTests
    {
        [TestMethod()]
        public void sendTest()
        {
            string message = "테스트 문자 메시지 입니다.";
            
            //Mock Object 생성
            CellPhoneServiceMock mockSerivce = new CellPhoneServiceMock();

            //생성자에 Mock 전달
            CellphoneMmmSender sender = new CellphoneMmmSender(mockSerivce);

            //문자 메시지 전송
            sender.send(message);

            Assert.IsTrue(mockSerivce.isSendMMSCall());
            Assert.AreEqual(message, mockSerivce.getSendMsg());
        }
    }
}