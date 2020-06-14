all: Energy.svg S-wave.svg Energy_improve.svg S-wave_improve.svg output.txt

Energy.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key right;\
	set tics out;\
	set title "Obtained energy as function of r_{max}";\
	set xlabel "r_{max} [a_{Bohr}]";\
	set ylabel "E [Hatree]";\
	plot [0:11][-0.6:0]\
	 "$<" index 4 with points pt 2 notitle \
	,-0.5 with lines lt 0 lw 4 dt 7 title "exact"\
	'| gnuplot

Energy_improve.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key right;\
	set tics out;\
	set title "Obtained energy as function of r_{max} with better boundary condition";\
	set xlabel "r_{max} [a_{Bohr}]";\
	set ylabel "E [Hatree]";\
	plot [0:6][]\
	 "$<" index 9 with points pt 2 notitle \
	,-0.5 with lines lt 0 lw 4 dt 7 title "exact"\
	'| gnuplot

S-wave.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key right;\
	set tics out;\
	set title "Hydrogen s-wave shooting to F_E(r_{max})=0";\
	set xlabel "r [a_{Bohr}]";\
	set ylabel "F_E(r)";\
	f(x) = x*exp(-x);\
	plot [0:11][]\
	 "$<" index 3 with lines lw 6 title "r_{max}=10" \
	,"$<" index 2 with lines lw 6 title "r_{max}=5" \
	,"$<" index 1 with lines lw 6 title "r_{max}=3" \
	,"$<" index 0 with lines lw 6 title "r_{max}=2" \
	,f(x) title "exact" with lines linestyle 0 dt 2 lw 4; \
	'| gnuplot

S-wave_improve.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key right;\
	set tics out;\
	set title "Hydrogen s-wave shooting to F_E(r_{max})=0 with better boundary condition";\
	set xlabel "r [a_{Bohr}]";\
	set ylabel "F_E(r)";\
	f(x) = x*exp(-x);\
	plot [0:11][]\
	 "$<" index 8 with lines lw 6 title "r_{max}=5" \
	,"$<" index 7 with lines lw 6 title "r_{max}=3" \
	,"$<" index 6 with lines lw 6 title "r_{max}=2" \
	,"$<" index 5 with lines lw 6 title "r_{max}=1" \
	,f(x) title "exact" with lines linestyle 0 dt 2 lw 4; \
	'| gnuplot

output.txt data.txt: main.exe
	mono $< > output.txt 2>data.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs ../Linear-Equations/qrDecomp.cs ../ODE/rk23.cs rootfinding.cs schroedinger.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt

