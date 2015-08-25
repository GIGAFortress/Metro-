using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using System.IO.Ports;

namespace 串口助手Metro
{
    class MainWindowViewModel : BindableBase
    {
        private bool _IsHexCheckBox;

        public bool IsHexCheckBox
        {
            get { return _IsHexCheckBox; }
            set { _IsHexCheckBox = value; OnPropertyChanged("IsHexCheckBox"); }
        }

        private string _ReceiveDataTextBox;

        public string ReceiveDataTextBox
        {
            get { return _ReceiveDataTextBox; }
            set { _ReceiveDataTextBox = value;OnPropertyChanged("ReceiveDataTextBox"); }
        }

        private string _SendDataTextBox;

        public string SendDataTextBox
        {
            get { return _SendDataTextBox; }
            set { _SendDataTextBox = value; OnPropertyChanged("SendDataTextBox"); }
        }

        private bool _ShowTimeCheckBox;

        public bool ShowTimeCheckBox
        {
            get { return _ShowTimeCheckBox; }
            set { _ShowTimeCheckBox = value; OnPropertyChanged("ShowTimeCheckBox"); }
        }

        private string[] _PortNames;

        public string[] PortNames
        {
            get { return _PortNames; }
            set { _PortNames = value; OnPropertyChanged("PortNames"); }
        }

        private string[] _BaudRates;

        public string[] BaudRates
        {
            get { return _BaudRates; }
            set { _BaudRates = value; OnPropertyChanged("BaudRates");}
        }
        private string[] _DataBits;

        public string[] DataBits
        {
            get { return _DataBits; }
            set { _DataBits = value;OnPropertyChanged("DataBits"); }
        }

        private Parity  _Parities;

        public Parity Parities
        {
            get { return _Parities; }
            set { _Parities = value; }
        }

        string[] BaudRatesList = { "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200" };
        string[] DataBitsList = { "6", "7", "8" };
        

        public DelegateCommand ClearButtonCommand { get; set; }
        private void _ClearButtonCommand()
        {
            //MessageBox.Show("点击了清空按钮");
            ReceiveDataTextBox = "Hello";
        }
        public DelegateCommand SendButtonCommand { get; set; }
        private void _SendButtonCommand()
        {
            MessageBox.Show("点击了发送按钮");
        }
        public MainWindowViewModel()
        {            
            this.PortNames = SerialPort.GetPortNames();
            //不可用 this.BaudRates ={ "1200","2400","4800","9600","19200","38400","115200" }; 
            this.BaudRates = BaudRatesList;
            this.DataBits = DataBitsList;
            ClearButtonCommand = new DelegateCommand(new Action(_ClearButtonCommand));
            SendButtonCommand = new DelegateCommand(new Action(_SendButtonCommand));
        }
    }
}
