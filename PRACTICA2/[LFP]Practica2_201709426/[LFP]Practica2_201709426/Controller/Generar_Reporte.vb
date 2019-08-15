Public Class Generar_Reporte
    Dim nueva As New List(Of Token)
    Dim lista_token As List(Of Token)

    Public Function generar(lista_token As List(Of Token), lista_errores As List(Of Errror))
        Me.lista_token = lista_token
        actualizar()
        Dim html As String

        html = "<html>
                    <body>
		                <h1>ERRORES ENCONTRADOS EN EL ANALIS</h1>
                        <table border=" + ChrW(34) + "1" + ChrW(34) +
                        " style=" + ChrW(34) + "margin: 0 auto;" + ChrW(34) + ">
			                <tr>
				                <th>Lexema</th>
                                <th>Fila</th>
                                <th>Columna</th>
                                <th>Descripcion</th>
				            </tr>
		"
        For i = 0 To lista_errores.Count - 1
            html = html + "<tr>
				                <th>" + CType(lista_errores(i).getLexema, String) + "</th>
				                <th>" + CType(lista_errores(i).getFila, String) + "</th>
				                <th>" + CType(lista_errores(i).getColumna, String) + "</th>
				                <th>" + CType(lista_errores(i).getDescripcion, String) + "</th>
			                </tr>"

        Next
        html = html + "</table><br><br>
                        <h1>TABLA DE TOKENS ENCONTRADOS EN EL ANALISIS</h1>
                        <table border=" + ChrW(34) + "1" + ChrW(34) +
                        " style=" + ChrW(34) + "margin: 0 auto;" + ChrW(34) + ">
			                <tr>
				                <th>Numero de Token</th>
				                <th>Lexema</th>
                                <th>Fila</th>
                                <th>Columna</th>
			                </tr>"
        For i = 0 To nueva.Count - 1
            html = html + "<tr>
				                <th>" + CType(nueva(i).getNumero, String) + "</th>
				                <th>" + CType(nueva(i).getLexema, String) + "</th>
				                <th>" + CType(nueva(i).getFila, String) + "</th>
				                <th>" + CType(nueva(i).getColumna, String) + "</th>
			                </tr>"

        Next
        html = html + "</table></body></html>"
        Try
            Dim sfd As New SaveFileDialog
            sfd.Filter = "HTML tokens Field | *.html"
            Dim resultado_sfd As String
            sfd.ShowDialog()
            resultado_sfd = sfd.FileName

            Dim SW As New IO.StreamWriter(resultado_sfd)
            SW.Write(html)
            SW.Flush()
            SW.Close()


        Catch ex As Exception

        End Try
    End Function

    Public Sub actualizar()
        For i = 0 To lista_token.Count - 1
            If buscar(lista_token(i).getLexema) <> True Then
                nueva.Add(lista_token(i))
            End If
        Next
    End Sub

    Public Function buscar(lexema As String)

        Try
            For i = 0 To nueva.Count - 1
                If nueva(i).getLexema = lexema Then
                    Return True
                End If
            Next
        Catch ex As Exception
            Return False
        End Try

    End Function
End Class
