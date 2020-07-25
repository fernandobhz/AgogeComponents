Namespace Sync

    Public NotInheritable Class SimpleSync

        Private Shared Function TraillingBackSlash(ByVal s As String)
            Return String.Concat(s, IIf(s.EndsWith("\"), "", "\"))
        End Function

        Private Shared Function TraillingSlash(ByVal s As String)
            Return String.Concat(s, IIf(s.EndsWith("/"), "", "/"))
        End Function

        Public Shared Sub DownSync(URL As Storage.URL, LocalDirectory As String)
            URL.Resource = SimpleSync.TraillingSlash(URL.Resource)
            LocalDirectory = SimpleSync.TraillingBackSlash(LocalDirectory)

            Dim CloudFiles As List(Of String) = Storage.Cloud.List(URL)
            Dim LocalFiles As New List(Of String)(IO.Directory.GetFiles(LocalDirectory))

            'Deleting Local files that NOT in the Cloud
            For Each L As String In LocalFiles
                Dim FoundFlag As Boolean = False

                For Each C As String In CloudFiles
                    If IO.Path.GetFileName(L) = IO.Path.GetFileName(C) Then
                        FoundFlag = True
                        Exit For
                    End If
                Next

                If Not FoundFlag Then
                    IO.File.Delete(L)
                End If
            Next

            'Download cloud files
            For Each C As String In CloudFiles
                Dim FoundFlag As Boolean = False

                For Each L As String In LocalFiles
                    If IO.Path.GetFileName(L) = IO.Path.GetFileName(C) Then
                        FoundFlag = True
                        Exit For
                    End If
                Next

                If Not FoundFlag Then
                    Dim DownloadURL As Storage.URL = URL.Clone
                    DownloadURL.Resource = DownloadURL.Resource & IO.Path.GetFileName(C)
                    Storage.Cloud.Download(DownloadURL, LocalDirectory & IO.Path.GetFileName(C))
                End If
            Next

        End Sub

        Public Shared Sub DownSync(URL As String, LocalDirectory As String)
            DownSync(URL:=New Storage.URL(URL), LocalDirectory:=LocalDirectory)
        End Sub

        Public Shared Sub UpSync(URL As Storage.URL, LocalDirectory As String)
            URL.Resource = SimpleSync.TraillingSlash(URL.Resource)
            LocalDirectory = SimpleSync.TraillingBackSlash(LocalDirectory)

            Dim CloudFiles As List(Of String) = Storage.Cloud.List(URL)
            Dim LocalFiles As New List(Of String)(IO.Directory.GetFiles(LocalDirectory))

            'Upload NEW Files
            For Each L As String In LocalFiles
                Dim FoundFlag As Boolean = False

                For Each C As String In CloudFiles
                    If IO.Path.GetFileName(L) = IO.Path.GetFileName(C) Then
                        FoundFlag = True
                        Exit For
                    End If
                Next

                If Not FoundFlag Then
                    Dim UploadURL As Storage.URL = URL.Clone
                    UploadURL.Resource = SimpleSync.TraillingSlash(UploadURL.Resource) & IO.Path.GetFileName(L)
                    Storage.Cloud.Upload(UploadURL, L)
                End If
            Next

            'Deleting Cloud files that NOT in the Local
            For Each C As String In CloudFiles
                Dim FoundFlag As Boolean = False

                For Each L As String In LocalFiles
                    If IO.Path.GetFileName(L) = IO.Path.GetFileName(C) Then
                        FoundFlag = True
                        Exit For
                    End If
                Next

                If Not FoundFlag Then
                    Dim DeleteURL As Storage.URL = URL.Clone
                    DeleteURL.Resource = DeleteURL.Resource & IO.Path.GetFileName(C)
                    Storage.Cloud.Delete(DeleteURL)
                End If
            Next

        End Sub

        Public Shared Sub UpSync(URL As String, LocalDirectory As String)
            UpSync(URL:=New Storage.URL(URL), LocalDirectory:=LocalDirectory)
        End Sub

    End Class

End Namespace