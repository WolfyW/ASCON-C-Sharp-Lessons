namespace ConsoleApp1;

internal class Program
{
    static void Main(string[] args)
    {
        A a = new A();
        C c = new C();

        Print(a);
        Print(c);


        BanckAccount account1 = new BanckAccount("Vova");
        BanckAccount account2 = new BanckAccount("Anna");
        BanckAccount account3 = new BanckAccount("Natasha");

        account1.AmountChanged += Account_AmountChanged;
        account2.AmountChanged += Account_AmountChanged;
        account3.AmountChanged += Account_AmountChanged;

        account1.AddMoney(300000);
        account2.AddMoney(300000);
        account3.AddMoney(300000);

    }

    private static void Print(A a)
    {
        a.B();
    }

    private static void Account_AmountChanged(object? sender, AmountChangedArgs e)
    {
        BanckAccount b = sender as BanckAccount;
        Console.WriteLine($"{b.Name}: {e.Operation} with sum {e.Value}");
    }
}

delegate double GetData();

enum TypeOperation
{
    withdraw,
    add,
    Denien
}

class AmountChangedArgs : EventArgs
{
    public decimal Value { get; set; }
    public TypeOperation Operation { get; set; }
}

class BanckAccount
{
    public string Name { get; private set; } 
    public decimal Amount { get; private set; }

    public event EventHandler<AmountChangedArgs> AmountChanged;


    public BanckAccount(string name)
    {
        Name = name;
    }

    public void WithDraw(decimal amount)
    {
        AmountChanged?.Invoke(this, new AmountChangedArgs()
        {
            Operation = TypeOperation.withdraw,
            Value = amount
        });
    }

    public void AddMoney(decimal amount)
    {
        AmountChanged?.Invoke(this, new AmountChangedArgs()
        {
            Operation = TypeOperation.add,
            Value = amount
        });
    }
}

abstract class N
{
    public abstract string GetName();
}
class A : N
{
    public override string GetName()
    {
        return "My name is A";
    }

    public virtual void B()
    {
        Console.WriteLine("Call from A");
    }
}

class C : A
{
    public override string GetName()
    {
        return "My name is C";
    }

    public override void B()
    {
        Console.WriteLine("Call from C");
    }
}