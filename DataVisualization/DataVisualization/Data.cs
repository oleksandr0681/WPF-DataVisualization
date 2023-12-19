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

        public static void DrawChart(ChartData chartData, InputValues inputValues)
        {
            chartData.WinFormsChart.Titles.Clear();
            chartData.WinFormsChart.ChartAreas.Clear();
            chartData.WinFormsChart.Series.Clear();
            chartData.WinFormsChart.Legends.Clear();
            chartData.WinFormsChart.ChartAreas.Add(new ChartArea("Default"));
            string seriesName;
            if (inputValues.SeriesName != null)
            {
                seriesName = inputValues.SeriesName;
            }
            else
            {
                seriesName = "Series";
            }
            chartData.WinFormsChart.Series.Add(new Series(seriesName));
            chartData.WinFormsChart.Series[seriesName].ChartType = SeriesChartType.Line;
            if (inputValues.ChartType == "Лінійна діаграма")
            {
                chartData.WinFormsChart.Series[seriesName].ChartType = SeriesChartType.Line;
            }
            else if (inputValues.ChartType == "Колова діаграма")
            {
                chartData.WinFormsChart.Series[seriesName].ChartType = SeriesChartType.Doughnut;
            }
            else if (inputValues.ChartType == "Пелюсткова діаграма")
            {
                chartData.WinFormsChart.Series[seriesName].ChartType = SeriesChartType.Radar;
            }
            else if (inputValues.ChartType == "Стовпчаста діаграма")
            {
                chartData.WinFormsChart.Series[seriesName].ChartType = SeriesChartType.Column;
            }
            else if (inputValues.ChartType == "Пірамідальна діаграма")
            {
                chartData.WinFormsChart.Series[seriesName].ChartType = SeriesChartType.Pyramid;
            }
            if (inputValues.LineType == "Суцільна")
            {
                chartData.WinFormsChart.Series[seriesName].BorderDashStyle = ChartDashStyle.Solid;
            }
            else if (inputValues.LineType == "Штрихова")
            {
                chartData.WinFormsChart.Series[seriesName].BorderDashStyle = ChartDashStyle.Dash;
            }
            else if (inputValues.LineType == "Штрих-пунктирна")
            {
                chartData.WinFormsChart.Series[seriesName].BorderDashStyle = ChartDashStyle.DashDot;    
            }
            if (inputValues.LineColor == "Синій")
            {
                chartData.WinFormsChart.Series[seriesName].Color = Color.Blue;
            }
            else if (inputValues.LineColor == "Зелений")
            {
                chartData.WinFormsChart.Series[seriesName].Color = Color.Green;
            }
            else if (inputValues.LineColor == "Червоний")
            {
                chartData.WinFormsChart.Series[seriesName].Color = Color.Red;
            }
            else if (inputValues.LineColor == "Чорний")
            {
                chartData.WinFormsChart.Series[seriesName].Color = Color.Black;
            }
            if (inputValues.BackgroundColor == "Білий")
            {
                chartData.WinFormsChart.ChartAreas["Default"].BackColor = Color.White;
            }
            else if (inputValues.BackgroundColor == "Блакитний")
            {
                chartData.WinFormsChart.ChartAreas["Default"].BackColor = Color.Azure;
            }
            else if (inputValues.BackgroundColor == "Пшеничний")
            {
                chartData.WinFormsChart.ChartAreas["Default"].BackColor = Color.Wheat;
            }
            else if (inputValues.BackgroundColor == "Бежевий")
            {
                chartData.WinFormsChart.ChartAreas["Default"].BackColor = Color.Beige;
            }
            else if (inputValues.BackgroundColor == "Сірий")
            {
                chartData.WinFormsChart.ChartAreas[0].BackColor = Color.LightGray;
            }
            if (inputValues.AxisXTitle != null)
            {
                chartData.WinFormsChart.ChartAreas["Default"].AxisX.Title = inputValues.AxisXTitle;
                chartData.WinFormsChart.ChartAreas["Default"].AxisX.TitleAlignment = StringAlignment.Far;
            }
            if (inputValues.AxisYTitle != null)
            {
                chartData.WinFormsChart.ChartAreas["Default"].AxisY.Title = inputValues.AxisYTitle;
                chartData.WinFormsChart.ChartAreas["Default"].AxisY.TitleAlignment = StringAlignment.Far;
                chartData.WinFormsChart.ChartAreas["Default"].AxisY.TextOrientation = TextOrientation.Horizontal;
            }
            chartData.WinFormsChart.Legends.Add(new Legend("Legend"));
            if (inputValues.LegendTitle != null)
            {
                chartData.WinFormsChart.Legends["Legend"].Title = inputValues.LegendTitle;
            }
            if (inputValues.LegendDocking == "Праворуч")
            {
                chartData.WinFormsChart.Legends["Legend"].Docking = Docking.Right;
            }
            else if (inputValues.LegendDocking == "Ліворуч")
            {
                chartData.WinFormsChart.Legends["Legend"].Docking = Docking.Left;
            }
            else if (inputValues.LegendDocking == "Зверху")
            {
                chartData.WinFormsChart.Legends["Legend"].Docking = Docking.Top;
            }
            else if (inputValues.LegendDocking == "Знизу")
            {
                chartData.WinFormsChart.Legends["Legend"].Docking = Docking.Bottom;
            }
            if (inputValues.LegendBackgroundColor == "Білий")
            {
                chartData.WinFormsChart.Legends["Legend"].BackColor = Color.White;
            }
            else if (inputValues.LegendBackgroundColor == "Блакитний")
            {
                chartData.WinFormsChart.Legends["Legend"].BackColor = Color.Azure;
            }
            else if (inputValues.LegendBackgroundColor == "Пшеничний")
            {
                chartData.WinFormsChart.Legends["Legend"].BackColor = Color.Wheat;
            }
            else if (inputValues.LegendBackgroundColor == "Бежевий")
            {
                chartData.WinFormsChart.Legends["Legend"].BackColor = Color.Beige;
            }
            else if (inputValues.LegendBackgroundColor == "Сірий")
            {
                chartData.WinFormsChart.Legends["Legend"].BackColor = Color.LightGray;
            }
            if (chartData.axisXPoints.Count == chartData.axisYPoints.Count)
            {
                for (int i = 0; i < chartData.axisXPoints.Count; i++)
                {
                    chartData.WinFormsChart.Series[seriesName].Points.AddXY(chartData.axisXPoints[i], chartData.axisYPoints[i]);
                }
            }
        }
    }
}
