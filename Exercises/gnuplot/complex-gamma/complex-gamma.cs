using static System.Math;
using static cmath;
public static partial class math{

public static complex gamma(complex z){
/// single precision gamma function (Gergo Nemes, from Wikipedia)
	complex Pi = new complex(PI,0);
	if(z.Re<0)return Pi/cmath.sin(Pi*z)/gamma(new complex(1,0)-z);
	if(z.Re<9)return gamma(z+new complex(1,0))/z; // move argument up
	complex a = new complex(1,0);
	complex b = new complex(12,0);
	complex c = new complex(10,0);
	complex d = new complex(2,0);
	complex lngamma=z*cmath.log(z+a/(b*z-a/z/c))-z+cmath.log(d*Pi/z)/d;
	return cmath.exp(lngamma);
}

}//math
