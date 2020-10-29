using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AlertToCareFrontend.ViewModels
{
    public class PersonalInformation : Base
    {
        public PersonalInformation()
        {
            this.UpdatePatientList();
        }

        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != _selectedItem)
                {
                    this._selectedItem = value;

                    OnPropertyChanged("SelectedItem");
                }
            }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        public void UpdatePatientList()
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("monitoring/patientinfo", Method.GET);

            _response = _client.Execute(_request);
            var _patientStore = _deserializer.Deserialize<List<Patients>>(_response);


            foreach (var patient in _patientStore)
            {
                PatientIdList.Add(patient);
            }

        }
        #region private members
        public string _baseUrl = "http://localhost:5000/api/";
        private static RestClient _client;
        private static RestRequest _request;
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private static IRestResponse _response;

        #endregion
        public ObservableCollection<Patients> PatientIdList { get; set; } = new ObservableCollection<Patients>();
    }
}