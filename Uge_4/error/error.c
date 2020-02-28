#include <stdio.h>
#include <math.h>
int main()
{
for(double x=-4;x<=4;x+=0.5)
printf("%lg %lg\n",x,erf(x));
return 0;
}
