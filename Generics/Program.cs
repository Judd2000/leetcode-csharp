using Generics;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

List<GenericsEx> list = new List<GenericsEx>(); // declare list of type
// GENERIC

// System.Collections.Generic namespace.

// Collection initialization syntax.
int[] arrayInts = { 0, 1, 2, 3, 4 }; // regular array initialization

List<int> genericsIntList = new List<int> { 2, 2, 2, 3, 4, 5, 6 };

// you can also use class initialization right in the call too

//List<Point> listOfPoints = new List<Point> { 
//    new() { X=2, Y=3},
//    new(){ X=4, Y=5},

//    new() { X=7, Y=8},
//};

//foreach (Point point in listOfPoints) {
//    Console.WriteLine(point);
//}

static void UseGenericList() {
    List<Person> list = new List<Person>() {
        new() { FirstName = "Me", LastName = "N'You", Age = 12},
        new() { FirstName = "George", LastName = "Jorge", Age = 45}
    };

    Console.WriteLine("Items in list: {0}", list.Count);

    foreach (Person person in list) {
        Console.WriteLine(person);
    }

    // insert new.
    Console.WriteLine("Inserting new entry to list...");
    list.Insert(2, new() { FirstName = "Georgie", LastName = "Porgie", Age = 6 });
    Console.WriteLine("Items in list: {0}", list.Count);
    
    // use toArray to copy to a static sized array
    Person[] listAsArray = list.ToArray();
    foreach (Person person in listAsArray) {
        Console.WriteLine("Firstnames in array {0}", person.FirstName);
    }
}

UseGenericList();

// Stack <T> Last In, First Out

static void UseGenericStack() {
    Stack<Person> stackOfPeople = new();
    stackOfPeople.Push(new Person { FirstName = "Googoo", LastName = "Gaga", Age = 0 });
    stackOfPeople.Push(new Person { FirstName = "Listen", LastName = "Tome", Age = 36 });
    stackOfPeople.Push(new Person { FirstName = "Green", LastName = "Goblin", Age = 67 });

    Console.WriteLine("First person in stack {0}", stackOfPeople.Peek());
    Console.WriteLine("Popped {0} off the top of the stack", stackOfPeople.Pop());
    Console.WriteLine("First person in stack {0}", stackOfPeople.Peek());
    Console.WriteLine("Popped {0} off the top of the stack", stackOfPeople.Pop());
    Console.WriteLine("First person in stack {0}", stackOfPeople.Peek());
    Console.WriteLine("Popped {0} off the top of the stack", stackOfPeople.Pop());
    // if nothing to 'pop' an exception is thrown, probably a good idea to handle it with try catch

    try
    {
        Console.WriteLine("First person in stack {0}", stackOfPeople.Peek());
        Console.WriteLine("Popped {0} off the top of the stack", stackOfPeople.Pop());
    }
    catch (InvalidOperationException e) {
        Console.WriteLine("Error: {0}", e.Message);
    }

}

UseGenericStack();

static void UseGenericQueue() {
    // methods include Dequeue() (get next in line), Enqueue(add to end),
    // and Peek
    Queue<Person> coffeeLine = new();
    coffeeLine.Enqueue(new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 });
    coffeeLine.Enqueue(new Person { FirstName = "Marge", LastName = "Simpson", Age = 45 });
    coffeeLine.Enqueue(new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 });

    // peek
    Console.WriteLine("May I take your order, {0}", coffeeLine.Peek());

    Console.WriteLine("Now serving {0}", coffeeLine.Dequeue());
    Console.WriteLine("Now serving {0}", coffeeLine.Dequeue());

    Console.WriteLine("Now serving {0}", coffeeLine.Dequeue());

    // trying to Deque or peek when there is nothing returns an error
    try
    {
        Console.WriteLine("Now serving {0}", coffeeLine.Dequeue());

    }
    catch (InvalidOperationException e) {
        Console.WriteLine("Error: {0}", e.Message);
    }
}

UseGenericQueue();

// Priority Queue

