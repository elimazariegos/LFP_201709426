Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports iTextSharp.text.pdf.BaseFont
Imports System.IO
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Se pueden crear los diseños que se quieran


        Dim pdfDoc As New Document()
        'EN SIMPLE SE MANDA LA RUTA DEL ARCHIVO Y SU NOMBRE------------------------*
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream("Simple.PDF", FileMode.Create))
        pdfDoc.Open()

        'Normal
        '.........................( cadena     ,CreateFont(tamaño letra, Estilo)
        pdfDoc.Add(New Paragraph("Normal", CreateFont(12, iTextSharp.text.Font.NORMAL)))

        'Negrita
        '.........................( cadena     ,CreateFont(tamaño letra, Estilo)
        pdfDoc.Add(New Paragraph("Negrita", CreateFont(12, iTextSharp.text.Font.BOLD)))

        'Salto de linea
        '.........................( cadena     ,CreateFont(tamaño letra, Estilo)
        pdfDoc.Add(Chunk.NEWLINE)

        'Cursiva
        '.........................( cadena     ,CreateFont(tamaño letra, Estilo)
        pdfDoc.Add(New Paragraph("Cursiva", CreateFont(12, iTextSharp.text.Font.ITALIC)))

        'Subrayado
        '.........................( cadena     ,CreateFont(tamaño letra, Estilo)
        pdfDoc.Add(New Paragraph("Subrayado v1", CreateFont(12, iTextSharp.text.Font.STRIKETHRU)))
        pdfDoc.Add(New Paragraph("Subrayado v2", CreateFont(12, iTextSharp.text.Font.UNDERLINE)))

        'Ejemplo de imagen-----(Ruta, Alto, Ancho)--------
        pdfDoc.Add(CreateImage("imagen.jpg", 60, 60))


        'TERMINAR DE EDITAR ARCHIVO
        pdfDoc.Close()

    End Sub
    Public Function CreateFont(size As Integer, Optional style As Integer = iTextSharp.text.Font.NORMAL) As iTextSharp.text.Font
        Dim FontColour = New BaseColor(0, 0, 0)  'Color code
        Return New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, size, style, FontColour)
    End Function

    Public Function CreateImage(ruta As String, tamanioX As Integer, tamanioY As Integer) As iTextSharp.text.Image
        Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ruta)
        img.ScaleAbsolute(tamanioX, tamanioY)
        Return img
    End Function
End Class
