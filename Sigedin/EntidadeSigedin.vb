Public Class EntidadeSigedin
    Property Projeto As ProjetoSigedin

    Private _NomeEntidade As String
    Property NomeEntidade As String
        Get
            Return _NomeEntidade
        End Get
        Set(value As String)
            If Not String.IsNullOrEmpty(value) Then
                _NomeEntidade = Left(value, 1).ToUpper & Mid(value, 2)
            Else
                _NomeEntidade = String.Empty
            End If
        End Set
    End Property

    Property NomeCadastro As String

    Property SubPasta As String

    Property Identificador As String
    Property IdenTipo As String
    Property TipoCad As String

    <System.Diagnostics.DebuggerNonUserCode()> _
    Sub New(Projeto As ProjetoSigedin, NomeEntidade As String, NomeCadastro As String, Optional SubPasta As String = "Cadastros")
        Me.Projeto = Projeto
        Me.NomeEntidade = NomeEntidade
        Me.NomeCadastro = NomeCadastro
        Me.SubPasta = SubPasta

        Identificador = Me.Projeto.ValorDB("select coluna from master..sigedin_coluna where banco = '" & Me.Projeto.DatabaseName & "' and identificador = 1 and tabela = '" & Me.NomeEntidade & "'")
        IdenTipo = Me.Projeto.ValorDB("select tipo_dado from master..sigedin_coluna where banco = '" & Me.Projeto.DatabaseName & "' and identificador = 1 and tabela = '" & Me.NomeEntidade & "'")
        TipoCad = Me.Projeto.ValorDB("select tipo_cad from master..sigedin_tabela where banco = '" & Me.Projeto.DatabaseName & "' and tabela = '" & Me.NomeEntidade & "'")
    End Sub

    ReadOnly Property Pasta As String
        Get
            Return Me.Projeto.PastaProjeto & Me.SubPasta & "\"
        End Get
    End Property

    ReadOnly Property CadCodeFile As String
        Get
            Return Me.Pasta & "cad" & NomeEntidade & "Sigedin.vb"
        End Get
    End Property

    ReadOnly Property CadDesignerFile As String
        Get
            Return Me.Pasta & "cad" & NomeEntidade & "Sigedin.Designer.vb"
        End Get
    End Property

    ReadOnly Property CadInheritedFile As String
        Get
            Return Me.Pasta & "cad" & NomeEntidade & ".vb"
        End Get
    End Property

    ReadOnly Property PesCodeFile As String
        Get
            Return Me.Pasta & "pes" & NomeEntidade & "Sigedin.vb"
        End Get
    End Property

    ReadOnly Property PesDesignerFile As String
        Get
            Return Me.Pasta & "pes" & NomeEntidade & "Sigedin.Designer.vb"
        End Get
    End Property

    ReadOnly Property PesInheritedFile As String
        Get
            Return Me.Pasta & "pes" & NomeEntidade & ".vb"
        End Get
    End Property

End Class
