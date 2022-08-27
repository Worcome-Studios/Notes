Public Class About

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If InicioNotes.OfflineMode = True Then
            CheckBox1.CheckState = CheckState.Checked
        Else
            CheckBox1.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        AppSupport.show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AppUpdate.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        AppHelper.Show()
        AppHelper.Focus()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            InicioNotes.OfflineMode = True
        ElseIf CheckBox1.CheckState = CheckState.Unchecked Then
            InicioNotes.OfflineMode = False
            If MsgBox("Do you want to run an AppService instance?", MsgBoxStyle.YesNo, "Worcome Security") = MsgBoxResult.Yes Then
                Try
                    AppService.StartAppService(False, False, True, True, True) 'Offline, SecureMode, AppManager, SignRegistry (quitado), AppService
                Catch
                End Try
            End If
        End If
        InicioNotes.SaveRegedit()
    End Sub
End Class