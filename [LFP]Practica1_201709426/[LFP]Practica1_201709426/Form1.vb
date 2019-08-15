
Public Class Form1
    Dim id_ventana As Integer = 0 ' guarda el id de cada ventana
    Dim index_global As Integer = 0 ' sirve para recorrer las listas 
    Dim editor As New TextBox ' editor de texto
    Dim editores(100) As TextBox ' arreglo de editores

    Dim token As String ' para ir guardando cada caracter
    Dim siguiente As Integer ' obttiene el siguiente

    Dim numero_tokens As New ArrayList 'lusta para numero de tokens
    Dim lexemas As New ArrayList 'lista para los lexemas
    Dim atributo As New ArrayList 'sta para los atributos
    Dim errores As New ArrayList 'lista para los errores
    Dim comentarios As New ArrayList 'ista para los comentarios

    Dim filas As New ArrayList 'ista para las filas 
    Dim columnas As New ArrayList 'lista para las columnas

    Dim dictionary As New Dictionary(Of String, Object)
    ' 1        2        3       4       5         6       7        8
    Dim palabras_resevadas = New String() {"VAR", "ASIG", "OPERA", "SUMA", "RESTA", "MULT", "DIVI", "SALIDA"}
    'obtener todos los tokens
    Private Sub obtener_tokens(texto As String)
        token = ""
        Dim estado = 0
        Dim n_token As Integer
        Dim lineas() As String = texto.Split(ChrW(10)) 'separa las lineas del texto cada vez que aparezca un salto de linea
        Dim i, j As Integer

        For i = 0 To lineas.Length - 1 'recorrer filas de la cadena
            For j = 0 To Len(lineas(i)) - 1 'recorrer columnas de la cadena
                Dim caracter_actual, caracter_siguiente As String
                caracter_actual = Asc(lineas(i).Chars(j)) 'asignacion de caracter en la posicion actual (i,j)

                If estado = 0 Then ' si el estado es el inicial vamos a estado transicion
                    estado = estado_transicion(caracter_actual) 'se manda el caracter actual para ver a que estado pertenece
                End If

                Try
                    caracter_siguiente = Asc(lineas(i).Chars(j + 1)) ' se asigna el caracter siguiente, se pone una excepcion por el (j + 1)
                Catch ex As Exception
                End Try

                siguiente = caracter_siguiente ' se asigna el valor Ascii de caracter_siguiente 

                Select Case estado
                    Case 1 ' estado de aceptacion de identificadores
                        token = token + lineas(i).Chars(j) ' va concatenando el caracter de la posicion (i, j)
                        If es_solo_mayuscula(siguiente) Or es_solo_minuscula(siguiente) Or
                            es_solo_numero(siguiente) Then 'valida si el caracter sig es mayuscula, numero o minuscula 
                            estado = 1 'si lo es, va al estado 1 (estado de aceptacion de identificadores)
                        Else ' si  no...
                            n_token = es_palabra_reservada(token) ' token palabra reservada

                            estado = 0 'cambia el estado a 0

                        End If
                    Case 2 ' si viene un numero
                        token = token + lineas(i).Chars(j) 'concatena el caracter en la posicion i,j
                        If es_solo_numero(siguiente) Then 'valida si es solo numero
                            estado = 2 ' nom cambia el estado
                        ElseIf es_punto(siguiente) Then ' valida si viene un punto
                            estado = 10 ' manda al estado de decimal
                        Else

                            n_token = 10 ' solo numero

                            estado = 0
                        End If
                    Case 3 ' estado si viene parentesis
                        token = token + lineas(i).Chars(j) 'concatena el caracter en la posicion i,j
                        n_token = 11 ' guarda el numero de token 
                        estado = 0 ' regresa al estado inicial

                    Case 4 ' si viene parenetsis fin
                        token = token + lineas(i).Chars(j) 'concatena los caracteres
                        n_token = 12 'guarda el numero de token 

                        estado = 0 ' regresa al estado inicial

                    Case 5 ' si viene un punto y coma
                        token = token + lineas(i).Chars(j) 'concatena los caracateres
                        n_token = 13 ' guarda el numero de token

                        estado = 0 ' regresa al estado inicial

                    Case 6 ' si viene una coma
                        token = token + lineas(i).Chars(j) 'concatena el caracter
                        n_token = 14 ' guarda el numero de token

                        estado = 0 ' regresa al estado inicial

                    Case 7 'si viene una comilla
                        token = token + lineas(i).Chars(j)
                        n_token = 15 'guarda el numeor de token
                        estado = 0 ' regresa al estado inicial

                    Case 8 ' si viene diagonal
                        token = token + lineas(i).Chars(j) ' concatena el caracter
                        n_token = 16 ' guarda el numero de token
                        estado = 0' regresa al estado inicial
                    Case 9 ' si vien un asterisco
                        token = token + lineas(i).Chars(j) ' concatena el caracter
                        n_token = 17 'guarda el numero de token
                        estado = 0'regresa al estado inicial
                    Case 10 ' validacion de decimal
                        token = token + lineas(i).Chars(j) 'sigue concatenando numoeros
                        If es_solo_numero(siguiente) Then
                            estado = 10

                        Else
                            n_token = 19 'guarda el numero de token y regresa a la posicion inicial
                            estado = 0 '
                        End If

                    Case 20 ' case de token extranio
                        token = token + lineas(i).Chars(j)
                        n_token = 20
                        estado = 0

                    Case 100
                        estado = -2
                End Select
                If estado = 0 Then
                    filas.Add(i + 1) 'agrega la fila
                    columnas.Add(j + 1) 'agrega la columna
                    numero_tokens.Add(n_token) ' agreaga el numero de token
                    lexemas.Add(token) ' agrega el token
                    token = "" ' limpiar

                    '      estado = estado_transicion(siguiente)
                ElseIf estado = -2 Then
                    estado = 0
                End If

            Next
        Next

        lexemas.Add("$.#.$")
        numero_tokens.Add(20)
        filas.Add(0)
        columnas.Add(0)
        analizador()
        MsgBox("Por favor seleccione la ruta donde desea guardar el html de resultados")
        generar_archivo(html_resultados) 'generar resultados

        'limpiar todoooooooooooo
        index_global = 0
        token = ""
        siguiente = 0
        'lista_consola.text = ""
        numero_tokens.Clear()
        lexemas.Clear()
        atributo.Clear()
        errores.Clear()
        comentarios.Clear()
        filas.Clear()
        columnas.Clear()
        dictionary.Clear()
    End Sub

    'a que estado me dirijo dependiendo del caracter siguiente
    Private Function estado_transicion(ByVal n As Integer) As Integer
        If es_solo_mayuscula(n) Or es_solo_minuscula(n) Then 'validar si viene una letra mayuscula mediante el codigo ascii
            Return 1 'retorna 1 para avanzar al estado 1 (estado de aceptacion de palabras reservadas)
        ElseIf es_solo_numero(n) Then
            Return 2 'retorna 2 si es solo numero
        ElseIf es_parentesis_inicio(n) Then
            Return 3 'retunra 3 si es (
        ElseIf es_parentesis_fin(n) Then
            Return 4 'retorna 4 si es )
        ElseIf es_punto_coma(n) Then
            Return 5 'retorna 5 si es punto y coma
        ElseIf es_coma(n) Then
            Return 6        'retorna 6 si es coma
        ElseIf es_doble_comilla(n) Then
            Return 7        'retorna 7 si es doble comilla
        ElseIf es_diagonal(n) Then
            Return 8        'retorna 8 si es diagonal
        ElseIf es_asterisco(n) Then
            Return 9        'retorna 9 si es asterisco
        ElseIf es_espacios(n) Then
            Return 100      'retorna 100 si es algun espacio
        Else
            Return 20
        End If
    End Function
    'analizar los tokens obtenidos
    Private Sub analizador() ' sirve para ver que funcion hay que analizar
        Try
            While lexemas(index_global) <> "$.#.$" 'caracter extranio agregado al final de la lista como tope
                If lexemas(index_global) = "VAR" Then ' si el token es VAR
                    declarar() ' nos vamos al metodo declarar
                ElseIf lexemas(index_global) = "ASIG" Then ' si el token es ASIG
                    asignar() ' nos vamos al metodo asignar
                ElseIf lexemas(index_global) = "OPERA" Then ' si el token es OPERA
                    operar() ' nos vamos al metodo operar
                ElseIf lexemas(index_global) = "SUMA" Then ' si el token es SUMA
                    sumar() ' nos vamos al metodo suma
                ElseIf lexemas(index_global) = "RESTA" Then ' Si el token es RESTA
                    restar() ' nos vamos al metodo resta
                ElseIf lexemas(index_global) = "MULT" Then ' si el token es MULT
                    multiplicar() ' nos vamos al metodo multiplicar
                ElseIf lexemas(index_global) = "DIVI" Then 'si el token es DIVI
                    dividir() ' nos vamos al metodo dividir
                ElseIf lexemas(index_global) = "SALIDA" Then ' Si el token es SALIDA
                    salida() ' nos vamos al metodo salida
                ElseIf lexemas(index_global) = "/" Then ' si viene una diagonal
                    If lexemas(index_global + 1) = "/" Then ' valida si la siguiente es diagonal o asterisco
                        comentario_linea() ' nos vamos al metod de comentario de lina
                    ElseIf lexemas(index_global + 1) = "*" Then
                        comentario_multilinea() 'nos vamos al metodo de comentario de multilinea
                    Else
                        'capturar_error("se esperaba / o *")
                    End If
                Else
                    capturar_error("Se esperaba una palabra reservada") 'si no es error lexico
                End If
            End While

        Catch ex As Exception

        End Try


    End Sub
    'funcion var
    Private Function declarar()
        If lexemas(index_global) = "VAR" Then
            consumir() ' si es var se incrementa el index en consumir
        Else
            capturar_error("se esperaba un identificador")
        End If
        If lexemas(index_global) = "(" Then
            consumir() ' si es ( se incrementa el index en consumir
        Else

        End If
        While lexemas(index_global) <> ")"
            If numero_tokens(index_global) = 9 Then ' valida si hya mas de un id
                dictionary.Add(lexemas(index_global), Nothing)
                consumir()
            Else
                '   capturar_error("se esperaba un identificador")
            End If
            If lexemas(index_global) = "," Then
                consumir() ' si es , se incrementa el index en consumir
            Else
                '  capturar_error("se esperaba coma ,")
            End If
        End While
        If lexemas(index_global) = ")" Then
            consumir() ' si es ) se incrementa el index en consumir
        Else
            'capturar_error("se esperaba parentesis fin )")
        End If
        If lexemas(index_global) = ";" Then
            consumir() ' si es ; se incrementa el index en consumir
        Else
            'capturar_error("se esperaba punto y coma ;")
        End If

    End Function
    'funcion opera
    Private Function operar()
        Dim valor As Double = 0 'se guardara el valor de la operacoin
        Dim token_ident As String

        If lexemas(index_global) = "OPERA" Then
            consumir() ' si es OPERA se incrementa el index en consumir
        Else
            capturar_error("se esperaba un identificador")
        End If
        If numero_tokens(index_global) = 9 Then
            token_ident = lexemas(index_global)
            consumir() ' si es id se incrementa el index en consumir
        Else
            capturar_error("no ha sido declarado " + lexemas(index_global))
        End If
        'valida que operacion desea hacer y retorna el valor
        If lexemas(index_global) = "SUMA" Then
            valor = sumar()
        ElseIf lexemas(index_global) = "RESTA" Then
            valor = restar()
        ElseIf lexemas(index_global) = "MULT" Then
            valor = multiplicar()
        ElseIf lexemas(index_global) = "DIVI" Then
            valor = dividir()
        End If
        If token_ident <> Nothing Then
            dictionary(token_ident) = valor ' guarda el valor correspondiente al id
        End If
    End Function
    'fincion suma
    Private Function sumar() As Double
        Dim lista As ArrayList 'lista para los numoers
        Dim total As Double = 0.0
        If lexemas(index_global) = "SUMA" Then
            consumir() ' si es SUMA se incrementa el index en consumir
        Else
            '    capturar_error("se esperaba un identificador")
        End If
        If lexemas(index_global) = "(" Then
            consumir() ' si es ( se incrementa el index en consumir
        Else
            '   capturar_error("se esperaba abrir parentesis (")

        End If
        lista = obtener_lista()
        If lexemas(index_global) = ")" Then
            consumir() ' si es ) se incrementa el index en consumir
        Else
            'capturar_error("se esperaba cerrar parentesis )")
        End If
        If lexemas(index_global) = ";" Then
            consumir() ' si es ; se incrementa el index en consumir
        Else
            'capturar_error("se esperaba punto y coma ;")

        End If

        For i = 0 To lista.Count - 1
            total += CType(lista(i).ToString, Double)
        Next
        Return total
        '    MsgBox("La suma es: " + CType(total, String))
    End Function
    'funcion resta
    Private Function restar() As Double
        Dim num1, num2 As Double
        If lexemas(index_global) = "RESTA" Then
            consumir() ' si es RESTA se incrementa el index en consumir
        Else
            'capturar_error("se esperaba un identificador")

        End If
        If lexemas(index_global) = "(" Then
            consumir() ' si es ( se incrementa el index en consumir
        Else
            'capturar_error("se esperaba abrir parentesis (")

        End If
        num1 = CType(validar_numero_variable(), Double)
        If lexemas(index_global) = "," Then
            consumir() ' si es , se incrementa el index en consumir
        Else
            'capturar_error("se esperaba coma ,")
        End If
        num2 = CType(validar_numero_variable(), Double)
        If lexemas(index_global) = ")" Then
            consumir() ' si es ) se incrementa el index en consumir
        Else
            'capturar_error("se esperaba cerrar parentesis )")

        End If
        If lexemas(index_global) = ";" Then
            consumir() ' si es ; se incrementa el index en consumir
        Else
            'apturar_error("se esperaba punto y coma ;")

        End If
        Return (num1 - num2)
    End Function
    'funcion mult
    Private Function multiplicar() As Double
        Dim lista As ArrayList
        Dim total As Double = 1.0
        If lexemas(index_global) = "MULT" Then
            consumir() ' si es MULT se incrementa el index en consumir
        Else
            'capturar_error("se esperaba un identificador")
        End If
        If lexemas(index_global) = "(" Then
            consumir() ' si es ( se incrementa el index en consumir
        Else
            'capturar_error("se esperaba abrir parentesis (")
        End If
        lista = obtener_lista()
        If lexemas(index_global) = ")" Then
            consumir() ' si es ) se incrementa el index en consumir
        Else
            'capturar_error("se esperaba cerrar parentesis )")
        End If
        If lexemas(index_global) = ";" Then
            consumir() ' si es ; se incrementa el index en consumir
        Else
            'capturar_error("se esperaba punto y coma")
        End If

        Try
            For i = 0 To lista.Count - 1
                total *= CType(lista(i).ToString, Double) 'multiplica cada valor
            Next
        Catch ex As Exception
            index_global -= 1
        End Try

        Return total
    End Function
    'fincion divi
    Private Function dividir() As Double
        Dim num1, num2 As Integer
        If lexemas(index_global) = "DIVI" Then
            consumir() ' si es divi se incrementa el index en consumir
        Else
            '  capturar_error("se esperaba un identificador")
        End If
        If lexemas(index_global) = "(" Then
            consumir() ' si es ( se incrementa el index en consumir
        Else
            ' capturar_error("se esperaba abrir parentesis (")
        End If
        num1 = CType(validar_numero_variable(), Double)
        If lexemas(index_global) = "," Then
            consumir() ' si es , se incrementa el index en consumir
        Else
            'capturar_error("se esperaba coma ,")
        End If
        num2 = CType(validar_numero_variable(), Double)
        If lexemas(index_global) = ")" Then
            consumir() ' si es ) se incrementa el index en consumir
        Else
            'capturar_error("se esperaba cerrar parentesis )")
        End If
        If lexemas(index_global) = ";" Then
            consumir() ' si es ; se incrementa el index en consumir
        Else
            'capturar_error("se esperaba punto y coma ;")
        End If
        If num2 <> 0 Then
            Return (num1 / num2)
        Else
        End If
    End Function
    'fincion asgnar
    Private Function asignar()
        Dim id As String = ""
        Dim valor As Double = 0.0
        If lexemas(index_global) = "ASIG" Then
            consumir() ' si es asig se incrementa el index en consumir
        Else
            '        capturar_error("se esperaba un identificador")
        End If
        If lexemas(index_global) = "(" Then
            consumir() ' si es ( se incrementa el index en consumir
        Else
            '         capturar_error("se esperaba abrir parentesis (")

        End If
        If numero_tokens(index_global) = 9 Then
            id = lexemas(index_global)
            consumir() ' si es id se incrementa el index en consumir
        Else
            '          capturar_error("se esperaba un identificador")
        End If
        If lexemas(index_global) = "," Then
            consumir() ' si es , se incrementa el index en consumir
        Else
            '           capturar_error("se esperaba coma ,")

        End If
        If numero_tokens(index_global) = 10 Or numero_tokens(index_global) = 9 Or
            numero_tokens(index_global) = 19 Then
            valor = CType(lexemas(index_global), Double)
            consumir() ' si es id, entero o decimal se incrementa el index en consumir
        Else
            '            capturar_error("se esperaba un identificador o numero")

        End If
        If lexemas(index_global) = ")" Then
            consumir() ' si es ) se incrementa el index en consumir
        Else
            'capturar_error("se esperaba cerrar parentesis )")

        End If
        If lexemas(index_global) = ";" Then
            consumir() ' si es ; se incrementa el index en consumir
        Else
            'capturar_error("se esperaba punto y coma ;")
        End If
        If id <> "" And valor <> 0 Then
            dictionary(id) = valor
        End If
    End Function
    'funcion salida
    Private Function salida()
        Dim texto_salida As String
        If lexemas(index_global) = "SALIDA" Then
            consumir() ' si es SALIDA se incrementa el index en consumir
        Else
            '          capturar_error("se esperaba un identificador")

        End If
        If lexemas(index_global) = "(" Then
            consumir() ' si es ( se incrementa el index en consumir
        Else
            '  capturar_error("se esperaba abrir parentesis (")

        End If
        If numero_tokens(index_global) = 15 Then
            consumir() ' si es comilla se incrementa el index en consumir
        Else
            'capturar_error("se esperaba doble comilla")
        End If
        Try
            While numero_tokens(index_global) <> 15
                texto_salida = texto_salida + " " + lexemas(index_global)

                If numero_tokens(index_global) = 10 Then
                    texto_salida = lexemas(index_global)
                End If
                numero_tokens(index_global) = 20
                consumir() '  se incrementa el index en consumir
            End While

        Catch ex As Exception

        End Try
        If numero_tokens(index_global) = 15 Then
            consumir() ' si es comilla se incrementa el index en consumir
        Else
            'capturar_error("se esperaba doble comilla")
        End If
        If lexemas(index_global) = "," Then
            consumir() '  se incrementa el index en consumir
        Else
            'capturar_error("se esperaba coma ,")
        End If
        If numero_tokens(index_global) = 9 Then
            texto_salida = " " + texto_salida + " " + CType(dictionary(lexemas(index_global)), String)
            consumir() '  se incrementa el index en consumir
        Else
            'capturar_error("se esperaba un identificador")
        End If
        If lexemas(index_global) = ")" Then
            consumir() '  se incrementa el index en consumir
        Else
            ' capturar_error("se esperaba cerrar parentesis )")

        End If
        If lexemas(index_global) = ";" Then
            consumir() '  se incrementa el index en consumir
        Else
            'capturar_error("se esperaba punto y coma ;")

        End If
        lista_consola.Text = lista_consola.Text + ChrW(10) + " 
