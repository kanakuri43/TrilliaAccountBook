using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrilliaAccountBook.ViewModels
{
    public sealed class Accounts
    {
        public Accounts(int AccountCode, String AccountName)
        {
            Code = AccountCode;
            Name = AccountName;
        }

        public int Code { get; }
        public string Name { get; }
    }
}
