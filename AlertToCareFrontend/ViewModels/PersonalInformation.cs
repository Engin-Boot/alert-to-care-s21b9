using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;

namespace AlertToCareFrontend.ViewModels
{
    public class PersonalInformation : Base
    {
        public PersonalInformation() { }
        #region private members
        public string _baseUrl = "http://localhost:5000/api/";
        private static RestClient _client;
        private static RestRequest _request;
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;

        #endregion

        #region properties
        private string _patientName;
        public string PatientName
        {
            get { return _patientName; }
            set
            {
                if (value != _patientName)
                {
                    this._patientName = value;
                    OnPropertyChanged("PatientName");
                }
            }
        }

        private int _patientId;
        public int PatientId
        {
            get { return _patientId; }
            set
            {
                if (value != _patientId)
                {
                    this._patientId = value;
                    OnPropertyChanged("PatientId");
                }
            }
        }

        private int _patientAge;
        public int PatientAge
        {
            get { return _patientAge; }
            set
            {
                if (value != _patientAge)
                {
                    this._patientAge = value;
                    OnPropertyChanged("PatientAge");
                }
            }
        }

        private string _contactNo;
        public string ContactNo
        {
            get { return _contactNo; }
            set
            {
                if (value != _contactNo)
                {
                    this._contactNo = value;
                    OnPropertyChanged("ContactNo");
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
                    OnPropertyChanged("BedId");
                }
            }
        }
        private int _icuno;
        public int IcuNo
        {
            get { return _icuno; }
            set
            {
                if (value != _icuno)
                {
                    this._icuno = value;
                    OnPropertyChanged("IcuNo");
                }
            }
        }
        private string _monitoringStatus;
        public string MonitoringStatus
        {
            get { return _monitoringStatus; }
            set
            {
                if (value != _monitoringStatus)
                {
                    this._monitoringStatus = value;
                    OnPropertyChanged("MonitoringStatus");
                }
            }
        }
        #endregion

        public void UpdatePatientInfo(int patientid)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("monitoring/patientinfo/{patientid}", Method.GET);
            _request.AddUrlSegment("patientid", patientid);
            _response = _client.Execute(_request);
            var _patient = _deserializer.Deserialize<Patients>(_response);

            this.PatientId = _patient.PatientId;
            this.PatientName = _patient.PatientName;
            this.BedId = _patient.BedId;
            this.ContactNo = _patient.ContactNo;
            this.PatientAge = _patient.Age;
            this.MonitoringStatus = _patient.MonitoringStatus == 0 ? "On" : "Off";

            _request = new RestRequest("config/beds/{bedid}", Method.GET);
            _request.AddUrlSegment("bedid", BedId);
            _response = _client.Execute(_request);
            var _beds = _deserializer.Deserialize<Beds>(_response);

            IcuNo = _beds.IcuNo;


        }
    }
}
