// See https://aka.ms/new-console-template for more information
using LINQ;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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

// You can also use OfType<T> as a filter. If you have a mixed object collection, it can be a filter.

static void OfTypeAsFilter() {
    ArrayList someObjs = new();
    someObjs.AddRange(new object[] { 10, 400, 8, false, new Car(), "string" });
    var myInts = someObjs.OfType<int>();

    foreach (int i in myInts) {
        Console.WriteLine("Int val: {0}", i);
    }
}

OfTypeAsFilter();

// Exploring query operators. Not all operations have a shorthand notation for query syntax but do have extension
// method
// examples: from, in, where eselect, join, on, equals, into, orderby, groupby

Console.WriteLine("*** Fun with Query Expressions ***\n");

ProductInfo[] itemsInStock = new ProductInfo[] {
    new() { Name = "Mac's Coffee", Description = "Description", NumberInStock = 24},
    new() { Name = "Joseph Josh", Description = "Description", NumberInStock = 1},
    new() { Name = "Business 0", Description = "Description", NumberInStock = 3},
    new() { Name = "Pokemon TCG", Description = "Alfred", NumberInStock = 120_000}
};

// operation order is very important
// general template: var result = from matchingItem in container select matchingItem;

// select every item in the container

static void SelectAll(ProductInfo[] products) {
    Console.WriteLine("All products");

    var allProducts = from p in products select p.Name;

    foreach (var prod in allProducts) {
        Console.WriteLine(prod.ToString());
    }
}

SelectAll(itemsInStock);

// Subsets of Data.(where operator)

// var result = from item in container where BooleanExpression select item;

static void GetOverstock(ProductInfo[] products) {
    Console.WriteLine("Overstock items:");

    // more than 2 in stock
    var overstock = from p in products where p.NumberInStock > 2 select p;

    foreach (ProductInfo c in overstock) Console.WriteLine(c.ToString());
}

GetOverstock(itemsInStock);

// Page data using Take(), TakeWhile(), TakeLast(), Skip(), SkipWhile(), SkipLast() // skips are omits

//Paging with LINQ

static void PagingWithLINQ(ProductInfo[] products) {
    Console.WriteLine("paging operations");

    IEnumerable<ProductInfo> list = (from p in products select p).Take(3); // first 3;
    Console.WriteLine("The first 3:");
    foreach (var l in list) {
        Console.WriteLine(l.ToString());
    }

    // NOTE: TAKEWHILE STOPS TAKING RECORDS THE FIRST TIME A CONDITION FAILS

    // TakeLast: speciifed number from the end of the collection.

    //Skips however many you don't want to process. .Skip() and SkipWhile()
    Console.WriteLine("SKIPPING THE FIRST 3...");

    list = (from p in products select p).Skip(3);

    foreach (var l in list)
    {
        Console.WriteLine(l.ToString());
    }

    // skip while

    Console.WriteLine("SKIP while numbers in stock is greater than 20 ");

    list = (from p in products select p).SkipWhile((x) => x.NumberInStock > 20);

    foreach (var l in list)
    {
        Console.WriteLine(l.ToString());
    }

    // SkipLast
    Console.WriteLine("Skipping last two");

    list = (from p in products select p).SkipLast(2);

    foreach (var l in list)
    {
        Console.WriteLine(l.ToString());
    }
}

PagingWithLINQ(itemsInStock);

// Ranges in the take command.

static void PagingWithRanges(ProductInfo[] products) {
    Console.WriteLine("Paging operations");

    IEnumerable<ProductInfo> list = (from p in products select p).Take(..3); // FROM 0 TO 3. 3.. would be 3 onward
    // ^2..


    Console.WriteLine("Products 0 to 3:");

    foreach (var l in list)
    {
        Console.WriteLine(l.ToString());
    }

    Console.WriteLine("skipping 3 starting projecrs.");
    list = (from p in products select p).Take(3..0);

    foreach (var l in list)
    {
        Console.WriteLine(l.ToString());
    }

    // Use carat for last however many

    Console.WriteLine("Take last of a number.");

    list = (from p in products select p).Take(^2..);

    foreach (var l in list)
    {
        Console.WriteLine(l.ToString());
    }
} // ^ means LAST. 3..5 means FROM 3 TO 5

PagingWithRanges(itemsInStock);

// CHUNK new with .NET 6. Takes a parameter of size and splits source into an enumerable of enumerables
// array of arrays

