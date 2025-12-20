using System;

// IMPORT STATIC MEMBERS USING 'using static'
// Although they can lead to confusion on where code comes from.
using static System.Console;
using static System.DateTime;

namespace StaticDataAndMembers;

public class SavingsAccount
{
    // allocated on a per-Object basis
    public double currBalance;

    // Shared among ALL class instances. Perfect for values common to all of the objs.
    public static double currInterestRate = 0.04;

    public SavingsAccount(double balance)
    {
        currInterestRate = 0.04;
        currBalance = balance;
    }

    // Static Constructor. Great place to initialize values of data
    // not known at runtime and shared across class instances.

    // Before the first instance is created, static constructor called.
    // Before any static member is accessed, the static constructor is called.

    static SavingsAccount() {
        Console.WriteLine("IN static constructor");
        currInterestRate = 0.04;
    }

    // Static Methods
    public static double GetInterestRate()
    {
        return currInterestRate;
    }

    public static void SetInterestRate(double newRate)
    {
        currInterestRate = newRate;
    }

    // FULLY static classes - not made using new, only can contain static fields and methods.
}
// This has no instances so it is really just a grouping of vals and methods.
static class TimeUtilClass {
    public static void PrintTime() {
        Console.WriteLine(Now.ToShortTimeString());
    }

    public static void PrintDate() {
        Console.WriteLine(Today.ToShortDateString());
    }
}
