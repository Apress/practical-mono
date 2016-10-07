using System;
public class UnaryOperators
{

    public static void Main(string[] args)
    {

        int a = 1; 
        Console.Out.WriteLine("+a = " + +a);  // would output ‘+a=1’
        Console.Out.WriteLine("-a = " + -a);  // would output ‘-a=-1’
        Console.Out.WriteLine("!false=" + !false);  // would output ‘!false = true’
        Console.Out.WriteLine("~a=" + ~a);  // would output '~a=-2'
        int b;
        b = ++a;  // b would equal 2, a would equal 2
        Console.Out.WriteLine("b=" + b);
        b = --a;  // b would equal 1, a would equal 0
        Console.Out.WriteLine("b=" + b);

    }

}
