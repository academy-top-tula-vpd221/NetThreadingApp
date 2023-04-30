using System.Threading.Channels;
int count = 0;
//object locker = new();

//AutoResetEvent waitHandler = new AutoResetEvent(true);
//Mutex mutex = new Mutex();


//for(int i = 0; i < 6; i++)
//{
//    ReaderBd reader = new ReaderBd(i + 1);
//    //Thread thread = new Thread(CountInc);
//    //thread.Name = $"Thread {i + 1}";
//    //thread.Start();
//}
var thread = Thread.CurrentThread;

Thread thread2 = new Thread(Counter);
thread2.IsBackground = true;
//Thread thread3 = new Thread(CounterNum);

thread2.Start();
thread2.Join();
//thread3.Start(25);
//thread2.Join();
//thread4.Start(new Employee() { Name = "Bob", Age = 23 });
//for (int i = 0; i < 20; i++)
//{
//    Console.WriteLine($"Main thread {i}");
//    Thread.Sleep(100);
//}



void Counter()
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"\tSub thread {i}");
        Thread.Sleep(200);
    }
}

void CounterNum(object obj)
{
    for (int i = 0; i < (int)obj; i++)
    {
        Console.WriteLine($"\t\tSub thread {i}");
        Thread.Sleep(100);
    }
}


void CountInc()
{
    bool isBlock;
    for (int i = 0; i < 10; i++)
    {
        //waitHandler.WaitOne();
        //mutex.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.Name} {++count}");
        Thread.Sleep(100);
        //mutex.ReleaseMutex();
        //waitHandler.Set();
        //try
        //{
        //    isBlock = false;
        //    Monitor.Enter(locker, ref isBlock);
        //    Console.WriteLine($"{Thread.CurrentThread.Name} {++count}");
        //    Thread.Sleep(100);
        //}
        //finally
        //{
        //    Monitor.Exit(locker);
        //}

    }
}

void ThreadWelcome()
{
    var thread = Thread.CurrentThread;

    //Console.WriteLine(thread.Name);
    //thread.Name = "My thread";
    //Console.WriteLine(thread.Name);
    //Console.WriteLine(thread.ManagedThreadId);
    //Console.WriteLine(thread.ThreadState);

    Thread thread2 = new Thread(Counter);
    Thread thread3 = new Thread(CounterNum);
    Thread thread4 = new Thread(PrintEmployee);

    //Thread thread3 = new Thread(new ThreadStart(Counter));
    //Thread thread4 = new Thread(() => Console.WriteLine("Hello"));
    //thread2.IsBackground = true;

    thread2.Start();
    thread3.Start(20);
    //thread4.Start(new Employee() { Name = "Bob", Age = 23 });
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"Main thread {i}");
        Thread.Sleep(300);
    }
    //thread.Join();

    void Counter()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"\tSub thread {i}");
            Thread.Sleep(500);
        }
    }

    void CounterNum(object obj)
    {
        for (int i = 0; i < (int)obj; i++)
        {
            Console.WriteLine($"\t\tSub thread {i}");
            Thread.Sleep(400);
        }
    }

    void PrintEmployee(object obj)
    {
        if (obj is Employee e)
            Console.WriteLine($"{e.Name} {e.Age}");
    }
}


class ReaderBd
{
    static Semaphore semaphore = new Semaphore(3, 3);
    static Random random = new Random();
    Thread thread;
    int count = 3;

    public ReaderBd(int id)
    {
        thread = new Thread(Read);
        thread.Name = $"Reader #{id}";
        thread.Start();
    }

    public void Read()
    {
        while (count > 0)
        {
            semaphore.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} enter");
            Console.WriteLine($"{Thread.CurrentThread.Name} read");
            Thread.Sleep(random.Next(200, 500));
            Console.WriteLine($"{Thread.CurrentThread.Name} leave");
            semaphore.Release();

            count--;
            Thread.Sleep(random.Next(200, 500));
        }
    }
}
class Employee
{
    public string Name { get; set; }
    public int Age { get; set; }
}