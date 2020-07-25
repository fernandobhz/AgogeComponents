Public Class HoraMascara
    Inherits MaskedTextBox

    Public Sub New()
        MyBase.New()
        MyBase.Mask = "00:00:00"
        MyBase.TextMaskFormat = MaskFormat.IncludePromptAndLiterals
    End Sub

End Class
