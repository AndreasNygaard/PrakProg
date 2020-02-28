using static System.Math;
public static partial class math{

public static double ln_gamma(double x){
/// single precision gamma function (Gergo Nemes, from Wikipedia)
	if(x<0)return Log(Abs(PI/Sin(PI*x)/Exp(ln_gamma(1-x))));
	if(x<9)return Log(Exp(ln_gamma(x+1))/x); // move argument up
	double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
	return lngamma;
}

}//math
