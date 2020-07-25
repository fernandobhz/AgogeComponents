Imports System
Imports System.Windows.Forms

Public Class DataMascara
    Inherits MaskedTextBox

    Public Sub New()
        MyBase.New()
        MyBase.HidePromptOnLeave = True
        MyBase.TextMaskFormat = MaskFormat.IncludeLiterals
        MyBase.InsertKeyMode = Windows.Forms.InsertKeyMode.Overwrite
        MyBase.Mask = "00/00/0000"
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        MyBase.SelectionStart = 0
    End Sub

End Class
