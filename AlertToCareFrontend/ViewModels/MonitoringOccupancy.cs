using AlertToCareFrontend.Commands;
using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace AlertToCareFrontend.ViewModels
{

    public class MonitoringOccupancy : Base
    {
        public MonitoringOccupancy()
        {
            SetIcuNoList();
            SaveCommand = new DelegateCommandClass(SaveCommandWrapper, CommandCanExecuteWrapper);
        }


        private void SetBedsInIcuList()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/BedsInIcu/{IcuNo}", Method.GET);
            _request.AddUrlSegment("IcuNo", IcuRoomNo);

            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var bedListInIcu = _deserializer.Deserialize<List<Beds>>(_response);
                foreach (var bed in bedListInIcu)
                {
                    BedIdList.Add(bed.BedId);
                }

            }
            else
            {
                MessageBox.Show("Bed List Not Given...");
            }

        }
        private void SetIcuNoList()
        {
            //call api to return all Icus: monitoring/patientinfo
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/icu", Method.GET);

            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var icuInfo = _deserializer.Deserialize<List<Icu>>(_response);
                foreach (var icu in icuInfo)
                {
                    IcuIdList.Add(icu.IcuNo);
                }
            }
            else
            {
                MessageBox.Show("Not able to fetch the Icu list...");
            }
        }

        public void SetOccupancyStatus()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("occupancy/status/{IcuNo}/{BedId}", Method.GET);
            _request.AddUrlSegment("IcuNo", IcuRoomNo);
            _request.AddUrlSegment("BedId", BedId);
            var _response = _client.Execute(_request);

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
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("occupancy/Update", Method.POST);
            _request.AddJsonBody(_bed);
            var _response = _client.Execute(_request);

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
        #region properties
        private int _icuRoomNo;
        public int IcuRoomNo
        {
            get { return _icuRoomNo; }
            set
            {
                if (value != _icuRoomNo)
                {
                    this._icuRoomNo = value;
                    SetBedsInIcuList();
                    OnPropertyChanged("IcuRoomNo");
                }
            }
        }
        private int _bedid;
        public int BedId
        {
            get { return _bedid; }
            set
            {
                if (value != _bedid)
                {
                    this._bedid = value;
                    SetOccupancyStatus();
                    OnPropertyChanged("BedId");
                }
            }
        }
        private ObservableCollection<int> _icuIdList = new ObservableCollection<int>();
        public ObservableCollection<int> IcuIdList
        {
            get { return this._icuIdList; }
            set { this._icuIdList = value; }
        }

        private ObservableCollection<int> _bedIdList = new ObservableCollection<int>();
        public ObservableCollection<int> BedIdList
        {
            get { return this._bedIdList; }
            set { this._bedIdList = value; }
        }
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
        #region private variables
        public string _baseUrl = "http://localhost:5000/api/";
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        public Beds _bed;
        #region properties
        public ICommand SaveCommand { get; set; }
        #endregion



        #endregion

    }
}
