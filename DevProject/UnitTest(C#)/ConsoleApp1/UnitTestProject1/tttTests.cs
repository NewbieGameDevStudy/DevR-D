using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Tests
{
    [TestClass()]
    public class tttTests
    {
        [TestMethod()]
        //표준 동작을 확인하는 테스트
        public void Withdraw_ValidAmount()
        {
            //arrange
            //단위 테스트 메서드의 정렬 섹션은 객체를 초기화하고
            //테스트 중인 메서드에 전달되는 데이터의 값을 설정한다
            double currentBalance = 10.0;
            double withdrawal = 1.0;
            double expected = 9.0;

            var account = new CheckingAccount("JohnDoe", currentBalance);
            //action
            //동작 섹션은 정렬된 매개 변수를 사용하여 테스트 중인 메서드를 호출한다.
            account.Withdraw(withdrawal);
            double actual = account.Balance;

            //assert
            //테스트 중인 메서드의 작업이 예상한 대로 작동하는지 확인한다
            Assert.AreEqual(expected, actual);
        }

        //잔액 이상의 출금이 실패하는지 확인하는 테스트
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Withdraw_AmountMore()
        {
            //arrange
            var account = new CheckingAccount("John Doe", 10.0);
            //act
            account.Withdraw(20.0);
            //assert
            //여기서 assert는 위에 ExpectedException으로 대체된다
            //내부적으로 단위 테스트 프레임워크는 try ~ catch문으로 되어있다.
            //ExpectedException 특성은 지정된 예외가 throw될 경우 테스트 메서드가 통과되도록 만든다.
        }
    }
}