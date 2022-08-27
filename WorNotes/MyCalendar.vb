Public Class MyCalendar

    Private Sub MyCalendar_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        InicioNotes.Show()
        Me.Dispose()
    End Sub

    Private Sub MyCalendar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class