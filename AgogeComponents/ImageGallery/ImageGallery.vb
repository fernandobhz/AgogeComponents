Public Class ImageGallery

#Region "Private Classes"

    Private Class Pic
        Inherits PictureBox

        Property Local As String
        Property Selecionado As Boolean

    End Class

#End Region

#Region "Núcleo"

    Dim CtrlWidth As Integer
    Dim CtrlHeight As Integer
    Dim PicWidth As Integer
    Dim PicHeight As Integer
    Dim XLocation As Integer
    Dim YLocation As Integer

    Private Sub ImageGallery_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        CtrlHeight = Me.Height
        CtrlWidth = Me.Width
    End Sub
    Private _Directory_Path As String

    Public Property Directorypath() As String
        Get
            Return _Directory_Path
        End Get
        Set(ByVal value As String)

            _Directory_Path = value
            XLocation = 25
            YLocation = 25
            PicWidth = 117
            PicHeight = 109
            CreateGallery()
        End Set
    End Property

    Public Overrides Sub Refresh()
        Me.Directorypath = Me.Directorypath
        MyBase.Refresh()
    End Sub

    Dim i As Integer = 0

    Private Sub Draw(ByVal _filename As String, ByVal _displayname As String)
        Dim Pic As New Pic
        Pic.Location = New System.Drawing.Point(XLocation, YLocation)
        XLocation = XLocation + PicWidth + 20
        If XLocation + PicWidth >= CtrlWidth Then
            XLocation = 25
            YLocation = YLocation + PicHeight + 20
        End If
        Pic.Name = "PictureBox" & i
        i += 1
        Pic.Size = New System.Drawing.Size(PicWidth, PicHeight)
        Pic.TabIndex = 0
        Pic.TabStop = False
        Pic.BorderStyle = BorderStyle.Fixed3D
        Me.ToolTip1.SetToolTip(Pic, _displayname)

        AddHandler Pic.MouseEnter, AddressOf Pic_MouseEnter
        AddHandler Pic.MouseLeave, AddressOf Pic_MouseLeave

        AddHandler Pic.MouseClick, AddressOf Me.Pic_MouseClick

        Me.Panel1.Controls.Add(Pic)

        Pic.Local = _filename

        Dim fs As System.IO.FileStream
        fs = New System.IO.FileStream(_filename, IO.FileMode.Open, IO.FileAccess.Read)
        Pic.Image = System.Drawing.Image.FromStream(fs)
        fs.Close()

        Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage

    End Sub

    Private Sub CreateGallery()
        i = 0
        RemoveControls()
        If Directorypath IsNot Nothing Then
            Dim di As New IO.DirectoryInfo(Directorypath)
            Dim diar1 As IO.FileInfo() = di.GetFiles("*.picture").Concat(di.GetFiles("*.jpg")).Concat(di.GetFiles("*.jpeg")).Concat(di.GetFiles("*.bmp")).Concat(di.GetFiles("*.png")).Concat(di.GetFiles("*.gif")).ToArray
            Dim dra As IO.FileInfo
            For Each dra In diar1
                Draw(dra.FullName, dra.Name)
            Next
        End If
    End Sub

    Private Sub Pic_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Pic As Pic
        Pic = sender

        If Not Pic.Selecionado Then
            Pic.BorderStyle = BorderStyle.FixedSingle
        End If

    End Sub

    Private Sub Pic_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Pic As Pic
        Pic = sender

        If Not Pic.Selecionado Then
            Pic.BorderStyle = BorderStyle.Fixed3D
        End If

    End Sub


    Private Sub Pic_MouseClick(sender As Object, e As MouseEventArgs)
        Dim Pic As Pic = sender

        If e.Button = Windows.Forms.MouseButtons.Left Then

            AbrirArquivoProgramaPadrao(Pic.Local)

        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then

            Pic.Selecionado = Not Pic.Selecionado

            If Pic.Selecionado Then
                Pic.BorderStyle = Windows.Forms.BorderStyle.FixedSingle

                Dim chk As New CheckBox
                chk.BackColor = Drawing.Color.Transparent
                chk.Checked = True

                Pic.Controls.Add(chk)
            Else
                Pic.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

                For Each ctrl As Control In Pic.Controls
                    If TypeOf (ctrl) Is CheckBox Then
                        Pic.Controls.Remove(ctrl)
                    End If
                Next

            End If

        End If


    End Sub

    Private Sub RemoveControls()

Again:  For Each ctrl As Control In Me.Panel1.Controls
            If TypeOf (ctrl) Is Pic Then
                Me.Panel1.Controls.Remove(ctrl)
            End If
        Next

        If Me.Panel1.Controls.Count > 0 Then
            GoTo Again
        End If

    End Sub

#End Region

#Region "Form Events"

    Public Property Resizing() As Boolean

    Private Sub ImageGallery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Me.DesignMode Then
            Dim parent = Me.Parent

            While Not (TypeOf parent Is Form)
                parent = parent.Parent
            End While

            Dim form As Form = TryCast(parent, Form)

            AddHandler form.ResizeEnd, AddressOf Me.Refresh
            AddHandler form.Resize, AddressOf Me.Form_Resize
        End If
    End Sub


    Private LastWindowState As FormWindowState = FormWindowState.Minimized

    Private Sub Form_Resize(sender As Object, e As EventArgs)

        Dim Form As Form = sender

        If Form.WindowState <> LastWindowState Then
            Me.LastWindowState = Form.WindowState

            If Form.WindowState = FormWindowState.Maximized Then
                Me.Refresh()
            End If

            If Form.WindowState = FormWindowState.Normal Then
                Me.Refresh()
            End If

        End If

    End Sub

#End Region

#Region "Adicionar"

    Event ImagemAdicionada(Local As String)

    Private Sub Adicionar_Click(sender As Object, e As EventArgs) Handles Adicionar.Click

        Dim dlg As New OpenFileDialog
        dlg.Filter = "Imagens|*.picture;*.png;*.jpg;*.jpeg;*.gif"
        If dlg.ShowDialog = DialogResult.OK Then

            Dim Destino As String = TrailingBackSlash(Me.Directorypath) & System.IO.Path.GetFileName(dlg.FileName)

            If Not IO.File.Exists(Destino) Then
                System.IO.File.Copy(dlg.FileName, Destino)
            End If

            RaiseEvent ImagemAdicionada(Destino)

            Me.Refresh()
        End If
    End Sub

#End Region

#Region "Deletar"

    Event ImagemDeletada(Local As String)

    Private Sub Deletar_Click(sender As Object, e As EventArgs) Handles Deletar.Click

        If Me.AlgumEstaSelecionado Then
            If MsgBox("Confirma exclusão dos itens selecionados?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
        Else
            MsgBox("Selecione um item para excluir", MsgBoxStyle.Critical)
            Exit Sub
        End If

        For Each ctrl As Control In Me.Panel1.Controls
            If TypeOf (ctrl) Is Pic Then

                Dim Pic As Pic = ctrl

                If Pic.Selecionado Then

                    System.IO.File.Delete(Pic.Local)

                    RaiseEvent ImagemDeletada(Pic.Local)

                End If

            End If
        Next

        Me.Refresh()

    End Sub

    Private Function AlgumEstaSelecionado() As Boolean
        Dim Ret As Boolean = False

        For Each ctrl As Control In Me.Panel1.Controls
            If TypeOf (ctrl) Is Pic Then

                Dim Pic As Pic = ctrl

                If Pic.Selecionado Then
                    Ret = True
                    Return Ret
                End If

            End If
        Next

        Return False
    End Function

#End Region

End Class

