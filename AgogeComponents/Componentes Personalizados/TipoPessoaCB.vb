Imports System
Imports System.Data
Imports System.Type
Imports System.Windows.Forms

Public Class TipoPessoaCB
    Inherits ComboBox

    Private DT As New DataTable("TP_PESSOA")

    Public Sub New()
        MyBase.New()
        MyBase.DropDownStyle = ComboBoxStyle.DropDownList

        Dim TP_PESSOAColumn As New DataColumn("TP_PESSOA", GetType(Char))
        Dim DS_PESSOAColumn As New DataColumn("DS_PESSOA", GetType(String))

        DT.Columns.Add(TP_PESSOAColumn)
        DT.Columns.Add(DS_PESSOAColumn)

        DT.Rows.Add(DBNull.Value, DBNull.Value)
        DT.Rows.Add("F", "Pessoa Física")
        DT.Rows.Add("J", "Pessoa Jurídica")

        MyBase.DataSource = DT
        MyBase.ValueMember = "TP_PESSOA"
        MyBase.DisplayMember = "DS_PESSOA"
    End Sub
End Class
