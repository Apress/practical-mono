using System;

public class Test
{
  public static void Main(string[] args)
  {
    string example="";
    for (int i=1; i<100000; i++)
    {
      example = "The ";
      example = example + "quick ";
      example = example + "brown ";
      example = example + "fox ";
      example = example + "jumped ";
      example = example + "over ";
      example = example + "the ";
      example = example + "lazy ";
      example = example + "dog ";
    }
  }
}