CSFLAGS = -optimize-

out.txt : main.exe
	mono $< > $@
	cat $@
main.exe : main.cs vector3d.dll
	mcs $(CSFLAGS) -reference:vector3d.dll $<

vector3d.dll : vector3d.cs
	mcs -target:library $<

clean:
	rm -f out.txt main.exe vector3d.dll

