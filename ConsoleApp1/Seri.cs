namespace ConsoleApp1;

using System.Xml.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;

public class Seri
{
    public static void JsonSer(Person[] people)
    {
        var str = JsonSerializer.Serialize(people);
        File.AppendAllText("Peoples.json", str);

        var text = File.ReadAllText("Peoples.json");
        var arr = JsonSerializer.Deserialize<Person[]>(text);

        foreach (var item in arr)
        {
            Console.WriteLine(item.Name);
        }
    }

    public static void JsonSer(Person person)
    {
        var str = JsonSerializer.Serialize(person);
        File.AppendAllText("person.json", str);

        var text = File.ReadAllText("person.json");
        var pers = JsonSerializer.Deserialize<Person>(text);
        Console.WriteLine(pers.Name);
    }

    public static void XMLSer(Person[] people)
    {
        Person person = new Person();
        person.Name = "Vasya";
        person.LastName = "Pupkin";
        person.Age = 30;

        Person person1 = new Person();
        person1.Name = "Vasya";
        person1.LastName = "Pupkin";
        person1.Age = 30;

        Person person2 = new Person();
        person2.Name = "Vasya";
        person2.LastName = "Pupkin";
        person2.Age = 30;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person[]));
        using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, new[] { person, person1, person2 });

            Console.WriteLine("Object has been serialized");
        }

        XmlSerializer xmlDeSerializer = new XmlSerializer(typeof(Person[]));

        using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
        {
            Person[]? newpeople = xmlSerializer.Deserialize(fs) as Person[];

            if (newpeople != null)
            {
                foreach (Person p in newpeople)
                {
                    Console.WriteLine($"Name: {p.Name} --- Age: {p.Age}");
                }
            }
        }
    }


    public static void XMLSer(Person people)
    {
        Person person = new Person();
        person.Name = "Vasya";
        person.LastName = "Pupkin";
        person.Age = 30;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person[]));
        using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, new[] { person });

            Console.WriteLine("Object has been serialized");
        }
    }
}

