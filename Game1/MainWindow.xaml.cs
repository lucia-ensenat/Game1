using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Game1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    DispatcherTimer timer = new DispatcherTimer();
    private int tenthsOfSecondsElapsed;
    private int matchesFound;

    public MainWindow()
    {
        InitializeComponent();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        SetUpGame();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        tenthsOfSecondsElapsed++;
        timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        if (matchesFound == 8)
        {
            timer.Stop();
            timeTextBlock.Text = timeTextBlock.Text + "- ¿Juegas otra vez?";
        }
    }


    private void SetUpGame()
    {
        List<string> animals = new List<string>()
        {
            "🐹", "🐰", "🐇", "🐿️", "🦔", "🦇", "🐻", "🐷",
            "🐹", "🐰", "🐇", "🐿️", "🦔", "🦇", "🐻", "🐷"
        };

        Random random = new Random();

        foreach (TextBlock t in mainGrid.Children.OfType<TextBlock>())
        {
            if (t.Name != "timeTextBlock")
            {
                t.Visibility = Visibility.Visible;
                int index = random.Next(animals.Count);
                string next = animals[index];
                t.Text = next;
                animals.RemoveAt(index);
            }
        }
        
        timer.Start();
        tenthsOfSecondsElapsed = 0;
        matchesFound = 0;
    }

    TextBlock lastTextBlock;
    bool finding;

    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock tb = (TextBlock)sender;


        if (finding == false)
        {
            tb.Visibility = Visibility.Hidden;
            lastTextBlock = tb;
            finding = true;
            return;
        }

        if (lastTextBlock.Text == tb.Text)
        {
            tb.Visibility = Visibility.Hidden;
            finding = false;
            lastTextBlock = null;
            matchesFound++;
            return;
        }

        lastTextBlock.Visibility = Visibility.Visible;
        finding = false;
    }

    private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (matchesFound == 8)
            SetUpGame();
    }
}