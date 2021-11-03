Public Class Debugger

    Private Sub Debugger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CommonLoad()
        ReadParameters()
    End Sub

    Sub ReadParameters()
        Try
            If My.Application.CommandLineArgs.Count > 0 Then
                For i As Integer = 0 To My.Application.CommandLineArgs.Count - 1
                    Dim parametro As String = My.Application.CommandLineArgs(i)
                    Dim Args As String = Nothing
                    Dim Pars As String() = Nothing
                    If parametro.Contains("|") Then
                        Args = parametro.Remove(0, parametro.LastIndexOf("|") + 1)
                        Pars = Args.Split(",")
                    End If
                    If parametro Like "*/Note|*" Then


                    ElseIf parametro Like "*/StuffNote|*" Then


                    End If
                Next
            Else
                CommonStart()
            End If
        Catch ex As Exception
            AddTelemetryToLog("ReadParameters@Debugger", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub CommonLoad()

    End Sub
    Sub CommonStart()
        Main.Show()
        Main.Focus()
    End Sub
End Class