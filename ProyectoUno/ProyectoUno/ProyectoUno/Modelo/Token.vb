Public Class Token
    Private no As Integer
    Private lexema As String
    Private tipo As String
    Private fila As Integer
    Private columnaInicio As Integer

    Public Sub New(no As Integer, lexema As String, tipo As String, fila As Integer, columnaInicio As Integer)
        Me.no = no
        Me.lexema = lexema
        Me.tipo = tipo
        Me.fila = fila
        Me.columnaInicio = columnaInicio
    End Sub

    Public Property No1 As Integer
        Get
            Return no
        End Get
        Set(value As Integer)
            no = value
        End Set
    End Property

    Public Property Lexema1 As String
        Get
            Return lexema
        End Get
        Set(value As String)
            lexema = value
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

    Public Property Fila1 As Integer
        Get
            Return fila
        End Get
        Set(value As Integer)
            fila = value
        End Set
    End Property

    Public Property ColumnaInicio1 As Integer
        Get
            Return columnaInicio
        End Get
        Set(value As Integer)
            columnaInicio = value
        End Set
    End Property
End Class
