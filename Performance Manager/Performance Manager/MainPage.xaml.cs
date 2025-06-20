using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Performance_Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private Dictionary<string, long> _buttonTimings = new Dictionary<string, long>();
        List<Task<int>> heavyTasks = new List<Task<int>>();
        CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        List<Employeess> employeesses = new List<Employeess>
        {
            new Employeess{Id=1,Gender='M',FirstName="Bob",LastName="Jones",Salary=45000},
            new Employeess{Id=3,Gender='F',FirstName="Ava",LastName="Stuwart",Salary=38000},
            new Employeess{Id=4,Gender='M',FirstName="Andrew",LastName="Jackson",Salary=41000},
            new Employeess{Id=2,Gender='F',FirstName="Ana",LastName="Clinton",Salary=36000},
        };
        static readonly IEnumerable<string> _urlList = new string[]
    {
            "https://learn.microsoft.com",
            "https://learn.microsoft.com/aspnet/core",
            "https://learn.microsoft.com/azure",
            "https://learn.microsoft.com/azure/devops",
            "https://learn.microsoft.com/dotnet",
            "https://learn.microsoft.com/dynamics365",
            "https://learn.microsoft.com/education",
            "https://learn.microsoft.com/enterprise-mobility-security",
            "https://learn.microsoft.com/gaming",
            "https://learn.microsoft.com/graph",
            "https://learn.microsoft.com/microsoft-365",
            "https://learn.microsoft.com/office",
            "https://learn.microsoft.com/powershell",
            "https://learn.microsoft.com/sql",
            "https://learn.microsoft.com/surface",
            "https://learn.microsoft.com/system-center",
            "https://learn.microsoft.com/visualstudio",
            "https://learn.microsoft.com/windows",
            "https://learn.microsoft.com/maui"
    };
        HeavyTasks HeavyTasks = new HeavyTasks();
        HttpClient httpClient = new HttpClient();
        public void AddListItemToListView(string text)
        {
            ListViewItem listViewItem = new ListViewItem();
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            listViewItem.Content = textBlock;
            theListView.Items.Add(listViewItem);
        }
        private async void localButton_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();
            await Task.Delay(3000);
            string message = $"Hello User welcome to the Performance manager  ";
            AddListItemToListView(message);
            stopWatch.Stop();
            _buttonTimings["A Fast Local Operation"] = stopWatch.ElapsedMilliseconds;
        }

        private async void cpuButton_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();
            heavyTasks.Add(HeavyTasks.Calculate());
            heavyTasks.Add(HeavyTasks.CalculatingStuff());
            heavyTasks.Add(HeavyTasks.Calculate2());
            heavyTasks.Add(HeavyTasks.Calculate3());
            await Task.WhenAll(heavyTasks);
            AddListItemToListView($"CPU operation nr.1 : {heavyTasks[0].Result.ToString()}");
            AddListItemToListView($"CPU operation nr.2 :  {heavyTasks[1].Result.ToString()}");
            AddListItemToListView($"CPU operation nr.3 : {heavyTasks[2].Result.ToString()}");
            AddListItemToListView($"CPU operation nr.4 :{ heavyTasks[3].Result.ToString()}");
            stopWatch.Stop();
            _buttonTimings["Cpu Bound Operation"] = stopWatch.ElapsedMilliseconds;
        }

        private async void filterer_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();
            AddListItemToListView($"The list in ascending Id order : ");
            var listAscended = from p in employeesses
                               orderby p.Id
                               select p;
            await Task.Delay(2000);
            foreach(var filteredItem in listAscended)
            {
                AddListItemToListView(filteredItem.ToString());
            }

            AddListItemToListView($"The list in descending Id order : ");
            var listDescended = from p in employeesses
                               orderby p.Id descending
                               select p;
            await Task.Delay(2000);
            foreach (var filteredItem in listDescended)
            {
                AddListItemToListView(filteredItem.ToString());
            }
            AddListItemToListView($"The list in ascending Annual Salary : ");
            var salaryAscending = from p in employeesses
                               orderby p.Salary
                               select p;
            await Task.Delay(2000);
            foreach (var filteredItem in salaryAscending)
            {
                AddListItemToListView(filteredItem.ToString());
            }
            AddListItemToListView($"The list in descending Annual Salary : ");
            var salaryDescending= from p in employeesses
                                  orderby p.Salary descending
                                  select p;
            await Task.Delay(2000);
            foreach (var filteredItem in salaryDescending)
            {
                AddListItemToListView(filteredItem.ToString());
            }
            AddListItemToListView($"The males in the List  : ");
            await Task.Delay(2000);
            var onlyMales = employeesses.Where(m=>m.Gender=='M');
            foreach(var m in onlyMales)
            {
                AddListItemToListView(m.ToString());
            }
            AddListItemToListView($"The females in the List  : ");
            await Task.Delay(2000);
            var onlyFemales = employeesses.Where(f => f.Gender == 'F');
            foreach (var f in onlyFemales)
            {
                AddListItemToListView(f.ToString());
            }
            stopWatch.Stop();
            _buttonTimings["Filter the list"] = stopWatch.ElapsedMilliseconds;
        }

        private async void JSONDataButton_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();
            HttpResponseMessage message = await httpClient.GetAsync("http://localhost:5210/api/MockData");
            string content = await message.Content.ReadAsStringAsync();
            AddListItemToListView(content);
            stopWatch.Stop();
            _buttonTimings["Get Json Data"] = stopWatch.ElapsedMilliseconds;

        }
        private async Task<int> Download(string url,CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage =  await httpClient.GetAsync(url,cancellationToken);
            byte[] data = await httpResponseMessage.Content.ReadAsByteArrayAsync();
            return data.Length;
        }
        private async Task ProcessTheUrls()
        {
            int length = 0;
            foreach (string url in _urlList)
            {
              
                int x =  await Download(url,CancellationTokenSource.Token);
                length += x;
                AddListItemToListView($"{url,-10} Size in bytes  :{length}");
            }
           
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                CancellationTokenSource.CancelAfter(25000);
                await ProcessTheUrls();
            }
            catch (TaskCanceledException)
            {
                AddListItemToListView("The downloads for the urls are cancelled");
            }
            catch(Exception ex)
            {
                AddListItemToListView($"An error occurred: {ex.Message}");
            }
            
            stopWatch.Stop();
            _buttonTimings["Download Some Urls"] = stopWatch.ElapsedMilliseconds;
        }

        private void cancelDownLoadButton_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();
            CancellationTokenSource.Cancel();
            stopWatch.Stop();
            _buttonTimings["Cancel Downloads"] = stopWatch.ElapsedMilliseconds;
        }

        private async void listViewer_Click(object sender, RoutedEventArgs e)
        {

            var stopWatch = Stopwatch.StartNew();
            AddListItemToListView($"The List : ");
            foreach(var employee in employeesses)
            {
                await Task.Delay(1000);
                AddListItemToListView(employee.ToString());
            }
            stopWatch.Stop();
            _buttonTimings["View The List"] = stopWatch.ElapsedMilliseconds;
        }

        private async void measurer_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(operationName.Text))
            {
                if (_buttonTimings.ContainsKey(operationName.Text))
                {
                    AddListItemToListView($"Time it took for the operation to complete in milliseconds : {_buttonTimings[operationName.Text].ToString()}");
                }
                else
                {
                    await SpeakAsync($"Either the user input does not match the button name(operation),or the operation must be finished before this input");
                }
            }
            else
            {
                await SpeakAsync($"Empty user input");
            }
            
          
        }
        private async Task SpeakAsync(string text)
        {

            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);
            mediaElement.SetSource(stream, stream.ContentType);

        }
    }
}
