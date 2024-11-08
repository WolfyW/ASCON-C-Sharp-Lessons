namespace ClassLibrary1;

public class OtherClass
{
    private SomeClass _f;
    
    internal OtherClass(SomeClass f)
    {
        _f = f;
    }

    public void DoSomthing(string message)
    {
        // Записываем в поле с защитой на запсь internal
        _f.PublicProperty = message;

        // Выводим на экран содержимое поля internal
        Console.WriteLine(_f.InternalProperty);

        //Ошибка нет доступа на запись
        //_f.InternalProperty = "data"
    }
}
