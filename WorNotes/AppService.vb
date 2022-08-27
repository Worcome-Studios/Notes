Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports Microsoft.Win32
Module AppService
    ReadOnly DIRCommons As String = "C:\Users\" & System.Environment.UserName & "\AppData\Local\Worcome_Studios\Commons"
    ReadOnly DIRAppManager As String = DIRCommons & "\AppManager"
    ReadOnly DIRKeyManager As String = DIRAppManager & "\KeyManager"
    ReadOnly DirAppPatch As String = Application.ExecutablePath
    Public ReadOnly ServerSwitchURLs = {"http://www.dropbox.com/s/9ktiht78g0n9v0y/WSS_ServerSwitch.ini?dl=1",
                    "http://worcomestudios.comule.com/Recursos/InfoData/WSS_ServerSwitch.ini",
                    "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/WSS_ServerSwitch.ini",
                    "http://worcomecorporations.000webhostapp.com/Source/WSS/WSS_ServerSwitch.ini",
                    "http://docs.google.com/uc?export=download&id=1ztBhx9ROXfHTBknpAyJk49S3P0EeypOA",
                    "http://worcomestudios.rf.gd/Recursos/WSS_Source/WSS_ServerSwitch.inf"}

    Dim ServerIndex As Integer = 0
    Dim ServerTrying As Integer = 0
    Public ReadOnly AppServiceVersion As String = "1.2.0.1"
    Public ReadOnly AppServiceCompilate As String = "23.12.20.20"

    Public AssemblyNameThis As String = My.Application.Info.AssemblyName
    Public AssemblyVersionThis As String = My.Application.Info.Version.ToString
    Public AssemblyProductNameThis As String = My.Application.Info.ProductName

    Public OfflineApp As Boolean
    Public SecureMode As Boolean
    Dim AppManager As Boolean
    Dim SignRegistry As Boolean
    Dim AppServiceStatus As Boolean

#Region "AppService Vars"
    'AppService
    Public Assembly_Status As String
    Public Assembly_Name As String
    Public Assembly_Version As String

    Public Runtime_URL As String
    Public Runtime_MSG As String
    Public Runtime_ArgumentLine As String
    Public Runtime_Command As String

    Public Updates_Critical As String
    Public Updates_CriticalMessage As String
    Public Updates_RAW_Download As String
    Public Updates_Download As String

    Public Installer_Status As String
    Public Installer_DownloadFrom As String

    Public AppRegistry As RegistryKey
    Public AppServiceConfig As RegistryKey
    Dim ShowAppServiceMessages As Boolean = True
    Public Registry_Assembly As String
    Public Registry_Version As String
    Public Registry_InstalledDate As String
    Public Registry_LastStart As String
    Public Registry_Directory As String
    Public Registry_AllUsersCanUse As String
#End Region

    Public IdiomaApp As String = "ENG"

    Public CSS1 As Boolean = False
    Public CSS2 As Boolean = False
    Public AMC As Boolean = False
    Public AAP As Boolean = False

#Region "ServerSwitch Vars"
    'ServerSwitch
    Public UsingServer As String = "WS1"
    Dim ServerStatus As String = Nothing
    Dim ServerMSG As String = Nothing
    Dim ServerURL As String = Nothing
    Dim ServerArgumentLine As String = Nothing
    Dim ServerCommand As String = Nothing
    Dim URLs_Update As String = Nothing
    Public CurrentServerURL As String = "http://worcomestudios.comule.com"
    Public URL_KeyAccessToken As String = "http://worcomestudios.comule.com/Recursos/InfoData/KeyAccessToken.WorCODE"
    Public URL_AppService As String = "http://worcomestudios.comule.com/Recursos/InfoData/WorAppServices"
    Public URL_AppUpdate As String = "http://worcomestudios.comule.com/Recursos/InfoData/Updates"
    Public URL_AppUpdate_WhatsNew As String = "http://worcomestudios.comule.com/Recursos/InfoData/WhatsNew"
    Public URL_AppHelper_Help As String = "http://worcomestudios.comule.com/Recursos/AppHelper"
    Public URL_AppHelper_About As String = "http://worcomestudios.comule.com/Recursos/AppHelper/AboutApps"
    Public URL_Support_Post As String = "http://worcomestudios.comule.com/Recursos/WorCommunity/soporte.php"
    Public URL_Telemetry_Post As String = "http://worcomestudios.comule.com/Recursos/InfoData/TelemetryPost.php"
    Public URL_Download_Updater As String = "http://worcomestudios.comule.com/Recursos/InfoData/Updates/Updater.zip"
