'Imports Microsoft.Reporting.WinForms
'Imports System.Net

'Module Relatorios

'    Public Sub ConfigurarReportViewer(ByRef ReportViewer As ReportViewer, ByVal CaminhoRelatorio As String, Servidor As Servidor)

'        ReportViewer.ShowParameterPrompts = False
'        ReportViewer.ProcessingMode = ProcessingMode.Remote
'        ReportViewer.ServerReport.ReportServerUrl = New Uri(Servidor.RptServer)
'        ReportViewer.ServerReport.ReportPath = IncludeBeginSlash(CaminhoRelatorio)

'        If Servidor.RptDomain = String.Empty Then
'            ReportViewer.ServerReport.ReportServerCredentials.NetworkCredentials = New Net.NetworkCredential(Servidor.RptUser, Servidor.RptPass, Nothing)
'        Else
'            ReportViewer.ServerReport.ReportServerCredentials.SetFormsCredentials(Nothing, Servidor.RptUser, Servidor.RptPass, Servidor.RptDomain)
'        End If

'    End Sub

'    Public Sub AtualizarReportViewer(ReportViewer As ReportViewer, Parametros As List(Of Parametro))
'        Dim rps As New List(Of ReportParameter)

'        For Each p As Parametro In Parametros

'            Dim rp As New ReportParameter()
'            rp.Name = p.Nome
'            rp.Values.Add(p.Valor)

'            rps.Add(rp)
'        Next

'        ReportViewer.ServerReport.SetParameters(rps)
'        ReportViewer.RefreshReport()
'    End Sub

'End Module

