Imports System.Security.Cryptography
Imports System.Text

Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Drawing

Public Module Funcoes

#Region "QRCode"

    Public Sub QRCode(Texto As String, Arquivo As String)

        Dim QREncoder As New Gma.QrCodeNet.Encoding.QrEncoder
        Dim QRCode As New Gma.QrCodeNet.Encoding.QrCode

        QREncoder.TryEncode(Texto, QRCode)
        Dim gRenderer As New Gma.QrCodeNet.Encoding.Windows.Render.GraphicsRenderer(New Gma.QrCodeNet.Encoding.Windows.Render.FixedModuleSize(10, Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Four), System.Drawing.Brushes.Black, System.Drawing.Brushes.White)

        Dim fs As New System.IO.FileStream(Arquivo, IO.FileMode.Create)

        gRenderer.WriteToStream(QRCode.Matrix, System.Drawing.Imaging.ImageFormat.Png, fs)

        fs.Close()

    End Sub

#End Region

#Region "Hashing functions MD5/SHA etc.."

    Public Function GeraHashMD5(ByVal texto As String) As String
        Dim btyScr() As Byte = ASCIIEncoding.ASCII.GetBytes(texto)
        Dim objMd5 As New MD5CryptoServiceProvider()
        Dim btyRes() As Byte = objMd5.ComputeHash(btyScr)
        Dim intTotal As Integer = (CInt(btyRes.Length * 2) + CInt((btyRes.Length / 8)))
        Dim strRes As StringBuilder = New StringBuilder(intTotal)
        Dim intI As Integer

        For intI = 0 To btyRes.Length - 1
            strRes.Append(BitConverter.ToString(btyRes, intI, 1))
        Next intI

        Return strRes.ToString().TrimEnd(New Char() {" "c}).ToLower
    End Function

    Public Function GeraHashMD5Arquivo(filePath As String) As String
        Dim computedHash As Byte() = New Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(IO.File.ReadAllBytes(filePath))
        Dim sBuilder = New System.Text.StringBuilder()
        For Each b As Byte In computedHash
            sBuilder.Append(b.ToString("x2").ToLower())
        Next
        Return sBuilder.ToString()
    End Function

#End Region

#Region "FetchDataRow"

    Function ValorDR(r As DataRow, c As String) As Object

        If Not r.IsNull(c) Then
            Return r(c)
        Else
            Return Nothing
        End If

    End Function

    Function ValorDRStr(r As DataRow, c As String) As String
        Dim Ret As Object = ValorDR(r, c)

        If IsNothing(Ret) Then Return String.Empty

        Select Case Ret.GetType
            Case GetType(TimeSpan)
                Return CType(Ret, TimeSpan).ToString("c")
            Case Else
                Return Ret.ToString
        End Select

        Return ValorDR(r, c)
    End Function

#End Region

#Region "Null/Nothing Handling"

    Public Function Coalesce(ByVal ParamArray Args() As Object) As Object
        For i As Integer = Args.GetLowerBound(0) To Args.GetUpperBound(0)
            If Not IsNothing(Args(i)) Then Return Args(i)
        Next

        Return Nothing
    End Function

    Public Function nz( _
    ByVal strValue As Object, _
    ByVal strReplacement As Object) As String

        Try
            If Convert.IsDBNull(strValue) Then
                Return strReplacement
            ElseIf String.IsNullOrEmpty(strValue) Then
                Return strReplacement
            Else
                Return strValue
            End If
        Catch e As Exception
            Return strReplacement
        End Try
    End Function

    Public Function nz(Of T)( _
        ByVal Value As T, _
        ByVal Replacement As T) As T

        Try
            If Value Is Nothing OrElse IsDBNull(Value) Then
                Return Replacement
            Else
                Return Value
            End If
        Catch e As Exception
            Return Replacement
        End Try
    End Function

#End Region

#Region "Fetch"

    Public Function FetchStr(ByVal Str As Object) As String
        If IsDBNull(Str) Then
            Return Nothing
        ElseIf String.IsNullOrEmpty(Str) Then
            Return Nothing
        Else
            Return Str.ToString
        End If
    End Function

    Public Function FetchInt(ByVal Str As Object) As Nullable(Of Integer)
        If IsDBNull(Str) Then
            Return Nothing
        ElseIf String.IsNullOrEmpty(Str) Then
            Return Nothing
        Else
            Dim x As Integer
            Integer.TryParse(Str, x)
            Return x
        End If
    End Function

    Public Function FetchCheck(ByVal Check As Boolean) As Nullable(Of Boolean)
        Return Check
    End Function

    Public Function FetchDate(ByVal Str As Object) As Nullable(Of Date)
        If IsDBNull(Str) Then
            Return Nothing
        ElseIf String.IsNullOrEmpty(Str) Then
            Return Nothing
        Else
            Return Str.ToString
        End If
    End Function
#End Region

