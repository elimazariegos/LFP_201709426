Public Class Traductor
    Dim lista_token As New List(Of Token)
    Dim texto_vb As String
    Dim index As Integer
    Dim i As Integer
    Public Function traducir(lista_token As List(Of Token), anexo As Integer) As String
        Me.lista_token = lista_token
        While lista_token(index).getNumero <> 999
            i = lista_token(index).getFila

            While lista_token(index).getFila = i
                If lista_token(index).getLexema = "public" Then
                    incrementar()
                    If lista_token(index).getLexema = "class" Then
                        incrementar()
                        traducir_class()
                    ElseIf lista_token(index).getLexema = "static" Then
                        incrementar()
                        traducir_main()
                    End If
                ElseIf lista_token(index).getLexema = "if" Then
                    incrementar()
                    traducir_if()
                ElseIf lista_token(index).getLexema = "for" Then
                    incrementar()
                    traducir_for()
                ElseIf lista_token(index).getLexema = "System" Then
                    incrementar()
                    traducir_salida()
                ElseIf lista_token(index).getNumero > 6 And lista_token(index).getNumero < 12 Then
                    traducir_variables()
                ElseIf lista_token(index).getLexema = "/" Then
                    incrementar()
                    If lista_token(index).getLexema = "/" Then
                        incrementar()
                        traducir_comentario_linea()
                    ElseIf lista_token(index).getLexema = "*" Then
                        incrementar()
                        traducir_comentario_multilinea()
                    End If
                End If
            End While
            If anexo = 1 Then
                Exit While
            End If
        End While
        If anexo <> 1 Then
            Return texto_vb
        End If
    End Function
    Public Sub incrementar()
        index += 1
    End Sub
    Public Sub consumir(caracter As Integer)
        While lista_token(index).getNumero <> caracter
            incrementar()
        End While
    End Sub
    Public Sub traducir_class()
        texto_vb = texto_vb + "Module " + lista_token(index).getLexema + "
"
        incrementar()
        incrementar()
        While lista_token(index).getLexema <> "}"
            traducir(lista_token, 1)
        End While
        texto_vb = texto_vb + "
End Module"
        incrementar()
    End Sub
    Public Sub traducir_main()
        incrementar()
        texto_vb = texto_vb + "     Sub " + lista_token(index).getLexema + " (args As String())
            "

        consumir(123)
        incrementar()
        While lista_token(index).getLexema <> "}"
            traducir(lista_token, 1)
        End While
        texto_vb = texto_vb + "
        End Sub"
        incrementar()
    End Sub

    Public Sub traducir_comentario_linea()
        texto_vb = texto_vb + "
'"
        Dim fila = lista_token(index).getFila
        While lista_token(index).getFila = fila
            texto_vb = texto_vb + " " + lista_token(index).getLexema
            incrementar()
        End While
        texto_vb = texto_vb + "
"
    End Sub
    Public Sub traducir_comentario_multilinea()
        While lista_token(index).getLexema <> "*"
            texto_vb = texto_vb + "'"
            Dim fila = lista_token(index).getFila
            While lista_token(index).getFila = fila
                If lista_token(index).getLexema = "*" Then
                    Exit While
                Else
                    texto_vb = texto_vb + " " + lista_token(index).getLexema
                End If
                incrementar()
            End While
            texto_vb = texto_vb + "
"
        End While
        incrementar()
        incrementar()
    End Sub
    Public Sub traducir_if()
        texto_vb = texto_vb + " if "
        If lista_token(index).getLexema = "(" Then
            incrementar()
            While lista_token(index).getLexema <> ")"
                texto_vb = texto_vb + " " + lista_token(index).getLexema
            End While
        End If
        If lista_token(index).getLexema = ")" Then
            incrementar()
        End If
        If lista_token(index).getLexema = "{" Then
            incrementar()
        End If
        While lista_token(index).getLexema <> "}"
            traducir(lista_token, 1)
        End While
        If lista_token(index).getLexema = "}" And lista_token(index + 1).getLexema <> "else" Then
            incrementar()
            texto_vb = texto_vb + " End If"
        Else
            If lista_token(index).getLexema = "else" Then
                texto_vb = texto_vb + " Else "
                incrementar()
            End If
            If lista_token(index).getLexema = "{" Then
                incrementar()
            End If
            While lista_token(index).getLexema <> "}"
                traducir(lista_token, 1)
            End While
            If lista_token(index).getLexema = "}" Then
                incrementar()
            End If
        End If
    End Sub
    Public Sub traducir_for()
        Dim inicio, fin, variable
        consumir(17)
        variable = lista_token(index).getLexema
        incrementar()
        incrementar()
        inicio = lista_token(index).getLexema
        incrementar()
        consumir(18)
        fin = lista_token(index).getLexema
        consumir(123)
        incrementar()
        texto_vb = "            " + texto_vb + " For " + variable + " As Integer = " + inicio + " To " + fin + "
"
        While lista_token(index).getLexema <> "}"
            traducir(lista_token, 1)
        End While
        incrementar()
        texto_vb = texto_vb + "
            Next"
    End Sub

    Public Sub traducir_variables()
        Dim variable
        Select Case lista_token(index).getNumero
            Case 7
                variable = "Integer"
            Case 8
                variable = "Double"
            Case 9
                variable = "String"
            Case 10
                variable = "Boolean"
            Case 11
                variable = "Char"
        End Select
        incrementar()
        texto_vb = texto_vb + " Dim " + lista_token(index).getLexema + " As " + variable + " "
        incrementar()
        If lista_token(index).getLexema = "," Then
            While lista_token(index).getLexema <> ";"
                texto_vb = texto_vb + " " + lista_token(index).getLexema
                incrementar()
            End While
        ElseIf lista_token(index).getLexema = "=" Then
            While lista_token(index).getLexema <> ";"
                texto_vb = texto_vb + " " + lista_token(index).getLexema + " "
                incrementar()
            End While
        End If
        texto_vb = texto_vb + "
"
        incrementar()
    End Sub

    Public Sub traducir_salida()
        Dim texto_salida = "                    
Console.WriteLine("
        consumir(40)
        incrementar()
        While lista_token(index).getLexema <> ")"
            texto_salida = texto_salida + " " + lista_token(index).getLexema
            incrementar()
        End While
        consumir(59)
        incrementar()
        texto_vb = texto_vb + texto_salida + ")"
    End Sub
End Class
