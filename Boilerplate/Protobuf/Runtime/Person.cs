
using System.IO;
using ProtoBuf;

public class PersonSerializer
{

    public void Load()
    {
        Person newPerson;
        using (var file = File.OpenRead("person.bin"))
        {
            newPerson = Serializer.Deserialize<Person>(file);
        }
    }
    public void Save()
    {
        var person = new Person
        {
            Id = 12345,
            Name = "Fred",
            Address = new Address
            {
                Line1 = "Flat 1",
                Line2 = "The Meadows"
            }
        };
        using (var file = File.Create("person.bin"))
        {
            Serializer.Serialize(file, person);
        }
    }
}
