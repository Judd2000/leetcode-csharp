Imports CommonSnappableTypes

<CompanyInfo(CompanyName:="Scammers of America", CompanyUrl:="www.get-scammed.org")>
Public Class VBSnapIn
    Implements IAppFunctionality

    Public Sub DoIt() Implements IAppFunctionality.DoIt
        Console.WriteLine("VB Snap-In is, well...SNAPPED IN!")
    End Sub
End Class
