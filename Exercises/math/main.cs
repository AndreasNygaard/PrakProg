using System;
using static System.Console;
using static System.Math;
using static cmath;
class main{
	static int Main(){
/*
	double x=1;
	char c='ø';
	string s="hello";
        int i=100;

	Write("sin({0})={1}\n",x,Sin(x));
	Write($"sin({x})={Sin(x)}\n");
	Write($"i={i}\n");
	double y=x*Exp(x);
	Write($"y={y} E={E}\n");
*/
	complex I = new complex(0,1);
	Write($"i^2={(I*I).Re}\n");
	Write($"√2={Sqrt(2)}\n");
	Write($"exp(i)={exp(I).Re} + i·({exp(I).Im})\n");
	Write($"exp(iπ)={exp(I*PI).Re} + i·({exp(I*PI).Im})\n");
	Write($"sin(iπ)={sin(I*PI).Re} + i·({sin(I*PI).Im})\n");
	Write($"I.pow(I)={I.pow(I).Re} + i·({I.pow(I).Im})\n");

	return 0;
	}
}
