Class AppUpdate
    Dim DIRCommons As String = "C:\Users\" & Environment.UserName & "\AppData\Local\Worcome_Studios\Commons\Updates"
    Dim DIRLeFile As String = DIRCommons & "\[UPDATE]" & My.Application.Info.AssemblyName & ".ini"
    Public WebBrowser As New WebBrowser
    Dim IdiomaAPP As String = "ENG"

    Private Sub AppUpdate_HelpRequested(ByVal sender As Object, ByVal hlpevent As System.Windows.Forms.HelpEventArgs) Handles Me.HelpRequested
        If IdiomaAPP = "ESP" Then
            MsgBox("Esta Aplicación se conecta a los Servidores de Servicios de Worcome.", MsgBoxStyle.Information, "Worcome Security")
        ElseIf IdiomaAPP = "ENG" Then
            MsgBox("This Application connects to Worcome Service Servers.", MsgBoxStyle.Information, "Worcome Security")
        End If
    End Sub

    Private Sub AppUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myCurrentLanguage As InputLanguage = InputLanguage.CurrentInputLanguage
        If myCurrentLanguage.Culture.EnglishName.Contains("Spanish") Then
            Lang_Español()
            IdiomaAPP = "ESP"
        ElseIf myCurrentLanguage.Culture.EnglishName.Contains("English") Then
            Lang_English()
            IdiomaAPP = "ENG"
        Else
            Lang_English()
            IdiomaAPP = "ENG"
        End If
        Label5.Text = AppService.AppServiceVersion
        If AppService.OfflineApp = False Then
            If AppService.CSS1 = False Or AppService.CSS2 = False Or AppService.AMC = False Or AppService.AAP = False Then
                If AppService.IdiomaApp = "ESP" Then
                    MsgBox("AppService no se ejecutó correctamente. Es posible que tenga problemas con este módulo.", MsgBoxStyle.Exclamation, "Worcome Security")
                ElseIf AppService.IdiomaApp = "ENG" Then
                    MsgBox("AppService did not run correctly. You may have problems with this module.", MsgBoxStyle.Exclamation, "Worcome Security")
                End If
            End If
        End If
        Try
            Me.Text = My.Application.Info.ProductName & " | Updates"
            If My.Computer.FileSystem.FileExists(DIRLeFile) = True Then
                My.Computer.FileSystem.DeleteFile(DIRLeFile)
            End If
            WebBrowser.ScriptErrorsSuppressed = True
            If IdiomaAPP = "ESP" Then
                Label3.Text = "Producto: " & My.Application.Info.ProductName &
                vbCrLf & "Ensamblado: " & My.Application.Info.AssemblyName &
                vbCrLf & "Versión: " & My.Application.Info.Version.ToString
            ElseIf IdiomaAPP = "ENG" Then
                Label3.Text = "Product: " & My.Application.Info.ProductName &
                vbCrLf & "Assembly: " & My.Application.Info.AssemblyName &
                vbCrLf & "Version: " & My.Application.Info.Version.ToString
            End If
        Catch ex As Exception
            Console.WriteLine("[AppUpdate]Error al Iniciar la Aplicacion: " & ex.Message)
        End Try
    End Sub

    Private Sub btnBuscarUpdates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If IdiomaAPP = "ESP" Then
                Button1.Text = "Cargando..."
            ElseIf IdiomaAPP = "ENG" Then
                Button1.Text = "Loading..."
            End If
            DownloadFile(URL_AppService & "/Wor_Services___" & My.Application.Info.ProductName & ".WorCODE")
        Catch ex As Exception
            Console.WriteLine("[AppUpdate]Error al Iniciar el Proceso de Descarga: " & ex.Message)
        End Try
    End Sub

    Sub DownloadFile(ByVal Link As String)
        Try
            Button1.Enabled = False
            If My.Computer.Network.IsAvailable = False Then
                If IdiomaAPP = "ESP" Then
                    Button1.Text = "No hay Conexion a Internet"
                    MsgBox("El computador no está conectado a internet.", MsgBoxStyle.Critical, "Worcome Security")
                ElseIf IdiomaAPP = "ENG" Then
                    Button1.Text = "No internet connection"
                    MsgBox("The computer is not connected to the internet.", MsgBoxStyle.Critical, "Worcome Security")
                End If
                Me.Close()
            ElseIf My.Computer.Network.IsAvailable = True Then
                My.Computer.Network.DownloadFile(Link, DIRLeFile) 'una descarga asincronica seria muy apreciadas
                Threading.Thread.Sleep(100)
                ReadFile()
            End If
        Catch ex As Exception
            If IdiomaAPP = "ESP" Then
                Button1.Text = "Error al Conectar"
                MsgBox("Fallo la conexión con el Servidor de Servicios de Worcome", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf IdiomaAPP = "ENG" Then
                Button1.Text = "Error trying to connect"
                MsgBox("Connection to Worcome Service Server failed", MsgBoxStyle.Critical, "Worcome Security")
            End If
            Console.WriteLine("[AppUpdate]Error al Descargar el Archivo de Actualizacion: " & ex.Message)
            Me.Close()
        End Try
    End Sub

    Sub ReadFile()
        Try
            AppService.Assembly_Status = AppService.GetIniValue("Assembly", "Status", DIRLeFile)
            AppService.Assembly_Name = AppService.GetIniValue("Assembly", "Name", DIRLeFile)
            AppService.Assembly_Version = AppService.GetIniValue("Assembly", "Version", DIRLeFile)
            AppService.Runtime_URL = AppService.GetIniValue("Runtime", "URL", DIRLeFile)
            AppService.Runtime_MSG = AppService.GetIniValue("Runtime", "MSG", DIRLeFile)
            AppService.Runtime_ArgumentLine = AppService.GetIniValue("Runtime", "ArgumentLine", DIRLeFile)
            AppService.Runtime_Command = AppService.GetIniValue("Runtime", "Command", DIRLeFile)
            AppService.Updates_Critical = AppService.GetIniValue("Updates", "Critical", DIRLeFile)
            AppService.Updates_CriticalMessage = AppService.GetIniValue("Updates", "CriticalMessage", DIRLeFile)
            AppService.Updates_RAW_Download = AppService.GetIniValue("Updates", "RAW_Download", DIRLeFile)
            AppService.Updates_Download = AppService.GetIniValue("Updates", "Download", DIRLeFile)
            AppService.Installer_Status = AppService.GetIniValue("Installer", "Status", DIRLeFile)
            AppService.Installer_DownloadFrom = AppService.GetIniValue("Installer", "DownloadFrom", DIRLeFile)
            If IdiomaAPP = "ESP" Then
                Button1.Text = "Leyendo datos..."
            ElseIf IdiomaAPP = "ENG" Then
                Button1.Text = "Reading data..."
            End If
            ReviewFile()
        Catch ex As Exception
            If IdiomaAPP = "ESP" Then
                Button1.Text = "Error al Leer"
                MsgBox("Error al leer el archivo de actualización", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf IdiomaAPP = "ENG" Then
                Button1.Text = "Read error"
                MsgBox("Error reading update file", MsgBoxStyle.Critical, "Worcome Security")
            End If
            Console.WriteLine("[AppUpdate]Error al Leer el Archivo de Actualizacion: " & ex.Message)
            Me.Close()
        End Try
    End Sub

    Sub ReviewFile()
        Try
            If IdiomaAPP = "ESP" Then
                Button1.Text = "Comparando..."
            ElseIf IdiomaAPP = "ENG" Then
                Button1.Text = "Comparing..."
            End If
            Dim vL As String = AppService.AssemblyVersionThis
            Dim vS As String = AppService.Assembly_Version
            Dim version1 = New Version(vL)
            Dim version2 = New Version(vS)
            Dim result = version1.CompareTo(version2)
            If (result > 0) Then
                If IdiomaAPP = "ESP" Then
                    Button1.Text = "No hay actualizaciones"
                    MsgBox("Tiene una versión superior a la encontrada en el servidor", MsgBoxStyle.Information, "Worcome Security")
                ElseIf IdiomaAPP = "ENG" Then
                    Button1.Text = "No Updates"
                    MsgBox("Has a higher version than the one found on the server", MsgBoxStyle.Information, "Worcome Security")
                End If
                Me.Close()
            ElseIf (result < 0) Then
                If IdiomaAPP = "ESP" Then
                    Button1.Text = "Actualización disponible"
                    MsgBox("Hay una nueva versión disponible!", MsgBoxStyle.Information, "Worcome Security")
                ElseIf IdiomaAPP = "ENG" Then
                    Button1.Text = "Update available"
                    MsgBox("There is a new version available!", MsgBoxStyle.Information, "Worcome Security")
                End If
                Button1.Enabled = False
                Me.TopMost = False
                If AppService.Updates_Download = "None" Then
                    If IdiomaAPP = "ESP" Then
                        MsgBox("No se pudo iniciar la descarga", MsgBoxStyle.Critical, "Worcome Security")
                    ElseIf IdiomaAPP = "ENG" Then
                        MsgBox("Could not start download", MsgBoxStyle.Critical, "Worcome Security")
                    End If
                Else
                    If My.Computer.FileSystem.FileExists(DIRCommons & "\Updater.zip") = True Then
                        My.Computer.FileSystem.DeleteFile(DIRCommons & "\Updater.zip")
                    End If
                    My.Computer.Network.DownloadFile(AppService.URL_Download_Updater, DIRCommons & "\Updater.zip")
                    Dim shObj As Object = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"))
                    Dim outputFolder As String = DIRCommons
                    Dim inputZip As String = DIRCommons & "\Updater.zip"
                    IO.Directory.CreateDirectory(outputFolder)
                    If My.Computer.FileSystem.FileExists(DIRCommons & "\Updater.exe") = True Then
                        My.Computer.FileSystem.DeleteFile(DIRCommons & "\Updater.exe")
                    End If
                    Dim output As Object = shObj.NameSpace((outputFolder))
                    Dim input As Object = shObj.NameSpace((inputZip))
                    output.CopyHere((input.Items), 4)
                    Threading.Thread.Sleep(50)
                    IO.File.Delete(inputZip)
                    If IdiomaAPP = "ESP" Then
                        MsgBox("El Add-In le pedira que cierre el programa actual en el futuro.", MsgBoxStyle.OkOnly, "Worcome Security")
                    ElseIf IdiomaAPP = "ENG" Then
                        MsgBox("The Add-In will ask you to close the current program in the future.", MsgBoxStyle.OkOnly, "Worcome Security")
                    End If
                    Process.Start(DIRCommons & "\Updater.exe", "/DownloadUpdates -" & My.Application.Info.AssemblyName & " -" & My.Application.Info.Version.ToString & " -" & Application.ExecutablePath)
                    'RECOMENDACION: Guardar las versiones anteriores de cada programa
                End If
            Else
                If IdiomaAPP = "ESP" Then
                    Button1.Text = "No hay actualizaciones"
                    MsgBox("No hay actualizaciones disponibles", MsgBoxStyle.Information, "Worcome Security")
                ElseIf IdiomaAPP = "ENG" Then
                    Button1.Text = "No Updates"
                    MsgBox("No updates available", MsgBoxStyle.Information, "Worcome Security")
                End If
                Me.Close()
            End If
            My.Computer.FileSystem.DeleteFile(DIRLeFile)
        Catch ex As Exception
            If IdiomaAPP = "ESP" Then
                Button1.Text = "Error al Verificar"
                MsgBox("Error al comprobar los datos del archivo de actualización", MsgBoxStyle.Critical, "Worcome Security")
            ElseIf IdiomaAPP = "ENG" Then
                Button1.Text = "Error Verifying"
                MsgBox("Error checking update file data", MsgBoxStyle.Critical, "Worcome Security")
            End If
            Console.WriteLine("[AppUpdate]Error al Analizar el Archivo de Actualizacion: " & ex.Message)
            Me.Close()
        End Try
        If IdiomaAPP = "ESP" Then
            Button1.Text = "Buscar actualizaciones"
        ElseIf IdiomaAPP = "ENG" Then
            Button1.Text = "Comparing..."
        End If
    End Sub

    Private Sub WhatsNuevo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        'abrir con AppHelper????
        Process.Start(AppService.URL_AppHelper_About & "/About_" & My.Application.Info.AssemblyName & ".html#WhatsNew")
    End Sub

    Sub Lang_Español()
        Label1.Text = "Buscar Actualizaciones"
        Label2.Text = "Revisa si la aplicación tiene una actualización disponible, siempre es bueno manejar la última versión."
        Button1.Text = "Buscar actualizaciones"
        Button2.Text = "Asistente"
    End Sub

    Sub Lang_English()
        Label1.Text = "Search for Updates"
        Label2.Text = "Check if the application has an update available, it is always good to handle the latest version."
        Button1.Text = "Search for updates"
        Button2.Text = "Assistant"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CheckUpdater()
    End Sub

    Sub CheckUpdater()
        Try
            If My.Computer.FileSystem.FileExists(DIRCommons & "\Updater.exe") = False Then
                If MsgBox("Se debe descargar un complemento para utilizar esta funcion" & vbCrLf & "¿Desea continuar?", MsgBoxStyle.YesNo, "Worcome Security") = MsgBoxResult.Yes Then
                    If IdiomaAPP = "ESP" Then
                        Button2.Text = "Descargando..."
                    ElseIf IdiomaAPP = "ENG" Then
                        Button2.Text = "Downloading..."
                    End If
                    Button2.Enabled = False
                    If My.Computer.FileSystem.FileExists(DIRCommons & "\Updater.zip") = True Then
                        My.Computer.FileSystem.DeleteFile(DIRCommons & "\Updater.zip")
                    End If
                    My.Computer.Network.DownloadFile(AppService.URL_Download_Updater, DIRCommons & "\Updater.zip")
                    Dim shObj As Object = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"))
                    Dim outputFolder As String = DIRCommons
                    Dim inputZip As String = DIRCommons & "\Updater.zip"
                    IO.Directory.CreateDirectory(outputFolder)
                    Dim output As Object = shObj.NameSpace((outputFolder))
                    Dim input As Object = shObj.NameSpace((inputZip))
                    output.CopyHere((input.Items), 4)
                    Threading.Thread.Sleep(50)
                    IO.File.Delete(inputZip)
                    Button2.Enabled = True
                    If IdiomaAPP = "ESP" Then
                        Button2.Text = "Asistente"
                    ElseIf IdiomaAPP = "ENG" Then
                        Button2.Text = "Assistant"
                    End If
                    MsgBox("Descarga completada correctamente!", MsgBoxStyle.Information, "Worcome Security")
                End If
            Else
                If MsgBox("El Add-In podria cerrar este programa en el futuro." & vbCrLf & "¿Desea continuar?", MsgBoxStyle.YesNo, "Worcome Security") = MsgBoxResult.Yes Then
                    Process.Start(DIRCommons & "\Updater.exe", "/SearchForUpdates -" & My.Application.Info.AssemblyName & " -" & My.Application.Info.Version.ToString & " -" & Application.ExecutablePath)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class