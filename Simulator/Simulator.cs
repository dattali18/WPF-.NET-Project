using BlApi;

namespace SimulatorImplementation;

public static class Simulator
{
    private static Thread? thread;

    public static volatile bool isRunning = false;

    public static event EventHandler<Tuple<BO.Order, int>>? Update;
    public static event EventHandler? Stop;

    private static IBl bl = BlApi.Factory.Get() ?? throw new NullReferenceException("");

    public static void Run()
    {
        isRunning = true;

        thread = new Thread(Simulation);
        thread.Start();
    }

    public static void stopSimulation()
    {
        isRunning = false;
        thread?.Interrupt();
    }

    private static void sleep(int seconds)
    {
        try
        {
            Thread.Sleep(seconds * 1000);
        }
        catch (ThreadInterruptedException) { }
    }

    public static void AddHandlerToUpdate(EventHandler<Tuple<BO.Order, int>> handler)
    {
        Update += handler;
    }

    public static void RemoveHandlerFromUpdate(EventHandler<Tuple<BO.Order, int>> handler)
    {
        if (Update!.GetInvocationList().Contains(handler))
        {
            Update -= handler;
        }
    }

    public static void AddHandlerToStop(EventHandler handler)
    {
        Stop += handler;
    }

    public static void RemoveHandlerFromStop(EventHandler handler)
    {
        if (Stop != null && Stop!.GetInvocationList().Contains(handler))
        {
            Stop -= handler;
        }
    }

    private static void Simulation()
    {
        while (isRunning && bl.Order.GenerateNextOrderToProccess() != null)
        {
            int id = bl.Order.GenerateNextOrderToProccess() ?? 0;
            BO.Order? order = bl.Order.GetOrder(id);
            int handleTime = new Random().Next(3, 7);
            int aproximateTime = new Random().Next(handleTime - 2, handleTime + 2);

            Update?.Invoke(null, new Tuple<BO.Order, int>(order, aproximateTime));

            sleep(handleTime);

            if (order?.Status == BO.OrderStatus.OrderPlaced || 
                order?.Status == BO.OrderStatus.Processing) 
            {
                bl.Order.SetSendOrderForAdmin(id);
            }
            else if (order?.Status == BO.OrderStatus.Shipped)
            {
                bl.Order.SetSendOrderDeliveredForAdmin(id);
            }
        }
        Stop?.Invoke(null, EventArgs.Empty);
        stopSimulation();
    }

    //private static event EventHandler<Tuple<BO.Order, int>>? updateSimulation;
    //private static event EventHandler? stopSimulation;

    //private static volatile bool isSimulationStopped = false;
    //private static bool isSimulationRunning = false;
    //public static bool IsSimulationRunning { get => isSimulationRunning; set => isSimulationRunning = value; }

    //private static IBl bl = BlApi.Factory.Get() ?? throw new NullReferenceException("");

    //private static Thread? thread;

    ///// <summary>
    ///// Running the simulation
    ///// </summary>
    //public static void Run()
    //{
    //    isSimulationStopped = false;
    //    isSimulationRunning = true;


    //    thread = new Thread(doWork) { Name = "Simulation"};
    //    thread?.Start();
    //}

    //public static void doWork()
    //{
    //    while (!isSimulationRunning && bl.Order.GenerateNextOrderToProccess() != null)
    //    {
    //        int handleTime = new Random().Next(3, 7);
    //        int estimatedTime = new Random().Next(handleTime - 2, handleTime + 2);

    //        int id = bl.Order.GenerateNextOrderToProccess() ?? 0;
    //        BO.Order? order = bl.Order.GetOrder(id);

    //        updateSimulation?.Invoke(null, new Tuple<BO.Order, int>(order, estimatedTime));

    //        sleep(handleTime);

    //        if (isSimulationStopped) break;

    //        if (order?.Status == BO.OrderStatus.OrderPlaced)
    //            bl.Order.SetSendOrderForAdmin(id);
    //        else if (order?.Status == BO.OrderStatus.Shipped)
    //            bl.Order.SetSendOrderDeliveredForAdmin(id);
    //    }

    //    stopSimulation?.Invoke(null, EventArgs.Empty);
    //    isSimulationRunning = false;

    //}

    ///// <summary>
    ///// stopping the simulation
    ///// </summary>
    //public static void Stop()
    //{
    //    isSimulationStopped = true;
    //    thread?.Interrupt();
    //}


    //public static void SubToUpdate(EventHandler<Tuple<BO.Order, int>> handler)
    //{
    //    updateSimulation += handler;
    //}

    //public static void SubToStop(EventHandler handler)
    //{
    //    stopSimulation += handler;
    //}

    //public static void UnSubToUpdate(EventHandler<Tuple<BO.Order, int>> handler)
    //{
    //    if (updateSimulation!.GetInvocationList().Contains(handler))
    //        updateSimulation -= handler;
    //}

    //public static void UnSubToStop(EventHandler handler)
    //{
    //    if (stopSimulation!.GetInvocationList().Contains(handler))
    //        stopSimulation -= handler;
    //}

    //private static void sleep(int time)
    //{
    //    try { Thread.Sleep(time * 1000); } catch (ThreadInterruptedException) { }
    //}
}