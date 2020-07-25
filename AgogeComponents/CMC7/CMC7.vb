Namespace CMC7

    Public Class CMC7Capture
        Inherits System.ComponentModel.Component

        Private _parentForm As Form
        Property ParentForm As Form
            Get
                Return _parentForm
            End Get
            Set(value As Form)
                _parentForm = value
                _parentForm.KeyPreview = True
                AddHandler _parentForm.KeyPress, AddressOf Me.ParentForm_KeyPress
            End Set
        End Property

        Public Overrides Property Site() As System.ComponentModel.ISite
            Get
                Return MyBase.Site
            End Get
            Set(value As System.ComponentModel.ISite)
                MyBase.Site = value
                If value Is Nothing Then
                    Return
                End If

                Dim host As System.ComponentModel.Design.IDesignerHost = TryCast(value.GetService(GetType(System.ComponentModel.Design.IDesignerHost)), System.ComponentModel.Design.IDesignerHost)
                Dim componentHost As System.ComponentModel.IComponent = host.RootComponent

                If TypeOf componentHost Is Form Then
                    ParentForm = TryCast(componentHost, ContainerControl)
                End If
            End Set
        End Property

        Property DeltaMilliseconds As Integer = 1000

        Public Event Captured(ByVal CMC7 As CMC7Data)
        Public Event Bulding(ByVal s As String)

        Private Sub ParentForm_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not Char.IsDigit(e.KeyChar) Then Exit Sub

            For i As Integer = 0 To 28
                Digits(i) = Digits(i + 1)
            Next

            Dim Digit As New Digit
            Digit.CharTyped = e.KeyChar
            Digit.DateTyped = Now

            Digits(29) = Digit

            Dim LinhaCMC7 As String = Build()

            RaiseEvent Bulding(LinhaCMC7)

            If Delta() <= DeltaMilliseconds Then
                If LinhaCMC7.Length = 30 Then
                    RaiseEvent Captured(New CMC7Data(LinhaCMC7))
                End If

            End If
        End Sub

        Private Function Build() As String
            Dim s As New System.Text.StringBuilder

            For i As Integer = 0 To 29

                If Not IsNothing(Digits(i)) Then
                    If Char.IsDigit(Digits(i).CharTyped) Then
                        s.Append(Digits(i).CharTyped.ToString)
                    End If
                End If
            Next

            Return s.ToString
        End Function

        Private Function Delta() As Nullable(Of Long)

            If Not IsNothing(Digits(29)) And Not IsNothing(Digits(0)) Then
                Return (Digits(29).DateTyped.Ticks - Digits(0).DateTyped.Ticks) / TimeSpan.TicksPerMillisecond
            End If

        End Function

        Private Digits(29) As Digit

        Private Class Digit
            Property CharTyped As Char
            Property DateTyped As DateTime
        End Class

    End Class

    Public Class CMC7Data
        Private _banco As String
        Public ReadOnly Property Banco As String
            Get
                Return _banco
            End Get
        End Property

        Private _agencia As String
        Public ReadOnly Property Agencia As String
            Get
                Return _agencia
            End Get
        End Property

        Private _camaraCompensacao As String
        Public ReadOnly Property CamaraCompensacao As String
            Get
                Return _camaraCompensacao
            End Get
        End Property

        Private _cheque As String
        Public ReadOnly Property Cheque As String
            Get
                Return _cheque
            End Get
        End Property

        Private _conta As String
        Public ReadOnly Property Conta As String
            Get
                Return _conta
            End Get
        End Property

        Private _linhaCMC7 As String

        Public ReadOnly Property LinhaCMC7 As String
            Get
                Return _linhaCMC7
            End Get
        End Property

        Public Sub New(LinhaCMC7 As String)
            If Not LinhaCMC7.Length = 30 Then
                Throw New ArgumentOutOfRangeException("linhaCMC7", "A Linha CMC7 deve conter 30 digitos")
            End If

            For i As Integer = 0 To 29
                If Not Char.IsDigit(LinhaCMC7(i)) Then
                    Throw New ArgumentException("Todos os caracteres da linha CMC7 devem ser digitos 0-9", "linhaCMC7")
                End If
            Next

            _linhaCMC7 = LinhaCMC7

            _banco = _linhaCMC7.Substring(0, 3)
            _agencia = _linhaCMC7.Substring(3, 4)
            _camaraCompensacao = _linhaCMC7.Substring(8, 3)
            _cheque = _linhaCMC7.Substring(11, 6)
            _conta = _linhaCMC7.Substring(19, 10)
        End Sub

        Shared Widening Operator CType(ByVal LinhaCMC7 As String) As CMC7Data
            Return New CMC7Data(LinhaCMC7)
        End Operator

        Shared Widening Operator CType(ByVal CMC7 As CMC7Data) As String
            Return CMC7.LinhaCMC7
        End Operator

    End Class

End Namespace
