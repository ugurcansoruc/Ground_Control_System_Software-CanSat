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
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.IO;
using System.IO.Ports;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;



namespace WpfApp2
{

    public partial class MainWindow : Window
    {

        SerialPort sp;
        string port;
        int baudRate;
        int dataBit;
        Parity parityBit;
        StopBits stpBit;

        string dosya_yolu = "C:\\Users\\Sinan\\Desktop\\3.6.2019 hanifin pc den\\3.6.2019 Arayuz SON\\FullArayüz-Yedek\\Flight_6160.csv";

        FileStream fs;
        StreamWriter sw;

        public MainWindow()
        {
            InitializeComponent();
            Grid_Ust2.Visibility = Visibility.Visible;
            Grid_Charts.Visibility = Visibility.Visible;
            listview1.Visibility = Visibility.Hidden;
            mapView.Visibility = Visibility.Hidden;

            fs = new FileStream(dosya_yolu, FileMode.Create, FileAccess.Write);// file mode kontrol edilecek.
            sw = new StreamWriter(fs);

            stpBit = StopBits.One;

            combobox_Baudrate.Items.Add("19200");
            combobox_Databits.Items.Add("8");
            combobox_Parity.Items.Add("None");


            string[] portlar = SerialPort.GetPortNames();

            foreach (string portAdi in portlar)
            {
                combobox_Ports.Items.Add(portAdi);
            }

            load3dModel();
            startl1.Background = Brushes.Red;
            startl2.Background = Brushes.Red;
            startl3.Background = Brushes.Red;

            btn_Stop.IsEnabled = false;
            btn_Calibre.IsEnabled = false;

        }

        public void load3dModel()
        {
            ObjReader CurrentHelixObjReader = new ObjReader();
            Model3DGroup MyModel = CurrentHelixObjReader.Read(@"C:\Users\Sinan\Desktop\3.6.2019 hanifin pc den\3.6.2019 Arayuz SON\FullArayüz-Yedek\uydu.obj");
            model.Content = MyModel;
            var hVp3D = new HelixViewport3D();
            var mode = new ModelVisual3D();
            hVp3D.Children.Add(mode);
        }

        private void Btn_Charts_Click(object sender, RoutedEventArgs e)
        {
            mapView.Visibility = Visibility.Hidden;
            Grid_Charts.Visibility = Visibility.Visible;
            listview1.Visibility = Visibility.Hidden;
        }


        private void Btn_ListView_Click(object sender, RoutedEventArgs e)
        {
            Grid_Charts.Visibility = Visibility.Hidden;
            listview1.Visibility = Visibility.Visible;
            mapView.Visibility = Visibility.Hidden;
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            if (combobox_Ports.Text != "" && combobox_Baudrate.Text != "" && combobox_Databits.Text != "" && combobox_Parity.Text != "")
            {


                XBee_configure();
                State1.Background = Brushes.LawnGreen;
                startl1.Background = Brushes.LawnGreen;
                btn_Start.IsEnabled = false;
                btn_Stop.IsEnabled = true;

                if (calibra_ctrl == 0)
                {
                    btn_Calibre.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("XBee Ayarlarını kontrol ediniz !");
            }
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            if (deneme_kontrol2 == 1)
            {
                sp.Close();
                startl1.Background = Brushes.Red;

                btn_Start.IsEnabled = true;
                btn_Stop.IsEnabled = false;
                btn_Calibre.IsEnabled = false;
            }
        }

        private void Btn_GPS_Click(object sender, RoutedEventArgs e)
        {
            mapView.Visibility = Visibility.Visible;
            Grid_Charts.Visibility = Visibility.Hidden;
            listview1.Visibility = Visibility.Hidden;
        }

        private void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            combobox_Ports.Items.Clear();
            string[] portlar = SerialPort.GetPortNames();
            foreach (string portAdi in portlar)
            {
                combobox_Ports.Items.Add(portAdi);
            }
        }

