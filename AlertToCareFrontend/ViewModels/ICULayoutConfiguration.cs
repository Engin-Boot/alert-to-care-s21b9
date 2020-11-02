using RestSharp;
using RestSharp.Serialization.Json;
using SharedProjects.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace AlertToCareFrontend.ViewModels
{
    public class IcuLayoutConfiguration : Base
    {
        public IcuLayoutConfiguration()
        {
            // this is a part of Adding new Icu
            SetFloorNoList();
            SetLayoutIdList();
            //SetIcuIdList();

        }

        #region Properties

        // add new icu and existing icu changes has the same list
        public ObservableCollection<int> FloorIdList { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<Layouts> LayoutTypeList { get; set; } = new ObservableCollection<Layouts>();

        // existing icu
        public ObservableCollection<Icu> IcuIdList { get; set; } = new ObservableCollection<Icu>();




        //existing
        private int _selectedIcuNoExisting;
        public int SelectedIcuNoExisting
        {
            get { return _selectedIcuNoExisting; }
            set
            {
                if (value != _selectedIcuNoExisting)
                {
                    this._selectedIcuNoExisting = value;
                    OnPropertyChanged("SelectedIcuNoExisting");
                    SetLayoutType();
                }
            }
        }
        // existing
        private int _selectedFloorForExistingIcu;
        public int SelectedFloorForExistingIcu
        {
            get { return _selectedFloorForExistingIcu; }
            set
            {
                if (value != _selectedFloorForExistingIcu)
                {
                    this._selectedFloorForExistingIcu = value;
                    OnPropertyChanged("SelectedFloorForExistingIcu");
                    SetIcuIdList();
                }
            }
        }
        //existing
        private string _layoutTypeExisting;
        public string LayoutTypeExisting
        {
            get { return _layoutTypeExisting; }
            set
            {
                if (value != _layoutTypeExisting)
                {
                    this._layoutTypeExisting = value;
                    OnPropertyChanged("LayoutTypeExisting");
                }
            }
        }
        //existing
        private string _selectedLayoutIdForExisting;
        public string SelectedLayoutIdForExisting
        {
            get { return _selectedLayoutIdForExisting; }
            set
            {
                if (value != _selectedLayoutIdForExisting)
                {
                    this._selectedLayoutIdForExisting = value;
                    OnPropertyChanged("SelectedLayoutIdForExisting");
                    SetBedNoBasedOnTheLayoutsExisting();
                }
            }
        }
        private int _bedsInLayoutExisting;
        public int BedsInLayoutExisting
        {
            get { return _bedsInLayoutExisting; }
            set
            {
                if (value != _bedsInLayoutExisting)
                {
                    this._bedsInLayoutExisting = value;
                    OnPropertyChanged("BedsInLayoutExisting");

                }
            }
        }

        //add 
        private int _selectedFloorNew;
        public int SelectedFloorNew
        {
            get { return _selectedFloorNew; }
            set
            {
                if (value != _selectedFloorNew)
                {
                    this._selectedFloorNew = value;
                    OnPropertyChanged("SelectedFloorNew");
                    SetNewIcuNo();
                }
            }
        }

        //add
        private string _selectedLayoutIdNew;
        public string SelectedLayoutIdNew
        {
            get { return _selectedLayoutIdNew; }
            set
            {
                if (value != _selectedLayoutIdNew)
                {
                    this._selectedLayoutIdNew = value;
                    OnPropertyChanged("SelectedLayoutIdNew");
                    SetBedNoBasedOnTheLayoutsNew();
                }
            }
        }
        //add
        private int _newIcuNo;
        public int NewIcuNo
        {
            get { return _newIcuNo; }
            set
            {
                if (value != _newIcuNo)
                {
                    this._newIcuNo = value;
                    OnPropertyChanged("NewIcuNo");
                }
            }
        }

        //add
        private int _bedsInLayoutNew;
        public int BedsInLayoutNew
        {
            get { return _bedsInLayoutNew; }
            set
            {
                if (value != _bedsInLayoutNew)
                {
                    this._bedsInLayoutNew = value;
                    OnPropertyChanged("BedsInLayoutNew");
                }
            }
        }


        public int LayoutId { get; set; }
        #endregion
        private void SetLayoutType()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/floor/{icuNo}", Method.GET);
            _request.AddUrlSegment("icuNo", SelectedIcuNoExisting);
            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var icuInfo = _deserializer.Deserialize<List<Icu>>(_response);
                foreach (var icu in icuInfo)
                {
                    if (icu.IcuNo == SelectedIcuNoExisting)
                    {
                        LayoutId = icu.LayoutId;
                        SetLayoutName();
                    }
                }

            }
            else
            {
                MessageBox.Show("Not able to fetch the Icu list...");
            }
        }


        private void SetIcuIdList()
        {
            IcuIdList.Clear();
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/floor/{floorno}", Method.GET);
            _request.AddUrlSegment("floorno", SelectedFloorForExistingIcu);
            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var icuInfo = _deserializer.Deserialize<List<Icu>>(_response);
                foreach (var icu in icuInfo)
                {
                    IcuIdList.Add(icu);
                }
            }
            else
            {
                MessageBox.Show("Not able to fetch the Icu list...");
            }
        }

        private void SetLayoutName()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/Layouts", Method.GET);
            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var layoutInfo = _deserializer.Deserialize<List<Layouts>>(_response);
                foreach (var layout in layoutInfo)
                {
                    if (layout.LayoutId == LayoutId)
                    {
                        LayoutTypeExisting = layout.LayoutType;
                    }
                }

            }
            else
            {
                MessageBox.Show("Not able to fetch the Icu list...");
            }
        }
        private void SetLayoutIdList()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/layouts", Method.GET);

            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var layoutInfo = _deserializer.Deserialize<List<Layouts>>(_response);
                foreach (var layoutName in layoutInfo)
                {
                    LayoutTypeList.Add(layoutName);
                }
            }
            else
            {
                MessageBox.Show("Not able to fetch the Layout list...");
            }
        }

        private void SetFloorNoList()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/icu", Method.GET);

            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var icuInfo = _deserializer.Deserialize<List<Icu>>(_response);
                var uniqueFloorNos = ExtractUniqueFloorNos(icuInfo);
                foreach (var icu in uniqueFloorNos)
                {
                    FloorIdList.Add(icu);
                }
            }
            else
            {
                MessageBox.Show("Not able to fetch the Layout list...");
            }
        }
        private HashSet<int> ExtractUniqueFloorNos(List<Icu> icuStore)
        {
            HashSet<int> uniqueFloorList = new HashSet<int>();
            foreach (var icu in icuStore)
            {
                uniqueFloorList.Add(icu.FloorNo);
            }
            return uniqueFloorList;
        }
        private void SetBedNoBasedOnTheLayoutsNew()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/layouts", Method.GET);

            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var layoutInfo = _deserializer.Deserialize<List<Layouts>>(_response);
                foreach (var layout in layoutInfo)
                {

                    if (layout.LayoutType == SelectedLayoutIdNew)
                    {
                        BedsInLayoutNew = layout.Capacity;
                    }
                }
            }
            else
            {
                MessageBox.Show("Not able to fetch the Layout list...");
            }
        }
        private void SetBedNoBasedOnTheLayoutsExisting()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/layouts", Method.GET);

            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var layoutInfo = _deserializer.Deserialize<List<Layouts>>(_response);
                foreach (var layout in layoutInfo)
                {

                    if (layout.LayoutType == LayoutTypeExisting)
                    {
                        BedsInLayoutExisting = layout.Capacity;
                    }
                }
            }
            else
            {
                MessageBox.Show("Not able to fetch the Layout list...");
            }
        }
        private void SetNewIcuNo()
        {
            var _client = new RestClient(_baseUrl);
            var _request = new RestRequest("config/floor/{floorno}", Method.GET);
            _request.AddUrlSegment("floorno", SelectedFloorNew);
            var _response = _client.Execute(_request);

            if (_response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var icuInfo = _deserializer.Deserialize<List<Icu>>(_response);
                var lastIcuNo = icuInfo[icuInfo.Count - 1].IcuNo;
                NewIcuNo = lastIcuNo + 1;
            }
            else
            {
                MessageBox.Show("Not able to fetch the Icu list...");
            }

        }
        #region Functions
        #endregion

        #region public variables
        public string _baseUrl = "http://localhost:5000/api/";
        public readonly JsonDeserializer _deserializer = new JsonDeserializer();
        #endregion


    }
}
