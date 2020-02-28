#include <stdio.h>
#include <math.h>
int main()
{
for(double x=-4;x<=4;x+=0.125)
printf("%lg %lg\n",x,tgamma(x));
return 0;
}
