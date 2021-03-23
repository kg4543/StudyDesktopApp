using LiveCharts;
using MahApps.Metro.Controls;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace PhotoSensorMonApp
{
    public class SensorData
    {
        public int Idx { get; set; }
        public DateTime CurrentDt { get; set; }
        public int Value { get; set; }
        public bool SimulFlag { get; set; }

        public SensorData(int idx, DateTime currentDt, int value, bool simulFlag)
        {
            Idx = idx;
            CurrentDt = currentDt;
            Value = value;
            SimulFlag = simulFlag;
        }
    }


    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();

            logger.Info("PhotoSensorMonApp Initialized....");
        }

        public ChartValues<int> ChartValues { get; set; }

        public int SensorValue { get; set; }

        public DispatcherTimer CustomTimer { get; set; }

        private string connString = "Data Source=210.119.12.92;" +
                                    "Initial Catalog=IoTData;" +
                                    "Persist Security Info=True;" +
                                    "User ID=sa;" +
                                    "Password=mssql_p@ssw0rd!";

        public SensorData GetRealTimeSensor()
        {
            SensorData result = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();

                    var query = @"SELECT TOP 1 Idx
                                              ,CurrentDt
                                              ,Value
                                              ,SimulFlag
                                          FROM dbo.Tbl_PhotoRegister
                                         Order by Idx Desc;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    var temp = cmd.ExecuteReader();
                    if (temp.Read())
                    {
                        result = new SensorData(Convert.ToInt32(temp["Idx"]),
                                                Convert.ToDateTime(temp["CurrentDt"]),
                                                Convert.ToInt32(temp["Value"]),
                                                Convert.ToBoolean(temp["SimulFlag"]));
                    }
                }

                logger.Info("GetRealTimeSensor() completed.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"예외발생 : {ex.Message}");
                logger.Error($"GetRealTimeSensor() error occured : {ex}");
            }
            return result;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            /*var x = Enumerable.Range(0, 1001).Select(i => i / 10.0).ToArray();
            var y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v).ToArray();
            ChtLine.Plot(x, y);*/

            ChartValues = new ChartValues<int>(); //{ 10, 5, 9, 5, 3, 2, 9, 16, 20, 40 };
            GrdHistory.DataContext = ChartValues;

            CustomTimer = new DispatcherTimer();
            CustomTimer.Interval = TimeSpan.FromSeconds(1);
            CustomTimer.Tick += CustomTimer_Tick;
        }

        private void CustomTimer_Tick(object sender, EventArgs e)
        {
            //SensorValue = new Random().Next(0, 1023);
            SensorValue = GetRealTimeSensor().Value;
            GrdRealTime.DataContext = SensorValue;
        }

        private void MnuBegin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CustomTimer.Start();
        }

        private void MnuStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CustomTimer.Stop();
        }

        private void MnuExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MnuLoad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HistoryValues.ItemsSource = GetHistorySensors();
        }

        private List<DataPoint> GetHistorySensors()
        {
            List<DataPoint> result = new List<DataPoint>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();

                    var query = $@"SELECT Top 30 Idx, Value
                                      FROM dbo.Tbl_PhotoRegister
                                     where CurrentDt > CONVERT(DATETIME, '{DateTime.Now.ToString("yyyy-MM-dd")}')
                                     order by Idx;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new DataPoint(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1])));
                    }
                }
                logger.Info("GetHistorySensors() completed.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"예외발생 : {ex.Message}");
                logger.Error($"GetHistorySensors() error occured. : {ex}");
            }

            return result;
        }
    }
}
