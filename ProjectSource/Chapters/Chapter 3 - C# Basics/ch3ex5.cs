using System;

public class test
{
  public int i;
}

public class CastTest
{
  public static void Main(string[] args)
  {
    // Losing precision through casting
    double floating = 10.45;
    int casted_float = (int)floating;

  }

}