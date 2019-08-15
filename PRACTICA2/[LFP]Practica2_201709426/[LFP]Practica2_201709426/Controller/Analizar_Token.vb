Public Class Analizar_Token
    Dim index As Integer
    Dim lista_token As New List(Of Token)
    Dim errores As New List(Of Errror)
    Dim declarada As Boolean = False
    Dim m_principal As Boolean = False
    Dim hubo_error As Boolean = False

    Dim id = ""
    Dim tipo = ""
    Dim cambio = 0
    Dim texto = ""

    Dim lista_salida As New ArrayList
    Dim comentarios As New ArrayList

    Dim variables As New Dictionary(Of String, Object)
    Dim tipo_variables As New Dictionary(Of String, Object)

    Public Function lista_errores(lista_token As List(Of Token)) As List(Of Errror)
        analizador(lista_token, 0)
        Return errores
    End Function
    Public Function analizador(lista_token As List(Of Token), anexo As Integer) As ArrayList
        Me.lista_token = lista_token

        While lista_token(index).getNumero <> 999
            Try
                Dim tnt As Integer = 0
                If lista_token(index).getLexema = "public" Then
                    If lista_token(index + 1).getLexema = "class" And declarada = False Then
                        declarar_clase()
                    ElseIf lista_token(index + 1).getLexema = "static" And m_principal = False Then
                        metodo_principal()
                        Exit While
                    Else
                        capturar_error("se esperaba una palabra reservada")
                    End If
                End If
                If lista_token(index).getNumero = 17 Then
                    asignar()
                    tnt = 1
                ElseIf lista_token(index).getLexema = "System" Then
                    salida()
                    tnt = 1
                ElseIf lista_token(index).getLexema = "/" Then
                    If lista_token(index + 1).getLexema = "/" Then
                        comentario_linea()
                        tnt = 1
                    ElseIf lista_token(index + 1).getLexema = "*" Then
                        comentario_multilinea()
                        tnt = 1
                    End If
                ElseIf lista_token(index).getLexema = "int" Then
                    tipo = lista_token(index).getLexema
                    incrementar()
                    declarar_numero()
                    tnt = 1
                ElseIf lista_token(index).getLexema = "double" Then
                    tipo = lista_token(index).getLexema
                    incrementar()
                    declarar_numero()
                    tnt = 1
                ElseIf lista_token(index).getLexema = "String" Then
                    tipo = "String"
                    incrementar()
                    declarar_string()
                    tnt = 1
                ElseIf lista_token(index).getLexema = "char" Then
                    tipo = "char"
                    incrementar()
                    declarar_char()
                    tnt = 1
                ElseIf lista_token(index).getLexema = "boolean" Then
                    tipo = "boolean"
                    incrementar()
                    declarar_bool()
                    tnt = 1
                ElseIf lista_token(index).getLexema = "if" Then
                    sentencia_if()
                    tnt = 1
                ElseIf lista_token(index).getLexema = "for" Then
                    sentencia_for()
                    tnt = 1
                End If

                If tnt = 0 And lista_token(index).getNumero <> 999 Then
                    diferentes_errores()
                End If
                If (anexo = 1) Then
                    Exit While
                End If

            Catch ex As Exception
                'generar_archivo()
                Return lista_salida
            Exit While
            End Try
        End While
        If anexo <> 1 Then
            If declarada = True And m_principal = True And errores.Count = 0 Then
                'generar_archivo()
                lista_salida.Add("#.#.#")
                Return lista_salida
            End If

        End If
    End Function

    Public Sub sentencia_for()
        Dim inicio, fin As Integer
        Dim id_temporal As String

        If lista_token(index).getLexema = "for" Then
            incrementar()
        End If
        If lista_token(index).getLexema = "(" Then
            incrementar()
            If lista_token(index).getLexema = "int" Then
                tipo = lista_token(index).getLexema
                incrementar()
                id = lista_token(index).getLexema
                id_temporal = lista_token(index).getLexema
                declarar_numero()
            End If
        Else
            capturar_error("Se esperaba (")
        End If
        If lista_token(index).getNumero = 17 Then
            incrementar()
        Else
            capturar_error("se esperaba una variable")
        End If
        If lista_token(index).getLexema = "<" Then
            incrementar()
            If lista_token(index).getLexema = "=" Then
                incrementar()
            End If
        Else
            capturar_error("se esperaba <")
        End If
        If lista_token(index).getNumero = 18 Then
            fin = lista_token(index).getValor
            incrementar()
        ElseIf lista_token(index).getNumero = 17 Then
            fin = lista_token(index).getValor
            incrementar()
        Else
            capturar_error("debe ingresar un entero")
        End If
        If lista_token(index).getLexema = ";" Then
            incrementar()
        Else
            capturar_error("se esperaba ;")
        End If
        If lista_token(index).getNumero = 17 Then
            incrementar()
        Else
            capturar_error("se esperaba una variable")
        End If
        If lista_token(index).getLexema = "+" Then
            incrementar()
        Else
            capturar_error("se esperaba +")
        End If
        If lista_token(index).getLexema = "+" Then
            incrementar()
        Else
            capturar_error("se esperaba +")
        End If
        If lista_token(index).getLexema = ")" Then
            incrementar()
        Else
            capturar_error("se esperaba )")
        End If
        If lista_token(index).getLexema = "{" Then
            incrementar()
            Dim tem = index
            For i = inicio To fin
                variables(id_temporal) = i
                While lista_token(index).getLexema <> "}"
                    analizador(lista_token, 1)
                End While
                If (i <> fin) Then
                    index = tem
                End If
            Next
            id = ""
        Else
            capturar_error("se esperaba {")
        End If
        If lista_token(index).getLexema = "}" Then
            lista_token(index).setTipo("for")
            incrementar()
        Else
            capturar_error("se esperaba }")
        End If

    End Sub

    Public Sub sentencia_if()
        Dim firts As Double = Nothing, second = Nothing
        Dim opera As String = ""
        Dim aceptacion As Boolean = False
        If lista_token(index).getLexema = "if" Then
            incrementar()
        End If
        If lista_token(index).getLexema = "(" Then
            incrementar()
        Else
            capturar_error("se esperaba (")
            hubo_error = True
        End If

        If lista_token(index).getNumero = 17 Then

            If buscar_variable("int") Then
                firts = variables(lista_token(index).getLexema)
                incrementar()
            ElseIf buscar_variable("double") Then
                firts = variables(lista_token(index).getLexema)
                incrementar()
            End If
        ElseIf lista_token(index).getNumero = 18 Or lista_token(index).getNumero = 19 Then
            firts = lista_token(index).getValor
            incrementar()
        Else
            capturar_error("la variable no ha sido declarada")
            hubo_error = True
        End If
        If lista_token(index).getLexema = ">" Then
            incrementar()
            opera = ">"
        ElseIf lista_token(index).getLexema = "<" Then
            incrementar()
            opera = "<"
        ElseIf lista_token(index).getLexema = "=" Then
            incrementar()
            If lista_token(index).getLexema = "=" Then
                incrementar()
                opera = "=="
            Else
                capturar_error("se esperaba =")
                hubo_error = True
            End If
        ElseIf lista_token(index).getLexema = "!" Then
            incrementar()
            If lista_token(index).getLexema = "=" Then
                incrementar()
                opera = "!="
            Else
                capturar_error("se esperaba !")
                hubo_error = True
            End If
        Else
            capturar_error("se esperaba una sentencia")
            hubo_error = True
        End If
        If lista_token(index).getNumero = 17 Then
            If buscar_variable("int") Then
                second = variables(lista_token(index).getLexema)
                incrementar()
            ElseIf buscar_variable("double") Then
                second = variables(lista_token(index).getLexema)
                incrementar()
            End If
        ElseIf lista_token(index).getNumero = 18 Or lista_token(index).getNumero = 19 Then
            second = lista_token(index).getValor
            incrementar()
        Else
            capturar_error("la variable no ha sido declarada")
            hubo_error = True
        End If
        If firts <> Nothing And second <> Nothing And opera <> "" Then
            If opera = ">" Then
                If firts > second Then
                    aceptacion = True
                End If
            ElseIf opera = "<" Then
                If firts < second Then
                    aceptacion = True
                End If
            ElseIf opera = "==" Then
                If firts = second Then
                    aceptacion = True
                End If
            ElseIf opera = "!=" Then
                If firts <> second Then
                    aceptacion = True
                End If
            End If
        End If
        If lista_token(index).getLexema = ")" Then
            incrementar()
        Else
            capturar_error("se esperaba )")
            hubo_error = True
        End If

        If lista_token(index).getLexema = "{" Then
            incrementar()
            If aceptacion = True Then
                While lista_token(index).getLexema <> "}"
                    analizador(lista_token, 1)
                End While
            Else
                consumir("}")
            End If

        Else
            capturar_error("se esperaba {")
            hubo_error = True
        End If
        If lista_token(index).getLexema = "}" Then
            incrementar()
        Else
            capturar_error("se esperaba }")
        End If
        If lista_token(index).getLexema = "else" Then
            incrementar()
            If lista_token(index).getLexema = "{" Then
                incrementar()
                If aceptacion = False Then
                    While lista_token(index).getLexema <> "}"
                        analizador(lista_token, 1)
                    End While
                Else
                    consumir("}")
                End If
            Else
                capturar_error("se esperaba {")
                hubo_error = True
            End If
            If lista_token(index).getLexema = "}" Then
                incrementar()
            Else
                capturar_error("se esperaba }")
                hubo_error = True
            End If

        End If


    End Sub

    Public Sub comentario_linea()
        Dim comentario As String
        If lista_token(index).getLexema = "/" Then
            incrementar()
        End If
        If lista_token(index).getLexema = "/" Then
            incrementar()
        Else
            capturar_error("Se esperaba  /")
        End If
        Try
            Dim fila_sig = lista_token(index).getFila
            While lista_token(index).getFila = fila_sig
                comentario = comentario + " " + lista_token(index).getLexema
                lista_token(index).setNumero(1000)
                incrementar()
            End While
        Catch ex As Exception

        End Try
        comentarios.Add(comentario)
    End Sub

    Public Sub comentario_multilinea()
        Dim comentario As String
        If lista_token(index).getLexema = "/" Then
            incrementar() '  se incrementa el index en consumir
        End If
        If lista_token(index).getLexema = "*" Then
            incrementar() '  se incrementa el index en consumir
        Else
            capturar_error("no se encontro el *")
        End If
        While lista_token(index).getLexema <> "*"
            comentario = comentario + " " + lista_token(index).getLexema
            lista_token(index).setNumero(1000)
            incrementar() '  se incrementa el index en consumir
            If lista_token(index).getLexema = "#" Then
                Exit While
                capturar_error("no se encontro la finalizacion del comentario")
            End If
        End While
        If lista_token(index).getLexema = "*" Then
            incrementar() '  se incrementa el index en consumir
        Else
            capturar_error("no se encontro *")
        End If
        If lista_token(index).getLexema = "/" Then
            incrementar() '  se incrementa el index en consumir
        Else
            capturar_error("no se encontro /")
        End If
        comentarios.Add(comentario)
    End Sub

    Public Sub salida()
        Dim texto As String
        If lista_token(index).getLexema = "System" Then
            incrementar()
        End If
        If lista_token(index).getLexema = "." Then
            incrementar()
        Else
            capturar_error("se esperaba punto")
        End If
        If lista_token(index).getLexema = "out" Then
            incrementar()
        Else
            capturar_error("se esperaba out")
        End If
        If lista_token(index).getLexema = "." Then
            incrementar()
        Else
            capturar_error("se esperaba punto")
        End If
        If lista_token(index).getLexema = "println" Then
            incrementar()
        Else
            capturar_error("se esperaba println")
        End If
        If lista_token(index).getLexema = "(" Then
            incrementar()
        Else
            capturar_error("se esperaba (")
        End If
        While lista_token(index).getLexema <> ")"
            If lista_token(index).getNumero = 17 Then
                Try
                    If variables(lista_token(index).getLexema) IsNot Nothing Then
                        texto = texto + " " + CType(variables(lista_token(index).getLexema), String)
                        incrementar()
                    Else
                        capturar_error("la variable no ha sido declarada")
                    End If
                Catch ex As Exception
                    capturar_error("la variable no ha sido declarada")
                End Try

            ElseIf lista_token(index).getNumero = 34 Then
                incrementar()
                While lista_token(index).getNumero <> 34
                    texto = texto + " " + lista_token(index).getLexema
                    incrementar()
                End While
                If lista_token(index).getNumero = 34 Then
                    incrementar()
                End If
            ElseIf lista_token(index).getLexema = "+" Then
                incrementar()
            Else
                capturar_error("se esperaba comillas o una variable para impimir")
            End If
        End While
        If lista_token(index).getLexema = ")" Then
            incrementar()
        End If
        If lista_token(index).getLexema = ";" Then
            incrementar()
        End If
        If declarada = True And m_principal = True Then
            If hubo_error <> True Then
                lista_salida.Add(texto)
            End If
        End If
    End Sub

    Public Function declarar_clase()
        If lista_token(index).getLexema = "public" Then
            incrementar()
            declarada = True
        End If
        If lista_token(index).getLexema = "class" Then
            incrementar()
        Else
            capturar_error("se esperaba la palabra reservada class")
            declarada = False

        End If
        If lista_token(index).getNumero = 17 And lista_token(index).getTipo IsNot Nothing Then
            incrementar()
        Else
            capturar_error("Debe escribir un nombre correcto para la clase")
            declarada = False
        End If
        If lista_token(index).getLexema = "{" Then
            incrementar()
        Else
            capturar_error("se esperaba {")
            declarada = False
        End If
        While lista_token(index).getLexema <> "}"
            analizador(lista_token, 1)
            If lista_token(index).getNumero = 999 Then
                Exit While
            End If
        End While
        If lista_token(index).getLexema = "}" Then
            incrementar()
        Else
            capturar_error("debe cerrar llave")
        End If
    End Function

    Public Function metodo_principal()
        If lista_token(index).getLexema = "public" Then
            incrementar()
            m_principal = True
        End If
        If lista_token(index).getLexema = "static" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba la palabra static")
        End If
        If lista_token(index).getLexema = "void" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba la palabra void")
        End If
        If lista_token(index).getNumero = 17 Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba el nombre de la clase")
        End If
        If lista_token(index).getLexema = "(" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba  (")
        End If
        If lista_token(index).getLexema = "String" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba la palabra String")
        End If
        If lista_token(index).getLexema = "[" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba [")
        End If
        If lista_token(index).getLexema = "]" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba ]")
        End If
        If lista_token(index).getLexema = "args" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba la palabra args")
        End If
        If lista_token(index).getLexema = ")" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba )")
        End If
        If lista_token(index).getLexema = "{" Then
            incrementar()
        Else
            m_principal = False
            capturar_error("se esperaba {")
        End If
        While lista_token(index).getLexema <> "}"
            analizador(lista_token, 1)
        End While
        If lista_token(index).getLexema = "}" Then
            incrementar()
        Else
            capturar_error("se esperaba cerrar Llave")
        End If

    End Function

    Public Sub asignar()

        Try
            Select Case tipo_variables(lista_token(index).getLexema)
                Case "int"
                    tipo = "int"
                    declarar_numero()
                Case "double"
                    tipo = "double"
                    declarar_numero()
                Case "String"
                    tipo = "String"
                    declarar_string()
                Case "char"
                    tipo = "char"
                    declarar_char()
                Case "boolean"
                    tipo = "boolean"
                    declarar_bool()
                Case Else
                    capturar_error("no se declaro la variable")
            End Select
        Catch ex As Exception
            capturar_error("error al validar token identificador")
        End Try

    End Sub



    Public Sub declarar_numero()
        Dim temporal As String
        While lista_token(index).getLexema <> ";"
            If lista_token(index).getLexema = "," And cambio = 1 Then
                incrementar()
            End If
            If lista_token(index).getNumero = 17 Then
                cambio = 1
                If lista_token(index - 1).getLexema = "int" Or lista_token(index - 1).getLexema = "double" Then
                    lista_token(index).setTipo(tipo)
                    id = lista_token(index).getLexema
                    temporal = id
                    variables.Add(lista_token(index).getLexema, Nothing)
                    tipo_variables.Add(lista_token(index).getLexema, tipo)
                    incrementar()
                ElseIf lista_token(index - 1).getLexema = "," Then
                    lista_token(index).setTipo(tipo)
                    id = lista_token(index).getLexema
                    temporal = id
                    variables.Add(lista_token(index).getLexema, Nothing)
                    tipo_variables.Add(lista_token(index).getLexema, tipo)
                    incrementar()
                ElseIf buscar_por_nombre() = True Then
                    id = lista_token(index).getLexema
                    temporal = id
                    incrementar()
                Else
                    capturar_error("la variable no ha sido declarada")
                    Exit While
                End If
            Else

                capturar_error("Se esperaba un token identificador")
                Exit While
            End If
            If lista_token(index).getLexema = "," Then
                incrementar()
            ElseIf lista_token(index).getLexema = "=" Then
                incrementar()
                If lista_token(index).getNumero = 18 Or lista_token(index).getNumero = 19 Then
                    variables(id) = lista_token(index).getLexema
                ElseIf lista_token(index).getNumero = 17 And buscar_variable(tipo) = True And lista_token(index + 1).getLexema = ";" Then
                    variables(id) = variables(lista_token(index).getLexema)
                    incrementar()
                ElseIf lista_token(index).getNumero = 17 And buscar_variable(tipo) = False And lista_token(index + 1).getLexema = ";" Then
                    capturar_error("la variable no ha sido declarada")
                    Exit While
                End If
                While lista_token(index + 1).getLexema <> ";"
                    If lista_token(index + 1).getLexema = "+" Then
                        variables(id) = sumar()
                    ElseIf lista_token(index + 1).getLexema = "*" Then
                        variables(id) = multiplicar()
                    ElseIf lista_token(index + 1).getLexema = "-" Then
                        variables(id) = restar()
                    ElseIf lista_token(index + 1).getLexema = "/" Then
                        variables(id) = dividir()
                    ElseIf lista_token(index + 1).getLexema <> ";" Then
                        Exit While
                    End If
                End While
                If lista_token(index).getLexema <> ";" And lista_token(index + 1).getLexema = "," Then
                    incrementar()
                ElseIf lista_token(index + 1).getLexema = ";" Then
                    incrementar()
                End If
                id = ""
            ElseIf lista_token(index).getLexema <> ";" Then
                Exit While
            End If
        End While

        tipo = ""
        If lista_token(index).getLexema = ";" And lista_token(index).getNumero <> 999 Then
            incrementar()
        Else
            capturar_error("se esperaba ;")
        End If
        cambio = 0
    End Sub

    Public Sub declarar_string()

        While lista_token(index).getLexema <> ";"
            If lista_token(index).getLexema = "," And cambio = 1 Then
                incrementar()
            End If
            If lista_token(index).getNumero = 17 Then
                cambio = 1
                If lista_token(index - 1).getLexema = "String" Then
                    lista_token(index).setTipo(tipo)
                    id = lista_token(index).getLexema
                    variables.Add(lista_token(index).getLexema, Nothing)
                    tipo_variables.Add(lista_token(index).getLexema, tipo)
                    incrementar()
                ElseIf lista_token(index - 1).getLexema = "," Then
                    lista_token(index).setTipo(tipo)
                    id = lista_token(index).getLexema
                    variables.Add(lista_token(index).getLexema, Nothing)
                    tipo_variables.Add(lista_token(index).getLexema, tipo)
                    incrementar()
                ElseIf buscar_por_nombre() = True Then
                    id = lista_token(index).getLexema
                    incrementar()
                Else
                    capturar_error("la variable no ha sido declarada")
                    Exit While
                End If
            Else
                capturar_error("Se esperaba un token identificador")
                Exit While
            End If
            If lista_token(index).getLexema = "," Then
                incrementar()
            ElseIf lista_token(index).getLexema = "=" Then
                incrementar()
                If lista_token(index).getNumero = 34 Then
                    incrementar()
                    While lista_token(index).getNumero <> 34
                        texto = texto + " " + lista_token(index).getLexema
                        If lista_token(index + 1).getNumero = 999 Or lista_token(index + 1).getLexema = ";" Then
                            Exit While
                        End If
                        incrementar()
                    End While
                    If lista_token(index).getNumero = 34 Then
                        variables(id) = texto
                        incrementar()
                    Else
                        capturar_error("se esperaban comillas dobles")
                        Exit While
                    End If

                ElseIf lista_token(index).getNumero = 17 And buscar_variable(tipo) = True Then
                    variables(id) = variables(lista_token(index).getLexema)
                    incrementar()
                ElseIf lista_token(index).getNumero = 17 And buscar_variable(tipo) = False Then
                    capturar_error("la variable no ha sido declarada")
                    Exit While
                Else
                    capturar_error("se esperaban comillas dobles ")
                    Exit While
                End If
                id = ""
                tipo = ""
                texto = ""
            ElseIf lista_token(index).getLexema <> ";" Then
                capturar_error("se esperaba ;")
                Exit While
            End If
        End While
        If lista_token(index).getLexema = ";" Then
            incrementar()
        Else
            capturar_error("se esperaba ;")
        End If
        cambio = 0
    End Sub

    Public Sub declarar_char()

        While lista_token(index).getLexema <> ";"
            If lista_token(index).getLexema = "," And cambio = 1 Then
                incrementar()
            End If
            If lista_token(index).getNumero = 17 Then
                cambio = 1
                If lista_token(index - 1).getLexema = "char" Then
                    lista_token(index).setTipo(tipo)
                    id = lista_token(index).getLexema
                    variables.Add(lista_token(index).getLexema, Nothing)
                    tipo_variables.Add(lista_token(index).getLexema, tipo)
                    incrementar()
                ElseIf lista_token(index - 1).getLexema = "," Then
                    lista_token(index).setTipo(tipo)
                    id = lista_token(index).getLexema
                    variables.Add(lista_token(index).getLexema, Nothing)
                    tipo_variables.Add(lista_token(index).getLexema, tipo)
                    incrementar()
                ElseIf buscar_por_nombre() = True Then
                    id = lista_token(index).getLexema
                    incrementar()
                Else
                    capturar_error("la variable no ha sido declarada")
                    Exit While
                End If

            Else
                capturar_error("Se esperaba un token identificador")
                Exit While
            End If
            If lista_token(index).getLexema = "," Then
                incrementar()
            ElseIf lista_token(index).getLexema = "=" Then
                incrementar()
                If lista_token(index).getNumero = 39 Then
                    incrementar()
                    If lista_token(index).getLexema.ToString.Length = 1 Then
                        texto = lista_token(index).getLexema
                        incrementar()
                    Else
                        capturar_error("debe ingresar solo un caracter")
                        Exit While
                    End If
                    If lista_token(index).getNumero = 39 Then
                        variables(id) = texto
                        incrementar()
                    Else
                        capturar_error("se esperaban comillas dobles")
                        Exit While
                    End If
                ElseIf lista_token(index).getNumero = 17 And buscar_variable(tipo) = True Then
                    variables(id) = variables(lista_token(index).getLexema)
                    incrementar()
                ElseIf lista_token(index).getNumero = 17 And buscar_variable(tipo) = False Then
                    capturar_error("la variable no ha sido declarada")
                    Exit While
                Else
                    capturar_error("se esperaban comilla simple ")
                    Exit While
                End If

                id = ""
                tipo = ""
                texto = ""
            ElseIf lista_token(index).getLexema <> ";" Then
                capturar_error("se esperaba ;")
                Exit While
            End If
        End While
        If lista_token(index).getLexema = ";" Then
            incrementar()
        Else
            capturar_error("se esperaba ;")
        End If
        cambio = 0
    End Sub

    Public Sub declarar_bool()
        While lista_token(index).getLexema <> ";"
            If lista_token(index).getLexema = "," And cambio = 1 Then
                incrementar()
            End If
            If lista_token(index).getNumero = 17 Then
                cambio = 1
                If lista_token(index - 1).getLexema = "boolean" Then
                    lista_token(index).setTipo(tipo)
                    id = lista_token(index).getLexema
                    variables.Add(lista_token(index).getLexema, Nothing)
                    tipo_variables.Add(lista_token(index).getLexema, tipo)
                    incrementar()
                ElseIf lista_token(index - 1).getLexema = "," Then
                    lista_token(index).setTipo(tipo)
                    id = lista_token(index).getLexema
                    variables.Add(lista_token(index).getLexema, Nothing)
                    tipo_variables.Add(lista_token(index).getLexema, tipo)
                    incrementar()
                ElseIf buscar_por_nombre() = True Then
                    id = lista_token(index).getLexema
                    incrementar()
                Else
                    capturar_error("la variable no ha sido declarada")
                    Exit While
                End If

            Else
                capturar_error("Se esperaba un token identificador")
                Exit While
            End If
            If lista_token(index).getLexema = "," Then
                incrementar()
            ElseIf lista_token(index).getLexema = "=" Then
                incrementar()
                If lista_token(index).getLexema = "true" Or lista_token(index).getLexema = "false" Then
                    variables(id) = lista_token(index).getLexema
                ElseIf lista_token(index).getNumero = 17 And buscar_variable(tipo) = True Then
                    variables(id) = variables(lista_token(index).getLexema)
                    incrementar()
                ElseIf lista_token(index).getNumero = 17 And buscar_variable(tipo) = False Then
                    capturar_error("la variable no ha sido declarada")
                    Exit While

                Else
                    capturar_error("se esperaban un valor booleano")
                    Exit While
                End If

                id = ""
                tipo = ""
                texto = ""
            ElseIf lista_token(index).getLexema <> ";" Then
                capturar_error("se esperaba ;")
                Exit While
            End If
        End While
        If lista_token(index).getLexema = ";" Then
            incrementar()
        Else
            capturar_error("se esperaba ;")
        End If
        cambio = 0
    End Sub

    Public Function sumar() As Double

        Dim total As Double = validar_inicial()
        If lista_token(index).getLexema = "+" Then
            incrementar()
        End If
        If lista_token(index).getNumero = 18 Or lista_token(index).getNumero = 19 Then
            total += lista_token(index).getValor
        ElseIf lista_token(index).getNumero = 17 Then
            If tipo_variables(lista_token(index).getLexema) = tipo Then
                total += variables(lista_token(index).getLexema)
            Else
                capturar_error("la variable no es del mismo tipo")
            End If
        ElseIf lista_token(index).getLexema = "(" Then
            total += agrupacion()
        End If
        Return total
    End Function

    Public Function restar() As Double
        Dim total As Double = validar_inicial()
        If lista_token(index).getLexema = "-" Then
            incrementar()
        End If
        If lista_token(index).getNumero = 18 Or lista_token(index).getNumero = 19 Then
            total -= lista_token(index).getValor
        ElseIf lista_token(index).getNumero = 17 Then
            If tipo_variables(lista_token(index).getLexema) = tipo Then
                total -= variables(lista_token(index).getLexema)
            Else
                capturar_error("la variable no es del mismo tipo")
            End If
        ElseIf lista_token(index).getLexema = "(" Then
            total -= agrupacion()
        End If
        Return total
    End Function

    Public Function multiplicar() As Double
        Dim total As Double = validar_inicial()
        If lista_token(index).getLexema = "*" Then
            incrementar()
        End If
        If lista_token(index).getNumero = 18 Or lista_token(index).getNumero = 19 Then
            total *= lista_token(index).getValor
        ElseIf lista_token(index).getNumero = 17 Then
            If tipo_variables(lista_token(index).getLexema) = tipo Then
                total *= variables(lista_token(index).getLexema)
            Else
                capturar_error("la variable no es del mismo tipo")
            End If
        ElseIf lista_token(index).getLexema = "(" Then
            total *= agrupacion()
        End If
        Return total
    End Function

    Public Function dividir() As Double
        Dim total As Double = validar_inicial()
        If lista_token(index).getLexema = "/" Then
            incrementar()
        End If
        If lista_token(index).getNumero = 18 Or lista_token(index).getNumero = 19 Then
            If lista_token(index).getValor <> 0 Then
                total /= lista_token(index).getValor
            Else
                capturar_error("no se puede dividir dentro de 0")
            End If
        ElseIf lista_token(index).getNumero = 17 Then
            If tipo_variables(lista_token(index).getLexema) = tipo Then
                If variables(lista_token(index).getLexema) <> 0 Then
                    total /= variables(lista_token(index).getLexema)
                Else
                    capturar_error("no se puede dividir dentro de 0")
                End If
            Else
                capturar_error("la variable no es del mismo tipo")
            End If
        ElseIf lista_token(index).getLexema = "(" Then
            If agrupacion() <> 0 Then
                total *= agrupacion()
            Else
                capturar_error("no se puede dividir dentro de 0")
            End If
        End If
        Return total
    End Function

    Public Function validar_inicial()
        Dim total
        If lista_token(index).getTipo = tipo Then
            total = lista_token(index).getValor
            incrementar()
        ElseIf lista_token(index).getNumero = 17 Then
            If tipo_variables(lista_token(index).getLexema) = tipo Then
                total = variables(lista_token(index).getLexema)
                incrementar()
            Else
                capturar_error("la variable no es del mismo tipo")
            End If
        End If
        Return total
    End Function

    Public Function agrupacion() As Double
        Dim valor_agrupado As Double

        If lista_token(index).getLexema = "(" Then
            incrementar()
            While lista_token(index + 1).getLexema <> ")"
                If lista_token(index).getTipo = "int" Or lista_token(index).getTipo = "double" Then
                    valor_agrupado = CType(lista_token(index).getValor, Double)
                End If
                If lista_token(index + 1).getLexema = "+" Then
                    valor_agrupado = sumar()
                ElseIf lista_token(index + 1).getLexema = "*" Then
                    valor_agrupado = multiplicar()
                ElseIf lista_token(index + 1).getLexema = "-" Then
                    valor_agrupado = restar()
                ElseIf lista_token(index + 1).getLexema = "/" Then
                    valor_agrupado = dividir()
                ElseIf lista_token(index + 1).getLexema <> ")" Then
                    capturar_error("se espera )")
                    Exit While
                End If
            End While

            incrementar()
            If lista_token(index).getLexema = ")" Then
                incrementar()
            End If
        End If
        Return valor_agrupado
    End Function


    Public Sub capturar_error(desc As String)
        errores.Add(New Errror(lista_token(index).getLexema, lista_token(index).getFila,
                               lista_token(index).getColumna, desc))
        incrementar()
    End Sub

    Public Sub incrementar()
        index += 1
    End Sub

    Public Sub diferentes_errores()
        If lista_token(index).getLexema = "=" Then
            capturar_error("esta escribiendo un = erroneamente")
        ElseIf lista_token(index).getNumero = 17 Then
            capturar_error("esta escribiendo una palabra mal")
        ElseIf lista_token(index).getNumero = 18 Then
            capturar_error("esta escribiendo un numero erroneamente")
        ElseIf lista_token(index).getLexema = ";" Then
            capturar_error("esta escribiendo un ; erroneamente")
        ElseIf lista_token(index).getLexema = "{" Then
            capturar_error("se espera una declaracion previa")
        ElseIf lista_token(index).getLexema = "public" Then
            capturar_error("fuera de lugar")
        ElseIf lista_token(index).getLexema = "class" Then
            capturar_error("debe llevar previo un public")
        ElseIf lista_token(index).getLexema = "{" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "static" Then
            capturar_error("se esperaba la palabra public previamente")
        ElseIf lista_token(index).getLexema = "void" Then
            capturar_error("se esperaba la palabra static previamente")
        ElseIf lista_token(index).getLexema = "main" Then
            capturar_error("se esperaba la palabra void previamente")
        ElseIf lista_token(index).getLexema = "(" Then
            capturar_error("se esperaba  una declaracion previa")
        ElseIf lista_token(index).getLexema = "String" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "[" Then
            capturar_error("se esperaba la palabra String previamente")
        ElseIf lista_token(index).getLexema = "]" Then
            capturar_error("se esperaba [ previamente")
        ElseIf lista_token(index).getLexema = "args" Then
            capturar_error("se esperaba ] previamente")
        ElseIf lista_token(index).getLexema = ")" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "System" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "out" Then
            capturar_error("se esperaba un System previamente")
        ElseIf lista_token(index).getLexema = "println" Then
            capturar_error("se esperaba un out previamente")
        ElseIf lista_token(index).getLexema = "." Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getNumero = 34 Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "/" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "*" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "-" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "+" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "if" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getLexema = "else" Then
            capturar_error("se esperaba una declaracion previa")
        ElseIf lista_token(index).getNumero = 1000 Then
            capturar_error("caracter no reconocido por el lenguaje")

        End If
    End Sub
    'geenrar archivo html con todos los datos obtenidos del analisis
    Private Sub generar_archivo()
        Dim texto As String
        Try
            For k = 0 To errores.Count - 1
                texto = texto + ChrW(10) + "
                " + errores(k).ToString
            Next
        Catch ex As Exception
            texto = "No se encontraron errores"
        End Try
        Try
            Dim sfd As New SaveFileDialog
            sfd.Filter = "HTML tokens Field | *.txt"
            Dim resultado_sfd As String
            sfd.ShowDialog()
            resultado_sfd = sfd.FileName

            Dim SW As New IO.StreamWriter(resultado_sfd)
            SW.Write(texto)
            SW.Flush()
            SW.Close()


        Catch ex As Exception

        End Try

    End Sub

    Public Function buscar_variable(tipo As String)
        For i = 0 To tipo_variables.Count - 1
            If variables.Keys(i) = lista_token(index).getLexema Then
                If tipo_variables(lista_token(index).getLexema) = tipo Then
                    Return True
                Else
                    Return False
                End If
            End If
        Next
    End Function

    Public Function buscar_por_nombre()
        Dim retorno = False
        For i = 0 To variables.Count - 1
            Try
                If variables.Keys(i) = lista_token(index).getLexema Then
                    retorno = True
                End If
            Catch ex As Exception
                Return False
            End Try

        Next
        Return retorno
    End Function
    Public Sub consumir(caracter As String)
        While lista_token(index).getLexema <> caracter
            incrementar()
        End While
    End Sub


End Class
