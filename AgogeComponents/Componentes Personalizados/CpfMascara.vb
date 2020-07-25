Public Class CpfMascara
    Inherits MaskedTextBox

    Public Sub New()
        MyBase.New()
        MyBase.Mask = "000\.000\.000\-00"
    End Sub

End Class
