Public Class ObtenerTokem
    Dim listaToken As New List(Of Token)
    Dim siguiente As Integer

    Dim reservadas = New String() {"INSTRUCCIONES", "VARIABLES", "TEXTO", "Interlineado", "Tamanio_letra",
        "Nombre_archivo", "Direccion_archivo", "Imagen", "Var", "Linea_en_blanco", "Promedio", "Suma", "Resta",
        "Multiplicar", "Division", "Asignar", "entero", "cadena"}

    Dim simbolos = New Integer() {34, 40, 41, 42, 43, 44, 46, 47, 58, 61, 91, 93, 123, 125}

    Public Function obtener(texto As String)

        Dim estado As Integer
        Dim numero As Integer
        Dim lexema As String
        Dim valor As Object
        Dim tipo As String

        Dim lineas() As String = texto.Split(ChrW(10))

        For i = 0 To lineas.Length - 1
            For j = 0 To Len(lineas(i)) - 1
                Dim caracter_actual, caracter_siguiente As String
                caracter_actual = Asc(lineas(i).Chars(j))
                If estado = 0 Then
                    estado = transicion(caracter_actual)
                End If

                Try
                    caracter_siguiente = Asc(lineas(i).Chars(j + 1))
                Catch ex As Exception
                    Try
                        caracter_siguiente = Asc(lineas(i + 1).Chars(0))

                    Catch eax As Exception

                    End Try
                End Try

                siguiente = caracter_siguiente

                Select Case estado
                    Case 1
                        lexema = lexema + lineas(i).Chars(j)

                        If (siguiente >= 65 And siguiente <= 90) Or (siguiente >= 97 And siguiente <= 122) Or
                            (siguiente >= 48 And siguiente <= 57) Or (siguiente = 95) Then
                            estado = 1
                        Else
                            numero = es_reservada(lexema)
                            estado = 0
                        End If
                    Case 2
                        lexema = lexema + lineas(i).Chars(j)

                        If (siguiente >= 48 And siguiente <= 57) Then
                            estado = 2
                        Else
                            numero = 20
                            estado = 0
                        End If
                    Case 100
                        estado = -2
                    Case 1000
                        lexema = lexema + lineas(i).Chars(j)
                        numero = 1000
                        estado = 0
                End Select
                If estado <> 100 Then
                    For k = 0 To 13
                        If estado = simbolos(k) Then
                            lexema = lexema + lineas(i).Chars(j)
                            estado = 0
                            numero = simbolos(k)
                            k = 20
                        End If
                    Next
                End If
                If estado = 0 Then
                    listaToken.Add(New Token(numero, lexema, Nothing, i + 1, j + 1))
                    lexema = ""
                End If
                If estado = -2 Then
                    estado = 0
                End If
            Next
        Next
        listaToken.Add(New Token(666, "#", Nothing, 999, 999))
        Return listaToken
    End Function

    Public Function transicion(n As Integer) As Integer
        Dim cambio = 0

        If (n >= 65 And n <= 90) Or (n >= 97 And n <= 122) Then
            cambio = 1
            Return 1
        ElseIf (n >= 48 And n <= 57) Then
            cambio = 1
            Return 2
        ElseIf (n = 32) Or (n = 13) Or (n = 9) Or (n = 15) Then
            cambio = 1
            Return 100

        End If
        For i = 0 To 13
            If n = simbolos(i) Then
                If n = 123 Then
                    n = 123
                End If
                cambio = 1
                Return n
                Exit For
            End If
        Next
        If cambio = 0 Then
            Return 1000
        End If
    End Function

    Public Function es_reservada(lex As String) As Integer
        Dim cambio = 0
        For i = 0 To 17
            If reservadas(i) = lex Then
                cambio = 1
                Return i + 1
            End If
        Next
        If cambio = 0 Then
            Return 19
        End If
    End Function
End Class
