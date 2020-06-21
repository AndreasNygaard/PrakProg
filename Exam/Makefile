# The 'make' command takes ~10 minutes because the data needs to be significant
# in order to identify the behavior of the convergence rate

all: report.pdf Convergence1D.png Convergence2D.png Convergence5D.png Sample.png output.txt

report.pdf: report.tex Convergence1D.png Convergence2D.png Convergence5D.png Sample.png
	pdflatex $<
	pdflatex $<

Convergence1D.png: data.txt Makefile
	rm -f fit.log
	echo '\
	set term pngcairo enhanced size 1024,756 background "white" font "Times,18";\
	set out "$@";\
	set key out;\
	set key center bottom;\
	set tics out;\
	set format y "10^{%L}";\
	set format x "10^{%L}";\
	set log x;\
	set log y;\
	set title "Error as a function of N - 1D function f(x)";\
	set xlabel "N";\
	set ylabel "Error";\
	set size 1.0,1.0;\
	f(x) = a/sqrt(x)+b;\
	fit f(x) "$<" index 0 using 1:2:2 via a,b;\
	g(x) = 0.2*1/x;\
	g1(x) = 0.2*0.21714724*log(x)/x;\
	plot [100:10000000][]\
	 "$<" index 0 using 1:2 with lines lw 3 lt rgb "cyan" title "Pseudo-random estimated error" \
	,"$<" index 0 using 1:3 with lines lw 3 lt rgb "red" title "Pseudo-random actual error" \
	,f(x) with lines lw 3 dt 3 lt rgb "black" title "O(1/√N)"\
	,"$<" index 1 using 1:2 with lines lw 3 lt rgb "magenta" title "Quasi-random estimated error" \
	,"$<" index 1 using 1:3 with lines lw 3 lt rgb "blue" title "Quasi-random actual error" \
	,g(x) with lines lw 3 dt 2 lt rgb "black" title "O(1/N)"\
	,g1(x) with lines lw 3 dt 4 lt rgb "black" title "O(log(N)/N)";\
	'| gnuplot

Convergence2D.png: data.txt Makefile
	rm -f fit.log
	echo '\
	set term pngcairo enhanced size 1024,756 background "white" font "Times,18";\
	set out "$@";\
	set key out;\
	set key center bottom;\
	set tics out;\
	set format y "10^{%L}";\
	set format x "10^{%L}";\
	set log x;\
	set log y;\
	set title "Error as a function of N - 2D function f(x,y)";\
	set xlabel "N";\
	set ylabel "Error";\
	set size 1.0,1.0;\
	f(x) = a/sqrt(x)+b;\
	fit f(x) "$<" index 2 using 1:2:2 via a,b;\
	g(x) = 80000/x;\
	g1(x) = 17371.78*log(x)/x;\
	g2(x) = 3772.234*log(x)**2/x;\
	g3(x) = 819.13*log(x)**3/x;\
	plot [100:10000000][]\
	 "$<" index 2 using 1:2 with lines lw 3 lt rgb "cyan" title "Pseudo-random estimated error" \
	,"$<" index 2 using 1:3 with lines lw 3 lt rgb "red" title "Pseudo-random actual error" \
	,f(x) with lines lw 3 dt 3 lt rgb "black" title "O(1/√N)"\
	,"$<" index 3 using 1:2 with lines lw 3 lt rgb "magenta" title "Quasi-random estimated error" \
	,"$<" index 3 using 1:3 with lines lw 3 lt rgb "blue" title "Quasi-random actual error" \
	,g(x) with lines lw 3 dt 2 lt rgb "black" title "O(1/N)"\
	,g1(x) with lines lw 3 dt 2 lt rgb "grey" title "O(log(N)/N)"\
	,g2(x) with lines lw 3 dt 4 lt rgb "black" title "O(log(N)^2/N)";\
	'| gnuplot

