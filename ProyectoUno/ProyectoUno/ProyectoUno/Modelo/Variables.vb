Public Class Variables
    Private nombre As String
    Private tipo As String
    Private valorString As String
    Private valorNumero As Integer

    Public Sub New(nombre As String, tipo As String, valorString As String, valorNumero As Integer)
        Me.nombre = nombre
        Me.tipo = tipo
        Me.valorString = valorString
        Me.valorNumero = valorNumero
    End Sub

    Public Property Nombre1 As String
        Get
            Return nombre
        End Get
        Set(value As String)
            nombre = value
        End Set
    End Property

    Public Property Tipo1 As String
        Get
            Return tipo
        End Get
        Set(value As String)
            tipo = value
        End Set
    End Property

    Public Property ValorString1 As String
        Get
            Return valorString
        End Get
        Set(value As String)
            valorString = value
        End Set
    End Property

    Public Property ValorNumero1 As Integer
        Get
            Return valorNumero
        End Get
        Set(value As Integer)
            valorNumero = value
        End Set
    End Property
End Class
