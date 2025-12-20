// See https://aka.ms/new-console-template for more information
//Console.WriteLine("***** Basic Console I/O *****");
//GetUserData();
//Console.ReadLine();

Console.WriteLine("Welcome to the Console App");
FormatNumericalData();

string userMessage = string.Format("100000 in hex is {0:x}", 100000);
Console.WriteLine(userMessage);

static void GetUserData() {
    Console.Write("Enter yer Name, amigo:");
    string userName = Console.ReadLine();
    Console.Write("Please enter yer Age, Me Hearty:");
    string userAge = Console.ReadLine();

    ConsoleColor prevColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Hello {0}! You are {1} years old.", userName, userAge);
}

static void FormatNumericalData() {
    int num = 99999;
    Console.WriteLine("The value {0} in various formats", num);
    Console.WriteLine("c format: {0:c}", num);
    Console.WriteLine("d9 format: {0:d9}", num);
    Console.WriteLine("f3 format: {0:f3}", num);
    Console.WriteLine("n format: {0:n}", num);

    Console.WriteLine("Hex format (E): {0:E}", num);
    Console.WriteLine("e format: {0:e}", num);

    Console.WriteLine("X format: {0:X}", num);
    Console.WriteLine("x format: {0:x}", num);
}
