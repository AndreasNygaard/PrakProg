using System;
public class halton{
private readonly int dim;
private int n;

public halton(int dim){
        this.dim=dim;
        this.n=0;
        }
public void next(vector x){
	n++;
	int[] b = new int[]{2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67}; //dimension must be smaller than the size of this
	for(int i=0;i<dim;i++)x[i]=corput(n,b[i]);
        }

private double corput(int k, int b){
	double q=0,bk=1.0/b;
	while(k>0){
		q+=(k%b)*bk;
		k/=b;
		bk/=b;
		}
	return q;
	}

}
