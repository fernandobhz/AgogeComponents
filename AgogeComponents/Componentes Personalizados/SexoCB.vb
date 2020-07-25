Imports System
Imports System.Data
Imports System.Type
Imports System.Windows.Forms

Public Class SexoCB
    Inherits ComboBox

    Private DT As New DataTable

    Public Sub New()
        MyBase.New()
        MyBase.DropDownStyle = ComboBoxStyle.DropDownList

        Dim TP_SEXOColumn As New DataColumn("TP_SEXO", GetType(String))
        Dim NM_SEXOColumn As New DataColumn("NM_SEXO", GetType(String))

        DT.Columns.Add(TP_SEXOColumn)
        DT.Columns.Add(NM_SEXOColumn)

        DT.Rows.Add(DBNull.Value, DBNull.Value)
        DT.Rows.Add("M", "Masculino")
        DT.Rows.Add("F", "Feminino")

        MyBase.DataSource = DT
        MyBase.ValueMember = "TP_SEXO"
        MyBase.DisplayMember = "NM_SEXO"
    End Sub
End Class
