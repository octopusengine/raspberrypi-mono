using System;

public class getSinus
{
    static public void Main(string[] args)
        {
        	//Console.WriteLine ("test arg > "+args[0].ToString()); //ok string
		double num=Int32.Parse(args[0]); //integer input
		double sin=Math.Sin(num/180*Math.PI);
		Console.WriteLine ("sinus >> "+sin.ToString()); //ok int
        }
}

/*
compile: 	mcs getSin.cs
run: 		./getSin.exe 30
return: 	sinus >> 0.5
*/

