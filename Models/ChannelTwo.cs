using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace ThesisApplication.Models
{
    public class ChannelTwo : ObservableObject
    {
        private DateTime _date;
        private int _entryId;
        private float? _oFreq;
        private float? _aFreq;
        private float? _cFreq;
        private int? _genus;
        private int? _species;
        private int? _sex;

        public DateTime created_at
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(nameof(created_at));
            }
        }

        public int entry_id
        {
            get { return _entryId; }
            set
            {
                _entryId = value;
                RaisePropertyChanged(nameof(entry_id));
            }
        }

        public float? field1
        {
            get { return _oFreq; }
            set
            {
                _oFreq = value;
                RaisePropertyChanged(nameof(field1));
            }
        }

        public float? field2
        {
            get { return _aFreq; }
            set
            {
                _aFreq = value;
                RaisePropertyChanged(nameof(field2));
            }
        }

        public float? field3
        {
            get { return _cFreq; }
            set
            {
                _cFreq = value;
                RaisePropertyChanged(nameof(field3));
            }
        }

        public int? field4
        {
            get { return _genus; }
            set
            {
                _genus = value;
                RaisePropertyChanged(nameof(field4));
            }
        }

        public int? field5
        {
            get { return _species; }
            set
            {
                _species = value;
                RaisePropertyChanged(nameof(field5));
            }
        }

        public int? field6
        {
            get { return _sex; }
            set
            {
                _sex = value;
                RaisePropertyChanged(nameof(field6));
            }
        }
    }
}
