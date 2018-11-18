#include<stdio.h>
#include<string.h>
#include<ctype.h>

int main()
{
   char str[100];
   int c = 0, count[26] = {0}, x,i,j;

   printf("Enter a string\n");
   gets(str);

   for(i=0,j=0;i<strlen(str);i++)
    {
        if(str[i]!=' ')
        {
            str[j]=toupper(str[i]);
    j++;
    }

    }

   while (str[c] != '\0') {
      x = str[c] - 'A';
      count[x]++;
      c++;
   }

   for (c = 0; c < 26; c++)
         printf("%c occurs %d times in the string.\n", c + 'A', count[c]);

   return 0;
}
