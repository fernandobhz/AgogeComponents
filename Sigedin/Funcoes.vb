Module Funcoes

    Function NovosNomes(ByVal Nome As String) As String
        Dim ret As String = Nome

        If ret = "frgmaskedtelefone" Then ret = "TelefoneMascara"
        If ret = "frgdatetimepicker" Then ret = "DateTimePicker"
        If ret = "" Then ret = ""

        Return ret
    End Function

    Public Function TrailingBackSlash(ByVal s As String) As String
        If Right(s, 1) <> "\" Then s = s & "\"
        Return s
    End Function

End Module
