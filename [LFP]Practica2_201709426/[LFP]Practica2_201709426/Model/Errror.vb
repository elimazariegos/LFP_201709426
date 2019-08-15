Public Class Errror
    Dim lexema As String
    Dim fila As Integer
    Dim columna As Integer
    Dim descripcion As String

    Public Sub New(lexema As String, fila As Integer, columna As Integer, descripcion As String)
        Me.lexema = lexema
        Me.fila = fila
        Me.columna = columna
        Me.descripcion = descripcion
    End Sub

    Public Overrides Function ToString() As String
        Return "Token: " + lexema + "   |  " + "Fila: " + CType(fila, String) + "  |   Columna: " + CType(columna, String) + "  |   descripcion: " + descripcion
    End Function
End Class
