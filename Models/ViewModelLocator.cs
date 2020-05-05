using System;
using System.Collections.Generic;
using System.Text;
using ThesisApplication.ViewModel;

namespace ThesisApplication.Models
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel { get; set; } = new MainViewModel();
    }
}
