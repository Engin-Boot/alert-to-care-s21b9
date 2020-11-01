using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;
using AlertToCareFrontend.Commands;
using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;

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
            BedIdList.Clear();
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/BedsInIcu/{IcuNo}", Method.GET);
            _request.AddUrlSegment("IcuNo", IcuRoomNo);

            var _response = _client.Execute(_request);

            if (_response.StatusCode == HttpStatusCode.OK)
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

            if (_response.StatusCode == HttpStatusCode.OK)
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

            if (_response.StatusCode == HttpStatusCode.OK)
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

            if (_response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Details not saved");
            }
            MessageBox.Show("Details saved successfully!");

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
                    _icuRoomNo = value;
                    SetBedsInIcuList();
                    OnPropertyChanged();
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
                    _bedid = value;
                    SetOccupancyStatus();
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<int> _icuIdList = new ObservableCollection<int>();
        public ObservableCollection<int> IcuIdList
        {
            get { return _icuIdList; }
            set { _icuIdList = value; }
        }

        private ObservableCollection<int> _bedIdList = new ObservableCollection<int>();
        public ObservableCollection<int> BedIdList
        {
            get { return _bedIdList; }
            set { _bedIdList = value; }
        }
        private bool _admitStatus;
        public bool AdmitStatus
        {
            get { return _admitStatus; }
            set
            {
                if (value != _admitStatus)
                {
                    _admitStatus = value;
                    OnPropertyChanged();
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
                    _dischargeStatus = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region public variables
        public string _baseUrl = "http://localhost:5000/api/";
        public readonly JsonDeserializer _deserializer = new JsonDeserializer();
        public Beds _bed;
        public ICommand SaveCommand { get; set; }
        #endregion

    }
}
