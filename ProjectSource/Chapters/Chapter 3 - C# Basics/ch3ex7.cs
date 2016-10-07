using System;
/*
public class test_class
{
  int i;

  void something() {}
}

public struct test_struct
{
  int i;
  void something() {}
}
*/

public class CastTest
{
  public static void Main(string[] args)
  {

    test_class a = new test_class();
    test_struct b = new test_struct();

    a.i = 1;
    a.something();

    b.i = 1;
    b.something();


  }

}