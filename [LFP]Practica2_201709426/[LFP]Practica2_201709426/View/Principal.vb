Public Class Principal
    Dim lista_token As New List(Of Token)

    Private Sub btn_analizar_Click(sender As Object, e As EventArgs) Handles btn_analizar.Click
        Dim obtener As New Obtener_Token()
        Dim analizar As New Analizar_Token()
        Dim traducir As New Traductor()
        lista_token = obtener.obtener_tokens(txt_java.Text)
        Dim lista_salida = analizar.analizador(lista_token, 0)
        For index = 0 To lista_salida.Count - 1
            txt_consola.Text = txt_consola.Text + "
                        " + ChrW(10) + lista_salida(index)
        Next
        'txt_consola.Text = traducir.traducir(lista_token)

    End Sub
End Class
