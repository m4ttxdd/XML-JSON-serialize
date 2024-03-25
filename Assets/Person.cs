using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Person
{
    public int age;
    public string name;
    public string favColor;

    public Person(int age, string name, string favColor)
    {
        this.age = age;
        this.name = name;
        this.favColor = favColor;
    }
}

[Serializable]
public class Persons
{
    public List<Person> people;
}