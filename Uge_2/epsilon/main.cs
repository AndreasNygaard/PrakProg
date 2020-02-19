using static System.Console;
using static System.Math;
class main{
	static int Main(){
//		CritInteger.MaxAndMin();
//		epsilon.Machine();
//		Harmonic.sum_float();
//		Harmonic.sum_double();
		bool bool_val=isequal.approx(2,2.000000002);
		Write($"{bool_val}\n");
	return 0;
	}
}

public class CritInteger{
        public static int MaxAndMin(){
                int i=1;
                while(i+1>i) {i++;} Write("my max int = {0}\n",i);
                Write($"Machine says max int is {int.MaxValue}\n");
                i=1;
                while(i-1<i) {i++;} Write("my min int = {0}\n",i);
                Write($"Machine says min int is {int.MinValue}\n");
        return 0;
        }
}

public class epsilon{
        public static int Machine(){
                double x=1; while(1+x!=1){x/=2;} x*=2;
                Write($"epsilon (double) is {x}\n");
                float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F;
                Write($"epsilon (float) is {y}\n");
        return 0;
        }
}

public class Harmonic{
        public static int sum_float(){
		int max=int.MaxValue/2;
		float float_sum_up=1.0F;
		for(int i=2;i<max;i++)float_sum_up+=1.0F/i;
		float float_sum_down=1.0F/max;
		for(int i=max-1;i>0;i--)float_sum_down+=1.0F/i;
		Write($"float_sum_up={float_sum_up}\n");
                Write($"float_sum_down={float_sum_down}\n");
        return 0;
        }
	public static int sum_double(){
                int max=int.MaxValue/2;
                double double_sum_up=1.0;
                for(int i=2;i<max;i++)double_sum_up+=1.0/i;
                double double_sum_down=1.0/max;
                for(int i=max-1;i>0;i--)double_sum_down+=1.0/i;
                Write($"double_sum_up={double_sum_up}\n");
                Write($"float_sum_down={double_sum_down}\n");
        return 0;
        }

}
