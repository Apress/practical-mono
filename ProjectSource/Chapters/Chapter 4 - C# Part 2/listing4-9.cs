using System;

public class CopyrightAttribute : System.Attribute
{
  private string _copyrightString;

  public string Signature
  {
    get { return _copyrightString; }
  }

  public CopyrightAttribute(string copyrightString)
  {
    _copyrightString = copyrightString;
  }
}


class AttributeTest
{

  [Obsolete("This method has been deprecated, please use NewMethod() instead", true)]
  public void OldMethod()
  {
    // Does something
  }

  public void NewMethod()
  {
    // Does something
  }

}



class Test
{
  [Copyright("(C) 2005 Mark Mamone")]
  static void Main(string[] args)
  {
    AttributeTest t = new AttributeTest();
    t.NewMethod();


    Type t1 = typeof(t);

    Attribute[] attrs;
    Attribute.GetCustomAttributes(typeof(t1));
    foreach (Attribute a in attrs)
    {
      Console.Out.WriteLine((a as CopyrightAttribute).Signature);
    }
  }
}