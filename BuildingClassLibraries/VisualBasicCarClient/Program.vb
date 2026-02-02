Imports System
Imports CarLibrary

Module Program
    Sub Main(args As String())
        Console.WriteLine("** Visual Basic Client App **")
        Dim myMiniVan As New MiniVan()
        myMiniVan.TurboBoost()

        Dim mySportsCar As New SportsCar()
        mySportsCar.TurboBoost()

        Dim aCar As New PerformanceCar()

        aCar.Name = "Hank"
        aCar.TurboBoost()

        'we cannot reference internal class because the library hasn't granted us permission.
        'Dim internalInstance As New MyInternalClass()
    End Sub
End Module
