using static interp;
using static System.Math;
static class main{
static void Main(){
	int N=17;
	double x_min=-4;
	double x_max=4;
	double Dx=(x_max-x_min)/(N-1);
	vector x_tab = new vector(N);
	vector y_tab = new vector(N);
	for(int i=0;i<N;i++){
	x_tab[i]=x_min+i*Dx;
	y_tab[i]=Cos(x_tab[i]);
	}
	double z=3.1415;
	double integral = interp.cintegral(x_tab,y_tab,z);
	System.Console.WriteLine("integral from {0} to {1}= {2}",x_min,z,integral);
	double diff = interp.cdiff(x_tab,y_tab,z);
	System.Console.WriteLine("diff at x={0} = {1}",z,diff);
}
}
