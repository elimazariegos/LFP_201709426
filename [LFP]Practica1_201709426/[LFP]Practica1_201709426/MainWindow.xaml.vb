Class MainWindow

    Dim id_ventana As Integer = 0
    Dim editor As TextBox
    Dim ventanas(100) As TextBox

    Dim token As String
    Dim siguiente As Integer

    Dim numero_tokens As New ArrayList
    Dim lexemas As New ArrayList
    Dim atributo As New ArrayList

    Dim filas As New ArrayList
    Dim columnas As New ArrayList
                                           ' 1        2        3       4       5         6       7        8
    Dim palabras_resevadas = New String() {"VAR", "ASIG", "OPERA", "SUMA", "RESTA", "MULT", "DIVI", "SALIDA"}



    Private Sub Btn_analizar_Click(sender As Object, e As RoutedEventArgs) Handles btn_analizar.Click
        id_ventana = tab_principal.SelectedIndex
        Try
            For i As Integer = 0 To ventanas.Length - 1
                If i = id_ventana Then
                    analizar(ventanas(i).Text)

                End If
            Next
        Catch ex As Exception
            MsgBox("No hay o no esta seleccionando ventana")
        End Try
    End Sub

    Private Sub analizar(texto As String)
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
                            filas.Add(i + 1)
                            columnas.Add(j + 1)
                            estado = 0

                        End If
                    Case 100
                        estado = -2
                End Select
                If estado = 0 Then
                    n_token = es_palabra_reservada(token) ' token palabra reservada
                    numero_tokens.Add(n_token)
                    lexemas.Add(token)
                    token = ""
                    '      estado = estado_transicion(siguiente)
                ElseIf estado = -2 Then
                    estado = 0
                End If

            Next
        Next

        For i = 0 To numero_tokens.Count - 1
            For index = 1 To 8
                If numero_tokens(i) = index Then 'token palabra reservada VAR
                    list_consola.Items.Add("PALABRA RESERVADA: " + lexemas(i))
                    MsgBox("PALABRA RESERVADA: " + lexemas(i))
                End If

            Next
            If numero_tokens(i) = 9 Then
                list_consola.Items.Add("IDENTIFICADOR: " + lexemas(i))
                MsgBox("IDENTIFICADOR: " + lexemas(i))
            End If
        Next
        lexemas.Clear()
        numero_tokens.Clear()

    End Sub


    Private Function estado_transicion(ByVal n As Integer) As Integer
        If es_solo_mayuscula(n) Or es_solo_minuscula(n) Then 'validar si viene una letra mayuscula mediante el codigo ascii
            Return 1 'retorna 1 para avanzar al estado 1 (estado de aceptacion de palabras reservadas)
        ElseIf es_solo_numero(n) Then
            Return 3
        ElseIf es_parentesis_inicio(n) Then
            Return 4
        ElseIf es_parentesis_fin(n) Then
            Return 5
        ElseIf es_punto_coma(n) Then
            Return 6
        ElseIf es_coma(n) Then
            Return 7

        ElseIf es_espacios(n) Then
            Return 100
        End If
    End Function


    Private Function es_solo_mayuscula(num As Integer) As Boolean 'validar que solo vengan mayusculas
        If (num >= 65 And num <= 90) Then ' se validapor medio del numero de caracter en la tabla acsii
            Return True
        End If
    End Function

    Private Function es_solo_minuscula(num As Integer) As Boolean 'validar que solo vengan minusculas
        If (num >= 97 And num <= 122) Then ' se validapor medio del numero de caracter en la tabla acsii
            Return True
        End If
    End Function
    Private Function es_solo_numero(num As Integer) As Boolean 'validar que solo vengan numeros
        If (num >= 48 And num <= 57) Then ' se validapor medio del numero de caracter en la tabla acsii
            Return True
        End If
    End Function
    Private Function es_parentesis_inicio(num As Integer) As Boolean
        If num = 40 Then
            Return True
        End If
    End Function
    Private Function es_parentesis_fin(num As Integer) As Boolean
        If num = 41 Then
            Return True
        End If
    End Function
    Private Function es_punto_coma(num As Integer) As Boolean
        If num = 59 Then
            Return True
        End If
    End Function
    Private Function es_coma(num As Integer) As Boolean
        If num = 44 Then
            Return True
        End If
    End Function

    Private Function es_espacios(num As Integer) As Boolean
        If (num = 32) Or (num = 13) Or (num = 9) Then
            Return True
        End If
    End Function

    Private Function es_palabra_reservada(lexema As String) As Integer
        Dim cambio = 0
        For i = 0 To 7
            If palabras_resevadas(i) = lexema Then
                MsgBox(lexema)
                Return i + 1
                cambio = 1
            End If
        Next
        If cambio = 0 Then
            Return 9
        End If
    End Function

    Private Function crear_ventana(text As String)
        Dim ventana As New TabItem
        ventana.Height = 25
        ventana.Width = 50
        'ventana.Name = "1"
        For i As Integer = 0 To ventanas.Length - 1
            If ventanas(i) Is Nothing Then
                ventana.Header = "ventana " + CStr(i)

                editor = New TextBox
                editor.Height = 380
                editor.Width = 860
                editor.AcceptsReturn = "True"
                editor.AcceptsTab = "True"
                editor.IsUndoEnabled = "True"
                editor.AllowDrop = "False"
                editor.Text = text
                editor.Name = "ventana" + CStr(i)

                ventanas(i) = editor
                i = ventanas.Length
            End If

        Next
        ventana.Content = editor
        tab_principal.Items.Add(ventana)
    End Function

    Private Sub Btn_nuevav_Click(sender As Object, e As RoutedEventArgs) Handles btn_nuevav.Click



    End Sub

    Private Sub Btn_cerrarv_Click(sender As Object, e As RoutedEventArgs) Handles btn_cerrarv.Click

        Try
            id_ventana = tab_principal.SelectedIndex
            tab_principal.Items.RemoveAt(id_ventana)

            ventanas(id_ventana) = Nothing
            MsgBox(ventanas(id_ventana))
            Dim auxiliar(100) As TextBox
            Dim j As Integer
            For i As Integer = 0 To ventanas.Length - 1
                If ventanas(i) IsNot Nothing Then
                    auxiliar(j) = ventanas(i)
                    j += 1
                End If
                ventanas(i) = Nothing
            Next
            j = 0
            For i As Integer = 0 To auxiliar.Length - 1
                If auxiliar(i) IsNot Nothing Then
                    ventanas(i) = auxiliar(i)
                    j += 1
                End If
            Next
            tab_principal.Items.Refresh()
        Catch ex As Exception
            MsgBox("ERROR AL TRATAR DE ELIMINAR")
        End Try

    End Sub

    Private Sub Btn_abrir_Click(sender As Object, e As RoutedEventArgs) Handles btn_abrir.Click


    End Sub
End Class
