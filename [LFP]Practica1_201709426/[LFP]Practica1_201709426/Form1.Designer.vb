<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.tab_principal = New System.Windows.Forms.TabControl()
        Me.btn_nueva = New System.Windows.Forms.Button()
        Me.btn_cerar = New System.Windows.Forms.Button()
        Me.btn_cerrar_todas = New System.Windows.Forms.Button()
        Me.btn_abrir = New System.Windows.Forms.Button()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.btn_guardar_como = New System.Windows.Forms.Button()
        Me.btn_guardar_todo = New System.Windows.Forms.Button()
        Me.btn_analizar = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.lista_consola = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tab_principal
        '
        Me.tab_principal.Location = New System.Drawing.Point(75, 56)
        Me.tab_principal.Name = "tab_principal"
        Me.tab_principal.SelectedIndex = 0
        Me.tab_principal.Size = New System.Drawing.Size(856, 334)
        Me.tab_principal.TabIndex = 0
        '
        'btn_nueva
        '
        Me.btn_nueva.Location = New System.Drawing.Point(75, 13)
        Me.btn_nueva.Name = "btn_nueva"
        Me.btn_nueva.Size = New System.Drawing.Size(75, 23)
        Me.btn_nueva.TabIndex = 1
        Me.btn_nueva.Text = "NUEVA VENTANA"
        Me.btn_nueva.UseVisualStyleBackColor = True
        '
        'btn_cerar
        '
        Me.btn_cerar.Location = New System.Drawing.Point(156, 12)
        Me.btn_cerar.Name = "btn_cerar"
        Me.btn_cerar.Size = New System.Drawing.Size(116, 23)
        Me.btn_cerar.TabIndex = 2
        Me.btn_cerar.Text = "CERRAR VENTANA"
        Me.btn_cerar.UseVisualStyleBackColor = True
        '
        'btn_cerrar_todas
        '
        Me.btn_cerrar_todas.Location = New System.Drawing.Point(278, 12)
        Me.btn_cerrar_todas.Name = "btn_cerrar_todas"
        Me.btn_cerrar_todas.Size = New System.Drawing.Size(105, 23)
        Me.btn_cerrar_todas.TabIndex = 3
        Me.btn_cerrar_todas.Text = "CERRAR TODAS"
        Me.btn_cerrar_todas.UseVisualStyleBackColor = True
        '
        'btn_abrir
        '
        Me.btn_abrir.Location = New System.Drawing.Point(389, 12)
        Me.btn_abrir.Name = "btn_abrir"
        Me.btn_abrir.Size = New System.Drawing.Size(75, 23)
        Me.btn_abrir.TabIndex = 4
        Me.btn_abrir.Text = "ABRIR"
        Me.btn_abrir.UseVisualStyleBackColor = True
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(470, 13)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(75, 23)
        Me.btn_guardar.TabIndex = 5
        Me.btn_guardar.Text = "GUARDAR"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'btn_guardar_como
        '
        Me.btn_guardar_como.Location = New System.Drawing.Point(551, 13)
        Me.btn_guardar_como.Name = "btn_guardar_como"
        Me.btn_guardar_como.Size = New System.Drawing.Size(111, 23)
        Me.btn_guardar_como.TabIndex = 6
        Me.btn_guardar_como.Text = "GUARDAR COMO"
        Me.btn_guardar_como.UseVisualStyleBackColor = True
        '
        'btn_guardar_todo
        '
        Me.btn_guardar_todo.Location = New System.Drawing.Point(668, 12)
        Me.btn_guardar_todo.Name = "btn_guardar_todo"
        Me.btn_guardar_todo.Size = New System.Drawing.Size(117, 23)
        Me.btn_guardar_todo.TabIndex = 7
        Me.btn_guardar_todo.Text = "GUARDAR TODO"
        Me.btn_guardar_todo.UseVisualStyleBackColor = True
        '
        'btn_analizar
        '
        Me.btn_analizar.Location = New System.Drawing.Point(856, 13)
        Me.btn_analizar.Name = "btn_analizar"
        Me.btn_analizar.Size = New System.Drawing.Size(75, 23)
        Me.btn_analizar.TabIndex = 8
        Me.btn_analizar.Text = "ANALIZAR"
        Me.btn_analizar.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'lista_consola
        '
        Me.lista_consola.BackColor = System.Drawing.SystemColors.Info
        Me.lista_consola.Font = New System.Drawing.Font("Microsoft YaHei Light", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lista_consola.Location = New System.Drawing.Point(75, 422)
        Me.lista_consola.Multiline = True
        Me.lista_consola.Name = "lista_consola"
        Me.lista_consola.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.lista_consola.Size = New System.Drawing.Size(856, 95)
        Me.lista_consola.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(85, 406)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Consola de resultados"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(991, 542)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lista_consola)
        Me.Controls.Add(Me.btn_analizar)
        Me.Controls.Add(Me.btn_guardar_todo)
        Me.Controls.Add(Me.btn_guardar_como)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.btn_abrir)
        Me.Controls.Add(Me.btn_cerrar_todas)
        Me.Controls.Add(Me.btn_cerar)
        Me.Controls.Add(Me.btn_nueva)
        Me.Controls.Add(Me.tab_principal)
        Me.Name = "Form1"
        Me.Text = "BASIC LEX"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tab_principal As TabControl
    Friend WithEvents btn_nueva As Button
    Friend WithEvents btn_cerar As Button
    Friend WithEvents btn_cerrar_todas As Button
    Friend WithEvents btn_abrir As Button
    Friend WithEvents btn_guardar As Button
    Friend WithEvents btn_guardar_como As Button
    Friend WithEvents btn_guardar_todo As Button
    Friend WithEvents btn_analizar As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents lista_consola As TextBox
    Friend WithEvents Label2 As Label
End Class
