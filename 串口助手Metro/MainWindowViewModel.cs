using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using System.IO.Ports;
using System.Windows.Documents;
using System.Threading;
using System.Windows.Threading;

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

        private bool _PortToggleSwitchIsChecked;
        public bool PortToggleSwitchIsChecked
        {
            get { return _PortToggleSwitchIsChecked; }
            set { _PortToggleSwitchIsChecked = value; OnPropertyChanged("PortToggleSwitch"); }
        }

        private int _SendDataTimmerTextBox;
        public int SendDataTimmerTextBox
        {
            get { return _SendDataTimmerTextBox; }
            set { _SendDataTimmerTextBox = value; }
        }

        private bool _AfterReceivedSendCheckBox;
        public bool AfterReceiveSendCheckBox
        {
            get { return _AfterReceivedSendCheckBox; }
            set { _AfterReceivedSendCheckBox = value; }
        }

        private string _MessageLabel;
        public string MessageLabel
        {
            get { return _MessageLabel; }
            set { _MessageLabel = value; OnPropertyChanged("MessageLabel"); }
        }

#region StatuBar Counter
        private int _ReceiveDataCounter;
        public int ReceiveDataCounter
        {
            get { return _ReceiveDataCounter; }
            set { _ReceiveDataCounter = value; OnPropertyChanged("ReceiveDataCounter"); }
        }

        private int _ReceiveDataCounterByte;
        public int ReceiveDataCounterByte
        {
            get { return _ReceiveDataCounterByte; }
            set { _ReceiveDataCounterByte = value; OnPropertyChanged("ReceiveDataCounterByte"); }
        }

        private int _SendDataCounter;
        public int SendDataCounter
        {
            get { return _SendDataCounter; }
            set { _SendDataCounter = value; OnPropertyChanged("SendDataCounter"); }
        }

        private int _SendDataCounterByte;

        public int SendDataCounterByte
        {
            get { return _SendDataCounterByte; }
            set { _SendDataCounterByte = value; OnPropertyChanged("SendDataCounterByte"); }
        }
        #endregion
