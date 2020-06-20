using static System.Console;
using static System.Math;
class main{
	static int Main(){
		vector3d a=new vector3d(1,1,0);
		vector3d b=new vector3d(0,0,1);
		string str_a=a.ToString();
                string str_b=b.ToString();
                string str_b_minus_a=(b-a).ToString();
		string str_a_dot_b=(vector3d.dot_product(a,b)).ToString();
                string str_a_X_b=(vector3d.vector_product(a,b)).ToString();
                string str_mag_a=(vector3d.magnitude(a)).ToString();
		Write("a = "+str_a+"\n");
                Write("b = "+str_b+"\n");
		Write("b-a = "+str_b_minus_a+"\n");
		Write("a dot b = "+str_a_dot_b+"\n");
		Write("a X b = "+str_a_X_b+"\n");
		Write("magnitude(a) = "+str_mag_a+"\n");
	return 0;
	}
}
