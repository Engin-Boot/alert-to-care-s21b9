using AlertToCareFrontend.Commands;
using Microsoft.EntityFrameworkCore.Internal;
using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AlertToCareFrontend.ViewModels
{
    public class LeftPanel:Base
    {
        public string _baseUrl = "http://localhost:5000/api/";
        private static RestClient _client;
        private static RestRequest _request;
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;
        public ICommand FindPatient { get; set; }
        public ICommand FindOccupancy { get; set; }

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
        public LeftPanel()
        {
            SetPatientList();
            SetIcuNoList();
            SetBedNoList();
            FindPatient = new DelegateCommandClass(FindPatientWrapper, CommandCanExecuteWrapper);
            FindOccupancy = new DelegateCommandClass(FindOccupancyWrapper, CommandCanExecuteWrapper);
        }

        private void FindOccupancyWrapper(object obj)
        {
            GetOccupancyRecord();
        }

        private void GetOccupancyRecord()
        {
            // call monitoringOccupancy/SetOccupancyRecord
            var monitoringOccupancy = new MonitoringOccupancy();
            
        }

        private void FindPatientWrapper(object obj)
        {
            GetPatientPersonalInformation();
        }

        private void GetPatientPersonalInformation()
        {

            // call PersonalInfo/UpdatePatientInfo
            var personalInfo = new PersonalInformation();
            
        }

        private void SetPatientList()
        {
            //call api to return all patients: monitoring/patientinfo
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("monitoring/patientinfo", Method.GET);
           
            _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var patientInfo = _deserializer.Deserialize<List<Patients>>(_response);
                foreach(var patient in patientInfo)
                {
                    PatientIdList.Add(patient.PatientId);
                }
            }
            else
            {
                MessageBox.Show("Not able to fetch the patient list from the db. Try restarting the application again.");
            }
        }
        private void SetIcuNoList()
        {
            //call api to return all Icus: monitoring/patientinfo
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("config/icu", Method.GET);

            _response = _client.Execute(_request);

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
        private void SetBedNoList()
        {
            //call api to return all Beds: monitoring/patientinfo
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("config/beds", Method.GET);

            _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var bedStore = _deserializer.Deserialize<List<Beds>>(_response);
                foreach (var bed in bedStore)
                {
                    BedIdList.Add(bed.BedId);
                }
            }
            else
            {
                MessageBox.Show("Not able to fetch bed list from the db. Try restarting the application again.");
            }
        }
        bool CommandCanExecuteWrapper(object parameter)
        {
            return true;
        }

        public ObservableCollection<int> PatientIdList { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<int> IcuIdList { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<int> BedIdList { get; set; } = new ObservableCollection<int>();
        public delegate void UpdatePatientInformation(int icuNo, int bedId);
    }
}
