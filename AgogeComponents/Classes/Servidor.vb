Public Class Servidor
    Implements ICloneable

    Private _NomeServidor As String
    Property NomeServidor As String
        Get
            Return _NomeServidor
        End Get
        Private Set(value As String)
            _NomeServidor = value
        End Set
    End Property

    Private _WsServer As String
    Property WsServer As String
        Get
            Return _WsServer
        End Get
        Private Set(value As String)
            _WsServer = TrailingSlash(value)
        End Set
    End Property

    Private _WsUser As String
    Property WsUser As String
        Get
            Return _WsUser
        End Get
        Private Set(value As String)
            _WsUser = value
        End Set
    End Property

    Private _WsPass As String
    Property WsPass As String
        Get
            Return _WsPass
        End Get
        Private Set(value As String)
            _WsPass = value
        End Set
    End Property

    Private _RptServer As String
    Property RptServer As String
        Get
            Return _RptServer
        End Get
        Private Set(value As String)
            _RptServer = TrailingSlash(value)
        End Set
    End Property

    Private _RptUser As String
    Property RptUser As String
        Get
            Return _RptUser
        End Get
        Private Set(value As String)
            _RptUser = value
        End Set
    End Property

    Private _RptPass As String
    Property RptPass As String
        Get
            Return _RptPass
        End Get
        Private Set(value As String)
            _RptPass = value
        End Set
    End Property

    Private _RptDomain As String
    Property RptDomain As String
        Get
            Return _RptDomain
        End Get
        Private Set(value As String)
            _RptDomain = value
        End Set
    End Property

    Private _Cor As Drawing.Color
    Public Property Cor() As Drawing.Color
        Get
            Return _Cor
        End Get
        Set(ByVal value As Drawing.Color)
            _Cor = value
        End Set
    End Property

    Private _CloudStorage As String
    Public Property CloudStorage() As String
        Get
            Return _CloudStorage
        End Get
        Set(ByVal value As String)
            _CloudStorage = value
        End Set
    End Property


    Public Sub New(ByVal NomeServidor As String, _
                   ByVal WsServer As String, WsUser As String, WsPass As String, _
                   ByVal RptServer As String, ByVal RptUser As String, ByVal RptPass As String, RptDomain As String, _
                   ByVal Cor As Drawing.Color, ByVal CloudStorage As String)

        Me.NomeServidor = NomeServidor
        Me.WsServer = WsServer
        Me.WsUser = WsUser
        Me.WsPass = WsPass

        Me.RptServer = RptServer
        Me.RptUser = RptUser
        Me.RptPass = RptPass
        Me.RptDomain = RptDomain

        Me.Cor = Cor
        Me.CloudStorage = CloudStorage
    End Sub

    ReadOnly Property RemoteWseUrl As String
        Get
            Return Me.WsServer & "Wse.asmx"
        End Get
    End Property

    ReadOnly Property RemoteWslUrl As String
        Get
            Return Me.WsServer & "Wsl.asmx"
        End Get
    End Property

    ReadOnly Property RemoteSSRSUrl As String
        Get
            Return Me.RptServer & "ReportService2005.asmx?wsdl"
        End Get
    End Property

    ReadOnly Property RemoteSSRSExecUrl As String
        Get
            Return Me.RptServer & "reportexecution2005.asmx"
        End Get
    End Property

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New Servidor(Me.NomeServidor, Me.WsServer, Me.WsUser, Me.WsPass, Me.RptServer, Me.RptUser, Me.RptPass, Me.RptDomain, Me.Cor, Me.CloudStorage)
    End Function

End Class
