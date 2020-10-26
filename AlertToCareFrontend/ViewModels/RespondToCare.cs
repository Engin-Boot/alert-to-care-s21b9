using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;

namespace AlertToCareFrontend.ViewModels
{
    public class RespondToCare : Base
    {
        public string _baseUrl = "http://localhost:5000/api/";
        private static RestClient _client;
        private static RestRequest _request;
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;

        public RespondToCare()
        {
            AddItemsToStatusList();
            UpdatePatientInfo(2);
            VitalAndAlarmSelection(2);
        }
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
        public void VitalAndAlarmSelection(int patientid)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("monitoring/healthstatus/{patientid}", Method.GET);
            _request.AddUrlSegment("patientid", patientid);
            _response = _client.Execute(_request);

            var logMessages = _deserializer.Deserialize<Alarm>(_response).Messages;
            var lastLogMessage = logMessages[logMessages.Count - 1];

            var messages = lastLogMessage.Split(',');

            //set the combo-box selected parameter
            var spo2Status = messages[1].Split(':')[1].Trim().Split(' ')[1];
            var bpStatus = messages[2].Split(':')[1].Trim().Split(' ')[1];
            var respRateStatus = messages[3].Split(':')[1].Trim().Split(' ')[1];

            Spo2Alarm = SetAlarmForParameter(ref spo2Status);
            BpAlarm = SetAlarmForParameter(ref bpStatus);
            RespRateAlarm = SetAlarmForParameter(ref respRateStatus);

            Spo2Status = spo2Status;
            BpStatus = bpStatus;
            RespRateStatus = respRateStatus;

            //EnableComboBoxOnAlarmValue();
        }

        private string SetAlarmForParameter(ref string status)
        {
            if (status.Equals("low"))
            {
                status = "Low";
                return "True";
            }
            else if (status.Equals("good"))
            {
                status = "Normal";
                return "False";
            }
            else
            {
                status = "High";
                return "True";
            }
        }

        public void AddItemsToStatusList()
        {
            StatusList.Add("High");
            StatusList.Add("Low");
            StatusList.Add("Normal");
        }
        public void UpdatePatientInfo(int patientid)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("monitoring/patientinfo/{patientid}", Method.GET);
            _request.AddUrlSegment("patientid", patientid);
            _response = _client.Execute(_request);
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

    }
}
