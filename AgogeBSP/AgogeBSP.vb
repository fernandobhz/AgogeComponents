Imports NITGEN.SDK.NBioBSP

Public Module AgogeBSP

    Private m_NBioAPI As New NBioAPI
    Private DispositivoAberto As Boolean

    Function CapturarDigital() As String
        Dim Ret As UInt32
        AbrirDispositivo()

        Dim hFIR As NBioAPI.Type.HFIR

        Ret = m_NBioAPI.Capture(hFIR, NBioAPI.Type.TIMEOUT.INFINITE, Nothing)
        Select Case Ret
            Case NBioAPI.Error.NONE
            Case NBioAPI.Error.USER_CANCEL : Throw New UsuarioCancelou
            Case NBioAPI.Error.DEVICE_NOT_OPENED : Throw New Exception("DEVICE_NOT_OPENED")
            Case Else : Throw New Exception(Ret & ": " & NBioAPI.Error.GetErrorDescription(Ret))
        End Select
        FecharDispositivo()

        Dim m_textFir As NBioAPI.Type.FIR_TEXTENCODE
        Select Case m_NBioAPI.GetTextFIRFromHandle(hFIR, m_textFir, True)
            Case NBioAPI.Error.NONE
            Case NBioAPI.Error.INVALID_HANDLE : Throw New Exception("INVALID_HANDLE")
            Case Else : Throw New Exception
        End Select

        Return m_textFir.TextFIR

    End Function

    Function VerificarDigital(DigitalArmazenada As String) As Boolean
        Dim DigitalCapturada As String = CapturarDigital()
        Return VerificarDigital(DigitalCapturada, DigitalArmazenada)
    End Function

    Function VerificarDigital2(DigitalArmazenada As String) As Boolean

        Dim result As Boolean

        AbrirDispositivo()

        Select Case m_NBioAPI.Verify(Digital2FIR(DigitalArmazenada), result, Nothing)
            Case NBioAPI.Error.NONE
            Case NBioAPI.Error.USER_CANCEL : Throw New UsuarioCancelou
            Case NBioAPI.Error.INVALID_HANDLE : Throw New Exception("INVALID_HANDLE")
            Case NBioAPI.Error.ENCRYPTED_DATA_ERROR : Throw New Exception("ENCRYPTED_DATA_ERROR")
            Case NBioAPI.Error.INTERNAL_CHECKSUM_FAIL : Throw New Exception("INTERNAL_CHECKSUM_FAIL")
            Case Else : Throw New Exception
        End Select

        FecharDispositivo()

        Return result

    End Function

    Function VerificarDigital(DigitalCapturada As String, DigitalArmazenada As String) As Boolean

        Dim result As Boolean

        Select Case m_NBioAPI.VerifyMatch(Digital2FIR(DigitalCapturada), Digital2FIR(DigitalArmazenada), result, Nothing)
            Case NBioAPI.Error.NONE
            Case NBioAPI.Error.INVALID_HANDLE : Throw New Exception("INVALID_HANDLE")
            Case NBioAPI.Error.ENCRYPTED_DATA_ERROR : Throw New Exception("ENCRYPTED_DATA_ERROR")
            Case NBioAPI.Error.INTERNAL_CHECKSUM_FAIL : Throw New Exception("INTERNAL_CHECKSUM_FAIL")
            Case NBioAPI.Error.MUST_BE_PROCESSED_DATA : Throw New Exception("MUST_BE_PROCESSED_DATA")
            Case Else : Throw New Exception
        End Select

        Return result

    End Function

    Private Function Digital2FIR(Digital As String) As NBioAPI.Type.FIR_TEXTENCODE
        Dim m_textFIR As New NBioAPI.Type.FIR_TEXTENCODE
        m_textFIR.TextFIR = Digital
        Return m_textFIR
    End Function

    Public Function ExisteDispositivo() As Boolean
        Dim nNumDevice As UInt32
        m_NBioAPI.EnumerateDevice(nNumDevice, Nothing)

        If nNumDevice > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub AbrirDispositivo()
        If Not DispositivoAberto Then
            Select Case m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO)
                Case NBioAPI.Error.NONE
                Case NBioAPI.Error.DEVICE_ALREADY_OPENED : Throw New Exception("DEVICE_ALREADY_OPENED")
                Case NBioAPI.Error.DEVICE_OPEN_FAIL : Throw New Exception("DEVICE_OPEN_FAIL")
                Case Else : Throw New Exception
            End Select
            DispositivoAberto = True
        End If

    End Sub

    Private Sub FecharDispositivo()
        If DispositivoAberto Then
            Select Case m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO)
                Case NBioAPI.Error.NONE
                Case NBioAPI.Error.DEVICE_NOT_OPENED : Throw New Exception("DEVICE_NOT_OPENED")
                Case NBioAPI.Error.WRONG_DEVICE_ID : Throw New Exception("WRONG_DEVICE_ID")
                Case Else : Throw New Exception
            End Select
            DispositivoAberto = False
        End If
    End Sub

End Module

Public Class UsuarioCancelou
    Inherits Exception
End Class
