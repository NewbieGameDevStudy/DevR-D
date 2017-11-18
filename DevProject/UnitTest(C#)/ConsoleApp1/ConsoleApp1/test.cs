using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CheckingAccount
    {
        private double m_balance;
        private string name;

        public double Balance {
            get { return m_balance; }
        }

        public CheckingAccount(string name, double balance)
        {
            this.name = name;
            m_balance = balance;
        }

        public void Withdraw(double amount)
        {
            if (m_balance >= amount)
            {
                m_balance -= amount;
            }
            else
            {
                throw new ArgumentException(amount + "Withdrawal exceeds balance!");
            }
        }

    }
}
