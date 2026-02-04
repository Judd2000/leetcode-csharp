using System.Reflection;
using System.Reflection.Emit;

Console.WriteLine("***** Dynamic Assembly App *****");
AssemblyBuilder builder = CreateAssembly();

// get HelloWorld type
Type hello = builder.GetType("My_Assembly.HelloWorld");

// create instance and call constructor
Console.WriteLine("Enter message to pass HelloWorld class:");
string msg = Console.ReadLine();
object[] constructorArgs = new object[1];
constructorArgs[0] = msg;

object obj = Activator.CreateInstance(hello, constructorArgs); //now we can use the activator to create a class instance.

// Call Say Hello
Console.WriteLine("Calling SayHello via late binding...");
MethodInfo methodInformation = hello.GetMethod("SayHello");
methodInformation.Invoke(obj, null);

// invoke
methodInformation = hello.GetMethod("GetMsg");
Console.WriteLine("Invoking GetMsg {0}", methodInformation.Invoke(obj, null));

static AssemblyBuilder CreateAssembly() {
    AssemblyName name = new() { Name = "My_Assembly", Version = new Version("1.0.0.0") };

    var builder = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);

    ModuleBuilder module = builder.DefineDynamicModule("My_Assembly"); 

    // public Hello World class
    TypeBuilder helloWorldClass = module.DefineType("My_Assembly.HelloWorld", TypeAttributes.Public); //key type during dynamic assembly, well, assembly.

    // private string var
    FieldBuilder msgField = helloWorldClass.DefineField("_theMessage", Type.GetType("System.String"), attributes:FieldAttributes.Public); //attributes: seems to be a clearer way to say, this is
    // the attributes input

    Type[] constructorArguments = new Type[1];

    constructorArguments[0] = typeof(string);

    ConstructorBuilder constructor = helloWorldClass.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, constructorArguments);

    ILGenerator constructorGeneratorIl = constructor.GetILGenerator();
    constructorGeneratorIl.Emit(OpCodes.Ldarg_0); //load arg onto stack
    Type objClass = typeof(object);
    ConstructorInfo superConstructor = objClass.GetConstructor([]);

    constructorGeneratorIl.Emit(OpCodes.Call, superConstructor);
    constructorGeneratorIl.Emit(OpCodes.Ldarg_0);
    constructorGeneratorIl.Emit(OpCodes.Ldarg_1); //pushed args 0 and 1 to stack
    constructorGeneratorIl.Emit(OpCodes.Stfld, msgField); // push string onto stack
    constructorGeneratorIl.Emit(OpCodes.Ret); //return value.

    // Def constructor
    helloWorldClass.DefineDefaultConstructor(MethodAttributes.Public);

    // GetMsg() prop method
    MethodBuilder getMsgMethod = helloWorldClass.DefineMethod("GetMsg", MethodAttributes.Public, typeof(string), null); // no params so null is last input
    ILGenerator methodIl = getMsgMethod.GetILGenerator();
    methodIl.Emit(OpCodes.Ldarg_0); // push 'this' to stack
    methodIl.Emit(OpCodes.Ldfld, msgField); // load message field
    methodIl.Emit(OpCodes.Ret);

    // HelloWorld method

    MethodBuilder sayHello = helloWorldClass.DefineMethod("SayHello", MethodAttributes.Public, null, null);
    methodIl = sayHello.GetILGenerator();
    methodIl.EmitWriteLine("Hello from Hello World Class");
    methodIl.Emit(OpCodes.Ret);

    // now we can emit it.
    helloWorldClass.CreateType();

    return builder;
}