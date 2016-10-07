struct MyStruct
{
  public int a;
}

public class Test
{
  public static void Main(string[] args)
  {
    MyStruct e;
    e.a=0;
    BoxIt(e);
  }

  public static void BoxIt(object val)
  {
    // Do Nothing, but val is now a Boxed version of the struct MyStruct
  }
}
