Imports System.IO
Public Class Notes
    Dim CARPETA As String = "C:\Users\" & System.Environment.UserName & "\NotesData\"
    Dim InfoDATA As ArrayList
    Dim CONTADOR As Integer = -1

    Private Sub Notes_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        InicioNotes.CARGAR()
        InicioNotes.Show()
        Me.Dispose()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DATA As String = "Nota:" & TextBox1.Text & vbCrLf
        My.Computer.FileSystem.WriteAllText(CARPETA & Label1.Text & ".WorCODE", DATA, False)
    End Sub

    Private Sub Label1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseDoubleClick
        TextBox2.Text = Label1.Text
        TextBox2.Visible = True
        Button2.Visible = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            My.Computer.FileSystem.DeleteFile(CARPETA & Label1.Text & ".WorCODE")
            InicioNotes.HelloWorld.Items.Remove(Label1.Text)
            Dim DATA As String = "Nota:" & TextBox1.Text & vbCrLf
            Threading.Thread.Sleep(1000)
            My.Computer.FileSystem.WriteAllText(CARPETA & TextBox2.Text & ".WorCODE", DATA, False)
            TextBox2.Visible = False
            Button2.Visible = False
            MsgBox("Nombre cambiado Correctamente", MsgBoxStyle.Information, "Worcome Security")
        Catch ex As Exception
            MsgBox("Error al Cambiar el Nombre" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Worcome Security")
        End Try
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        About.Show()
    End Sub
End Class
