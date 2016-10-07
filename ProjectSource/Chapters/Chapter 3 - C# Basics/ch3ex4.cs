using System;
public class UnaryOperators
{

    public static void Main(string[] args)
    {

        Int a = 1; 
        Console.Out.WriteLine(“+a = “ +a);             // would output ‘+a=1’
        Console.Out.WriteLine(“-a = “ -a);               // would output ‘-a=-1’
        Console.Out.WriteLine(“!false=” ,!false);     // would output ‘!false = true’
        Console.Out.WriteLine(“~a” ,~a);                // b would equal 1, a would equal 2
        int b;
        b = ++a;                                                      // b would equal 1, a would equal 2
        b = --a;                                                        // b would equal 1, a would equal 0

    }

}
