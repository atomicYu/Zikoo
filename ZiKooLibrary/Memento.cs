using System;
using System.Collections.Generic;


namespace ZiKooLibrary
{
    public class Memento
    {
        private List<Format> _listView1;
        private List<Coordinate> _listView2;
        private List<Format> _orientationReadingsList;
        private List<Format> _calcPointsList;
        private List<Format> _orientationsList;
        private List<Coordinate> _cooOrientationsList;

        private string _labelReadings;
        private bool _formatBtn;
        private bool _newStationBtn;
        private bool _addPointsBtn;
        private bool _calculateBtn;
        private bool _addOrientationBtn;



        public Memento(List<Format> listView1, List<Coordinate> listView2, List<Format> orientationReadingsList, List<Format> calcPointsList,
            List<Format> orientationsList, List<Coordinate> cooOrientationsList, bool formatBtn, bool newStationBtn, bool addPointsBtn, bool calculateBtn, bool addOrientationBtn, string labelReadings)
        {
            _listView1 = new List<Format>(listView1);
            _listView2 = new List<Coordinate>(listView2);
            _orientationReadingsList = new List<Format>(orientationReadingsList);
            _calcPointsList = new List<Format>(calcPointsList);
            _orientationsList = new List<Format>(orientationsList);
            _cooOrientationsList = new List<Coordinate>(cooOrientationsList);

            _labelReadings = labelReadings;
            _formatBtn = formatBtn;
            _newStationBtn = newStationBtn;
            _addPointsBtn = addPointsBtn;
            _calculateBtn = calculateBtn;
            _addOrientationBtn = addOrientationBtn;
        }

        public List<Format> GetlistView1()
        {
            return _listView1;
        }
        public List<Coordinate> GetlistView2()
        {
            return _listView2;
        }
        public string GetLabelReadings()
        {
            return _labelReadings;
        }
        public List<Format> GetOrientationReadings()
        {
            return _orientationReadingsList;
        }
        public List<Format> GetCalcPointsList()
        {
            return _calcPointsList;
        }
        public List<Format> GetOrientations()
        {
            return _orientationsList;
        }
        public List<Coordinate> GetCooOrientations()
        {
            return _cooOrientationsList;
        }
        public bool GetFormatBtn()
        {
            return _formatBtn;
        }
        public bool GetNewStationBtn()
        {
            return _newStationBtn;
        }
        public bool GetAddPointsBtn()
        {
            return _addPointsBtn;
        }
        public bool GetCalculateBtn()
        {
            return _calculateBtn;
        }
        public bool GetAddOrientationBtn()
        {
            return _addOrientationBtn;
        }

    }

    public class Originator
    {
        private List<Format> _listView1;
        private List<Coordinate> _listView2;

        private List<Format> _orientationReadingsList;
        private List<Format> _calcPointsList;
        private List<Format> _orientationsList;
        private List<Coordinate> _cooOrientationsList;

        private string _labelReadings;
        private bool _formatBtn;
        private bool _newStationBtn;
        private bool _addPointsBtn;
        private bool _calculateBtn;
        private bool _addOrientationBtn;

        public Memento SetData(List<Format> listView1, List<Coordinate> listView2, List<Format> orientationReadingsList, List<Format> calcPointsList,
            List<Format> orientationsList, List<Coordinate> cooOrientationsList,
            bool formatBtn, bool newStationBtn, bool addPointsBtn, bool calculateBtn, bool addOrientationBtn, string labelReadings)
        {
            _listView1 = listView1;
            _listView2 = listView2;
            _orientationReadingsList = orientationReadingsList;
            _calcPointsList = calcPointsList;
            _orientationsList = orientationsList;
            _cooOrientationsList = cooOrientationsList;

            _labelReadings = labelReadings;
            _formatBtn = formatBtn;
            _newStationBtn = newStationBtn;
            _addPointsBtn = addPointsBtn;
            _calculateBtn = calculateBtn;
            _addOrientationBtn = addOrientationBtn;

            Memento me = new Memento(_listView1, _listView2, _orientationReadingsList, _calcPointsList,
            _orientationsList, _cooOrientationsList, _formatBtn, _newStationBtn, _addPointsBtn, _calculateBtn, _addOrientationBtn, _labelReadings);
            return me;
        }

        public List<Format> GetlistView1()
        {
            return _listView1;
        }
        public List<Coordinate> GetlistView2()
        {
            return _listView2;
        }
        public string GetLabelReadings()
        {
            return _labelReadings;
        }
        public List<Format> GetOrientationReadings()
        {
            return _orientationReadingsList;
        }
        public List<Format> GetCalcPointsList()
        {
            return _calcPointsList;
        }
        public List<Format> GetOrientations()
        {
            return _orientationsList;
        }
        public List<Coordinate> GetCooOrientationsList()
        {
            return _cooOrientationsList;
        }
        public bool GetFormatBtn()
        {
            return _formatBtn;
        }
        public bool GetNewStationBtn()
        {
            return _newStationBtn;
        }
        public bool GetAddPointsBtn()
        {
            return _addPointsBtn;
        }
        public bool GetCalculateBtn()
        {
            return _calculateBtn;
        }
        public bool GetAddOrientationBtn()
        {
            return _addOrientationBtn;
        }

        public void Undo(Memento previousState)
        {
            _listView1 = previousState.GetlistView1();
            _listView2 = previousState.GetlistView2();
            _orientationReadingsList = previousState.GetOrientationReadings();
            _calcPointsList = previousState.GetCalcPointsList();
            _orientationsList = previousState.GetOrientations();
            _cooOrientationsList = previousState.GetCooOrientations();
            _labelReadings = previousState.GetLabelReadings();
            _formatBtn = previousState.GetFormatBtn();
            _newStationBtn = previousState.GetNewStationBtn();
            _addPointsBtn = previousState.GetAddPointsBtn();
            _calculateBtn = previousState.GetCalculateBtn();
            _addOrientationBtn = previousState.GetAddOrientationBtn();
        }
    }
}
