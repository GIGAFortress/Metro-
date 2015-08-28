﻿using System;
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
        #region 串口设置相关的一系列属性        
        public string[] PortNames { get { return SerialPort.GetPortNames(); } }
        
        private string[] _BaudRates = { "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200" };
        public string[] BaudRates{ get { return _BaudRates; } }

        private string[] _DataBits = { "6", "7", "8" };
        public string[] DataBits { get { return _DataBits; } }
              
        public string[] Parities { get { return Enum.GetNames(typeof(Parity)); } }

        public string[] StopBits { get { return Enum.GetNames(typeof(StopBits)); } }
        #endregion
        private bool _PortToggleSwitchIsChecked;

        public bool PotrToggleSwitchIsChecked
        {
            get { return _PortToggleSwitchIsChecked; }
            set { _PortToggleSwitchIsChecked = value;OnPropertyChanged("PortToggleSwitch"); }
        }
        #region 选择绑定属性 SelectedItem绑定用            
        public string SelectedPortName
        {
            get { return Port.PortName; }
            set { Port.PortName = value; }
        }                  
        public int SelectedBaudRate
        {
            get { return Port.BaudRate; }
            set { Port.BaudRate = value; }
        }       
        public int SelecetedDataBit
        {
            get { return Port.DataBits; }
            set { Port.DataBits = value; }
        }                 
        public Parity SelectedParity
        {
            get { return Port.Parity; }
            set { Port.Parity = value; }
        }       
        public StopBits SelectedStopBit
        {
            get { return Port.StopBits; }
            set { Port.StopBits = value; }
        }
        #endregion
        
        
        

        public DelegateCommand ClearButtonCommand { get; set; }
        private void _ClearButtonCommand()
        {            
            ReceiveDataTextBox = string.Format("{0} {1} {2} {3} {4}",Port.PortName,Port.BaudRate,Port.DataBits,Port.Parity,Port.StopBits);
        }
        public DelegateCommand SendButtonCommand { get; set; }
        private void _SendButtonCommand()
        {
                       
        }
        
        public SerialPort Port = new SerialPort();

        public MainWindowViewModel()
        {            
            ClearButtonCommand = new DelegateCommand(_ClearButtonCommand);
            SendButtonCommand = new DelegateCommand(new Action(_SendButtonCommand));
        }
        
    }
}
