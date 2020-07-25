Public Class CepMascara
    Inherits MaskedTextBox

    Public Sub New()
        MyBase.New()
        MyBase.Mask = "00\.000\-00"
    End Sub

End Class
