all: output.txt

output.txt: main.exe
	mono $< > output.txt

main.exe: main.cs math.dll
	mcs -r:math.dll $<

LIBDIR=../../lib/matrix
math.dll: $(LIBDIR)/vector.cs $(LIBDIR)/matrix.cs qrDecomp.cs
	mcs -t:library -out:$@ $^

clean:
	rm -f *.exe *.dll *.txt
