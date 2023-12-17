using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using DataVisualization.xamlInputOutput;

namespace DataVisualization
{
    public static class Data
    {
        public static string Open(ref List<string> lines)
        {
            string exceptionMessage = null;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    // Зчитування рядків з файла, поки не буде досягнутий кінець файла.
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs, Encoding.ASCII))
                        {
                            string tempLine;
                            while (sr.Peek() >= 0)
                            {
                                tempLine = sr.ReadLine();
                                lines.Add(tempLine); 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }
            }
            return exceptionMessage;
        }

        public static string SpitAxes(IList<string> points, out List<double> axisX, out List<double> axisY)
        {
            axisX = new List<double>();
            axisY = new List<double>();
            for (int i = 0; i < points.Count; i++)
            {
                string[] charPoint = points[i].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                double doubleResultX;
                bool conversionXSucceeded = double.TryParse(charPoint[0], out doubleResultX);
                double doubleResultY;
                bool conversionYSucceeded = double.TryParse(charPoint[1], out doubleResultY);
                if (conversionXSucceeded == true && conversionYSucceeded == true)
                {
                    axisX.Add(doubleResultX);
                    axisY.Add(doubleResultY);
                }
                else
                {
                    return "Помилка у вихідних даних.";
                }
            }
            return null;
        }

        public static void DrawChart(ChartData chartData)
        {
            chartData.WinFormsChart.Titles.Clear();
            chartData.WinFormsChart.ChartAreas.Clear();
            chartData.WinFormsChart.Series.Clear();
            chartData.WinFormsChart.Legends.Clear();
            chartData.WinFormsChart.ChartAreas.Add(new ChartArea("Default"));
            chartData.WinFormsChart.Series.Add(new Series("Series"));
            chartData.WinFormsChart.Series["Series"].ChartType = SeriesChartType.Doughnut;
            chartData.WinFormsChart.Legends.Add(new Legend());
            if (chartData.axisXPoints.Count == chartData.axisYPoints.Count)
            {
                for (int i=0; i<chartData.axisXPoints.Count; i++)
                {
                    chartData.WinFormsChart.Series["Series"].Points.AddXY(chartData.axisXPoints[i], chartData.axisYPoints[i]);
                }
            }
        }

        public static void DrawChart(ChartData chartData, InputValues inputValues)
        {
            chartData.WinFormsChart.Titles.Clear();
            chartData.WinFormsChart.ChartAreas.Clear();
            chartData.WinFormsChart.Series.Clear();
            chartData.WinFormsChart.Legends.Clear();
            chartData.WinFormsChart.ChartAreas.Add(new ChartArea("Default"));
            chartData.WinFormsChart.Series.Add(new Series("Series"));
            chartData.WinFormsChart.Series["Series"].ChartType = SeriesChartType.Line;
            if (inputValues.ChartType == "Лінійна діаграма")
            {
                chartData.WinFormsChart.Series["Series"].ChartType = SeriesChartType.Line;
            }
            else if (inputValues.ChartType == "Колова діаграма")
            {
                chartData.WinFormsChart.Series["Series"].ChartType = SeriesChartType.Doughnut;
            }
            
            chartData.WinFormsChart.Legends.Add(new Legend());
            if (chartData.axisXPoints.Count == chartData.axisYPoints.Count)
            {
                for (int i = 0; i < chartData.axisXPoints.Count; i++)
                {
                    chartData.WinFormsChart.Series["Series"].Points.AddXY(chartData.axisXPoints[i], chartData.axisYPoints[i]);
                }
            }
        }
    }
}
