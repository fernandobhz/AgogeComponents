Public Class TelefoneMascara
    Inherits MaskedTextBox

    Public Sub New()
        MyBase.New()
        MyBase.Mask = "\(00\)\ 0000\-0000"
    End Sub

End Class
