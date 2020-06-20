#include <stdio.h>
#include <math.h>
int main()
{
for(double x=-4.984375;x<=10.015625;x+=0.25)
printf("%lg %lg\n",x,lgamma(x));
return 0;
}
