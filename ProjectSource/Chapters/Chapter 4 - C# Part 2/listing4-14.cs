using System;
using System.Collections.Generic;

public class MyCollection
{

  string name = "Mark";
  string surname = "Mamone";
  string age = "36";

  public IEnumerator<string> GetEnumerator()
  {
    yield return name;
    yield return surname;
    yield return age;
  }

}

class Test
{
  static void Main(string[] args)
  {

    MyCollection m = new MyCollection();
    foreach (string s in m)
    {
      Console.Out.WriteLine(s);
    }
  
  }
}
