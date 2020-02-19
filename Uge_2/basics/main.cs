using static System.Console;
class main{
	string s="oldstring\n";
	static int Main(){
		double x=1.23;
		int nmax=100;
		string s="hællåµ\n";
		Write(s);
		if(nmax>1){Write($"{nmax}>1\n");}
		else{Write($"{nmax}!>1\n");}

		int i=0;
		for(i=0;i<10;i++){Write($"i={i}\n");}

		i=0;
		while(i<10){Write($"While loop: i={i}\n");i++;}

		i=0;
		do {Write($"While loop: i(do)={i}\n");i++;}while(i<10);

	return 0;
	}
}


