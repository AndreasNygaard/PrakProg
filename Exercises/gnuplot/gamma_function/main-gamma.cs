using static math;
static class main{
static void Main(){
	double eps=1.0/32,dx=1.0/16;
	for(double x=-4+eps;x<=6+eps;x+=dx)
	System.Console.WriteLine("{0} {1}",x,math.gamma(x));
	for(double x=1;x<=8;x++)
	System.Console.Error.WriteLine("gamma : {0,5} {1,16:f8}",x,math.gamma(x));
}
}