        private void XBee_configure()
        {
            port = combobox_Ports.SelectedItem.ToString();

            switch (combobox_Baudrate.SelectedIndex)
            {

                case 0:
                    baudRate = 19200;
                    break;
            }
            switch (combobox_Databits.SelectedIndex)
            {

                case 0:
                    dataBit = 8;
                    break;
            }

            switch (combobox_Parity.SelectedIndex)
            {
                case 0:
                    parityBit = Parity.None;
                    break;
            }
            sp = new SerialPort(port, baudRate, parityBit, dataBit, stpBit);
            sp.DataReceived += new SerialDataReceivedEventHandler(veri_AL);
            sp.Open();
        }

        string[] telemetri = new string[12000];
        string veri = "";
        string team_ID;
        string mission_time;
        int packet_count = 0;
        double altitude = 0;
        int pressure = 0;
        double temperature = 0;
        double voltage = 0;
        string gps_time;
        double gps_latitude = 0;
        double gps_longitude = 0;
        double gps_altitude = 0;
        int gps_sats = 0;
        int pitch = 0;
        int roll = 0;
        int blade_spin_rate = 0;
        int software_state = 0;
        int bonus_direction = 0;
        int pitch0 = 0;
        int roll0 = 0;
        char ayrac = ',';
        string[] ayrilmis_veriler;
        int sayac = 0;

        int deneme_kontrol = 0;
        int deneme_kontrol2 = 0;


        private void veri_AL(object sender, SerialDataReceivedEventArgs e)
        {
            deneme_kontrol2 = 0;
            veri = sp.ReadLine();
            deneme_kontrol2 = 1;
            for (int i = 0; i < veri.Length; i++)
            {
                if (veri[i] == ',')
                {
                    deneme_kontrol++;
                }
            }
            if (deneme_kontrol == 16)
            {
                telemetri[sayac] = veri;
                sayac++;
                verileri_ayiklave_yazdir();


            }
            deneme_kontrol = 0;
        }

        string deneme = "";


