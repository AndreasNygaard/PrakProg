using static cmath;
using static math;
static class main{
static void Main(){
	double eps=1.0/128,d_re=1.0/64,d_im=1.0/64;
	for(double re=-4.5+eps;re<=4+eps;re+=d_re){
//	System.Console.WriteLine("\n");
	for(double im=-4.5;im<=4.5;im+=d_im){
	complex z = new complex(re,im);
	System.Console.WriteLine("{0} {1} {2}",re,im,cmath.abs(math.gamma(z)));
	}}

	complex zz = new complex(-1.6,0);
	System.Console.Error.WriteLine("gamma : {0,5} {1,16:f8}",zz,cmath.abs(math.gamma(zz)));
}
}
