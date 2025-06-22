using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace MatchGame;



/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    DispatcherTimer timer = new DispatcherTimer();
    int tenthOfSecondElapsed;
    int matchsFound;
    public MainWindow()
    {
        InitializeComponent();
        timer.Interval = TimeSpan.FromSeconds(.1);
        timer.Tick += Timer_Tick;
        SetUpGame();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        tenthOfSecondElapsed++;
        timeTextBlock.Text = (tenthOfSecondElapsed / 10F).ToString("0.0s");
        if (matchsFound == 8)
        {
            timer.Stop();
            timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
        }
    }

    private void SetUpGame()
    {
        List<string> animalEmoji = new List<string>()
        {
            "💕", "💕",
            "🤍", "🤍",
            "🧡", "🧡",
            "💔", "💔",
            "💓", "💓",
            "💖", "💖",
            "💘", "💘",
            "💟", "💟",
        };

        Random random = new Random();

        foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
        {
            if (textBlock.Name != "timeTextBlock")
            {
                textBlock.Visibility = Visibility.Visible;
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }

            timer.Start();
            tenthOfSecondElapsed = 0;
            matchsFound = 0;
            
        }
    }

    TextBlock lastTextBlockClicked;  //字段，记录第一个点击的图像
    bool findingMatch = false;  //记录是否点击了配对动物的第一个图像，点了第一个，就变成true

    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock textBlock = sender as TextBlock;
        if (findingMatch == false)
        {
            textBlock.Visibility = Visibility.Hidden;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
        }

        else if (textBlock.Text == lastTextBlockClicked.Text)
        {
            matchsFound++;
            textBlock.Visibility = Visibility.Hidden;
            findingMatch = false;
        }

        else
        {
            lastTextBlockClicked.Visibility = Visibility.Visible;
            findingMatch = false;
        }
    }

    private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (matchsFound == 8)
        {
            SetUpGame();
        }
    }
}