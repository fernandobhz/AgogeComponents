Imports System.Data.SqlClient

Public Class PesDesigner

    Public Sub Criar(Entidade As EntidadeSigedin)
        Dim hide_pkfk As Boolean
        hide_pkfk = True

        Dim f As New System.IO.StreamWriter(Entidade.PesDesignerFile, False, System.Text.Encoding.UTF8)
        f.WriteLine("Partial Class pes" & Entidade.NomeEntidade & "Sigedin")
        f.WriteLine("    Inherits FormularioPesquisa")
        f.WriteLine("")
        f.WriteLine("    'Form overrides dispose to clean up the component list.")
        f.WriteLine("    <System.Diagnostics.DebuggerNonUserCode()> _")
        f.WriteLine("    Protected Overrides Sub Dispose(ByVal disposing As Boolean)")
        f.WriteLine("        Try")
        f.WriteLine("            If disposing AndAlso components IsNot Nothing Then")
        f.WriteLine("                components.Dispose()")
        f.WriteLine("            End If")
        f.WriteLine("        Finally")
        f.WriteLine("            MyBase.Dispose(disposing)")
        f.WriteLine("        End Try")
        f.WriteLine("    End Sub")
        f.WriteLine("")
        f.WriteLine("    'Required by the Windows Form Designer")
        f.WriteLine("    Private components As System.ComponentModel.IContainer")
        f.WriteLine("")
        f.WriteLine("    'NOTE: The following procedure is required by the Windows Form Designer")
        f.WriteLine("    'It can be modified using the Windows Form Designer.  ")
        f.WriteLine("    'Do not modify it using the code editor.")
        f.WriteLine("    <System.Diagnostics.DebuggerStepThrough()> _")
        f.WriteLine("    Private Sub InitializeComponent()")
        f.WriteLine("        Me.components = New System.ComponentModel.Container")
        f.WriteLine("        Me.PainelInferior = New System.Windows.Forms.Panel")
        f.WriteLine("        Me.btnExcluir = New System.Windows.Forms.Button")
        f.WriteLine("        Me.btnCadastrar = New System.Windows.Forms.Button")
        f.WriteLine("        Me.btnPesquisar = New System.Windows.Forms.Button")
        f.WriteLine("        Me.Tab = New System.Windows.Forms.TabControl")
        f.WriteLine("        Me.TabPesquisar = New System.Windows.Forms.TabPage")
        f.WriteLine("        Me.TabResultado = New System.Windows.Forms.TabPage")
        f.WriteLine("        Me.PesquisaDataGridView = New System.Windows.Forms.DataGridView")
        f.WriteLine("        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip")

        Declara(Entidade, f)

        'Sub declara


        f.WriteLine("        Me.PainelInferior.SuspendLayout()")
        f.WriteLine("        Me.Tab.SuspendLayout()")
        f.WriteLine("        Me.TabPesquisar.SuspendLayout()")
        f.WriteLine("        Me.TabResultado.SuspendLayout()")

        'Sub suspeend

        f.WriteLine("        Me.TotalRegRotulo = New System.Windows.Forms.ToolStripStatusLabel()")
        f.WriteLine("        Me.TotalRegValor = New System.Windows.Forms.ToolStripStatusLabel()")
        f.WriteLine("        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()")
        f.WriteLine("        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()")

        f.WriteLine("        Me.DS" & Entidade.NomeEntidade & " = New Aplicativo.RemoteWse.ds_" & Entidade.NomeEntidade & "")
        f.WriteLine("        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()")
        f.WriteLine("        Me.SuspendLayout()")
        f.WriteLine("        '")
        f.WriteLine("        'PainelInferior")
        f.WriteLine("        '")
        f.WriteLine("        Me.PainelInferior.Controls.Add(Me.btnCadastrar)")
        f.WriteLine("        Me.PainelInferior.Controls.Add(Me.btnPesquisar)")
        f.WriteLine("        Me.PainelInferior.Dock = System.Windows.Forms.DockStyle.Bottom")
        f.WriteLine("        Me.PainelInferior.Location = New System.Drawing.Point(0, 512)")
        f.WriteLine("        Me.PainelInferior.Name = ""PainelInferior""")
        f.WriteLine("        Me.PainelInferior.Size = New System.Drawing.Size(984, 50)")
        f.WriteLine("        Me.PainelInferior.TabIndex = 4")
        f.WriteLine("        '")
        f.WriteLine("        'btnCadastrar")
        f.WriteLine("        '")
        f.WriteLine("        Me.btnCadastrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)")
        f.WriteLine("        Me.btnCadastrar.Image = Global.Aplicativo.My.Resources.Resources.Cadastrar")
        f.WriteLine("        Me.btnCadastrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft")
        f.WriteLine("        Me.btnCadastrar.Location = New System.Drawing.Point(831, 0)")
        f.WriteLine("        Me.btnCadastrar.Name = ""btnCadastrar""")
        f.WriteLine("        Me.btnCadastrar.Size = New System.Drawing.Size(150, 50)")
        f.WriteLine("        Me.btnCadastrar.TabIndex = 0")
        f.WriteLine("        Me.btnCadastrar.Text = ""               Cadastrar""")
        f.WriteLine("        Me.btnCadastrar.UseVisualStyleBackColor = True")
        f.WriteLine("        '")
        f.WriteLine("        'btnPesquisar")
        f.WriteLine("        '")
        f.WriteLine("        Me.btnPesquisar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)")
        f.WriteLine("        Me.btnPesquisar.Image = Global.Aplicativo.My.Resources.Resources.Pesquisar")
        f.WriteLine("        Me.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft")
        f.WriteLine("        Me.btnPesquisar.Location = New System.Drawing.Point(4, 0)")
        f.WriteLine("        Me.btnPesquisar.Name = ""btnPesquisar""")
        f.WriteLine("        Me.btnPesquisar.Size = New System.Drawing.Size(150, 50)")
        f.WriteLine("        Me.btnPesquisar.TabIndex = 1")
        f.WriteLine("        Me.btnPesquisar.Text = ""               Pesquisar""")
        f.WriteLine("        Me.btnPesquisar.UseVisualStyleBackColor = True")
        f.WriteLine("        '")
        f.WriteLine("        'Tab")
        f.WriteLine("        '")
        f.WriteLine("        Me.Tab.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _")
        f.WriteLine("                    Or System.Windows.Forms.AnchorStyles.Left) _")
        f.WriteLine("                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)")
        f.WriteLine("        Me.Tab.Controls.Add(Me.TabPesquisar)")
        f.WriteLine("        Me.Tab.Controls.Add(Me.TabResultado)")

        f.WriteLine("        Me.Tab.Location = New System.Drawing.Point(0, 0)")
        f.WriteLine("        Me.Tab.Name = ""Tab""")
        f.WriteLine("        Me.Tab.SelectedIndex = 0")
        f.WriteLine("        Me.Tab.Size = New System.Drawing.Size(984, 509)")
        f.WriteLine("        Me.Tab.TabIndex = 5")


        'Dataset & BindingSources
        f.WriteLine("        'DS" & Entidade.NomeEntidade & "")
        f.WriteLine("        '")
        f.WriteLine("        Me.DS" & Entidade.NomeEntidade & ".DataSetName = ""ds_ " & Entidade.NomeEntidade & """")
        f.WriteLine("        Me.DS" & Entidade.NomeEntidade & ".SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema")
        f.WriteLine("        '")
        f.WriteLine("        'BS")
        f.WriteLine("        '")
        f.WriteLine("        Me.BS.DataMember = ""ps_" & LCase(Entidade.NomeEntidade) & """")
        f.WriteLine("        Me.BS.DataSource = Me.DS" & Entidade.NomeEntidade & "")



        'Controles
        f.WriteLine("        '")
        f.WriteLine("        'TabPesquisar")
        f.WriteLine("        '")

        Adiciona(Entidade, f)

        'Sub adiciona

        f.WriteLine("        Me.TabPesquisar.AutoScroll = True")
        f.WriteLine("        Me.TabPesquisar.AutoScrollMargin = New System.Drawing.Size(0, 10)")
        f.WriteLine("        Me.TabPesquisar.Location = New System.Drawing.Point(4, 22)")
        f.WriteLine("        Me.TabPesquisar.Name = ""TabPesquisar""")
        f.WriteLine("        Me.TabPesquisar.Padding = New System.Windows.Forms.Padding(3)")
        f.WriteLine("        Me.TabPesquisar.Size = New System.Drawing.Size(976, 483)")
        f.WriteLine("        Me.TabPesquisar.TabIndex = 0")
        f.WriteLine("        Me.TabPesquisar.Text = ""Pesquisa de " & Entidade.NomeCadastro & """")
        f.WriteLine("        Me.TabPesquisar.UseVisualStyleBackColor = True")
        f.WriteLine("        '")



        f.WriteLine("        '")
        f.WriteLine("        'TabResultado")
        f.WriteLine("        '")
        f.WriteLine("        Me.TabResultado.AutoScroll = True")
        f.WriteLine("        Me.TabResultado.Controls.Add(Me.PesquisaDataGridView)")
        f.WriteLine("        Me.TabResultado.Controls.Add(Me.StatusStrip1)")
        f.WriteLine("        Me.TabResultado.Location = New System.Drawing.Point(4, 22)")
        f.WriteLine("        Me.TabResultado.Name = ""TabResultado""")
        f.WriteLine("        Me.TabResultado.Padding = New System.Windows.Forms.Padding(3)")
        f.WriteLine("        Me.TabResultado.Size = New System.Drawing.Size(984, 490)")
        f.WriteLine("        Me.TabResultado.TabIndex = 1")
        f.WriteLine("        Me.TabResultado.Text = ""Resultado""")
        f.WriteLine("        Me.TabResultado.UseVisualStyleBackColor = True")


        Propriedade(Entidade, f)

        'Sub propriedades

        f.WriteLine("        'StatusStrip1")
        f.WriteLine("        '")
        f.WriteLine("        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TotalRegRotulo, Me.TotalRegValor})")
        f.WriteLine("        Me.StatusStrip1.Location = New System.Drawing.Point(3, 465)")
        f.WriteLine("        Me.StatusStrip1.Name = ""StatusStrip1""")
        f.WriteLine("        Me.StatusStrip1.Size = New System.Drawing.Size(978, 22)")
        f.WriteLine("        Me.StatusStrip1.TabIndex = 1")
        f.WriteLine("        Me.StatusStrip1.Text = ""StatusStrip1""")
        f.WriteLine("        '")
        f.WriteLine("        'TotalRegRotulo")
        f.WriteLine("        '")
        f.WriteLine("        Me.TotalRegRotulo.Name = ""TotalRegRotulo""")
        f.WriteLine("        Me.TotalRegRotulo.Size = New System.Drawing.Size(88, 17)")
        f.WriteLine("        Me.TotalRegRotulo.Text = ""Total Registros:""")
        f.WriteLine("        '")
        f.WriteLine("        'TotalRegValor")
        f.WriteLine("        '")
        f.WriteLine("        Me.TotalRegValor.Name = ""TotalRegValor""")
        f.WriteLine("        Me.TotalRegValor.Size = New System.Drawing.Size(13, 17)")
        f.WriteLine("        Me.TotalRegValor.Text = ""0""")
        f.WriteLine("        '")
        f.WriteLine("        'ToolStripStatusLabel1")
        f.WriteLine("        '")
        f.WriteLine("        Me.ToolStripStatusLabel1.Name = ""ToolStripStatusLabel1""")
        f.WriteLine("        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(734, 17)")
        f.WriteLine("        Me.ToolStripStatusLabel1.Spring = True")
        f.WriteLine("        Me.ToolStripStatusLabel1.Text = "" """)
        f.WriteLine("        '")
        f.WriteLine("        'ToolStripStatusLabel2")
        f.WriteLine("        '")
        f.WriteLine("        Me.ToolStripStatusLabel2.Name = ""ToolStripStatusLabel2""")
        f.WriteLine("        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(128, 17)")
        f.WriteLine("        Me.ToolStripStatusLabel2.Text = ""Clique duplo para abrir""")
        f.WriteLine("        '")
        f.WriteLine("        'pes_" & Entidade.NomeEntidade & "")
        f.WriteLine("        '")
        f.WriteLine("        Me.AcceptButton = Me.btnPesquisar")
        f.WriteLine("        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)")
        f.WriteLine("        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font")
        f.WriteLine("        Me.ClientSize = New System.Drawing.Size(992, 566)")
        f.WriteLine("        Me.Controls.Add(Me.Tab)")
        f.WriteLine("        Me.Controls.Add(Me.PainelInferior)")
        f.WriteLine("        Me.MinimumSize = New System.Drawing.Size(1000, 600)")
        f.WriteLine("        Me.Name = ""pes_" & Entidade.NomeEntidade & """")
        f.WriteLine("        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen")
        f.WriteLine("        Me.Text = ""Pesquisa de " & Entidade.NomeCadastro & "")
        f.WriteLine("        Me.PainelInferior.ResumeLayout(False)")
        f.WriteLine("        Me.Tab.ResumeLayout(False)")
        f.WriteLine("        Me.TabPesquisar.ResumeLayout(False)")
        f.WriteLine("        Me.TabPesquisar.PerformLayout()")
        f.WriteLine("        Me.TabResultado.ResumeLayout(False)")
        f.WriteLine("        Me.TabResultado.PerformLayout()")


        'Sub Perform layout

        f.WriteLine("        CType(Me.DS" & Entidade.NomeEntidade & ", System.ComponentModel.ISupportInitialize).EndInit()")
        f.WriteLine("        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()")
        f.WriteLine("        Me.ResumeLayout(False)")
        f.WriteLine("")
        f.WriteLine("    End Sub")
        f.WriteLine("    Friend WithEvents PesquisaDataGridView As System.Windows.Forms.DataGridView")
        f.WriteLine("    Friend WithEvents PainelInferior As System.Windows.Forms.Panel")
        f.WriteLine("    Friend WithEvents btnCadastrar As System.Windows.Forms.Button")
        f.WriteLine("    Friend WithEvents btnPesquisar As System.Windows.Forms.Button")
        f.WriteLine("    Friend WithEvents Tab As System.Windows.Forms.TabControl")
        f.WriteLine("    Friend WithEvents TabPesquisar As System.Windows.Forms.TabPage")
        f.WriteLine("    Friend WithEvents TabResultado As System.Windows.Forms.TabPage")
        f.WriteLine("    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip")
        f.WriteLine("    Friend WithEvents TotalRegRotulo As System.Windows.Forms.ToolStripStatusLabel")
        f.WriteLine("    Friend WithEvents TotalRegValor As System.Windows.Forms.ToolStripStatusLabel")
        f.WriteLine("    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel")
        f.WriteLine("    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel")
        f.WriteLine("    Friend WithEvents " & Entidade.NomeEntidade & "BS As System.Windows.Forms.BindingSource")
        f.WriteLine("    Friend WithEvents DS" & Entidade.NomeEntidade & " As Aplicativo.RemoteWse.ds_" & Entidade.NomeEntidade & "")
        f.WriteLine("    Friend WithEvents btnExcluir As System.Windows.Forms.Button")


        Evento(Entidade, f)

        'Sub evento

        f.WriteLine("End Class")
        f.Close()
        f = Nothing
    End Sub


    Private Function rParams(Entidade As EntidadeSigedin) As SqlDataReader
        Return Entidade.Projeto.ConsultaDB("select * from master..sigedin_params where banco = '" & Entidade.Projeto.DatabaseName & "' and procedimento = 'ps_" & Entidade.NomeEntidade & "' order by parameter_id")
    End Function

    Private Sub Declara(Entidade As EntidadeSigedin, f As System.IO.StreamWriter)
        Dim r As SqlDataReader = rParams(Entidade)
        Do While r.Read
            f.WriteLine("        Me." & r!parametro & "_label = New AgogeComponents.Label")
            f.WriteLine("        Me." & r!parametro & "" & "" & " = New AgogeComponents." & NovosNomes(r!cad_controle))
        Loop
        r.Close()


        Dim pess As SqlDataReader = Entidade.Projeto.ConsultaDB(Entidade.Projeto.DatabaseName & ".dbo.ps_" & Entidade.NomeEntidade & " 0")

        For i = 0 To pess.FieldCount - 1
            Dim xs As SqlDataReader = Entidade.Projeto.ConsultaDB("sp_parcol '" & Entidade.Projeto.DatabaseName & "', '" & Entidade.NomeEntidade & "', '" & pess.GetName(i) & "'")
            xs.Read()
            f.WriteLine("        Me." & "ps_" & Entidade.NomeEntidade & "_" & xs!coluna & " = New AgogeComponents." & xs!grd_controle)
            xs.Close()
        Next
        pess.Close()

    End Sub

    Private Sub Adiciona(Entidade As EntidadeSigedin, f As System.IO.StreamWriter)
        Dim r As SqlDataReader = rParams(Entidade)
        Do While r.Read
            f.WriteLine("        Me.TabPesquisar.Controls.Add(Me." & r!parametro & "_label)")
            f.WriteLine("        Me.TabPesquisar.Controls.Add(Me." & r!parametro & ")")
        Loop
        r.Close()

        f.WriteLine("		'")
        f.WriteLine("        'PesquisaDataGridView")
        f.WriteLine("        '")
        f.WriteLine("        Me.PesquisaDataGridView.AllowUserToAddRows = False")
        f.WriteLine("        Me.PesquisaDataGridView.AllowUserToDeleteRows = False")
        f.WriteLine("        Me.PesquisaDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _")
        f.WriteLine("            Or System.Windows.Forms.AnchorStyles.Left) _")
        f.WriteLine("            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)")
        f.WriteLine("        Me.PesquisaDataGridView.AutoGenerateColumns = False")
        f.WriteLine("        Me.PesquisaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize")


        Dim pess As SqlDataReader = Entidade.Projeto.ConsultaDB(Entidade.Projeto.DatabaseName & ".dbo.ps_" & Entidade.NomeEntidade & " 0")

        Dim a As String

        a = "        Me.PesquisaDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {"

        For i = 0 To pess.FieldCount - 1
            Dim xs As SqlDataReader = Entidade.Projeto.ConsultaDB("sp_parcol '" & Entidade.Projeto.DatabaseName & "', '" & Entidade.NomeEntidade & "', '" & pess.GetName(i) & "'")
            xs.Read()
            a = a & "Me.ps_" & Entidade.NomeEntidade & "_" & xs!coluna & ", "
            xs.Close()
        Next

        a = Mid(a, 1, Len(a) - 2)

        a = a & "})"

        f.WriteLine(a)


        f.WriteLine("        Me.PesquisaDataGridView.DataSource = Me.BS")
        f.WriteLine("        Me.PesquisaDataGridView.Location = New System.Drawing.Point(3, 3)")
        f.WriteLine("        Me.PesquisaDataGridView.Name = ""PesquisaDataGridView""")
        f.WriteLine("        Me.PesquisaDataGridView.ReadOnly = True")
        f.WriteLine("        Me.PesquisaDataGridView.Size = New System.Drawing.Size(978, 452)")
        f.WriteLine("        Me.PesquisaDataGridView.TabIndex = 1")

    End Sub

    Private Sub Evento(Entidade As EntidadeSigedin, f As System.IO.StreamWriter)
        Dim r As SqlDataReader = rParams(Entidade)
        Do While r.Read
            f.WriteLine("    Friend WithEvents " & r!parametro & "_label As AgogeComponents.Label")
            f.WriteLine("    Friend WithEvents " & r!parametro & " As AgogeComponents." & NovosNomes(r!cad_controle))
        Loop
        r.Close()

        Dim pess As SqlDataReader = Entidade.Projeto.ConsultaDB(Entidade.Projeto.DatabaseName & ".dbo.ps_" & Entidade.NomeEntidade & " 0")
        For i = 0 To pess.FieldCount - 1
            Dim xs As SqlDataReader = Entidade.Projeto.ConsultaDB("sp_parcol '" & Entidade.Projeto.DatabaseName & "', '" & Entidade.NomeEntidade & "', '" & pess.GetName(i) & "'")
            xs.Read()
            f.WriteLine("    Friend WithEvents " & "ps_" & Entidade.NomeEntidade & "_" & xs!coluna & " As System.Windows.Forms." & xs!grd_controle)
            xs.Close()
        Next

    End Sub

    Private Sub Propriedade(Entidade As EntidadeSigedin, f As System.IO.StreamWriter)
        Dim r As SqlDataReader = rParams(Entidade)

        Dim tj As Integer
        tj = 1

        Do While r.Read

            Dim Espacamento As Integer
            Espacamento = 12

            f.WriteLine("        '" & r!parametro & "_label")
            f.WriteLine("        '")
            f.WriteLine("        Me." & r!parametro & "_label" & ".AutoSize = False")
            f.WriteLine("        Me." & r!parametro & "_label" & ".Location = New System.Drawing.Point(10, " & tj * Espacamento & ")")
            f.WriteLine("        Me." & r!parametro & "_label" & ".Name = """ & r!parametro & "_label""")
            f.WriteLine("        Me." & r!parametro & "_label" & ".Size = New System.Drawing.Size(130, 20)")
            f.WriteLine("        Me." & r!parametro & "_label" & ".Text = """ & r!nome_amigavel & ":""")
            f.WriteLine("        Me." & r!parametro & "_label" & ".TextAlign = ContentAlignment.TopRight")
            f.WriteLine("        Me." & r!parametro & "_label" & ".TabIndex = " & tj)
            f.WriteLine("        '")
            f.WriteLine("        '" & r!parametro & "" & "")
            f.WriteLine("        '")
            'NO DATABINDINS - ITS A FINDING FORM
            f.WriteLine("        Me." & r!parametro & "" & "" & ".Location = New System.Drawing.Point(150, " & tj * Espacamento & ")")
            f.WriteLine("        Me." & r!parametro & "" & "" & ".Name = """ & r!parametro & "" & "" & """")

            If (r!tamanho_maximo <> -1) Then
                f.WriteLine("        Me." & r!parametro & "" & "" & ".Size = New System.Drawing.Size(" & r!cad_largura_controle & ", 20)")
            Else
                'Uma linha somente, mesmo se for varchar(max), isso é uma pesquisa
                f.WriteLine("        Me." & r!parametro & "" & "" & ".Size = New System.Drawing.Size(" & r!cad_largura_controle & ", 20)")
                f.WriteLine("        Me." & r!parametro & "" & "" & ".MultiLine = false")
            End If

            If LCase(r!cad_controle) = LCase("CheckBox") Then
                f.WriteLine("        Me." & r!parametro & "" & "" & ".Checked = True")
                f.WriteLine("        Me." & r!parametro & "" & "" & ".CheckState = System.Windows.Forms.CheckState.Indeterminate")
                f.WriteLine("        Me." & r!parametro & "" & "" & ".ThreeState = True")
            End If

            f.WriteLine("        Me." & r!parametro & "" & "" & ".TabIndex = " & tj + 1)
            tj = tj + 2

            If (LCase(r!cad_controle) = LCase("TextBox")) And (r!tamanho_maximo <> -1) Then
                f.WriteLine("        Me." & r!parametro & "" & "" & ".MaxLength = " & r!tamanho_maximo)
            End If

            If LCase(r!cad_controle) = LCase("ComboBox") Then
                f.WriteLine("        Me." & r!parametro & "" & "" & ".DataSource = Me.ds" & Entidade.NomeEntidade)
                f.WriteLine("        Me." & r!parametro & "" & "" & ".DisplayMember = """ & r!cb_source & "." & r!cb_display & """")
                f.WriteLine("        Me." & r!parametro & "" & "" & ".ValueMember = """ & r!cb_source & "." & r!cb_value & """")
                f.WriteLine("        Me." & r!parametro & "" & "" & ".DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList")
            End If

            f.WriteLine("        '")

        Loop
        r.Close()

        Dim pess As SqlDataReader = Entidade.Projeto.ConsultaDB(Entidade.Projeto.DatabaseName & ".dbo.ps_" & Entidade.NomeEntidade & " 0")

        For i = 0 To pess.FieldCount - 1
            Dim xs As SqlDataReader = Entidade.Projeto.ConsultaDB("sp_parcol '" & Entidade.Projeto.DatabaseName & "', '" & Entidade.NomeEntidade & "', '" & pess.GetName(i) & "'")
            xs.Read()

            f.WriteLine("        '" & "ps_" & Entidade.NomeEntidade & "_" & xs!coluna & "")
            f.WriteLine("        '")
            f.WriteLine("        Me." & "ps_" & Entidade.NomeEntidade & "_" & xs!coluna & ".DataPropertyName = """ & xs!coluna & """")
            f.WriteLine("        Me." & "ps_" & Entidade.NomeEntidade & "_" & xs!coluna & ".HeaderText = """ & xs!nome_amigavel & """")
            f.WriteLine("        Me." & "ps_" & Entidade.NomeEntidade & "_" & xs!coluna & ".Name = """ & "ps_" & Entidade.NomeEntidade & "_" & xs!coluna & """")
            f.WriteLine("        Me." & "ps_" & Entidade.NomeEntidade & "_" & xs!coluna & ".Width = " & xs!grd_largura_controle)

            If (xs!grd_controle = "TextBox") And (xs!tamanho_maximo <> -1) Then
                f.WriteLine("        Me." & xs!coluna & ".MaxInputLength = " & xs!tamanho_maximo)
            End If

            If xs!grd_controle = "DataGridViewComboBoxColumn" Then
                'Não existe combobox em resultado de pesquisa
            End If

            xs.Close()
        Next
        pess.Close()

    End Sub

End Class
