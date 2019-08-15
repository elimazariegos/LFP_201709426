Imports _LFP_Practica2_201709426.Token
Public Class Obtener_Token

    Dim lista_token As New List(Of Token)
    Dim sig As Integer ' obttiene el siguiente
    Dim reservadas = New String() {"public", "class", "static", "void", "for", "args", "int", "double", "String",
        "boolean", "char", "System", "out", "println", "if", "else"}
    Dim simbolos = New Integer() {33, 34, 39, 40, 41, 42, 43, 44, 45, 46, 47, 59, 60, 61, 62, 91, 93, 95, 123, 125}

    Public Function obtener_tokens(texto As String) As List(Of Token)

        Dim estado As Integer
        Dim numero_token As Integer
        Dim lexema As String
        Dim valor As Object
        Dim tipo As String

        Dim lineas() As String = texto.Split(ChrW(10)) 'separa las lineas del texto
        For i = 0 To lineas.Length - 1
            For j = 0 To Len(lineas(i)) - 1
                If i = 15 Then
                    i = 15
                End If
                Dim caracter_actual, caracter_siguiente As String
                caracter_actual = Asc(lineas(i).Chars(j)) 'asignacion de caracter en la posicion actual (i,j)
                If estado = 0 Then ' si el estado es el inicial vamos a estado transicion
                    estado = estado_transicion(caracter_actual) 'se manda el caracter actual para ver a que estado pertenece
                End If
                Try
                    caracter_siguiente = Asc(lineas(i).Chars(j + 1)) ' se asigna el caracter siguiente, se pone una excepcion por el (j + 1)

                Catch ex As Exception
                End Try

                sig = caracter_siguiente ' se asigna el valor Ascii de caracter_siguiente 

                Select Case estado
                    Case 1
                        lexema = lexema + lineas(i).Chars(j)
                        'valida si el siguiente es una letra o numero o guion bajo
                        If (sig >= 65 And sig <= 90) Or (sig >= 97 And sig <= 122) Or
                             (sig >= 48 And sig <= 57) Or (sig = 95) Then
                            estado = 1
                        Else 'identificador sirve para darle un id al token
                            numero_token = es_reservada(lexema) 'identificador o palabra reservada
                            'tipo = "id"
                            estado = 0
                        End If
                    Case 2
                        lexema = lexema + lineas(i).Chars(j)
                        'validar si el siguiente es un numero o un punto..
                        If (sig >= 48 And sig <= 57) Then
                            estado = 2
                        ElseIf (sig = 46) Then
                            estado = 3
                        Else
                            valor = lexema
                            tipo = "int"
                            numero_token = 18 'Token numero entero
                            estado = 0
                        End If
                    Case 3
                        lexema = lexema + lineas(i).Chars(j)
                        'valida que despues del punto vengan solo numeros
                        If (sig >= 48 And sig <= 57) Then
                            estado = 3
                        Else
                            tipo = "double"
                            numero_token = 19 'Token numero decimal
                            valor = lexema
                            estado = 0
                        End If
                    Case 1000
                        lexema = lexema + lineas(i).Chars(j)
                        numero_token = 1000
                        valor = lexema
                        estado = 0
                    Case 100
                        estado = -2
                End Select
                If estado <> 100 Then
                    For k = 0 To 19 'recorriendo listado de simbolos 
                        If estado = simbolos(k) Then 'validando si el retornado es igual a algun simbolo
                            lexema = lexema + lineas(i).Chars(j)
                            estado = 0
                            numero_token = simbolos(k) 'agregando el numero de token
                            k = 20
                        End If
                    Next

                End If
                If estado = 0 Then
                    If i >= 14 Then
                        i = i
                    End If
                    lista_token.Add(New Token(lexema, numero_token, valor, (i + 1), (j + 1), tipo))
                    lexema = ""
                    tipo = ""
                    valor = Nothing
                End If
                If estado = -2 Then
                    estado = 0
                End If
            Next
        Next
        lista_token.Add(New Token("#", 999, "#", -1, -1, ""))
        Return lista_token
    End Function

    Public Function estado_transicion(n As Integer) As Integer
        Dim cambio As Integer = 0
        If (n >= 65 And n <= 90) Or (n >= 97 And n <= 122) Then ' se validapor medio del numero de caracter en la tabla acsii
            cambio = 1
            Return 1 'estado 1
        ElseIf (n >= 48 And n <= 57) Then ' se validapor medio del numero de caracter en la tabla acsii
            cambio = 1
            Return 2 'estado 2
        ElseIf (n = 32) Or (n = 13) Or (n = 9) Or (n = 15) Then ' espacios en blanco
            cambio = 1
            Return 100
        End If
        For i = 0 To 19
            If n = simbolos(i) Then
                cambio = 1
                i = 20
                Return n
            End If
        Next
        If cambio = 0 Then
            Return 1000
        End If
    End Function

    Public Function es_reservada(lex As String) As Integer
        Dim cambio = 0
        For i = 0 To 15
            If reservadas(i) = lex Then 'recorre el arreglo de palabras reservadas 
                cambio = 1
                Return i + 1 'retorna el identificador del token
            End If
        Next
        If cambio = 0 Then 'si no encuentra palabra reservada alguna es un identificador
            Return 17
        End If
    End Function

End Class
