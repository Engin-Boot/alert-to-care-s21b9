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
        public void UpdatePatientList()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("monitoring/patientinfo", Method.GET);

            var _response = _client.Execute(_request);
            var _patientStore = _deserializer.Deserialize<List<Patients>>(_response);

            foreach (var patient in _patientStore)
            {
                PatientIdList.Add(patient);
            }

        }

        #region Properties
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
        #endregion
        #region private members
        private string _baseUrl = "http://localhost:5000/api/";
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        #endregion
        #region Public Members
        public ObservableCollection<Patients> PatientIdList { get; set; } = new ObservableCollection<Patients>();
        #endregion

    }
}