using System;
using System.Windows.Forms;

public class MyForm : Form
{
  public MyForm()
  {
    this.Text = "RSS Aggregator";
    this.Width = 800;
    this.Height = 600;
  }
}

public class MyApplication
{

  public static void Main(string[] args)
  {
    Application.Run(new MyForm());	
  }
}