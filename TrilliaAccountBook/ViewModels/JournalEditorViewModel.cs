﻿using Prism.Commands;
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

namespace TrilliaAccountBook.ViewModels
{
    public class JournalEditorViewModel : BindableBase
    {
        private DatabaseController _dc;
        private int _slipNo;
        private DateTime _slipDate;
        private string _description;
        private int _debitAccountCode;
        private int _creditAccountCode;
        private int _price;
        private string _messageString;
        private ObservableCollection<ComboBoxViewModel> _accounts = new ObservableCollection<ComboBoxViewModel>();

        private readonly IRegionManager _regionManager;

        public JournalEditorViewModel(IRegionManager regionManager)
        {
            var dc = new DatabaseController();

            _regionManager = regionManager;
            SlipDate = DateTime.Today;
            OKCommand = new DelegateCommand(OKCommandExecute);
            CancelCommand = new DelegateCommand(CancelCommandExecute);

            _dc = new DatabaseController();
            _dc.SQL = "SELECT "
                    + "  account_code "
                    + "  , account_name "
                    + " FROM "
                    + "   accounts "
                    + " WHERE "
                    + "   state = 0 "
                    + " ORDER BY "
                    + "   account_code ";
            SqlDataReader dr = _dc.ReadAsDataReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    Accounts.Add(new ComboBoxViewModel(int.Parse(dr["account_code"].ToString()), dr["account_name"].ToString()));
                }

            }

        }
        public DelegateCommand OKCommand { get; }
        public DelegateCommand CancelCommand { get; }



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
        public string MessageString
        {
            get { return _messageString; }
            set { SetProperty(ref _messageString, value); }
        }
        public ObservableCollection<ComboBoxViewModel> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value); }
        }

        private void OKCommandExecute()
        {

            //var cf = new CommonFunctions();

            using (SqlCommand command = new SqlCommand("usp_register_account_journal", _dc.Connection))
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

                        MessageString = @"伝票番号：" + reader["arg_slip_no"].ToString() + @"で登録しました。";
                    }
                    else
                    {
                        MessageString = @"エラー";

                    }
                }
            }

        }
        private void CancelCommandExecute()
        {
            // Menu表示
            var p = new NavigationParameters();
            _regionManager.RequestNavigate("ContentRegion", nameof(Menu), p);

        }
    }
}
