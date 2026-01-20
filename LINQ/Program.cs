// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Globalization;
using LINQ;

Console.WriteLine("Hello, World!");

// Language Integrated Query technology set

static void QueryOverStrings() {
    string[] currentVideoGames = { "Destiny 3", "Fallout 5", "GTA VI", "Minecraft 2" };

    IEnumerable<string> subset = from g in currentVideoGames where g.Contains(' ') orderby g select g; //LINQ Query Syntax

    ReflectOverQueryResults(subset);

    foreach (string sub in subset) {
        Console.WriteLine("Item: {0}", sub);
    }
}

static void QueryOverStringsExtension() {
    string[] currentVideoGames = { "Destiny 3", "Fallout 5", "GTA VI", "Minecraft 2" };

    IEnumerable<string> subset = currentVideoGames.Where((g) => g.Contains(' ')).OrderBy((g) => g).Select((g) => g);

    ReflectOverQueryResults(subset, "Extension Methods");


    Console.WriteLine("Query string array with extension methods on array");
    foreach (string sub in subset) {
        Console.WriteLine("Item: {0}", sub);
    }
}

static void QueryOverInts()
{
    int[] nums = { 10, 20, 30, 40, 1, 2, 3, 8 };
    // 
    var subset = from i in nums where i < 10 select i;

    // The linq query is not actually evaluated until the result is used.
    foreach (var num in subset) {
        Console.WriteLine("Item: {0}", num);
    }

    // If we change something in our array, and then reference subset query, it will update to reflect it.

    // Using debugging tools in Visual Studio, you can view what the query will evaluate to at any given moment in execution.
    nums[0] = 0;

    Console.WriteLine("We updated slot [0], now we should see a '0' in the subset even though we didn't run an update.");
    foreach (var num in subset) {
        Console.WriteLine("Item: {0}", num);
    }

    ReflectOverQueryResults(subset);
}

static void ReflectOverQueryResults(object resultSet, string queryType = "Query Expressions") {
    Console.WriteLine($"=========> Info about your query using {queryType}");
    Console.WriteLine("resultSet is of type: {0}", resultSet.GetType().Name);
    Console.WriteLine("resultSet location: {0}", resultSet.GetType().Assembly.GetName().Name);
}

Console.WriteLine("**** Fun with LINQ to Objects ****\n");
QueryOverStrings();
QueryOverStringsExtension();
QueryOverInts();

// As we can see, the types are difficult to predict. Consider using Var -> implicit typing should be use. Remember that usually the value is a type implementing IEnumerable<T>

// Linq and extensions => Arrays don't implement IEnumerable<T> however, it indirectly gets foreach etc via System.Linq.Enumerable class type. Generic extension methods for Array and others.

// DefaultIfEmpty()

static void DefaultWhenEmpty() {
    Console.WriteLine("Default when Empty");
    int[] nums = { 10, 20, 30, 40, 1, 2, 3, 8 };

    foreach (var i in nums.DefaultIfEmpty(-1)) {
        // all numbers printed because not empty.
        Console.WriteLine("Num: {0}", i);
    }

    foreach (var i in (from i in nums where i > 99 select i).DefaultIfEmpty(-1)) {
        // Fallback value
        Console.WriteLine("Num: {0}", i);
    }
}

DefaultWhenEmpty();

// Any time you call an execution method on a linq result, it will evaluate.
// If First() is used, it is executed right away, FirstOrDefault => first item, default item if none. Single and SingleOrDefault are used as well.

// first and single will throw an exception when no results while the others will return null.


// LINQ ops that trigger immediate execution.
static void ImmediateExecution() {
    Console.WriteLine("Immediate execution");
    int[] nums = { 10, 20, 30, 40, 1, 2, 3, 8 };
    int number = (from i in nums select i).First();
    Console.WriteLine("First member {0}", number);

    // first in query order
    number = (from i in nums orderby i select i).First();
    Console.WriteLine("First in categorical order {0}", number);

    // first and single or default
    number = (from i in nums where i > 99 select i).FirstOrDefault();
    Console.WriteLine("Querying a small array for something large: {0}", number);
    number = (from i in nums where i > 99 select i).SingleOrDefault();

    Console.WriteLine("Querying a small array for something large with Single: {0}", number);

    // Fail on no result
    try
    {
        Console.WriteLine("Querying .First from empty result");
        number = (from i in nums where i > 99 select i).First();
    }
    catch (Exception e) {
        Console.WriteLine("An exception occured: {0}", e.Message);
    }

    try
    {
        Console.WriteLine("Querying .Single from empty result");
        number = (from i in nums where i > 99 select i).Single();
    }
    catch (Exception e) {
        Console.WriteLine("An exception occured: {0}", e.Message);
    }

    try
    {
        Console.WriteLine("Querying .Single from multi-result query");
        number = (from i in nums where i > 10 select i).Single();
    }
    catch (Exception e)
    {
        Console.WriteLine("An exception occured: {0}", e.Message);
    }

    // Get data immediately and capture as a collection
    int[] subsetAsIntArray = (from i in nums where i < 10 select i).ToArray<int>(); //even this <int> tag isn't needed as C# can infer the type.
    Console.WriteLine("Getting data from query immediately as an array {0}", subsetAsIntArray);

    List<int> subsetAsList = (from i in nums where i < 10 select i).ToList<int>();
    Console.WriteLine("Getting data from query immediately as a List collection {0}", subsetAsList);
}

