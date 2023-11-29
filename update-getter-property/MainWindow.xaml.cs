﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
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

namespace update_getter_property
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new TestViewModel();
            InitializeComponent();
        }
    }
    public class TestViewModel : ViewModelBase
    {
        public DispatcherTimer _timer;

        public TestViewModel()
        {
            Test1 = new TestObject();
            Test1.Name = "TestName";

            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
                Test1.TimeSpan = DateTime.Now - Test1.DateTimeInit;
            };
            _timer.Start();
        }

        private TestObject test1;
        public TestObject Test1
        {
            get
            {
                return test1;
            }
            set
            {
                test1 = value;
                OnPropertyChanged(nameof(Test1));
            }
        }
    }
    public class TestObject : INotifyPropertyChanged
    {
        public string Name
        {
            get => _name;
            set
            {
                if (!Equals(_name, value))
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        string _name = string.Empty;

        public DateTime DateTimeInit { get; } = DateTime.Now;
        
        public TimeSpan TimeSpan
        {
            get => _timeSpan;
            set
            {
                if (!Equals(_timeSpan, value))
                {
                    _timeSpan = value;
                    OnPropertyChanged();
                }
            }
        }
        TimeSpan _timeSpan = default;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}