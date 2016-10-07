using System;

public class test
{
  public int i;
}

public class CastTest
{
  public static void Main(string[] args)
  {
    int nme = 10;
    int cpy = 10;

    // Same value...
    Console.Out.WriteLine("nme before change="+nme);
    Console.Out.WriteLine("cpy before change="+cpy);

    nme = 15;

    // ...Still the same value
    Console.Out.WriteLine("nme after change="+nme);
    Console.Out.WriteLine("cpy after change="+cpy);

  }

}