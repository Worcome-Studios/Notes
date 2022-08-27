Imports System.IO
Imports Microsoft.Win32
Public Class InicioNotes
    Dim CARPETA As String = "C:\Users\" & System.Environment.UserName & "\NotesData\"
    Dim InfoDATA As ArrayList
    Dim CONTADOR As Integer = -1
    Dim parametros As String

    Private Sub Menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        parametros = Command()
        If parametros = Nothing Then
        ElseIf parametros = "/FactoryReset" Then
            If MessageBox.Show("¿Desea hacer un reset?", "Worcome Security", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Try
                    Registry.CurrentUser.DeleteSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName)
                Catch ex As Exception
                End Try
                MsgBox("Reset completado", MsgBoxStyle.Information, "Worcome Security")
                End
            Else
                End
            End If
        End If
        If RegeditKey Is Nothing Then
            SaveRegedit()
        Else
            GetRegedit()
        End If
        Try
            If OfflineMode = False Then
                AppService.StartAppService(False, False, True, False, True)
                Threading.Thread.Sleep(150)
            End If
        Catch ex As Exception
            MsgBox("ERROR CRITICO CON 'AppService'", MsgBoxStyle.Critical, "Worcome Security")
        End Try
        Try
            If My.Computer.FileSystem.DirectoryExists(CARPETA) = False Then
                My.Computer.FileSystem.CreateDirectory(CARPETA)
            End If
            CARGAR()
        Catch ex As Exception
            MsgBox("Error al General la Base", MsgBoxStyle.Critical, "Worcome Security")
        End Try
    End Sub

    Public OfflineMode As Boolean = False

    Dim RegeditKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName, True)
    Sub SaveRegedit()
        If RegeditKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName)
            RegeditKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName, True)
        End If
        Try
            RegeditKey.SetValue("OfflineMode", OfflineMode, RegistryValueKind.String)
            GetRegedit()
        Catch ex As Exception
            Console.WriteLine("[ERROR1]: " & ex.Message)
        End Try
    End Sub
    Sub GetRegedit()
        Try
            OfflineMode = Boolean.Parse(RegeditKey.GetValue("OfflineMode"))
        Catch
        End Try
        Try

        Catch
        End Try
    End Sub

    Public Sub CARGAR()
        HelloWorld.Items.Clear()
        InfoDATA = New ArrayList
        For Each FICHA In Directory.GetFiles(CARPETA)
            InfoDATA.Add(FICHA)
            FICHA = FICHA.Replace(CARPETA, "")
            FICHA = FICHA.Replace(".WorCODE", "")
            HelloWorld.Items.Add(FICHA.ToString)
        Next
    End Sub

    Public Sub DATOS()
        Try
            Dim NOMBRE As String = InfoDATA(CONTADOR)
            NOMBRE = NOMBRE.Remove(0, NOMBRE.LastIndexOf("\") + 1)
            NOMBRE = NOMBRE.Replace(".WorCODE", "")
            Notes.Label1.Text = NOMBRE.ToString
            For Each LINEA In File.ReadLines(InfoDATA(CONTADOR).ToString)
                If LINEA.StartsWith("Nota:") Then
                    Notes.TextBox1.Text = LINEA.Split(":")(1)
                End If
            Next
            Notes.Show()
            Me.Hide()
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Worcome Security")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Panel1.Visible = False Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text <> Nothing Then
            Try
                Dim DATAServiceName As String = TextBox1.Text
                My.Computer.FileSystem.WriteAllText(CARPETA & DATAServiceName & ".WorCODE", Nothing, False)
                CARGAR()
                Panel1.Visible = False
            Catch ex As Exception
                'MsgBox(ex.Message, MsgBoxStyle.Critical, "Worcome Security")
            End Try
        Else
            MsgBox("Rellena con la Informacion Solicitada", MsgBoxStyle.Critical, "Worcome Security")
        End If
    End Sub

    Private Sub HelloWorld_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HelloWorld.MouseDoubleClick
        CONTADOR = HelloWorld.SelectedIndex
        DATOS()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MyTasker.Show()
        MyTasker.Focus()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        MyCalendar.Show()
        MyCalendar.Focus()
        Me.Hide()
    End Sub

    Private Sub HelloWorld_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelloWorld.SelectedIndexChanged

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            File.Delete(CARPETA & TextBox1.Text.ToLower & ".WorCODE")
            CARGAR()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        About.Show()
    End Sub
End Class