bool search (int n, int arr, int v) {
if (n < 0) 
{
 return False;
}
else if (n >= 0) 
{
 return True;
}
else return   (search(n-1, arr, v));
