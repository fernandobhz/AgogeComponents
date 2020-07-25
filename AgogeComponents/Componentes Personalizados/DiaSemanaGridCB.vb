Imports System
Imports System.Data
Imports System.Type
Imports System.Windows.Forms

Public Class DiaSemanaGridCB
    Inherits DataGridViewComboBoxColumn

    Public Sub New()
        MyBase.New()
        MyBase.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        'Tem que ser 2 espaços, um só da erro. Vai entender :|
        MyBase.DefaultCellStyle.NullValue = Space(2)
        MyBase.MaxDropDownItems = 13

        Dim DT As New DataTable("TP_MES")
        Dim NO_MESColumn As New DataColumn("NO_DIA", GetType(Integer), Nothing, MappingType.Attribute)
        Dim NM_MESColumn As New DataColumn("NM_DIA", GetType(String), Nothing, MappingType.Attribute)

        DT.Columns.Add(NO_MESColumn)
        DT.Columns.Add(NM_MESColumn)

        'Tem que ser 2 espaços, um só da erro. Vai entender :|
        DT.Rows.Add(DBNull.Value, Space(2))
        DT.Rows.Add(1, "Domingo")
        DT.Rows.Add(2, "Segunda-feira")
        DT.Rows.Add(3, "Terça-feira")
        DT.Rows.Add(4, "Quarta-feira")
        DT.Rows.Add(5, "Quinta-feira")
        DT.Rows.Add(6, "Sexta-feira")
        DT.Rows.Add(7, "Sabado")

        MyBase.DataSource = DT
        MyBase.ValueMember = "NO_DIA"
        MyBase.DisplayMember = "NM_DIA"
    End Sub

End Class
