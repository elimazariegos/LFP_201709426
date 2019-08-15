Imports ProyectoUno.A
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Public Class Form1
    Dim lista As New List(Of Token)
    Dim list_var As New Dictionary(Of String, Object)
    Dim obtener As New ObtenerTokem()
    Dim analizar As New Analizar_tokens()
    Dim escritura As New Analizar_escritura()

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        lista = obtener.obtener(txt_entrada.Text)
        list_var = analizar.analizar(lista, 0)


        For index = 0 To lista.Count - 1
            Try
                txt_salida.Text = txt_salida.Text + " 
Lexema: " + lista(index).Lexema1 + ", valor: " + list_var(lista(index).Lexema1)
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Sub txt_entrada_TextChanged(sender As Object, e As EventArgs) Handles txt_entrada.TextChanged

        txt_entrada = escritura.analizar_e(txt_entrada)
        txt_entrada.Refresh()
    End Sub
End Class