Convergence5D.png: data.txt Makefile
	rm -f fit.log
	echo '\
	set term pngcairo enhanced size 1524,880 background "white" font "Times,18";\
	set out "$@";\
	set multiplot layout 1,2 title "Error as a function of N - 5D function f(x,y,z,p,q)";\
	set key out;\
	set key center bottom;\
	set tics out;\
	set format y "10^{%L}";\
	set format x "10^{%L}";\
	set log x;\
	set log y;\
	set xlabel "N";\
	set ylabel "Error";\
	f(x) = a/sqrt(x)+b;\
	fit f(x) "$<" index 4 using 1:2:2 via a,b;\
	g(x) = 1.14*80000/187.5/x;\
	g1(x) = 1.14*17371.78/187.5*log(x)/x;\
	g2(x) = 1.14*3772.234/187.5*log(x)**2/x;\
	g3(x) = 1.14*819.13/187.5*log(x)**3/x;\
	g4(x) = 1.14*177.87/187.5*log(x)**4/x;\
	g5(x) = 1.14*38.6244/187.5*log(x)**5/x;\
	set rmargin 0;\
	set size 0.63,0.9;\
	set origin 0.0,0.0;\
	set style rect back fs empty border lc rgb "black";\
	set object 1 rect from 100,0.03 to 10000,20 lw 2;\
	set arrow 1 from screen 0.267,0.664 to screen 0.726,0.098 lw 2 nohead;\
	set arrow 2 from screen 0.267,0.832 to screen 0.726,0.873 lw 2 nohead;\
	plot [100:40000000][0.000001:100]\
	 "$<" index 4 using 1:2 with lines lw 3 lt rgb "cyan" title "Pseudo-random estimated error" \
	,"$<" index 4 using 1:3 with lines lw 3 lt rgb "red" title "Pseudo-random actual error" \
	,f(x) with lines lw 3 dt 3 lt rgb "black" title "O(1/√N)"\
	,"$<" index 5 using 1:2 with lines lw 3 lt rgb "magenta" title "Quasi-random estimated error" \
	,"$<" index 5 using 1:3 with lines lw 3 lt rgb "blue" title "Quasi-random actual error" \
	,g(x) with lines lw 3 dt 2 lt rgb "black" title "O(1/N)"\
	,g1(x) with lines lw 3 dt 4 lt rgb "grey" title "O(log(N)/N)"\
	,g2(x) with lines lw 3 dt 2 lt rgb "grey" title "O(log(N)^2/N)"\
	,g3(x) with lines lw 3 dt 3 lt rgb "grey" title "O(log(N)^3/N)"\
	,g4(x) with lines lw 3 dt 5 lt rgb "grey" title "O(log(N)^4/N)"\
	,g5(x) with lines lw 3 dt 4 lt rgb "black" title "O(log(N)^5/N)";\
	set key at 0.2,0.0;\
	show key;\
	set rmargin;\
	set size 0.33,0.9;\
	set origin 0.66,0.0;\
	plot [100:10000][0.03:20]\
	 "$<" index 5 using 1:2 with lines lw 3 lt rgb "magenta" title "Quasi-random estimated error" \
	,"$<" index 5 using 1:3 with lines lw 3 lt rgb "blue" title "Quasi-random actual error" \
	,g(x) with lines lw 3 dt 2 lt rgb "black" title "O(1/N)"\
	,g1(x) with lines lw 3 dt 4 lt rgb "grey" title "O(log(N)/N)"\
	,g2(x) with lines lw 3 dt 2 lt rgb "grey" title "O(log(N)^2/N)"\
	,g3(x) with lines lw 3 dt 3 lt rgb "grey" title "O(log(N)^3/N)"\
	,g4(x) with lines lw 3 dt 5 lt rgb "grey" title "O(log(N)^4/N)"\
	,g5(x) with lines lw 3 dt 4 lt rgb "black" title "O(log(N)^5/N)";\
	show arrow;\
	unset multiplot;\
	'| gnuplot


# When using Ariel as font
#set arrow 1 from screen 0.271,0.685 to screen 0.732,0.113 lw 2 nohead;\
#set arrow 2 from screen 0.271,0.833 to screen 0.732,0.869 lw 2 nohead;\


Sample.png: sample_points.txt Makefile
	echo '\
	set term pngcairo enhanced size 1500,500 background "white" font "Times,18";\
	set out "$@";\
	set multiplot layout 1,3 title "Sampling Points of Pseudo- and Quasi-Random Sampling";\
	set size ratio 1;\
	set key out;\
	set tics out;\
	set xlabel "x";\
	set ylabel "y";\
	set xtics 0.5;\
	set ytics 0.5;\
	set grid;\
	set key center bottom;\
	set rmargin 0;\
	plot "$<" index 0 using 1:2 with points ps 0.15 pt 7 lt rgb "black" title "Pseudo-random sampling";\
	set key center bottom;\
        set rmargin;\
	plot "$<" index 1 using 1:2 with points ps 0.15 pt 7 lt rgb "black" title "Lattice sampling";\
	set key center bottom;\
	set rmargin;\
	plot "$<" index 2 using 1:2 with points ps 0.15 pt 7 lt rgb "black" title "Halton sampling";\
	unset multiplot;\
	'|gnuplot

output.txt data.txt sample_points.txt: main.exe
	rm -f sample_points.txt
	mono $< > output.txt 2>data.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs mc.cs lattice.cs halton.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt *.log *.png *.out *.aux *.pdf

