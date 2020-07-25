Public Class FormularioDialogo
    Inherits Formulario

    Sub New()
        Me.BackColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.MaximizeBox = False
        Me.MinimizeBox = False
    End Sub

End Class
