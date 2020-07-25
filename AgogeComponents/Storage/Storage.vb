Namespace Storage

    Public NotInheritable Class Cloud

        Private Shared FTP As New FTPStorageCloudProvider
        Private Shared S3 As New S3StorageCloudProvider

#Region "String Methods Overload"

        Public Shared Function List(URL As String) As List(Of String)
            Return List(URL:=New URL(URL))
        End Function

        Public Shared Sub MakeDir(URL As String)
            MakeDir(URL:=New URL(URL))
        End Sub

        Public Shared Sub ForceDir(URL As String)
            ForceDir(URL:=New URL(URL))
        End Sub

        Public Shared Sub Download(ByVal URL As String, ByVal LocalFile As String)
            Download(URL:=New URL(URL), LocalFile:=LocalFile)
        End Sub

        Public Shared Sub Upload(ByVal URL As String, ByVal LocalFile As String)
            Upload(URL:=New URL(URL), LocalFile:=LocalFile)
        End Sub

        Public Shared Sub Delete(ByVal URL As String)
            Delete(URL:=New URL(URL))
        End Sub

#End Region

#Region "URL Methods"

        Public Shared Function List(URL As URL) As List(Of String)

            If URL.Protocol = "ftp" Then
                Return FTP.List(URL)
            ElseIf URL.Protocol = "s3" Then
                Return S3.List(URL)
            Else
                Throw New Exception("Protocol not supported")
            End If

        End Function

        Public Shared Sub MakeDir(URL As URL)
            Dim Thread As Threading.Thread

            If URL.Protocol = "ftp" Then
                Thread = New Threading.Thread(Sub() FTP.MakeDir(URL))
            ElseIf URL.Protocol = "s3" Then
                Thread = New Threading.Thread(Sub() S3.MakeDir(URL))
            Else
                Throw New Exception("Protocol not supported")
            End If

            Thread.Start()

            While Thread.IsAlive
                Application.DoEvents()
            End While
        End Sub

        Public Shared Sub ForceDir(URL As URL)
            Dim Thread As Threading.Thread

            If URL.Protocol = "ftp" Then
                Thread = New Threading.Thread(Sub() FTP.ForceDir(URL))
            ElseIf URL.Protocol = "s3" Then
                Thread = New Threading.Thread(Sub() S3.ForceDir(URL))
            Else
                Throw New Exception("Protocol not supported")
            End If

            Thread.Start()

            While Thread.IsAlive
                Application.DoEvents()
            End While
        End Sub

        Public Shared Sub Download(ByVal URL As URL, ByVal LocalFile As String)
            Dim Thread As Threading.Thread

            If URL.Protocol = "ftp" Then
                Thread = New Threading.Thread(Sub() FTP.Download(URL, LocalFile))
            ElseIf URL.Protocol = "s3" Then
                Thread = New Threading.Thread(Sub() S3.Download(URL, LocalFile))
            Else
                Throw New Exception("Protocol not supported")
            End If

            Thread.Start()

            While Thread.IsAlive
                Application.DoEvents()
            End While
        End Sub

        Public Shared Sub Upload(ByVal URL As URL, ByVal LocalFile As String)
            Dim Thread As Threading.Thread

            If URL.Protocol = "ftp" Then
                Thread = New Threading.Thread(Sub() FTP.Upload(URL, LocalFile))
            ElseIf URL.Protocol = "s3" Then
                Thread = New Threading.Thread(Sub() S3.Upload(URL, LocalFile))
            Else
                Throw New Exception("Protocol not supported")
            End If

            Thread.Start()

            While Thread.IsAlive
                Application.DoEvents()
            End While
        End Sub

        Public Shared Sub Delete(ByVal URL As URL)
            Dim Thread As Threading.Thread

            If URL.Protocol = "ftp" Then
                Thread = New Threading.Thread(Sub() FTP.Delete(URL))
            ElseIf URL.Protocol = "s3" Then
                Thread = New Threading.Thread(Sub() S3.Delete(URL))
            Else
                Throw New Exception("Protocol not supported")
            End If

            Thread.Start()

            While Thread.IsAlive
                Application.DoEvents()
            End While
        End Sub

