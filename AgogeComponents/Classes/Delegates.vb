Public Delegate Sub AcaoDelegate()

Public Delegate Function ParametroPesquisaDelegate() As List(Of Parametro)

Public Delegate Function WseLookUpDelegate(ByVal codigo As Nullable(Of Integer)) As DataSet
Public Delegate Function WseGetDelegate(ByVal codigo As Integer) As DataSet
Public Delegate Sub WseSetDelegate(ByRef DS As DataSet)
