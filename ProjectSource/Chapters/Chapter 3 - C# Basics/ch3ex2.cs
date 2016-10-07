using System;
class	markstest
{
    static void main(string[] args)
    {
        string nme = "Mark ";
        nme = nme + "Thomas ";
        nme = nme + "Mamone";

        int lgth = nme.Length;

        Console.Out.WriteLine("nme[" + nme[lgth-1] + "]");
    }
}
