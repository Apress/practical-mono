using System;
using System.Reflection;

public class Person
{
  private int age;
  public Person()
  {
    age = 0;
  }

  public Person(int age)
  {
    this.age = age;
  }

  public void WhatsMyAge()
  {
    Console.Out.WriteLine("My age is " + this.age.ToString());
  }
}

public class Test
{
  static void Main(string[] args)
  {
    Person p1 = new Person();
    p1.WhatsMyAge();
  }

}