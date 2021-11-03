Public Class Main

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    Sub IndexTheNotes()
        Try



        Catch ex As Exception
            AddTelemetryToLog("IndexTheNotes@Main", "Error: " & ex.Message, True)
        End Try
    End Sub
End Class