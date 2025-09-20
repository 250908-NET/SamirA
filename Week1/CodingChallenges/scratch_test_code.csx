
/*
use dotnet script <name_of_file>.csx to run simple scripts without having a project (only works with .csx files)
must install script
can also do csc nameOfFile.cs then ./nameOfFile.exe for linux/mac or nameOfFile/exe for windows (note .cs not .csx here)
must install csc
*/

Console.WriteLine("Hi");
var str1 = "     Hi there how are you?      ";
var str2 = str1.Trim();
Console.WriteLine(str1[0..]);
Console.WriteLine(str2[4..9]);
Console.WriteLine(str1.Trim());
Console.WriteLine(str1.Trim().IndexOf('o'));
Console.WriteLine(str2.IndexOf('r'));


var str3 = "abcdef";
//other ways to print substrings: 
Console.WriteLine(str3.Substring(2));// prints index 2 and up
Console.WriteLine(str3.Substring(2,3));// prints index 2, 3, and 4 (the second param 3 is num letters)

