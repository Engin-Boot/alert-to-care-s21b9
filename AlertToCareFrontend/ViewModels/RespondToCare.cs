using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
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
            ResetCommand = new DelegateCommandClass(ResetCommandWrapper, CommandCanExecuteWrapper);
            UpdatePatientList();
        }

        public void UpdatePatientList()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("monitoring/patientinfo", Method.GET);

            _response = _client.Execute(_request);
            var _patientStore = _deserializer.Deserialize<List<Patients>>(_response);
            foreach (var patient in _patientStore)
            {
                PatientIdList.Add(patient);
            }

        }
        public void VitalAndAlarmSelection()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("monitoring/vitals/{patientid}", Method.GET);
            _request.AddUrlSegment("patientid", PatientId);
            var _response = _client.Execute(_request);

            if (_response.StatusCode == HttpStatusCode.OK)
            {
                var vitals = _deserializer.Deserialize<VitalsLogs>(_response);
                VitalLogId = vitals.VitalsLogId;

                //set the text-box selected parameter
                this.Spo2Rate = vitals.Spo2Rate.ToString();
                this.BpRate = vitals.BpmRate.ToString();
                this.RespRate = vitals.RespRate.ToString();

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
        private void ResetVitalInfo()
        {
            VitalAndAlarmSelection();
        }

        public void SaveChanges()
        {

            double bp = default;
            double spo2 = default;
            double resp = default;
            
                bp = double.Parse(BpRate);
                spo2 = double.Parse(Spo2Rate);
                resp = double.Parse(RespRate);
           
            
                //if (BpRate == "" || Spo2Rate == "" || RespRate == "")
                //    MessageBox.Show("   This field cannot be null");

                //if (BpRate is double == false || Spo2Rate is double == false || RespRate is double == false)
                //    MessageBox.Show("Input string is not in correct format");
           


            string url = _baseUrl + "monitoring/Vitals/" + VitalLogId;
            var client = new RestClient(url);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            VitalsLogs vitals = new VitalsLogs()
            {
                PatientId = this.PatientId,
                BpmRate = bp,
                Spo2Rate = spo2,
                RespRate = resp,
                VitalsLogId = this.VitalLogId
            };
            request.AddJsonBody(vitals);
            IRestResponse _response = client.Execute(request);

            if (_response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Details Saved Successfully...");

            }
            else
            {
                MessageBox.Show("Details not saved");
            }
        }
        void SaveCommandWrapper(object parameter)
        {
            SaveChanges();
        }
        private void ResetCommandWrapper(object obj)
        {
            ResetVitalInfo();
        }
        bool CommandCanExecuteWrapper(object parameter)
        {
            return true;
        }
        #region
        public ObservableCollection<Patients> PatientIdList { get; set; } = new ObservableCollection<Patients>();
        #endregion

        #region private members
        public string _baseUrl = "http://localhost:5000/api/";

        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;
        public ICommand SaveCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        #endregion

        #region properties

        public int VitalLogId { get; set; }
        private int _patientId;
        public int PatientId
        {
            get { return _patientId; }
            set
            {
                if (value != _patientId)
                {
                    this._patientId = value;
                    VitalAndAlarmSelection();
                    UpdatePatientList();
                    OnPropertyChanged("PatientId");

                }
            }
        }

        private string _spo2Rate;
        public string Spo2Rate
        {
            get { return _spo2Rate; }
            set
            {
                if (value != _spo2Rate)
                {
                    this._spo2Rate = value;
                    OnPropertyChanged("Spo2Rate");
                }
            }
        }
        private string _bpRate;
        public string BpRate
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

        private string _respRate;
        public string RespRate
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



        #endregion
    }
}
