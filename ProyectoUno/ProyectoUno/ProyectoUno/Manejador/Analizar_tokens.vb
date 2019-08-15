Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class Analizar_tokens
    Dim lista As New List(Of Token)
    Dim errores As New List(Of Errores)
    Dim index As Integer

    Dim variables As New Dictionary(Of String, Object)
    Dim tipo_variables As New Dictionary(Of String, Object)
    Dim nombre, ruta
    Dim tamanio
    Dim pdfDoc As New Document()

    Dim arial As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)



    Public Function analizar(lista As List(Of Token), externo As Integer)
        Me.lista = lista
        While lista(index).No1 <> 666
            If lista(index).Lexema1 = "INSTRUCCIONES" Then
                instrucciones()
            ElseIf lista(index).Lexema1 = "VARIABLES" Then
                intruccion_variables()
            ElseIf lista(index).Lexema1 = "TEXTO" Then
                instruccion_texto()
            Else
            End If

            If externo = 1 Then
                Exit While
            End If
        End While

        pdfDoc.Close()
        Return variables
    End Function

    Public Sub instruccion_texto()
        If lista(index).Lexema1 = "TEXTO" Then
            consumir()
        End If
        If lista(index).Lexema1 = "{" Then
            consumir()
        Else

        End If
        While lista(index).Lexema1 <> "}"
            contenido_texto()
        End While
        If lista(index).Lexema1 = "}" Then
            consumir()
        Else

        End If
    End Sub

    Public Sub contenido_texto()
        If lista(index).Lexema1 = "Imagen" Then
            imagen()
        ElseIf lista(index).Lexema1 = "[" Then

            If lista(index + 1).Lexema1 = "+" Then
                pdfDoc.Add(New Paragraph(texto_negrita(), CreateFont(tamanio, iTextSharp.text.Font.BOLDITALIC)))
            ElseIf lista(index + 1).Lexema1 = "*" Then
                pdfDoc.Add(New Paragraph(texto_subrayado(), CreateFont(tamanio, iTextSharp.text.Font.STRIKETHRU)))
            Else

            End If
        ElseIf lista(index).Lexema1 = "/" Then
            instruccion_comentario()
        ElseIf lista(index).Lexema1 = "Var" Then
            pdfDoc.Add(New Paragraph(instruccion_var(), New Font(arial, tamanio)))
        ElseIf lista(index).Lexema1 = "Promedio" Then
            pdfDoc.Add(New Paragraph(instruccion_promedio(), New Font(arial, tamanio)))
        ElseIf lista(index).Lexema1 = "Suma" Then
            pdfDoc.Add(New Paragraph(instruccion_suma(), New Font(arial, tamanio)))
        ElseIf lista(index).Lexema1 = "Resta" Then
            pdfDoc.Add(New Paragraph(instruccion_resta(), New Font(arial, tamanio)))
        ElseIf lista(index).Lexema1 = "Multiplicar" Then
            pdfDoc.Add(New Paragraph(instruccion_multiplicar(), New Font(arial, tamanio)))
        ElseIf lista(index).Lexema1 = "Division" Then
            pdfDoc.Add(New Paragraph(instruccion_division(), New Font(arial, tamanio)))
        ElseIf lista(index).Lexema1 = "Asignar" Then
            instruccion_asignar()
        ElseIf lista(index).Lexema1 = "Linea_en_blanco" Then
            pdfDoc.Add(New Paragraph(" "))
            consumir()
            If lista(index).Lexema1 = ";" Then
                consumir()
            End If
        ElseIf lista(index).No1 = 34 Then
            pdfDoc.Add(New Paragraph(instruccion_Salida(), New Font(arial, tamanio)))
        End If
    End Sub


    Public Sub instruccion_asignar()
        Dim variable
        If lista(index).Lexema1 = "Asignar" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else

        End If
        If lista(index).No1 = 19 Then
            variable = lista(index).Lexema1
            consumir()
        Else

        End If
        If lista(index).Lexema1 = "," Then
            consumir()
        Else

        End If
        If lista(index).No1 = 19 Then
            Try
                variables(variable) = variables(lista(index).Lexema1)
                consumir()
            Catch ex As Exception

            End Try
        ElseIf lista(index).No1 = 20 Then
            variables(variable) = lista(index).Lexema1
            consumir()
        Else

        End If
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else

        End If
    End Sub


    Public Function instruccion_Salida()
        Dim texto
        If lista(index).No1 = 34 Then
            consumir()
        End If
        While lista(index).No1 <> 34
            texto = texto + " " + lista(index).Lexema1
            consumir()
        End While
        If lista(index).No1 = 34 Then
            consumir()
        Else

        End If
        Return texto
    End Function

    Public Function instruccion_division()
        Dim total_divi
        If lista(index).Lexema1 = "Division" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else

        End If
        While lista(index).Lexema1 <> ")"
            If lista(index).Lexema1 = "," And lista(index + 1).Lexema1 <> ")" Then
                consumir()
            End If
            Try

                If lista(index).No1 = 20 Then

                    total_divi /= CType(lista(index).Lexema1, Integer)
                ElseIf lista(index).No1 = 19 Then
                    total_divi *= variables(lista(index).Lexema1)
                End If
            Catch ex As Exception

            End Try
        End While
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else

        End If
        Return total_divi
    End Function

    Public Function instruccion_multiplicar()
        Dim total_multi
        If lista(index).Lexema1 = "Multiplicar" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else

        End If
        While lista(index).Lexema1 <> ")"
            If lista(index).Lexema1 = "," And lista(index + 1).Lexema1 <> ")" Then
                consumir()
            End If
            If lista(index).No1 = 20 Then
                total_multi *= CType(lista(index).Lexema1, Integer)
            ElseIf lista(index).No1 = 19 Then
                Try
                    total_multi *= variables(lista(index).Lexema1)
                Catch ex As Exception

                End Try
            End If
        End While
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else

        End If
        Return total_multi
    End Function

    Public Function instruccion_resta()
        Dim total_resta
        If lista(index).Lexema1 = "Resta" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else

        End If
        While lista(index).Lexema1 <> ")"
            If lista(index).Lexema1 = "," And lista(index + 1).Lexema1 <> ")" Then
                consumir()
            End If
            If lista(index).No1 = 20 Then
                total_resta -= CType(lista(index).Lexema1, Integer)
            ElseIf lista(index).No1 = 19 Then
                Try
                    total_resta -= variables(lista(index).Lexema1)
                Catch ex As Exception

                End Try
            End If
        End While
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else

        End If
        Return total_resta
    End Function

    Public Function instruccion_suma()
        Dim total_suma
        If lista(index).Lexema1 = "Suma" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else

        End If
        While lista(index).Lexema1 <> ")"
            If lista(index).Lexema1 = "," And lista(index + 1).Lexema1 <> ")" Then
                consumir()
            End If
            If lista(index).No1 = 20 Then
                total_suma += CType(lista(index).Lexema1, Integer)
            ElseIf lista(index).No1 = 19 Then
                Try
                    total_suma += variables(lista(index).Lexema1)
                Catch ex As Exception

                End Try
            End If
        End While
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else

        End If
        Return total_suma
    End Function


    Public Function instruccion_promedio()

        Dim total_numeros, total_suma
        If lista(index).Lexema1 = "Promedio" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        End If
        While lista(index).Lexema1 <> ")"
            If lista(index).Lexema1 = "," And lista(index + 1).Lexema1 <> ")" Then
                consumir()
            End If
            If lista(index).No1 = 20 Then
                total_numeros += 1
                total_suma += CType(lista(index).Lexema1, Integer)
            ElseIf lista(index).No1 = 19 Then
                Try
                    total_numeros += 1
                    total_suma += variables(lista(index).Lexema1)
                Catch ex As Exception

                End Try
            End If
        End While
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else

        End If
        total_suma = total_suma / total_numeros
        Return total_suma
    End Function

    Public Function instruccion_var()
        Dim valor_var
        If lista(index).Lexema1 = "Var" Then
            consumir()
        End If
        If lista(index).No1 = 19 Then
            Try
                valor_var = variables(lista(index).Lexema1)
            Catch ex As Exception

            End Try
            consumir()
        Else
            capturar_error("error sintactico, se esperaba ")
        End If
        If lista(index).Lexema1 = "]" Then
            consumir()
        Else

        End If
        Return valor_var
    End Function

    Public Sub instruccion_comentario()
        Dim comentario
        If lista(index).Lexema1 = "/" Then
            consumir()
        End If
        If lista(index).Lexema1 = "*" Then
            consumir()
        Else

        End If
        While lista(index).Lexema1 <> "*"
            comentario = comentario + " " + lista(index).Lexema1
        End While
        If lista(index).Lexema1 = "*" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba *")
        End If
        If lista(index).Lexema1 = "/" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba *")
        End If
    End Sub

    Public Function texto_negrita()
        Dim texto
        If lista(index).Lexema1 = "[" Then
            consumir()
        End If
        If lista(index).Lexema1 = "+" Then
            consumir()
        End If
        While lista(index).Lexema1 <> "+"
            texto = texto + " " + lista(index).Lexema1
            consumir()
        End While
        If lista(index).Lexema1 = "+" Then
            consumir()
        Else

        End If
        If lista(index).Lexema1 = "]" Then
            consumir()
            else
        End If
        If lista(index).Lexema1 = ";" Then
            consumir()
        Else

        End If
        Return texto
    End Function

    Public Function texto_subrayado()
        Dim texto
        If lista(index).Lexema1 = "[" Then
            consumir()
        End If
        If lista(index).Lexema1 = "*" Then
            consumir()
        End If
        While lista(index).Lexema1 <> "*"
            texto = texto + " " + lista(index).Lexema1
            consumir()
        End While
        If lista(index).Lexema1 = "*" Then
            consumir()
        Else

        End If
        If lista(index).Lexema1 = "]" Then
            consumir()
        End If
        If lista(index).Lexema1 = ";" Then
            consumir()
        Else

        End If

        Return texto
    End Function

    Public Function imagen()
        Dim direccion, x, y
        If lista(index).Lexema1 = "Imagen" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else

        End If
        If lista(index).No1 = 34 Then
            consumir()
        Else

        End If
        While lista(index).No1 <> 34
            direccion = direccion + lista(index).Lexema1
            consumir()
        End While
        If lista(index).No1 = 34 Then
            consumir()
        Else

        End If
        If lista(index).Lexema1 = "," Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba una coma")
        End If
        If lista(index).No1 = 20 Then
            x = lista(index).Lexema1
            consumir()
        Else
            capturar_error("errror sintactico, se esperaba un numero")
        End If
        If lista(index).Lexema1 = "," Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba una coma")
        End If
        If lista(index).No1 = 20 Then
            y = lista(index).Lexema1
            consumir()
        Else
            capturar_error("errror sintactico, se esperaba un numero")
        End If
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba un )")
        End If
        If lista(index).Lexema1 = ";" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba un ;")
        End If

        pdfDoc.Add(CreateImage(direccion, x, y))

    End Function

    'todo hacerca de las variables 

    Public Sub intruccion_variables()
        If lista(index).Lexema1 = "VARIABLES" Then
            consumir()
        End If
        If lista(index).Lexema1 = "{" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba {")
        End If
        While lista(index).Lexema1 <> "}"
            contenido_variables()
        End While
        If lista(index).Lexema1 = "}" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba }")
        End If
    End Sub

    Public Sub contenido_variables()
        If lista(index).No1 = 19 Then
            declarar_variable()
        End If
    End Sub

    Public Sub declarar_variable()
        Dim inicio As Integer = index
        Dim valor As Object = Nothing
        While lista(index).Lexema1 <> ":"
            If (lista(index).Lexema1 = "," Or lista(index).No1 = 19) And lista(index + 1).Lexema1 <> ":" Then
                If lista(index).No1 = 19 Then
                    variables.Add(lista(index).Lexema1, Nothing)
                End If
                consumir()
            ElseIf lista(index).No1 = 19 And lista(index + 1).Lexema1 = ":" Then
                variables.Add(lista(index).Lexema1, Nothing)
                consumir()
            Else

            End If

        End While
        If lista(index).Lexema1 = ":" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba dos puntos")
        End If
        If lista(index).Lexema1 = "entero" Then
            asignar_variable(inicio, valor)
            consumir()
            If lista(index).Lexema1 = "=" Then
                consumir()
                If lista(index).No1 = 20 Then
                    valor = lista(index).Lexema1
                    asignar_variable(inicio, valor)
                    consumir()
                Else

                End If
            End If

        ElseIf lista(index).Lexema1 = "cadena" Then
            consumir()
            If lista(index).Lexema1 = "=" Then
                consumir()
                If lista(index).No1 = 34 Then
                    consumir()
                Else

                End If
                While lista(index).No1 <> 34
                    valor = valor + " " + lista(index).Lexema1
                    consumir()
                End While

                valor = lista(index).Lexema1
                asignar_variable(inicio, valor)
                If lista(index).No1 <> 34 Then
                    consumir()
                Else
                End If
            End If

        Else
            capturar_error("se esperaban los tipos de datos")
        End If
        If lista(index).Lexema1 = ";" Then
            consumir()
        Else

        End If
    End Sub

    Public Sub asignar_variable(inicio As Integer, valor As Object)
        While lista(inicio).Lexema1 <> ":"
            If lista(inicio).No1 = 19 Then
                If valor = Nothing Then
                    tipo_variables.Add(lista(inicio).Lexema1, "entero")
                Else
                    variables(lista(inicio).Lexema1) = valor
                End If
            End If
            If lista(inicio).Lexema1 = "," And lista(inicio + 1).Lexema1 <> ":" Then
            End If
            inicio += 1
        End While

    End Sub


    'todo hacerca de las instrucciones 

    Public Sub instrucciones()
        If lista(index).Lexema1 = "INSTRUCCIONES" Then
            consumir()
        End If
        If lista(index).Lexema1 = "{" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba {")
        End If
        While lista(index).Lexema1 <> "}"
            contenido_instrucciones()
        End While
        If lista(index).Lexema1 = "}" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba }")
        End If
    End Sub

    Public Sub contenido_instrucciones()

        If lista(index).Lexema1 = "Nombre_archivo" Then
            nombre = nombre_archivo()
        ElseIf lista(index).Lexema1 = "Interlineado" Then
            interlineado()
        ElseIf lista(index).Lexema1 = "Tamanio_letra" Then
            tamanio_letra()
        ElseIf lista(index).Lexema1 = "Direccion_archivo" Then
            ruta = direccion_archivo()
        End If
        If nombre IsNot Nothing And ruta IsNot Nothing Then
            Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(ruta + nombre, FileMode.Create))
            pdfDoc.Open()
            nombre = Nothing
            ruta = Nothing
        End If
    End Sub

    Public Function tamanio_letra()
        If lista(index).Lexema1 = "Tamanio_letra" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba (")
        End If
        If lista(index).No1 = 20 Then
            tamanio = lista(index).Lexema1
            consumir()
        Else
            capturar_error("error sintactico, se esperaba un numero")
        End If
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba )")
        End If
        If lista(index).Lexema1 = ";" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba ;")
        End If
    End Function

    Public Sub interlineado()
        If lista(index).Lexema1 = "Interlineado" Then
            consumir()
        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba (")
        End If
        If lista(index).No1 = 18 Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba un numero")
        End If
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba )")
        End If
        If lista(index).Lexema1 = ";" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba ;")
        End If
    End Sub

    Public Function nombre_archivo()
        Dim parametro As String
        If lista(index).Lexema1 = "Nombre_archivo" Then
            consumir()

        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba (")
        End If
        If lista(index).No1 = 34 Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba comillas dobles")
        End If
        While lista(index).No1 <> 34
            parametro = parametro + lista(index).Lexema1
            consumir()
        End While
        If lista(index).No1 = 34 Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba comillas dobles")
        End If
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba )")
        End If
        If lista(index).Lexema1 = ";" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba ;")
        End If
        Return parametro
    End Function

    Public Function direccion_archivo()
        Dim parametro As String
        If lista(index).Lexema1 = "Direccion_archivo" Then
            consumir()

        End If
        If lista(index).Lexema1 = "(" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba (")
        End If
        If lista(index).No1 = 34 Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba comillas dobles")
        End If
        While lista(index).No1 <> 34
            parametro = parametro + lista(index).Lexema1
            consumir()
        End While
        If lista(index).No1 = 34 Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba comillas dobles")
        End If
        If lista(index).Lexema1 = ")" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba )")
        End If
        If lista(index).Lexema1 = ";" Then
            consumir()
        Else
            capturar_error("error sintactico, se esperaba ;")
        End If
        Return parametro
    End Function


    Public Function CreateFont(size As Integer, Optional style As Integer = iTextSharp.text.Font.BOLD) As iTextSharp.text.Font
        Dim FontColour = New BaseColor(193, 36, 67)  'Color code
        Return New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, size, style, FontColour)
    End Function

    Public Sub capturar_error(descripcion As String)
        errores.Add(New Errores(lista(index).No1, lista(index).Lexema1, descripcion, lista(index).Fila1, lista(index).ColumnaInicio1))
        consumir()
    End Sub
    Public Sub consumir()
        index += 1
    End Sub

    Public Function CreateImage(ruta As String, tamanioX As Integer, tamanioY As Integer) As iTextSharp.text.Image
        Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ruta)
        img.ScaleAbsolute(tamanioX, tamanioY)
        Return img
    End Function
End Class
