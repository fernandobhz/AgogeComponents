Public Class FormularioPesquisa
    Inherits FormularioMDI
    Implements iFormularioAbrivel

    Protected DS As DataSet
    Protected DT As DataTable
    Protected BS As New BindingSource

    Private ClasseCrud As Type
    Private DataGridView As DataGridView

    Private WseLookUp As WseLookUpDelegate
    Private Pesquisar As AcaoDelegate

    Protected OnAbrirCrud As MouseEventHandler
    Protected OnNovoCrud As EventHandler
    Protected OnPesquisarCrud As EventHandler

    Sub New(frmPrincipal As Formulario)
        MyBase.New(frmPrincipal)
    End Sub

    'Modificar essa classe para trabalhar no novo formato
    'A classe derivada deve-se registrar somente
    'Remover esse tipo de código abaixo, pois, a classe derivada deve fazer isso
    '   Me.BS.DataSource = Me.DS
    '   Me.BS.DataMember = Me.DT.TableName


    'Alterar também o Delegate Pesquisar
    'Hoje a classe derivada é que deve fazer o MERGE
    'Modificar para a classe derivada retornar somente um lista de parametros,
    '   como é feito hoje no relatório
    Public Sub Registrar(DS As System.Data.DataSet, DT As System.Data.DataTable, _
                         ClasseCrud As Type, DataGridView As DataGridView, _
                         WseLookUp As WseLookUpDelegate, Pesquisar As AcaoDelegate, _
                         btnPesquisar As Button, btnCadastrar As Button)
        Me.DS = DS
        Me.DT = DT

        Me.BS.DataSource = Me.DS
        Me.BS.DataMember = Me.DT.TableName

        Me.ClasseCrud = ClasseCrud
        Me.DataGridView = DataGridView

        Me.WseLookUp = WseLookUp
        Me.Pesquisar = Pesquisar

        Me.OnPesquisarCrud = AddressOf Me.PesquisarCrud
        Me.OnNovoCrud = AddressOf Me.NovoCrud
        Me.OnAbrirCrud = AddressOf Me.AbrirCrud

        AddHandler DataGridView.MouseDoubleClick, Me.OnAbrirCrud
        AddHandler btnCadastrar.Click, Me.OnNovoCrud
        AddHandler btnPesquisar.Click, Me.OnPesquisarCrud

    End Sub

    Public Overridable Sub Abrir() Implements iFormularioAbrivel.Abrir
        Me.DS.Merge(Me.WseLookUp(codigo:=Nothing))
        Me.Mostrar()
    End Sub

    Public Overridable Sub Mostrar()
        Me.Show()
    End Sub

    Protected Overridable Sub PesquisarCrud()
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.DT.Clear()

            Me.Pesquisar()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Protected Overridable Sub AbrirCrud()
        If Me.BS.Count = 0 Then Exit Sub

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim Crud As FormularioCrud = Activator.CreateInstance(Me.ClasseCrud)
            Crud.Abrir(Codigo:=CType(Me.BS.Current, DataRowView).Row(0))
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Protected Overridable Sub NovoCrud()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim Crud As FormularioCrud = Activator.CreateInstance(Me.ClasseCrud)
            Crud.Novo()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

End Class
