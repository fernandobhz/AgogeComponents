Imports System.Data.SqlClient

Public Class WseCode

    Public Sub Criar(Entidade As EntidadeSigedin)
        Dim wse As String
        wse = Entidade.Projeto.WseFile


        Dim temp As String
        temp = wse & ".temp"

        Dim t As New System.IO.StreamWriter(temp)

        Dim s As String

        Dim f As New System.IO.StreamReader(wse, System.Text.Encoding.UTF8)
        Do
            s = f.ReadLine

            If Trim(s) <> "End Class" Then
                t.WriteLine(s)
            Else
                Exit Do
            End If
        Loop
        f.Close()
        f = Nothing

        'Region
        t.WriteLine("#Region """ & Entidade.NomeEntidade & """")


        'LookUP
        'LookUP
        'LookUP
        'LookUP
        t.WriteLine("    <WebMethod()> _")

        t.WriteLine("    Public Function lok_" & Entidade.NomeEntidade & "(ByVal " & Entidade.Identificador & " As Nullable(of Integer)) As ds_" & Entidade.NomeEntidade)
        t.WriteLine("        Dim DS As New ds_" & Entidade.NomeEntidade & "")
        t.WriteLine("")

        Dim ls As SqlDataReader = Entidade.Projeto.ConsultaDB("select distinct cb_source, cb_param_name, cb_nullable from master..sigedin_coluna where banco = '" & Entidade.Projeto.DatabaseName & "' and cb_source is not null and tabela = '" & Entidade.NomeEntidade & "'")
        Do While ls.Read
            t.WriteLine("        Dim ta_" & ls!cb_source & " As New ds_" & Entidade.NomeEntidade & "tableadapters." & ls!cb_source & "tableadapter")
            t.WriteLine("        ta_" & ls!cb_source & ".Fill(dataTable:=DS." & ls!cb_source & ", " & ls!cb_param_name & ":=" & ls!cb_param_name & ", nullable:=True)")

            t.WriteLine("")
        Loop
        ls.Close()

        Dim ts As SqlDataReader = Entidade.Projeto.ConsultaDB("select * from master..sigedin_tabela where tipo_cad = 'sub' and banco = '" & Entidade.Projeto.DatabaseName & "' and tabela_master = '" & Entidade.NomeEntidade & "'")
        Do While ts.Read

            Dim cs As SqlDataReader = Entidade.Projeto.ConsultaDB("select distinct cb_source, cb_param_name, cb_nullable from master..sigedin_coluna where banco = '" & Entidade.Projeto.DatabaseName & "' and cb_source is not null and tabela = '" & ts!tabela & "'")
            Do While cs.Read
                t.WriteLine("        Dim ta_" & cs!cb_source & " As New ds_" & Entidade.NomeEntidade & "tableadapters." & cs!cb_source & "tableadapter")
                t.WriteLine("        ta_" & cs!cb_source & ".Fill(dataTable:=DS." & cs!cb_source & ", " & cs!cb_param_name & ":=" & cs!cb_param_name & ", nullable:=" & cs!cb_nullable & ")")

                t.WriteLine("")
            Loop
            cs.Close()
        Loop
        ts.Close()

        t.WriteLine("        Return DS")
        t.WriteLine("    End Function")
        t.WriteLine("")




        'Get
        'Get
        'Get
        'Get
        t.WriteLine("    <WebMethod()> _")
        If Entidade.Identificador <> "" Then
            If Entidade.IdenTipo = "int" Then
                t.WriteLine("    Public Function get_" & Entidade.NomeEntidade & "(ByVal " & Entidade.Identificador & " as Integer) As ds_" & Entidade.NomeEntidade & "")
            Else
                t.WriteLine("    Public Function get_" & Entidade.NomeEntidade & "(ByVal " & Entidade.Identificador & " as String) As ds_" & Entidade.NomeEntidade & "")
            End If
        Else
            t.WriteLine("    Public Function get_" & Entidade.NomeEntidade & "() As ds_" & Entidade.NomeEntidade & "")
        End If

        t.WriteLine("        Dim DS As New ds_" & Entidade.NomeEntidade & "")
        t.WriteLine("")
        t.WriteLine("        DS.merge(lok_" & Entidade.NomeEntidade & "(" & Entidade.Identificador & "))")
        t.WriteLine("")
        t.WriteLine("        Dim ta_" & Entidade.NomeEntidade & " As New ds_" & Entidade.NomeEntidade & "tableadapters." & Entidade.NomeEntidade & "tableadapter")

        s = "        ta_" & Entidade.NomeEntidade & ".Fill(dataTable:=DS." & Entidade.NomeEntidade

        If Entidade.Identificador <> "" Then
            s = s & ", " & Entidade.Identificador & ":=" & Entidade.Identificador
        End If
        'If tipocad = "cad" Then
        '    s = s & ", " & identificador & ":=" & identificador
        'End If
        s = s & ")"
        t.WriteLine(s)

        t.WriteLine("")


        ts = Entidade.Projeto.ConsultaDB("select * from master..sigedin_tabela where tipo_cad = 'sub' and banco = '" & Entidade.Projeto.DatabaseName & "' and tabela_master = '" & Entidade.NomeEntidade & "'")

        Do While ts.Read
            t.WriteLine("        Dim ta_" & ts!tabela & " As New ds_" & Entidade.NomeEntidade & "tableadapters." & ts!tabela & "tableadapter")
            s = "        ta_" & ts!tabela & ".Fill(dataTable:=DS." & ts!tabela
            If Entidade.TipoCad = "cad" Then
                s = s & ", " & Entidade.Identificador & ":=" & Entidade.Identificador
            End If
            s = s & ")"
            t.WriteLine(s)

            t.WriteLine("")
        Loop
        ts.Close()


        t.WriteLine("        Return DS")
        t.WriteLine("    End Function")
        t.WriteLine("")



        'Set
        'Set
        'Set
        'Set
        'Set
        t.WriteLine("    <WebMethod()> _")
        t.WriteLine("    Public Sub set_" & Entidade.NomeEntidade & "(ByRef DS As ds_" & Entidade.NomeEntidade & ")")

        t.WriteLine("        Dim ta_" & Entidade.NomeEntidade & " As New ds_" & Entidade.NomeEntidade & "tableadapters." & Entidade.NomeEntidade & "tableadapter")

        ts = Entidade.Projeto.ConsultaDB("select * from master..sigedin_tabela where tipo_cad = 'sub' and banco = '" & Entidade.Projeto.DatabaseName & "' and tabela_master = '" & Entidade.NomeEntidade & "'")
        Do While ts.Read
            'If ts!tv = "t" Then
            t.WriteLine("        Dim ta_" & ts!tabela & " As New ds_" & Entidade.NomeEntidade & "tableadapters." & ts!tabela & "tableadapter")
            'End If
        Loop
        ts.Close()

        t.WriteLine("")

        t.WriteLine("        Using ts As New TransactionScope")

        ts = Entidade.Projeto.ConsultaDB("select * from master..sigedin_tabela where tipo_cad = 'sub' and banco = '" & Entidade.Projeto.DatabaseName & "' and tabela_master = '" & Entidade.NomeEntidade & "'")
        Do While ts.Read
            'If ts!tv = "t" Then
            t.WriteLine("            ta_" & ts!tabela & ".Update(DS." & ts!tabela & ".Select("""", """", DataViewRowState.Deleted))")
            'End If
        Loop
        ts.Close()

        t.WriteLine("")

        t.WriteLine("            ta_" & Entidade.NomeEntidade & ".Update(DS." & Entidade.NomeEntidade & ")")

        t.WriteLine("")

        ts = Entidade.Projeto.ConsultaDB("select * from master..sigedin_tabela where tipo_cad = 'sub' and banco = '" & Entidade.Projeto.DatabaseName & "' and tabela_master = '" & Entidade.NomeEntidade & "'")
        Do While ts.Read
            'If ts!tv = "t" Then
            t.WriteLine("            ta_" & ts!tabela & ".Update(DS." & ts!tabela & ")")
            'End If
        Loop
        ts.Close()

        t.WriteLine("")
        t.WriteLine("            ts.complete")
        t.WriteLine("        End Using")
        t.WriteLine("    End Sub")
        t.WriteLine("")


        Dim ParamsSQL As String
        ParamsSQL = "select * from master..sigedin_params where banco = '" & Entidade.Projeto.DatabaseName & "' and procedimento = 'ps_" & Entidade.NomeEntidade & "' order by parameter_id"

        'Pesquisa
        'Pesquisa
        'Pesquisa
        'Pesquisa
        'Pesquisa
        'Pesquisa
        If Entidade.TipoCad = "cad" Then




            t.WriteLine("    <WebMethod()> _")

            t.WriteLine("    Public Function pes_" & Entidade.NomeEntidade & "( _ ")

            s = String.Empty

            Dim rs As SqlDataReader = Entidade.Projeto.ConsultaDB(ParamsSQL)
            Do While rs.Read

                If rs!tipo_dado = "int" Then
                    s = s & "        ByVal " & rs!parametro & " as Nullable(Of Integer)"
                ElseIf rs!tipo_dado = "bigint" Then
                    s = s & "        ByVal " & rs!parametro & " as Nullable(Of Long)"
                ElseIf rs!tipo_dado = "date" Then
                    s = s & "        ByVal " & rs!parametro & " as Nullable(Of Date)"
                ElseIf rs!tipo_dado = "bit" Then
                    s = s & "        ByVal " & rs!parametro & " as Nullable(Of Boolean)"
                Else
                    s = s & "        ByVal " & rs!parametro & " as String"
                End If

                s = s & ", _" & vbCrLf

            Loop
            rs.Close()


            s = Left(s, Len(s) - 5) & " _"
            t.WriteLine(s)

            rs.Close()
            t.WriteLine("        ) As ds_" & Entidade.NomeEntidade & "")

            t.WriteLine("        Dim DS As New ds_" & Entidade.NomeEntidade & "")
            t.WriteLine("")
            t.WriteLine("        Dim ps_" & Entidade.NomeEntidade & " As New ds_" & Entidade.NomeEntidade & "tableadapters.ps_" & Entidade.NomeEntidade & "tableadapter")

            t.WriteLine("        ps_" & Entidade.NomeEntidade & ".Fill( _")
            t.WriteLine("            dataTable:=DS.ps_" & Entidade.NomeEntidade & ", _")

            s = String.Empty

            rs = Entidade.Projeto.ConsultaDB(ParamsSQL)
            Do While rs.Read
                s = s & "            " & rs!parametro & ":=" & rs!parametro

                s = s & ", _" & vbCrLf
            Loop
            rs.Close()

            s = Left(s, Len(s) - 5) & " _"
            t.WriteLine(s)

            t.WriteLine("        )")
            t.WriteLine("")
            t.WriteLine("        Return DS")
            t.WriteLine("    End Function")
            t.WriteLine("")

            rs.Close()
            rs = Nothing

        End If

        t.WriteLine("#End Region")

        t.WriteLine("")

        t.Write("End Class")

        t.Close()

        System.IO.File.Delete(wse)
        System.IO.File.Move(temp, wse)

        t = Nothing

    End Sub


End Class