#End Region

    Dim WithEvents DownloaderArrayServerSwitch As New Net.WebClient
    Dim WithEvents DownloaderArrayAppService As New Net.WebClient
    Dim DownloadURIServerSwitch As Uri
    Dim DownloadURIAppService As Uri

    Sub StartAppService(ByVal OffLineApp_SAS As Boolean, ByVal SecureModeSAS As Boolean, ByVal AppManager_SAS As Boolean, ByVal SignRegistry_SAS As Boolean, ByVal AppServiceStatus_SAS As Boolean, Optional ByVal AssemblyName As String = Nothing, Optional ByVal AssemblyVersion As String = Nothing)
        Dim myCurrentLanguage As InputLanguage = InputLanguage.CurrentInputLanguage
        If myCurrentLanguage.Culture.EnglishName.Contains("Spanish") Then
            IdiomaApp = "ESP"
        ElseIf myCurrentLanguage.Culture.EnglishName.Contains("English") Then
            IdiomaApp = "ENG"
        Else
            IdiomaApp = "ENG"
        End If
        If AssemblyName IsNot Nothing And AssemblyVersion IsNot Nothing Then
            AssemblyNameThis = AssemblyName
            AssemblyVersionThis = AssemblyVersion
            AssemblyProductNameThis = AssemblyNameThis.Replace("Wor", Nothing)
        End If
        'If MsgBox("Do you want to run an AppService instance?", MsgBoxStyle.YesNo, "Worcome Security") = MsgBoxResult.Yes Then
        '    Try
        '        AppService.StartAppService(False, False, True, True, True) 'Offline, SecureMode, AppManager, SignRegistry (quitado), AppService
        '    Catch
        '    End Try
        'End If
        'If MessageBox.Show("¿Desea hacer un reset?", "Worcome Security", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
        '    Try
        '        Registry.CurrentUser.DeleteSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName)
        '    Catch ex As Exception
        '    End Try
        '    MsgBox("Reset completado", MsgBoxStyle.Information, "Worcome Security")
        '    End
        'Else
        '    End
        'End If
        'If RegeditKey Is Nothing Then
        '    SaveRegedit()
        'Else
        '    GetRegedit()
        'End If
        '    Public OfflineMode As Boolean = False

        '    Dim RegeditKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName, True)
        'Sub SaveRegedit()
        '    If RegeditKey Is Nothing Then
        '        Registry.CurrentUser.CreateSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName)
        '        RegeditKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & My.Application.Info.AssemblyName, True)
        '    End If
        '    Try
        '        RegeditKey.SetValue("WelcomeMessage", WelcomeMessage, RegistryValueKind.String)
        '        RegeditKey.SetValue("ThemeSelected", ThemeSelected, RegistryValueKind.String)
        '        RegeditKey.SetValue("SaveDir", SaveDir, RegistryValueKind.String)
        '        RegeditKey.SetValue("Email", Email, RegistryValueKind.String)
        '        RegeditKey.SetValue("BackUp", BackUp, RegistryValueKind.String)
        '        RegeditKey.SetValue("CryptoKey", CryptoKey, RegistryValueKind.String)
        '        RegeditKey.SetValue("OfflineMode", OfflineMode, RegistryValueKind.String)
        '        GetRegedit()
        '    Catch ex As Exception
        '        Console.WriteLine("[ERROR1]: " & ex.Message)
        '    End Try
        'End Sub
        'Sub GetRegedit()
        '    Try
        '        WelcomeMessage = Boolean.Parse(RegeditKey.GetValue("WelcomeMessage"))
        '        ThemeSelected = RegeditKey.GetValue("ThemeSelected")
        '        SaveDir = RegeditKey.GetValue("SaveDir")
        '        Email = RegeditKey.GetValue("Email")
        '        BackUp = RegeditKey.GetValue("BackUp")
        '        CryptoKey = RegeditKey.GetValue("CryptoKey")
        '        OfflineMode = Boolean.Parse(RegeditKey.GetValue("OfflineMode"))
        '    Catch
        '    End Try
        '    Try
        '        
        '    Catch
        '    End Try
        'End Sub
        Console.WriteLine("[StartAppService]Iniciado en: " & vbCrLf & "   Offline Mode: " & OffLineApp_SAS & vbCrLf & "   Secure Mode: " & SecureModeSAS & vbCrLf & "   AppManager: " & AppManager_SAS & vbCrLf & "   SignRegistry: " & SignRegistry_SAS & vbCrLf & "   AppService: " & AppServiceStatus_SAS)

        OfflineApp = OffLineApp_SAS
        SecureMode = SecureModeSAS
        AppManager = AppManager_SAS
        SignRegistry = SignRegistry_SAS
        AppServiceStatus = AppServiceStatus_SAS

        AppRegistry = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & AssemblyNameThis, True)
        Try
            AppServiceConfig = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\AppService", True)
            If AppServiceConfig Is Nothing Then
                Registry.CurrentUser.CreateSubKey("Software\\Worcome_Studios\\AppService")
                Dim Reg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\AppService", True)
                Reg.SetValue("ShowMessages", "True", RegistryValueKind.String)
            End If
            ShowAppServiceMessages = Boolean.Parse(AppServiceConfig.GetValue("ShowMessages"))
        Catch
        End Try
        If SecureMode = True Then
            If My.Computer.Network.IsAvailable = False Then
                MsgBox("Esta aplicación necesita acceso a internet para continuar", MsgBoxStyle.Critical, "Worcome Security")
                End 'END_PROGRAM
            End If
        End If
        Try
            If My.Computer.FileSystem.DirectoryExists(DIRCommons) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            If My.Computer.FileSystem.DirectoryExists(DIRAppManager) = False Then
                My.Computer.FileSystem.CreateDirectory(DIRAppManager)
            End If
        Catch ex As Exception
        End Try
        If My.Computer.FileSystem.FileExists(DIRCommons & "\[" & AssemblyNameThis & "]Status_WSS.ini") = True Then
            My.Computer.FileSystem.DeleteFile(DIRCommons & "\[" & AssemblyNameThis & "]Status_WSS.ini")
        End If
        If OffLineApp_SAS = False Then
            ServerTrying = ServerTrying + 1
            DownloadURIServerSwitch = New Uri(ServerSwitchURLs(ServerIndex))
            DownloaderArrayServerSwitch.DownloadFileAsync(DownloadURIServerSwitch, DIRCommons & "\[" & AssemblyNameThis & "]Status_WSS.ini")
        Else
            Console.WriteLine("'AppService' Omitido")
        End If
    End Sub

    Private Sub DownloaderArrayServerSwitch_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles DownloaderArrayServerSwitch.DownloadFileCompleted
        ClueChangeServer(OfflineApp, SecureMode, AppManager, SignRegistry, AppServiceStatus)
    End Sub

    Sub ClueChangeServer(ByVal OffLineApp_CCS As Boolean, ByVal SecureMode_CCS As Boolean, ByVal AppManager_CCS As Boolean, ByVal SignRegistry_CCS As Boolean, ByVal AppServiceStatus_CCS As Boolean)
        Try
            Dim SwitchFilePath As String = DIRCommons & "\[" & AssemblyNameThis & "]Status_WSS.ini"
            UsingServer = GetIniValue("WSS", "UsingServer", SwitchFilePath)
            ServerStatus = GetIniValue("WSS", "ServerStatus", SwitchFilePath)
            ServerMSG = GetIniValue("WSS", "Message", SwitchFilePath)
            ServerURL = GetIniValue("WSS", "URL", SwitchFilePath)
            ServerArgumentLine = GetIniValue("WSS", "ArgumentLine", SwitchFilePath)
            ServerCommand = GetIniValue("WSS", "Command", SwitchFilePath)
            URLs_Update = GetIniValue("ServerSwitch", "URL_Update", SwitchFilePath)
            CurrentServerURL = GetIniValue("ServerSwitch", "CurrentServerURL", SwitchFilePath)
            URL_KeyAccessToken = GetIniValue("ServerSwitch", "URL_KeyAccessToken", SwitchFilePath)
            URL_AppService = GetIniValue("ServerSwitch", "URL_AppService", SwitchFilePath)
            URL_AppUpdate = GetIniValue("ServerSwitch", "URL_AppUpdate", SwitchFilePath)
            URL_AppUpdate_WhatsNew = GetIniValue("ServerSwitch", "URL_AppUpdate_WhatsNew", SwitchFilePath)
            URL_AppHelper_Help = GetIniValue("ServerSwitch", "URL_AppHelper_Help", SwitchFilePath)
            URL_AppHelper_About = GetIniValue("ServerSwitch", "URL_AppHelper_About", SwitchFilePath)
            URL_Support_Post = GetIniValue("ServerSwitch", "URL_AppSupport_Post", SwitchFilePath)
            URL_Telemetry_Post = GetIniValue("ServerSwitch", "URL_Telemetry_Post", SwitchFilePath)
            URL_Download_Updater = GetIniValue("ServerSwitch", "URL_Download_Updater", SwitchFilePath)
            If ServerTrying >= 6 Then
                Console.WriteLine("[AppService]All servers failed!.")
                CSS1 = False
                CSS2 = False
                AMC = False
                AAP = False
                ServerTrying = 0
                Exit Sub
            Else
                If UsingServer = Nothing Or ServerStatus = Nothing Or URLs_Update = Nothing Or CurrentServerURL = Nothing Then
                    If ServerIndex = 0 Then
                        ServerIndex = 1
                    ElseIf ServerIndex = 1 Then
                        ServerIndex = 2
                    ElseIf ServerIndex = 2 Then
                        ServerIndex = 3
                    ElseIf ServerIndex = 3 Then
                        ServerIndex = 4
                    ElseIf ServerIndex = 4 Then
                        ServerIndex = 5
                    ElseIf ServerIndex = 5 Then
                        ServerIndex = 0
                    End If
                    If DownloaderArrayServerSwitch.IsBusy Then
                        DownloaderArrayServerSwitch.Dispose()
                        DownloaderArrayServerSwitch.CancelAsync()
                    End If
                    Console.WriteLine("[AppService]ServerSwitch failed!. Trying with another server " & "(" & ServerIndex & ")")
                    StartAppService(OfflineApp, SecureMode, AppManager, SignRegistry, AppServiceStatus)
                    Exit Sub
                Else
                    'algo como 403 o moved o 404?
                End If
            End If
            If ServerStatus = "Stopped" Then
                If SecureMode = True Then
                    MsgBox("The Worcome Server are not working." & vbCrLf & "Try it later", MsgBoxStyle.Critical, "Worcome Security")
                    If ServerMSG = "None" Then
                    Else
                        MsgBox("Worcome Server Services" & vbCrLf & ServerMSG, MsgBoxStyle.Information, "Worcome Security")
                    End If
                    End 'END_PROGRAM
                End If
            Else
                If ServerMSG = "None" Then
                Else
                    MsgBox("Worcome Server Services" & vbCrLf & ServerMSG, MsgBoxStyle.Information, "Worcome Security")
                End If
                If ServerURL = "None" Then
                Else
                    Process.Start(ServerURL)
                End If
                If ServerArgumentLine = "None" Then
                Else
                    Process.Start(DirAppPatch, ServerArgumentLine)
                End If
                If ServerCommand = "None" Then
                Else
                    Process.Start(ServerCommand)
                End If
            End If
            Console.WriteLine("[AppService]Using Server: " & UsingServer)
            CSS1 = True
        Catch ex As Exception
            Console.WriteLine("[AppService@ServerSwitch:AnalizeInformation]Error: " & ex.Message)
            If SecureMode_CCS = True Then
                If My.Computer.Network.IsAvailable = False Then
                    MsgBox("Esta aplicación necesita acceso a internet para continuar", MsgBoxStyle.Critical, "Worcome Security")
                Else
                    MsgBox("No se pudo conectar a los Servidores de Servicios de Worcome", MsgBoxStyle.Critical, "Worcome Security")
                End If
                End 'END_PROGRAM
            End If
        End Try
        Try
            If URLs_Update = "No" Then
                If UsingServer = "WS1" Then
                    CurrentServerURL = "http://worcomestudios.comule.com"
                    URL_KeyAccessToken = "http://worcomestudios.comule.com/Recursos/InfoData/KeyAccessToken.WorCODE"
                    URL_AppService = "http://worcomestudios.comule.com/Recursos/InfoData/WorAppServices"
                    URL_AppUpdate = "http://worcomestudios.comule.com/Recursos/InfoData/Updates"
                    URL_AppUpdate_WhatsNew = "http://worcomestudios.comule.com/Recursos/InfoData/WhatsNew"
                    URL_AppHelper_Help = "http://worcomestudios.comule.com/Recursos/AppHelper"
                    URL_AppHelper_About = "http://worcomestudios.comule.com/Recursos/AppHelper/AboutApps"
                    URL_Support_Post = "http://worcomestudios.comule.com/Recursos/WorCommunity/soporte.php"
                    URL_Telemetry_Post = "http://worcomestudios.comule.com/Recursos/InfoData/TelemetryPost.php"
                ElseIf UsingServer = "WS2" Then
                    CurrentServerURL = "http://worcomestudios.mywebcommunity.org"
                    URL_KeyAccessToken = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/KeyAccessToken.WorCODE"
                    URL_AppService = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/WorAppServices"
                    URL_AppUpdate = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/Updates"
                    URL_AppUpdate_WhatsNew = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/WhatsNew"
                    URL_AppHelper_Help = "http://worcomestudios.mywebcommunity.org/Recursos/AppHelper"
                    URL_AppHelper_About = "http://worcomestudios.mywebcommunity.org/Recursos/AppHelper/AboutApps"
                    URL_Support_Post = "http://worcomestudios.mywebcommunity.org/Recursos/WorCommunity/soporte.php"
                    URL_Telemetry_Post = "http://worcomestudios.mywebcommunity.org/Recursos/WSS_Source/TelemetryPost.php"
                ElseIf UsingServer = "WS3" Then
                    CurrentServerURL = "http://worcomecorporations.000webhostapp.com"
                    URL_KeyAccessToken = "http://worcomecorporations.000webhostapp.com/Source/WSS/KeyAccessToken.WorCODE"
                    URL_AppService = "http://worcomecorporations.000webhostapp.com/Source/WSS/WorAppServices"
                    URL_AppUpdate = "http://worcomecorporations.000webhostapp.com/Source/WSS/Updates"
                    URL_AppUpdate_WhatsNew = "http://worcomecorporations.000webhostapp.com/Source/WSS/WhatsNew"
                    URL_AppHelper_Help = "http://worcomecorporations.000webhostapp.com/Source/WSS/AppHelper"
                    URL_AppHelper_About = "http://worcomecorporations.000webhostapp.com/Source/WSS/AppHelper/AboutApps"
                    URL_Support_Post = "http://worcomecorporations.000webhostapp.com/Source/WSS/soporte.php"
                    URL_Telemetry_Post = "http://worcomecorporations.000webhostapp.com/Source/WSS/TelemetryPost.php"
                End If
                Console.WriteLine("[AppService]Ahora estara utilizando el servidor '" & UsingServer & "'")
            End If
            CSS2 = True
        Catch ex As Exception
            Console.WriteLine("[AppService@ServerSwitch:ActionInformation]Error: " & ex.Message)
        End Try
        AppManagerCompatibility(OffLineApp_CCS, SecureMode_CCS, AppManager_CCS, SignRegistry_CCS, AppServiceStatus_CCS)
    End Sub

    'dudas si el AppManagerCompatibility es viable a estas alturas. es ambiguo y WorApps tiene mas cosillas
    Sub AppManagerCompatibility(ByVal OffLineApp_AMC As Boolean, ByVal SecureMode_AMC As Boolean, ByVal AppManager_AMC As Boolean, ByVal SignRegistry_AMC As Boolean, ByVal AppServiceStatus_AMC As Boolean)
        If AppManager_AMC = False Then
            Console.WriteLine("[AppService]'AppManagerCompatibility' Omitido")
            SignRegistryStack(OffLineApp_AMC, SecureMode_AMC, SignRegistry_AMC, AppServiceStatus_AMC)
        Else
            Try
                If My.Computer.FileSystem.DirectoryExists(DIRAppManager) = False Then
                    My.Computer.FileSystem.CreateDirectory(DIRAppManager)
                ElseIf My.Computer.FileSystem.DirectoryExists(DIRAppManager) = True Then
                    If My.Computer.FileSystem.FileExists(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE") = False Then
                        My.Computer.FileSystem.WriteAllText(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE",
                                                            "AssemblyName>" & AssemblyNameThis &
                                                            vbCrLf & "ProductName>" & AssemblyProductNameThis &
                                                            vbCrLf & "Description>" & My.Application.Info.Description &
                                                            vbCrLf & "Version>" & AssemblyVersionThis &
                                                            vbCrLf & "Patch>" & DirAppPatch, False)
                        AMC = True
                    ElseIf My.Computer.FileSystem.FileExists(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE") = True Then
                        My.Computer.FileSystem.DeleteFile(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE")
                        My.Computer.FileSystem.WriteAllText(DIRAppManager & "\" & AssemblyNameThis & ".WorCODE",
                                                            "AssemblyName>" & AssemblyNameThis &
                                                            vbCrLf & "ProductName>" & AssemblyProductNameThis &
                                                            vbCrLf & "Description>" & My.Application.Info.Description &
                                                            vbCrLf & "Version>" & AssemblyVersionThis &
                                                            vbCrLf & "Patch>" & DirAppPatch, False)
                        AMC = True
                    End If
                End If
            Catch ex As Exception
                Console.WriteLine("[AppService][AppManager Compatibility]Error: " & ex.Message)
            End Try
            SignRegistryStack(OffLineApp_AMC, SecureMode_AMC, SignRegistry_AMC, AppServiceStatus_AMC)
        End If
    End Sub

    Sub SignRegistryStack(ByVal OffLineApp_SRS As Boolean, ByVal SecureMode_SRS As Boolean, ByVal SignRegistry_SRS As Boolean, ByVal AppServiceStatus_SRS As Boolean)
        If SignRegistry_SRS = False Then
            Console.WriteLine("[AppService]'SignRegistryStack' Omitido")
        Else
            Try
                Try
                    If AppRegistry Is Nothing Then
                        Registry.CurrentUser.CreateSubKey("Software\\Worcome_Studios\\" & AssemblyNameThis)
                        AppRegistry = Registry.CurrentUser.OpenSubKey("Software\\Worcome_Studios\\" & AssemblyNameThis, True)
                    End If
                Catch
                    Console.WriteLine("[AppService@SignRegistryStack]Error al generar llave de registro.")
                End Try
                Try
                    Registry_Assembly = AppRegistry.GetValue("Assembly")
                    Registry_Version = AppRegistry.GetValue("Version")
                    Registry_InstalledDate = AppRegistry.GetValue("Installed Date")
                    Registry_LastStart = AppRegistry.GetValue("Last Start")
                    Registry_Directory = AppRegistry.GetValue("Directory")
                    Registry_AllUsersCanUse = AppRegistry.GetValue("AllUsersCanUse")
                    Try
                        If Registry_Assembly = AssemblyNameThis Then
                            Dim versionApp = New Version(AssemblyVersionThis)
                            Dim versionReg = New Version(Registry_Version)
                            Dim result = versionApp.CompareTo(versionReg)
                            If (result > 0) Then 'Sobre-actualizado App > Reg
                                If ShowAppServiceMessages = False Then
                                    MsgBox("An inferior version was registered", MsgBoxStyle.Information, "Worcome Security")
                                End If
                                AppRegistry.SetValue("Version", AssemblyVersionThis, RegistryValueKind.String)
                            ElseIf (result < 0) Then 'Desactualizado App < Reg
                                If ShowAppServiceMessages = False Then
                                    MsgBox("A higher version was registered", MsgBoxStyle.Information, "Worcome Security")
                                End If
                                AppRegistry.SetValue("Version", AssemblyVersionThis, RegistryValueKind.String)
                            End If
                            'ver si esta disponible para todos
                            'quiza esto no tiene sentido, ya que se escribe en un usuario actual y no de forma global (Registry.CurrentUser es para el usuario logeado (?))
                            Dim AllUserCanUse As String() = Registry_AllUsersCanUse.Split(":")
                            If AllUserCanUse(0) = "False" Then
                                If AllUserCanUse(1) IsNot Environment.UserName Then
                                    MsgBox("The Software was not registered for the current user", MsgBoxStyle.Critical, "Worcome Security")
                                    End
                                    Exit Sub
                                End If
                            End If
                        End If
                        AppRegistry.SetValue("Assembly Path", Application.ExecutablePath, RegistryValueKind.String)
                        AppRegistry.SetValue("Last Start", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), RegistryValueKind.String)
                        AppRegistry.SetValue("Description", My.Application.Info.Description, RegistryValueKind.String)
                        AppRegistry.SetValue("Compilated", Application.ProductVersion, RegistryValueKind.String)
                    Catch
                        MsgBox("The installation log for this app could not be read", MsgBoxStyle.Critical, "Worcome Security")
                    End Try
                Catch
                    'If ShowAppServiceMessages = False Then
                    '    MsgBox("This Software is not installed", MsgBoxStyle.Information, "Worcome Security")
                    'End If
                End Try
            Catch
            End Try
        End If
        AppServiceStatusStack(OffLineApp_SRS, SecureMode_SRS, AppServiceStatus_SRS)
    End Sub

    Sub AppServiceStatusStack(ByVal OffLineApp_ASS As Boolean, ByVal SecureMode_ASS As Boolean, ByVal AppServiceStatus_ASS As Boolean)
        If My.Computer.FileSystem.FileExists(DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini") Then
            My.Computer.FileSystem.DeleteFile(DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini")
        End If
        If AppServiceStatus_ASS = False Then
            Console.WriteLine("[AppService]'AppServiceStatus' Omitido")
            If OffLineApp_ASS = False Then
                Console.WriteLine("[AppService]Aplicacion en Linea")
                DownloadURIAppService = New Uri(URL_AppService & "/Wor_Services___" & AssemblyProductNameThis & ".WorCODE")
                DownloaderArrayAppService.DownloadFileAsync(DownloadURIAppService, DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini")
            Else
                Console.WriteLine("[AppService]Aplicacion fuera de Linea")
            End If
        Else
            DownloadURIAppService = New Uri(URL_AppService & "/Wor_Services___" & AssemblyProductNameThis & ".WorCODE")
            DownloaderArrayAppService.DownloadFileAsync(DownloadURIAppService, DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini")
        End If
    End Sub

    Private Sub DownloaderArrayAppService_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles DownloaderArrayAppService.DownloadFileCompleted
        ApplyAppService(OfflineApp, SecureMode, AppServiceStatus)
    End Sub

    Sub ApplyAppService(ByVal OffLineApp_AAS As Boolean, ByVal SecureMode_AAS As Boolean, ByVal AppServiceStatus_AAS As Boolean)
        Try
            Dim ServiceFilePath As String = DIRCommons & "\WorAppService_" & AssemblyNameThis & ".ini"
            Assembly_Status = GetIniValue("Assembly", "Status", ServiceFilePath)
            Assembly_Name = GetIniValue("Assembly", "Name", ServiceFilePath)
            Assembly_Version = GetIniValue("Assembly", "Version", ServiceFilePath)
            Runtime_URL = GetIniValue("Runtime", "URL", ServiceFilePath)
            Runtime_MSG = GetIniValue("Runtime", "MSG", ServiceFilePath)
            Runtime_ArgumentLine = GetIniValue("Runtime", "ArgumentLine", ServiceFilePath)
            Runtime_Command = GetIniValue("Runtime", "Command", ServiceFilePath)
            Updates_Critical = GetIniValue("Updates", "Critical", ServiceFilePath)
            Updates_CriticalMessage = GetIniValue("Updates", "CriticalMessage", ServiceFilePath)
            Updates_RAW_Download = GetIniValue("Updates", "RAW_Download", ServiceFilePath)
            Updates_Download = GetIniValue("Updates", "Download", ServiceFilePath)
            Installer_Status = GetIniValue("Installer", "Status", ServiceFilePath)
            Installer_DownloadFrom = GetIniValue("Installer", "DownloadFrom", ServiceFilePath)
            If Assembly_Name = AssemblyNameThis = True Then
                If Runtime_URL = "None" Then
                    Console.WriteLine("[AppService]Sin URL para Ejecutar")
                Else
                    Process.Start(Runtime_URL)
                    Console.WriteLine("[AppService]URL Ejecutada: " & Runtime_URL)
                End If
                If Runtime_MSG = "None" Then
                    Console.WriteLine("[AppService]Sin Mensajes para Ejecutar")
                Else
                    MsgBox(Runtime_MSG, MsgBoxStyle.Information, "Worcome Security")
                    Console.WriteLine("[AppService]Mensaje Ejecutado: " & Runtime_MSG)
                End If
                If Runtime_ArgumentLine = "None" Then
                    Console.WriteLine("[AppService]Sin Argumentos para Iniciar")
                Else
                    Process.Start(DirAppPatch, Runtime_ArgumentLine)
                    Console.WriteLine("[AppService]Argumento Iniciado: " & Runtime_Command)
                End If
                If Runtime_Command = "None" Then
                    Console.WriteLine("[AppService]Sin Comandos para Ejecutar")
                Else
                    Process.Start(Runtime_Command)
                    Console.WriteLine("[AppService]Comando Ejecutado: " & Runtime_Command)
                End If
                If Assembly_Version = "*.*.*.*" Then
                    Console.WriteLine("[AppService]Comprobacion de version omitida.")
                Else
                    Dim vL As String = AssemblyVersionThis
                    Dim vS As String = Assembly_Version
                    Dim version1 = New Version(vL)
                    Dim version2 = New Version(vS)
                    Dim result = version1.CompareTo(version2)
                    If (result > 0) Then
                        'MsgBox("Actualmente está corriendo una versión superior a la del servidor", MsgBoxStyle.Information, "Worcome Security")
                        Console.WriteLine("[AppService]La version actual esta sobre-actualizada")
                    ElseIf (result < 0) Then
                        If Updates_Critical = "True" Then
                            Console.WriteLine("[AppService]Actualizacion critica")
                            If Updates_CriticalMessage = "None" Then
                            Else
                                MsgBox(Updates_CriticalMessage, MsgBoxStyle.Information, "Worcome Security")
                                AppUpdate.CheckUpdater()
                                If My.Computer.FileSystem.FileExists(DIRCommons & "\Updater.exe") = True Then
                                    MsgBox("Se iniciara un asistente de actualizacion", MsgBoxStyle.Information, "Worcome Security")
                                    Process.Start(DIRCommons & "\Updater.exe", "/SearchForUpdates -" & AssemblyNameThis & " -" & AssemblyVersionThis & " -" & Application.ExecutablePath)
                                Else
                                    AppUpdate.ShowDialog()
                                End If
                            End If
                            End 'END_PROGRAM
                        Else
                            If Updates_CriticalMessage = "None" Then
                            Else
                                MsgBox(Updates_CriticalMessage, MsgBoxStyle.Information, "Worcome Security")
                            End If
                            Console.WriteLine("[AppService]Hay una nueva version disponible")
                            'MsgBox("Hay una Actualizacion Disponible", MsgBoxStyle.Information, "Worcome Security")
                        End If
                    Else
                        Console.WriteLine("[AppService]La Aplicacion esta Actualizada")
                    End If
                End If
                If Assembly_Status = "Enabled" Then
                    Console.WriteLine("[AppService]Aplicacion en Estado Activa")
                ElseIf Assembly_Status = "Disabled" Then
                    MsgBox("Los servicios de esta aplicación fueron desactivados por Worcome", MsgBoxStyle.Exclamation, "Worcome Security")
                    End 'END_PROGRAM
                ElseIf Assembly_Status = "Waiting" Then
                    MsgBox("Los servicios de esta aplicación están en espera...", MsgBoxStyle.Exclamation, "Worcome Security")
                    End 'END_PROGRAM
                ElseIf Assembly_Status = "Stopped" Then
                    MsgBox("Los servicios de esta aplicación fueron detenidos por Worcome", MsgBoxStyle.Exclamation, "Worcome Security")
                    End 'END_PROGRAM
                Else
                    Console.WriteLine("[AppService]Aplicacion en Estado Indefinida")
                    If SecureMode_AAS = True Then
                        Console.WriteLine("[AppService]Aplicacion en Estado Indefinida, Secure Mode esta Activado")
                        MsgBox("La aplicación está en un estado indefinido" & vbCrLf & "Secure Mode está activo" & vbCrLf & "La aplicación se cerrará", MsgBoxStyle.Critical, "Worcome Security")
                        End 'END_PROGRAM
                    End If
                End If
            End If
            AppServiceConfig.SetValue("Working Server", ServerSwitchURLs(ServerIndex), RegistryValueKind.String)
            AAP = True
        Catch ex As Exception
            Console.WriteLine("[AppService Status]Error: " & ex.Message)
            If SecureMode_AAS = True Then
                If My.Computer.Network.IsAvailable = False Then
                    MsgBox("Esta aplicación necesita acceso a internet para continuar", MsgBoxStyle.Critical, "Worcome Security")
                Else
                    MsgBox("No se pudo conectar a los Servidores de Servicios de Worcome", MsgBoxStyle.Critical, "Worcome Security")
                End If
                End 'END_PROGRAM
            End If
        End Try
    End Sub

    <DllImport("kernel32")>
    Private Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
        'Use GetIniValue("KEY_HERE", "SubKEY_HERE", "filepath")
    End Function

    Public Function GetIniValue(section As String, key As String, filename As String, Optional defaultValue As String = Nothing) As String
        Dim sb As New StringBuilder(500)
        If GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filename) > 0 Then
            Return sb.ToString
        Else
            Return defaultValue
        End If
    End Function
End Module
'Last update 23/12/2020 01:26 PM Chile by ElCris009
'Last update 29/11/2020 01:01 PM Chile by ElCris009
'Last update 23/10/2020 11:25 PM Chile by ElCris009
'Last update 17/09/2020 04:21 PM Argentina by Juako
'Last update 22/08/2020 06:56 PM Chile by ElCris009
'Updated 30/05/2020 09:45 PM Chile by ElCris009