Public Class FormularioCrud
    Inherits FormularioMDI
    Implements iFormularioAbrivelCodigo, iFormularioCrudNovo

    Protected WithEvents CrudDS As DataSet
    Protected WithEvents CrudBS As BindingSource

    Protected OnExcluir As EventHandler
    Protected OnCancelar As EventHandler
    Protected OnSalvar As EventHandler

    Private btnExcluir As Button
    Private btnCancelar As Button
    Private btnSalvar As Button

    Private WseLookUp As WseLookUpDelegate, WseGet As WseGetDelegate, WseSet As WseSetDelegate

    Sub New(frmPrincipal As Formulario)
        MyBase.New(frmPrincipal)
    End Sub

    Private CodigoUsuario As Integer

    Public Sub Registrar(ByRef DS As System.Data.DataSet, ByRef BS As BindingSource, CodigoUsuario As Integer, _
                         WseLookUp As WseLookUpDelegate, WseGet As WseGetDelegate, WseSet As WseSetDelegate, _
                         btnExcluir As Button, btnCancelar As Button, btnSalvar As Button)
        Me.CrudDS = DS
        Me.CrudBS = BS
        Me.CodigoUsuario = CodigoUsuario

        Me.WseLookUp = WseLookUp
        Me.WseGet = WseGet
        Me.WseSet = WseSet

        Me.btnExcluir = btnExcluir
        Me.btnCancelar = btnCancelar
        Me.btnSalvar = btnSalvar

        Me.OnExcluir = AddressOf Me.Excluir
        Me.OnCancelar = AddressOf Me.Cancelar
        Me.OnSalvar = AddressOf Me.Salvar

        If Not IsNothing(Me.btnExcluir) Then AddHandler btnExcluir.Click, Me.OnExcluir
        If Not IsNothing(Me.btnCancelar) Then AddHandler btnCancelar.Click, Me.OnCancelar
        If Not IsNothing(Me.btnSalvar) Then AddHandler btnSalvar.Click, Me.OnSalvar
    End Sub

    Public Overridable Sub Mostrar()
        Me.Show()
        Me.FocusMe()
    End Sub

    Public ReadOnly Property Crud As DataRow
        Get
            If Me.CrudBS.Count = 1 Then
                Return CType(Me.CrudBS.Current, DataRowView).Row
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Overridable Sub Popular(ByVal Codigo As Integer)
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.CrudDS.Merge(Me.WseGet(codigo:=Codigo))
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Overridable Sub Abrir(ByVal Codigo As Integer) Implements iFormularioAbrivelCodigo.Abrir
        Me.Popular(Codigo)
        Me.Mostrar()
    End Sub

    Public Overridable Sub Novo() Implements iFormularioCrudNovo.Novo
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.CrudDS.Merge(Me.WseLookUp(codigo:=Nothing))
            Me.CrudBS.AddNew()

            Dim r As DataRow = CType(Me.CrudBS.Current, DataRowView).Row
            If r.Table.Columns.Contains("cd_usuario") Then r("cd_usuario") = Me.CodigoUsuario
            If r.Table.Columns.Contains("dt_registro") Then r("dt_registro") = Now
        Finally
            Me.Cursor = Cursors.Default
        End Try

        'Não faz sentido mostrar botão de excluir quando estamos inserindo novo registro.
        If Not IsNothing(Me.btnExcluir) Then Me.btnExcluir.Visible = False
        Me.Mostrar()
    End Sub

    Protected Overridable Function Mudou() As Boolean
        Me.Validate()
        Me.CrudBS.EndEdit()

        Return DataSetMudou(Me.CrudDS, Me.CodigoUsuario)
    End Function

    Public Shared Function DataSetMudou(ByRef MudouDS As DataSet, Optional ByVal cd_usuario As Nullable(Of Integer) = Nothing) As Boolean
        If MudouDS.HasChanges Then 'Verificar reais mudanças
            'Itera tabelas
            For t = 0 To MudouDS.Tables.Count - 1

                Dim rCurrent As New DataView(MudouDS.Tables(t))
                rCurrent.RowStateFilter = DataViewRowState.ModifiedCurrent

                Dim rOriginal As New DataView(MudouDS.Tables(t))
                rOriginal.RowStateFilter = DataViewRowState.ModifiedOriginal

                'Itera registros
                Dim i As Integer = 0
                While i < rCurrent.Count
                    Dim Alterado As Boolean = False

                    'Itera colunas
                    For j As Integer = 0 To rCurrent.Table.Columns.Count - 1
                        'Ss(MudouDS, t, i, j, rCurrent, rOriginal)

                        If rCurrent(i)(j).ToString <> rOriginal(i)(j).ToString Then
                            Alterado = True
                            Exit For
                        End If
                    Next

                    If Alterado Then
                        If Not IsNothing(cd_usuario) Then
                            If rCurrent(i).Row.Table.Columns.Contains("cd_usuario") Then rCurrent(i).Row("cd_usuario") = cd_usuario
                            If rCurrent(i).Row.Table.Columns.Contains("dt_registro") Then rCurrent(i).Row("dt_registro") = Now
                        End If
                        i = i + 1
                    Else
                        rCurrent(i).Row.RejectChanges()
                    End If

                End While

            Next

            Return MudouDS.HasChanges
        Else
            Return False
        End If
    End Function

    Private Shared Sub Ss(MudouDS As DataSet, T As Integer, I As Integer, J As Integer, rCurrent As DataView, rOriginal As DataView)
        Dim s As String
        s = "T: " & T & ": - Tabela: " & MudouDS.Tables(T).TableName & vbCrLf & vbCrLf

        s = s & "I: " & I & vbCrLf & vbCrLf

        s = s & "J: " & J & vbCrLf
        s = s & "J: " & J & " - Campo: " & rCurrent.Table.Columns(J).ColumnName & vbCrLf & vbCrLf

        s = s & "Current: " & rCurrent(I)(J).ToString & vbCrLf
        s = s & "Original: " & rOriginal(I)(J).ToString & vbCrLf
        MsgBox(s)
    End Sub

    Protected FecharAposSalvar As Boolean = True

    Protected Overridable Sub Salvar()
        Try
            If Mudou() Then
                Me.Cursor = Cursors.WaitCursor

                Me.WseSet(Me.CrudDS)
                Me.CrudDS.AcceptChanges()
                Me.CrudBS.DataSource = Me.CrudDS

                If CrudBS.Count = 0 Then If Me.FecharAposSalvar Then Me.Fechar()
                If Not IsNothing(Me.btnExcluir) Then If Not Me.btnExcluir.Visible Then Me.btnExcluir.Visible = True
            Else
                If Me.FecharAposSalvar Then Me.Fechar()
            End If
        Catch ex As Exception
            MsgBox("Erro" & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Protected Overridable Sub Excluir()
        If MsgBox("Confirma exclusão?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

        Me.CrudBS.RemoveCurrent()
        Salvar()
    End Sub

    Protected Overridable Sub Cancelar()
        Me.CrudBS.CancelEdit()
        Me.CrudDS.RejectChanges()
        Me.Fechar()
    End Sub

    Protected Overridable Sub Fechar()
        Me.PularFormClosing = True
        Me.Close()
    End Sub

    Protected PularFormClosing As Boolean = False

    Protected Overridable Sub FormularioCrud_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.PularFormClosing Then Exit Sub
        Try
            If Mudou() Then
                Select Case MsgBox("Deseja salvar as alterações?", MsgBoxStyle.YesNoCancel)
                    Case MsgBoxResult.Yes : Salvar()
                    Case MsgBoxResult.No : Me.CrudDS.RejectChanges()
                    Case MsgBoxResult.Cancel : e.Cancel = True
                End Select
            End If
        Catch ex As Exception
            Select Case MsgBox("Erro ao verificar alterações" & vbCrLf & ex.Message & vbCrLf & vbCrLf & "Deseja sair sem salvar alterações?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Critical)
                Case MsgBoxResult.Yes
                    Me.CrudDS.RejectChanges()
                Case MsgBoxResult.No
                    e.Cancel = True
                Case MsgBoxResult.Cancel
                    e.Cancel = True
            End Select
        End Try
    End Sub

End Class
