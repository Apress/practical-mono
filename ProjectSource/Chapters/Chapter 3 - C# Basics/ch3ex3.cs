using System;

// Class encapsulating my name
class NoNamespace
{
    public string nme = "Mark Thomas Mamone";
}

// Class acting as the entry point for the executable
class Entry
{
    static void Main(string[] args)
    {
        NoNamespace def = new NoNamespace();
        Console.Out.WriteLine("nme[" + def.nme + "]");   
    }
}
