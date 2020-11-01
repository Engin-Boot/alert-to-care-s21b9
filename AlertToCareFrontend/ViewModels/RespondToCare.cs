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
           var client = new RestClient(BaseUrl);
            var request = new RestRequest("monitoring/patientinfo", Method.GET);

            _response = client.Execute(request);
            var patientStore = _deserializer.Deserialize<List<Patients>>(_response);
            foreach (var patient in patientStore)
            {
                PatientIdList.Add(patient);
            }

        }
        public void VitalAndAlarmSelection()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("monitoring/vitals/{patientid}", Method.GET);
            request.AddUrlSegment("patientid", PatientId);
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var vitals = _deserializer.Deserialize<VitalsLogs>(response);

                //set the text-box selected parameter
                Spo2Rate = vitals.Spo2Rate.ToString(CultureInfo.InvariantCulture);
                BpRate = vitals.BpmRate.ToString(CultureInfo.InvariantCulture);
                RespRate = vitals.RespRate.ToString(CultureInfo.InvariantCulture);

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
                var msg = _deserializer.Deserialize<string>(response);
                MessageBox.Show(msg);
            }

        }
        private string SetAlarmForParameter(string status)
        {
            if (status.Equals("low"))
                return "True";
            if (status.Equals("good"))
                return "False";
            return "True";
        }
        private void ResetVitalInfo()
        {
            VitalAndAlarmSelection();
        }


        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void SaveChanges()
        {
            // save change in data
            double bp = default;
            double spo2 = default;
            double resp = default;
            // save change in data
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("monitoring/vitals", Method.POST);
            try
            {

                bp = double.Parse(BpRate);
                spo2 = double.Parse(Spo2Rate);
                resp = double.Parse(RespRate);

            }

            catch (Exception)
            {
                if (BpRate == "" || Spo2Rate == "" || RespRate == "")
                    MessageBox.Show("   This field cannot be null");
#pragma warning disable 184
                if (BpRate is double == false || Spo2Rate is double == false || RespRate is double == false)
#pragma warning restore 184
                    MessageBox.Show("Input string is not in correct format");


            }
            var vitals = new VitalsLogs { PatientId = PatientId, BpmRate = bp, Spo2Rate = spo2, RespRate = resp, VitalsLogId = 200 };
            request.AddJsonBody(vitals);
            _response = client.Execute(request);
            if (_response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Details not saved");
            }
            else
            {
                MessageBox.Show("Details Saved Successfully...");
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
        public string BaseUrl = "http://localhost:5000/api/";

        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;
        public ICommand SaveCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        #endregion

        #region properties


        private int _patientId;
        public int PatientId
        {
            get { return _patientId; }
            set
            {
                if (value != _patientId)
                {
                    _patientId = value;
                    VitalAndAlarmSelection();
                    UpdatePatientList();
                    OnPropertyChanged();

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
                    _spo2Rate = value;
                    OnPropertyChanged();
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
                    _bpRate = value;
                    OnPropertyChanged();
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
                    _respRate = value;
                    OnPropertyChanged();
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
                    _spo2Alarm = value;
                    OnPropertyChanged();
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
                    _bpAlarm = value;
                    OnPropertyChanged();
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
                    _respRateAlarm = value;
                    OnPropertyChanged();
                }
            }
        }



        #endregion
    }
}
