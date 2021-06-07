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
using WpfApp6.Model;
using System.IO.Ports;

namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro HledaniView.xaml
    /// </summary>
    public partial class Hardware : UserControl
    {
        SerialPort _serialPort;

        public Hardware()
        {
            InitializeComponent();



        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            _serialPort = new SerialPort(serialport.Text, 9600, Parity.None, 8, StopBits.One);
            _serialPort.Handshake = Handshake.None;

            // Makes sure serial port is open before trying to write  
            try
            {
                _serialPort.Open();
                if (!(_serialPort.IsOpen))
                    _serialPort.Open();

                byte[] buff = { 0x83, 0x0, 0x0, 0x0, 0x83, 0x81 };
                _serialPort.Write(buff, 0, buff.Length);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _serialPort.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                _serialPort.Open();
                if (!(_serialPort.IsOpen))
                    _serialPort.Open();

                byte[] buff = { 0x83, 0x1, 0x0, 0x0, 0x84, 0x81 };
                _serialPort.Write(buff, 0, buff.Length);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
            }
        }
    }
}
