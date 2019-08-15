<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txt_salida = New System.Windows.Forms.RichTextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.txt_entrada = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(45, 252)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 37)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Analizar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txt_salida
        '
        Me.txt_salida.DetectUrls = False
        Me.txt_salida.ForeColor = System.Drawing.Color.Yellow
        Me.txt_salida.Location = New System.Drawing.Point(25, 305)
        Me.txt_salida.Name = "txt_salida"
        Me.txt_salida.Size = New System.Drawing.Size(738, 160)
        Me.txt_salida.TabIndex = 2
        Me.txt_salida.Text = ""
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(365, 471)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(113, 37)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Generar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(26, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(71, 25)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Guardar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(103, 4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(71, 25)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Abrir"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(176, 252)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(113, 37)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "GenerarTokens"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(309, 252)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(113, 37)
        Me.Button6.TabIndex = 7
        Me.Button6.Text = "GenerarErrores"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(440, 252)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(113, 37)
        Me.Button7.TabIndex = 8
        Me.Button7.Text = "GenerarVariables"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'txt_entrada
        '
        Me.txt_entrada.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txt_entrada.Location = New System.Drawing.Point(26, 46)
        Me.txt_entrada.Multiline = True
        Me.txt_entrada.Name = "txt_entrada"
        Me.txt_entrada.Size = New System.Drawing.Size(737, 200)
        Me.txt_entrada.TabIndex = 9
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(817, 484)
        Me.Controls.Add(Me.txt_entrada)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txt_salida)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents txt_salida As RichTextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents txt_entrada As TextBox
End Class
