Imports System.Threading
Imports System.Drawing
Imports PdfSharp

Namespace Web

    Public Class Render

        Public Property Url() As String

        Public Property Width() As Integer
        Public Property Height() As Integer
        Public Property ThumbnailImage() As Bitmap

        Public Shared Function ToBitmap(Url As String, Optional Width As Integer = 1024, Optional Height As Integer = 768) As Bitmap
            Dim r As New Render(Url, Width, Height)
            Dim Img As Bitmap = r.GenerateThumbnail
            Return Img
        End Function

        Public Shared Sub ToPDF(Url As String, LocalPDFPath As String)
            ToBitmap(Url).ToPDF(LocalPDFPath)
        End Sub

        Private Sub New(Url As String, Width As Integer, Height As Integer)
            Me.Url = Url
            Me.Width = Width
            Me.Height = Height
        End Sub

        Public Function GenerateThumbnail() As Bitmap
            Dim thread As New Thread(New ThreadStart(AddressOf GenerateThumbnailInteral))
            thread.SetApartmentState(ApartmentState.STA)
            thread.Start()
            thread.Join()
            Return ThumbnailImage
        End Function

        Private Sub GenerateThumbnailInteral()
            Dim webBrowser As New WebBrowser()
            webBrowser.ScrollBarsEnabled = False
            webBrowser.ScriptErrorsSuppressed = True
            webBrowser.Navigate(Me.Url)

            AddHandler webBrowser.DocumentCompleted, AddressOf WebBrowser_DocumentCompleted

            While webBrowser.ReadyState <> WebBrowserReadyState.Complete
                Application.DoEvents()
            End While

            webBrowser.Dispose()
        End Sub

        Private Sub WebBrowser_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs)
            Dim webBrowser As WebBrowser = DirectCast(sender, WebBrowser)
            webBrowser.ClientSize = New Size(Me.Width, Me.Height)
            webBrowser.ScrollBarsEnabled = False
            Me.ThumbnailImage = New Bitmap(webBrowser.Bounds.Width, webBrowser.Bounds.Height)
            webBrowser.BringToFront()
            webBrowser.DrawToBitmap(ThumbnailImage, webBrowser.Bounds)
        End Sub

        Public Shared Sub ToMHT(ByVal Url As String, ByVal FilePath As String)
            Dim iMessage As CDO.Message = New CDO.Message
            iMessage.CreateMHTMLBody(Url, _
            CDO.CdoMHTMLFlags.cdoSuppressNone, "", "")
            Dim adodbstream As ADODB.Stream = New ADODB.Stream
            adodbstream.Type = ADODB.StreamTypeEnum.adTypeText
            adodbstream.Charset = "US-ASCII"
            adodbstream.Open()
            iMessage.DataSource.SaveToObject(adodbstream, "_Stream")
            adodbstream.SaveToFile(FilePath, _
                      ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
        End Sub

    End Class


End Namespace
