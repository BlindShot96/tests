using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace SimpleTestCreator.ViewModels
{
    

    class QuestionListItemViewModel: ViewModelBase
    {
        #region Fields

          private string _name;

          private string _type;

          private int _number;

        #endregion //Fields

        #region Interfaces

          public string Name
          {
            get { return _name; }
          }

          public string Type
          {
            get { return _type; }
          }

          public int Number
          {
            get { return _number; }
          }

        #endregion //Interfaces

        public QuestionListItemViewModel(QuestionBase q)
        {
            this._name = q.Name;
            this._type = q.GetTypeOfQuestion();
            this._number = q.Number;
        }
    }
}
