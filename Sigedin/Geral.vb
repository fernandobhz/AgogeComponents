Public Module Geral
    Dim CadDesigner As New CadDesigner()
    Dim CadCode As New CadCode()

    Dim PesDesigner As New PesDesigner()
    Dim PesCode As New PesCode

    Dim WseCode As New WseCode

    Dim Brilho As New ProjetoSigedin(DatabaseName:="brilho_desenv", _
                                     PastaProjeto:="E:\Dropbox\Clientes\BRILHO\BRILHONET\trunk\BrilhoVB.NET\BrilhoApp", _
                                     WseFile:="E:\Dropbox\Clientes\BRILHO\BRILHONET\trunk\BrilhoASP.NET\BrilhoWebService\wse.asmx.vb")


    Public Sub CriaCad(ByVal NomeEntidade As String)
        CriaCad(NomeEntidade, NomeEntidade)
    End Sub

    Public Sub CriaCad(ByVal NomeEntidade As String, NomeCadastro As String)
        Dim Projeto As ProjetoSigedin = Geral.Brilho
        Dim Entidade As New EntidadeSigedin(Projeto, NomeEntidade, NomeCadastro)

        CadDesigner.Criar(Entidade)
        CadCode.Criar(Entidade)
        Debug.Print("OK")
    End Sub




    Public Sub CriaPes(ByVal NomeEntidade As String)
        CriaPes(NomeEntidade, NomeEntidade)
    End Sub

    Public Sub CriaPes(ByVal NomeEntidade As String, NomeCadastro As String)
        Dim Projeto As ProjetoSigedin = Geral.Brilho
        Dim Entidade As New EntidadeSigedin(Projeto, NomeEntidade, NomeCadastro)

        PesDesigner.Criar(Entidade)
        PesCode.Criar(Entidade)
        Debug.Print("OK")
    End Sub


    Public Sub CriaWse(ByVal NomeEntidade As String)
        Dim Projeto As ProjetoSigedin = Geral.Brilho
        Dim Entidade As New EntidadeSigedin(Projeto, NomeEntidade, NomeEntidade)

        WseCode.Criar(Entidade)
        Debug.Print("OK")
    End Sub

End Module
