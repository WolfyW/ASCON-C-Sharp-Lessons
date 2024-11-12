namespace ConsoleApp1;

using System.Text;

internal class Program
{
    private static int num = 0;

    static void Main(string[] args)
    {
        IWindow car = new Car();
        car.Open();
        car.Close();


        IOpen window = new Window();

        Open(car);
        Open(window);
    }

    static void Open(IOpen open)
    {
        open.Open();
    }
}

public interface IOpen
{
    void Open();
    string Name { get; }
    private void Data(int y)
    {
        Console.WriteLine(y);
    }
}

public interface IWindow : IOpen
{
    void Close();
}

public class Window : Car, IOpen, IWork
{
    public string Name { get; set; }

    public void Open()
    {
        Console.WriteLine("Вы открыли окно");
    }
}

public class Car : IOpen, IWindow
{
    public string Name { get; set; }

    public int Time => throw new NotImplementedException();

    public event EventHandler WorkStoped;
    public event EventHandler WorkStarted;

    void IOpen.Open()
    {
        Console.WriteLine("Вы открыли машину");
    }

    void IWindow.Open()
    {
        Console.WriteLine("Вы открыли окно");
    }

    public void Start(TimeSpan time)
    {
        throw new NotImplementedException();
    }

    public void Start(TimeSpan time, Action callBack)
    {
        throw new NotImplementedException();
    }

    public int Stop()
    {
        throw new NotImplementedException();
    }
}

public interface IWork 
{
    event EventHandler WorkStoped;
    event EventHandler WorkStarted;
    const int MAX = 40;
    int Time { get; }
    void Start(TimeSpan time);
    void Start(TimeSpan time, Action callBack);
    int Stop();
    private void Create()
    {
        Console.WriteLine("Created");
    }
}

