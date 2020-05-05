using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace ThesisApplication.Models
{
    public class Reading : ObservableObject
    {
        private int _readingId;
        private int _collectionId;
        private string _trapNumber;
        private string _geo1;
        private string _geo2;
        private float _oFreq;
        private float _aFreq;
        private float _cFreq;
        private string _datetime;
        private float _temperature;
        private float _humidity;
        private int _genus;
        private int _species;
        private int _sex;

        // Properties
        public int ReadingId
        {
            get { return _readingId; }
            set
            {
                _readingId = value;
                RaisePropertyChanged(nameof(ReadingId));
            }
        }

        public int CollectionId
        {
            get { return _collectionId; }
            set
            {
                _collectionId = value;
                RaisePropertyChanged(nameof(CollectionId));
            }
        }

        public string TrapNumber
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

        public float OFreq
        {
            get { return _oFreq; }
            set
            {
                _oFreq = value;
                RaisePropertyChanged(nameof(OFreq));
            }
        }

        public float AFreq
        {
            get { return _aFreq; }
            set
            {
                _aFreq = value;
                RaisePropertyChanged(nameof(AFreq));
            }
        }

        public float CFreq
        {
            get { return _cFreq; }
            set
            {
                _cFreq = value;
                RaisePropertyChanged(nameof(CFreq));
            }
        }

        public string Datetime
        {
            get { return _datetime; }
            set
            {
                _datetime = value;
                RaisePropertyChanged(nameof(Datetime));
            }
        }

        public float Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                RaisePropertyChanged(nameof(Temperature));
            }
        }

        public float Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                RaisePropertyChanged(nameof(Humidity));
            }
        }

        public int Genus
        {
            get { return _genus; }
            set
            {
                _genus = value;
                RaisePropertyChanged(nameof(Genus));
            }
        }

        public int Species
        {
            get { return _species; }
            set
            {
                _species = value;
                RaisePropertyChanged(nameof(Species));
            }
        }

        public int Sex
        {
            get { return _sex; }
            set
            {
                _sex = value;
                RaisePropertyChanged(nameof(Sex));
            }
        }
    }
}