static void PagingWithChunks(ProductInfo[] products) {
    Console.WriteLine("Chunking operations");

    IEnumerable<ProductInfo[]> chunks = products.Chunk(size: 2);

    int counter = 0;

    foreach (var chunk in chunks) {
        Console.WriteLine($"Chunk #{++counter}");
        foreach (var c in chunk)
        {
            Console.WriteLine($"{c.ToString()}");
        }
    }
}

PagingWithChunks(itemsInStock);

// Project data types

static void GetNamesAndDescr(ProductInfo[] products) {
    Console.WriteLine("Names and Descriptions:");
    var nameDesc = from p in products select new { p.Name, p.Description }; // when projecting to an anonymous
    // type, you need var since the type cannot be determined before compile time.

    foreach (var l in nameDesc)
    {
        Console.WriteLine(l.ToString());
    }
}

GetNamesAndDescr(itemsInStock);

// what if you need to return an anonymous type to a caller?
// try ToArray() extension

static Array GetProjectedSubset(ProductInfo[] products) {
    var nameDesc = from p in products select new { p.Name, p.Description };
    return nameDesc.ToArray();
}

Array resultOfProjection = GetProjectedSubset(itemsInStock);

Console.WriteLine("=> Projection return with general Array type: ");

foreach (var l in resultOfProjection)
{
    Console.WriteLine(l.ToString());
}

// You can also project to other datatypes. Project product results into MiniProducts.

static void GetNamesAndDescrTyped(ProductInfo[] products) {
    Console.WriteLine("Names and descriptions as a concrete type:");

    IEnumerable<MiniProduct> nameDesc = from p in products select new MiniProduct { Name = p.Name, Description = p.Description };

    foreach (MiniProduct p in nameDesc) {
        Console.WriteLine(p.ToString());
    }
}

GetNamesAndDescrTyped(itemsInStock);

// Counts using Enumerable. Count extension method on the Enumerable class will tell you how many were returned.

static void GetCountFromQuery()
{

    Console.WriteLine("** Using Count()");
    string[] currentVideoGames = { "Destiny 3", "GTA 6", "Windows 12 the Game" };

    int num = (from g in currentVideoGames where g.Length > 5 select g).Count();

    Console.WriteLine($"{num} items returned by query");
}

GetCountFromQuery();

// Nonenumerated Counts TryGetNonEnumeratedCount() => attempt to determine count without fully running query

static void GetUnenumeratedCount(ProductInfo[] products) {
    Console.WriteLine("Get Unenumerated Count");
    IEnumerable<ProductInfo> query = from p in products select p;

    var result = query.TryGetNonEnumeratedCount(out int count); //takes an out int, returns true if success
    if (result)
    {
        Console.WriteLine($"Total Count: {count}");
    } else {
        Console.WriteLine("Try Get Count Failed.");
    }
}

// example of where TryGetNonEnumeratedCount may fail. Yield is an automatic enumeration
static IEnumerable<ProductInfo> GetProduct(ProductInfo[] products) {
    for (int i = 0; i < products.Count(); i++) {
        yield return products[i]; //won't run until calling code requests it.
    }
}

static void GetUnenumeratedCountFail(ProductInfo[] products) {
    Console.WriteLine("Case where Unenumerated Count may fail");
    var newRes = GetProduct(products).TryGetNonEnumeratedCount(out int newCount);

    if (newRes)
    {
        Console.WriteLine("Total count: {0}", newCount);
    }
    else Console.WriteLine("Try Get Count Failed, must be enumerated.");
}

GetUnenumeratedCountFail(itemsInStock);

// reverse()

static void ReverseAll(ProductInfo[] products) {
    Console.WriteLine("Product in reverse:");

    var allProducts = from p in products select p;
    foreach (var p in allProducts.Reverse()) { // still deferred
        Console.WriteLine(p.ToString());
    }
}

ReverseAll(itemsInStock);

// by default, orderBy is ascending but you can include it for clarity. you can also use descending.

//Extension methods to find unions, differences, intersections.

// Except() => result set that contains the difference between 2 containers.

static void DisplayDifference() {
    List<string> theCars = [
        "Yugo",
        "Aztec",
        "BMW",
        "Lambo"
    ];

    List<string> theOtherCars = [
        "Aztec",
        "BMW",
        "Lambo"
    ];

    var carDiff = (from c in theCars select c).Except(from c2 in theOtherCars select c2);

    Console.WriteLine("Difference .Except Operator");

    foreach (string s in carDiff) {
        Console.WriteLine(s);
    }
}

