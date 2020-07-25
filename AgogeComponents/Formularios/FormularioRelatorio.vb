'Public Class FormularioRelatorio
'    Inherits FormularioMDI
'    Implements iFormularioAbrivel

'    Private CaminhoRelatorio As String

'    Private ParametrosPesquisa As ParametroPesquisaDelegate

'    Private Servidor As Servidor

'    Sub New(frmPrincipal As Formulario, Servidor As Servidor)
'        MyBase.New(frmPrincipal)
'        Me.InitializeComponent()
'        Me.Servidor = Servidor
'    End Sub

'    Public Overridable Sub Abrir() Implements iFormularioAbrivel.Abrir
'        Relatorios.ConfigurarReportViewer(Me.ReportViewer, Me.CaminhoRelatorio, Servidor)
'        Me.Show()
'        Me.Atualizar()
'    End Sub

'    Public Sub Registrar(CaminhoRelatorio As String, ParametrosPesquisa As ParametroPesquisaDelegate, btnPesquisar As Button)
'        Me.CaminhoRelatorio = CaminhoRelatorio
'        Me.ParametrosPesquisa = ParametrosPesquisa

'        AddHandler btnPesquisar.Click, AddressOf Me.Filtrar
'    End Sub

'    Public Property Copias As Short
'        Get
'            Return Me.ReportViewer.PrinterSettings.Copies
'        End Get
'        Set(value As Short)
'            Me.ReportViewer.PrinterSettings.Copies = value
'        End Set
'    End Property

'    Private _Parameterless As Nullable(Of Boolean)
'    Public Property Parameterless As Nullable(Of Boolean)
'        Get
'            Return _Parameterless
'        End Get
'        Set(value As Nullable(Of Boolean))
'            If value Then
'                _Parameterless = True

'                Me.btnFiltrar.Visible = False
'                Me.ReportViewer.Dock = DockStyle.Fill
'            ElseIf Not value Then
'                _Parameterless = False

'                Me.btnFiltrar.Visible = True
'                Me.ReportViewer.Dock = DockStyle.None
'            Else
'                _Parameterless = Nothing
'            End If
'        End Set
'    End Property

'    Protected Overridable Sub Atualizar()

'        Dim Parametros As List(Of Parametro)

'        Try
'            Parametros = Me.ParametrosPesquisa()
'        Catch ex As FaltandoDadosParametroPesquisa
'            Exit Sub
'        End Try

'        If IsNothing(Me.Parameterless) Then
'            Me.Parameterless = IIf(Parametros.Count = 0, True, False)
'        End If

'        Relatorios.AtualizarReportViewer(Me.ReportViewer, Parametros)
'    End Sub

'    Sub Filtrar()
'        Me.Atualizar()
'    End Sub

'End Class

