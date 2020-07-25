Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Web.Services.Protocols

Public MustInherit Class ServidorConexao

    Property Servidor As Servidor

    Public Sub New(ByVal Servidor As Servidor)
        Me.Servidor = Servidor
    End Sub

    Protected MustOverride Sub InstanciarObjetos(ByRef Wse As SoapHttpClientProtocol, ByRef Wsl As SoapHttpClientProtocol, ByRef SSRS As SoapHttpClientProtocol, ByRef SSRSExec As SoapHttpClientProtocol, ByRef CC As Net.CookieContainer)
    Protected MustOverride Sub Logar()

    Private Wse As SoapHttpClientProtocol
    Private Wsl As SoapHttpClientProtocol

    Private SSRS As SoapHttpClientProtocol
    Private SSRSExec As SoapHttpClientProtocol

    Private CC As Net.CookieContainer

    Private _Conectado As Boolean = False
    Property Conectado As Boolean
        Get
            Return _Conectado
        End Get
        Set(value As Boolean)
            If value = True Then
                If Not _Conectado Then
                    Conectar()
                    _Conectado = True
                End If
            Else
                InstanciarObjetos(Wse, Wsl, SSRS, SSRSExec, CC)
                _Conectado = False
            End If

        End Set
    End Property

    Private Sub Conectar()
        InstanciarObjetos(Wse, Wsl, SSRS, SSRSExec, CC)

        Me.Wse.Url = Me.Servidor.RemoteWseUrl
        Me.Wsl.Url = Me.Servidor.RemoteWslUrl
        Me.SSRS.Url = Me.Servidor.RemoteSSRSUrl
        Me.SSRSExec.Url = Me.Servidor.RemoteSSRSExecUrl

        Me.Wsl.CookieContainer = CC
        Me.Wsl.EnableDecompression = True
        Dim WslCredential As New CredentialCache
        WslCredential.Add(New Uri(Me.Servidor.WsServer), "Basic", New NetworkCredential(Me.Servidor.WsUser, Me.Servidor.WsPass))
        Wsl.Credentials = WslCredential

        Me.Wse.CookieContainer = CC
        Me.Wse.EnableDecompression = True
        Dim WseCredential As New CredentialCache
        WseCredential.Add(New Uri(Me.Servidor.WsServer), "Basic", New NetworkCredential(Me.Servidor.WsUser, Me.Servidor.WsPass))
        Wse.Credentials = WseCredential

        Me.Wse.Timeout = 5 * 60 * 1000 '5 Minutos

        Net.ServicePointManager.ServerCertificateValidationCallback = _
        Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True

        Try
            Me.Logar()
        Catch ex As Exception

            If InStr(ex.Message, "ERRO_VERSAO", CompareMethod.Text) Then
                Throw New Exception("Versão desatualizada, baixe nova versão")
            ElseIf InStr(ex.Message, "ERRO_LOGIN", CompareMethod.Text) Then
                Throw New Exception("Falha na conexão com servidor")
            Else
                Throw New Exception(ex.Message, ex.InnerException)
            End If
        End Try

    End Sub

End Class