DisplayDifference();

static void DisplayIntersection() {
    List<string> theCars = [
        "Yugo",
        "Aztec",
        "BMW",
        "Lambo"
    ];

    List<string> theOtherCars = [
        "Aztec",
        "BMW",
        "Lambo"
    ];

    var carIntersection = (from c in theCars select c).Intersect(from c2 in theOtherCars select c2);

    Console.WriteLine("Intersection of two string lists:");

    foreach (string s in carIntersection) {
        Console.WriteLine(s);
    }
}

DisplayIntersection();

static void DisplayUnion() {
    List<string> theCars = [
        "Yugo",
        "Aztec",
        "BMW",
        "Lambo"
];

    List<string> theOtherCars = [
        "Aztec",
        "BMW",
        "Lambo"
    ];

    var carUnion = (from c in theCars select c).Union(from c2 in theOtherCars select c2); //removes repeating vals

    Console.WriteLine("Union of two string lists:");

    foreach (string s in carUnion)
    {
        Console.WriteLine(s);
    }
}

DisplayUnion();

static void DisplayConcat()
{
    List<string> theCars = [
        "Yugo",
        "Aztec",
        "BMW",
        "Lambo"
];

    List<string> theOtherCars = [
        "Aztec",
        "BMW",
        "Lambo"
    ];

    var carConcat = (from c in theCars select c).Concat(from c2 in theOtherCars select c2); // includes repeating vals

    Console.WriteLine("Concat of two string lists:");

    foreach (string s in carConcat)
    {
        Console.WriteLine(s);
    }
}

DisplayConcat();

// New methods in .NET 6

//ExceptBy() => uses selector to remove records from the first where the value exists in the second

static void DisplayDiffBySelector() {
    var first = new (String name, int Age)[] { ("frANCis", 20), ("wow", 1), ("YOLO", 25) };
    var second = new (String name, int Age)[] {
        ("NewName", 25),
        ("Dr. Fillibuster", 75),
        ("Dr. Phil", 65)
    };

    // perform ExceptBy on Age

    var result = first.ExceptBy(second.Select((x) => x.Age), (product) => product.Age); //filters out any entries
    // between the two that share an equivalent Age property, which is NewName and YOLO.

    Console.WriteLine("ExceptBy selector");

    foreach (var item in result) {
        Console.WriteLine(item);
    }
}

static void DisplayIntersectBySelector() { //return result set that contains common data items based on selector
    var first = new (string Name, int Age)[] {
        ("Francis", 20),
        ("Lindsey", 30),
        ("Ashley", 40)
    };

    var second = new (string Name, int Age)[] { 
        ("Claire", 30),
        ("Pat", 30),
        ("Drew", 33)
    };

    var res = first.IntersectBy(second.Select((x) => x.Age), (person) => person.Age);

    Console.WriteLine("Intersection by selector");
    foreach (var item in res) {
        Console.WriteLine(item);
    } // only one since there is only a single age 30 to match to in the first set.
}

DisplayDiffBySelector();

DisplayIntersectBySelector();

static void DisplayUnionBySelector() {
    var firstTuples = new (string Name, int Age)[] {
        ("Frank", 20),
        ("Alan", 45)
    };

    var secondTuples = new (string Name, int Age)[] { 
        ("Justine", 85),
        ("Warren", 45),
        ("Dolt", 45)
    };

    // this will not double combine the single Alan with Warren and Dolt. Needs to be 1-1

    var result = firstTuples.UnionBy(secondTuples, (person) => person.Age);

    Console.WriteLine("Union by selector:");

    foreach (var item in result) {
        Console.WriteLine(item);
    }
}

DisplayUnionBySelector();

// Want to remove duplicates? Consider the "distict" extension method

static void DisplayContactNoDuplicates() {
    List<string> carBrands = [
        "Ford",
        "Bugatti",
        "Ferrari"
    ];

    List<string> otherCarBrands = [
        "Ford",
        "Bugatti",
        "I woke up in a new Bugatti",
        "OK I Pull Up"
    ];

    var concatenatedCars = (from c in carBrands select c).Concat(from c2 in otherCarBrands select c2);

    Console.WriteLine("Concat with Distinct method");

    foreach (string carName in concatenatedCars.Distinct()) {
        Console.WriteLine(carName);
    }
}

DisplayContactNoDuplicates();

// remove duplicates with selectors (DistinctBy) operating the same was as other ._By methods.

