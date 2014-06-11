Imports System.ComponentModel
Imports System.Threading
Imports System.IO

Public Class unrarLaunch
    Public Event FireFlag(ByVal FiredName As String, ByVal FiredStatus As String)
    Private WithEvents BackgroundWorker1 As New System.ComponentModel.BackgroundWorker
    Private EndedAt As String, StartedAt As String, fName As String, Status As String, RenamingFilters As String
    Private UnRarDir As String, MovDwn As String, MovExt As String, TVDwn As String, TVExt As String, TVUn As String
    Private isRunning As Boolean = False, Notifieonend As Boolean = True, ExitIfNotTrue As Boolean = True, VidExtList As String
    Private fileCount As Integer, fileName(100) As String, fileReName(100) As String, sourceDir As String, FilterStr As String
    Private MovieNaming As String, TVNaming As String, fSizeIgnore As Integer, TVDirName As String, TVSeason As Integer
    Private MaxFiles As Integer, OverRide As Boolean, CheckAllTors As Boolean, FilePathName As String, TVorMovie As String
    Private CleanUpFiles As Boolean

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim p As New ProcessStartInfo, myProcess As Process, RarsInDir As Boolean = True
        Console.WriteLine("Unrar Launched for: " & fName)
        If Not (CheckAllTors) And Not (CheckMovieOrTV()) Then
            Status = "Other:Not sure what to do"
            Exit Sub
        End If

        If FolderExists(MovDwn & fName) Then
            p.WorkingDirectory = MovDwn & fName & "\"
        ElseIf FolderExists(TVDwn & fName) Then
            p.WorkingDirectory = TVDwn & fName & "\"
        Else
            Status = "Error:Can't Find downloaded Folder"
            Exit Sub
        End If
        FndExtdFiles(p.WorkingDirectory, True)
        If fileCount = 0 Then
            RarsInDir = False
            FndExtdFiles(p.WorkingDirectory, RarsInDir)
        ElseIf RarsInDir Then
            If Not MakDirNow(p.WorkingDirectory & "extracted_files") Then
                Exit Sub
            End If

            p.FileName = UnRarDir & "unrar.exe"
            p.WindowStyle = ProcessWindowStyle.Hidden
            p.Arguments = "e -y *.rar " & Chr(34) & p.WorkingDirectory & "extracted_files" & Chr(34)
            myProcess = Process.Start(p)
            While Not myProcess.HasExited
                If BackgroundWorker1.CancellationPending Then e.Cancel = True : Exit Sub
                Thread.Sleep(300)
            End While

            FndExtdFiles(p.WorkingDirectory & "extracted_files", False)
        End If

        If fileCount = 0 Then
            Status = "Error:No Files to Move"
            Exit Sub
        Else
            RenameFiles()
            ExitIfNotTrue = MoveFilesNow()
            If Not ExitIfNotTrue Then Exit Sub
        End If

        If CleanUpFiles Then DeleteDirectory(p.WorkingDirectory & "extracted_files")

        Status = "Completed:" + FilePathName
    End Sub

    Public Sub startBackgroundTask(ByVal tempfName As String, ByVal URDir As String, ByVal MDwn As String, ByVal MExt As String, _
                ByVal Tdwn As String, ByVal TVExtDir As String, ByVal TVUnDir As String, ByVal TVfilters As String, _
                ByVal NameFilters As String, ByVal MovieNamingConvention As String, ByVal TVNamingConvention As String, _
                ByVal IgnoreThisSize As Integer, ByVal VidFileExt As String, ByVal MaxNumFiles As Integer, _
                ByVal OverRideFiles As Boolean, ByVal CheckTors As Boolean, ByVal CleanUpF As Boolean) ' This will start the backgroundworker
        isRunning = True
        fName = tempfName
        UnRarDir = URDir
        MovDwn = MDwn
        MovExt = MExt
        TVDwn = Tdwn
        TVExt = TVExtDir
        TVUn = TVUnDir
        Status = "Extracting"
        fileCount = 0
        fileName(fileCount) = ""
        FilterStr = TVfilters
        RenamingFilters = NameFilters
        MovieNaming = MovieNamingConvention.ToUpper
        TVNaming = TVNamingConvention.ToUpper
        fSizeIgnore = IgnoreThisSize
        VidExtList = VidFileExt
        MaxFiles = MaxNumFiles
        OverRide = OverRideFiles
        CheckAllTors = CheckTors
        CleanUpFiles = CleanUpF

        BackgroundWorker1.RunWorkerAsync()
        BackgroundWorker1.WorkerSupportsCancellation = True
        RaiseEvent FireFlag(fName, Status)
    End Sub

    Public Sub StopMe()
        Status = "Stopped:Forced Stopped"
        If isRunning Then BackgroundWorker1.CancelAsync()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Dim tName As String = fName
        isRunning = False
        fName = ""
        RaiseEvent FireFlag(tName, Status)
    End Sub

    Public ReadOnly Property IsWorking() As Boolean
        Get
            Return isRunning
        End Get
    End Property

    Public ReadOnly Property WhoAmI() As String
        Get
            Return fName
        End Get
    End Property

    Public Property Notifie_on_end() As Boolean
        Get
            Return Notifieonend
        End Get
        Set(ByVal value As Boolean)
            Notifieonend = value
        End Set
    End Property

    Private Sub DeleteDirectory(ByVal TargetDir As String)
        Try
            Dim tFiles As String() = Directory.GetFiles(TargetDir)
            Dim tDirs As String() = Directory.GetDirectories(TargetDir)

            For Each F In tFiles
                File.SetAttributes(F, FileAttributes.Normal)
                File.Delete(F)
            Next

            For Each D In tDirs
                DeleteDirectory(D)
            Next

            Directory.Delete(TargetDir, False)
            Console.WriteLine("Deleted: " & TargetDir)
        Catch ex As Exception
            Console.WriteLine("Error Deleting: " & TargetDir)
        End Try

    End Sub

    Private Function CheckMovieOrTV() As Boolean
        Dim ProceedOn As Boolean = False
        TVorMovie = ""
        If WildCmp(FilterStr, fName) Then
            TVorMovie = "TV"
            ProceedOn = True
        End If
        If WildCmp("720p, 1080p", fName) Then ProceedOn = True
        If WildCmp(RenamingFilters, fName) Then ProceedOn = True
        Return ProceedOn
    End Function

    Private Function FolderExists(ByVal sFullPath As String) As Boolean
        Dim myFSO As Object
        myFSO = CreateObject("Scripting.FileSystemObject")
        FolderExists = myFSO.FolderExists(sFullPath)
    End Function

    Private Sub FndExtdFiles(ByVal tmpdir As String, ByVal LookforRar As Boolean)
        Dim strFileSize As Integer, FSignore As Integer, strExtList As String = VidExtList
        Dim di As New IO.DirectoryInfo(tmpdir)
        Dim fi As IO.FileInfo
        Dim aryFi As IO.FileInfo() = di.GetFiles("*.*")
        If Not (Right(tmpdir, 1) = "\") Then tmpdir = tmpdir & "\"
        sourceDir = tmpdir
        If LookforRar = True Then
            strExtList = "*.rar"
            FSignore = 1
        Else
            FSignore = fSizeIgnore
            fileCount = 0
        End If

        For Each fi In aryFi
            strFileSize = (Math.Round(fi.Length / 1024))
            If (strFileSize > FSignore And strExtList.Contains(fi.Extension.ToLower)) Then
                fileName(fileCount) = fi.Name
                fileCount += 1
            End If
        Next
    End Sub

    Private Function MakDirNow(ByVal MyDir As String) As Boolean
        Try
            If Not FolderExists(MyDir) Then
                MkDir(MyDir)
            End If
        Catch ex As Exception
            Status = "Error:Making Directory"
            Return False
        End Try
        Return True
    End Function

    Private Function MoveFilesNow() As Boolean
        Dim MoveOrNot As Boolean = True, FinalDir As String = MovExt, FinalName As String = ""
        If Not sourceDir.ToUpper.Contains("extracted_files".ToUpper) Then MoveOrNot = False
        If fileReName(0) = "MoreThan1TVShows" Or fileCount > MaxFiles Then Array.Copy(fileName, fileReName, fileCount)

        If Not TVDirName = "" Then
            If FolderExists(TVExt & TVDirName) Then
                If FolderExists(TVExt & TVDirName & "\Season " & TVSeason) Then
                    FinalDir = TVExt & TVDirName & "\Season " & TVSeason & "\"
                ElseIf FolderExists(TVExt & TVDirName & "\Season 0" & TVSeason) Then
                    FinalDir = TVExt & TVDirName & "\Season 0" & TVSeason & "\"
                Else
                    Try
                        Directory.CreateDirectory(TVExt & TVDirName & "\Season " & TVSeason)
                        FinalDir = TVExt & TVDirName & "\Season " & TVSeason & "\"
                    Catch ex As Exception
                        FinalDir = TVUn
                    End Try
                End If
            Else
                FinalDir = TVUn
                If Not TVNaming.Contains("{SEASON}") Then
                    For z = 0 To fileCount - 1
                        If TVSeason < 10 Then
                            fileReName(z) = fileReName(z).Insert(fileReName(z).LastIndexOfAny("."), " - S0" & TVSeason)
                        Else
                            fileReName(z) = fileReName(z).Insert(fileReName(z).LastIndexOfAny("."), " - S" & TVSeason)
                        End If
                    Next z
                End If
            End If
        End If

        For z = 0 To fileCount - 1
            Try
                If MoveOrNot Then
                    If File.Exists(FinalDir & fileReName(z)) And Not (OverRide) Then
                        If File.Exists(FinalDir & fileName(z)) Then
                            'Deletes source file since the name exists at destination
                            File.Delete(sourceDir & fileName(z))
                        Else
                            'Moves the file as the original name
                            File.Move(sourceDir & fileName(z), FinalDir & fileName(z))

                        End If
                        FinalName = fileName(z)
                    Else
                        'Checks for File at destination directory, if exists it is deleted
                        If File.Exists(FinalDir & fileReName(z)) And OverRide Then
                            File.Delete(FinalDir & fileReName(z))
                        End If
                        'Moves the file as the renamed file
                        File.Move(sourceDir & fileName(z), FinalDir & fileReName(z))
                        FinalName = fileReName(z)
                    End If
                Else
                    If Not File.Exists(FinalDir & fileReName(z)) Or OverRide Then
                        File.Copy(sourceDir & fileName(z), FinalDir & fileReName(z), True)
                        FinalName = fileReName(z)
                    Else
                        File.Copy(sourceDir & fileName(z), FinalDir & fileName(z), True)
                        FinalName = fileName(z)
                    End If
                End If
            Catch ex As Exception
                Status = "Error:Couldn't Move File"
                Return False
            End Try
        Next z

        If fileCount > 1 Then FinalName = ""
        FilePathName = FinalDir & FinalName

        Return True
    End Function

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

    Private Sub RenameFiles()
        Dim fWords() As String, NamingFilters() As String, tPart As String, NewName As String = "", tTitle As String = ""
        Dim StringSeparators() As String = {" ", "."}
        Dim tYear As String = "", tSeason As String = "", tEpisode As String = "", tHD As String = "", tName As String = ""
        Dim txtPos As Integer = 0, cutofftxt As Integer = 9999, tIndex As Integer = 0, fSeason As Integer = 0, fEpisode As Integer = 0

        NamingFilters = RenamingFilters.Replace(",", "").Split(StringSeparators, StringSplitOptions.RemoveEmptyEntries)
        For filt = 0 To NamingFilters.Count - 1
            txtPos = InStr(1, fName, NamingFilters(filt), CompareMethod.Text)
            If txtPos > 0 And cutofftxt > txtPos Then cutofftxt = txtPos
        Next

        If cutofftxt = 9999 Then cutofftxt = fName.Substring(0).Length + 1
        fWords = RTrim(fName.Substring(0, cutofftxt - 1)).Split(StringSeparators, StringSplitOptions.RemoveEmptyEntries)

        For z = 0 To fWords.Count - 1
            tPart = fWords(z)
            If WildCmp("19##, 20##", tPart) Then 'Or WildCmp("20##", tPart) Then 'Renames the Year Portion
                For Each ch As Char In tPart
                    If Char.IsDigit(ch) Then tYear += ch
                Next
            ElseIf WildCmp(FilterStr, tPart) Then 'Renames the Season/Episode Portion to "NAME - S## - ##"
                tIndex = InStr(1, tPart.ToUpper, "S") - 1
                If Char.IsDigit(tPart.Chars(tIndex + 1)) And Char.IsDigit(tPart.Chars(tIndex + 2)) Then
                    fSeason = Convert.ToInt32(tPart.Chars(tIndex + 1) + tPart.Chars(tIndex + 2))
                ElseIf Char.IsDigit(tPart.Chars(tIndex + 1)) Then
                    fSeason = Convert.ToInt32(tPart.Chars(tIndex + 1))
                End If

                tIndex = InStr(1, tPart.ToUpper, "E") - 1
                If Char.IsDigit(tPart.Chars(tIndex + 1)) And Char.IsDigit(tPart.Chars(tIndex + 2)) Then
                    fEpisode = Convert.ToInt32(tPart.Chars(tIndex + 1) & tPart.Chars(tIndex + 2))
                ElseIf Char.IsDigit(tPart.Chars(tIndex + 1)) Then
                    fEpisode = Convert.ToInt32(tPart.Chars(tIndex + 1))
                End If

                If fSeason < 10 Then tSeason = "S0" & fSeason.ToString Else tSeason = "S" & fSeason.ToString
                If fEpisode < 10 Then tEpisode = "0" & fEpisode Else tEpisode = fEpisode
            ElseIf Not (WildCmp("1080p, 720p", tPart)) Then
                tPart = tPart.Substring(0, 1).ToUpper + tPart.Substring(1).ToLower
                If tSeason = "" And tHD = "" And tYear = "" Then
                    If tName = "" Then tName = tPart Else tName += " " + tPart
                ElseIf tHD = "" Then
                    If tTitle = "" Then tTitle = tPart Else tTitle += " " + tPart
                End If
            End If
        Next

        If WildCmp("1080p", fName) Then 'Looks for HD files
            tHD = "HD 1080p"
        ElseIf WildCmp("720p", fName) Then 'Looks for HD files
            tHD = "HD 720p"
        End If

        If TVorMovie = "TV" Then
            NewName = TVNaming.Replace("{NAME}", tName)
            NewName = NewName.Replace("{SEASON}", tSeason)
            NewName = NewName.Replace("{EPISODE}", tEpisode)
            NewName = NewName.Replace("{TITLE}", tTitle)
            NewName = NewName.Replace("{HD}", tHD)
            TVDirName = tName
            TVSeason = fSeason
        Else
            NewName = MovieNaming.Replace("{NAME}", tName)
            NewName = NewName.Replace("{YEAR}", tYear)
            NewName = NewName.Replace("{HD}", tHD)
            TVDirName = ""
            TVSeason = 0
        End If
            NewName = NewName.Replace("  ", "")
            NewName = NewName.Replace("--", "-")

        While NewName.Chars(NewName.Length - 1) = " " Or NewName.Chars(NewName.Length - 1) = "-"
            NewName = NewName.Remove(NewName.Length - 1)
        End While

        If fileCount > 1 And TVorMovie = "TV" Then fileReName(0) = "MoreThan1TVShows" : Exit Sub
        For z = 0 To fileCount - 1
            If z = 0 And fileCount - 1 = z Then
                fileReName(z) = NewName
            ElseIf z = 0 And fileCount - 1 > z Then
                fileReName(z) = NewName + " - Part 1"
            Else
                fileReName(z) = NewName + " - Part " + (z + 1).ToString
            End If
            fileReName(z) += Path.GetExtension(sourceDir + fileName(z))
        Next z
    End Sub
End Class


