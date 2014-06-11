Imports System
Imports System.IO
Imports System.Globalization
Imports System.Security.Permissions
Imports System.Configuration
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Threading
Imports Microsoft.Win32
Imports IWshRuntimeLibrary

Public Class Monitor
    Public watchfolder As FileSystemWatcher
    Public WithEvents bw As unrarLaunch = New unrarLaunch
    Public myFileQue(100) As String
    Public myFileQueCount As Integer = 0, StatusCol As Integer
    Private lviDraggedItem As ListViewItem
    Private m_SortingColumn As ColumnHeader
    Private CloseAllowed As Boolean = False
    Public MyMsgBox As MsgBoxTimer


    Private Sub Monitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim TMstatus As String
        btnStop.Enabled = False
        NotifyIcon.Text = "Torrent Monitor, NOT active"

        LoadSettingsFile()

        Dim foundItem As ListViewItem = FindLVIteminSubitem(lViewLog, 4, "Queued", True)

        If AutoChkBox.Checked Then
            If checkFolderPaths() Then StartMonitoringFolder()
            If foundItem IsNot Nothing Then
                StartUnrarProcess(foundItem.Text)
                popFrmQue()
            End If
        Else
            If foundItem IsNot Nothing Then
                If Not StartMinChkBox.Checked Then Me.Show()
                MyMsgBox = New MsgBoxTimer
                MyMsgBox.ShowMsg("Parent", "You have items in your Que, would you like to start extracting them?", 10, "yes", "Items in Que")
                If MyMsgBox.MsgAnswer Then
                    StartUnrarProcess(foundItem.Text)
                    popFrmQue()
                Else
                    RightClkContextMenu.Items("StartQueuedItemsToolStripMenuItem").Visible = True
                End If
            End If
        End If

        If StartMinChkBox.Checked Then
            Me.Opacity = 0
            tTimer.Start()
            ProgramContextMenu.Items(1).Text = "Show"
            If btnStart.Enabled Then
                TMstatus = "But is NOT monitoring for torrents"
            Else
                TMstatus = "and is monitoring for torrents"
            End If
            NotifyYou("Torrent monitor is Minimized", TMstatus, 10, True)
        End If

    End Sub

    Private Sub Monitor_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim foundItem As ListViewItem = FindLVIteminSubitem(lViewLog, 4, "Extracting")

        SaveSettingsFile()
        If Not CloseAllowed And e.CloseReason = CloseReason.UserClosing Then
            Me.WindowState = FormWindowState.Minimized
            e.Cancel = True
            Exit Sub
        End If

        If bw.IsWorking And myFileQueCount > 0 Then
            MyMsgBox = New MsgBoxTimer
            MyMsgBox.ShowMsg("Center", "There are " & myFileQueCount.ToString & " Files in Que & one extracting, are you sure you want to exit?", 5, "no", "Files are in QUE & Working!")
            If Not MyMsgBox.MsgAnswer Then e.Cancel = True
        ElseIf bw.IsWorking And myFileQueCount = 0 Then
            MyMsgBox = New MsgBoxTimer
            MyMsgBox.ShowMsg("Center", "There is a file extracting, are you sure you want to exit?", 5, "no", "Files are Extracting!")
            If Not MyMsgBox.MsgAnswer Then e.Cancel = True
        End If

        If e.Cancel = False Then
            Me.Hide()
            bw.StopMe()
            Try
                NotifyIcon.Visible = False
                watchfolder.EnableRaisingEvents = False
            Catch
            End Try
            myFileQueCount = 0
            If foundItem IsNot Nothing Then
                addToLog(foundItem.Text, "Exited before completed", "Error", "", "")
            End If
            SaveSettingsFile()
        End If
    End Sub

    Private Sub LoadSettingsFile()
        Dim tName As String, tDesc As String, tDate As String, tTime As String, tGroup As String, tMade As String, tPath As String
        Dim ColWidths As String()
        Dim eMessage As String = ""
        Dim filename As String = ""

        Try
            lViewSetAdv.Items(0).Text = My.Settings.ext
            lViewSetAdv.Items(1).Text = My.Settings.Filters
            lViewSetAdv.Items(2).Text = My.Settings.RenamingFilters
            lViewSetAdv.Items(3).Text = My.Settings.MovieNaming
            lViewSetAdv.Items(4).Text = My.Settings.TVNaming
            lViewSetAdv.Items(5).Text = My.Settings.FileIgnoreSize
            lViewSetAdv.Items(6).Text = My.Settings.FileExtensions
            lViewSetAdv.Items(7).Text = My.Settings.MaxRename
        Catch ex As ConfigurationErrorsException
            filename = ex.Filename
            If filename Is Nothing Then filename = CType(ex.InnerException, ConfigurationErrorsException).Filename
        Catch ex1 As Exception
            ResetSettings("Advanced")
            eMessage += "An Error occured when loading the Advanced Settings, they are reset to default." & Chr(13)
        End Try

        Try
            lViewSetDir.Items(0).Text = My.Settings.dirUnRar
            lViewSetDir.Items(1).Text = My.Settings.dirComTor
            lViewSetDir.Items(2).Text = My.Settings.dirMovDl
            lViewSetDir.Items(3).Text = My.Settings.dirMovExt
            lViewSetDir.Items(4).Text = My.Settings.dirTVDl
            lViewSetDir.Items(5).Text = My.Settings.dirTVExt
            lViewSetDir.Items(6).Text = My.Settings.dirTVUnSor
        Catch ex As ConfigurationErrorsException
            filename = ex.Filename
            If filename Is Nothing Then filename = CType(ex.InnerException, ConfigurationErrorsException).Filename
        Catch ex1 As Exception
            ResetSettings("Directory")
            eMessage += "An Error occured when loaded the Directory Paths, please set them again." & Chr(13)
        End Try

        Try
            NotifyChkBox.Checked = My.Settings.NotifyYorN
            AutoChkBox.Checked = My.Settings.AutoStart
            lViewLog.ShowGroups = My.Settings.GroupToggle
            StartWithWindowsStartupToolStripMenuItem.Checked = My.Settings.WinStartUp
            StartMinChkBox.Checked = My.Settings.StartupMin
            OverRideChkBox.Checked = My.Settings.OverRide
            ProcessAllChkBox.Checked = My.Settings.ProcessAllTors
            CleanUpChkBox.Checked = My.Settings.CleanUp
        Catch ex As ConfigurationErrorsException
            filename = ex.Filename
            If filename Is Nothing Then filename = CType(ex.InnerException, ConfigurationErrorsException).Filename
        Catch ex1 As Exception
            ResetSettings("Other")
            eMessage += "An Error occured when loaded the Checkbox settings, they have been reset." & Chr(13)
        End Try
        
        Try
            Me.Size = My.Settings.FormSize
            ColWidths = My.Settings.ColumnSizes.Split(":")
            For tempCol = 0 To lViewLog.Columns.Count - 1
                lViewLog.Columns(tempCol).Width = Convert.ToInt32(ColWidths(tempCol))
            Next
        Catch ex As Exception
            ColWidths = "153:112:73:75:79:0:0".Split(":")
            For tempCol = 0 To lViewLog.Columns.Count - 1
                lViewLog.Columns(tempCol).Width = Convert.ToInt32(ColWidths(tempCol))
            Next
            If lViewLog.ShowGroups = True Then
                StatusCol = lViewLog.Columns(4).Width()
                lViewLog.Columns(4).Width() = 0
            End If
            Me.Size = New Size(621, 279)
        End Try

        Try
            If My.Settings.LVSName IsNot Nothing Then
                For z = 0 To My.Settings.LVSName.Count - 1
                    tName = My.Settings.LVSName(z)
                    tDesc = My.Settings.LVSDesc(z)
                    tDate = My.Settings.LVSDate(z)
                    tTime = My.Settings.LVSTime(z)
                    tGroup = My.Settings.LVSGroup(z)
                    tMade = My.Settings.LVSMade(z)
                    tPath = My.Settings.LVSPath(z)
                    addToLog(tName, tDesc, tGroup, tMade, tPath, tDate, tTime)
                Next z
            End If
        Catch ex As ConfigurationErrorsException
            filename = ex.Filename
            If filename Is Nothing Then filename = CType(ex.InnerException, ConfigurationErrorsException).Filename
        Catch ex1 As Exception
            eMessage += "And Error occured trying to load the log files" & Chr(13)
        End Try

        If filename IsNot "" Then
            If RestoreBackUpConfig(filename) Then
                MsgBox("Your settings file has become corrupt, however it was restored with a backup of the last time the program opened successfully." _
                       & Chr(13) & Chr(13) & "Click OK to restart the program to load the new settings.")
                Application.Restart()
                Process.GetCurrentProcess().Kill()
            Else
                MsgBox("An error occured! Your user settings file has become corrupted, this may be due to a crash or improper exiting of the program." _
                   & Chr(13) & Chr(13) & "Your must be reset to DEFAULT, click ok to restart the program.")
                System.IO.File.Delete(filename)
                Application.Restart()
                Process.GetCurrentProcess().Kill()
            End If
        ElseIf eMessage IsNot "" Then
            MsgBox(eMessage)
        Else
            BackUpConfigFile()
        End If
    End Sub

    Private Sub SaveSettingsFile()
        Dim LVSName As New StringCollection
        Dim LVSDesc As New StringCollection
        Dim LVSDate As New StringCollection
        Dim LVSTime As New StringCollection
        Dim LVSGroup As New StringCollection
        Dim LVSMade As New StringCollection
        Dim LVSPath As New StringCollection
        Dim ColumnWidths As String = ""

        Try
            For Each item As ListViewItem In lViewLog.Items
                LVSName.Add(item.SubItems(0).Text)
                LVSDesc.Add(item.SubItems(1).Text)
                LVSDate.Add(item.SubItems(2).Text)
                LVSTime.Add(item.SubItems(3).Text)
                LVSGroup.Add(item.SubItems(4).Text)
                LVSMade.Add(item.SubItems(5).Text)
                LVSPath.Add(item.SubItems(6).Text)
            Next

            My.Settings.LVSName = LVSName
            My.Settings.LVSDesc = LVSDesc
            My.Settings.LVSDate = LVSDate
            My.Settings.LVSTime = LVSTime
            My.Settings.LVSGroup = LVSGroup
            My.Settings.LVSMade = LVSMade
            My.Settings.LVSPath = LVSPath
            My.Settings.ext = lViewSetAdv.Items(0).Text
            My.Settings.Filters = lViewSetAdv.Items(1).Text
            My.Settings.RenamingFilters = lViewSetAdv.Items(2).Text
            My.Settings.MovieNaming = lViewSetAdv.Items(3).Text
            My.Settings.TVNaming = lViewSetAdv.Items(4).Text
            My.Settings.FileIgnoreSize = lViewSetAdv.Items(5).Text
            My.Settings.FileExtensions = lViewSetAdv.Items(6).Text
            My.Settings.MaxRename = lViewSetAdv.Items(7).Text
            My.Settings.dirUnRar = lViewSetDir.Items(0).Text
            My.Settings.dirComTor = lViewSetDir.Items(1).Text
            My.Settings.dirMovDl = lViewSetDir.Items(2).Text
            My.Settings.dirMovExt = lViewSetDir.Items(3).Text
            My.Settings.dirTVDl = lViewSetDir.Items(4).Text
            My.Settings.dirTVExt = lViewSetDir.Items(5).Text
            My.Settings.dirTVUnSor = lViewSetDir.Items(6).Text
            My.Settings.NotifyYorN = NotifyChkBox.Checked
            My.Settings.AutoStart = AutoChkBox.Checked
            My.Settings.GroupToggle = lViewLog.ShowGroups
            My.Settings.WinStartUp = StartWithWindowsStartupToolStripMenuItem.Checked
            My.Settings.StartupMin = StartMinChkBox.Checked
            My.Settings.OverRide = OverRideChkBox.Checked
            My.Settings.ProcessAllTors = ProcessAllChkBox.Checked
            My.Settings.CleanUp = CleanUpChkBox.Checked

            My.Settings.FormSize = Me.Size

            For tempCol = 0 To lViewLog.Columns.Count - 1
                ColumnWidths += lViewLog.Columns(tempCol).Width.ToString & ":"
            Next

            My.Settings.ColumnSizes = ColumnWidths

            My.Settings.Save()
        Catch ex As ConfigurationErrorsException

        End Try
    End Sub

    Private Sub BackUpConfigFile()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal)
        Dim FullPathName As String = config.FilePath
        Dim FolderPath As String = FullPathName.Remove(FullPathName.LastIndexOf("\"))
        Dim fName As String = FullPathName.Remove(0, FullPathName.LastIndexOf("\"))
        Try
            If System.IO.File.Exists(FullPathName) Then
                System.IO.File.Copy(FullPathName, FolderPath & "\" & fName & ".Backup", True)
            End If
        Catch ex As Exception
        End Try
        
    End Sub

    Private Function RestoreBackUpConfig(ByVal FullPathName As String) As Boolean
        Dim DidItRestore As Boolean = False
        Try
            Dim fName As String = FullPathName.Remove(0, FullPathName.LastIndexOf("\")) & ".Backup"

            If System.IO.File.Exists(FullPathName & ".Backup") Then
                System.IO.File.Copy(FullPathName & ".Backup", FullPathName, True)
                DidItRestore = True
            End If
        Catch ex As Exception
        End Try

        Return DidItRestore
    End Function

    Private Sub ResetSettings(ByVal WhatToReset As String)
        Select Case WhatToReset
            Case "Advanced"
                lViewSetAdv.Items(0).Text = "*.torrent"
                lViewSetAdv.Items(1).Text = "S#?*E#?"
                lViewSetAdv.Items(2).Text = "TS, CAM, AC3, xvid, DVDSCR, DVDR, DVDRip, BDRip, PROPER, HDTV, PDTV, [REQ], PAL, NTSC"
                lViewSetAdv.Items(3).Text = "{Name} {Year} - {HD}"
                lViewSetAdv.Items(4).Text = "{Name} - {Season} - {Episode} - {Title} - {HD}"
                lViewSetAdv.Items(5).Text = "50000"
                lViewSetAdv.Items(6).Text = ".mkv .m4v .avi .mp4 .flv"
                lViewSetAdv.Items(7).Text = "2"
            Case "Directory"
                lViewSetDir.Items(0).Text = "Double Click to set Directory"
                lViewSetDir.Items(1).Text = "Double Click to set Directory"
                lViewSetDir.Items(2).Text = "Double Click to set Directory"
                lViewSetDir.Items(3).Text = "Double Click to set Directory"
                lViewSetDir.Items(4).Text = "Double Click to set Directory"
                lViewSetDir.Items(5).Text = "Double Click to set Directory"
                lViewSetDir.Items(6).Text = "Double Click to set Directory"
            Case "Other"
                NotifyChkBox.Checked = False
                AutoChkBox.Checked = False
                lViewLog.ShowGroups = True
                StartWithWindowsStartupToolStripMenuItem.Checked = False
                StartMinChkBox.Checked = False
                OverRideChkBox.Checked = False
                ProcessAllChkBox.Checked = False
                CleanUpChkBox.Checked = True
        End Select
    End Sub

    Private Sub FiredEvents_FiredAgain(ByVal FiredN As String, ByVal FiredS As String) Handles bw.FireFlag
        Dim ActionString As String, ShortActString As String, tPath As String = ""
        Dim sites As String() = FiredS.Split(":")

        ActionString = ""
        ShortActString = ""
        Select Case sites(0)
            Case "Extracting"
                ActionString = "Extracting new Torrent"
                ShortActString = "Extracting "
            Case "Completed"
                ActionString = "Done Extracting"
                ShortActString = "Done Extracting "
                tPath = sites(1) + ":" + sites(2)
            Case "Error"
                ActionString = "ERROR Extracting"
                ShortActString = "Error Extracting "
            Case "Stopped"
                ActionString = "Extracting Stopped"
                ShortActString = "User Stopped extracting the torrent "
                sites(0) = "Error"
            Case "Other"
                ActionString = "Unsure"
                ShortActString = "Not sure what to do with the torrent"
            Case Else
                ActionString = FiredS
                ShortActString = FiredS & ": "
        End Select

        NotifyYou(ActionString, ShortActString & " " & FiredN, 500)
        If Not lViewLog.FindItemWithText(FiredN, False, 0, False) Is Nothing Then
            addToLog(FiredN, ShortActString, sites(0), "", tPath)
        End If

        If Not FiredS = "Extracting" And Not bw.IsWorking And myFileQueCount > 0 Then
            RunProcessNow(myFileQue(0), ProcessAllChkBox.Checked)
            popFrmQue()
        ElseIf Not FiredS = "Extracting" And bw.IsWorking And myFileQueCount > 0 Then
            Console.WriteLine("Starting New Unrar Thread")
            bw = New unrarLaunch
            RunProcessNow(myFileQue(0), ProcessAllChkBox.Checked)
            popFrmQue()
        End If
    End Sub

    Private Sub logchange(ByVal source As Object, ByVal e As System.IO.FileSystemEventArgs)
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Dim FileName As String, Notifytitle As String, FileMadeTime As String
        Dim dirinfo As DirectoryInfo = New DirectoryInfo(lViewSetDir.Items(1).Text)
        Dim FileCreation() As FileInfo = dirinfo.GetFiles(e.Name)
        Notifytitle = "New Torrent Downloaded"
        FileName = e.Name.Remove(e.Name.LastIndexOf("."))
        FileMadeTime = Format(FileCreation(0).CreationTime, "MM/dd/yyyy h:mm:ss tt")

        NotifyYou(Notifytitle, FileName, 50)
        StartUnrarProcess(FileName, FileMadeTime)
    End Sub

    Private Sub StartUnrarProcess(ByVal FileName As String, Optional ByVal FileMadeTime As String = "")
        Dim foundItem As ListViewItem = Nothing
        Dim ProcessAllTemp As Boolean = ProcessAllChkBox.Checked

        If lViewLog.Items.Count Then foundItem = lViewLog.FindItemWithText(FileName, False, 0, False)
        If (foundItem IsNot Nothing) Then
            If (foundItem.Group.Name = "Other") Then ProcessAllTemp = True
        End If

        addToLog(FileName, "New Download", "Queued", FileMadeTime, "")
        If Not bw.IsWorking Then
            RunProcessNow(FileName, ProcessAllTemp)
        Else
            addToQue(FileName)
        End If
    End Sub

    Private Sub RunProcessNow(ByVal fname As String, ByVal ProcessAllTemp As Boolean)

        bw.startBackgroundTask(fname, lViewSetDir.Items(0).Text, lViewSetDir.Items(2).Text, lViewSetDir.Items(3).Text, _
                               lViewSetDir.Items(4).Text, lViewSetDir.Items(5).Text, lViewSetDir.Items(6).Text, _
                               lViewSetAdv.Items(1).Text, lViewSetAdv.Items(2).Text, lViewSetAdv.Items(3).Text, _
                               lViewSetAdv.Items(4).Text, lViewSetAdv.Items(5).Text, lViewSetAdv.Items(6).Text, _
                               lViewSetAdv.Items(7).Text, OverRideChkBox.Checked, ProcessAllTemp, CleanUpChkBox.Checked)
    End Sub

    Private Sub addToQue(ByVal fName As String)
        Console.WriteLine("Adding " & fName & " To que # " & myFileQueCount)
        myFileQueCount += 1
        myFileQue(myFileQueCount - 1) = fName
    End Sub

    Private Sub popFrmQue(Optional ByVal fname As String = "")
        Dim founditem As Boolean = False
        If myFileQueCount = 0 Then Exit Sub
        If fname = "" Then
            Console.WriteLine("popping First Item: " & myFileQue(0))
            For z = 0 To myFileQueCount
                myFileQue(z) = myFileQue(z + 1)
            Next
            myFileQueCount = myFileQueCount - 1
        Else
            For z = 0 To myFileQueCount
                If myFileQue(z) = fname Then
                    Console.WriteLine("Found Item to Pop " & fname)
                    founditem = True
                    myFileQueCount = myFileQueCount - 1
                End If
                If founditem Then myFileQue(z) = myFileQue(z + 1)
            Next
        End If
    End Sub

    Private Overloads Sub addToLog(ByVal tName As String, ByVal tDesc As String, ByVal tStatus As String, _
                           Optional ByVal timeCreated As String = "", Optional ByVal tPath As String = "", _
                           Optional ByVal oldDate As String = "", Optional ByVal oldTime As String = "")
        Dim errorString1 As String = "*An Error occured adding more items to the Log box*"
        Dim errorString2 As String = "Log was Cleared"
        Dim tDate As String = Format(Now, "MM/dd/yyyy")
        Dim tTime As String = Format(Now, "h:mm:ss tt")
        Dim tCreation As String = "Not Sure"
        Dim foundItem As ListViewItem = Nothing
        If Not timeCreated = "" Then tCreation = timeCreated
        If Not oldDate = "" And Not oldTime = "" Then
            tDate = oldDate
            tTime = oldTime
        End If

        Dim ThisItem As ListViewItem = New ListViewItem(New String() {tName, tDesc, tDate, tTime, tStatus, tCreation, tPath})
        ThisItem.Group = lViewLog.Groups(tStatus)
        Try
            If lViewLog.Items.Count Then foundItem = lViewLog.FindItemWithText(tName, False, 0, False)

            If (foundItem Is Nothing) Then
                lViewLog.Items.Add(ThisItem)
                foundItem = lViewLog.FindItemWithText(tName)
            Else
                ThisItem.SubItems(5) = lViewLog.Items(foundItem.Index).SubItems(5)
                lViewLog.Items(foundItem.Index) = ThisItem
            End If
        Catch ex As Exception
            If lViewLog.Items.Count > 0 Then lViewLog.Items.Clear()
            lViewLog.Items.Add(New ListViewItem(New String() {errorString1, errorString2, Format(Now, "MM/dd/yyyy"), Format(Now, "h:mm:ss tt"), "", "", ""})).Group = lViewLog.Groups("Error")
        End Try

        lViewLog.Refresh()
    End Sub

    Public Sub NotifyYou(ByVal NotifyTitle As String, ByVal NotifyText As String, ByVal NotifyTime As Integer, Optional ByVal OverRide As Boolean = False)
        If NotifyChkBox.Checked = True Or OverRide Then
            NotifyIcon.BalloonTipIcon = ToolTipIcon.Info
            NotifyIcon.BalloonTipTitle = NotifyTitle
            NotifyIcon.BalloonTipText = NotifyText
            NotifyIcon.ShowBalloonTip(NotifyTime)
        End If
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        If checkFolderPaths() Then StartMonitoringFolder()
        If Not bw.IsWorking And myFileQueCount > 0 Then
            StartUnrarProcess(myFileQue(0))
            popFrmQue()
        End If
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        StopMonitoringFolder()
    End Sub

    Private Function checkFolderPaths() As Boolean
        Dim myresult As Boolean = True, AdvResult As Boolean, finalResult As Boolean = True
        Dim msgboxstring As String = ""
        For z = 0 To 6
            If Not (Microsoft.VisualBasic.Right(lViewSetDir.Items(z).Text, 1) = "\") Then lViewSetDir.Items(z).Text += "\"
            If Not FolderExists(lViewSetDir.Items(z).Text) Then
                msgboxstring += "ERROR: " & Chr(34) & lViewSetDir.Items(z).Text & Chr(34) & " is not a valid path." & Chr(13)
                myresult = False
                lViewSetDir.Items(z).ForeColor = Color.Red
            Else
                lViewSetDir.Items(z).ForeColor = Color.Black
            End If
        Next

        AdvResult = checkAdvSettings(msgboxstring)

        If Not myresult Or Not AdvResult Then
            myTabControl.SelectedTab = Settings
            finalResult = False
            MsgBox(msgboxstring)
        End If

        Return finalResult
    End Function

    Private Function checkAdvSettings(ByRef msgboxstring As String) As Boolean
        Dim myresult As Boolean = True, tmpString As String = ""
        Dim fIgnore As Integer

        If Not lViewSetAdv.Items(0).Text.Contains("*.") Or lViewSetAdv.Items(0).Text.Length < 3 Then
            tmpString += "ERROR: File Extention MUST be in the format of " & Chr(34) & "*.(SomeFileExtention)." & Chr(34) & Chr(13)
            lViewSetAdv.Items(0).ForeColor = Color.Red
        Else
            lViewSetAdv.Items(0).ForeColor = Color.Black
        End If

        If lViewSetAdv.Items(1).Text = "" Then
            tmpString += "ERROR: TV Episode Filter MUST contain a filter to differentiate Movies or TV shows" & Chr(13)
            lViewSetAdv.Items(1).ForeColor = Color.Red
        Else
            lViewSetAdv.Items(1).ForeColor = Color.Black
        End If

        If lViewSetAdv.Items(2).Text = "" Then
            tmpString += "ERROR: Filters to Stop at when Renaming MUST have some key words to stop at so the title will be correct" & Chr(13)
            lViewSetAdv.Items(2).ForeColor = Color.Red
        Else
            lViewSetAdv.Items(2).ForeColor = Color.Black
        End If

        If Not lViewSetAdv.Items(3).Text.ToUpper.Contains("{NAME}") Then
            tmpString += "ERROR: Movies Naming Convention MUST include the {Name} tag" & Chr(13)
            lViewSetAdv.Items(3).ForeColor = Color.Red
        Else
            lViewSetAdv.Items(3).ForeColor = Color.Black
        End If

        If Not lViewSetAdv.Items(4).Text.ToUpper.Contains("{NAME}") Then
            tmpString += "ERROR: TV Show Naming Convention MUST include the {Name} tag" & Chr(13)
            lViewSetAdv.Items(4).ForeColor = Color.Red
        Else
            lViewSetAdv.Items(4).ForeColor = Color.Black
        End If

        If lViewSetAdv.Items(5).Text = "" Then lViewSetAdv.Items(5).Text = "0"
        Try
            fIgnore = Convert.ToInt32(lViewSetAdv.Items(5).Text)
            If fIgnore < 0 Or fIgnore > 100000000 Then
                tmpString += "ERROR: Minimum File Size MUST be between 0 and 1000000(1gig)" & Chr(13)
                lViewSetAdv.Items(5).ForeColor = Color.Red
            Else
                lViewSetAdv.Items(5).ForeColor = Color.Black
            End If
        Catch ex As Exception
            lViewSetAdv.Items(5).ForeColor = Color.Red
            tmpString += "ERROR: Minimum File Size MUST be between 0 and 1000000(1gig)" & Chr(13)
        End Try

        If Not lViewSetAdv.Items(6).Text.Contains(".") Or lViewSetAdv.Items(5).Text = "" Then
            tmpString += "ERROR: Video File Extensions MUST include a file extension in the format of '.avi .mp4' etc..." & Chr(13)
            lViewSetAdv.Items(6).ForeColor = Color.Red
        Else
            lViewSetAdv.Items(6).ForeColor = Color.Black
        End If

        If Not tmpString = "" Then
            myresult = False
            msgboxstring += tmpString
        End If

        Return myresult
    End Function

    Private Function BrowseDirectories(ByVal textString As String, ByVal dirStart As String) As String
        Dim theFolderBrowser As New FolderBrowserDialog
        theFolderBrowser.SelectedPath = ""
        theFolderBrowser.Description = textString
        theFolderBrowser.ShowNewFolderButton = False

        If FolderExists(dirStart) Then
            theFolderBrowser.SelectedPath = dirStart
        Else
            theFolderBrowser.SelectedPath = "c:\"
        End If
        theFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer


        If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not (Microsoft.VisualBasic.Right(theFolderBrowser.SelectedPath, 1) = "\") Then theFolderBrowser.SelectedPath += "\"
            Return theFolderBrowser.SelectedPath
        End If

        Return ""
    End Function

    Private Function FolderExists(ByVal sFullPath As String) As Boolean
        Dim myFSO As Object
        myFSO = CreateObject("Scripting.FileSystemObject")
        FolderExists = myFSO.FolderExists(sFullPath)
    End Function

    Private Sub StartMonitoringFolder()
        watchfolder = New System.IO.FileSystemWatcher()
        watchfolder.SynchronizingObject = Me
        watchfolder.Filter = lViewSetAdv.Items(0).Text
        watchfolder.Path = lViewSetDir.Items(1).Text
        watchfolder.NotifyFilter = IO.NotifyFilters.DirectoryName
        watchfolder.NotifyFilter = watchfolder.NotifyFilter Or IO.NotifyFilters.FileName

        AddHandler watchfolder.Created, AddressOf logchange

        'Set this property to true to start watching
        watchfolder.EnableRaisingEvents = True
        NotifyYou("Monitoring", "waiting for new torrents", 10)
        NotifyIcon.Text = "Monitoring for new torrents"
        ButtonsEnableDisable()
    End Sub

    Private Sub StopMonitoringFolder()
        watchfolder.EnableRaisingEvents = False
        NotifyYou("Monitoring HAULTED", "Torrent Monitor, NOT active", 10)
        NotifyIcon.Text = "Torrent Monitor, NOT active"
        ButtonsEnableDisable()
    End Sub

    Private Sub ButtonsEnableDisable()
        If (btnStart.Enabled = True) Then
            btnStart.Enabled = False
            btnStop.Enabled = True
            lViewSetDir.Enabled = False
            lViewSettings.Enabled = False
            lViewSetAdv.Enabled = False
            lViewAdvSettings.Enabled = False
            ProgramContextMenu.Items(2).Text = "Stop Monitoring"
        Else
            btnStart.Enabled = True
            btnStop.Enabled = False
            lViewSettings.Enabled = True
            lViewSetDir.Enabled = True
            lViewSetAdv.Enabled = True
            lViewAdvSettings.Enabled = True
            ProgramContextMenu.Items(2).Text = "Start Monitoring"
        End If
    End Sub

    Private Sub NotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon.MouseDoubleClick
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
            Me.Focus()
            ProgramContextMenu.Items(1).Text = "Hide"
        Else
            Me.WindowState = FormWindowState.Minimized
            System.Threading.Thread.Sleep(200)
            Me.Hide()
            ProgramContextMenu.Items(1).Text = "Show"
        End If
    End Sub

    Private Sub Monitor_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If (FormWindowState.Minimized = WindowState) Then
            System.Threading.Thread.Sleep(200)
            Me.Hide()
            ProgramContextMenu.Items(1).Text = "Show"
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        CloseAllowed = True
        Me.Close()
    End Sub

    Private Sub StartStopMonitoringToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartStopMonitoringToolStripMenuItem.Click
        If (btnStart.Enabled = True) Then
            If Not checkFolderPaths() Then Exit Sub
            StartMonitoringFolder()
        Else
            StopMonitoringFolder()
        End If
    End Sub

    Private Sub ShowHideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideToolStripMenuItem.Click
        If Me.Visible Then
            Me.WindowState = FormWindowState.Minimized
            System.Threading.Thread.Sleep(200)
            Me.Hide()
            ProgramContextMenu.Items(1).Text = "Show"
        Else
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            Me.Focus()
            ProgramContextMenu.Items(1).Text = "Hide"
        End If
    End Sub

    Private Sub RemoveFromListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFromListToolStripMenuItem.Click
        ChangeItems("Remove")
    End Sub

    Private Sub CompletedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompletedToolStripMenuItem.Click
        ChangeItems("Completed")
    End Sub

    Private Sub ErrorToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ErrorToolStripMenuItem1.Click
        ChangeItems("Error")
    End Sub

    Private Sub AddToQueueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToQueueToolStripMenuItem.Click
        ChangeItems("Queued")
    End Sub

    Private Sub ChangeItems(ByVal tStatus As String)
        Dim tName As String, tDesc As String, tTime As String, tDate As String, tMade As String, tpath As String
        For Each item As ListViewItem In lViewLog.SelectedItems
            tName = lViewLog.Items.Item(item.Index).Text
            tDesc = lViewLog.Items.Item(item.Index).SubItems(1).Text
            tDate = lViewLog.Items.Item(item.Index).SubItems(2).Text
            tTime = lViewLog.Items.Item(item.Index).SubItems(3).Text
            tMade = lViewLog.Items.Item(item.Index).SubItems(5).Text
            tpath = lViewLog.Items.Item(item.Index).SubItems(6).Text

            popFrmQue(tName)
            Select Case tStatus
                Case ("Queued")
                    If Not bw.WhoAmI = tName Then StartUnrarProcess(tName, tMade)
                Case ("Remove")
                    If (bw.WhoAmI = tName) Then
                        bw.StopMe()
                    End If
                    item.Remove()
                Case Else
                    addToLog(tName, tDesc, tStatus, tMade, tpath, tDate, tTime)
            End Select
        Next
        lViewLog.Refresh()
    End Sub

    Private Sub btnClearTorLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTorLog.Click
        Dim responce As MsgBoxResult
        responce = MsgBox("Are you sure you want to clear the activity list?", MsgBoxStyle.YesNo, "Clear Activity List?")
        If responce = MsgBoxResult.Yes Then
            If bw.IsWorking Then bw.StopMe()
            lViewLog.Items.Clear()
            RightClkContextMenu.Items("StartQueuedItemsToolStripMenuItem").Visible = False
            RightClkContextMenu.Items("StopExtractingToolStripMenuItem").Visible = False
            myFileQueCount = 0
            myFileQue(0) = ""
        End If
    End Sub

    Private Sub SearchBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchBox.TextChanged
        If lViewLog.Items.Count Then
            Dim foundItem As ListViewItem = FindLVItemWithText(lViewLog, SearchBox.Text)
            lViewLog.MultiSelect = False

            If (foundItem IsNot Nothing) Then
                lViewLog.Items(foundItem.Index).Selected = True
                lViewLog.EnsureVisible(foundItem.Index)
                lViewLog.Refresh()
            Else
                lViewLog.Items(0).Selected = True
                lViewLog.EnsureVisible(0)
                lViewLog.Refresh()
            End If
            lViewLog.MultiSelect = True
        End If
    End Sub

    Private Sub SearchBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SearchBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            If lViewLog.Items.Count Then
                Dim foundItem As ListViewItem = FindLVItemWithText(lViewLog, SearchBox.Text, True)

                lViewLog.MultiSelect = False

                If (foundItem IsNot Nothing) Then
                    lViewLog.Items(foundItem.Index).Selected = True
                    lViewLog.EnsureVisible(foundItem.Index)
                    lViewLog.Refresh()
                Else
                    lViewLog.Items(0).Selected = True
                    lViewLog.EnsureVisible(0)
                    lViewLog.Refresh()
                End If
                lViewLog.MultiSelect = True
            End If
        End If
    End Sub

    Private Function FindLVItemWithText(ByVal objLV As ListView, ByVal SrchTerm As String, Optional ByVal FindNext As Boolean = False) As ListViewItem
        Dim result As ListViewItem = Nothing
        Dim selectedItem As ListViewItem = Nothing
        Dim founditem As Boolean = False, firstItem As ListViewItem = Nothing

        If Not lViewLog.SelectedItems.Count = 0 Then selectedItem = lViewLog.SelectedItems(0)
        ' Loops in the items 
        For Each item As ListViewItem In objLV.Items
            ' Loops in the sub items 
            For Each subitem As ListViewItem.ListViewSubItem In item.SubItems
                ' Test if the search string matches 
                If subitem.Text.ToUpper.Contains(SrchTerm.ToUpper) Then
                    If firstItem Is Nothing Then
                        founditem = True
                        firstItem = item
                    End If
                    If selectedItem IsNot Nothing And FindNext Then
                        If item.Index > selectedItem.Index Then Return item
                    ElseIf Not FindNext Then
                        Return item
                    End If
                End If
            Next
        Next
        If founditem Then result = firstItem
        Return result
    End Function

    Private Function FindLVIteminSubitem(ByVal objLV As ListView, ByVal subCol As Integer, ByVal itemStatus As String, Optional ByVal putInQue As Boolean = False) As ListViewItem
        Dim result As Boolean = False
        Dim FirstItem As ListViewItem = Nothing

        For Each item As ListViewItem In objLV.Items
            If item.SubItems(subCol).Text.ToUpper.Contains(itemStatus.ToUpper) Then
                If putInQue Then addToQue(item.Text)
                If FirstItem Is Nothing Then
                    FirstItem = item
                End If
                result = True
            End If
        Next
        Return FirstItem
    End Function

    Private Sub lViewLog_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lViewLog.ColumnClick
        Dim new_sorting_column As ColumnHeader = lViewLog.Columns(e.Column)
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            sort_order = SortOrder.Ascending
        Else ' See if this is the same column. 
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order. 
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending. 
                sort_order = SortOrder.Ascending
            End If
            ' Remove the old sort indicator. 
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If
        ' Display the new sort order. 
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If
        ' Create a comparer. 
        lViewLog.ListViewItemSorter = New clsListviewSorter(e.Column, sort_order)
        ' Sort. 
        lViewLog.Sort()
        lViewLog.Refresh()
    End Sub

    Private Sub lViewLog_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lViewLog.MouseUp
        Dim tItem As ListViewItem = Nothing
        Dim SCount As Integer = lViewLog.SelectedItems.Count

        If e.Button = Windows.Forms.MouseButtons.Right And SCount > 0 Then
            tItem = lViewLog.SelectedItems(0)
            If bw.IsWorking And SCount = 1 And (bw.WhoAmI = lViewLog.Items(lViewLog.SelectedItems.Item(0).Index).Text) Then
                RightClkContextMenu.Items("StopExtractingToolStripMenuItem").Visible = True
            Else : RightClkContextMenu.Items("StopExtractingToolStripMenuItem").Visible = False
            End If

            If SCount = 1 And tItem.SubItems(6).Text IsNot "" And Microsoft.VisualBasic.Right(tItem.SubItems(6).Text, 1) IsNot "\" Then
                If System.IO.File.Exists(tItem.SubItems(6).Text) Then
                    RightClkContextMenu.Items("PlayToolStripMenuItem").Visible = True
                Else : RightClkContextMenu.Items("PlayToolStripMenuItem").Visible = False
                End If
            Else : RightClkContextMenu.Items("PlayToolStripMenuItem").Visible = False
            End If

            If SCount = 1 Then
                RightClkContextMenu.Items("OpenSourceDirectoryToolStripMenuItem").Visible = True
            Else : RightClkContextMenu.Items("OpenSourceDirectoryToolStripMenuItem").Visible = False
            End If

            Me.RightClkContextMenu.Show(lViewLog, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub btnGroupsTog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroupsTog.Click
        If lViewLog.ShowGroups Then
            lViewLog.ShowGroups = False
            If lViewLog.Columns(4).Width() = 0 Then
                lViewLog.Columns(4).Width() = StatusCol
            End If

            lViewLog.Refresh()
        Else
            lViewLog.ShowGroups = True
            StatusCol = lViewLog.Columns(4).Width()
            lViewLog.Columns(4).Width() = 0
            RefreshGroups()
            lViewLog.Refresh()
        End If
    End Sub

    Private Sub RefreshGroups()
        For z = 0 To lViewLog.Items.Count - 1
            lViewLog.Items(z) = lViewLog.Items.Item(z)
            lViewLog.Items(z).Group = lViewLog.Groups(lViewLog.Items.Item(z).SubItems(4).Text)
        Next
    End Sub

    Private Sub lViewLog_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lViewLog.DoubleClick
        Dim args As String = ""
        Dim tItem As ListViewItem = lViewLog.SelectedItems(0)

        If lViewLog.SelectedItems.Count = 1 And tItem.SubItems(6).Text IsNot "" Then

            If Microsoft.VisualBasic.Right(tItem.SubItems(6).Text, 1) = "\" Then
                If FolderExists(tItem.SubItems(6).Text) Then
                    args = tItem.SubItems(6).Text
                End If
            Else
                If System.IO.File.Exists(tItem.SubItems(6).Text) Then
                    args = tItem.SubItems(6).Text.Remove(tItem.SubItems(6).Text.LastIndexOf("\"))
                End If
            End If

            Dim procstart As New ProcessStartInfo("explorer")
            procstart.Arguments = args
            Process.Start(procstart)
        End If
    End Sub

    Private Sub OpenSourceDirectoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenSourceDirectoryToolStripMenuItem.Click
        Dim args As String = ""

        If lViewLog.SelectedItems.Count = 1 Then
            Dim tItem As ListViewItem = lViewLog.SelectedItems(0)

            If FolderExists(lViewSetDir.Items(2).Text + tItem.Text) Then
                args = lViewSetDir.Items(2).Text + tItem.Text
            ElseIf FolderExists(lViewSetDir.Items(4).Text + tItem.Text) Then
                args = lViewSetDir.Items(4).Text + tItem.Text
            ElseIf WildCmp("S??*E??", tItem.Text) Then
                args = lViewSetDir.Items(2).Text
            Else
                args = lViewSetDir.Items(4).Text
            End If

            Dim procstart As New ProcessStartInfo("explorer")
            procstart.Arguments = args
            Process.Start(procstart)
        End If
    End Sub

    Private Sub PlayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayToolStripMenuItem.Click
        Dim args As String = ""
        Dim tItem As ListViewItem = lViewLog.SelectedItems(0)

        If System.IO.File.Exists(tItem.SubItems(6).Text) Then
            args = tItem.SubItems(6).Text
        End If

        Dim procstart As New ProcessStartInfo("explorer")
        procstart.Arguments = args
        Process.Start(procstart)

    End Sub

    Private Sub btnNewTors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewTors.Click
        If Not checkFolderPaths() Then Exit Sub

        Dim ListNewestDateTime As DateTime = Nothing, tDateTime As DateTime = Nothing
        Dim dirinfo As DirectoryInfo = New DirectoryInfo(lViewSetDir.Items(1).Text)
        Dim allFiles() As FileInfo = dirinfo.GetFiles(lViewSetAdv.Items(0).Text)
        Dim myfiles As StringCollection = New StringCollection, myfileMade As StringCollection = New StringCollection
        Dim newfiles As Integer = 0
        Dim responce As MsgBoxResult

        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            If lViewLog.Items.Count > 0 Then
                For z = 0 To lViewLog.Items.Count - 1
                    tDateTime = lViewLog.Items(z).SubItems(5).Text
                    If DateTime.Compare(tDateTime, ListNewestDateTime) > 0 Then
                        ListNewestDateTime = tDateTime
                    End If
                Next
            End If

            For Each fl As FileInfo In allFiles
                If DateTime.Compare(Format(fl.CreationTime, "MM/dd/yyyy h:mm:ss tt"), ListNewestDateTime) > 0 Then
                    myfiles.Add(fl.Name)
                    myfileMade.Add(Format(fl.CreationTime, "MM/dd/yyyy h:mm:ss tt"))
                    newfiles += 1
                End If
            Next

            If lViewLog.Items.Count = 0 And newfiles > 0 Then
                responce = MsgBox("There are no files in the log, would you like to load all " & newfiles.ToString & " files from the torrent directory?", MsgBoxStyle.YesNo)
                If responce = MsgBoxResult.No Then Exit Sub
            ElseIf newfiles = 0 Then
                MsgBox("There are no new torrents to load")
                Exit Sub
            Else
                responce = MsgBox(newfiles & " New Torrents will be loaded", MsgBoxStyle.OkCancel)
                If responce = MsgBoxResult.Cancel Then Exit Sub
            End If

            For z = 0 To myfiles.Count - 1
                StartUnrarProcess(myfiles(z).Remove(myfiles(z).LastIndexOf(".")), myfileMade(z))
            Next

        Catch ex As Exception
            MsgBox("An unknown error occured! Couldn't check for new files, for now a workaround could be to start monitoring the directory, move the new torrents out that you want loaded and move them back into the completed torrent directory.  Or you have a corrupt file in your list, try clearing the items.  Sorry for the mess.")
        End Try

    End Sub

    Private Function WildCmp(ByVal WildStr As String, ByVal WholeStr As String) As Boolean
        If WildStr = "" Or WholeStr = "" Then Return False
        Dim WildArray() = WildStr.Split(New String() {",", " "}, StringSplitOptions.RemoveEmptyEntries)
        Dim WildS() As Char, WildC As Integer, tmpz As Integer
        Dim WholeS() As Char = CType(WholeStr.ToLower, Char())
        Dim match As Boolean
        Dim State As String 'Exact, Number, Any, AnyRepeat

        For Each item As String In WildArray

            WildS = CType(item.ToLower.TrimStart(" "), Char())
            State = "Exact"
            match = True
            WildC = 0
            tmpz = 0
            For z = 0 To WholeS.Count - 1
                If WildS(WildC) = "*" Then
                    State = "AnyRepeat"
                ElseIf WildS(WildC) = "#" Then
                    State = "Number"
                ElseIf WildS(WildC) = "?" Then
                    State = "Any"
                Else
                    State = "Exact"
                End If
                Select Case State
                    Case "AnyRepeat"
                        If WildC = WildS.Count - 1 Then
                            match = True
                            WildC = WildC + 1
                        Else
                            WildC = WildC + 1
                            While WildS(WildC) = "*" Or WildS(WildC) = "?"
                                If WildS(WildC) = "?" And Not z = WholeS.Count Then
                                    z = z + 1
                                ElseIf WildS(WildC) = "?" Then
                                    Exit For
                                End If
                                WildC = WildC + 1
                                If WildC = WildS.Count Then Return True
                            End While
                            tmpz = z
                            Do While Not tmpz = WholeS.Count - 1 And Not WildS(WildC) = WholeS(tmpz)
                                tmpz = tmpz + 1
                            Loop
                            z = tmpz
                            If Not z = WholeS.Count Then
                                If WildS(WildC) = WholeS(z) Then
                                    match = True
                                    WildC = WildC + 1
                                Else
                                    If match And z > 0 Then z -= 1
                                    match = False
                                    WildC = 0
                                End If
                            Else
                                If match And z > 0 Then z -= 1
                                match = False
                                WildC = 0
                            End If
                        End If
                    Case "Number"
                        If IsNumeric(WholeS(z)) Then
                            WildC = WildC + 1
                            match = True
                        Else
                            If match And z > 0 Then z -= 1
                            match = False
                            WildC = 0
                        End If
                    Case "Any"
                        match = True
                        WildC = WildC + 1
                    Case "Exact"
                        If WildS(WildC) = WholeS(z) Then
                            WildC += 1
                            match = True
                        Else
                            If match And z > 0 Then z -= 1
                            match = False
                            WildC = 0
                        End If
                End Select
                If WildC = WildS.Count And match = True Then
                    Return match
                ElseIf match = False Then
                    WildC = 0
                ElseIf match = True Then
                End If
            Next

        Next
        Return False
    End Function

    Private Sub lViewSetDir_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lViewSetDir.DoubleClick
        If Not lViewSetDir.SelectedItems.Count = 1 Then Exit Sub
        Dim MyFolderPath As String
        Dim tItem As ListViewItem = lViewSetDir.SelectedItems(0)
        If tItem.Index = 7 Then
            lViewSetAdv.Items(0).BeginEdit()
        Else
            MyFolderPath = BrowseDirectories(lViewSettings.Items(tItem.Index).Text, lViewSetDir.Items(tItem.Index).Text)
            If MyFolderPath <> "" Then
                lViewSetDir.Items(tItem.Index).Text = MyFolderPath
            End If
        End If
    End Sub

    Private Sub lViewSettings_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lViewSettings.DoubleClick
        If Not lViewSetDir.SelectedItems.Count = 1 Then Exit Sub
        Dim MyFolderPath As String
        Dim tItem As ListViewItem = lViewSettings.SelectedItems(0)
        If tItem.Index = 7 Then
            lViewSetAdv.Items(0).BeginEdit()
        Else
            MyFolderPath = BrowseDirectories(lViewSettings.Items(tItem.Index).Text, lViewSetDir.Items(tItem.Index).Text)
            If MyFolderPath <> "" Then
                lViewSetDir.Items(tItem.Index).Text = MyFolderPath
            End If
        End If
    End Sub

    Private Sub StartQueuedItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartQueuedItemsToolStripMenuItem.Click
        If Not bw.IsWorking And myFileQueCount > 0 Then
            StartUnrarProcess(myFileQue(0))
            popFrmQue()
        End If
        RightClkContextMenu.Items("StartQueuedItemsToolStripMenuItem").Visible = False
    End Sub

    Private Sub StartWithWindowsStartupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartWithWindowsStartupToolStripMenuItem.Click
        Dim valueName As String = "Torrent Auto Extractor"
        Dim RegPathInLocalMachine As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
        Dim resultStr As String = "", StartupYN As Boolean = StartWithWindowsStartupToolStripMenuItem.Checked = True

        If StartupYN Then
            resultStr = CreateRegValue(RegPathInLocalMachine, valueName, True)
        Else
            resultStr = CreateRegValue(RegPathInLocalMachine, valueName)
        End If

        If resultStr = "" And Not StartupYN Then
            NotifyYou("Starting with Windows", "Added to Registry Startup", 10, True)
            StartWithWindowsStartupToolStripMenuItem.Checked = True
        ElseIf resultStr = "" And StartupYN Then
            NotifyYou("Removed from windows Startup", "Removed Registry Startup Entry", 10, True)
            StartWithWindowsStartupToolStripMenuItem.Checked = False
        Else
            If StartupYN Then
                If CreateShortCut("Torrent Monitor", System.Environment.SpecialFolder.ApplicationData, "\Microsoft\Windows\Start Menu\Programs\Startup\", True) Then
                    NotifyYou("Removed from windows Startup", "Removed from User Profile Startup Folder", 10, True)
                    StartWithWindowsStartupToolStripMenuItem.Checked = False
                Else
                    NotifyYou("ERROR!", "Couldn't remove from startup", 10, True)
                End If
            Else
                If CreateShortCut("Torrent Monitor", System.Environment.SpecialFolder.ApplicationData, "\Microsoft\Windows\Start Menu\Programs\Startup\") Then
                    NotifyYou("Starting with Windows", "Couldn't add to registry, however it was added to the User Profile Startup Folder", 10, True)
                    StartWithWindowsStartupToolStripMenuItem.Checked = True
                Else
                    NotifyYou("ERROR!", "Couldn't add to Registry Startup or User Profile Startup folder", 10, True)
                End If
            End If
        End If

    End Sub

    Private Function CreateRegValue(ByVal PathInLocalMachine As String, ByVal ValueName As String, Optional ByVal RemoveMe As Boolean = False) As String
        Dim foundValue = False
        Try
            Dim regStartUp As RegistryKey = Registry.LocalMachine.OpenSubKey(PathInLocalMachine, True)
            If regStartUp Is Nothing Then
                Throw New Exception("The Registry SubKey provided doesnt exist!")
            End If

            If RemoveMe Then
                For Each AllvalueNames As String In regStartUp.GetValueNames()
                    If AllvalueNames = ValueName Then regStartUp.DeleteValue(ValueName)
                Next
            Else
                regStartUp.SetValue("TorrentAutoExtractor", Application.ExecutablePath.ToString())
                For Each allValueNames As String In regStartUp.GetValueNames()
                    If allValueNames = ValueName Then foundValue = True
                Next
                If Not foundValue Then Throw New Exception("Couldn't add Registry Value to the startup")
            End If
            regStartUp.Close()
        Catch ex As Exception
            Return ex.Message
        End Try
        Return ""
    End Function

    Private Function CreateShortCut(ByVal shortcutName As String, ByVal SpecialFld As System.Environment.SpecialFolder, ByVal MorePath As String, Optional ByVal RemoveMe As Boolean = False) As Boolean
        Try
            If RemoveMe Then
                Dim fileToDelete = System.Environment.GetFolderPath(SpecialFld) & MorePath & shortcutName & ".lnk"
                If System.IO.File.Exists(fileToDelete) Then System.IO.File.Delete(fileToDelete)
                Return True
            Else
                Dim oShell As IWshRuntimeLibrary.WshShell
                Dim shortCut As IWshRuntimeLibrary.IWshShortcut

                oShell = New IWshRuntimeLibrary.WshShell
                shortCut = oShell.CreateShortcut(System.Environment.GetFolderPath(SpecialFld) & MorePath & shortcutName & ".lnk")
                With shortCut
                    .TargetPath = Application.ExecutablePath.ToString()
                    .WindowStyle = 1
                    .Description = shortcutName
                    .WorkingDirectory = Application.ExecutablePath.ToString()
                    .IconLocation = Application.ExecutablePath.ToString()
                    .Save()
                End With
                Return True
            End If
        Catch ex As System.Exception
            Return False
        End Try
    End Function

    Private Sub tTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tTimer.Tick
        Me.WindowState = FormWindowState.Minimized
        Me.Hide()
        Me.Opacity = 1
        tTimer.Stop()
    End Sub

    Private Sub StopExtractingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopExtractingToolStripMenuItem.Click
        If bw.IsWorking And lViewLog.SelectedItems.Count = 1 Then
            If (bw.WhoAmI = lViewLog.Items(lViewLog.SelectedItems.Item(0).Index).Text) Then
                bw.StopMe()
            End If
        End If
    End Sub

    Private Sub lViewSetAdv_ItemSelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lViewSetAdv.ItemSelectionChanged
        'lViewSetAdv.Items(0).Selected = False
    End Sub

    Private Sub lViewSetAdv_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lViewSetAdv.ColumnClick
        MyMsgBox = New MsgBoxTimer
        MyMsgBox.ShowMsg("Parent", "Are you sure you want to reset the Advanced Settings to the DEFAULTS?", 0, "no", "Reset Adv Settings")
        If MyMsgBox.MsgAnswer Then ResetSettings("Advanced")
    End Sub

    

End Class
