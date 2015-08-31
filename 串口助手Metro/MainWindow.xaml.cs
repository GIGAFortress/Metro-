using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Interactivity;

namespace 串口助手Metro
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
            PortNameComboBox.SelectedIndex = 0;
            BaudRateComboBox.SelectedIndex = 3;
            DataBitComboBox.SelectedIndex = 2;
            ParityComboBox.SelectedIndex = 0;
            StopBitCombobBox.SelectedIndex = 1;
            
        }



        // private void PortToggleSwitch_Click(object sender, RoutedEventArgs e)
        // {
        //     MessageBox.Show("点击了ToggleSwitch");
        //
        // }
    }
}
