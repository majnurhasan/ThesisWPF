using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace ThesisApplication.Models
{
    public class Trap : ObservableObject
    {
        private int _trapNumber;
        private string _geo1;
        private string _geo2;
        private int _noOfCaughtMosquitoes;

        public int TrapNumber
        {
            get { return _trapNumber; }
            set
            {
                _trapNumber = value;
                RaisePropertyChanged(nameof(TrapNumber));
            }
        }

        public string Geo1
        {
            get { return _geo1; }
            set
            {
                _geo1 = value;
                RaisePropertyChanged(nameof(Geo1));
            }
        }

        public string Geo2
        {
            get { return _geo2; }
            set
            {
                _geo2 = value;
                RaisePropertyChanged(nameof(Geo2));
            }
        }

        public int NoOfCaughtMosquitoes
        {
            get { return _noOfCaughtMosquitoes; }
            set
            {
                _noOfCaughtMosquitoes = value;
                RaisePropertyChanged(nameof(NoOfCaughtMosquitoes));
            }
        }
    }
}
