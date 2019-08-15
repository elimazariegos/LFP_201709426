Public Class Traductor
    Dim lista_token As New List(Of Token)
    Dim texto_vb As String
    Dim index As Integer
    Dim i As Integer
    Public Function traducir(lista_token As List(Of Token)) As String
        Me.lista_token = lista_token
        While lista_token(index).getNumero <> 999
            Dim fila_sig = lista_token(index + 1).getFila

            If lista_token(index).getLexema = "public" Then
                incrementar()
                If lista_token(index).getLexema = "class" Then
                    incrementar()
                    If lista_token(index).getNumero = 17 Then
                        i = index
                        incrementar()
                        If lista_token(index).getLexema = "{" Then
                            texto_vb = texto_vb + " Module " + lista_token(i).getLexema
                            incrementar()
                        End If
                    End If
                ElseIf lista_token(index).getLexema = "static" Then
                    incrementar()
                    If lista_token(index).getLexema = "void" Then
                        incrementar()
                        If lista_token(index).getNumero = 17 Then
                            i = index
                            incrementar()
                            If lista_token(index).getLexema = "(" Then
                                incrementar()
                                If lista_token(index).getLexema = "String" Then
                                    incrementar()
                                    If lista_token(index).getLexema = "[" Then
                                        incrementar()
                                        If lista_token(index).getLexema = "]" Then
                                            incrementar()
                                            If lista_token(index).getLexema = "args" Then
                                                incrementar()
                                                If lista_token(index).getLexema = ")" Then
                                                    incrementar()
                                                    If lista_token(index).getLexema = "{" Then
                                                        incrementar()
                                                        texto_vb = texto_vb + "  Sub " + lista_token(i).getLexema +
                                                                                    " (args As String())"
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            If lista_token(index).getNumero > 6 And lista_token(index).getNumero < 12 Then
                Dim texto = asignacion()
                texto_vb = texto_vb + texto

            End If
            If lista_token(index).getLexema = "}" Then
                texto_vb = texto_vb + "         End Sub " + ChrW(10) +
                "End Module"

                incrementar()
                incrementar()
            End If
            Try
                If lista_token(index + 1).getFila = fila_sig Then
                    texto_vb = texto_vb + " " + ChrW(10) + "
                        "
                End If
            Catch ex As Exception

            End Try

        End While
        Return texto_vb
    End Function

    Public Function asignacion() As String
        Dim tipo As String
        Select Case lista_token(index).getNumero
            Case 7
                tipo = "Integer"
            Case 8
                tipo = "Double"
            Case 9
                tipo = "String"
            Case 10
                tipo = "Boolean"
            Case 11
                tipo = "Char"

        End Select
        Dim texto_asignar As String = "Dim"
        Dim asignado As Integer = 0
        incrementar()
        While lista_token(index).getLexema <> ";"
            If lista_token(index).getNumero = 17 Then
                i = index
            End If
            If lista_token(index).getLexema = "=" Then
                If asignado = 0 Then
                    texto_asignar = texto_asignar + " " + lista_token(i).getLexema + " As " + tipo + " = " + lista_token(i).getValor
                    asignado = 1
                Else
                    texto_asignar = texto_asignar + ", " + lista_token(i).getLexema + " = " + lista_token(i).getValor
                End If
            End If
            If lista_token(index).getLexema = "," Then
                If asignado = 0 And lista_token(index + 2).getLexema <> "=" Then
                    texto_asignar = texto_asignar + " " + lista_token(i).getLexema + " As " + tipo
                ElseIf lista_token(index + 2).getLexema <> "=" Then
                    texto_asignar = texto_asignar + ", " + lista_token(i).getLexema
                End If
            End If
            If lista_token(index + 1).getLexema = ";" And asignado = 0 Then
                texto_asignar = texto_asignar + " " + lista_token(i).getLexema + " As " + tipo
            End If
            incrementar()
        End While
        If lista_token(index).getLexema = ";" Then
            incrementar()
        End If
        Return texto_asignar
    End Function
    Public Sub incrementar()
        index += 1
    End Sub
End Class
