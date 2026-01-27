
// CompanyName.ProductName.AssemblyName.Path.

// best-practice for related classes is to make separate .cs files and use the same namespace definitions.

// you can have a top-level namespace declaration and the rest of the file will behave as if it was in a namespace {} block

// if you have name clashes or classes with the same names, use fully qualified names.

// aliases! 
// type alias: using 3DHexagon = My3DShapes.Hexagon;
// namespace alias: using ThreeD = CustomNamespaces.My3DShapes;

// nested namespaces can be done with nested namespace blocks namespace space { namespace solarSystem {} }, or with a file-level namespace and a block namespace
// namespace space;
// namespace SolarSystem {}

// have namespaces match folder structure

// in the project file, 	  <RootNamespace>PUT YOUR ROOT NAMESPACE HERE</RootNamespace> in <PropertyGroup> also configures the root namespace.

// FIND HEADERS FOR A .DLL WITH:
// dumpbin /headers file.dll
// dumpbin /clrheader file.dll