#region Port属性
        public string[] PortNames { get { return SerialPort.GetPortNames(); } }
        
        private string[] _BaudRates = { "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200" };
        public string[] BaudRates{ get { return _BaudRates; } }

        private string[] _DataBits = { "6", "7", "8" };
        public string[] DataBits { get { return _DataBits; } }
              
        public string[] Parities { get { return Enum.GetNames(typeof(Parity)); } }

        public string[] StopBits { get { return Enum.GetNames(typeof(StopBits)); } }
             
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
            //ReceiveDataTextBox = string.Format("{0} {1} {2} {3} {4}",Port.PortName,Port.BaudRate,Port.DataBits,Port.Parity,Port.StopBits);
            ReceiveDataTextBox = "";
        }
        public DelegateCommand SendButtonCommand { get; set; }
        private void _SendButtonCommand()
        {
            if (!PortToggleSwitchIsChecked) { MessageLabel = "请开启串口"; }
            if (SendDataTextBox == "") { }
            else { MessageLabel = SendDataUpdate(SendDataTextBox); }
            
        }

        private string BytesToHexString(byte[] bytes)
        {
            string str = bytes[0].ToString("X2");
            for (int i = 1; i < bytes.Length; i++)
            {
                str = string.Format("{0} {1:X2}", str, bytes[i]);
            }
            return str;
        }
        public string SendDataUpdate(string str)
        {
            try
            {
                if (IsHexCheckBox)
                {
                    str = str.Replace(" ", "");
                    if ((str.Length % 2) != 0)
                    {
                        str = str.Insert(str.Length - 1, "0");
                    }
                    byte[] txData = new byte[str.Length / 2];

                    for (int i = 0; i < txData.Length; i++)
                    {
                        txData[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
                    }
                    return SendDataUpdate(txData);
                }
                else
                {
                    Port.WriteLine(str);

                    SendDataCounter++;
                    SendDataCounterByte += str.Length;

                    //if (IsShowSend) str = "[发送] " + str;

                    //if (IsShowDate) str = String.Format("[{0:HH:mm:ss.fff}]{1}", DateTime.Now, str);

                    return str;

                }
            }
            catch (Exception)
            {
                MessageLabel = "请输入标准Hex";
                return str;
            }
        }
        public string SendDataUpdate(byte[] data)
        {   
            Port.Write(data, 0, data.Length);

            //IsSend = true;

            SendDataCounter++;
            SendDataCounterByte += data.Length;

            string str = BytesToHexString(data);

            //if (IsShowSend) str = "[发送] " + str;
            
            //if (IsShowDate) str = String.Format("[{0:HH:mm:ss.fff}]{1}", DateTime.Now, str);

            return str;
        }

        public string ReceiveDataUpdate(byte[] data)
        {
            ReceiveDataCounter++;
            ReceiveDataCounterByte += data.Length;

            string str = null;

            if (IsHexCheckBox)
            {
                str = BytesToHexString(data);
            }
            else
            {
                str = Encoding.ASCII.GetString(data);
            }

            //if (IsShowSend) str = "[接收] " + str;

            //if (IsShowDate) str = String.Format("[{0:HH:mm:ss.fff}]{1}", DateTime.Now, str);

            return str;
        }
        public string ReceiveDataUpdate(string str)
        {
            ReceiveDataCounter++;
            ReceiveDataCounterByte += str.Length;

           // if (IsShowSend) str = "[接收] " + str;

           // if (IsShowDate) str = String.Format("[{0:HH:mm:ss.fff}]{1}", DateTime.Now, str);

            return str;
        }

        public DelegateCommand ToggleSwitchCheckedEventCommand { get; set; }
        private void _ToggleSwitchCheckedEventCommand()
        {
            //ReceiveDataTextBox = "Click & ToggleSwitchDelegateCommand";
            try { Port.Open(); }
            catch { MessageBox.Show("Error"); }
        }

        public DelegateCommand ToggleSwitchUncheckedEventCommand { get; set; }
        private void _ToggleSwitchUncheckedEventCommand()
        {
            //ReceiveDataTextBox = "Unchecked Event Active";
            Port.Close();
        }

        public DelegateCommand ClearCounterButtonCommand { get; set; }
        private void _ClearCounterButtonCommand()
        {
            //MessageBox.Show("Clear the counter!");
            ReceiveDataCounter = 0;
            ReceiveDataCounterByte = 0;
            SendDataCounter = 0;
            SendDataCounterByte = 0;

        }
        public SerialPort Port = new SerialPort();
        public MainWindowViewModel()
        {            
            ClearButtonCommand = new DelegateCommand(_ClearButtonCommand);
            SendButtonCommand = new DelegateCommand(new Action(_SendButtonCommand));
            ToggleSwitchCheckedEventCommand = new DelegateCommand(_ToggleSwitchCheckedEventCommand);
            ToggleSwitchUncheckedEventCommand = new DelegateCommand(_ToggleSwitchUncheckedEventCommand);
            ClearCounterButtonCommand = new DelegateCommand(_ClearCounterButtonCommand);
            Port.DataReceived += Port_DataReceived;            
        }
        Thread th1 = new Thread(DispatcherTesting);
        public delegate void UpdateBytesDelegate(byte[] data);
        public delegate void testing();
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // //throw new NotImplementedException();
          //int length = Port.BytesToRead;
          //byte[] data = new byte[length];
          //for (int i = 0; i < length; i++)
          //{
          //    data[i] = (byte)Port.ReadByte();
          //}
          ReceiveDataCounter++;
            if (flag)
            {
                flag = false; th1.Start();
            }
            //MessageBox.Show("接收数据");
            //Dispatcher.CurrentDispatcher.BeginInvoke(new UpdateBytesDelegate(UpdateBytesbox), data);
            //Dispatcher.CurrentDispatcher.BeginInvoke(new UpdateBytesDelegate(DispatcherTesting));
            //System.Threading.Thread.
        }
        static bool flag = true;
        private static void DispatcherTesting()
        {
            //throw new NotImplementedException();
            MessageBox.Show("来自Dispatcher.CurrentDispatcher.BeginInvoke(new UpdateBytesDelegate(DispatcherTesting));");
        }

        private void UpdateBytesbox(byte[] data)
        {
            
            Run r = new Run(ReceiveDataUpdate(data) + Environment.NewLine);           
            
        }
    }
}
