MCS = mcs -optimize+ -platform:arm
MONO = mono -O=all #-O=all,-shared #--optimize=unsafe,loop,inline #--gc=sgen --llvm --optimize=all

comma:=,
empty:=
space:=$(empty) $(empty)
commalist = $(subst $(space),$(comma),$(1))

all: B.out.txt B.compare-times.svg B.check-energies.svg B.compare-rotations.svg

TC=out.t.cyclic
EC=out.e.cyclic
T1=out.t.1value
E1=out.e.1value
TV=out.t.values
EV=out.e.values

B.compare-rotations.svg:$(EC) $(E1) $(EV) Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set title "Rotations: cyclic vs 1value";\
	set xlabel "matrix size n";\
	set ylabel "number of rotations";\
	set key left;\
	set log x;\
	set log y;\
	plot [][]\
	 "$(EC)" using 1:3 with linespoints pt 5 title "cyclic" \
	,"$(E1)" using 1:3 with linespoints pt 7 title "1value" \
	,"$(EV)" using 1:3 with linespoints pt 9 title "1by1" \
	'| gnuplot

B.check-energies.svg: $(EC) $(E1) Makefile
	echo '\
	set term svg enhanced size 640,480 background "white" font "Arial,18";\
	set out "$@";\
	set title "Lowest eigenvalue: cyclic vs 1value";\
	set xlabel "matrix size n";\
	set ylabel "e_0";\
	plot [:][]\
	 "$(EC)" with linespoints pt 1 title "cyclic" \
	,"$(E1)" with linespoints pt 2 title "1value" \
	'| gnuplot

B.out.txt: main.exe Makefile
	$(MONO) $< 7 >$@

$(EC): main.exe #Makefile
	cat /dev/null | tee $(EC)
	for N in `seq 73 5 93`; do \
	echo "N=$$N";\
	$(MONO) main.exe $$N cyclic >>$(EC); \
	done
$(E1): main.exe #Makefile
	cat /dev/null | tee $(E1)
	for N in `seq 73 5 93`; do \
	echo "N=$$N";\
	$(MONO) main.exe $$N values 1 >>$(E1); \
	done
$(EV): main.exe #Makefile
	cat /dev/null | tee $(EV)
	for N in `seq 23 5 53`; do \
	echo "N=$$N";\
	$(MONO) main.exe $$N values $$N >>$(EV); \
	done

main.exe: main.cs jacobi.dll
	$(MCS) $< -o:$@ -r:$(call commalist,$(filter-out $<,$^))

LIBDIR=../../../lib/matrix
jacobi.dll: jacobi.cs values.cs $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs
	mcs $(filter %.cs,$^) -t:library -o:$@

#LIBDIR=../../lib/matrix
#matrix.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs
#        $(MCS) -t:library -out:$@ $^

clean: ; $(RM) *.dll *.exe out.* log* *.svg B*

#%.dll: %.cs ; $(MCS) -t:library $*.cs -o:$*.dll *log *.log
