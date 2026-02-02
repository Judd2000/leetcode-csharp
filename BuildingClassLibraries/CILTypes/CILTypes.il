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

    } 

    .class public MyDerivedClass extends MyNamespace.MyBaseClass {

    }
} 