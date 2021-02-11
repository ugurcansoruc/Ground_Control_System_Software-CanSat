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


using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Grid_Ust2.Visibility = Visibility.Visible;
            Grid_Charts.Visibility = Visibility.Visible;
            listview1.Visibility = Visibility.Hidden;
            //GPS_Maps.Visibility = Visibility.Hidden;
            offlineMAP.Visibility = Visibility.Hidden;  


            combobox_Ports.Items.Add("COM7");
            combobox_Baudrate.Items.Add("9600");
            combobox_Databits.Items.Add("8");
            combobox_Parity.Items.Add("None");
            lblMissiontime.Content = "15:10:21";
            lblPacketcount.Content = "10";
            lblTeam.Content = "6160";
            lblSats.Content = "23";


            veri_Altitude.AddPoint("15:10:11", 450.5);
            veri_Altitude.AddPoint("15:10:12", 400.1);
            veri_Altitude.AddPoint("15:10:13", 300.4);
            veri_Altitude.AddPoint("15:10:14", 200.2);
            veri_Altitude.AddPoint("15:10:15", 155.0);
            veri_Altitude.AddPoint("15:10:16", 110.7);
            veri_Altitude.AddPoint("15:10:17", 69.7 );
            veri_Altitude.AddPoint("15:10:18", 34.9 );
            veri_Altitude.AddPoint("15:10:19", 14.1 );
            veri_Altitude.AddPoint("15:10:20", 2.2 );
            veri_Altitude.AddPoint("15:10:21", 0.0);

            veri_Pressure.AddPoint("15:10:11", 100);
            veri_Pressure.AddPoint("15:10:12", 100);
            veri_Pressure.AddPoint("15:10:13", 102);
            veri_Pressure.AddPoint("15:10:14", 101);
            veri_Pressure.AddPoint("15:10:15", 98);
            veri_Pressure.AddPoint("15:10:16", 105);
            veri_Pressure.AddPoint("15:10:17", 100);
            veri_Pressure.AddPoint("15:10:18", 103);
            veri_Pressure.AddPoint("15:10:19", 103);
            veri_Pressure.AddPoint("15:10:20", 103);
            veri_Pressure.AddPoint("15:10:21", 101);

            veri_Temperature.AddPoint("15:10:11", 31.5);
            veri_Temperature.AddPoint("15:10:12", 30.1);
            veri_Temperature.AddPoint("15:10:13", 30.0);
            veri_Temperature.AddPoint("15:10:14", 30.2);
            veri_Temperature.AddPoint("15:10:15", 32.3);
            veri_Temperature.AddPoint("15:10:16", 31.4);
            veri_Temperature.AddPoint("15:10:17", 30.6);
            veri_Temperature.AddPoint("15:10:18", 30.8);
            veri_Temperature.AddPoint("15:10:19", 30.9);
            veri_Temperature.AddPoint("15:10:20", 29.1);
            veri_Temperature.AddPoint("15:10:21", 30.5);


            veri_BladeSpinRate.AddPoint("15:10:11", 8);
            veri_BladeSpinRate.AddPoint("15:10:12", 10);
            veri_BladeSpinRate.AddPoint("15:10:13", 9);
            veri_BladeSpinRate.AddPoint("15:10:14", 7);
            veri_BladeSpinRate.AddPoint("15:10:15", 7);
            veri_BladeSpinRate.AddPoint("15:10:16", 7);
            veri_BladeSpinRate.AddPoint("15:10:17", 6);
            veri_BladeSpinRate.AddPoint("15:10:18", 9);
            veri_BladeSpinRate.AddPoint("15:10:19", 7);
            veri_BladeSpinRate.AddPoint("15:10:20", 8);
            veri_BladeSpinRate.AddPoint("15:10:21", 9);


            veri_BonusDirection.AddPoint("15:10:11", 2);
            veri_BonusDirection.AddPoint("15:10:12", 5);
            veri_BonusDirection.AddPoint("15:10:13", 1);
            veri_BonusDirection.AddPoint("15:10:14", 0);
            veri_BonusDirection.AddPoint("15:10:15", 0);
            veri_BonusDirection.AddPoint("15:10:16", 0);
            veri_BonusDirection.AddPoint("15:10:17", 0);
            veri_BonusDirection.AddPoint("15:10:18", 6);
            veri_BonusDirection.AddPoint("15:10:19", 7);
            veri_BonusDirection.AddPoint("15:10:20", 8);
            veri_BonusDirection.AddPoint("15:10:21", 10);



           veri_Voltage.AddPoint("15:10:11", 0.6);
           veri_Voltage.AddPoint("15:10:12", 0.6);
           veri_Voltage.AddPoint("15:10:13", 0.7);
           veri_Voltage.AddPoint("15:10:14", 0.6);
           veri_Voltage.AddPoint("15:10:15", 0.5);
           veri_Voltage.AddPoint("15:10:16", 0.5);
           veri_Voltage.AddPoint("15:10:17", 0.3);
           veri_Voltage.AddPoint("15:10:18", 0.3);
           veri_Voltage.AddPoint("15:10:19", 0.5);
           veri_Voltage.AddPoint("15:10:20", 0.6);
           veri_Voltage.AddPoint("15:10:21", 0.4);



            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:11", PacketCount ="0", Altitude =  "450.5", Pressure = "100", Temp = "31.5", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621 ", GPSLongitude = "34.1308", GPSAltitude = "450.3", GPSSats ="21",   Pitch = "5", Roll = "4", BladeSpinRate = "8", SoftwareState ="1",  BonusDirection = "2" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:12", PacketCount = "1", Altitude = "400.1", Pressure = "100", Temp = "30.1", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621 ", GPSLongitude = "34.1308", GPSAltitude =  "400.6", GPSSats = "21", Pitch = "2", Roll = "-4",BladeSpinRate = "10",SoftwareState = "2", BonusDirection = "5" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:13", PacketCount = "2", Altitude = "300.4", Pressure = "102", Temp = "30.0", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621 ", GPSLongitude = "34.1308", GPSAltitude =  "300.7", GPSSats = "21", Pitch = "3", Roll = "1", BladeSpinRate = "9", SoftwareState = "3", BonusDirection = "1" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:14", PacketCount = "3", Altitude = "200.2", Pressure = "101", Temp = "30.2", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621", GPSLongitude = "34.1308", GPSAltitude =  "200.0", GPSSats = "21", Pitch = "1", Roll = "0", BladeSpinRate = "7", SoftwareState = "3", BonusDirection = "0" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:15", PacketCount = "4", Altitude = "155.0", Pressure = "98 ", Temp = "32.3", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621", GPSLongitude = "34.1308", GPSAltitude =  "155.1", GPSSats = "21", Pitch = "5", Roll = "0", BladeSpinRate = "7", SoftwareState = "3", BonusDirection = "0" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:16", PacketCount = "5", Altitude = "110.7", Pressure = "105", Temp = "31.4", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621", GPSLongitude = "34.1308", GPSAltitude =  "110.1", GPSSats = "21", Pitch = "3", Roll = "0", BladeSpinRate = "7", SoftwareState = "3", BonusDirection = "0" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:17", PacketCount = "6", Altitude = "69.7", Pressure = "100", Temp = "30.6", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621", GPSLongitude = "34.1308", GPSAltitude = "69.4", GPSSats = "21", Pitch = "-4", Roll = "2",BladeSpinRate = "6", SoftwareState = "4", BonusDirection = "0" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:18", PacketCount = "7", Altitude = "34.9", Pressure = "103", Temp = "30.8", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621", GPSLongitude = "34.1308", GPSAltitude = "34.8", GPSSats = "21", Pitch = "8", Roll = "3", BladeSpinRate = "9", SoftwareState = "4", BonusDirection = "6" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:19", PacketCount = "8", Altitude = "14.1", Pressure = "103", Temp = "30.9", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621", GPSLongitude = "34.1308", GPSAltitude = "14.7", GPSSats = "21", Pitch = "1", Roll = "5", BladeSpinRate = "7", SoftwareState = "5", BonusDirection = "7" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:20", PacketCount = "9", Altitude = "2.2",   Pressure = "103", Temp = "29.1", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621", GPSLongitude = "34.1308", GPSAltitude = "2.8", GPSSats = "21", Pitch = "0", Roll = "1", BladeSpinRate = "8", SoftwareState = "5", BonusDirection = "8" });
            listview1.Items.Add(new { TeamID = 6160, MissionTime = "15:10:21", PacketCount = "10", Altitude ="0.0",  Pressure = "101", Temp = "30.5", Voltage = "0,6", GPSTime = "15.10", GPSLatitude = "47.5621", GPSLongitude = "34.1308", GPSAltitude = "0.0", GPSSats = "21", Pitch = "2", Roll = "9", BladeSpinRate = "9", SoftwareState = "6", BonusDirection = "10" });




            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            offlineMAP.CacheLocation = @"C:\\Users\\Sinan\\source\\repos\\WpfApp1\\WpfApp1\\bin\\Debug\\cache";
            offlineMAP.DragButton = MouseButton.Left;

            offlineMAP.MapProvider = GMapProviders.GoogleMap;
            offlineMAP.ShowCenter = false;
            offlineMAP.Position = new PointLatLng(38.663516, 55.495512);

    
            offlineMAP.MinZoom = 5;
            offlineMAP.MaxZoom = 100;



            load3dModel();
        }

        public void load3dModel()
        {
            ObjReader CurrentHelixObjReader = new ObjReader();
            // Model3DGroup MyModel = CurrentHelixObjReader.Read(@"D:\3DModel\dinosaur_FBX\dinosaur.fbx");
            Model3DGroup MyModel = CurrentHelixObjReader.Read(@"C:\Users\Sinan\Desktop\New WinRAR arşivi\137522.obj");
            //C:\Users\Sinan\Desktop\New WinRAR arşivi

            model.Content = MyModel;
        }

        private void Btn_Charts_Click(object sender, RoutedEventArgs e)
        {

            //GPS_Maps.Visibility = Visibility.Hidden;
            offlineMAP.Visibility = Visibility.Hidden;
            Grid_Charts.Visibility = Visibility.Visible;
            listview1.Visibility = Visibility.Hidden;

        }
        int i = 1;
        Random r = new Random();
        private void dongu_tick(object sender, EventArgs e)
        {

            i++;
            veri_Altitude.AddPoint(i, r.Next(1, 50));
            veri_Pressure.AddPoint(i, r.Next(1, 50));
            veri_Temperature.AddPoint(i, r.Next(1, 50));
            veri_BladeSpinRate.AddPoint(i, r.Next(1, 50));
            veri_BonusDirection.AddPoint(i, r.Next(1, 50));
            veri_Voltage.AddPoint(i, r.Next(1, 50));
            listeye_Ekle();

        }


        private void listeye_Ekle()
        {
            listview1.Items.Add(new { TeamID = 3944, MissionTime = r.Next(1, 50), PacketCount = r.Next(1, 50), Altitude= r.Next(1, 50), Pressure = r.Next(1, 50), Temp = r.Next(1, 50), Voltage = r.Next(1, 50), GPSTime = r.Next(1, 50), GPSLatitude = r.Next(1, 50), GPSLongitude = r.Next(1, 50), GPSAltitude = r.Next(1, 50), GPSSats = r.Next(1, 50), Pitch = r.Next(1, 50), Roll = r.Next(1, 50), BladeSpinRate = r.Next(1, 50), SoftwareState = r.Next(1, 50), BonusDirection = r.Next(1, 50) });
        }

        private void Btn_ListView_Click(object sender, RoutedEventArgs e)
        {
            Grid_Charts.Visibility = Visibility.Hidden;
            listview1.Visibility = Visibility.Visible;

            //GPS_Maps.Visibility = Visibility.Hidden;
            offlineMAP.Visibility = Visibility.Hidden;
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
          
            System.Windows.Threading.DispatcherTimer dongu = new System.Windows.Threading.DispatcherTimer();
            dongu.Tick += dongu_tick;
            dongu.Interval = new TimeSpan(0, 0, 1);
            dongu.Start();
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void Btn_GPS_Click(object sender, RoutedEventArgs e)
        {

            //GPS_Maps.Visibility = Visibility.Visible;
            offlineMAP.Visibility = Visibility.Visible;

            Grid_Charts.Visibility = Visibility.Hidden;
            listview1.Visibility = Visibility.Hidden;
           

        }
    }
}
