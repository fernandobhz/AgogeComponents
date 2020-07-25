Namespace Storage

    Public Class URL
        Implements ICloneable

        Private _Protocol As String
        Public Property Protocol() As String
            Get
                Return _Protocol
            End Get
            Set(ByVal value As String)
                If value.IndexOf(" ") >= 0 Then Throw New Exception("Protocol may not contain spaces")
                If String.IsNullOrEmpty(value) Then Throw New Exception("Protocol can't be Null Or Empty")
                _Protocol = value
            End Set
        End Property

        Private _Host As String
        Public Property Host() As String
            Get
                Return System.Web.HttpUtility.UrlDecode(_Host)
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) Then Throw New Exception("Host can't be Null Or Empty")
                _Host = System.Web.HttpUtility.UrlEncode(value)
            End Set
        End Property

        Private _Port As Nullable(Of Integer)
        Public Property Port() As Nullable(Of Integer)
            Get
                Return _Port
            End Get
            Set(ByVal value As Nullable(Of Integer))
                If value < 0 Or value > 65535 Then Throw New Exception("Port range must be between 0 and 65535")
                _Port = value
            End Set
        End Property

        Private _Resource As String
        Public Property Resource() As String
            Get
                'Return System.Web.HttpUtility.UrlDecode(_Resource)
                Return System.Web.HttpUtility.UrlDecode(_Resource)
            End Get
            Set(ByVal value As String)
                Dim Parts As String() = value.Split("/")
                For i As Integer = 0 To Parts.Count - 1
                    'Parts(i) = System.Web.HttpUtility.UrlEncode(Parts(i))
                    Parts(i) = System.Web.HttpUtility.UrlPathEncode(Parts(i))
                Next

                _Resource = String.Join("/", Parts)

                If value.EndsWith("/") Then
                    _Resource = TrailingSlash(_Resource)
                End If
            End Set
        End Property

        Private _Username As String
        Public Property Username() As String
            Get
                Return System.Web.HttpUtility.UrlDecode(_Username)
            End Get
            Set(ByVal value As String)
                _Username = System.Web.HttpUtility.UrlPathEncode(value)
            End Set
        End Property

        Private _Password As String
        Public Property Password() As String
            Get
                Return System.Web.HttpUtility.UrlDecode(_Password)
            End Get
            Set(ByVal value As String)
                _Password = System.Web.HttpUtility.UrlEncode(value)
            End Set
        End Property

        Sub New(ByVal URL As String)
            's3://acesskey:secretkey@endpoint/bucket/filekey.txt
            'ftp://usuario:senha@host:21/resource/location/resource.txt

            Dim PontoInicial As Integer, PontoFinal As Integer

            'Protocolo
            PontoInicial = 0
            PontoFinal = URL.IndexOf("://")

            If PontoFinal < 0 Then Throw New Exception("Invalid URL, missing ://")

            Me._Protocol = URL.Substring(PontoInicial, PontoFinal - PontoInicial)
            PontoInicial = PontoFinal + 3

            'Username Password
            PontoFinal = URL.IndexOf("@")
            If PontoFinal >= 0 Then
                Dim Credentials As String = URL.Substring(PontoInicial, PontoFinal - PontoInicial)

                If Credentials.Contains(":") Then
                    'Contains the username and password

                    Me._Username = Credentials.Split(":")(0)
                    Me._Password = Credentials.Split(":")(1)
                Else
                    'Only conaints the username
                    Me._Username = Credentials
                End If

                PontoInicial = PontoFinal + 1
            End If

            'Host
            PontoFinal = URL.IndexOf(":", PontoInicial)
            If PontoFinal >= 0 Then
                'HOST

                Me._Host = URL.Substring(PontoInicial, PontoFinal - PontoInicial)
                PontoInicial = PontoFinal + 1

                'PORT
                PontoFinal = URL.IndexOf("/", PontoInicial)
                If PontoFinal < 0 Then PontoFinal = URL.Length

                If Not PontoFinal = PontoInicial Then
                    Me.Port = URL.Substring(PontoInicial, PontoFinal - PontoInicial)
                End If

                If PontoFinal < URL.Length Then
                    PontoInicial = PontoFinal + 1
                Else
                    PontoInicial = PontoFinal
                End If
            Else
                'HOST ONLY

                PontoFinal = URL.IndexOf("/", PontoInicial)
                If PontoFinal < 0 Then PontoFinal = URL.Length

                Me._Host = URL.Substring(PontoInicial, PontoFinal - PontoInicial)

                If PontoFinal < URL.Length Then
                    PontoInicial = PontoFinal + 1
                Else
                    PontoInicial = PontoFinal
                End If
            End If

            'Resource
            Me._Resource = URL.Substring(PontoInicial)

        End Sub

        Sub New(ByVal Protocol As String, ByVal Host As String)
            Me.Protocol = Protocol
            Me.Host = Host
        End Sub

        Sub New(ByVal Protocol As String, ByVal Host As String, ByVal Resource As String)
            Me.Protocol = Protocol
            Me.Host = Host
            Me.Resource = Resource
        End Sub

        Sub New(ByVal Protocol As String, ByVal Host As String, ByVal Port As Nullable(Of Integer), ByVal Resource As String)
            Me.Protocol = Protocol
            Me.Host = Host
            Me.Port = Port
            Me.Resource = Resource
        End Sub

        Sub New(ByVal Protocol As String, ByVal Host As String, ByVal Resource As String, ByVal Username As String, ByVal Password As String)
            Me.Protocol = Protocol
            Me.Host = Host
            Me.Resource = Resource
            Me.Username = Username
            Me.Password = Password
        End Sub

        Sub New(ByVal Protocol As String, ByVal Host As String, ByVal Port As Nullable(Of Integer), ByVal Resource As String, ByVal Username As String, ByVal Password As String)
            Me.Protocol = Protocol
            Me.Host = Host
            Me.Port = Port
            Me.Resource = Resource
            Me.Username = Username
            Me.Password = Password
        End Sub

        Private Function Build(ByVal IncludeCredentials As Boolean)
            Dim s As String = String.Empty
            s = s & Me._Protocol & "://"

            If IncludeCredentials Then
                If Not String.IsNullOrEmpty(Me._Username) Then
                    s = s & Me._Username

                    If Not String.IsNullOrEmpty(Me._Password) Then
                        s = s & ":"
                        s = s & Me._Password
                    End If

                    s = s & "@"
                End If
            End If

            s = s & _Host

            If Not IsNothing(Me._Port) Then
                s = s & ":"
                s = s & _Port
            End If

            s = s & "/"

            If Not String.IsNullOrEmpty(Me._Resource) Then
                s = s & _Resource
            End If

            Return s

        End Function

        Public ReadOnly Property URL As String
            Get
                Return Build(True)
            End Get
        End Property

        Public ReadOnly Property URLWithoutCredentials As String
            Get
                Return Build(False)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Me.URL
        End Function

        Public Function Clone() As Object Implements ICloneable.Clone
            Return New URL(Protocol:=Me.Protocol, Host:=Me.Host, port:=Me.Port, Resource:=Me.Resource, Username:=Me.Username, Password:=Me.Password)
        End Function
    End Class

End Namespace