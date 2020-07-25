Imports System.Data.SqlClient

Public Class CadDesigner

    Public Sub Criar(Entidade As EntidadeSigedin)
        Dim hide_pkfk As Boolean
        hide_pkfk = True

        Dim f As New System.IO.StreamWriter(Entidade.CadDesignerFile, False, System.Text.Encoding.UTF8)
        f.WriteLine("Partial Class cad" & Entidade.NomeEntidade & "Sigedin")
        f.WriteLine("    Inherits FormularioCrud")
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
        f.WriteLine("        Me.btnCancelar = New System.Windows.Forms.Button")
        f.WriteLine("        Me.btnSalvar = New System.Windows.Forms.Button")
        f.WriteLine("        Me.Tab = New System.Windows.Forms.TabControl")
        f.WriteLine("        Me.TabCad = New System.Windows.Forms.TabPage")


        Declara(Entidade, f)

        'Sub declara

        f.WriteLine("        Me.BS = New BindingSource")
        f.WriteLine("        Me.PainelInferior.SuspendLayout()")
        f.WriteLine("        Me.Tab.SuspendLayout()")
        f.WriteLine("        Me.TabCad.SuspendLayout()")

        'Sub suspeend

        f.WriteLine("        Me.ds" & Entidade.NomeEntidade & " = New Aplicativo.RemoteWse.ds_" & Entidade.NomeEntidade & "")
        f.WriteLine("        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()")
        f.WriteLine("        Me.SuspendLayout()")
        f.WriteLine("        '")
        f.WriteLine("        'PainelInferior")
        f.WriteLine("        '")
        f.WriteLine("        Me.PainelInferior.Controls.Add(Me.btnExcluir)")
        f.WriteLine("        Me.PainelInferior.Controls.Add(Me.btnCancelar)")
        f.WriteLine("        Me.PainelInferior.Controls.Add(Me.btnSalvar)")
        f.WriteLine("        Me.PainelInferior.Dock = System.Windows.Forms.DockStyle.Bottom")
        f.WriteLine("        Me.PainelInferior.Location = New System.Drawing.Point(0, 512)")
        f.WriteLine("        Me.PainelInferior.Name = ""PainelInferior""")
        f.WriteLine("        Me.PainelInferior.Size = New System.Drawing.Size(984, 50)")
        f.WriteLine("        Me.PainelInferior.TabIndex = 4")
        f.WriteLine("        '")
        f.WriteLine("        'btnCancelar")
        f.WriteLine("        '")
        f.WriteLine("        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)")
        f.WriteLine("        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel")
        f.WriteLine("        Me.btnCancelar.Image = Global.Aplicativo.My.Resources.Resources.Cancelar")
        f.WriteLine("        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft")
        f.WriteLine("        Me.btnCancelar.Location = New System.Drawing.Point(668, 0)")
        f.WriteLine("        Me.btnCancelar.Name = ""btnCancelar""")
        f.WriteLine("        Me.btnCancelar.Size = New System.Drawing.Size(150, 50)")
        f.WriteLine("        Me.btnCancelar.TabIndex = 0")
        f.WriteLine("        Me.btnCancelar.Text = ""               Cancelar""")
        f.WriteLine("        Me.btnCancelar.UseVisualStyleBackColor = True")
        f.WriteLine("        '")
        f.WriteLine("        'btnExcluir")
        f.WriteLine("        '")
        f.WriteLine("        Me.btnExcluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)")
        f.WriteLine("        Me.btnExcluir.Image = Global.Aplicativo.My.Resources.Resources.Excluir")
        f.WriteLine("        Me.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft")
        f.WriteLine("        Me.btnExcluir.Location = New System.Drawing.Point(0, 0)")
        f.WriteLine("        Me.btnExcluir.Name = ""btnExcluir""")
        f.WriteLine("        Me.btnExcluir.Size = New System.Drawing.Size(150, 50)")
        f.WriteLine("        Me.btnExcluir.TabIndex = 3")
        f.WriteLine("        Me.btnExcluir.TabStop = False")
        f.WriteLine("        Me.btnExcluir.Text = ""               Excluir""")
        f.WriteLine("        Me.btnExcluir.UseVisualStyleBackColor = True")
        f.WriteLine("        '")
        f.WriteLine("        'btnSalvar")
        f.WriteLine("        '")
        f.WriteLine("        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)")
        f.WriteLine("        Me.btnSalvar.Image = Global.Aplicativo.My.Resources.Resources.Salvar")
        f.WriteLine("        Me.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft")
        f.WriteLine("        Me.btnSalvar.Location = New System.Drawing.Point(831, 0)")
        f.WriteLine("        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)")
        f.WriteLine("        Me.btnSalvar.Name = ""btnSalvar""")
        f.WriteLine("        Me.btnSalvar.Size = New System.Drawing.Size(150, 50)")
        f.WriteLine("        Me.btnSalvar.TabIndex = 1")
        f.WriteLine("        Me.btnSalvar.Text = ""               Salvar""")
        f.WriteLine("        Me.btnSalvar.UseVisualStyleBackColor = True")
        f.WriteLine("        '")
        f.WriteLine("        'Tab")
        f.WriteLine("        '")
        f.WriteLine("        Me.Tab.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _")
        f.WriteLine("                    Or System.Windows.Forms.AnchorStyles.Left) _")
        f.WriteLine("                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)")
        f.WriteLine("        Me.Tab.Controls.Add(Me.TabCad)")


        f.WriteLine("        Me.Tab.Location = New System.Drawing.Point(0, 0)")
        f.WriteLine("        Me.Tab.Name = ""Tab""")
        f.WriteLine("        Me.Tab.SelectedIndex = 0")
        f.WriteLine("        Me.Tab.Size = New System.Drawing.Size(984, 509)")
        f.WriteLine("        Me.Tab.TabIndex = 5")


        'Dataset & BindingSources
        f.WriteLine("        'ds" & Entidade.NomeEntidade & "")
        f.WriteLine("        '")
        f.WriteLine("        Me.ds" & Entidade.NomeEntidade & ".DataSetName = ""ds_ " & Entidade.NomeEntidade & """")
        f.WriteLine("        Me.ds" & Entidade.NomeEntidade & ".SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema")
        f.WriteLine("        '")
        f.WriteLine("        'BS")
        f.WriteLine("        '")
        f.WriteLine("        Me.BS.DataMember = """ & LCase(Entidade.NomeEntidade) & """")
        f.WriteLine("        Me.BS.DataSource = Me.ds" & Entidade.NomeEntidade & "")



        'Controles
        f.WriteLine("        '")
        f.WriteLine("        'TabCad")
        f.WriteLine("        '")

        Adiciona(Entidade, f)

        'Sub adiciona

        f.WriteLine("        Me.TabCad.AutoScroll = True")
        f.WriteLine("        Me.TabCad.AutoScrollMargin = New System.Drawing.Size(0, 10)")
        f.WriteLine("        Me.TabCad.Location = New System.Drawing.Point(4, 22)")
        f.WriteLine("        Me.TabCad.Name = ""TabCad""")
        f.WriteLine("        Me.TabCad.Padding = New System.Windows.Forms.Padding(3)")
        f.WriteLine("        Me.TabCad.Size = New System.Drawing.Size(976, 483)")
        f.WriteLine("        Me.TabCad.TabIndex = 0")
        f.WriteLine("        Me.TabCad.Text = ""Cadastro de " & Entidade.NomeCadastro & """")
        f.WriteLine("        Me.TabCad.UseVisualStyleBackColor = True")
        f.WriteLine("        '")


        Propriedade(Entidade, f)

        'Sub propriedades

        f.WriteLine("        'cad_" & Entidade.NomeEntidade & "Sigedin")
        f.WriteLine("        '")
        f.WriteLine("        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)")
        f.WriteLine("        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font")
        f.WriteLine("        Me.ClientSize = New System.Drawing.Size(984, 562)")
        f.WriteLine("        Me.Controls.Add(Me.Tab)")
        f.WriteLine("        Me.Controls.Add(Me.PainelInferior)")
        f.WriteLine("        Me.Name = ""cad_" & Entidade.NomeEntidade & """")
        f.WriteLine("        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen")
        f.WriteLine("        Me.Text = ""Cadastro de " & Entidade.NomeCadastro & """")
        f.WriteLine("        Me.PainelInferior.ResumeLayout(False)")
        f.WriteLine("        Me.Tab.ResumeLayout(False)")
        f.WriteLine("        Me.TabCad.ResumeLayout(False)")
        f.WriteLine("        Me.TabCad.PerformLayout()")

        'Sub Perform layout

        f.WriteLine("        CType(Me.ds" & Entidade.NomeEntidade & ", System.ComponentModel.ISupportInitialize).EndInit()")
        f.WriteLine("        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()")
        f.WriteLine("        Me.ResumeLayout(False)")
        f.WriteLine("")
        f.WriteLine("    End Sub")
        f.WriteLine("    Friend WithEvents PainelInferior As System.Windows.Forms.Panel")
        f.WriteLine("    Friend WithEvents btnCancelar As System.Windows.Forms.Button")
        f.WriteLine("    Friend WithEvents btnSalvar As System.Windows.Forms.Button")
        f.WriteLine("    Friend WithEvents Tab As System.Windows.Forms.TabControl")
        f.WriteLine("    Friend WithEvents TabCad As System.Windows.Forms.TabPage")
        f.WriteLine("    Friend WithEvents ds" & Entidade.NomeEntidade & " As Aplicativo.RemoteWse.ds_" & Entidade.NomeEntidade & "")
        f.WriteLine("    Friend WithEvents btnExcluir As System.Windows.Forms.Button")
        f.WriteLine("    Friend WithEvents BS As BindingSource")


        Evento(Entidade, f)

        'Sub evento

        f.WriteLine("End Class")
        f.Close()
        f = Nothing
    End Sub


    Private Function rColunas(Entidade As EntidadeSigedin) As SqlDataReader
        Return Entidade.Projeto.ConsultaDB("select * from master..sigedin_coluna where banco = '" & Entidade.Projeto.DatabaseName & "' and tabela = '" & Entidade.NomeEntidade & "' order by no_ordem")
    End Function

    Private Sub Declara(Entidade As EntidadeSigedin, f As System.IO.StreamWriter)
        Dim r As SqlDataReader = rColunas(Entidade)
        Do While r.Read
            f.WriteLine("        Me." & r!coluna & "_label = New AgogeComponents.Label")
            f.WriteLine("        Me." & r!coluna & "" & "" & " = New AgogeComponents." & NovosNomes(r!cad_controle))
        Loop
        r.Close()

    End Sub

    Private Sub Adiciona(Entidade As EntidadeSigedin, f As System.IO.StreamWriter)
        Dim r As SqlDataReader = rColunas(Entidade)
        Do While r.Read
            f.WriteLine("        Me.TabCad.Controls.Add(Me." & r!coluna & "_label)")
            f.WriteLine("        Me.TabCad.Controls.Add(Me." & r!coluna & ")")
        Loop
        r.Close()
    End Sub

    Private Sub Evento(Entidade As EntidadeSigedin, f As System.IO.StreamWriter)
        Dim r As SqlDataReader = rColunas(Entidade)
        Do While r.Read
            f.WriteLine("    Friend WithEvents " & r!coluna & "_label As AgogeComponents.Label")
            f.WriteLine("    Friend WithEvents " & r!coluna & " As AgogeComponents." & NovosNomes(r!cad_controle))
        Loop
        r.Close()
    End Sub

    Private Sub Propriedade(Entidade As EntidadeSigedin, f As System.IO.StreamWriter)
        Dim r As SqlDataReader = rColunas(Entidade)

        Dim tj As Integer
        tj = 1

        Do While r.Read

            Dim Espacamento As Integer
            Espacamento = 12

            f.WriteLine("        '" & r!coluna & "_label")
            f.WriteLine("        '")
            f.WriteLine("        Me." & r!coluna & "_label" & ".AutoSize = False")
            f.WriteLine("        Me." & r!coluna & "_label" & ".Location = New System.Drawing.Point(10, " & tj * Espacamento & ")")
            f.WriteLine("        Me." & r!coluna & "_label" & ".Name = """ & r!coluna & "_label""")
            f.WriteLine("        Me." & r!coluna & "_label" & ".Size = New System.Drawing.Size(130, 20)")
            f.WriteLine("        Me." & r!coluna & "_label" & ".Text = """ & r!nome_amigavel & ":""")
            f.WriteLine("        Me." & r!coluna & "_label" & ".TextAlign = ContentAlignment.TopRight")
            f.WriteLine("        Me." & r!coluna & "_label" & ".TabIndex = " & tj)
            f.WriteLine("        '")
            f.WriteLine("        '" & r!coluna & "" & "")
            f.WriteLine("        '")
            f.WriteLine("        Me." & r!coluna & "" & "" & ".DataBindings.Add(New System.Windows.Forms.Binding(""" & r!cad_campo_ligacao & """, Me.BS, """ & r!coluna & """, True))")
            f.WriteLine("        Me." & r!coluna & "" & "" & ".Location = New System.Drawing.Point(150, " & tj * Espacamento & ")")
            f.WriteLine("        Me." & r!coluna & "" & "" & ".Name = """ & r!coluna & "" & "" & """")

            If (r!tamanho_maximo <> -1) Then
                f.WriteLine("        Me." & r!coluna & "" & "" & ".Size = New System.Drawing.Size(" & r!cad_largura_controle & ", 20)")
            Else
                f.WriteLine("        Me." & r!coluna & "" & "" & ".Size = New System.Drawing.Size(" & r!cad_largura_controle & ", 100)")
                f.WriteLine("        Me." & r!coluna & "" & "" & ".MultiLine = true")
                tj = tj + 7
            End If

            f.WriteLine("        Me." & r!coluna & "" & "" & ".TabIndex = " & tj + 1)
            tj = tj + 2

            If (r!cad_controle = "TextBox") And (r!tamanho_maximo <> -1) Then
                f.WriteLine("        Me." & r!coluna & "" & "" & ".MaxLength = " & r!tamanho_maximo)
            End If

            If r!numeracao_automatica = True Then
                f.WriteLine("        Me." & r!coluna & "" & "" & ".ReadOnly = True")
            End If

            If LCase(r!cad_controle) = LCase("ComboBox") Then
                f.WriteLine("        Me." & r!coluna & "" & "" & ".DataSource = Me.ds" & r!tabela)
                f.WriteLine("        Me." & r!coluna & "" & "" & ".DisplayMember = """ & r!cb_source & "." & r!cb_display & """")
                f.WriteLine("        Me." & r!coluna & "" & "" & ".ValueMember = """ & r!cb_source & "." & r!cb_value & """")
                f.WriteLine("        Me." & r!coluna & "" & "" & ".DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList")
            End If

            f.WriteLine("        '")
        Loop
        r.Close()

    End Sub

End Class
