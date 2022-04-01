using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrilliaAccountBook.ViewModels
{
    public sealed class ComboBoxViewModel
    {
        public ComboBoxViewModel(int Code, String Name)
        {
            AccountCode = Code;
            AccountName = Name;

        }

        public int AccountCode { get; }
        public string AccountName { get; }
    }
}
