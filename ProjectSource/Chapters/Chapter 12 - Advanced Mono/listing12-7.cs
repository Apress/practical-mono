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

    Type obj = p1.GetType();
    ConstructorInfo [] info = obj.GetConstructors();
    foreach( ConstructorInfo cf in info )
    {
      Console.WriteLine(cf);
    }
  }

}