Public Class NumeroTB
    Inherits TextBox

    Public Sub New()
        MyBase.New()
        MyBase.MaxLength = 9
    End Sub

    Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        If ((Not Char.IsNumber(e.KeyChar)) And (Not Char.IsControl(e.KeyChar))) Then
            e.Handled = True
        End If
    End Sub
End Class
