using System;
public class corput{
private int n,b;
public corput(int b){
	this.b=b;
	this.n=0;
	}
public double next(){
	double q=0,bk=1.0/b;
	int k=++n;
	while(k>0){q+=(k%b)*bk;k/=b;bk/=b;}
	return q;
	}
}
