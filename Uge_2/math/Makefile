CSFLAGS = -optimize+ -platform:anycpu
.PHONEY:all
all: out.txt
	cat out.txt
out.txt : main.exe
	mono main.exe > out.txt

main.exe : main.cs cmath.dll
	mcs $(CSFLAGS) -out:main.exe -reference:cmath.dll main.cs

LIBDIR = ../../lib/complex
cmath.dll: $(LIBDIR)/cmath.cs $(LIBDIR)/complex.cs
	mcs $(CSFLAGS) -target:library -out:cmath.dll $^


#.PHONEY:clean
clean:
	rm -f out.txt main.exe cmath.dll
