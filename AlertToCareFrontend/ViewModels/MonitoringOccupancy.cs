using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;

namespace AlertToCareFrontend.ViewModels
{

    public class MonitoringOccupancy : Base
    {
        public MonitoringOccupancy()
        {
            SetOccupancyStatus(11, 2);
        }
        public string _baseUrl = "http://localhost:5000/api/";
        private static RestClient _client;
        private static RestRequest _request;
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;

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
        public void SetOccupancyStatus(int icuno, int bedid)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("occupancy/status/{IcuNo}/{BedId}", Method.GET);
            _request.AddUrlSegment("IcuNo", icuno);
            _request.AddUrlSegment("BedId", bedid);

            _response = _client.Execute(_request);
            var _bed = _deserializer.Deserialize<Beds>(_response);

            if (_bed.OccupancyStatus)
                AdmitStatus = true;
            else
                DischargeStatus = true;

        }
    }
}
