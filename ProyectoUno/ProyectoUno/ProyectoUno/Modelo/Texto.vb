Public Class Texto
    Private texto As String
    Private es As Integer
    Private v As String


    Public Sub New(texto As String, es As Integer)
        Me.texto = texto
        Me.es = es
    End Sub

    Public Property Texto1 As String
        Get
            Return texto
        End Get
        Set(value As String)
            texto = value
        End Set
    End Property

    Public Property Es1 As Integer
        Get
            Return es
        End Get
        Set(value As Integer)
            es = value
        End Set
    End Property
End Class
