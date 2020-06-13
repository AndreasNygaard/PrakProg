all: output_A.txt output_B.txt A_eig_vec.svg A_eig_val.svg B_time.svg B_rot.svg

A_eig_val.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key left;\
	set tics out;\
	set title "Quantum particle in a box: eigenvalues";\
	set xlabel "n";\
	set ylabel "√E_n";\
	plot \
	 "$<" index 0 u 1:(sqrt($$2)) with lines title "calculated"\
	,"$<" index 0 u 1:(sqrt($$3)) with lines title "exact"\
	'| gnuplot

A_eig_vec.svg: data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key left;\
	set title "Quantum particle in a box: eigenfunctions";\
	set xlabel "x";\
	set ylabel "Psi_n(x)";\
	plot \
	 "$<" index 1 with lines notitle\
	,sqrt(2) with lines notitle\
	,-sqrt(2) with lines notitle\
	'| gnuplot

#B_rot_data.txt Makefile
B_rot.svg: rot_c_data.txt rot_1_data.txt rot_v_data.txt Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set title "Rotations: cyclic vs 1value";\
	set xlabel "matrix size n";\
	set ylabel "number of rotations";\
	set key right;\
	set log x;\
	set log y;\
	plot [][]\
	 "rot_c_data.txt" using 1:3 with linespoints pt 5 title "cyclic" \
	,"rot_1_data.txt" using 1:3 with linespoints pt 7 title "1value" \
	,"rot_v_data.txt" using 1:3 with linespoints pt 9 title "1by1" \
	'| gnuplot
#        "$<" index 0 using 1:3 with linespoints pt 5 title "cyclic" \
#       ,"$<" index 1 using 1:3 with linespoints pt 7 title "1value" \
#       ,"$<" index 2 using 1:3 with linespoints pt 9 title "1by1" \



#B_time_data.txt Makefile
B_time.svg: time_c_data.txt time_1_data.txt time_v_data.txt Makefile
	echo '\
	set term svg size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set key top left;\
	set log x;\
	set log y;\
	set title "matrix diagonalization times: cyclic vs 1value vs 1by1";\
	set xlabel "matrix size n";\
	set ylabel "diagonalization time t, sec";\
	bc=1; ac=90; cc=3;\
	bv=1; av=90; cv=3;\
	b1=1; a1=90; c1=3;\
	fc(x)=bc+(x/ac)**cc;\
	f1(x)=b1+(x/a1)**c1;\
	fv(x)=bv+(x/av)**cv;\
	fit fc(x) "time_c_data.txt" via ac,bc;\
	fit f1(x) "time_1_data.txt" via a1,b1;\
	fit fv(x) "time_v_data.txt" via av,bv;\
	plot [20:1000][0.01:10]\
	 "time_c_data.txt" pointtype 9 notitle \
	,"time_1_data.txt" pointtype 5 notitle \
	,"time_v_data.txt" pointtype 7 notitle \
	,x>150?1/0:fv(x) t sprintf("1by1: (n/%.f)^{%.2f}+%.3f",av,cv,bv) \
	,x>200?1/0:fc(x) title sprintf("cyclic: (n/%.f)^{%.2f}+%.3f",ac,cc,bc) \
	,x<100?1/0:f1(x) t sprintf("1value: (n/%.f)^{%.2f}+%.3f",a1,c1,b1) \
	'| gnuplot
#       fit fc(x) "$<" index 0 via ac,bc;\
#       fit f1(x) "$<" index 1 via a1,b1;\
#       fit fv(x) "$<" index 2 via av,bv;\
#        "$<" index 0 pointtype 9 notitle \
#       ,"$<" index 1 pointtype 5 notitle \
#       ,"$<" index 2 pointtype 7 notitle \


output_A.txt: main_A.exe
	mono $< > output_A.txt 2> data.txt

output_B.txt: main_B.exe
	mono $< > output_B.txt

#B_rot_data.txt B_time_data.txt: B_rot_time.exe
#	mono $< > B_rot_data.txt 2> B_time_data.txt

rot_c_data.txt time_c_data.txt: test.exe
	for N in `seq 35 5 200`; do \
	echo "N=$$N";\
	mono test.exe $$N cyclic >> rot_c_data.txt 2>> time_c_data.txt; \
	done

rot_1_data.txt: test.exe
	for N in `seq 100 5 140`; do \
	echo "N=$$N";\
	mono test.exe $$N values >> rot_1_data.txt 2>> trash.txt; \
	done
	rm -f trash.txt

time_1_data.txt: test.exe
	for N in `seq 400 50 700`; do \
	echo "N=$$N";\
	mono test.exe $$N values >> trash.txt 2>> time_1_data.txt; \
	done
	rm -f trash.txt

rot_v_data.txt time_v_data.txt: test.exe
	for N in `seq 33 5 73`; do \
	echo "N=$$N";\
	mono test.exe $$N values $$N >> rot_v_data.txt 2>> time_v_data.txt; \
	done

main_A.exe: main_A.cs math.dll
	mcs -r:math.dll $<

main_B.exe: main_B.cs math.dll
	mcs -r:math.dll $<

test.exe: test.cs math.dll
	mcs -r:math.dll $<

#B_rot_time.exe: B_rot_time.cs math.dll
#	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs diag.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt *.svg

