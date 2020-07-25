Imports System.Data.SqlClient

Public Class PesCode

    Public Sub Criar(Entidade As EntidadeSigedin)
        Dim f As New System.IO.StreamWriter(Entidade.PesCodeFile, False, System.Text.Encoding.UTF8)

        f.WriteLine("Public Class pes" & Entidade.NomeEntidade & "Sigedin")
        f.WriteLine("    Inherits FormularioPesquisa")
        f.WriteLine("")
        f.WriteLine("    Sub New()")
        f.WriteLine("        Me.InitializeComponent()")
        f.WriteLine("        Me.Registrar(Me.DS" & Entidade.NomeEntidade & ", Me.DS" & Entidade.NomeEntidade & ".ps_" & Entidade.NomeEntidade & ", _")
        f.WriteLine("                     GetType(cad" & Entidade.NomeEntidade & "), Me.PesquisaDataGridView, _")
        f.WriteLine("                     AddressOf Wse.lok_" & Entidade.NomeEntidade & ", AddressOf Me.Pesquisar, _")
        f.WriteLine("                     Me.btnPesquisar, Me.btnCadastrar)")
        f.WriteLine("    End Sub")
        f.WriteLine("")
        f.WriteLine("    Protected Sub Pesquisar()")
        f.WriteLine("")
        f.WriteLine("            Me.ds" & Entidade.NomeEntidade & ".Merge(Wse.pes_" & Entidade.NomeEntidade & "( _")

        Dim rs As SqlDataReader = Entidade.Projeto.ConsultaDB("select * from master..sigedin_params where banco = '" & Entidade.Projeto.DatabaseName & "' and procedimento = 'ps_" & Entidade.NomeEntidade & "' order by parameter_id")
        Dim a As String
        a = ""
        Do While rs.Read
            a = a & "                " & rs!parametro & ":=("
            If rs!tipo_dado = "varchar" Or rs!tipo_dado = "char" Then
                a = a & "FetchStr"
            ElseIf rs!tipo_dado = "date" Then
                a = a & "FetchDate"
            ElseIf rs!tipo_dado = "bit" Then
                a = a & "FetchCheck"
            Else
                a = a & "FetchInt"
            End If
            a = a & "(Me." & rs!parametro & "." & rs!cad_campo_ligacao & ")), _" & vbCrLf
        Loop
        rs.Close()

        a = Mid(a, 1, Len(a) - 5)
        a = a & " _"

        f.WriteLine(a)

        f.WriteLine("                ))")
        f.WriteLine("")
        f.WriteLine("        Me.Tab.SelectedTab = Me.TabResultado")
        f.WriteLine("        Me.TotalRegValor.Text = Me.BS.Count")
        f.WriteLine("")
        f.WriteLine("    End Sub")
        f.WriteLine("")
        f.WriteLine("End Class")

        f.Close()

        If Not System.IO.File.Exists(Entidade.PesInheritedFile) Then
            f = New System.IO.StreamWriter(Entidade.PesInheritedFile, False, System.Text.Encoding.UTF8)
            f.WriteLine("Public Class pes" & Entidade.NomeEntidade)
            f.WriteLine("   Inherits pes" & Entidade.NomeEntidade & "Sigedin")
            f.WriteLine("End Class")
            f.Close()
        End If
    End Sub

End Class
