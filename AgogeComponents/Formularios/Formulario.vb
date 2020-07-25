Public Class Formulario
    Inherits Form

    Private Shadows Sub Show()
    End Sub

    Private Shadows Sub ShowDialog()
    End Sub

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    'QUALQUER acesso a Me.Handle faz o formulário principal minimizar quando um popUp minimiza também.

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    'Dim hkr As New HKey(Me.Handle)
    'Private Acoes As New List(Of AcaoDelegate)

    'Protected Sub RegistrarAtalhoGlobal(ByVal Modifier As HKey.Mods, ByVal Key As System.Windows.Forms.Keys, Acao As AcaoDelegate)
    '    Me.hkr.Register(Modifier, Key)
    '    Me.Acoes.Add(Acao)
    'End Sub

    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    '    If m.Msg = HKey.Mensagens.HotKey Then Me.Acoes(m.WParam.ToInt32).Invoke()
    '    MyBase.WndProc(m)
    'End Sub

    Protected Sub FocusMe()
        TopMost = True
        Focus()
        BringToFront()
        TopMost = False
    End Sub

End Class
