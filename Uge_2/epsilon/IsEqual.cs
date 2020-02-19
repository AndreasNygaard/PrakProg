using static System.Console;
using static System.Math;
public class isequal{
        public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
                bool bool_val;
                if (Abs(a-b)<tau) {bool_val=true;}
                else if (Abs(a-b)/(Abs(a)+Abs(b))<epsilon/2) {bool_val=true;}
                else {bool_val=false;}
        return bool_val;
        }
}
