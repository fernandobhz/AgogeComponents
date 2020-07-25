Imports System.Data.SqlClient

Public Class ProjetoSigedin
    Property DatabaseName As String

    Private _PastaProjeto As String
    Property PastaProjeto As String
        Get
            Return _PastaProjeto
        End Get
        Set(value As String)
            _PastaProjeto = TrailingBackSlash(value)
        End Set
    End Property

    Property WseFile As String

    <System.Diagnostics.DebuggerNonUserCode()> _
    Sub New(DatabaseName As String, PastaProjeto As String, WseFile As String)
        Me.DatabaseName = DatabaseName
        Me.PastaProjeto = PastaProjeto
        Me.WseFile = WseFile
    End Sub

    ReadOnly Property ConnStr As String
        Get
            Return "Data Source=LOCALHOST;Initial Catalog=" & DatabaseName & ";Current Language=Portuguese;Integrated Security=true"
        End Get
    End Property


    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Function ValorDB(ByVal SQL As String) As Object
        Using SQLConn As SqlConnection = ObterConexaoDB(), _
        SQLCmd As New SqlCommand(SQL, SQLConn)
            Return SQLCmd.ExecuteScalar
        End Using
    End Function

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Function ConsultaDB(ByVal SQL As String) As SqlDataReader
        Dim SQLConn As SqlConnection = ObterConexaoDB(), _
        SQLCmd As New SqlCommand(SQL, SQLConn)
        Return SQLCmd.ExecuteReader
    End Function

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Function ExecutaDB(ByVal SQL As String) As Object
        Using SQLConn As SqlConnection = ObterConexaoDB(), _
        SQLCmd As New SqlCommand(SQL, SQLConn)
            Return (SQLCmd.ExecuteNonQuery())
        End Using
    End Function

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Function ObterConexaoDB() As SqlConnection
        Dim SQLConn As New SqlConnection(Me.ConnStr)
        SQLConn.Open()

        Dim SQLCmd As New SqlCommand("SET DATEFORMAT DMY;", SQLConn)
        SQLCmd.ExecuteNonQuery()
        SQLCmd.Dispose()

        Return SQLConn
    End Function


End Class
