Imports System
Imports System.Windows.Forms

Public Class MaskedTextBox
    Inherits System.Windows.Forms.MaskedTextBox

    Public Sub New()
        MyBase.New()
        MyBase.HidePromptOnLeave = True
        MyBase.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        MyBase.InsertKeyMode = Windows.Forms.InsertKeyMode.Overwrite
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        MyBase.SelectionStart = 0
    End Sub

End Class
