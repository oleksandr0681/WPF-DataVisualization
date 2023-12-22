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
            if (chartType.SelectedItem != null && chartType.SelectedItem is ComboBoxItem)
            {
                inputValues.ChartType = (chartType.SelectedItem as ComboBoxItem).Content.ToString();
            }
            if (lineType.SelectedItem != null && lineType.SelectedItem is ComboBoxItem)
            {
                inputValues.LineType = (lineType.SelectedItem as ComboBoxItem).Content.ToString();
            }
            if (lineColor.SelectedItem != null && lineColor.SelectedItem is ComboBoxItem)
            {
                inputValues.LineColor = (lineColor.SelectedItem as ComboBoxItem).Content.ToString();
            }
            if (backgroundColor.SelectedItem != null && backgroundColor.SelectedItem is ComboBoxItem)
            {
                inputValues.BackgroundColor = (backgroundColor.SelectedItem as ComboBoxItem).Content.ToString();
            }
            inputValues.LegendTitle = legendTitle.Text;
            inputValues.SeriesName = seriesName.Text;
            inputValues.AxisXTitle = axisXTitle.Text;
            inputValues.AxisYTitle = axisYTitle.Text;
            if (legendDocking.SelectedItem != null && legendDocking.SelectedItem is ComboBoxItem)
            {
                inputValues.LegendDocking = (legendDocking.SelectedItem as ComboBoxItem).Content.ToString();
            }
            if (legendBackgroundColor.SelectedItem != null && legendBackgroundColor.SelectedItem is ComboBoxItem)
            {
                inputValues.LegendBackgroundColor = (legendBackgroundColor.SelectedItem as ComboBoxItem).Content.ToString();
            }
            return inputValues;
        }

        private void openDataFile_Click(object sender, RoutedEventArgs e)
        {
            List<string> points = new List<string>();
            string exceptionMessage = Data.Open(ref points);
            if (exceptionMessage != null)
            {
                MessageBox.Show(exceptionMessage);
            }
            else
            {
                exceptionMessage = Data.SplitAxes(points, out listX, out listY);
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

        private void drawChart_Click(object sender, RoutedEventArgs e)
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

        private void aboutAuthor_Click(object sender, RoutedEventArgs e)
        {
            AboutAuthorWindow aboutAuthor = new AboutAuthorWindow();
            aboutAuthor.ShowDialog();
        }
    }
}
