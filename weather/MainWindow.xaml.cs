using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace weather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<int> Temperatures { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Temperatures = new List<int>();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addTempWindow = new AddTemperatureWindow(this);
            addTempWindow.Show();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var filterSortWindow = new FilterWindow(this);
            FilterWindow.Show();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            var filterSortWindow = new SortWindow(this);
            SortWindow.Show();
        }

        public void UpdateStatistics()
        {
            if (Temperatures.Count == 0)
            {
                MaxTempTextBlock.Text = "Max Temperature: N/A";
                MinTempTextBlock.Text = "Min Temperature: N/A";
                MaxRepeatTextBlock.Text = "Max Repetitions: N/A";
                AnomaliesTextBlock.Text = "Anomalies: N/A";
                return;
            }

            var maxTemp = Temperatures.Max();
            var minTemp = Temperatures.Min();
            var maxRepetition = Temperatures.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Count();
            var anomalies = new List<string>();

            for (int i = 1; i < Temperatures.Count; i++)
            {
                if (Math.Abs(Temperatures[i] - Temperatures[i - 1]) >= 10)
                {
                    anomalies.Add($"Day {i}: {Temperatures[i - 1]} -> {Temperatures[i]}");
                }
            }

            MaxTemperatureTextBlock.Text = $"Max Temperature: {maxTemp}";
            MinTemperatureTextBlock.Text = $"Min Temperature: {minTemp}";
            MaxRepetitionsTextBlock.Text = $"Max Repetitions: {maxRepetition}";
            AnomaliesTextBlock.Text = $"Anomalies: {(anomalies.Count > 0 ? string.Join(", ", anomalies) : "None")}";
        }
    }
}