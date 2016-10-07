using System;
using System.Threading;

public class Worker
{
  public Thread thread = null;
  private int delay;

  // Constructor
  //
  public Worker(int val)
  {
    delay = val;
  }

  // Main 'worker' function
  //
  protected void process()
  {
    while (1==1)
    {
      Console.Out.WriteLine(thread.Name + " " + DateTime.Now.ToString() + " State:" + thread.ThreadState);
      Thread.Sleep(delay*1000);
    }
  }

  // Start the thread
  //
  public void Start()
  {
    thread = new Thread(new ThreadStart(process));    
    if (thread != null)
    {
      thread.Name = delay.ToString();
      thread.Start();
    }
  }

  // Terminate the thread
  //
  public void Terminate()
  {
    if (thread != null)
      if (thread.IsAlive)
        thread.Abort();
  }

  // Suspend the thread
  //
  public void Suspend()
  {
    if (thread != null)
      if (thread.ThreadState != ThreadState.Suspended)
        thread.Suspend();
  }

  // Resume the thread
  //
  public void Resume()
  {
    if (thread != null)
      if (thread.ThreadState != ThreadState.Running)
        thread.Resume();
  }

}


public class Test
{
  static void Main(string[] args)
  {

    Worker w1 = new Worker(1);
    Worker w2 = new Worker(2);

    w1.Start();
    w2.Start();

    Console.Out.WriteLine("Press 'q' to quit, '1' to Suspend Thread 1, '2' to Resume Thread 1 - Followed by Enter");

    bool bQuit = false;
    while (!bQuit)
    {
      int i = Console.Read();
      // 'q' Pressed
      if (i==113)
        bQuit = true;

      // '1' Pressed
      if (i==49)
      {
        w1.Suspend();
        Console.Out.WriteLine("*Thread 1 Suspended...");
      }

      // '2' Pressed
      if (i==50)
      {
        w1.Resume();
        Console.Out.WriteLine("*Thread 1 Resumed...");
      }

    }
	

    w1.Terminate();
    w2.Terminate();

  }

}