CSFLAGS = -optimize+ -platform:anycpu
.PHONEY:all
all: out.txt

out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs cmath.dll
	mcs $(CSFLAGS) -reference:cmath.dll main.cs

cmath.dll: ../../lib/complex/complex.cs ../../lib/complex/cmath.cs
	mcs $(CSFLAGS) -out:$@ -target:library $^
#.PHONEY:clean
clean:
	rm -f out.txt main.exe cmath.dll
