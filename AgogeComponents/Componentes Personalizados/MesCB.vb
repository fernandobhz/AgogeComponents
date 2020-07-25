Imports System
Imports System.Data
Imports System.Type
Imports System.Windows.Forms

Public Class MesCB
    Inherits ComboBox

    Private DT As New DataTable

    Public Sub New()
        MyBase.New()
        MyBase.DropDownStyle = ComboBoxStyle.DropDownList

        Dim NO_MESColumn As New DataColumn("NO_MES", GetType(Integer))
        Dim NM_MESColumn As New DataColumn("NM_MES", GetType(String))

        DT.Columns.Add(NO_MESColumn)
        DT.Columns.Add(NM_MESColumn)

        DT.Rows.Add(DBNull.Value, DBNull.Value)
        DT.Rows.Add("1", "Janeiro")
        DT.Rows.Add("2", "Fevereiro")
        DT.Rows.Add("3", "Março")
        DT.Rows.Add("4", "Abril")
        DT.Rows.Add("5", "Maio")
        DT.Rows.Add("6", "Junho")
        DT.Rows.Add("7", "Julho")
        DT.Rows.Add("8", "Agosto")
        DT.Rows.Add("9", "Setembro")
        DT.Rows.Add("10", "Outubro")
        DT.Rows.Add("11", "Novembro")
        DT.Rows.Add("12", "Dezembro")

        MyBase.DataSource = DT
        MyBase.ValueMember = "NO_MES"
        MyBase.DisplayMember = "NM_MES"
    End Sub
End Class
