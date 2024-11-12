namespace ConsoleApp1;

class Test
{
    public int A { get; private set; } = 0;
    public event EventHandler<int> Changing;
     public void DoSomething(Action<int> action)
    {
        action?.Invoke(12);
    }
    public void Increment(Action a)
    {
        a?.Invoke();
    }
    public Action GetAction()
    {
        return () =>
        {
            Changing?.Invoke(this, A);
            A++;
        };
    }
}

