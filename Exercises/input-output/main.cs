using System;
using static System.Console;
using static System.Math;
using System.Diagnostics;
class main{
public static void Main(string[] args){
	int n = args.Length;
	double x;
	WriteLine("x\t\tcos(x)\t\tsin(x)\n");
	for(int i=0;i<n;i++){
		x = double.Parse(args[i]);
		WriteLine($"{DoFormat(x)}\t\t{Sin(x),1:f4}\t\t{Cos(x),1:f4}");
		}
	}

public static string DoFormat(double myNumber){
	var s = string.Format("{0:f4}", myNumber);
	if(s.EndsWith("0000")){
		return ((int)myNumber).ToString();
		}
	if(s.EndsWith("000")){
		var str = string.Format("{0:f1}", myNumber);
		return str;
		}
	if(s.EndsWith("00")){
		var str = string.Format("{0:f2}", myNumber);
		return str;
		}
	if(s.EndsWith("0")){
		var str = string.Format("{0:f3}", myNumber);
		return str;
		}
	else{
		return s;
		}
	}

}
