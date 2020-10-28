using AlertToCareFrontend.Commands;
using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;
using System.Windows;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModels
{

    public class MonitoringOccupancy : Base
    {
        public MonitoringOccupancy()
        {
            SaveCommand = new DelegateCommandClass(SaveCommandWrapper, CommandCanExecuteWrapper);
        }
        #region private variables
        public string _baseUrl = "http://localhost:5000/api/";
        private static RestClient _client;
        private static RestRequest _request;
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;
        Beds _bed;
        #region properties
        public ICommand SaveCommand { get; set; }
        #endregion


        private bool _admitStatus;
        public bool AdmitStatus
        {
            get { return _admitStatus; }
            set
            {
                if (value != _admitStatus)
                {
                    this._admitStatus = value;
                    OnPropertyChanged("AdmitStatus");
                }
            }
        }

        private bool _dischargeStatus;
        public bool DischargeStatus
        {
            get { return _dischargeStatus; }
            set
            {
                if (value != _dischargeStatus)
                {
                    this._dischargeStatus = value;
                    OnPropertyChanged("DischargeStatus");
                }
            }
        }
        #endregion


        public void SetOccupancyStatus(int icuno, int bedid)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("occupancy/status/{IcuNo}/{BedId}", Method.GET);
            _request.AddUrlSegment("IcuNo", icuno);
            _request.AddUrlSegment("BedId", bedid);
            _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _bed = _deserializer.Deserialize<Beds>(_response);
                if (_bed.OccupancyStatus)
                    AdmitStatus = true;
                else
                    DischargeStatus = true;
            }
            else
            {
                string msg = _deserializer.Deserialize<string>(_response);
                MessageBox.Show(msg);
            }
        }
        public void SaveChanges()
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("occupancy/Update", Method.POST);
            _request.AddJsonBody(_bed);
            _response = _client.Execute(_request);

            if (_response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Details not saved");
            }

        }
        void SaveCommandWrapper(object parameter)
        {
            SaveChanges();
        }

        bool CommandCanExecuteWrapper(object parameter)
        {
            return true;
        }
    }
}
