using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using OxyPlot;
using ThesisApplication.Models;

namespace ThesisApplication.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        // Windows 
        private AnalysisWindow _analysisWindow;
        private HelpAndInformationWindow _helpAndInformationWindow;
        private SeparateReadingWindow _separateReadingWindow;
        private TrapStatusWindow _trapStatusWindow;
        private int _totalNumberOfMosquitoes;
        private int _totalFemales;
        private int _totalMales;

        // Constructor
        public MainViewModel()
        {
            // Filler for Line graph
            this.Title = "Line Graph";

            var trap1 = new Trap()
            {
                TrapNumber = 1,
                Geo1 = "7.080744",
                Geo2 = "125.506953",
                NoOfCaughtMosquitoes = 0
            };

            var trap2 = new Trap()
            {
                TrapNumber = 2,
                Geo1 = "7.081007",
                Geo2 = "125.507435",
                NoOfCaughtMosquitoes = 0
            };

            var trap3 = new Trap()
            {
                TrapNumber = 3,
                Geo1 = "7.080533",
                Geo2 = "125.507446",
                NoOfCaughtMosquitoes = 0
            };

            TrapList.Add(trap1);
            TrapList.Add(trap2);
            TrapList.Add(trap3);
        }

        // Lists for Oxyplot
        public ObservableCollection<DataPoint> Points { get; set; } = new ObservableCollection<DataPoint>();
        public ObservableCollection<Collection> CollectionList { get; set; } = new ObservableCollection<Collection>();
        public ObservableCollection<Reading> ReadingList { get; set; } = new ObservableCollection<Reading>();
        public ObservableCollection<Trap> TrapList { get; set; } = new ObservableCollection<Trap>();

        // Properties
        public string Title { get; private set; }

        public int TotalNumberOfMosquitoes
        {
            get { return _totalNumberOfMosquitoes; }
            set
            {
                _totalNumberOfMosquitoes = value;
                RaisePropertyChanged(nameof(TotalNumberOfMosquitoes));
            }
        }

        public int TotalFemales
        {
            get { return _totalFemales; }
            set
            {
                _totalFemales = value;
                RaisePropertyChanged(nameof(TotalFemales));
            }
        }

        public int TotalMales
        {
            get { return _totalMales; }
            set
            {
                _totalMales = value;
                RaisePropertyChanged(nameof(TotalMales));
            }
        }


        // Commands
        public ICommand OpenCSVFileCommand => new RelayCommand(OpenCSVFileProc);

        private void OpenCSVFileProc()
        {
            // get readings
            string filename = "";
            int counter = 1;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open CSV File";
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            dialog.ShowDialog();
            filename = dialog.FileName;

            while (filename == "")
            {
                OpenFileDialog repeatDialog = new OpenFileDialog();
                repeatDialog.Title = "Open CSV File";
                repeatDialog.Filter = "CSV Files (*.csv)|*.csv";
                repeatDialog.ShowDialog();
                filename = repeatDialog.FileName;
            }

            var myList = new List<Reading>();
            using (var streamReader = new StreamReader(filename))
            {
                while (!streamReader.EndOfStream)
                {
                    var splitLine = streamReader.ReadLine().Split(',');

                    var newReading = new Reading()
                    {
                        ReadingId = counter,
                        TrapNumber = splitLine[0],
                        Geo1 = splitLine[1],
                        Geo2 = splitLine[2],
                        OFreq = float.Parse(splitLine[3]),
                        AFreq = float.Parse(splitLine[4]),
                        CFreq = float.Parse(splitLine[5]),
                        Datetime = splitLine[6],
                        Temperature = float.Parse(splitLine[7]),
                        Humidity = float.Parse(splitLine[8]),
                        Genus = int.Parse(splitLine[9]),
                        Species = int.Parse(splitLine[10]),
                        Sex = int.Parse(splitLine[11]),
                    };

                    // add the entity  in the list
                    myList.Add(newReading);
                    counter += 1;
                }
                streamReader.Close();
            }

            ReadingList = new ObservableCollection<Reading>(myList);

            // Convert readings to collections
            string oldDate = "";
            int numberOfReadings = 0;
            int collectionCounter = CollectionList.Count + 1;
            foreach (var reading in ReadingList)
            {
                if (reading.TrapNumber == "1")
                {
                    TrapList[0].NoOfCaughtMosquitoes += 1;
                }
                else if (reading.TrapNumber == "2")
                {
                    TrapList[1].NoOfCaughtMosquitoes += 1;
                }
                else if (reading.TrapNumber == "3")
                {
                    TrapList[2].NoOfCaughtMosquitoes += 1;
                }

                if (reading.Sex == 1)
                {
                    TotalFemales += 1;
                }
                else
                {
                    TotalMales += 1;
                }

                if (oldDate == "")
                {
                    oldDate = reading.Datetime.Remove(10);
                    numberOfReadings += 1; 
                }
                else
                {
                    if (oldDate != reading.Datetime.Remove(10))
                    {
                        var newCollection = new Collection()
                        {
                            CollectionId = collectionCounter,
                            Date = oldDate,
                            NumberOfReadings = numberOfReadings
                        };
                        CollectionList.Add(newCollection);
                        collectionCounter += 1;
                        oldDate = reading.Datetime.Remove(10);
                        numberOfReadings = 1;

                        var point = new DataPoint(newCollection.CollectionId, newCollection.NumberOfReadings);
                        Points.Add(point);
                    }
                    else
                    {
                        numberOfReadings += 1;
                    }
                }
            }

            var lastCollection = new Collection()
            {
                CollectionId = collectionCounter,
                Date = oldDate,
                NumberOfReadings = numberOfReadings
            };
            CollectionList.Add(lastCollection);

            var lastPoint = new DataPoint(lastCollection.CollectionId, lastCollection.NumberOfReadings);
            Points.Add(lastPoint);

            foreach (var collection in CollectionList)
            {
                TotalNumberOfMosquitoes += collection.NumberOfReadings;
            }

            MessageBox.Show("Loaded data from a .csv file!", "Load Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ICommand GETThingspeakCommand => new RelayCommand(GETThingspeakProc);

        private void GETThingspeakProc()
        {
            // code block for DEMO





            // end block

            // get data from thingspeak code block
            string proxy = "D:\\Documents\\School Documents\\Thesis C\\SD Trap Data and Other Results\\CompiledDG.CSV";
            int counter = 1;

            var myList = new List<Reading>();
            using (var streamReader = new StreamReader(proxy))
            {
                while (!streamReader.EndOfStream)
                {
                    var splitLine = streamReader.ReadLine().Split(',');

                    var newReading = new Reading()
                    {
                        ReadingId = counter,
                        TrapNumber = splitLine[0],
                        Geo1 = splitLine[1],
                        Geo2 = splitLine[2],
                        OFreq = float.Parse(splitLine[3]),
                        AFreq = float.Parse(splitLine[4]),
                        CFreq = float.Parse(splitLine[5]),
                        Datetime = splitLine[6],
                        Temperature = float.Parse(splitLine[7]),
                        Humidity = float.Parse(splitLine[8]),
                        Genus = int.Parse(splitLine[9]),
                        Species = int.Parse(splitLine[10]),
                        Sex = int.Parse(splitLine[11]),
                    };

                    // add the entity  in the list
                    myList.Add(newReading);
                    counter += 1;
                }
                streamReader.Close();
            }

            // scry block
            string query1 = "";
            string query2 = "";
            const string url = "https://api.thingspeak.com/channels/1005703/feeds.json?";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream receiveStream = myHttpWebResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode);
            Char[] read = new Char[256];
            int count = readStream.Read(read, 0, 256);
            while (count > 0)
            {
                String str = new String(read, 0, count);
                query1 = query1 + str;
                count = readStream.Read(read, 0, 256);
            }
            myHttpWebResponse.Close();
            readStream.Close();

            const string url2 = "https://api.thingspeak.com/channels/1005707/feeds.json?";
            HttpWebRequest myHttpWebRequest2 = (HttpWebRequest)WebRequest.Create(url2);
            HttpWebResponse myHttpWebResponse2 = (HttpWebResponse)myHttpWebRequest2.GetResponse();
            Stream receiveStream2 = myHttpWebResponse2.GetResponseStream();
            Encoding encode2 = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream2 = new StreamReader(receiveStream2, encode);
            Char[] read2 = new Char[256];
            int count2 = readStream2.Read(read2, 0, 256);
            while (count2 > 0)
            {
                String str2 = new String(read2, 0, count2);
                query2 = query2 + str2;
                count2 = readStream2.Read(read2, 0, 256);
            }
            myHttpWebResponse2.Close();
            readStream2.Close();

            channelOneFeeds channelOneFeeds = JsonConvert.DeserializeObject<channelOneFeeds>(query1);
            channelTwoFeeds channelTwoFeeds = JsonConvert.DeserializeObject<channelTwoFeeds>(query2);
            List<ChannelOne> channelOneList = new List<ChannelOne>();
            List<ChannelTwo> channelTwoList = new List<ChannelTwo>();
            channelOneList = channelOneFeeds.feeds;
            channelTwoList = channelTwoFeeds.feeds;
            //channelOneList.Reverse();
            //channelTwoList.Reverse();
            // end block 

            // compare and add to CollectionList, ReadingList, TrapList[1].NoOfCaughtMosquitoes, TotalMales, TotalFemales properties

            int scryValue = 0;
            if (channelOneList.Count > 5)
            {
                scryValue = 5;
            }
            else
            {
                scryValue = channelOneList.Count;
            }
            for (int i = 0; i < scryValue; i++)
            {
                DateTime feb16 = new DateTime(2020,2,16);
                DateTime feb15 = new DateTime(2020, 2, 15);
                DateTime feb9 = new DateTime(2020, 2, 8);
                DateTime feb8 = new DateTime(2020, 2, 16);
                if (feb16.Month != channelOneList[i].created_at.Month && feb16.Day != channelOneList[i].created_at.Day &&
                    feb15.Day != channelOneList[i].created_at.Day && feb9.Day != channelOneList[i].created_at.Day &&
                    feb8.Day != channelOneList[i].created_at.Day)
                {
                    var newReading = new Reading()
                    {
                        ReadingId = counter,
                        TrapNumber = channelOneList[i].field5.ToString(),
                        Geo1 = channelOneList[i].field1,
                        Geo2 = channelOneList[i].field2,
                        OFreq = (float)channelTwoList[i].field1,
                        AFreq = (float)channelTwoList[i].field2,
                        CFreq = (float)channelTwoList[i].field3,
                        Datetime = channelOneList[i].created_at.AddHours(8).ToString("dd/MM/yyyy hh:mm tt"),
                        Temperature = (float)channelOneList[i].field3,
                        Humidity = (float)channelOneList[i].field4,
                        Genus = (int)channelTwoList[i].field4,
                        Species = (int)channelTwoList[i].field5,
                        Sex = (int)channelTwoList[i].field6,
                    };

                    // add the entity  in the list
                    myList.Add(newReading);
                    counter += 1;
                }
            }

            ReadingList = new ObservableCollection<Reading>(myList);

            // end block

            // Convert readings to collections
            string oldDate = "";
            int numberOfReadings = 0;
            int collectionCounter = CollectionList.Count + 1;
            foreach (var reading in ReadingList)
            {
                if (reading.TrapNumber == "1")
                {
                    TrapList[0].NoOfCaughtMosquitoes += 1;
                }
                else if (reading.TrapNumber == "2")
                {
                    TrapList[1].NoOfCaughtMosquitoes += 1;
                }
                else if (reading.TrapNumber == "3")
                {
                    TrapList[2].NoOfCaughtMosquitoes += 1;
                }

                if (reading.Sex == 1)
                {
                    TotalFemales += 1;
                }
                else
                {
                    TotalMales += 1;
                }

                if (oldDate == "")
                {
                    oldDate = reading.Datetime.Remove(10);
                    numberOfReadings += 1;
                }
                else
                {
                    if (oldDate != reading.Datetime.Remove(10))
                    {
                        var newCollection = new Collection()
                        {
                            CollectionId = collectionCounter,
                            Date = oldDate,
                            NumberOfReadings = numberOfReadings
                        };
                        CollectionList.Add(newCollection);
                        collectionCounter += 1;
                        oldDate = reading.Datetime.Remove(10);
                        numberOfReadings = 1;

                        var point = new DataPoint(newCollection.CollectionId, newCollection.NumberOfReadings);
                        Points.Add(point);
                    }
                    else
                    {
                        numberOfReadings += 1;
                    }
                }
            }

            var lastCollection = new Collection()
            {
                CollectionId = collectionCounter,
                Date = oldDate,
                NumberOfReadings = numberOfReadings
            };
            CollectionList.Add(lastCollection);

            var lastPoint = new DataPoint(lastCollection.CollectionId, lastCollection.NumberOfReadings);
            Points.Add(lastPoint);

            foreach (var collection in CollectionList)
            {
                TotalNumberOfMosquitoes += collection.NumberOfReadings;
            }
            // end block

            MessageBox.Show("Loaded data from Thingspeak!", "Load Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ICommand ClearCollectionsCommand => new RelayCommand(ClearCollectionsProc);

        private void ClearCollectionsProc()
        {
            CollectionList.Clear();
            Points.Clear();
            TrapList[0].NoOfCaughtMosquitoes = 0;
            TrapList[1].NoOfCaughtMosquitoes = 0;
            TrapList[2].NoOfCaughtMosquitoes = 0;
            TotalMales = 0;
            TotalFemales = 0;
        }


        public ICommand ShowAnalysisWindowCommand => new RelayCommand(ShowAnalysisWindowProc);

        private void ShowAnalysisWindowProc()
        {
            _analysisWindow = new AnalysisWindow();
            _analysisWindow.Owner = Application.Current.MainWindow;
            _analysisWindow.ShowDialog();
        }

        public ICommand ShowHelpWindowCommand => new RelayCommand(ShowHelpWindowProc);

        private void ShowHelpWindowProc()
        {
            _helpAndInformationWindow = new HelpAndInformationWindow();
            _helpAndInformationWindow.Owner = Application.Current.MainWindow;
            _helpAndInformationWindow.ShowDialog();
        }

        public ICommand ShowSeparateReadingWindowCommand => new RelayCommand(ShowSeparateReadingWindowProc);

        private void ShowSeparateReadingWindowProc()
        {
            _separateReadingWindow = new SeparateReadingWindow();
            _separateReadingWindow.Owner = Application.Current.MainWindow;
            _separateReadingWindow.ShowDialog();
        }

        public ICommand ShowTrapStatusWindowCommand => new RelayCommand(ShowTrapStatusProc);

        private void ShowTrapStatusProc()
        {
            _trapStatusWindow = new TrapStatusWindow();
            _trapStatusWindow.Owner = Application.Current.MainWindow;
            _trapStatusWindow.ShowDialog();
        }
    }
}