#End Region

    End Class

    Friend Interface iStorageCloudProvider


        Function List(URL As URL) As List(Of String)
        Sub MakeDir(URL As URL)
        Sub ForceDir(URL As URL)

        Sub Delete(URL As URL)
        Sub Download(URL As URL, ByVal LocalFile As String)
        Sub Upload(URL As URL, ByVal LocalFile As String)

    End Interface

    Friend Class FTPStorageCloudProvider
        Implements iStorageCloudProvider


        Public Function List(URL As URL) As List(Of String) Implements iStorageCloudProvider.List

            Dim ftp As System.Net.FtpWebRequest = _
                DirectCast(System.Net.WebRequest.Create(URL.URLWithoutCredentials), System.Net.FtpWebRequest)

            ftp.Credentials = New System.Net.NetworkCredential(URL.Username, URL.Password)
            ftp.Method = System.Net.WebRequestMethods.Ftp.ListDirectoryDetails
            ftp.UseBinary = True

            Dim response As System.Net.FtpWebResponse = ftp.GetResponse()

            Dim rs As IO.Stream = response.GetResponseStream
            Dim reader As New IO.StreamReader(rs)

            Dim Files As New List(Of String)

            While Not reader.EndOfStream
                Dim s As String = reader.ReadLine

                Dim iBeforeFileName As Integer
                Dim DirFlag As Boolean

                If s.StartsWith("-") Or s.StartsWith("d") Then
                    'Linux response style
                    'drwxr-xr-x  22 fernandobhz pg1314064     8192 Apr  8 13:45 antigos
                    '-rw-rw-r--   1 fernandobhz pg1314064      806 Jun  3 20:36 b.sql

                    DirFlag = s.StartsWith("d")

                    iBeforeFileName = 8
                Else
                    'MS-DOS reponse style
                    '06-18-13  01:03AM       <DIR>          ass.agoge.com.br
                    '07-06-13  05:35PM             27802721 backup_migracacao.rar

                    DirFlag = s.Contains("<DIR>")

                    iBeforeFileName = 3
                End If

				'Xyz
                'Haverá casos onde o nome do arquivo possue +1 ESPAÇO EM BRANCO
                '"09-29-14  03:45PM                 3290 a  bcd.PNG"
                Dim Parts As String() = s.Split(separator:=" ", options:=System.StringSplitOptions.RemoveEmptyEntries)
                Dim AllParts As String() = s.Split(" ")


                Dim PNameStart As Integer = 0

                Dim j As Integer = 0

                For i As Integer = 0 To AllParts.Count - 1
                    Dim ap = AllParts(i)

                    If Not String.IsNullOrEmpty(ap) Then
                        j += 1
                    End If

                    If String.IsNullOrEmpty(ap) Then
                        PNameStart += 1
                    Else
                        PNameStart += ap.Length

                        If i > 0 Then
                            If AllParts(i - 1).Length = 0 Then
                                PNameStart += 1
                            End If
                        End If

                    End If

                    If j = iBeforeFileName Then
                        PNameStart += 1
                        Exit For
                    End If

                Next

                Dim CorretFileName As String = s.Substring(PNameStart)

                If DirFlag Then CorretFileName = CorretFileName & "/"

                Files.Add(CorretFileName)

            End While

            Return Files
        End Function

        Public Sub MakeDir(URL As URL) Implements iStorageCloudProvider.MakeDir
            Dim ftp As System.Net.FtpWebRequest = _
                DirectCast(System.Net.WebRequest.Create(URL.URLWithoutCredentials), System.Net.FtpWebRequest)

            ftp.Credentials = New System.Net.NetworkCredential(URL.Username, URL.Password)
            ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory
            ftp.UseBinary = True

            Dim response As System.Net.FtpWebResponse = ftp.GetResponse()
        End Sub

        Public Sub ForceDir(URL As URL) Implements iStorageCloudProvider.ForceDir

            Dim Dirs As String() = URL.Resource.Split("/")

            For i As Integer = 0 To Dirs.Count - 1

                Dim iResource As String = String.Empty

                For j As Integer = 1 To i
                    iResource = String.Concat(iResource, Dirs(j - 1), "/")
                Next

                Dim iURL As New URL(Protocol:=URL.Protocol, Host:=URL.Host, port:=URL.Port, Resource:=iResource, Username:=URL.Username, Password:=URL.Password)

                If Not String.Join(vbCrLf, Me.List(iURL)).Contains(Dirs(i)) Then

                    Dim NewURL As New URL(Protocol:=iURL.Protocol, Host:=iURL.Host, Port:=iURL.Port, Resource:=String.Concat(iURL.Resource, Dirs(i), "/"), Username:=iURL.Username, Password:=iURL.Password)

                    Me.MakeDir(NewURL)

                End If

            Next

        End Sub

        Public Sub Delete(URL As URL) Implements iStorageCloudProvider.Delete
            Dim ftp As System.Net.FtpWebRequest = _
                DirectCast(System.Net.WebRequest.Create(URL.URLWithoutCredentials), System.Net.FtpWebRequest)

            ftp.Credentials = New System.Net.NetworkCredential(URL.Username, URL.Password)
            ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile
            ftp.UseBinary = True

            Dim response As System.Net.FtpWebResponse = ftp.GetResponse()
        End Sub

        Public Sub Download(URL As URL, ByVal LocalFile As String) Implements iStorageCloudProvider.Download
            Dim ftp As System.Net.FtpWebRequest =
               DirectCast(System.Net.WebRequest.Create(URL.URLWithoutCredentials), System.Net.FtpWebRequest)

            ftp.Credentials = New System.Net.NetworkCredential(URL.Username, URL.Password)
            ftp.Method = System.Net.WebRequestMethods.Ftp.DownloadFile
            ftp.UseBinary = True

            Dim F As New System.IO.FileInfo(LocalFile)


            Const BufferSize As Integer = 2048 '50 * 1024
            Dim Content(BufferSize - 1) As Byte
            Dim DataRead As Integer

            Dim response As System.Net.FtpWebResponse = ftp.GetResponse()
            Dim cl As Long = response.ContentLength

            Using rs As IO.Stream = response.GetResponseStream
                If F.Exists Then F.Delete()
                Using fs As IO.FileStream = F.OpenWrite

                    Do
                        DataRead = rs.Read(Content, 0, BufferSize)
                        fs.Write(Content, 0, DataRead)

                    Loop While (DataRead > 0)
                    rs.Close()

                End Using
            End Using

        End Sub

        Public Sub Upload(URL As URL, ByVal LocalFile As String) Implements iStorageCloudProvider.Upload
            Dim F As New System.IO.FileInfo(LocalFile)

            If Not F.Exists Then Throw New System.IO.FileNotFoundException(LocalFile & " does not exists")

            Dim ftp As System.Net.FtpWebRequest =
               DirectCast(System.Net.WebRequest.Create(URL.URLWithoutCredentials), System.Net.FtpWebRequest)

            ftp.Credentials = New System.Net.NetworkCredential(URL.Username, URL.Password)
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile
            ftp.UseBinary = True
            ftp.ContentLength = F.Length

            Const BufferSize As Integer = 2048 '50 * 1024
            Dim Content(BufferSize - 1) As Byte
            Dim DataRead As Integer

            Using fs As IO.FileStream = F.OpenRead
                Using rs As IO.Stream = ftp.GetRequestStream

                    Do
                        DataRead = fs.Read(Content, 0, BufferSize)
                        rs.Write(Content, 0, DataRead)

                    Loop Until (DataRead < BufferSize)
                    rs.Close()

                End Using
            End Using

        End Sub

    End Class

    Friend Class S3StorageCloudProvider
        Implements iStorageCloudProvider
        'http://docs.aws.amazon.com/general/latest/gr/rande.html#ec2_region

        Public Function List(URL As URL) As List(Of String) Implements iStorageCloudProvider.List
            If Not URL.Resource.Contains("/") Then Throw New Exception("Resource not contain bucket name")

            Dim S3Config As New Amazon.S3.AmazonS3Config() With {.ServiceURL = URL.Host}

            Using client As Amazon.S3.AmazonS3 = Amazon.AWSClientFactory.CreateAmazonS3Client(URL.Username, URL.Password, S3Config)

                Dim BucketName As String = URL.Resource.Substring(0, URL.Resource.IndexOf("/"))
                Dim FileKey As String = URL.Resource.Substring(URL.Resource.IndexOf("/") + 1)

                Dim ListObjectsRequest As New Amazon.S3.Model.ListObjectsRequest With {.BucketName = BucketName, .Prefix = FileKey, .Delimiter = "/"}
                Dim ListObjectsResponse As Amazon.S3.Model.ListObjectsResponse = client.ListObjects(ListObjectsRequest)

                Dim Files As New List(Of String)

                Files.AddRange(ListObjectsResponse.CommonPrefixes)

                For Each S3Object As Amazon.S3.Model.S3Object In ListObjectsResponse.S3Objects
                    Files.Add(S3Object.Key)
                Next

                Return Files
            End Using
        End Function

        Public Sub MakeDir(URL As URL) Implements iStorageCloudProvider.MakeDir
            If Not URL.Resource.Contains("/") Then Throw New Exception("Resource not contain bucket name")

            Dim S3Config As New Amazon.S3.AmazonS3Config() With {.ServiceURL = URL.Host}

            Using client As New Amazon.S3.AmazonS3Client(URL.Username, URL.Password, S3Config)

                Dim BucketName As String = URL.Resource.Substring(0, URL.Resource.IndexOf("/"))
                Dim FileKey As String = URL.Resource.Substring(URL.Resource.IndexOf("/") + 1)

                If Not FileKey.EndsWith("/") Then
                    FileKey = String.Concat(FileKey, "/")
                End If

                Dim putObjectRequest As New Amazon.S3.Model.PutObjectRequest With {.BucketName = BucketName, .Key = FileKey, .ContentBody = String.Empty}
                client.PutObject(putObjectRequest)
            End Using
        End Sub

        Public Sub ForceDir(URL As URL) Implements iStorageCloudProvider.ForceDir
            Me.MakeDir(URL)
        End Sub

        Public Sub Delete(URL As URL) Implements iStorageCloudProvider.Delete
            If Not URL.Resource.Contains("/") Then Throw New Exception("Resource not contain bucket name")

            Dim S3Config As New Amazon.S3.AmazonS3Config() With {.ServiceURL = URL.Host}

            Using client As Amazon.S3.AmazonS3 = Amazon.AWSClientFactory.CreateAmazonS3Client(URL.Username, URL.Password, S3Config)

                Dim BucketName As String = URL.Resource.Substring(0, URL.Resource.IndexOf("/"))
                Dim FileKey As String = URL.Resource.Substring(URL.Resource.IndexOf("/") + 1)

                Dim deleteObjectRequest As New Amazon.S3.Model.DeleteObjectRequest With {.BucketName = BucketName, .Key = FileKey}
                client.DeleteObject(deleteObjectRequest)
            End Using
        End Sub

        Public Sub Download(URL As URL, ByVal LocalFile As String) Implements iStorageCloudProvider.Download
            If Not URL.Resource.Contains("/") Then Throw New Exception("Resource not contain bucket name")

            Dim S3Config As New Amazon.S3.AmazonS3Config() With {.ServiceURL = URL.Host}

            Dim F As New System.IO.FileInfo(LocalFile)

            Const BufferSize As Integer = 2048
            Dim Content(BufferSize - 1) As Byte
            Dim DataRead As Integer

            Using client As New Amazon.S3.AmazonS3Client(URL.Username, URL.Password, S3Config)

                Dim BucketName As String = URL.Resource.Substring(0, URL.Resource.IndexOf("/"))
                Dim FileKey As String = URL.Resource.Substring(URL.Resource.IndexOf("/") + 1)

                Dim getObjectRequest As New Amazon.S3.Model.GetObjectRequest With {.BucketName = BucketName, .Key = FileKey}

                Using getObjectResponse As Amazon.S3.Model.GetObjectResponse = client.GetObject(getObjectRequest)

                    Using rs As IO.Stream = getObjectResponse.ResponseStream
                        If F.Exists Then F.Delete()
                        Using fs As IO.FileStream = F.OpenWrite

                            Do
                                DataRead = rs.Read(Content, 0, BufferSize)
                                fs.Write(Content, 0, DataRead)

                            Loop While (DataRead > 0)

                        End Using
                    End Using

                End Using
            End Using
        End Sub

        Public Sub Upload(URL As URL, ByVal LocalFile As String) Implements iStorageCloudProvider.Upload
            If Not URL.Resource.Contains("/") Then Throw New Exception("Resource not contain bucket name")

            Dim S3Config As New Amazon.S3.AmazonS3Config() With {.ServiceURL = URL.Host}

            Using client As New Amazon.S3.AmazonS3Client(URL.Username, URL.Password, S3Config)

                Dim BucketName As String = URL.Resource.Substring(0, URL.Resource.IndexOf("/"))
                Dim FileKey As String = URL.Resource.Substring(URL.Resource.IndexOf("/") + 1)

                Dim putObjectRequest As New Amazon.S3.Model.PutObjectRequest With {.BucketName = BucketName, .Key = FileKey, .FilePath = LocalFile}
                client.PutObject(putObjectRequest)
            End Using
        End Sub

    End Class

End Namespace