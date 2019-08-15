Imports _LFP_Practica2_201709426

Public Class Token

    Dim lexema As String
    Dim numero_token As Integer
    Dim valor As Object
    Dim fila As Integer
    Dim columna As Integer
    Dim tipo As String

    Public Sub New()
    End Sub

    Public Sub New(lexema As String, numero_token As Integer, valor As Object, fila As Integer, columna As Integer, tipo As String)
        setLexema(lexema)
        setNumero(numero_token)
        setValor(valor)
        setFila(fila)
        setColumna(columna)
        setTipo(tipo)
    End Sub
    Public Function getLexema() As String
        Return Me.lexema
    End Function

    Public Function getNumero() As Integer
        Return Me.numero_token
    End Function

    Public Function getValor() As Object
        Return Me.valor
    End Function

    Public Function getFila() As Integer
        Return Me.fila
    End Function

    Public Function getColumna() As Integer
        Return Me.columna
    End Function

    Public Function getTipo() As String
        Return Me.tipo
    End Function
    Public Sub setLexema(lexema As String)
        Me.lexema = lexema
    End Sub

    Public Sub setNumero(numero_token As Integer)
        Me.numero_token = numero_token
    End Sub

    Public Sub setValor(valor As Object)
        Me.valor = valor
    End Sub

    Public Sub setFila(fila As Integer)
        Me.fila = fila
    End Sub

    Public Sub setColumna(columna As Integer)
        Me.columna = columna
    End Sub
    Public Sub setTipo(tipo As String)
        Me.tipo = tipo
    End Sub
    Public Overrides Function ToString() As String
        Return "Token: " + getLexema() + "  |  Numero_token: " + CType(getNumero(), String) + "   |   fila: " +
            CType(getFila(), String) + "   |    Columna: " + CType(getColumna(), String) + "   |    Valor: " +
            CType(getValor(), String)
    End Function
End Class
