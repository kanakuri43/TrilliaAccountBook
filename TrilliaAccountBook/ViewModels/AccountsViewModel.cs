using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrilliaAccountBook.ViewModels
{
    public sealed class AccountsViewModel
    {
        public AccountsViewModel(int Code, String Name)
        {
            AccountCode = Code;
            AccountName = Name;

        }

        public int AccountCode { get; }
        public string AccountName { get; }
    }
}
