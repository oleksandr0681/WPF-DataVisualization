using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace DataVisualization
{
    public class ChartData
    {
        public Chart WinFormsChart { get; set; }

        public List<double> axisXPoints { get; set; }

        public List<double> axisYPoints { get; set; }
    }
}
