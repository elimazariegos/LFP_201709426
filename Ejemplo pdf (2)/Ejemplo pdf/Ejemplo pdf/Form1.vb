﻿Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports iTextSharp.text.pdf.BaseFont
Imports System.IO
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Se pueden crear los diseños que se quieran


        Dim pdfDoc As New Document()
        'EN SIMPLE SE MANDA LA RUTA DEL ARCHIVO Y SU NOMBRE------------------------*
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream("Simple.pdf", FileMode.Create))
        pdfDoc.Open()

        pdfDoc.Add(New Paragraph("Esto es un texto simple"))

        'SE PUEDEN CREAR LOS FORMATOS QUE SEAN
        Dim arial As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        pdfDoc.Add(New Paragraph("Esto es un texto con formato", New Font(arial, 16)))
        pdfDoc.Add(New Paragraph("Este es un titulo", New Font(arial, 20)))


        'SE PUEDEN CREAR LOS FORMATOS QUE SEAN
        Dim calibri As BaseFont = BaseFont.CreateFont("c:\windows\fonts\calibri.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        pdfDoc.Add(New Paragraph("Esto es un texto con otro formato", New Font(calibri, 16)))


        'SE PUEDEN CREAR LOS FORMATOS
        pdfDoc.Add(New Paragraph("NEGRITA", CreateFont(12, iTextSharp.text.Font.BOLDITALIC)))

        'SE PUEDEN CREAR LOS FORMATOS
        pdfDoc.Add(New Paragraph("Cursiva", CreateFont(12, iTextSharp.text.Font.ITALIC)))


        'SE PUEDEN CREAR LOS FORMATOS
        pdfDoc.Add(New Paragraph("Subrayado", CreateFont(12, iTextSharp.text.Font.STRIKETHRU)))

        'TERMINAR DE EDITAR ARCHIVO
        pdfDoc.Close()

    End Sub

    Public Function CreateFont(size As Integer, Optional style As Integer = iTextSharp.text.Font.BOLD) As iTextSharp.text.Font
        Dim FontColour = New BaseColor(193, 36, 67)  'Color code
        Return New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, size, style, FontColour)
    End Function


End Class
