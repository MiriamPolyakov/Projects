using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>

public partial class SimulatorWindow : Window
{


    private Stopwatch stopWatch;
    Duration duration;
    DoubleAnimation doubleanimation;
    ProgressBar ProgressBar;
    Simulator.SimulatorDetails? details;
    private bool flag = true;
    BackgroundWorker worker;
    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    public SimulatorWindow()
    {
        InitializeComponent();
        stopWatch = new Stopwatch();
        worker = new BackgroundWorker();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        stopWatch.Start();
        worker.RunWorkerAsync();
    }

    void ProgressBarStart(int sec)
    {
        if (ProgressBar != null)
        {
            progressBar.Items.Remove(ProgressBar);
        }
        ProgressBar = new ProgressBar();
        ProgressBar.IsIndeterminate = false;
        ProgressBar.Orientation = Orientation.Horizontal;
        ProgressBar.Width = 750;
        ProgressBar.Height = 30;
        duration = new Duration(TimeSpan.FromSeconds(sec * 2));
        doubleanimation = new DoubleAnimation(200.0, duration);
        ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        progressBar.Items.Add(ProgressBar);
    }
    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        try
        {
            Simulator.Simulator.ProgressChange += change;
            Simulator.Simulator.StopSimulator += stop;
            Simulator.Simulator.play();
            while (flag)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        catch(Simulator.Exceptions.Simulator_BlFromDalEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message + " " + ex?.InnerException?.Message);
        }
    }


    private void onLoad(object sender, RoutedEventArgs e)
    {
        var hwnd = new WindowInteropHelper(this).Handle;
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    private void finishBtn_Click(object sender, RoutedEventArgs e)
    {
        Simulator.Simulator.Stop();
    }
    private void stop(object sender, EventArgs e)
    {
        Simulator.Simulator.ProgressChange -= change;
        Simulator.Simulator.StopSimulator -= stop;
        if (flag)
        {
            stopWatch.Stop();
            flag = false;
        }
    }
    private void change(object sender, EventArgs e)
    {

        if (!(e is Simulator.SimulatorDetails))
        {
            return;
        }
        details = e as Simulator.SimulatorDetails;
        stopWatch.Restart();
        Tuple<BO.Enums.eOrderStatus, BO.Enums.eOrderStatus, int, int, string> tupel =
            new(details.nextStatus, details.prevStatus, details.id, details.seconds, DateTime.Now.ToLongTimeString());
        if (!CheckAccess())
        {
            Dispatcher.BeginInvoke(change, sender, e);
        }
        else
        {
            DataContext = tupel;
            ProgressBarStart(details.seconds);
            // AdminWindow.orderCollection.ToList();
        }
    }

    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        MessageBox.Show("simulator completed!!");
        this.Close();
    }

    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        timerLbl.Content = timerText;

    }
}
