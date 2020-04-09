using static interp;
using static System.Math;
static class main{
static void Main(){
	int N=50;
	double x_min=-4;
	double x_max=4;
	double Dx=(x_max-x_min)/(N-1);
	vector x_tab = new vector(N);
	vector y_tab = new vector(N);
	for(int i=0;i<N;i++){
	x_tab[i]=x_min+i*Dx;
	y_tab[i]=Cos(x_tab[i]);
	}
	double integral = interp.lintegral(x_tab,y_tab,3.5);
	System.Console.WriteLine("integral = {0}",integral);
}
}
