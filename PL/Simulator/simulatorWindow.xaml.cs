using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;

using SI = SimulatorImplementation;
using BO;
using DO;

namespace PL.Simulator;

/// <summary>
/// Interaction logic for simulatorWindow.xaml
/// </summary>
public partial class simulatorWindow : Window, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged()
    {

        if (PropertyChanged != null)
        {
            var e = new PropertyChangedEventArgs("");
            PropertyChanged(this, e);
        }
    }

    private volatile int percentage = 0;

    public int Percentage
    {
        get
        {
            return percentage;
        }
        set
        {
            percentage = value;
            //new Thread(() => { MessageBox.Show(percentage.ToString()); }).Start();
            OnPropertyChanged();
        }
    }


    bool canClose = false;

    //private TimeOnly time = TimeOnly.MinValue;

    Stopwatch stopWatch;

    BackgroundWorker backgroundWorker;

    BackgroundWorker progressBarWorker;


    public simulatorWindow()
    {
        stopWatch = new Stopwatch();

        backgroundWorker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };


        backgroundWorker.DoWork += DoWork;
        backgroundWorker.ProgressChanged += progressChanged;
        backgroundWorker.RunWorkerCompleted += SimulationEnded;

        progressBarWorker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

        progressBarWorker.DoWork += ProgressBar_DoWork;
        progressBarWorker.ProgressChanged += ProgressBar_progressChanged;
        progressBarWorker.RunWorkerCompleted += ProgressBar_RunWorkerCompleted;


        InitializeComponent();
    }

    private void ProgressBar_DoWork(object? sender, DoWorkEventArgs e)
    {

        var strArgument = e.Argument?.ToString();

        if (int.TryParse(strArgument, out int args))
        {
            //progressBarWorker.ReportProgress(args);
            for (int i = 0; i <= 100; i++)
            {
                progressBarWorker.ReportProgress(i);
                System.Threading.Thread.Sleep(100 / args);

                if (progressBarWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    //break;
                }

            }
        }
    }

    private void ProgressBar_progressChanged(object? sender, ProgressChangedEventArgs e)
    {
        //Percentage = e.ProgressPercentage;
        //new Thread(() => { MessageBox.Show(e.ProgressPercentage.ToString()); }).Start();
        PercentagLbl.Content = string.Format("{0}%", e.ProgressPercentage);
        progressBar.Value = e.ProgressPercentage;
    }

    private void ProgressBar_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        progressBarWorker.CancelAsync();
    }

    private void DoWork(object? sender, DoWorkEventArgs e)
    {
        SI.Simulator.AddHandlerToUpdate(StartWorkingOnOrder);

        SI.Simulator.Run();

        stopWatch.Start();

        while (!backgroundWorker.CancellationPending)
        {
            Thread.Sleep(1000);
            backgroundWorker.ReportProgress(0);
        }
    }

    /// <summary>
    /// Update view if needed
    /// </summary>
    private void progressChanged(object? sender, ProgressChangedEventArgs e)
    {
        ClockLbl.Content = stopWatch.Elapsed.ToString("mm\\:ss");
        if (e.UserState != null)
        {

            var args = e.UserState as Tuple<BO.Order, int>;
            var now = DateTime.Now;

            //progress bar starting
            if (!progressBarWorker.IsBusy)
            {
                progressBarWorker.RunWorkerAsync(args?.Item2 ?? 0);
            }


            EstEndTime.Content = (now + new TimeSpan(0, 0, args?.Item2 ?? 0)).ToString("HH:mm:ss");
            StartingTime.Content = now.ToString("HH\\:mm\\:ss");

            OrderID.Content = args!.Item1!.ID;
            OrderID1.Content = args!.Item1!.ID;
            OrderStatus.Content = args!.Item1!.Status ?? throw new NullReferenceException();
            NextOrderStatus.Content = args.Item1.Status == BO.OrderStatus.OrderPlaced ||
                args.Item1.Status == BO.OrderStatus.Processing ?
                BO.OrderStatus.Shipped : BO.OrderStatus.Delivered;


            //progressBarWorker.CancelAsync();
        }
    }

    private void SimulationEnded(object? sender, RunWorkerCompletedEventArgs e)
    {
        SI.Simulator.RemoveHandlerFromUpdate(StartWorkingOnOrder);
        SI.Simulator.RemoveHandlerFromStop(EndSimulator);
    }

    private void EndSimulator(object? sender, EventArgs e)
    {
        backgroundWorker.CancelAsync();
        SI.Simulator.isRunning = false;
    }

    private void StartWorkingOnOrder(object? sender, Tuple<BO.Order, int> args)
    {
        backgroundWorker.ReportProgress(args.Item2, args);
    }    

    private void StartSimulatorBtn_Click(object? sender, EventArgs e)
    {
        stopWatch.Start();
        if(!backgroundWorker.IsBusy)
            backgroundWorker.RunWorkerAsync();
    }

    private void EndSimulatorBtn_Click(object? sender, EventArgs e)
    {
        EndSimulator(null, e);
        canClose = true;
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void Window_Closed(object sender, CancelEventArgs e)
    {
        if (!canClose)
        {
            MessageBox.Show("Can't close Window while simulation running!", "ERROR", MessageBoxButton.OK);
            e.Cancel = true;
        }
        else
        {
            e.Cancel = false;
        }
    }
}
