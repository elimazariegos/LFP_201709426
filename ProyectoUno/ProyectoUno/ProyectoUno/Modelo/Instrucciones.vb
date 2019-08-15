Public Class Instrucciones
    Private interlineado As Double
    Private letra As Double
    Private nombreArchivo As String
    Private directorio As String

    Public Sub New(interlineado As Double, letra As Double, nombreArchivo As String, directorio As String)
        Me.interlineado = interlineado
        Me.letra = letra
        Me.nombreArchivo = nombreArchivo
        Me.directorio = directorio
    End Sub

    Public Property Interlineado1 As Double
        Get
            Return interlineado
        End Get
        Set(value As Double)
            interlineado = value
        End Set
    End Property

    Public Property Letra1 As Double
        Get
            Return letra
        End Get
        Set(value As Double)
            letra = value
        End Set
    End Property

    Public Property NombreArchivo1 As String
        Get
            Return nombreArchivo
        End Get
        Set(value As String)
            nombreArchivo = value
        End Set
    End Property

    Public Property Directorio1 As String
        Get
            Return directorio
        End Get
        Set(value As String)
            directorio = value
        End Set
    End Property
End Class
