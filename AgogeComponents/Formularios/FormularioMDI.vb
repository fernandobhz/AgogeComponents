Public Class FormularioMDI
    Inherits Formulario

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(frmPrincipal As Formulario)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MdiParent = frmPrincipal
    End Sub

End Class