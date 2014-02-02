using SimpleTestCreator.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestLibrary;

namespace SimpleTestCreator.ViewModels
{
    public class MainWindowViewModel:ViewModelBase
    {
        private DataManager _testManager;

        public DataManager TestManager
        {
            get { return _testManager; }
        }
    }
}
