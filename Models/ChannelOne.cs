using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace ThesisApplication.Models
{
    public class ChannelOne : ObservableObject
    {
        private DateTime _date;
        private int _entryId;
        private string? _geo1;
        private string? _geo2;
        private float? _temperature;
        private float? _humidity;
        private int? _bootReport;

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

        public string? field1
        {
            get { return _geo1; }
            set
            {
                _geo1 = value;
                RaisePropertyChanged(nameof(field1));
            }
        }

        public string? field2
        {
            get { return _geo2; }
            set
            {
                _geo2 = value;
                RaisePropertyChanged(nameof(field2));
            }
        }

        public float? field3
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                RaisePropertyChanged(nameof(field3));
            }
        }

        public float? field4
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                RaisePropertyChanged(nameof(field4));
            }
        }

        public int? field5
        {
            get { return _bootReport; }
            set
            {
                _bootReport = value;
                RaisePropertyChanged(nameof(field5));
            }
        }
    }
}
