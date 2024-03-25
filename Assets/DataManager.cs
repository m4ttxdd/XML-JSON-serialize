using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

//[XmlRoot("Persons")]
//public class Persons
//{
//    [XmlElement("Person")]
//    public List<Person> people { get; set; }
//}

//public class Person
//{
//    [XmlElement("age")]
//    public string age { get; set; }

//    [XmlElement("name")]
//    public string name { get; set; }

//    [XmlElement("favColor")]
//    public string favColor { get; set; }
//}


public class DataManager : MonoBehaviour
{

    private List<Person> people = new List<Person>()
    {
        new Person { age = 25, name = "John", favColor = "Blue" },
        new Person { age = 30, name = "Jane", favColor = "Red" },
        new Person { age = 35, name = "Joe", favColor = "Green" },
        new Person { age = 40, name = "Jill", favColor = "Yellow" },
        new Person { age = 45, name = "Jack", favColor = "Orange" },
        new Person { age = 50, name = "Jen", favColor = "Purple" },
        new Person { age = 55, name = "Jim", favColor = "Black" }
    };

    private List<Person> peopleFromXML = new List<Person>();

    // Start is called before the first frame update
    void Start()
    {
        NewDirectory();

        //WriteToXML(Application.persistentDataPath + "/Player_Data/" + "Progress_Data.xml");

        //ReadXML(Application.persistentDataPath + "/Player_Data/" + "Progress_Data.xml");

        SerializeXML();

        DeserializeXML();

        SerializeJSON2();

        //DeserializeJSON();
    }


    public void NewDirectory()
    {
        if (Directory.Exists(Application.persistentDataPath + "/Player_Data/"))
        {
            Debug.Log("Directory already exists...");
            return;
        }

        Directory.CreateDirectory(Application.persistentDataPath + "/Player_Data/");
        Debug.Log("New directory created!");
    }

    //public void WriteToXML(string filename)
    //{
    //    if (!File.Exists(filename))
    //    {
    //        using (FileStream xmlStream = File.Create(filename))
    //        {
    //            using (XmlWriter xmlWriter = XmlWriter.Create(xmlStream))
    //            {
    //                xmlWriter.WriteStartDocument();
    //                xmlWriter.WriteStartElement("Persons"); // Start of root element

    //                foreach (Person person in people)
    //                {
    //                    WriteElement(person.age, person.name, person.favColor, xmlWriter);
    //                }

    //                xmlWriter.WriteEndElement(); // End of root element
    //                xmlWriter.WriteEndDocument();
    //            }
    //        }
    //    }
    //    else Debug.Log("no write");
    //}

    //private void WriteElement(int age, string name, string favColor, XmlWriter xmlWriter)
    //{
    //    xmlWriter.WriteStartElement("Person");

    //    xmlWriter.WriteElementString("age", age.ToString());
    //    xmlWriter.WriteElementString("name", name);
    //    xmlWriter.WriteElementString("favColor", favColor);

    //    xmlWriter.WriteEndElement();
    //}



    //public void ReadXML(string filename) //fuck u
    //{
    //    if (!File.Exists(filename))
    //    {
    //        Debug.Log("File doesn't exist...");
    //        return;
    //    }

    //    XmlSerializer serializer = new XmlSerializer(typeof(Persons));
    //    using (FileStream stream = File.OpenRead(filename))
    //    {
    //        var persons = (Persons)serializer.Deserialize(stream);
    //        foreach (var person in persons.people)
    //        {
    //            Debug.Log($"name: {person.name}, age: {person.age}, Favorite Color: {person.favColor}");
    //            //people.Add(person);
    //        }
    //    }
    //}

    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Person>));

        using (FileStream stream = File.Create(Application.persistentDataPath + "/Player_Data/" + "personsPerson.xml"))
        {
            xmlSerializer.Serialize(stream, people);
        }
    }

    public void DeserializeXML()
    {
        if (File.Exists(Application.persistentDataPath + "/Player_Data/" + "personsPerson.xml"))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Person>));

            using (FileStream stream = File.OpenRead(Application.persistentDataPath + "/Player_Data/" + "personsPerson.xml"))
            {
                var persons = (List<Person>)xmlSerializer.Deserialize(stream);
                foreach (var person in persons)
                {
                    Debug.LogFormat("name: {0} - age: {1} - favColor: {2}", person.name, person.age, person.favColor);
                    peopleFromXML.Add(new Person(person.age, person.name, person.favColor)); //saves data from XML to a new list because of assignment requirements
                }
            }
        }
    }


    //public void SerializeJSON()
    //{
    //    Persons person = new Persons();
    //    Debug.Log("count" + people.Count);
    //    person.people = people;

    //    string jsonString = JsonUtility.ToJson(person, true);
    //    string jsonFilePath = Application.persistentDataPath + "/Player_Data/" + "personJSON.json";

    //    Debug.Log("Writing JSON to: " + jsonFilePath);
    //    using (StreamWriter stream = File.CreateText(Application.persistentDataPath + "/Player_Data/" + "personJSON.json"))
    //    {
    //        stream.WriteLine(jsonString);
    //    }

    //    Debug.Log("JSON file written.");
    //}

    public void SerializeJSON2()
    {
        Persons persons = new Persons();
        persons.people = peopleFromXML;//uses the saved data from XML

        string jsonString = JsonUtility.ToJson(persons, true);
        using (StreamWriter stream = File.CreateText(Application.persistentDataPath + "/Player_Data/" + "personJSON.json"))
        {
            stream.WriteLine(jsonString);
        }
    }

    public void DeserializeJSON()
    {
        if (File.Exists(Application.persistentDataPath + "/Player_Data/" + "personJSON.json"))
        {
            using (StreamReader stream = new StreamReader(Application.persistentDataPath + "/Player_Data/" + "personJSON.json"))
            {
                var jsonString = stream.ReadToEnd();
                var personData = JsonUtility.FromJson<Persons>(jsonString);

                foreach (var person in personData.people)
                {
                    Debug.LogFormat("name: {0} - age: {1} - favColor: {2}", person.name, person.age, person.favColor);
                }
            }
        }
    }


}


