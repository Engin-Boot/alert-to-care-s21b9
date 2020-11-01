using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AlertToCareFrontend.ViewModels
{
    class ICULayoutConfiguration:Base
    {
        private int _configurenew;
        
        public int Configurenew
        {
            get { return _configurenew; }
            set
            {
                if (value != _configurenew)
                {
                    this._configurenew = value;

                    OnPropertyChanged("Configurenew");
                }
            }
        }


        
    }
}
