using System;
using System.Text;

public class Test
{
  public static void Main(string[] args)
  {

    StringBuilder example = new StringBuilder();
    for (int i=1; i<100000; i++)
    {
      example.Append("The ");
      example.Append("quick ");
      example.Append("brown ");
      example.Append("fox ");
      example.Append("jumped ");
      example.Append("over ");
      example.Append("the ");
      example.Append("lazy ");
      example.Append("dog ");
    }
  }
}