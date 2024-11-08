namespace ConsoleApp1;

public class Appartment
{
    private List<User> _users = new List<User>();
    public string Addres { get; private set; }

    public Appartment(string addres)
    {
         Addres = addres;
    }

    public void Add(User user)
    {
        _users.Add(user);
        user.Appartment = this;
    }

    public void Remove(User user)
    {
        if (_users.Contains(user))
        {
            _users.Remove(user);
        }
        user.Appartment = null;
    }

    public void Counts()
    {
        foreach (var user in _users)
        {
            user.SayHi($"Я тут живу {Addres}");
        }
    }
}

