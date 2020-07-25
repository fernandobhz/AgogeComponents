Public Class CadCode

    Public Sub Criar(Entidade As EntidadeSigedin)
        Dim f As New System.IO.StreamWriter(Entidade.CadCodeFile, False, System.Text.Encoding.UTF8)

        f.WriteLine("Public Class cad" & Entidade.NomeEntidade & "Sigedin")
        f.WriteLine("    Inherits FormularioCrud")
        f.WriteLine("")
        f.WriteLine("    Protected Sub New()")
        f.WriteLine("        Me.InitializeComponent()")
        f.WriteLine("        Me.Registrar(Me.ds" & Entidade.NomeEntidade & ", Me.BS, Ule.cd_usuario, _ ")
        f.WriteLine("                     AddressOf Wse.lok_" & Entidade.NomeEntidade & ", AddressOf Wse.get_" & Entidade.NomeEntidade & ", AddressOf Wse.set_" & Entidade.NomeEntidade & ", _")
        f.WriteLine("                     Me.btnExcluir, Me.btnCancelar, Me.btnSalvar)")
        f.WriteLine("    End Sub")
        f.WriteLine("")
        f.WriteLine("End Class")

        f.Close()

        If Not System.IO.File.Exists(Entidade.CadInheritedFile) Then
            f = New System.IO.StreamWriter(Entidade.CadInheritedFile, False, System.Text.Encoding.UTF8)
            f.WriteLine("Public Class cad" & Entidade.NomeEntidade)
            f.WriteLine("   Inherits cad" & Entidade.NomeEntidade & "Sigedin")
            f.WriteLine("End Class")
            f.Close()
        End If
    End Sub

End Class
