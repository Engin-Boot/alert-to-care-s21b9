namespace AlertToCareFrontend.ViewModels
{
    internal class IcuLayoutConfiguration:Base
    {
        private int _configurenew;
        
        public int Configurenew
        {
            get { return _configurenew; }
            set
            {
                if (value != _configurenew)
                {
                    _configurenew = value;

                    OnPropertyChanged();
                }
            }
        }


        
    }
}