#Region "Outras funções"

    Public Function OnlyNumbers(ByVal S As Object) As String
        If IsDBNull(S) Then Return Nothing

        Dim Ret As String = String.Empty

        For Each C As Char In S
            If Char.IsNumber(C) Then Ret &= C
        Next

        Return Ret
    End Function

    Public Function BRDateTime() As DateTime
        Return DateAdd(DateInterval.Hour, -3, System.DateTime.UtcNow)
    End Function


    Public Function FMes(ByVal DATA As DateTime) As String

        Dim Ret As String = String.Empty

        Select Case Month(DATA)
            Case 1 : Ret = "Janeiro"
            Case 2 : Ret = "Fevereiro"
            Case 3 : Ret = "Março"
            Case 4 : Ret = "Abril"
            Case 5 : Ret = "Maio"
            Case 6 : Ret = "Junho"
            Case 7 : Ret = "Julho"
            Case 8 : Ret = "Agosto"
            Case 9 : Ret = "Setembro"
            Case 10 : Ret = "Outubro"
            Case 11 : Ret = "Novembro"
            Case 12 : Ret = "Dezembro"
        End Select

        Return Ret

    End Function

    Public Function FCpf(ByVal CPF As String) As String
        CPF = CPF.Replace(".", "").Replace("-", "")
        Return CPF.Substring(0, 3) & "." & CPF.Substring(3, 3) & "." & CPF.Substring(6, 3) & "-" & CPF.Substring(9, 2)
    End Function

    Public Function FCnpj(ByVal CNPJ As String) As String
        CNPJ = CNPJ.Replace(".", "").Replace("-", "")
        Return CNPJ.Substring(0, 2) & "." & CNPJ.Substring(2, 3) & "." & CNPJ.Substring(5, 3) & "/" & CNPJ.Substring(8, 4) + "-" & CNPJ.Substring(12, 2)
    End Function

#End Region

#Region "Slash"

    Public Function TrailingSlash(ByVal s As String) As String
        If Right(s, 1) <> "/" Then s = s & "/"
        Return s
    End Function

    Public Function TrailingBackSlash(ByVal s As String) As String
        If Right(s, 1) <> "\" Then s = s & "\"
        Return s
    End Function

    Public Function StripBeginSlash(ByVal s As String) As String
        If Left(s, 1) = "/" Then
            Return Mid(s, 2)
        Else
            Return s
        End If
    End Function

    Public Function StripBeginBackSlash(ByVal s As String) As String
        If Left(s, 1) = "\" Then
            Return Mid(s, 2)
        Else
            Return s
        End If
    End Function

    Public Function IncludeBeginSlash(ByVal s As String) As String
        If Left(s, 1) = "/" Then
            Return s
        Else
            Return "/" & s
        End If
    End Function

    Public Function IncludeBeginBackSlash(ByVal s As String) As String
        If Left(s, 1) = "\" Then
            Return s
        Else
            Return "\" & s
        End If
    End Function

#End Region

#Region "Temp Files"

    Function ArquivoTemporario(Optional ByVal ExtensaoArquivo As String = "txt") As String

        If Left(ExtensaoArquivo, 1) <> "." Then
            ExtensaoArquivo = "." & ExtensaoArquivo
        End If

        Return Environment.GetEnvironmentVariable("TEMP") & "\" & System.Guid.NewGuid.ToString & ExtensaoArquivo
    End Function

#End Region

#Region "Shell/Process execute"

    Function Executar(ExePath As String, Argumentos As String) As Integer
        Dim startInfo As New System.Diagnostics.ProcessStartInfo
        Dim MyProces As Process

        startInfo.FileName = ExePath
        startInfo.Arguments = Argumentos
        startInfo.CreateNoWindow = True
        startInfo.UseShellExecute = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden

        MyProces = Process.Start(startInfo)
        MyProces.WaitForExit()

        Return MyProces.ExitCode
    End Function


    Sub AbrirArquivoProgramaPadrao(ByVal Arquivo As String)
        Dim p As New System.Diagnostics.Process
        Dim s As New System.Diagnostics.ProcessStartInfo(Arquivo)
        s.UseShellExecute = True
        s.WindowStyle = ProcessWindowStyle.Normal
        p.StartInfo = s
        p.Start()
    End Sub

#End Region

#Region "Extensions Methods"

    <Runtime.CompilerServices.Extension()> _
    Public Sub ToPDF(Img As Image, LocalPDFPath As String)
        Using PDF As New PdfSharp.Pdf.PdfDocument
            Dim Page1 As New PdfSharp.Pdf.PdfPage()
            PDF.Pages.Add(Page1)

            Dim G As PdfSharp.Drawing.XGraphics = PdfSharp.Drawing.XGraphics.FromPdfPage(Page1)
            Dim XImg As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromGdiPlusImage(Img)

            G.DrawImage(XImg, 0, 0)

            PDF.Save(LocalPDFPath)
            PDF.Close()
        End Using
    End Sub

#End Region

End Module

