using System;
using System.Windows.Forms;

public class MyForm : Form
{
}

public class MyApplication
{

  public static void Main(string[] args)
  {
    Application.Run(new MyForm());	
  }
}