        public void verileri_ayiklave_yazdir()
        {
            ayrilmis_veriler = veri.Split(ayrac);

            deneme += ayrilmis_veriler[0];
            deneme += ",";
            deneme += ayrilmis_veriler[1];
            deneme += ",";
            deneme += ayrilmis_veriler[2];
            deneme += ",";
            deneme += ayrilmis_veriler[3];
            deneme += ",";
            deneme += ayrilmis_veriler[4];
            deneme += ",";
            deneme += ayrilmis_veriler[5];
            deneme += ",";
            deneme += ayrilmis_veriler[6];
            deneme += ",";
            deneme += ayrilmis_veriler[7];
            deneme += ",";
            deneme += ayrilmis_veriler[8];
            deneme += ",";
            deneme += ayrilmis_veriler[9];
            deneme += ",";
            deneme += ayrilmis_veriler[10];
            deneme += ",";
            deneme += ayrilmis_veriler[11];
            deneme += ",";
            deneme += ayrilmis_veriler[12];
            deneme += ",";
            deneme += ayrilmis_veriler[13];
            deneme += ",";
            deneme += ayrilmis_veriler[14];
            deneme += ",";
            deneme += ayrilmis_veriler[15];
            deneme += ",";
            deneme += ayrilmis_veriler[16];

            sw.Write(deneme);
            sw.Flush();

            deneme = "";

            try
            {
                team_ID = ayrilmis_veriler[0];
                lblTeam.Dispatcher.Invoke(() =>
                {
                    lblTeam.Content = team_ID;
                });
            }
            catch { }

            try
            {
                mission_time = ayrilmis_veriler[1];
                lblMissiontime.Dispatcher.Invoke(() =>
                {
                    lblMissiontime.Content = mission_time;
                });
            }
            catch { }

            try
            {
                packet_count = Convert.ToInt32(ayrilmis_veriler[2]);
                lblPacketcount.Dispatcher.Invoke(() =>
                {
                    lblPacketcount.Content = packet_count;
                });
            }
            catch { }

            try
            {
                altitude = double.Parse(string.Format("{0:0.#}", ayrilmis_veriler[3]));
                veri_Altitude.Dispatcher.Invoke(() =>
                {
                    if (calibra_ctrl != 1)
                    {
                        veri_Altitude.AddPoint(mission_time, altitude);
                    }
                    else
                    {
                        veri_Altitude2.AddPoint(mission_time, altitude);
                    }
                });


            }
            catch { }

            try
            {
                pressure = Convert.ToInt32(double.Parse(ayrilmis_veriler[4]));
                veri_Pressure.Dispatcher.Invoke(() =>
                {
                    if (calibra_ctrl != 1)
                    {
                        veri_Pressure.AddPoint(mission_time, pressure);
                    }
                    else
                    {
                        veri_Pressure2.AddPoint(mission_time, pressure);
                    }
                    
                });
            }
            catch { }

            try
            {
                temperature = double.Parse(string.Format("{0:0.#}", ayrilmis_veriler[5]));
                veri_Temperature.Dispatcher.Invoke(() =>
                {
                    if (calibra_ctrl != 1)
                    {
                        veri_Temperature.AddPoint(mission_time, temperature);
                    }
                    else
                    {
                        veri_Temperature2.AddPoint(mission_time, temperature);
                    }
                });
            }
            catch { }

            try
            {
                voltage = double.Parse(string.Format("{0:0.##}", ayrilmis_veriler[6]));
                veri_Voltage.Dispatcher.Invoke(() =>
                {
                    if (calibra_ctrl != 1)
                    {
                        veri_Voltage.AddPoint(mission_time, voltage);
                    }
                    else
                    {
                        veri_Voltage2.AddPoint(mission_time, voltage);
                    }
                });
            }
            catch { }

            gps_time = ayrilmis_veriler[7];

            try
            {
                gps_latitude = double.Parse(string.Format("{0:0.####}", ayrilmis_veriler[8]));
                gps_longitude = double.Parse(string.Format("{0:0.####}", ayrilmis_veriler[9]));


                mapView.Dispatcher.Invoke(() =>
                 {
                     marker = new GMap.NET.WindowsPresentation.GMapMarker(new GMap.NET.PointLatLng(gps_latitude, gps_longitude));
                     marker.Shape = new Ellipse
                     {
                         Width = 10,
                         Height = 10,
                         Stroke = Brushes.Red,
                         StrokeThickness = 2,
                         Visibility = Visibility.Visible,
                         Fill = Brushes.Yellow,

                     };
                     mapView.Zoom = 18;
                     mapView.Position = new GMap.NET.PointLatLng(gps_latitude, gps_longitude);
                     mapView.Markers.Clear();
                     mapView.Markers.Add(marker);
                 });
            }
            catch { }

            try
            {
                gps_altitude = double.Parse(string.Format("{0:0.#}", ayrilmis_veriler[10]));
            }
            catch { }

            try
            {
                gps_sats = Convert.ToInt32(ayrilmis_veriler[11]);
                lblSats.Dispatcher.Invoke(() =>
                {
                    lblSats.Content = gps_sats;
                });
            }
            catch { }
            if (software_state == 4)
            {
                try
                {

                    pitch = Convert.ToInt32(ayrilmis_veriler[12]);
                    roll = Convert.ToInt32(ayrilmis_veriler[13]);

                    //if (roll < 65)
                    // {
                    this.Dispatcher.Invoke(() =>
                    {
                        var matrix = model.Transform.Value; //değer değiştirme için model e erişme
                        Vector3D vc3d1 = new Vector3D();

                        if (roll < 0)
                        {
                            roll += 360;
                        }

                        //sağsol--------
                        vc3d1.X = 0;
                        vc3d1.Y = 0;
                        vc3d1.Z = 1;
                        matrix.Rotate(new Quaternion(vc3d1, roll - roll0));

                        model.Transform = new MatrixTransform3D(matrix);
                        //-----sağsol için

                        roll0 = roll;
                    });


                    this.Dispatcher.Invoke(() =>
                    {
                        var matrix = model.Transform.Value; //değer değiştirme için model e erişme
                        Vector3D vc3d2 = new Vector3D();

                        //yukariasaği-------
                        vc3d2.X = 1;
                        vc3d2.Y = 0;
                        vc3d2.Z = 0;
                        matrix.Rotate(new Quaternion(vc3d2, pitch - pitch0)); //buradada x y z ye açı değeri yollar.           
                        model.Transform = new MatrixTransform3D(matrix);
                        //--------------yukariasagi

                        pitch0 = pitch;
                    });
                    //  }
                }

                catch { }
            }
            try
            {
                blade_spin_rate = Convert.ToInt32(ayrilmis_veriler[14]);
                veri_BladeSpinRate.Dispatcher.Invoke(() =>
                {

                    if (calibra_ctrl != 1)
                    {
                        veri_BladeSpinRate.AddPoint(mission_time, blade_spin_rate);
                    }
                    else
                    {
                        veri_BladeSpinRate2.AddPoint(mission_time, blade_spin_rate);
                    }
                  
                });
            }
            catch { }
            
            //Burasını Feyzullah düzeltirse silincek
            /////////////////////////////////////////////////////////////////////////////////////////////////
            if (packet_count != 0)
            {
                try
                {
                    software_state = Convert.ToInt32(ayrilmis_veriler[15]);
                    if (software_state == 1)
                    {
                        State2.Dispatcher.Invoke(() =>
                        {
                            State2.Background = Brushes.LawnGreen;
                        });

                    }
                    else if (software_state == 2)
                    {
                        State2.Dispatcher.Invoke(() =>
                        {
                            State2.Background = Brushes.LawnGreen;
                        });
                        State3.Dispatcher.Invoke(() =>
                        {
                            State3.Background = Brushes.LawnGreen;
                        });

                    }
                    else if (software_state == 3)
                    {
                        State2.Dispatcher.Invoke(() =>
                        {
                            State2.Background = Brushes.LawnGreen;
                        });
                        State3.Dispatcher.Invoke(() =>
                        {
                            State3.Background = Brushes.LawnGreen;
                        });
                        State4.Dispatcher.Invoke(() =>
                        {
                            State4.Background = Brushes.LawnGreen;
                        });

                    }
                    else if (software_state == 4)
                    {
                        State2.Dispatcher.Invoke(() =>
                        {
                            State2.Background = Brushes.LawnGreen;
                        });
                        State3.Dispatcher.Invoke(() =>
                        {
                            State3.Background = Brushes.LawnGreen;
                        });
                        State4.Dispatcher.Invoke(() =>
                        {
                            State4.Background = Brushes.LawnGreen;
                        });
                        State5.Dispatcher.Invoke(() =>
                        {
                            State5.Background = Brushes.LawnGreen;
                        });

                    }
                    if (software_state == 4 && altitude < 10)
                    {
                        State2.Dispatcher.Invoke(() =>
                        {
                            State2.Background = Brushes.LawnGreen;
                        });
                        State3.Dispatcher.Invoke(() =>
                        {
                            State3.Background = Brushes.LawnGreen;
                        });
                        State4.Dispatcher.Invoke(() =>
                        {
                            State4.Background = Brushes.LawnGreen;
                        });
                        State5.Dispatcher.Invoke(() =>
                        {
                            State5.Background = Brushes.LawnGreen;
                        });
                        State6.Dispatcher.Invoke(() =>
                        {
                            State6.Background = Brushes.LawnGreen;
                        });

                    }
                }
                catch { }
            }
            try
            {
                bonus_direction = Convert.ToInt32(double.Parse(ayrilmis_veriler[16]));
                veri_BonusDirection.Dispatcher.Invoke(() =>
                {
                    if (calibra_ctrl != 1)
                    {
                        veri_BonusDirection.AddPoint(mission_time, bonus_direction);
                    }
                    else
                    {
                        veri_BonusDirection2.AddPoint(mission_time, bonus_direction);
                    }
                    
                });
            }
            catch { }

            try
            {

                listview1.Dispatcher.Invoke(() =>
                {
                    listview1.Items.Add(new { TeamID = ayrilmis_veriler[0], MissionTime = ayrilmis_veriler[1], PacketCount = ayrilmis_veriler[2], Altitude = ayrilmis_veriler[3], Pressure = ayrilmis_veriler[4], Temp = ayrilmis_veriler[5], Voltage = ayrilmis_veriler[6], GPSTime = ayrilmis_veriler[7], GPSLatitude = ayrilmis_veriler[8], GPSLongitude = ayrilmis_veriler[9], GPSAltitude = ayrilmis_veriler[10], GPSSats = ayrilmis_veriler[11], Pitch = ayrilmis_veriler[12], Roll = ayrilmis_veriler[13], BladeSpinRate = ayrilmis_veriler[14], SoftwareState = ayrilmis_veriler[15], BonusDirection = ayrilmis_veriler[16] });
                    listview1.SelectedIndex = listview1.Items.Count - 1;
                    listview1.ScrollIntoView(listview1.SelectedItem);

                });

            }
            catch { }


        }


