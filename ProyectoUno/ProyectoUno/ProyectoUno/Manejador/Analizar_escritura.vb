Public Class Analizar_escritura
    Dim txt_entrada As New TextBox
    Dim txt_salida As New TextBox
    Dim index
    Dim texto
    Public Function analizar_e(txt_entrada As TextBox)

        While index < txt_entrada.Text.ToString.Length - 1
            If txt_entrada.Text = "rojo" Then
                buscar(Color.red)
            End If
            index += 1
        End While
        index = 0
        Return txt_entrada
    End Function

    Public Sub buscar(color As Color)
        txt_entrada.ForeColor = color
        txt_entrada.Refresh()
    End Sub




End Class
