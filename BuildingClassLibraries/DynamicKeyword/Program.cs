using DynamicKeyword;

static void ImplicitlyTyped() {
    var a = new List<int> { 10, 20, 30, 40, 50 };
    // despite being 'dynamic', a cannot be assigned to another type after this.
    //a = "What"; Even though this is an implicitly typed variable, it becomes strongly typed on assignment.
}

static void UseObjectVar() {
    object o = new Person() { FirstName = "Nathaniel", LastName = "Judd" };

    // cast to Person to gain access to Person props
    Console.WriteLine("Person's firstname: {0}", ((Person)o).FirstName);
}

// dynamic keyword - specialized System.Object that can be assigned to a dynamic data type.
// Dynamic data is not statically typed. The compiler accepts dynamic data being assigned to anything and reassigned to any new value.

static void ChangeDynamicDataType() {
    dynamic t = "Hoopla!";
    Console.WriteLine($"T is of type {t.GetType()}");

    t = false;
    Console.WriteLine($"T is of type {t.GetType()}");

    t = new List<List<int>>() { new() { 4, 5, 6 }, new() { 7, 8, 9 } };
    Console.WriteLine($"T is of type {t.GetType()}");
}

ChangeDynamicDataType();