" + texto_salida 'agrega los datos a la consola
    End Function
    'comentario una linea
    Private Function comentario_linea()
        Dim comentario As String
        If (lexemas(index_global) = "/") Then
            consumir() '  se incrementa el index en consumir
        Else
            'capturar_error("se esperaba diagonal /")

        End If
        If (lexemas(index_global) = "/") Then
            consumir() '  se incrementa el index en consumir
        Else
            'capturar_error("se esperaba diagonal /")
        End If
        Try
            Dim fila_sig As Integer = filas(index_global) + 1
            While filas(index_global) <> fila_sig
                comentario = comentario + " " + lexemas(index_global)
                numero_tokens(index_global) = 21
                consumir() '  se incrementa el index en consumir
            End While


        Catch ex As Exception

        End Try
        comentarios.Add(comentario)

    End Function
    'comentario multi linea
    Private Function comentario_multilinea()
        Dim comentario As String
        If lexemas(index_global) = "/" Then
            consumir() '  se incrementa el index en consumir
        End If
        If lexemas(index_global) = "*" Then
            consumir() '  se incrementa el index en consumir
        End If
        While lexemas(index_global) <> "*"
            comentario = comentario + " " + lexemas(index_global)
            numero_tokens(index_global) = 21
            consumir() '  se incrementa el index en consumir
        End While
        If lexemas(index_global) = "*" Then
            consumir() '  se incrementa el index en consumir
        End If
        If lexemas(index_global) = "/" Then
            consumir() '  se incrementa el index en consumir
        End If
        comentarios.Add(comentario)
    End Function

    'obtener la lista de datos a operar
    Private Function obtener_lista() As ArrayList
        Dim lista As New ArrayList
        While lexemas(index_global) <> ")"
            If numero_tokens(index_global) = 9 Then ' si es un id
                lista.Add(dictionary(lexemas(index_global)))
                consumir() '  se incrementa el index en consumir

            ElseIf numero_tokens(index_global) = 10 Then 'si es enntero
                lista.Add(lexemas(index_global))
                consumir() '  se incrementa el index en consumir
            ElseIf numero_tokens(index_global) = 19 Then ' si es decimal
                lista.Add(lexemas(index_global))
                consumir() '  se incrementa el index en consumir
            End If
            If lexemas(index_global) = "," Then
                consumir() '  se incrementa el index en consumir
            Else
                '   capturar_error("se esperaba coma ,")
            End If
        End While
        Return lista

    End Function

    Private Sub consumir()
        index_global += 1 '  se incrementa el index en consumir
    End Sub
    'valida si es entero, decimal o id
    Private Function validar_numero_variable() As Integer
        Dim num As Double
        If numero_tokens(index_global) = 9 Then ' si es id
            num = dictionary(lexemas(index_global))
            consumir() '  se incrementa el index en consumir
        ElseIf numero_tokens(index_global) = 10 Then 'si es entero
            num = lexemas(index_global)
            consumir() '  se incrementa el index en consumir
        ElseIf numero_tokens(index_global) = 19 Then 'si es decimal
            num = lexemas(index_global)
            consumir() '  se incrementa el index en consumir

        End If
        Return num
    End Function
    'captura el error 
    Private Function capturar_error(texto As String)
        Dim erro As String
        erro = "Error obtenido " + lexemas(index_global) + " en la fila " + CType(filas(index_global), String) +
            " y columna " + CType(columnas(index_global), String) + " " + texto
        errores.Add(erro) 'agrregar a la lista de errores 
        consumir() '  se incrementa el index en consumir
    End Function

    ' **************************** validacion de tokens  por codigo ascii*************************
    'valida si es solo mayuscula
    Private Function es_solo_mayuscula(num As Integer) As Boolean 'validar que solo vengan mayusculas
        If (num >= 65 And num <= 90) Then ' se validapor medio del numero de caracter en la tabla acsii
            Return True
        End If
    End Function
    'valida si es solo mun==minuscula
    Private Function es_solo_minuscula(num As Integer) As Boolean 'validar que solo vengan minusculas
        If (num >= 97 And num <= 122) Then ' se validapor medio del numero de caracter en la tabla acsii
            Return True
        End If
    End Function
    'valida si es solo numero
    Private Function es_solo_numero(num As Integer) As Boolean 'validar que solo vengan numeros
        If (num >= 48 And num <= 57) Then ' se validapor medio del numero de caracter en la tabla acsii
            Return True
        End If
    End Function
    'valida si es parentesis inciio
    Private Function es_parentesis_inicio(num As Integer) As Boolean
        If num = 40 Then
            Return True
        End If
    End Function
    'valida si es parentesis fin
    Private Function es_parentesis_fin(num As Integer) As Boolean
        If num = 41 Then
            Return True
        End If
    End Function
    'valida si es punto y coma
    Private Function es_punto_coma(num As Integer) As Boolean
        If num = 59 Then
            Return True
        End If
    End Function
    'valida si es coma
    Private Function es_coma(num As Integer) As Boolean
        If num = 44 Then
            Return True
        End If
    End Function
    'valida si es doble comilla
    Private Function es_doble_comilla(num As Integer) As Boolean
        If num = 34 Then
            Return True
        End If
    End Function
    'valida si es espacios
    Private Function es_espacios(num As Integer) As Boolean
        If (num = 32) Or (num = 13) Or (num = 9) Then
            Return True
        End If
    End Function
    'valida si es punto
    Private Function es_punto(num As Integer) As Boolean
        If num = 46 Then
            Return True
        End If
    End Function
    'valida si es palabra reserfvada
    Private Function es_palabra_reservada(lexema As String) As Integer
        Dim cambio = 0 ' sirve para ver si es un identificador
        For i = 0 To 7 'recorre el arreglode palabras reservadas
            If palabras_resevadas(i) = lexema Then
                Return i + 1 'retorna el no_token
                cambio = 1
            End If
        Next
        If cambio = 0 Then
            Return 9
        End If
    End Function
    'valida si es diagonal
    Private Function es_diagonal(num As Integer) As Boolean
        If num = 47 Then
            Return True
        End If
    End Function
    'vaida si es asterisco
    Private Function es_asterisco(num As Integer) As Boolean
        If num = 42 Then
            Return True
        End If
    End Function

    '*************manejo de vista********************************************
    'crear ventana nueva
    Private Function crear_ventana(text As String, ruta As String)
        Dim ventana As New TabPage
        ventana.Height = 25
        ventana.Width = 50
        'ventana.Name = "1"
        Dim reciente As Integer
        For i As Integer = 0 To editores.Length - 1
            If editores(i) Is Nothing Then ' validar si no hay ventana en el index i
                'atributos del cuadro de texto
                editor = New TextBox
                editor.Height = 380
                editor.Width = 860
                editor.AcceptsReturn = "True"
                editor.AcceptsTab = "True"
                editor.Modified = False
                editor.AllowDrop = "False"
                editor.Text = text
                editor.Multiline = True
                editor.Name = ruta 'agregar ruta donde se encuetra guardaod
                editores(i) = editor
                editor.ScrollBars = ScrollBars.Horizontal
                editor.ScrollBars = ScrollBars.Vertical



                reciente = i
                i = editores.Length
            End If

        Next
        ventana.Controls.Add(editor)
        ventana.Text = "nueva"
        tab_principal.TabPages.Add(ventana)



    End Function
    'guarddar textos
    Private Function guardar(id As Integer, ruta As String)
        Dim nombre As String
        Try
            If ruta = Nothing Then
                MsgBox("Guardando ventana" + CType((id + 1), String))
                Dim sfd As New SaveFileDialog
                sfd.Filter = "LFP Field | *.lfp"
                Dim resultado_sfd As String
                sfd.ShowDialog()
                resultado_sfd = sfd.FileName
                tab_principal.TabPages(id).Text = "guardado"
                tab_principal.Refresh()
                Dim SW As New IO.StreamWriter(resultado_sfd)
                SW.Write(editores(id).Text)
                SW.Flush()
                SW.Close()
                editores(id).Name = resultado_sfd


            Else
                MsgBox("Guardando ventana" + CType((id + 1), String))
                Dim SW As New IO.StreamWriter(ruta)
                SW.Write(editores(id).Text)
                SW.Flush()
                SW.Close()
                tab_principal.TabPages(id).Text = "guardado"
                tab_principal.Refresh()
            End If
        Catch ex As Exception

        End Try

    End Function
    'cerar ventana
    Private Function cerrar(id As Integer)
        Try
            tab_principal.TabPages.RemoveAt(id)

            editores(id) = Nothing
            Dim auxiliar(100) As TextBox
            Dim j As Integer
            For i As Integer = 0 To editores.Length - 1
                If editores(i) IsNot Nothing Then
                    auxiliar(j) = editores(i)
                    j += 1
                End If
                editores(i) = Nothing
            Next
            j = 0
            For i As Integer = 0 To auxiliar.Length - 1
                If auxiliar(i) IsNot Nothing Then
                    editores(i) = auxiliar(i)
                    j += 1
                End If
            Next
            tab_principal.Refresh()
        Catch ex As Exception
            MsgBox("ERROR AL TRATAR DE ELIMINAR")
        End Try
    End Function
    'botno nueva ventana
    Private Sub btn_nueva_Click(sender As Object, e As EventArgs) Handles btn_nueva.Click
        crear_ventana("VAR(var1, va2); ASIG(var1, 3); ASIG(var2, 10);
SUMA(var1, 10); MULT(3,4, 5,var1);
RESTA(var2, var1);", Nothing)

    End Sub
    'boton analizar
    Private Sub btn_analizar_Click(sender As Object, e As EventArgs) Handles btn_analizar.Click
        id_ventana = tab_principal.SelectedIndex
        ' Try
        'MsgBox(editores(id_ventana).Text)
        obtener_tokens(editores(id_ventana).Text)
        ' Catch ex As Exception
        'MsgBox("No hay o no esta seleccionando ventana")
        'End Try
    End Sub
    'boton cerrar ventana
    Private Sub btn_cerar_Click(sender As Object, e As EventArgs) Handles btn_cerar.Click
        id_ventana = tab_principal.SelectedIndex
        Try
            If editores(id_ventana).Modified Then
                guardar(id_ventana, editores(id_ventana).Name)
                cerrar(id_ventana)
            ElseIf editores(id_ventana).Name = Nothing Then
                guardar(id_ventana, editores(id_ventana).Name)
                cerrar(id_ventana)
            Else
                cerrar(id_ventana)
            End If
        Catch ex As Exception

        End Try


    End Sub
    'boton cerrar todas
    Private Sub btn_cerrar_todas_Click(sender As Object, e As EventArgs) Handles btn_cerrar_todas.Click
        Try
            MsgBox(tab_principal.TabCount)
            For i = 0 To tab_principal.TabCount - 1
                If editores(i).Modified Then
                    guardar(i, editores(i).Name)
                    cerrar(i)
                ElseIf editores(i).Name = Nothing Then
                    guardar(i, editores(i).Name)
                    cerrar(i)
                Else
                    cerrar(i)
                End If

            Next

        Catch ex As Exception
            MsgBox("Error")
        End Try

    End Sub
    'boton abrir archiov
    Private Sub btn_abrir_Click(sender As Object, e As EventArgs) Handles btn_abrir.Click
        id_ventana = tab_principal.SelectedIndex
        Dim texto_ventana As String
        Dim texto_archivo As String
        Dim ruta_archivo As String
        Dim resultado As MsgBoxResult

        OpenFileDialog1.Filter = "LFP Field | *.lfp"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            ruta_archivo = OpenFileDialog1.FileName
            texto_archivo = My.Computer.FileSystem.ReadAllText(ruta_archivo)
        End If
        Try
            texto_ventana = editores(id_ventana).Text

        Catch ex As Exception
            texto_ventana = ".$." ' si no esta creada
        End Try
        If texto_ventana = Nothing Then
            resultado = MsgBox("DESEA CARGAR EL ARCHIVO?", vbYesNo, "CONFIRMACION")
            If resultado = vbYes Then
                editores(id_ventana).Text = texto_archivo
                editores(id_ventana).Name = ruta_archivo
                editores(id_ventana).Modified = False
            Else
                MsgBox("No se cargo el archivo")
            End If
        ElseIf texto_ventana = ".$." Then
            resultado = MsgBox("DESEA CREAR UNA NUEVA VENTANA?", vbYesNo, "CONFIRMACION")
            If resultado = vbYes Then
                crear_ventana(texto_archivo, ruta_archivo)
            Else
                MsgBox("No se cargo el archivo")
            End If
        Else
            resultado = MsgBox("DESEA CREAR UNA NUEVA VENTANA?", vbYesNoCancel, "CONFIRMACION")
            If resultado = vbYes Then
                crear_ventana(texto_archivo, ruta_archivo)
            ElseIf resultado = vbNo Then
                MsgBox("Sobreescribiendo texto en ventana actual")
                editores(id_ventana).Text = texto_archivo
                editores(id_ventana).Name = ruta_archivo
                editores(id_ventana).Modified = False

            Else
                MsgBox("No se cargo el archivo")

            End If
        End If

    End Sub
    'boton guardar archivo
    Private Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        id_ventana = tab_principal.SelectedIndex
        Try
            guardar(id_ventana, editores(id_ventana).Name)

        Catch ex As Exception

        End Try


    End Sub
    'boton guardar como
    Private Sub btn_guardar_como_Click(sender As Object, e As EventArgs) Handles btn_guardar_como.Click
        id_ventana = tab_principal.SelectedIndex
        Try
            guardar(id_ventana, Nothing)

        Catch ex As Exception

        End Try
    End Sub
    'boton guardar todo
    Private Sub btn_guardar_todo_Click(sender As Object, e As EventArgs) Handles btn_guardar_todo.Click
        Try
            For i = 0 To tab_principal.TabCount - 1
                guardar(i, editores(i).Name)
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub accion_teclado(sender As Object, e As EventArgs) Handles tab_principal.KeyPress
        id_ventana = tab_principal.SelectedIndex
        Try
            editores(id_ventana).Modified = True
            If tab_principal.TabPages(id_ventana).Text = "guardado" Or
                tab_principal.TabPages(id_ventana).Text = "nueva" Then

                tab_principal.TabPages(id_ventana).Text = "No Guardado*"
                tab_principal.Refresh()

            End If
        Catch ex As Exception

        End Try
    End Sub

    '******************** generar htmls
    Private Function html_resultados() As String
        Dim html As String

        html = "<html>
                    <body>
		                <h1>ERRORES ENCONTRADOS EN EL ANALIS</h1>
                        <table border=" + ChrW(34) + "1" + ChrW(34) +
                        " style=" + ChrW(34) + "margin: 0 auto;" + ChrW(34) + ">
			                <tr>
				                <th>Descripcion de errores encontrados</th>
				            </tr>
		"
        For i = 0 To errores.Count - 1
            html = html + "<tr>
				                <th>" + CType(errores(i), String) + "</th>
			                </tr>"
        Next
        html = html + "</table><br><br>
                        <h1>TABLA DE TOKENS ENCONTRADOS EN EL ANALISIS</h1>
                        <table border=" + ChrW(34) + "1" + ChrW(34) +
                        " style=" + ChrW(34) + "margin: 0 auto;" + ChrW(34) + ">
			                <tr>
				                <th>Numero de Token</th>
				                <th>Lexema</th>
                                <th>Fila</th>
                                <th>Columna</th>
                                <th>Atributo</th>
			                </tr>"
        For i = 0 To lexemas.Count - 1
            Dim atributo As String = ""
            If numero_tokens(i) = 9 Then
                Try
                    atributo = dictionary(lexemas(i))
                Catch ex As Exception

                End Try
            End If
            If numero_tokens(i) <> 20 And numero_tokens(i) <> 21 Then
                html = html + "<tr>
				                <th>" + CType(numero_tokens(i), String) + "</th>
				                <th>" + lexemas(i) + "</th>
                                <th>" + CType(filas(i), String) + "</th>
                                <th>" + CType(columnas(i), String) + "</th>
                                <th>" + atributo + "
			                </tr>"
            End If

        Next
        html = html + "</table><br><br><h1>COMENTARIOS ENCONTRADOS EN EL ANALIS</h1>
                        <table border=" + ChrW(34) + "1" + ChrW(34) +
                        " style=" + ChrW(34) + "margin: 0 auto;" + ChrW(34) + ">
			                <tr>
				                <th>Comentario encontrado</th>
				            </tr>
		"
        For i = 0 To comentarios.Count - 1
            html = html + "<tr>
				                <th>" + CType(comentarios(i), String) + "</th>
			                </tr>"
        Next
        html = html + "</table></body></html>"
        Return html
    End Function

    'geenrar archivo html con todos los datos obtenidos del analisis
    Private Sub generar_archivo(text As String)
        Try
            Dim sfd As New SaveFileDialog
            sfd.Filter = "HTML tokens Field | *.html"
            Dim resultado_sfd As String
            sfd.ShowDialog()
            resultado_sfd = sfd.FileName

            Dim SW As New IO.StreamWriter(resultado_sfd)
            SW.Write(text)
            SW.Flush()
            SW.Close()


        Catch ex As Exception

        End Try

    End Sub


End Class