ImmediateExecution();

// The OrDefault methods can have their default values overwritten.

static void SettingDefaults()
{
    int[] numbers = Array.Empty<int>();
    var query = from i in numbers where i > 100 select i; // no results
    var number = query.FirstOrDefault(-1);
    Console.WriteLine("Result of query {0}", number);

    number = query.SingleOrDefault(-2);
    Console.WriteLine("SingleOrDefault on empty result {0}", number);

    number = query.LastOrDefault(-3);
    Console.WriteLine("LastOrDefault with empty result def fallback {0}", number);
}

SettingDefaults();

// you can define a field in a class that is the result of a Linq query (have to specify type). 
// target of LINQ cannot be instance level data.


// Returning LINQ data to external caller - You could return the query...but it might be better and more granular
// if you run the query and return the result as a collection ,array, first item, etc. This eliminates complex types.
Console.WriteLine("**** LINQ Return Values ****\n");
IEnumerable<string> subset = GetStringSubset();

foreach (string item in subset) {
    Console.WriteLine(item);
}

static IEnumerable<string> GetStringSubset()
{
    string[] colors = { "Light Red", "Green", "Yellow", "Dark Red" };
    IEnumerable<string> redColors = from c in colors where c.Contains("Red") select c;
    return redColors;
}

static string[] GetStringSubsetAsArray()
{
    string[] colors = { "Light Red", "Green", "Yellow", "Dark Red" };
    var redColors = from c in colors where c.Contains("Red") select c;

    // return result as array right away.
    return redColors.ToArray(); // toArray infers string as the type from context.
}

Console.WriteLine("etting string subset as array");
string[] result = GetStringSubsetAsArray();
foreach (string s in result) {
    Console.WriteLine("String: {0}", s);
}

// Using LINQ queries on Collection objects.
Console.WriteLine("***************** LINQ Over Generic Collections ***************************** \n");
List<Car> theCars = [
    new() { Name = "Radford", Color = "Tinted Black", Speed = 100, Make = "BMW"},
    new() { Name = "Quentin", Color = "Alpha Red", Speed = 25, Make = "GOAT Nissan"},
    new() { Name = "BentOverForwards", Color = "Orange", Speed = 14, Make = "Subaru"},
    new() { Name = "Ex Dee", Color = "Irrelevant", Speed = 66, Make = "Fake Manufacturer"}
];

// remember, LINQ queries can be used on any type implementing IEnumerable<T>. 

static void GetFastCars(List<Car> candidates) {
    var fastestCars = from c in candidates where c.Speed >= 55 select c;

    foreach (var car in fastestCars) {
        Console.WriteLine($"{car.Name} is a quickie.");
    }
}

GetFastCars(theCars);

// say you want a more complex query with an AND

static void GetFastBMWs(List<Car> candidates) {
    var fastBMW = from c in candidates where c.Speed > 90 && c.Make == "BMW" select c;

    foreach (var car in fastBMW) {
        Console.WriteLine("{0} is a fast BMW.", car.Name);
    }
}

GetFastBMWs(theCars);

// LINQ queries to nongeneric collections, use Enumerable.OfType<T>()

static void LINQOverArrayList() {
    Console.WriteLine("**** LINQ over ArrayList ****");

    ArrayList cars = new() {
        new Car() { Name = "Sporty mcSport", Color = "Walter", Speed = 1, Make = "Capri Sun"},
        new Car() {  Name = "Alfredo", Color = "White", Speed = 11, Make = "Buick"},
        new Car() { Name = "Example", Color = "Grey (Default)", Speed = 45, Make = "Chevrolet"}
    };

    // Now we need to transform it into an IEnumerable<T> compatible type
    var myCarsEnumerable = cars.OfType<Car>(); //extension method.

    // NOW we can make a query on it
    var fastCars = from c in myCarsEnumerable where c.Speed > 11 select c; // some real speedsters.
    foreach (var car in fastCars) {
        Console.WriteLine($"{car.Name} is blinding by at {car.Speed} MPH");
    }
}