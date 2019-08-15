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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuDocumentosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GuardarComoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarTraduccionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerarReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LimpriarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AcercaDeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliMazariegosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txt_java
        '
        Me.txt_java.Location = New System.Drawing.Point(50, 59)
        Me.txt_java.Multiline = True
        Me.txt_java.Name = "txt_java"
        Me.txt_java.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txt_java.Size = New System.Drawing.Size(401, 427)
        Me.txt_java.TabIndex = 0
        '
        'txt_vb
        '
        Me.txt_vb.Location = New System.Drawing.Point(492, 59)
        Me.txt_vb.Multiline = True
        Me.txt_vb.Name = "txt_vb"
        Me.txt_vb.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txt_vb.Size = New System.Drawing.Size(389, 427)
        Me.txt_vb.TabIndex = 1
        '
        'txt_consola
        '
        Me.txt_consola.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txt_consola.Location = New System.Drawing.Point(50, 509)
        Me.txt_consola.Multiline = True
        Me.txt_consola.Name = "txt_consola"
        Me.txt_consola.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txt_consola.Size = New System.Drawing.Size(831, 147)
        Me.txt_consola.TabIndex = 2
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(939, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuDocumentosToolStripMenuItem, Me.ToolStripMenuItem2, Me.AcercaDeToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.ToolStripMenuItem1.Text = "File"
        '
        'MenuDocumentosToolStripMenuItem
        '
        Me.MenuDocumentosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirToolStripMenuItem, Me.GuardarComoToolStripMenuItem, Me.SalirToolStripMenuItem})
        Me.MenuDocumentosToolStripMenuItem.Name = "MenuDocumentosToolStripMenuItem"
        Me.MenuDocumentosToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.MenuDocumentosToolStripMenuItem.Text = "Menu Archivo"
        '
        'AbrirToolStripMenuItem
        '
        Me.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
        Me.AbrirToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.AbrirToolStripMenuItem.Text = "Abrir"
        '
        'GuardarComoToolStripMenuItem
        '
        Me.GuardarComoToolStripMenuItem.Name = "GuardarComoToolStripMenuItem"
        Me.GuardarComoToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.GuardarComoToolStripMenuItem.Text = "Guardar Como..."
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.SalirToolStripMenuItem.Text = "Salir"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerarTraduccionToolStripMenuItem, Me.GenerarReportesToolStripMenuItem, Me.LimpriarToolStripMenuItem})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(171, 22)
        Me.ToolStripMenuItem2.Text = "Menu Documento"
        '
        'GenerarTraduccionToolStripMenuItem
        '
        Me.GenerarTraduccionToolStripMenuItem.Name = "GenerarTraduccionToolStripMenuItem"
        Me.GenerarTraduccionToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.GenerarTraduccionToolStripMenuItem.Text = "Generar Traduccion"
        '
        'GenerarReportesToolStripMenuItem
        '
        Me.GenerarReportesToolStripMenuItem.Name = "GenerarReportesToolStripMenuItem"
        Me.GenerarReportesToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.GenerarReportesToolStripMenuItem.Text = "Generar Reportes"
        '
        'LimpriarToolStripMenuItem
        '
        Me.LimpriarToolStripMenuItem.Name = "LimpriarToolStripMenuItem"
        Me.LimpriarToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.LimpriarToolStripMenuItem.Text = "Limpriar "
        '
        'AcercaDeToolStripMenuItem
        '
        Me.AcercaDeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EliMazariegosToolStripMenuItem, Me.ToolStripMenuItem4})
        Me.AcercaDeToolStripMenuItem.Name = "AcercaDeToolStripMenuItem"
        Me.AcercaDeToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.AcercaDeToolStripMenuItem.Text = "Acerca De"
        '
        'EliMazariegosToolStripMenuItem
        '
        Me.EliMazariegosToolStripMenuItem.Name = "EliMazariegosToolStripMenuItem"
        Me.EliMazariegosToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EliMazariegosToolStripMenuItem.Text = "Eli Mazariegos "
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem4.Text = "201709426"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Viner Hand ITC", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(402, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(185, 34)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Interpreter J-VB"
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(939, 679)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_consola)
        Me.Controls.Add(Me.txt_vb)
        Me.Controls.Add(Me.txt_java)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "Principal"
        Me.Text = "Interpreter J-VB"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_java As TextBox
    Friend WithEvents txt_vb As TextBox
    Friend WithEvents txt_consola As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents MenuDocumentosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AbrirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GuardarComoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents GenerarTraduccionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GenerarReportesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LimpriarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AcercaDeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EliMazariegosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label1 As Label
End Class
