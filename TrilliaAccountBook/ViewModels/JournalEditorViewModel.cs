using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using TrilliaAccountBook.Views;
using TrilliaAccountBookf.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TrilliaAccountBook.ViewModels
{
    public class JournalEditorViewModel : BindableBase
    {
        private ObservableCollection<ComboBoxViewModel> _extractYearList = new ObservableCollection<ComboBoxViewModel>();
        private int _extractYear;
        private int _slipNo;
        private DateTime _slipDate;
        private string _description;
        private int _debitAccountCode;
        private int _creditAccountCode;
        private int _price;
        private string _resultMessage;
        private string _slipNoMessageString;
        private ObservableCollection<ComboBoxViewModel> _accounts = new ObservableCollection<ComboBoxViewModel>();

        private readonly IRegionManager _regionManager;

        public JournalEditorViewModel(IRegionManager regionManager)
        {
            var dc = new DatabaseController();

            _regionManager = regionManager;
            SlipDate = DateTime.Today;

            OKCommand = new DelegateCommand(OKCommandExecute);
            DeleteCommand = new DelegateCommand(DeleteCommandExecute);
            CancelCommand = new DelegateCommand(CancelCommandExecute);
            SlipSearchCommand = new DelegateCommand<TextBox>(SlipSearchCommandExecute);

            dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + "  account_code "
                    + "  , account_name "
                    + " FROM "
                    + "   accounts "
                    + " WHERE "
                    + "   state = 0 "
                    + " ORDER BY "
                    + "   account_code ";
            SqlDataReader dr = dc.ReadAsDataReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    Accounts.Add(new ComboBoxViewModel(int.Parse(dr["account_code"].ToString()), dr["account_name"].ToString()));
                }
            }

            int CurrentYear = DateTime.Today.Year;
            for (int i = -5; i < 6; i++)
            {
                ExtractYearList.Add(new ComboBoxViewModel(CurrentYear + i, (CurrentYear + i).ToString()));
            }
            ExtractYear = CurrentYear;


        }
        public DelegateCommand OKCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand<TextBox> SlipSearchCommand { get; }



        public ObservableCollection<ComboBoxViewModel> ExtractYearList
        {
            get { return _extractYearList; }
            set { SetProperty(ref _extractYearList, value); }
        }
        public int ExtractYear
        {
            get { return _extractYear; }
            set { SetProperty(ref _extractYear, value); }
        }
        public int SlipNo
        {
            get { return _slipNo; }
            set { SetProperty(ref _slipNo, value); }
        }
        public DateTime SlipDate
        {
            get { return _slipDate; }
            set { SetProperty(ref _slipDate, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public int DebitAccountCode
        {
            get { return _debitAccountCode; }
            set { SetProperty(ref _debitAccountCode, value); }
        }
        public int CreditAccountCode
        {
            get { return _creditAccountCode; }
            set { SetProperty(ref _creditAccountCode, value); }
        }
        public int Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        public string ResultMessage
        {
            get { return _resultMessage; }
            set { SetProperty(ref _resultMessage, value); }
        }
        public string SlipNoMessage
        {
            get { return _slipNoMessageString; }
            set { SetProperty(ref _slipNoMessageString, value); }
        }
        public ObservableCollection<ComboBoxViewModel> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        private void OKCommandExecute()
        {
            var dc = new DatabaseController();

            if (SlipNo == 0)
            {
                // 新規登録

                using (SqlCommand command = new SqlCommand("usp_register_account_journal", dc.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@arg_slip_no", SlipNo);
                    command.Parameters.AddWithValue("@arg_slip_date", SlipDate);
                    command.Parameters.AddWithValue("@arg_description", Description);
                    command.Parameters.AddWithValue("@arg_debit_account_code", DebitAccountCode);
                    command.Parameters.AddWithValue("@arg_credit_account_code", CreditAccountCode);
                    command.Parameters.AddWithValue("@arg_price", Price);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            ResultMessage = @"伝票番号：" + reader["arg_slip_no"].ToString() + @"で登録しました。";
                        }
                        else
                        {
                            ResultMessage = @"エラー";
                        }
                    }
                }
            }
            else
            {
                // 更新はdelete insert

                using (SqlCommand command = new SqlCommand("usp_invalidate_account_journal", dc.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@arg_slip_no", SlipNo);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                        }
                        else
                        {
                            ResultMessage = @"エラー";
                        }
                    }
                }

                using (SqlCommand command = new SqlCommand("usp_register_account_journal", dc.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@arg_slip_no", SlipNo);
                    command.Parameters.AddWithValue("@arg_slip_date", SlipDate);
                    command.Parameters.AddWithValue("@arg_description", Description);
                    command.Parameters.AddWithValue("@arg_debit_account_code", DebitAccountCode);
                    command.Parameters.AddWithValue("@arg_credit_account_code", CreditAccountCode);
                    command.Parameters.AddWithValue("@arg_price", Price);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ResultMessage = @"伝票番号：" + reader["arg_slip_no"].ToString() + @"を更新しました。";
                        }
                        else
                        {
                            ResultMessage = @"エラー";
                        }
                    }
                }
            }


        }
        private void DeleteCommandExecute()
        {
            var dc = new DatabaseController();

            using (SqlCommand command = new SqlCommand("usp_invalidate_account_journal", dc.Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@arg_slip_no", SlipNo);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        ResultMessage = @"伝票番号：" + reader["arg_slip_no"].ToString() + @"を削除しました。";
                    }
                    else
                    {
                        ResultMessage = @"エラー";

                    }
                }
            }

        }
        private void CancelCommandExecute()
        {
            // Menu表示
            var p = new NavigationParameters();
            _regionManager.RequestNavigate("ContentRegion", nameof(TrilliaAccountBook.Views.Menu), p);

        }
        private void SlipSearchCommandExecute(TextBox slipNoTextBox)
        {
            if (slipNoTextBox.Text == "")
            {

            }
            else
            {
                CallSlip(int.Parse(slipNoTextBox.Text));
            }
        }
        private void CallSlip(int slipNo)
        {

            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + "  * "
                    + "FROM "
                    + "  uv_account_journal "
                    + "WHERE "
                    + "  slip_no = " + slipNo
                    ;
            SqlDataReader dr = dc.ReadAsDataReader();
            if (dr != null)
            {
                SlipNoMessage = "";

                while (dr.Read())
                {
                    SlipNo = (int)dr["slip_no"];
                    SlipDate = (DateTime)dr["slip_date"];
                    Description = dr["description"].ToString();
                    DebitAccountCode = (int)dr["debit_account_code"];
                    CreditAccountCode = (int)dr["credit_account_code"];
                    Price = (int)dr["debit_price"];
                }
            }
            else
            {
                SlipNoMessage = "Slip No. not found.";
            }



        }

    }
}
