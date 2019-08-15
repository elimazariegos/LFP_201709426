<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Principal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txt_java = New System.Windows.Forms.TextBox()
        Me.txt_vb = New System.Windows.Forms.TextBox()
        Me.txt_consola = New System.Windows.Forms.TextBox()
        Me.btn_analizar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txt_java
        '
        Me.txt_java.Location = New System.Drawing.Point(50, 59)
        Me.txt_java.Multiline = True
        Me.txt_java.Name = "txt_java"
        Me.txt_java.Size = New System.Drawing.Size(401, 427)
        Me.txt_java.TabIndex = 0
        '
        'txt_vb
        '
        Me.txt_vb.Location = New System.Drawing.Point(492, 59)
        Me.txt_vb.Multiline = True
        Me.txt_vb.Name = "txt_vb"
        Me.txt_vb.Size = New System.Drawing.Size(389, 427)
        Me.txt_vb.TabIndex = 1
        '
        'txt_consola
        '
        Me.txt_consola.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txt_consola.Location = New System.Drawing.Point(50, 509)
        Me.txt_consola.Multiline = True
        Me.txt_consola.Name = "txt_consola"
        Me.txt_consola.Size = New System.Drawing.Size(831, 147)
        Me.txt_consola.TabIndex = 2
        '
        'btn_analizar
        '
        Me.btn_analizar.Location = New System.Drawing.Point(50, 13)
        Me.btn_analizar.Name = "btn_analizar"
        Me.btn_analizar.Size = New System.Drawing.Size(75, 23)
        Me.btn_analizar.TabIndex = 3
        Me.btn_analizar.Text = "Analizar"
        Me.btn_analizar.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(939, 679)
        Me.Controls.Add(Me.btn_analizar)
        Me.Controls.Add(Me.txt_consola)
        Me.Controls.Add(Me.txt_vb)
        Me.Controls.Add(Me.txt_java)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_java As TextBox
    Friend WithEvents txt_vb As TextBox
    Friend WithEvents txt_consola As TextBox
    Friend WithEvents btn_analizar As Button
End Class
