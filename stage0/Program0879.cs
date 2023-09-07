partial class Program
{
    private static void Main(string[] args)
    {
        Welcome0879();
        Welcome5987();
        Console.ReadKey();
    }

    static partial void Welcome5987();

    private static void Welcome0879()
    {
        Console.Write("Enter you name: ");
        string userName = Convert.ToString(Console.ReadLine());
        Console.WriteLine("{0}, welcome to my first console application", userName);
    }
}