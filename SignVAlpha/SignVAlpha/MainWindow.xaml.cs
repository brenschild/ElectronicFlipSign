using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Documents;
using System.Diagnostics;
using System.Windows.Data;
using System.Collections.Generic;

namespace SignVAlpha
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
			
            GetPorts();
			
            ButtonsControl.IsEnabled = false;
            Brightness.IsEnabled = false;
            Save.IsEnabled = false;
            DisconnectButton.IsEnabled = false;
            DisconnectedText.Visibility = Visibility.Visible;
            ConnectedText.Visibility = Visibility.Collapsed;
			
			// Set scroll speed to default
            b1ScrollSpeed.SelectedIndex = 0;
            b2ScrollSpeed.SelectedIndex = 0;
            b3ScrollSpeed.SelectedIndex = 0;
            b4ScrollSpeed.SelectedIndex = 0;
            b5ScrollSpeed.SelectedIndex = 0;
            b6ScrollSpeed.SelectedIndex = 0;
			
			// Set display speed
            b1DisplayTime.Text = "5";
            b2DisplayTime.Text = "5";
            b3DisplayTime.Text = "5";
            b4DisplayTime.Text = "5";
            b5DisplayTime.Text = "5";
            b6DisplayTime.Text = "5";
            b1DisplayTime.Text = "5";

        }

        //Serial 
        SerialPort serial = new SerialPort
        {
            //Sets up serial port
            BaudRate = Convert.ToInt32(9600),
            Handshake = System.IO.Ports.Handshake.None,
            Parity = Parity.None,
            DataBits = 8,

        };
        private void GetPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            List<string> ComList = new List<string>();

            ComList.Add("Choose one");
            foreach (string port in ports)
            {
                ComList.Add(port);
            }

            COMBox.ItemsSource = ComList;
        }

        private void Connect(object sender, RoutedEventArgs e)
        {

            try
            {
                if (!serial.IsOpen)
                {
                    serial.Open();
                    DisconnectedText.Visibility = Visibility.Collapsed;
                    ConnectedText.Visibility = Visibility.Visible;
                    DisconnectButton.IsEnabled = true;
                    ConnectButton.IsEnabled = false;
                    ButtonsControl.IsEnabled = true;
                    Save.IsEnabled = true;
                    Brightness.IsEnabled = true;
                }
            }

            catch (Exception a)
            {
                Debug.Write("Not Working");
            }
        }

        private void Disconnect(object sender, RoutedEventArgs e)
        {
            if (serial.IsOpen)
            {
                DisconnectedText.Visibility = Visibility.Visible;
                ConnectedText.Visibility = Visibility.Collapsed;
                serial.Close();
                ConnectButton.IsEnabled = true;
                DisconnectButton.IsEnabled = false;
                ButtonsControl.IsEnabled = false;
                Save.IsEnabled = false;
            }
        }

        /*public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }*/

        public static string addSpaces(string s)
        {
            int length = s.Length;
            int i;
            for (i = length; i < 6;i++)
            {
                s = s+" ";
            }
            return s;
        }

        private void SaveStuffToArduino(object sender, RoutedEventArgs e)
        {

            try
            {
                if (serial.IsOpen)
                {
                    
                    // signal to send data to MCU
                    serial.Write("{");


                    // Send SingleLine data of each button
                    string SingleLine = b1SingleLine.IsChecked == true ? "1" : "0";
                    SingleLine += b2SingleLine.IsChecked == true ? "1" : "0";
                    SingleLine += b3SingleLine.IsChecked == true ? "1" : "0";
                    SingleLine += b4SingleLine.IsChecked == true ? "1" : "0";
                    SingleLine += b5SingleLine.IsChecked == true ? "1" : "0";
                    SingleLine += b6SingleLine.IsChecked == true ? "1" : "0";
                    SingleLine += "=";
                    serial.Write(SingleLine);


                    // Send AutoScroll data of each button
                    string AutoScroll = b1AutoScroll.IsChecked == true ? "1" : "0";
                    AutoScroll += b2AutoScroll.IsChecked == true ? "1" : "0";
                    AutoScroll += b3AutoScroll.IsChecked == true ? "1" : "0";
                    AutoScroll += b4AutoScroll.IsChecked == true ? "1" : "0";
                    AutoScroll += b5AutoScroll.IsChecked == true ? "1" : "0";
                    AutoScroll += b6AutoScroll.IsChecked == true ? "1" : "0";
                    AutoScroll += "=";
                    serial.Write(AutoScroll);
                    
                    // Send ScrollSpeed data of each button
                    string ScrollSpeed = b1ScrollSpeed.SelectionBoxItem.ToString();
                    ScrollSpeed += b2ScrollSpeed.SelectionBoxItem.ToString();
                    ScrollSpeed += b3ScrollSpeed.SelectionBoxItem.ToString();
                    ScrollSpeed += b4ScrollSpeed.SelectionBoxItem.ToString();
                    ScrollSpeed += b5ScrollSpeed.SelectionBoxItem.ToString();
                    ScrollSpeed += b6ScrollSpeed.SelectionBoxItem.ToString();
                    ScrollSpeed += "=";
                    serial.Write(ScrollSpeed);
                    
                    // Send DisplayTime data of each button
                    serial.Write(b1DisplayTime.Text + "=");
                    serial.Write(b2DisplayTime.Text + "=");
                    serial.Write(b3DisplayTime.Text + "=");
                    serial.Write(b4DisplayTime.Text + "=");
                    serial.Write(b5DisplayTime.Text + "=");
                    serial.Write(b6DisplayTime.Text + "=");

                    // Send Line1 of each button
                    serial.Write(" " + addSpaces(b1Line1.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b2Line1.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b3Line1.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b4Line1.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b5Line1.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b6Line1.Text.ToUpper()) + "=");

                    // Send Line2 of each button
                    serial.Write(" " + addSpaces(b1Line2.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b2Line2.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b3Line2.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b4Line2.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b5Line2.Text.ToUpper()) + "=");
                    serial.Write(" " + addSpaces(b6Line2.Text.ToUpper()) + "=");

                    // Send Mirror data of each button
                    string Reverse = b1Reverse.IsChecked == true ? "1" : "0";
                    Reverse += b2Reverse.IsChecked == true ? "1" : "0";
                    Reverse += b3Reverse.IsChecked == true ? "1" : "0";
                    Reverse += b4Reverse.IsChecked == true ? "1" : "0";
                    Reverse += b5Reverse.IsChecked == true ? "1" : "0";
                    Reverse += b6Reverse.IsChecked == true ? "1" : "0";
                    Reverse += "=";
                    serial.Write(Reverse);

                    //Send Flashing data of each button
                    string Flash = b1Flashing.IsChecked == true ? "1" : "0";
                    Flash += b2Flashing.IsChecked == true ? "1" : "0";
                    Flash += b3Flashing.IsChecked == true ? "1" : "0";
                    Flash += b4Flashing.IsChecked == true ? "1" : "0";
                    Flash += b5Flashing.IsChecked == true ? "1" : "0";
                    Flash += b6Flashing.IsChecked == true ? "1" : "0";
                    Flash += "=";
                    serial.Write(Flash);

                    serial.Write("}");
                    
                }
            }
            catch (Exception f)
            {

            }
        }

        void ShutDown(object sender, RoutedEventArgs e)
        {
            if (serial.IsOpen)
            {
                serial.Close();
            }
            System.Windows.Application.Current.Shutdown();
        }

        private void b1SingleLine_Click(object sender, RoutedEventArgs e)
        {

            b1Line2.IsEnabled = (b1SingleLine.IsChecked == true) ? false : true;
            if (b1SingleLine.IsChecked == true)
            {
                b1AutoScroll.IsChecked = false;
            }
            b1AutoScroll.IsEnabled = (b1SingleLine.IsChecked == true) ? false : true;

        }

        private void b2SingleLine_Click(object sender, RoutedEventArgs e)
        {
            b2Line2.IsEnabled = (b2SingleLine.IsChecked == true) ? false : true;
            if (b2SingleLine.IsChecked == true)
            {
                b2AutoScroll.IsChecked = false;
            }
            b2AutoScroll.IsEnabled = (b2SingleLine.IsChecked == true) ? false : true;

        }

        private void b3SingleLine_Click(object sender, RoutedEventArgs e)
        {
            b3Line2.IsEnabled = (b3SingleLine.IsChecked == true) ? false : true;
            if (b3SingleLine.IsChecked == true)
            {
                b3AutoScroll.IsChecked = false;
            }
            b3AutoScroll.IsEnabled = (b3SingleLine.IsChecked == true) ? false : true;

        }

        private void b4SingleLine_Click(object sender, RoutedEventArgs e)
        {
            b4Line2.IsEnabled = (b4SingleLine.IsChecked == true) ? false : true;
            if (b4SingleLine.IsChecked == true)
            {
                b4AutoScroll.IsChecked = false;
            }
            b4AutoScroll.IsEnabled = (b4SingleLine.IsChecked == true) ? false : true;

        }

        private void b5SingleLine_Click(object sender, RoutedEventArgs e)
        {
            b5Line2.IsEnabled = (b5SingleLine.IsChecked == true) ? false : true;
            if (b5SingleLine.IsChecked == true)
            {
                b5AutoScroll.IsChecked = false;
            }
            b5AutoScroll.IsEnabled = (b5SingleLine.IsChecked == true) ? false : true;

        }

        private void b6SingleLine_Click(object sender, RoutedEventArgs e)
        {
            b6Line2.IsEnabled = (b6SingleLine.IsChecked == true) ? false : true;
            if (b6SingleLine.IsChecked == true)
            {
                b6AutoScroll.IsChecked = false;
            }
            b6AutoScroll.IsEnabled = (b6SingleLine.IsChecked == true) ? false : true;

        }



        private void COMBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (COMBox.SelectedIndex != 0)
            {

                serial.PortName = COMBox.SelectedItem.ToString();

                ConnectButton.IsEnabled = true;

                if (serial.IsOpen)
                {
                    serial.Close();
                }
                ButtonsControl.IsEnabled = false;

                DisconnectButton.IsEnabled = false;
                DisconnectedText.Visibility = Visibility.Visible;
                ConnectedText.Visibility = Visibility.Collapsed;
                Save.IsEnabled = false;
            }

            else
            {
                if (serial.IsOpen)
                {
                    serial.Close();
                }
                ButtonsControl.IsEnabled = false;
                ConnectButton.IsEnabled = false;
                DisconnectButton.IsEnabled = false;
                DisconnectedText.Visibility = Visibility.Visible;
                ConnectedText.Visibility = Visibility.Collapsed;
                Save.IsEnabled = false;
            }
        }

        private void b1AutoScroll_Click(object sender, RoutedEventArgs e)
        {
            b1ScrollSpeed.IsEnabled = (b1AutoScroll.IsChecked == false) ? false : true;
            if (b1AutoScroll.IsChecked == true)
            {
                b1SingleLine.IsChecked = false;
                b1Flashing.IsChecked = false;
            }

            b1Flashing.IsEnabled = (b1AutoScroll.IsChecked == true) ? false : true;
            b1SingleLine.IsEnabled = (b1AutoScroll.IsChecked == true) ? false : true;

        }

        private void b2AutoScroll_Click(object sender, RoutedEventArgs e)
        {
            b2ScrollSpeed.IsEnabled = (b2AutoScroll.IsChecked == false) ? false : true;
            if (b2AutoScroll.IsChecked == true)
            {
                b2SingleLine.IsChecked = false;
                b2Flashing.IsChecked = false;
            }

            b2Flashing.IsEnabled = (b2AutoScroll.IsChecked == true) ? false : true;
            b2SingleLine.IsEnabled = (b2AutoScroll.IsChecked == true) ? false : true;

        }

        private void b3AutoScroll_Click(object sender, RoutedEventArgs e)
        {
            b3ScrollSpeed.IsEnabled = (b3AutoScroll.IsChecked == false) ? false : true;
            if (b3AutoScroll.IsChecked == true)
            {
                b3SingleLine.IsChecked = false;
                b3Flashing.IsChecked = false;
            }

            b3Flashing.IsEnabled = (b3AutoScroll.IsChecked == true) ? false : true;
            b3SingleLine.IsEnabled = (b3AutoScroll.IsChecked == true) ? false : true;

        }

        private void b4AutoScroll_Click(object sender, RoutedEventArgs e)
        {
            b4ScrollSpeed.IsEnabled = (b4AutoScroll.IsChecked == false) ? false : true;
            if (b4AutoScroll.IsChecked == true)
            {
                b4SingleLine.IsChecked = false;
                b4Flashing.IsChecked = false;
            }

            b4Flashing.IsEnabled = (b4AutoScroll.IsChecked == true) ? false : true;
            b4SingleLine.IsEnabled = (b4AutoScroll.IsChecked == true) ? false : true;
        }

        private void b5AutoScroll_Click(object sender, RoutedEventArgs e)
        {
            b5ScrollSpeed.IsEnabled = (b5AutoScroll.IsChecked == false) ? false : true;
            if (b5AutoScroll.IsChecked == true)
            {
                b5SingleLine.IsChecked = false;
                b5Flashing.IsChecked = false;
            }

            b5Flashing.IsEnabled = (b5AutoScroll.IsChecked == true) ? false : true;
            b5SingleLine.IsEnabled = (b5AutoScroll.IsChecked == true) ? false : true;

        }

        private void b6AutoScroll_Click(object sender, RoutedEventArgs e)
        {
            b6ScrollSpeed.IsEnabled = (b6AutoScroll.IsChecked == false) ? false : true;
            if (b6AutoScroll.IsChecked == true) {
                b6SingleLine.IsChecked = false;
                b6Flashing.IsChecked = false;
            }

            b6Flashing.IsEnabled = (b6AutoScroll.IsChecked == true) ? false : true;
            b6SingleLine.IsEnabled = (b6AutoScroll.IsChecked == true) ? false : true;

        }


        private void b1Flashing_Click(object sender, RoutedEventArgs e)
        {
            b1AutoScroll.IsEnabled = (b1Flashing.IsChecked == false) ? false : true;
        }

        private void b2Flashing_Click(object sender, RoutedEventArgs e)
        {
            b2AutoScroll.IsEnabled = (b2Flashing.IsChecked == false) ? false : true;
        }


        private void b3Flashing_Click(object sender, RoutedEventArgs e)
        {
            b3AutoScroll.IsEnabled = (b3Flashing.IsChecked == false) ? false : true;
        }

        private void b4Flashing_Click(object sender, RoutedEventArgs e)
        {
            b4AutoScroll.IsEnabled = (b4Flashing.IsChecked == false) ? false : true;

        }

        private void b5Flashing_Click(object sender, RoutedEventArgs e)
        {
            b5AutoScroll.IsEnabled = (b5Flashing.IsChecked == false) ? false : true;
        }


        private void b6Flashing_Click(object sender, RoutedEventArgs e)
        {
            b6AutoScroll.IsEnabled = (b6Flashing.IsChecked == false) ? false : true;
        }

        private void b1Reverse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b2Reverse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b3Reverse_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void b4Reverse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b5Reverse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b6Reverse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


