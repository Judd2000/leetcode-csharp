.assembly extern System.Runtime {
    .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A)
    .ver 10:0:0:0
}

.assembly extern System.Runtime.Extensions {
    .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A)
    .ver 10:0:0:0
}

.assembly extern mscorlib {
    .publickeytoken = (B7 7A 5C 56 19 34 E0 89)
    .ver 10:0:0:0
}

.assembly CILTypes {
    .ver 1:0:0:0
}

.module CILTypes.dll

.namespace MyNamespace { //You can define nested namespaces, too.
    .class public MyBaseClass { //use 'extends' for derived classes
        .field private string stringField = "whataburger"
        .field private int32 intField = int32(42)

        // constructor
        .method public hidebysig specialname rtspecial-name instance void .ctor(string s, int32 i) cil managed {
            // implementation code
        }

        // Properties
        .method public hidebysig specialname instance string get_TheString() cil managed {
            // get underlying value
        }

        .method public hidebysig specialname instance void set_TheString(string 'value') cil managed {
            // implement setter here.
         }

         .property instance string TheString() { //map property syntax to the methods we defined above.
            .get instance string MyNamespace.MyBaseClass::get_TheString()

            .set instance void MyNamespace.MyBaseClass::set_TheString(string)
         }

         // The following method in C#:
         // public static void MyMethod(int input, ref int refInt, ArrayList ar, out int outputInt) outputInt = 0;

         .method public hidebysig static void MyMethod(int32 input, int32& refInt, class [System.Runtime.Extensions]System.Collections.ArrayList ar, [out] int32& outputInt) cil managed {}
    } 

    .class public interface IMyInterface {}

    .class public MyDerivedClass 
        extends MyNamespace.MyBaseClass
        implements MyNamespace.IMyInterface {

    }

    .class public interface IOtherInterface implements MyNamespace.IMyInterface {}

    // to get a struct, create a sealed class that extends System.ValueType
    .class public sealed MyStruct extends [System.Runtime]System.ValueType {}

    // you can also define it as a class value type
    .class public sealed value MyOtherStruct {}

    // enum is shorthand for 'extends [System.Runtime]System.Enum {}
    .class public sealed enum MyEnum {
        .field public static literal valuetype MyNamespace.MyEnum A = int32(0)
        .field public static literal valuetype MyNamespace.MyEnum B = int32(1)
        .field public static literal valuetype MyNamespace.MyEnum C = int32(2)
    }

    .method public hidebysig static void MyLocalVariables() cil managed {
        .locals init (string myStr, int32 myInt, object myObj) //allocate locals with init


        // load string onto exec stack
        loadstr "CIL Code Is WILD"
        stloc.0 // pop off valuie and store in local var 0

        // load constant of type i4 (int32)
        ldc.i4.s 33
        // pop stack and store in local var 1
        stloc.1

        // create obj on stack
        newobj instance void [mscorlib]System.Object::ctor()

        stloc.2 //pop current value and save to local at [2]
        ret
    }

    // a simple add method becomes pretty verbose in CIL:
    .method public hidebysig static int32 Add(int32 a, int32 b) cil managed { //if this was a nonstatic function, the first larg is 'this' so you'd use 1 and 2
        .maxstack 2
        ldarg.0 //load a onto stack
        ldarg.1 // load b onto stack
        add (takes top two and pushes onto stack)
        ret //pops and returns
    }

    // loops in C# are standard. Think of CIL loops like y86 loops, meaning you have jumps to tags in the code.
} 