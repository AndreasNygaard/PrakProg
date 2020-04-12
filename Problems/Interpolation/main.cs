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
	System.Console.Error.WriteLine("{0} {1}",x_tab[i],y_tab[i]);
	}
        double dx=1.0/32;
	for(double x=x_min;x<=x_max;x+=dx)
	System.Console.WriteLine("{0} {1}",x,interp.cinterp(x_tab,y_tab,x));
}
}
