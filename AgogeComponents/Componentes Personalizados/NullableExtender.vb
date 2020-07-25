'http://thejoyofcode.com/Databinding_and_Nullable_types_in_WinForms.NET.aspx
'http://stackoverflow.com/questions/4628029/databinding-textbox-cant-exit

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Windows.Forms


<ProvideProperty("NullableBinding", GetType(TextBox))> _
Partial Public Class NullableExtender
    Inherits Component
    Implements IExtenderProvider
    Private _nullables As Dictionary(Of Control, [Boolean]) = New Dictionary(Of Control, Boolean)()

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal container As IContainer)
        container.Add(Me)

        InitializeComponent()
    End Sub

    Public Function CanExtend(ByVal extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
        Return TypeOf extendee Is TextBox
    End Function


    ''' <summary>
    ''' When parsing, set the value to null if the value is empty.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub NullableExtender_Parse(ByVal sender As Object, ByVal e As ConvertEventArgs)
        If e.Value Is Nothing OrElse e.Value.ToString().Length = 0 Then
            e.Value = DBNull.Value
        End If
    End Sub

    ''' <summary>
    ''' This is the extender property. It is actually a method because it takes the control.
    ''' </summary>
    ''' <param name="control"></param>
    <DefaultValue(False), Category("Data")> _
    Public Function GetNullableBinding(ByVal control As Control) As Boolean
        Dim nullableBinding As Boolean = False
        _nullables.TryGetValue(control, nullableBinding)
        Return nullableBinding
    End Function

    ''' <summary>
    ''' This is the extender property. It is actually a method because it takes the control.
    ''' </summary>
    ''' <param name="control"></param>
    ''' <param name="nullable"></param>
    Public Sub SetNullableBinding(ByVal control As Control, ByVal nullable As Boolean)
        If _nullables.ContainsKey(control) Then
            _nullables(control) = nullable
        Else
            _nullables.Add(control, nullable)
        End If
        If nullable Then
            Dim binding As Binding = control.DataBindings("Text")
            If binding IsNot Nothing Then
                ' When the NullableBinding property is set to true and the textbox already has
                ' data bound to the "Text" property
                AddHandler binding.Parse, AddressOf NullableExtender_Parse
            Else
                ' If the "Text" property doesn't already have any data bound to it.
                AddHandler control.DataBindings.CollectionChanged, AddressOf DataBindings_CollectionChanged
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds the Parse function on CollectionChanged event.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataBindings_CollectionChanged(ByVal sender As Object, ByVal e As CollectionChangeEventArgs)
        Dim bindings As ControlBindingsCollection = DirectCast(sender, ControlBindingsCollection)
        Dim binding As Binding = bindings("Text")
        If binding IsNot Nothing Then
            ' When the NullableBinding property is set to true and the textbox already has
            AddHandler binding.Parse, AddressOf NullableExtender_Parse
        End If
    End Sub

End Class
