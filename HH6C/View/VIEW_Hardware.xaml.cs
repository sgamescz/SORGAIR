using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using System.IO.Ports;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Globalization;


namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro HledaniView.xaml
    /// </summary>
    public partial class Hardware : UserControl
    {
        
        SerialPort _serialPort;
        private MODEL_ViewModel VM => DataContext as MODEL_ViewModel;


        public Hardware()
        {
            InitializeComponent();



        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            _serialPort = new SerialPort(serialport.Text, 9600, Parity.None, 8, StopBits.One);
            _serialPort.Handshake = Handshake.None;
            _serialPort.DataReceived += DataReceivedHandler; //This is to add event handler delegate when data is received by the port

            // Makes sure serial port is open before trying to write  
            try
            {
                _serialPort.Open();
                if (!(_serialPort.IsOpen))
                    _serialPort.Open();

                _serialPort.WriteLine("AT/r/n");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //VM.HW_ATI = "ABCD";

            _serialPort.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                _serialPortWrite("AT+MEM\r");
                _serialPortWrite("AT+UPTIME\r");
                _serialPortWrite("AT+SN\r");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
            }
        }

        private void _serialPortWrite(string cozapsat)
        {
            Console.WriteLine("Zapisuji na seriový port:" + cozapsat);
            _serialPort.Write(cozapsat);
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {



            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Detecting, please wait...", "Detecting SORG AIR Hardware...");

            await Task.Delay(500);


            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            Console.WriteLine("The following serial ports were found:" + ports);
            controller.SetMessage("Awaiable ports : " + string.Join(", ", ports));
            await Task.Delay(1000);

            Console.WriteLine("[{0}]", string.Join(", ", ports));

            // Display each port name to the console.
            foreach (string port in ports)
            {
                Console.WriteLine("Checking port:" + port);
                controller.SetProgress(0.1);
                controller.SetMessage("Checking port : " + port);
                await Task.Delay(500);

                _serialPort = new SerialPort();

                _serialPort.ReadTimeout = 10;
                _serialPort.WriteTimeout = 10;
                _serialPort.Handshake = Handshake.None;
                _serialPort.PortName = port;
                _serialPort.BaudRate = 9600;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Parity = Parity.None;
                _serialPort.DataReceived += DataReceivedHandler; //This is to add event handler delegate when data is received by the port

                // Makes sure serial port is open before trying to write  
                try
                {
                    _serialPort.Open();
                    if (!(_serialPort.IsOpen))
                        _serialPort.Open();
                    Console.WriteLine("Serial port " + port + " is open");
                    _serialPortWrite("ATI\r");
                    _serialPortWrite("AT+SN\r");
                    _serialPortWrite("AT+MEM\r");
                    _serialPortWrite("AT+UPTIME\r");

                    //_serialPort.Write("ATI\r");
                    //_serialPort.Write("AT+SN\r");



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error opening/writing to serial port :: " + ex.Message, "Error!");
                }


            }

            Console.ReadLine();
            await Task.Delay(1000);
            if (VM.BINDING_HW_MENU_BASE is true)
            {
                controller.SetMessage("SORG AIR Hardware found, great!");
            }
            else
            {
                controller.SetMessage("NO SORG AIR Hardware found :(");
            }
            await Task.Delay(1000);
            await controller.CloseAsync();

        }




        public void DataReceivedHandler(
                       object sender,
                       SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Console.WriteLine(sp.BytesToRead + "bytes to read");
            while (sp.BytesToRead > 0)
            {
                Console.WriteLine("reading by line if bytestoread is greather than zero");

                string indata = sp.ReadLine();
                if (indata.Contains("Sorg HW")) { 
                    this.Invoke(() => VM.HW_ATI = indata);
                    this.Invoke(() => VM.BINDING_HW_MENU_BASE = true);
                }
                if (indata.Contains("+MEM")) { this.Invoke(() => VM.HW_ATIMEM = indata); }
                if (indata.Contains("+UPTIME")) { this.Invoke(() => VM.HW_ATIUPTIME = indata); }
                if (indata.Contains("+SN")) { this.Invoke(() => VM.HW_ATISN = indata); }
                Console.WriteLine("Data Received:" + indata);
            }


            Console.Write("------------------\n");
        }

        private void serialport_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pripojit_old_Click(object sender, RoutedEventArgs e)
        {

            VM._serialPort  = new SerialPort(serialportold.Text);

            VM._serialPort.Handshake = Handshake.None;
            VM._serialPort.BaudRate = 19200;
            VM._serialPort.StopBits = StopBits.One;
            VM._serialPort.Parity = Parity.None;
            VM._serialPort.DataBits = 8;

            // Makes sure serial port is open before trying to write  
            try
            {
                VM._serialPort.Open();
                if (!(VM._serialPort.IsOpen))
                    VM._serialPort.Open();

                
                byte[] bytes = { 0x83, 0x1, 0x0, 0x0, 0x84, 0x81};
                VM._serialPort.Write(bytes, 0, bytes.Length);
                Console.WriteLine("serial open");
                VM.HARDWARE_CLOCK_OLD_ISCONNECTED = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
            }

        }

        private void clock_stopky_old_Click(object sender, RoutedEventArgs e)
        {


            VM.FUNCTION_CLOCK_SET_STOPWATCH_MODE();


        }

        private void clock_hodiny_old_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_CLOCK_SET_CLOCK_MODE();

        }

        private void clock_odpojit_old_Click(object sender, RoutedEventArgs e)
        {


             VM._serialPort.Close();
            VM.HARDWARE_CLOCK_OLD_ISCONNECTED = false;

        }

        private void clock_stopkyup_old_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_CLOCK_SET_DIRECTION(1);
            //VM.FUNCTION_CLOCK_SET_STOPWATCH_COUNT_UP(1, 1);
        }

        private void clock_stopkydown_old_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_CLOCK_SET_DIRECTION(2);

            //VM.FUNCTION_CLOCK_SET_STOPWATCH_COUNT_DOWN(2, 2);

        }

        private void clock_stopkysettime_old_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_CLOCK_SET_STOPWATCH_TIME(10, 00);

        }

        private void clock_stopkystop_old_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_CLOCK_SET_DIRECTION(0);

            //VM.FUNCTION_CLOCK_SET_STOPWATCH_STOP();
        }
    }
}