static void UsePriorityQeue() {
    Console.WriteLine("Generic Priority Queue");
    PriorityQueue<Person, int> rankedPeople = new();
    rankedPeople.Enqueue(new Person { FirstName = "Noname", LastName = "Jones", Age = 0 }, 2);

    // if multiple are set to same priority, order of Dequeue and Peek is not guaranteed.

    rankedPeople.Enqueue(new Person { FirstName = "Bill", LastName = "Clinton", Age = 100 }, 1);

    rankedPeople.Enqueue(new Person { FirstName = "Will", LastName = "Wonka", Age = 0 }, 3);

    // while to loop while pq is full
    while (rankedPeople.Count > 0) {
        // Should be Bill, noname and Will Wonka.
        Console.WriteLine(rankedPeople.Dequeue().FirstName);
        Console.WriteLine(rankedPeople.Dequeue().FirstName);

        Console.WriteLine(rankedPeople.Dequeue().FirstName);

    }
}

UsePriorityQeue();

// Sorted set => needs a method to sort with. Pass an object that implements IComparer<T>
static void UseSortedSet() {
    SortedSet<Person> sortedByAgePeople = new SortedSet<Person>(new SortPeopleByAge())
    {
        new() { FirstName = "Bill", LastName = "Clinton", Age = 100 },
        new() { FirstName = "George", LastName = "Bush", Age = 24 },
        new() { FirstName = "Donald", LastName = "Trump", Age = 200 },

    };

    // now when we run foreach, note that it goes in ascending age order
    foreach (Person p in sortedByAgePeople) {
        Console.WriteLine("Next up from sorted set {0}", p);
    }
}

UseSortedSet();

// Dictionary<TKey, TValue>!

static void UseDictionary() {
    Dictionary<string, Person> peopleIds = new();

    peopleIds.Add("123", new() { FirstName = "Dylan", LastName = "Jenkins", Age = 10 });
    peopleIds.Add("234", new() { FirstName = "Ryna", LastName = "ragfdsa", Age = 19 });
    peopleIds.Add("345", new Person { FirstName = "Gaga", LastName = "Googoo", Age = 16 });

    Person firstPerson = peopleIds["123"];

    Console.WriteLine(firstPerson);

    // try initialization syntax!
    Dictionary<string, Person> nicknames = new() {
        { "Alpha", new() { FirstName ="Biggy", LastName = "Smalls", Age = 45} },
        { "AlphaAndOmega", new() { FirstName = "Ralph", LastName = "Peters", Age = 65 } }
    };

    // this is also valid
    //{ ["Homer"] = new(){ FirstName = "hi", LastName = "There", Age = 120}}


    foreach (var keyVal in nicknames) {
        Console.WriteLine("{0}'s nickname is: {1}", keyVal.Value, keyVal.Key);
    }
}

UseDictionary();

// useful collections.ObjectModel classes.

//ObservableCollection<T> - informs external objects when contents change

ObservableCollection<Person> observableFellas = new() {
    new() { FirstName = "Peter", LastName = "Murphy", Age = 52},
    new() { FirstName = "Kevin", LastName = "Key", Age = 48}
};

// I don't understand this syntax. Learn it more!
observableFellas.CollectionChanged += people_CollectionChanged;

static void people_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
    // NotifyCollectionChangedEventArgs has - OldItems, and NewItems.

    // An enum (NotifyCollectionChangedAction) defines the type of action. 0 = Add, 1 = Remove, 2 = Replace, 3 = Move, 4 = Reset.

    if (e.Action == NotifyCollectionChangedAction.Remove) {
        Console.WriteLine("Here are old items");
        foreach (Person p in e.OldItems) {
            Console.WriteLine(p.ToString());
        }
    }

    if (e.Action == NotifyCollectionChangedAction.Add) {
        Console.WriteLine("New items:");

        foreach (Person p in e.NewItems) {
            Console.WriteLine(p.ToString());
        }
    }
}

observableFellas.Add(new Person { FirstName = "Walt", LastName = "Whitman", Age = 65 });
observableFellas.RemoveAt(0);