static void DisplayConcatNoDuplicatesSelector() {
    var first = new (string Name, string SecretName)[] { 
        ("Francis", "daFran"),
        ("Angel", ("Diablo")),
        ("Alfred", "Alfie")
    };

    var second = new (string Name, string SecretName)[] {
        ("Francine", "daFran"),
        ("Angel", ("Angelique")),
        ("Alfred", "Alf")
    };

    var result = first.Concat(second).DistinctBy((item) => item.SecretName);

    Console.WriteLine("Distict by selector (secret name):");

    foreach (var item in result) {
        Console.WriteLine(item);
    }
}

DisplayConcatNoDuplicatesSelector();

// Aggregation operations e.g Count(), Max(),  Min(), Average(), Sum()

static void AggregationOps()
{
    double[] winterTemperatures = { 2.0, -21.3, 8, -4, 0, 8, 2 };

    Console.WriteLine("Max temp: {0}", (from t in winterTemperatures select t).Max());

    Console.WriteLine("Min temp {0}", (from t in winterTemperatures select t).Min());

    // And so on.
}

AggregationOps();

static void AggregateOpsBySelector(ProductInfo[] products) {
    Console.WriteLine($"Max by stock: {products.MaxBy((x) => x.NumberInStock)}");
    Console.WriteLine($"Min by stock: {products.MinBy((x) => x.NumberInStock)}");
}

AggregateOpsBySelector(itemsInStock);

// Linq using enumerable

static void QueryStringsWithOperators() {
    Console.WriteLine("******* Using Query Operators ********* \n");

    string[] currentVideoGames = { "Destiny 3", "GTA 6", "Windows 12 the Game" };

    var subset = from game in currentVideoGames where game.Contains(' ') orderby game select game;

    foreach (string s in subset) {
        Console.WriteLine("Item: {0}", s);
    }
}

QueryStringsWithOperators();

static void QueryStringsWithEnumerableAndLambdas() {
    Console.WriteLine("**** Using Enumerable and Lambda ***** ");

    string[] currentVideoGames = { "Destiny 3", "GTA 6", "Windows 12 the Game" };

    // The array has .Where from an Enumerable extension method on Array

    var subset = currentVideoGames.Where((game) => game.Contains(' ')).OrderBy((game) => game).Select((game) => game);

    foreach (var game in subset)
    {
        Console.WriteLine("Item: {0}", game);
    }
}

QueryStringsWithEnumerableAndLambdas();

static void QueryStringsWithEnumerableLambdas2() {
    Console.WriteLine("*** Using Enumerable Lambda Expressions and Separating out the Pieces ***");

    string[] currentVideoGames = { "Destiny 3", "GTA 6", "Windows 12 the Game" };

    var gamesWithSpaces = currentVideoGames.Where((game) => game.Contains(' '));

    var orderedGames = gamesWithSpaces.OrderBy((g) => g);
    var subset = orderedGames.Select((g) => g);

    foreach (var game in subset)
    {
        Console.WriteLine("Item: {0}", game);
    }
}

QueryStringsWithEnumerableLambdas2();

static void QueryStringsNoShorthand() {
    Console.WriteLine("*** Using Anonymous Methods, no shorthand really");

    string[] currentVideoGames = { "Destiny 3", "GTA 6", "Windows 12 the Game" };

    Func<string, bool> searchFilter = delegate (string game) { return game.Contains(' '); };

    Func<string, string> itemToProcess = delegate (string s) { return s; };

    var subset = currentVideoGames.Where(searchFilter).OrderBy(itemToProcess).Select(itemToProcess);

    foreach (var game in subset)
    {
        Console.WriteLine("Item: {0}", game);
    }
}

QueryStringsNoShorthand();

static void QueryStringsFull() {
    Console.WriteLine("Not even using delegates, let's do it again");

    string[] currentVideoGames = { "Destiny 3", "GTA 6", "Windows 12 the Game" };

    Func<string, bool> searchFilter = new Func<string, bool>(Filter);

    Func<string, string> itemToProcess = new Func<string, string>(ProcessItem);

    var subset = currentVideoGames.Where(searchFilter).OrderBy(itemToProcess).Select(itemToProcess);

    foreach (var game in subset)
    {
        Console.WriteLine("Item: {0}", game);
    }

    static bool Filter(string game) {
        return game.Contains(' ');
    }

    static string ProcessItem(string game) { return game; }
}

QueryStringsFull();