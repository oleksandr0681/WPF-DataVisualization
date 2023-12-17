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
using System.Windows.Forms.DataVisualization.Charting; // Графіки.
using DataVisualization.xamlInputOutput;

namespace DataVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<double> listX;
        List<double> listY;

        public MainWindow()
        {
            InitializeComponent();
            listX = new List<double>();
            listY = new List<double>();
        }

        private InputValues GetInputValues()
        {
            InputValues inputValues = new InputValues();
            if(ChartType.SelectedItem != null && ChartType.SelectedItem is ComboBoxItem)
            {
                inputValues.ChartType = (ChartType.SelectedItem as ComboBoxItem).Content.ToString();
            }
            return inputValues;
        }

        private void OpenDataFile_Click(object sender, RoutedEventArgs e)
        {
            List<string> points = new List<string>();
            string exceptionMessage = Data.Open(ref points);
            if (exceptionMessage != null)
            {
                MessageBox.Show(exceptionMessage);
            }
            else
            {
                exceptionMessage = Data.SpitAxes(points, out listX, out listY);
                if (exceptionMessage != null)
                {
                    MessageBox.Show(exceptionMessage);
                }
                else
                {
                    ChartData chartData = new ChartData();
                    chartData.WinFormsChart = DataVisualizationChart;
                    chartData.axisXPoints = listX;
                    chartData.axisYPoints = listY;
                    Data.DrawChart(chartData, GetInputValues());
                }
            }
        }

        private void DrawChart_Click(object sender, RoutedEventArgs e)
        {
            if (listX.Count == 0)
            {
                MessageBox.Show("Відсутні дані для діаграми.");
            }
            else
            {
                ChartData chartData = new ChartData();
                chartData.WinFormsChart = DataVisualizationChart;
                chartData.axisXPoints = listX;
                chartData.axisYPoints = listY;
                Data.DrawChart(chartData, GetInputValues());
            }
        }
    }
}
