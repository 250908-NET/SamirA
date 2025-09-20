/*
You do have the .NET SDK (9.0.305), so you’re set up for modern .NET.

But on modern .NET, csc is not on your PATH. That’s why you’re getting command not found: csc.
 Microsoft ships csc only as a DLL inside the SDK

 Easier shortcut (alias)

If you want to just type csc Hello.cs, add this to your ~/.zshrc:

alias csc='dotnet /usr/local/share/dotnet/sdk/9.0.305/Roslyn/bincore/csc.dll'

then reoad: 
source ~/.zshrc


*/

//Console.WriteLine("testing the command csc for quick csharp tests");

//Alternatively, create a class:

using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hi");
    }
}