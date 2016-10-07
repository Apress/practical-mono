using System;

class Dummy
{
  public void DivideBy(int val)
  {
    try
    {
      // If I pass a value of 1, throw a custom exception
      if (val == 1)
        throw new Exception("My Custom Exception");
 
      // Perform a division
      int aValue = 10 / val;		
      Console.Out.WriteLine("10 / " + val + " = " + aValue);
    }
    catch(DivideByZeroException e)
    {
      Console.Out.WriteLine("In Dummy.DivideBy() (Level 1): Exception [" + e.Message + "]");
    }
  }
}

class Test
{
  static void Main()
  {

    Dummy d = new Dummy();

    try
    {
      // Main Loop
      d.DivideBy(2);		// Should be OK
      d.DivideBy(0);		// Should throw a divide by zero exception, caught higher up in a custom
      d.DivideBy(1);		// Should Throw a Custom Exception but catch it within the Dummy Class
     
    }
    catch (Exception e)
    {
      Console.Out.WriteLine("In Main() (Level 0): Exception [" + e.Message + "]");
    }
  }

}