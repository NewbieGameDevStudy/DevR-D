using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CellphoneMmmSender
    {
        private CellPhoneService phoneService;

        public CellphoneMmmSender(CellPhoneService service)
        {
            phoneService = service;
        }

        /// <summary>
        /// send 메소드의 테스트 코드를 작성해보자
        /// 반환값이 있다면 반환값으로 될텐데..근데 void형이다
        /// </summary>
        /// <param name="msg"></param>
        public void send(string msg)
        {
            phoneService.sendMMS(msg);
        }
    }

    public class CellPhoneService
    {
        private string msgName;

        /// <summary>
        /// 실제 아래의 sendMMS는 내부적으로 다양한 것?들을 함.
        /// 문제 메시지를 보내는 등..
        /// </summary>
        /// <param name="msg"></param>
        public virtual void sendMMS(string msg)
        {
            msgName = msg;
        }
    }

    public interface IPhoneService
    {
        void sendMMS(string msg);
    }
}
