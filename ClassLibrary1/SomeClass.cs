namespace ClassLibrary1;

public class SomeClass
{
    public string PublicProperty { get; internal set; }
    internal string InternalProperty { get; private set; }
    private string PrivateProperty { get; set; }

    public void Get()
    {
        PrivateProperty = "Some private data";
        InternalProperty = "Some Internal Data";
        PublicProperty = "Some Public data";
        Utils.Print("Print by interanl method", ConsoleColor.Blue);
    }
}
