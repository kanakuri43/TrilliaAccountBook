﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using TrilliaAccountBook.Views;

namespace TrilliaAccountBook.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public MenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            JournalEntryCommand = new DelegateCommand(JournalEntryCommandExecute);

        }
        public DelegateCommand JournalEntryCommand { get; }

        private void JournalEntryCommandExecute()
        {
            // Menu表示
            var p = new NavigationParameters();
            _regionManager.RequestNavigate("ContentRegion", nameof(JournalEditor), p);

        }

    }

}

