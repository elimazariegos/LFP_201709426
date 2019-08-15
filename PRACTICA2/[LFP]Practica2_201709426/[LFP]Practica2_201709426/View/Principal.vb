Public Class Principal
    Dim lista_token As New List(Of Token)

    Dim obtener As New Obtener_Token()
    Dim analizar As New Analizar_Token()
    Dim traducir As New Traductor()
    Private Sub GenerarTraduccionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerarTraduccionToolStripMenuItem.Click
        Try
            lista_token = obtener.obtener_tokens(txt_java.Text)
            Dim lista_salida = analizar.analizador(lista_token, 0)

            For index = 0 To lista_salida.Count - 1
                If lista_salida(index) <> "#.#.#" Then
                    txt_consola.Text = txt_consola.Text + "
                        " + ChrW(10) + lista_salida(index)
                Else
                    txt_vb.Text = traducir.traducir(lista_token, 0)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AbrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem.Click
        Dim texto_ventana As String
        Dim texto_archivo As String
        Dim ruta_archivo As String
        Dim resultado As MsgBoxResult


        OpenFileDialog1.Filter = "JAVA Field | *.java"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            ruta_archivo = OpenFileDialog1.FileName
            texto_archivo = My.Computer.FileSystem.ReadAllText(ruta_archivo)
            txt_java.Text = texto_archivo
            txt_java.Name = ruta_archivo
        End If

    End Sub

    Private Sub GuardarComoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarComoToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "JAVA Field | *.java"
        Dim resultado_sfd As String
        sfd.ShowDialog()
        resultado_sfd = sfd.FileName

        Dim SW As New IO.StreamWriter(resultado_sfd)
        SW.Write(txt_java.Text)
        SW.Flush()
        SW.Close()
        txt_java.Name = resultado_sfd

    End Sub

    Private Sub GenerarReportesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerarReportesToolStripMenuItem.Click
        Dim ge As New Generar_Reporte()
        ge.generar(lista_token, analizar.lista_errores(lista_token))
    End Sub

End Class
