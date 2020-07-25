Imports System
Imports System.Data
Imports System.Type
Imports System.Windows.Forms

Public Class MesGridCB
    Inherits DataGridViewComboBoxColumn

    Public Sub New()
        MyBase.New()
        MyBase.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        'Tem que ser 2 espaços, um só da erro. Vai entender :|
        MyBase.DefaultCellStyle.NullValue = Space(2)
        MyBase.MaxDropDownItems = 13

        Dim DT As New DataTable("TP_MES")
        Dim NO_MESColumn As New DataColumn("NO_MES", GetType(Byte), Nothing, MappingType.Attribute)
        Dim NM_MESColumn As New DataColumn("NM_MES", GetType(String), Nothing, MappingType.Attribute)

        DT.Columns.Add(NO_MESColumn)
        DT.Columns.Add(NM_MESColumn)

        'Tem que ser 2 espaços, um só da erro. Vai entender :|
        DT.Rows.Add(DBNull.Value, Space(2))
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
