Public Class ComboBox
    Inherits System.Windows.Forms.ComboBox

    Property ListLimited As Boolean

    Protected Overrides Sub OnValidating(e As System.ComponentModel.CancelEventArgs)

        If Me.ListLimited Then
            If Me.FindStringExact(Text) = -1 Then
                e.Cancel = True
                Me.DroppedDown = True
            End If
            MyBase.OnValidating(e)
        End If

    End Sub

End Class
