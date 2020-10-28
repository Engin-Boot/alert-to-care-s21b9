using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AlertToCareFrontend.Commands;
using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;
using SharedProjects.Utilities;

namespace AlertToCareFrontend.ViewModels
{
    public class RespondToCare : Base
    {
        public RespondToCare()
        {
            SaveCommand = new DelegateCommandClass(SaveCommandWrapper, CommandCanExecuteWrapper);
        }
        #region private members
        public string _baseUrl = "http://localhost:5000/api/";
        private static RestClient _client;
        private static RestRequest _request;
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;
        public ICommand SaveCommand { get; set; }
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

        private ObservableCollection<string> statusList = new ObservableCollection<string>();
        public ObservableCollection<string> StatusList
        {
            get { return statusList; }
            set { this.statusList = value; }
        }

        private double _spo2Rate;
        public double Spo2Rate
        {
            get { return _spo2Rate; }
            set
            {
                if (value != _spo2Rate)
                {
                    this._spo2Rate = value;
                    OnPropertyChanged("Spo2Status");
                }
            }
        }

        private double _bpRate;
        public double BpRate
        {
            get { return _bpRate; }
            set
            {
                if (value != _bpRate)
                {
                    this._bpRate = value;
                    OnPropertyChanged("BpRate");
                }
            }
        }

        private double _respRate;
        public double RespRate
        {
            get { return _respRate; }
            set
            {
                if (value != _respRate)
                {
                    this._respRate = value;
                    OnPropertyChanged("RespRate");
                }
            }
        }

        private string _spo2Alarm;
        public string Spo2Alarm
        {
            get { return _spo2Alarm; }
            set
            {
                if (value != _spo2Alarm)
                {
                    this._spo2Alarm = value;
                    OnPropertyChanged("Spo2Alarm");
                }
            }
        }

        private string _bpAlarm;
        public string BpAlarm
        {
            get { return _bpAlarm; }
            set
            {
                if (value != _bpAlarm)
                {
                    this._bpAlarm = value;
                    OnPropertyChanged("BpAlarm");
                }
            }
        }

        private string _respRateAlarm;
        public string RespRateAlarm
        {
            get { return _respRateAlarm; }
            set
            {
                if (value != _respRateAlarm)
                {
                    this._respRateAlarm = value;
                    OnPropertyChanged("RespRateAlarm");
                }
            }
        }

        private string _spo2Status;
        public string Spo2Status
        {
            get { return _spo2Status; }
            set
            {
                if (value != _spo2Status)
                {
                    this._spo2Status = value;
                    OnPropertyChanged("Spo2Status");
                }
            }
        }

        private string _bpStatus;
        public string BpStatus
        {
            get { return _bpStatus; }
            set
            {
                if (value != _bpStatus)
                {
                    this._bpStatus = value;
                    OnPropertyChanged("BpStatus");
                }
            }
        }

        private string _respRateStatus;
        public string RespRateStatus
        {
            get { return _respRateStatus; }
            set
            {
                if (value != _respRateStatus)
                {
                    this._respRateStatus = value;
                    OnPropertyChanged("RespRateStatus");
                }
            }
        }

        #endregion

        public void VitalAndAlarmSelection(int patientid)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("monitoring/vitals/{patientid}", Method.GET);
            _request.AddUrlSegment("patientid", patientid);
            _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var vitals = _deserializer.Deserialize<VitalsLogs>(_response);

                //set the text-box selected parameter
                this.Spo2Rate = vitals.Spo2Rate;
                this.BpRate = vitals.BpmRate;
                this.RespRate = vitals.RespRate;

                var vitalsMonitoring = new VitalsMonitoring();
                string message = vitalsMonitoring.CheckVitals(vitals);

                var spo2Status = message.Split(',')[2].Split(' ')[3];
                var bpStatus = message.Split(',')[3].Split(' ')[3];
                var respRateStatus = message.Split(',')[4].Split(' ')[3];

                Spo2Alarm = SetAlarmForParameter(spo2Status);
                BpAlarm = SetAlarmForParameter(bpStatus);
                RespRateAlarm = SetAlarmForParameter(respRateStatus);
            }
            else
            {
                var msg = _deserializer.Deserialize<string>(_response);
                MessageBox.Show(msg);
            }

        }

        private string SetAlarmForParameter(string status)
        {
            if (status.Equals("low"))
                return "True";
            else if (status.Equals("good"))
                return "False";
            else
                return "True";
        }
        public void UpdatePatientInfo(int patientid)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("monitoring/patientinfo/{patientid}", Method.GET);
            _request.AddUrlSegment("patientid", patientid);
            _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var _patient = _deserializer.Deserialize<Patients>(_response);

                PatientId = _patient.PatientId;
                PatientName = _patient.PatientName;
                BedId = _patient.BedId;
                ContactNo = _patient.ContactNo;
                PatientAge = _patient.Age;
                MonitoringStatus = _patient.MonitoringStatus == 0 ? "On" : "Off";

                _request = new RestRequest("config/beds/{bedid}", Method.GET);
                _request.AddUrlSegment("bedid", BedId);
                _response = _client.Execute(_request);
                var _beds = _deserializer.Deserialize<Beds>(_response);

                IcuNo = _beds.IcuNo;
            }
            else
            {
                var msg = _deserializer.Deserialize<string>(_response);
                MessageBox.Show(msg);
            }


        }
        public void SaveChanges()
        {
            // save change in data
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("monitoring/vitals", Method.POST);
            var vitals = new VitalsLogs() { PatientId = this.PatientId, BpmRate = this.BpRate, Spo2Rate = this.Spo2Rate, RespRate = this.RespRate, VitalsLogId = 200 };
            _request.AddJsonBody(vitals);
            _response = _client.Execute(_request);
            if (_response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Details not saved");
            }
        }
        void SaveCommandWrapper(object parameter)
        {
            //call function that needs to get executed
            SaveChanges();
        }

        bool CommandCanExecuteWrapper(object parameter)
        {
            return true;
        }

    }
}
