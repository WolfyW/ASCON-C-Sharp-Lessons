namespace ConsoleApp1;

public class User
{
    public User UserParrent { get; set; }
    public List<User> Children { get; set; }

    public string Name { get; set; }
    public string FamilyName { get; set; }
    public string Email { get; set; }
    public string BithDate { get; set; }

    public Appartment Appartment { get; set; }

    public void SayHi(string message)
    {
        Console.WriteLine($"{Name} говорит: {message}");
    }
}