        GMap.NET.WindowsPresentation.GMapMarker marker;
        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.CacheLocation = @"C:\Users\Sinan\Desktop\3.6.2019 hanifin pc den\3.6.2019 Arayuz SON\FullArayüz-Yedek\WpfApp2";
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;
            mapView.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            mapView.MaxZoom = 24;
            mapView.MinZoom = 6;
            mapView.CanDragMap = false;
            mapView.Position = new GMap.NET.PointLatLng(32.24694, -98.2009);
            mapView.ShowCenter = false;
            marker = new GMap.NET.WindowsPresentation.GMapMarker(new GMap.NET.PointLatLng(32.24694, -98.2009));

            marker.Shape = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                Visibility = Visibility.Visible,
                Fill = Brushes.Yellow,

            };
            mapView.Markers.Add(marker);
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            mapView.DragButton = MouseButton.Left;
            mapView.Zoom = 16;
        }

        int calibra_ctrl = 0;
        int tik_ctrl = 0;
        private void Btn_Calibre_Click(object sender, RoutedEventArgs e)
        {
            //SONRADAN EKLENDI
            ///////////////////////////////////////////////////////////////////////////////
            veri_Altitude.Points.Clear();
            veri_BladeSpinRate.Points.Clear();
            veri_BonusDirection.Points.Clear();
            veri_Pressure.Points.Clear();
            veri_Temperature.Points.Clear();
            veri_Voltage.Points.Clear();
            ////////////////////////////////////////////////////////////////////////////////////
            calibra_ctrl = 1;
            sp.Write("1");
            tik_ctrl++;
            if (tik_ctrl % 2 == 0)
            {
                startl2.Background = Brushes.Red;
                startl3.Background = Brushes.LawnGreen;
            }
            else
            {
                startl2.Background = Brushes.LawnGreen;
                startl3.Background = Brushes.Red;
            }




            State2.Dispatcher.Invoke(() =>
            {
                State2.Background = Brushes.Red;
            });
            State3.Dispatcher.Invoke(() =>
            {
                State3.Background = Brushes.Red;
            });
            State4.Dispatcher.Invoke(() =>
            {
                State4.Background = Brushes.Red;
            });
            State5.Dispatcher.Invoke(() =>
            {
                State5.Background = Brushes.Red;
            });
            State6.Dispatcher.Invoke(() =>
            {
                State6.Background = Brushes.Red;
            });
        }


    }
}
