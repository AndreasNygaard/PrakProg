all: out.txt

out.txt: main.exe
	mono $< > out.txt 2>err.out

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/
math.dll: $(LIBDIR)/integration/quado8.cs $(LIBDIR)/matrix/vector.cs ../gnuplot/gamma_function/gamma.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f main.exe out.txt err.out math.dll
