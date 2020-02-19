CSFLAGS = -optimize+ -platform:anycpu
.PHONEY:all
all: out.txt
	cat out.txt
out.txt : main.exe
	mono main.exe > out.txt
main.exe : main.cs
	mcs $(CSFLAGS) -out:main.exe -reference:cmath.dll main.cs

cmath.dll: ../../lib/complex/complex.cs ../../lib/complex/cmath.cs
	mcs $(CSFLAGS) -out:../../lib/complex/cmath.dll -target:library ../../lib/complex/cmath.cs
	cp ../../lib/complex/cmath.dll .

#.PHONEY:clean
clean:
	rm -f out.txt main.exe
