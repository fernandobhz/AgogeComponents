Public Class Parametro
    Property Nome As String
    Property Valor As Object

    Sub New(ByVal Nome As String, ByVal Valor As Object)
        Me.Nome = Nome
        Me.Valor = Valor
    End Sub
End Class