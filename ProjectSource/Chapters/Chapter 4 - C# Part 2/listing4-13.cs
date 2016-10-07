using System;

public static class MathematicsHelper
{
  // Simple method to return highest value
  // of two value passed
  public static int returnHigher(int a, int b)
  {
    if (a>b)
      return a;
    else
      return b;
  }
}

class Test
{
  static void Main(string[] args)
  {

    // The next line will not compile, comment
    // it out to test the next bit
    MathematicsHelper a = new MathematicsHelper();	

    // This next block will work
    int b = 10;
    int c = 20;
    int highest = MathematicsHelper.returnHigher(b,c);
    Console.Out.WriteLine("Highest between {0} and {1} is {2}.", b,c,highest);
  
  }
}