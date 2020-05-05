using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;

namespace ThesisApplication.Models
{
    public class Collection : ObservableObject
    {
        private int _collectionId;
        private string _date;
        private int _numberOfReadings;

        // Lists
        public ObservableCollection<Reading> Readings { get; } = new ObservableCollection<Reading>();

        // Properties
        public int CollectionId
        {
            get { return _collectionId; }
            set
            {
                _collectionId = value;
                RaisePropertyChanged(nameof(CollectionId));
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(nameof(Date));
            }
        }

        public int NumberOfReadings
        {
            get { return _numberOfReadings; }
            set
            {
                _numberOfReadings = value;
                RaisePropertyChanged(nameof(NumberOfReadings));
            }
        }
    }
}
