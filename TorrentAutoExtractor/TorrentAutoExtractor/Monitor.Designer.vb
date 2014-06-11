<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Monitor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Error", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Extracting", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Queued", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Other", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Completed", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("*.torrent")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"S##*E##"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("TS, CAM, AC3, xvid, DVDSCR, DVDR, DVDRip, BDRip, 1080p, 720p, PROPER, HDTV, PDTV," & _
                " [REQ], PAL, NTSC")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("{Name} {Year} - {HD}")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("{Name} - {Season} - {Episode} - {Title} - {HD}")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("50000")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(".mkv .m4v .avi .mp4 .flv")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("2")
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"File Extention to Watch for"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem10 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"TV Episode Filters"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem11 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Filters to Stop at When Renaming Files"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem12 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Movie Naming Convention"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem13 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"TV Show Naming Convention"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem14 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Minimum Video File Size (KB)"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem15 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Video File Extensions"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem16 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Max # of files to rename"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem17 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"UNRAR.EXE Location"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem18 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Completed Torrents"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem19 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"MOVIES are Downloaded"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem20 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"MOVIES are Extracted"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem21 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"TV SHOWS are downloaded"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem22 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"TV SHOWS are Extracted"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem23 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"TV SHOWS Unsorted Directory"}, -1, System.Drawing.Color.DodgerBlue, System.Drawing.Color.Empty, Nothing)
        Dim ListViewItem24 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Double Click to set directory"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem25 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Double Click to set directory"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem26 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Double Click to set directory"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem27 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Double Click to set directory"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem28 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Double Click to set directory"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem29 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Double Click to set directory"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem30 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Double Click to set directory"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Monitor))
        Me.btnStop = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.FileSystemWatcher = New System.IO.FileSystemWatcher()
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ProgramContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.StartWithWindowsStartupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowHideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartStopMonitoringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnClearTorLog = New System.Windows.Forms.Button()
        Me.lViewLog = New System.Windows.Forms.ListView()
        Me.Torrent_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Description = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CurrentDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Time = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DateTimeCreated = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Path = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SearchBox = New System.Windows.Forms.RichTextBox()
        Me.RightClkContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PlayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenSourceDirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CompletedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddToQueueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopExtractingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveFromListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartQueuedItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnGroupsTog = New System.Windows.Forms.Button()
        Me.myTabControl = New System.Windows.Forms.TabControl()
        Me.Torrents = New System.Windows.Forms.TabPage()
        Me.btnNewTors = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.Settings = New System.Windows.Forms.TabPage()
        Me.ProcessAllChkBox = New System.Windows.Forms.CheckBox()
        Me.OverRideChkBox = New System.Windows.Forms.CheckBox()
        Me.StartMinChkBox = New System.Windows.Forms.CheckBox()
        Me.lViewSetAdv = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lViewAdvSettings = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lViewSettings = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lViewSetDir = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.NotifyChkBox = New System.Windows.Forms.CheckBox()
        Me.AutoChkBox = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tTimer = New System.Windows.Forms.Timer(Me.components)
        Me.CleanUpChkBox = New System.Windows.Forms.CheckBox()
        CType(Me.FileSystemWatcher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProgramContextMenu.SuspendLayout()
        Me.RightClkContextMenu.SuspendLayout()
        Me.myTabControl.SuspendLayout()
        Me.Torrents.SuspendLayout()
        Me.Settings.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStop.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnStop.Location = New System.Drawing.Point(505, 79)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(64, 34)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "Stop Monitoring"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(312, 7)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(41, 13)
        Me.lblSearch.TabIndex = 6
        Me.lblSearch.Text = "Search"
        '
        'FileSystemWatcher
        '
        Me.FileSystemWatcher.EnableRaisingEvents = True
        Me.FileSystemWatcher.SynchronizingObject = Me
        '
        'NotifyIcon
        '
        Me.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon.ContextMenuStrip = Me.ProgramContextMenu
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Visible = True
        '
        'ProgramContextMenu
        '
        Me.ProgramContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartWithWindowsStartupToolStripMenuItem, Me.ShowHideToolStripMenuItem, Me.StartStopMonitoringToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ProgramContextMenu.Name = "ContextMenuStrip1"
        Me.ProgramContextMenu.Size = New System.Drawing.Size(172, 92)
        '
        'StartWithWindowsStartupToolStripMenuItem
        '
        Me.StartWithWindowsStartupToolStripMenuItem.Name = "StartWithWindowsStartupToolStripMenuItem"
        Me.StartWithWindowsStartupToolStripMenuItem.ShowShortcutKeys = False
        Me.StartWithWindowsStartupToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.StartWithWindowsStartupToolStripMenuItem.Text = "Start With Windows"
        '
        'ShowHideToolStripMenuItem
        '
        Me.ShowHideToolStripMenuItem.Name = "ShowHideToolStripMenuItem"
        Me.ShowHideToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ShowHideToolStripMenuItem.Text = "Hide"
        '
        'StartStopMonitoringToolStripMenuItem
        '
        Me.StartStopMonitoringToolStripMenuItem.Name = "StartStopMonitoringToolStripMenuItem"
        Me.StartStopMonitoringToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.StartStopMonitoringToolStripMenuItem.Text = "Start Monitoring"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'btnClearTorLog
        '
        Me.btnClearTorLog.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnClearTorLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearTorLog.ForeColor = System.Drawing.Color.Crimson
        Me.btnClearTorLog.Location = New System.Drawing.Point(398, 6)
        Me.btnClearTorLog.Name = "btnClearTorLog"
        Me.btnClearTorLog.Size = New System.Drawing.Size(101, 17)
        Me.btnClearTorLog.TabIndex = 5
        Me.btnClearTorLog.Text = "Clear Activity Log"
        Me.btnClearTorLog.UseVisualStyleBackColor = True
        '
        'lViewLog
        '
        Me.lViewLog.AllowColumnReorder = True
        Me.lViewLog.AllowDrop = True
        Me.lViewLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lViewLog.AutoArrange = False
        Me.lViewLog.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Torrent_Name, Me.Description, Me.CurrentDate, Me.Time, Me.Status, Me.DateTimeCreated, Me.Path})
        Me.lViewLog.FullRowSelect = True
        Me.lViewLog.GridLines = True
        ListViewGroup1.Header = "Error"
        ListViewGroup1.Name = "Error"
        ListViewGroup2.Header = "Extracting"
        ListViewGroup2.Name = "Extracting"
        ListViewGroup3.Header = "Queued"
        ListViewGroup3.Name = "Queued"
        ListViewGroup4.Header = "Other"
        ListViewGroup4.Name = "Other"
        ListViewGroup5.Header = "Completed"
        ListViewGroup5.Name = "Completed"
        Me.lViewLog.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3, ListViewGroup4, ListViewGroup5})
        Me.lViewLog.HideSelection = False
        Me.lViewLog.Location = New System.Drawing.Point(3, 24)
        Me.lViewLog.Name = "lViewLog"
        Me.lViewLog.Size = New System.Drawing.Size(496, 206)
        Me.lViewLog.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lViewLog.TabIndex = 6
        Me.lViewLog.UseCompatibleStateImageBehavior = False
        Me.lViewLog.View = System.Windows.Forms.View.Details
        '
        'Torrent_Name
        '
        Me.Torrent_Name.Text = "Torrent"
        Me.Torrent_Name.Width = 153
        '
        'Description
        '
        Me.Description.Text = "Description"
        Me.Description.Width = 112
        '
        'CurrentDate
        '
        Me.CurrentDate.Text = "Date"
        Me.CurrentDate.Width = 73
        '
        'Time
        '
        Me.Time.Tag = ""
        Me.Time.Text = "Time"
        Me.Time.Width = 75
        '
        'Status
        '
        Me.Status.Text = "Status"
        Me.Status.Width = 79
        '
        'DateTimeCreated
        '
        Me.DateTimeCreated.Text = "When File was Created"
        Me.DateTimeCreated.Width = 0
        '
        'Path
        '
        Me.Path.Text = "Path of Extracted Files(s)"
        Me.Path.Width = 0
        '
        'SearchBox
        '
        Me.SearchBox.Location = New System.Drawing.Point(145, 5)
        Me.SearchBox.Multiline = False
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.Size = New System.Drawing.Size(167, 18)
        Me.SearchBox.TabIndex = 4
        Me.SearchBox.Text = ""
        '
        'RightClkContextMenu
        '
        Me.RightClkContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlayToolStripMenuItem, Me.OpenSourceDirectoryToolStripMenuItem, Me.ChangeGroupToolStripMenuItem, Me.StopExtractingToolStripMenuItem, Me.RemoveFromListToolStripMenuItem, Me.StartQueuedItemsToolStripMenuItem})
        Me.RightClkContextMenu.Name = "RightClkContextMenu"
        Me.RightClkContextMenu.Size = New System.Drawing.Size(194, 136)
        '
        'PlayToolStripMenuItem
        '
        Me.PlayToolStripMenuItem.Name = "PlayToolStripMenuItem"
        Me.PlayToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.PlayToolStripMenuItem.Text = "Play"
        Me.PlayToolStripMenuItem.Visible = False
        '
        'OpenSourceDirectoryToolStripMenuItem
        '
        Me.OpenSourceDirectoryToolStripMenuItem.Name = "OpenSourceDirectoryToolStripMenuItem"
        Me.OpenSourceDirectoryToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.OpenSourceDirectoryToolStripMenuItem.Text = "Open Source Directory"
        Me.OpenSourceDirectoryToolStripMenuItem.Visible = False
        '
        'ChangeGroupToolStripMenuItem
        '
        Me.ChangeGroupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompletedToolStripMenuItem, Me.ErrorToolStripMenuItem1, Me.AddToQueueToolStripMenuItem})
        Me.ChangeGroupToolStripMenuItem.Name = "ChangeGroupToolStripMenuItem"
        Me.ChangeGroupToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ChangeGroupToolStripMenuItem.Text = "Change Group"
        '
        'CompletedToolStripMenuItem
        '
        Me.CompletedToolStripMenuItem.Name = "CompletedToolStripMenuItem"
        Me.CompletedToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CompletedToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.CompletedToolStripMenuItem.Text = "Completed"
        '
        'ErrorToolStripMenuItem1
        '
        Me.ErrorToolStripMenuItem1.Name = "ErrorToolStripMenuItem1"
        Me.ErrorToolStripMenuItem1.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.ErrorToolStripMenuItem1.Size = New System.Drawing.Size(261, 22)
        Me.ErrorToolStripMenuItem1.Text = "Error"
        '
        'AddToQueueToolStripMenuItem
        '
        Me.AddToQueueToolStripMenuItem.Name = "AddToQueueToolStripMenuItem"
        Me.AddToQueueToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.AddToQueueToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.AddToQueueToolStripMenuItem.Text = "Add back into Queue"
        '
        'StopExtractingToolStripMenuItem
        '
        Me.StopExtractingToolStripMenuItem.Name = "StopExtractingToolStripMenuItem"
        Me.StopExtractingToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.StopExtractingToolStripMenuItem.Text = "Stop Extracting"
        '
        'RemoveFromListToolStripMenuItem
        '
        Me.RemoveFromListToolStripMenuItem.Name = "RemoveFromListToolStripMenuItem"
        Me.RemoveFromListToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.RemoveFromListToolStripMenuItem.Text = "Remove From List"
        '
        'StartQueuedItemsToolStripMenuItem
        '
        Me.StartQueuedItemsToolStripMenuItem.Name = "StartQueuedItemsToolStripMenuItem"
        Me.StartQueuedItemsToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.StartQueuedItemsToolStripMenuItem.Text = "Start Queued Items"
        Me.StartQueuedItemsToolStripMenuItem.Visible = False
        '
        'btnGroupsTog
        '
        Me.btnGroupsTog.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnGroupsTog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGroupsTog.ForeColor = System.Drawing.Color.Crimson
        Me.btnGroupsTog.Location = New System.Drawing.Point(3, 6)
        Me.btnGroupsTog.Name = "btnGroupsTog"
        Me.btnGroupsTog.Size = New System.Drawing.Size(86, 17)
        Me.btnGroupsTog.TabIndex = 3
        Me.btnGroupsTog.Text = "Toggle Groups"
        Me.btnGroupsTog.UseVisualStyleBackColor = True
        '
        'myTabControl
        '
        Me.myTabControl.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.myTabControl.AllowDrop = True
        Me.myTabControl.Controls.Add(Me.Torrents)
        Me.myTabControl.Controls.Add(Me.Settings)
        Me.myTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.myTabControl.Location = New System.Drawing.Point(0, 0)
        Me.myTabControl.Multiline = True
        Me.myTabControl.Name = "myTabControl"
        Me.myTabControl.SelectedIndex = 0
        Me.myTabControl.Size = New System.Drawing.Size(605, 241)
        Me.myTabControl.TabIndex = 8
        Me.myTabControl.Tag = ""
        '
        'Torrents
        '
        Me.Torrents.AllowDrop = True
        Me.Torrents.BackColor = System.Drawing.Color.GhostWhite
        Me.Torrents.Controls.Add(Me.btnNewTors)
        Me.Torrents.Controls.Add(Me.lblSearch)
        Me.Torrents.Controls.Add(Me.lViewLog)
        Me.Torrents.Controls.Add(Me.SearchBox)
        Me.Torrents.Controls.Add(Me.btnStart)
        Me.Torrents.Controls.Add(Me.btnGroupsTog)
        Me.Torrents.Controls.Add(Me.btnStop)
        Me.Torrents.Controls.Add(Me.btnClearTorLog)
        Me.Torrents.Location = New System.Drawing.Point(4, 4)
        Me.Torrents.Name = "Torrents"
        Me.Torrents.Padding = New System.Windows.Forms.Padding(3)
        Me.Torrents.Size = New System.Drawing.Size(578, 233)
        Me.Torrents.TabIndex = 0
        Me.Torrents.Text = "Torrents Monitor"
        '
        'btnNewTors
        '
        Me.btnNewTors.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewTors.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnNewTors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewTors.ForeColor = System.Drawing.Color.Crimson
        Me.btnNewTors.Location = New System.Drawing.Point(500, 199)
        Me.btnNewTors.Name = "btnNewTors"
        Me.btnNewTors.Size = New System.Drawing.Size(78, 28)
        Me.btnNewTors.TabIndex = 2
        Me.btnNewTors.Text = "Look for new Torrents"
        Me.btnNewTors.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStart.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnStart.Location = New System.Drawing.Point(505, 24)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(64, 35)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start Monitoring"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'Settings
        '
        Me.Settings.AutoScroll = True
        Me.Settings.AutoScrollMargin = New System.Drawing.Size(1, 1)
        Me.Settings.Controls.Add(Me.CleanUpChkBox)
        Me.Settings.Controls.Add(Me.ProcessAllChkBox)
        Me.Settings.Controls.Add(Me.OverRideChkBox)
        Me.Settings.Controls.Add(Me.StartMinChkBox)
        Me.Settings.Controls.Add(Me.lViewSetAdv)
        Me.Settings.Controls.Add(Me.lViewAdvSettings)
        Me.Settings.Controls.Add(Me.lViewSettings)
        Me.Settings.Controls.Add(Me.lViewSetDir)
        Me.Settings.Controls.Add(Me.NotifyChkBox)
        Me.Settings.Controls.Add(Me.AutoChkBox)
        Me.Settings.Location = New System.Drawing.Point(4, 4)
        Me.Settings.Name = "Settings"
        Me.Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.Settings.Size = New System.Drawing.Size(578, 233)
        Me.Settings.TabIndex = 1
        Me.Settings.Text = "Settings"
        Me.Settings.UseVisualStyleBackColor = True
        '
        'ProcessAllChkBox
        '
        Me.ProcessAllChkBox.AutoSize = True
        Me.ProcessAllChkBox.Location = New System.Drawing.Point(12, 29)
        Me.ProcessAllChkBox.Name = "ProcessAllChkBox"
        Me.ProcessAllChkBox.Size = New System.Drawing.Size(119, 17)
        Me.ProcessAllChkBox.TabIndex = 18
        Me.ProcessAllChkBox.Text = "Process all Torrents"
        Me.ProcessAllChkBox.UseVisualStyleBackColor = True
        '
        'OverRideChkBox
        '
        Me.OverRideChkBox.AutoSize = True
        Me.OverRideChkBox.Location = New System.Drawing.Point(274, 29)
        Me.OverRideChkBox.Name = "OverRideChkBox"
        Me.OverRideChkBox.Size = New System.Drawing.Size(90, 17)
        Me.OverRideChkBox.TabIndex = 17
        Me.OverRideChkBox.Text = "Override Files"
        Me.OverRideChkBox.UseVisualStyleBackColor = True
        '
        'StartMinChkBox
        '
        Me.StartMinChkBox.AutoSize = True
        Me.StartMinChkBox.Location = New System.Drawing.Point(274, 6)
        Me.StartMinChkBox.Name = "StartMinChkBox"
        Me.StartMinChkBox.Size = New System.Drawing.Size(97, 17)
        Me.StartMinChkBox.TabIndex = 16
        Me.StartMinChkBox.Text = "Start Minimized"
        Me.StartMinChkBox.UseVisualStyleBackColor = True
        '
        'lViewSetAdv
        '
        Me.lViewSetAdv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4})
        Me.lViewSetAdv.FullRowSelect = True
        Me.lViewSetAdv.GridLines = True
        ListViewItem2.ToolTipText = "Seperate Multiple Filters by a Comma"
        ListViewItem3.ToolTipText = "Seperate Multiple Filters by a Comma"
        ListViewItem4.ToolTipText = " {Name} {Year} {HD}"
        ListViewItem5.ToolTipText = "{Name} {Season} {Episode} {Title} {HD}"
        Me.lViewSetAdv.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8})
        Me.lViewSetAdv.LabelEdit = True
        Me.lViewSetAdv.LabelWrap = False
        Me.lViewSetAdv.Location = New System.Drawing.Point(195, 203)
        Me.lViewSetAdv.MultiSelect = False
        Me.lViewSetAdv.Name = "lViewSetAdv"
        Me.lViewSetAdv.ShowGroups = False
        Me.lViewSetAdv.ShowItemToolTips = True
        Me.lViewSetAdv.Size = New System.Drawing.Size(286, 164)
        Me.lViewSetAdv.TabIndex = 15
        Me.lViewSetAdv.UseCompatibleStateImageBehavior = False
        Me.lViewSetAdv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Click to RESET Adv Settings to DEFAULT"
        Me.ColumnHeader4.Width = 281
        '
        'lViewAdvSettings
        '
        Me.lViewAdvSettings.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3})
        Me.lViewAdvSettings.FullRowSelect = True
        Me.lViewAdvSettings.GridLines = True
        Me.lViewAdvSettings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        ListViewItem9.ToolTipText = "*.(FileExtention)"
        ListViewItem10.ToolTipText = "Seperate Multiple Filters by a Comma, Wild's= * # ? "
        ListViewItem11.ToolTipText = "Seperate Multiple Filters by a Comma"
        ListViewItem12.ToolTipText = "Tags: {Name} {Year} {HD}"
        ListViewItem13.ToolTipText = "Tags: {Name} {Season} {Episode} {Title} {HD}"
        ListViewItem16.ToolTipText = "Incase of Seasons or other combination video packs"
        Me.lViewAdvSettings.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem9, ListViewItem10, ListViewItem11, ListViewItem12, ListViewItem13, ListViewItem14, ListViewItem15, ListViewItem16})
        Me.lViewAdvSettings.Location = New System.Drawing.Point(12, 203)
        Me.lViewAdvSettings.MultiSelect = False
        Me.lViewAdvSettings.Name = "lViewAdvSettings"
        Me.lViewAdvSettings.ShowGroups = False
        Me.lViewAdvSettings.ShowItemToolTips = True
        Me.lViewAdvSettings.Size = New System.Drawing.Size(186, 164)
        Me.lViewAdvSettings.TabIndex = 14
        Me.lViewAdvSettings.TabStop = False
        Me.lViewAdvSettings.UseCompatibleStateImageBehavior = False
        Me.lViewAdvSettings.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Advanced Settings"
        Me.ColumnHeader3.Width = 180
        '
        'lViewSettings
        '
        Me.lViewSettings.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lViewSettings.FullRowSelect = True
        Me.lViewSettings.GridLines = True
        Me.lViewSettings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lViewSettings.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem17, ListViewItem18, ListViewItem19, ListViewItem20, ListViewItem21, ListViewItem22, ListViewItem23})
        Me.lViewSettings.Location = New System.Drawing.Point(12, 49)
        Me.lViewSettings.MultiSelect = False
        Me.lViewSettings.Name = "lViewSettings"
        Me.lViewSettings.Scrollable = False
        Me.lViewSettings.ShowGroups = False
        Me.lViewSettings.Size = New System.Drawing.Size(163, 146)
        Me.lViewSettings.TabIndex = 12
        Me.lViewSettings.TabStop = False
        Me.lViewSettings.UseCompatibleStateImageBehavior = False
        Me.lViewSettings.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Settings"
        Me.ColumnHeader2.Width = 159
        '
        'lViewSetDir
        '
        Me.lViewSetDir.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lViewSetDir.FullRowSelect = True
        Me.lViewSetDir.GridLines = True
        Me.lViewSetDir.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        ListViewItem24.ToolTipText = "Double Click to set directory"
        ListViewItem25.ToolTipText = "Double Click to set directory"
        ListViewItem26.ToolTipText = "Double Click to set directory"
        ListViewItem27.ToolTipText = "Double Click to set directory"
        ListViewItem28.ToolTipText = "Double Click to set directory"
        ListViewItem29.ToolTipText = "Double Click to set directory"
        ListViewItem30.ToolTipText = "Double Click to set directory"
        Me.lViewSetDir.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem24, ListViewItem25, ListViewItem26, ListViewItem27, ListViewItem28, ListViewItem29, ListViewItem30})
        Me.lViewSetDir.LabelEdit = True
        Me.lViewSetDir.LabelWrap = False
        Me.lViewSetDir.Location = New System.Drawing.Point(174, 49)
        Me.lViewSetDir.MultiSelect = False
        Me.lViewSetDir.Name = "lViewSetDir"
        Me.lViewSetDir.Scrollable = False
        Me.lViewSetDir.ShowGroups = False
        Me.lViewSetDir.ShowItemToolTips = True
        Me.lViewSetDir.Size = New System.Drawing.Size(307, 146)
        Me.lViewSetDir.TabIndex = 13
        Me.lViewSetDir.UseCompatibleStateImageBehavior = False
        Me.lViewSetDir.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Set Your Directory Paths"
        Me.ColumnHeader1.Width = 310
        '
        'NotifyChkBox
        '
        Me.NotifyChkBox.AutoSize = True
        Me.NotifyChkBox.Location = New System.Drawing.Point(415, 6)
        Me.NotifyChkBox.Name = "NotifyChkBox"
        Me.NotifyChkBox.Size = New System.Drawing.Size(131, 17)
        Me.NotifyChkBox.TabIndex = 10
        Me.NotifyChkBox.Text = "Windows Notifications"
        Me.NotifyChkBox.UseVisualStyleBackColor = True
        '
        'AutoChkBox
        '
        Me.AutoChkBox.AutoSize = True
        Me.AutoChkBox.Location = New System.Drawing.Point(12, 6)
        Me.AutoChkBox.Name = "AutoChkBox"
        Me.AutoChkBox.Size = New System.Drawing.Size(220, 17)
        Me.AutoChkBox.TabIndex = 9
        Me.AutoChkBox.Text = "Start Monitoring when the program opens"
        Me.AutoChkBox.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel1.Location = New System.Drawing.Point(65, 420)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(10, 10)
        Me.Panel1.TabIndex = 11
        '
        'tTimer
        '
        '
        'CleanUpChkBox
        '
        Me.CleanUpChkBox.AutoSize = True
        Me.CleanUpChkBox.Location = New System.Drawing.Point(415, 29)
        Me.CleanUpChkBox.Name = "CleanUpChkBox"
        Me.CleanUpChkBox.Size = New System.Drawing.Size(133, 17)
        Me.CleanUpChkBox.TabIndex = 19
        Me.CleanUpChkBox.Text = "Cleanup extracted files"
        Me.CleanUpChkBox.UseVisualStyleBackColor = True
        '
        'Monitor
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.ClientSize = New System.Drawing.Size(605, 241)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.myTabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(621, 279)
        Me.Name = "Monitor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Torrent Monitor"
        CType(Me.FileSystemWatcher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProgramContextMenu.ResumeLayout(False)
        Me.RightClkContextMenu.ResumeLayout(False)
        Me.myTabControl.ResumeLayout(False)
        Me.Torrents.ResumeLayout(False)
        Me.Torrents.PerformLayout()
        Me.Settings.ResumeLayout(False)
        Me.Settings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents FileSystemWatcher As System.IO.FileSystemWatcher
    Friend WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents ProgramContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents StartStopMonitoringToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnClearTorLog As System.Windows.Forms.Button
    Friend WithEvents lViewLog As System.Windows.Forms.ListView
    Friend WithEvents Time As System.Windows.Forms.ColumnHeader
    Friend WithEvents Status As System.Windows.Forms.ColumnHeader
    Friend WithEvents Torrent_Name As System.Windows.Forms.ColumnHeader
    Friend WithEvents SearchBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ShowHideToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Description As System.Windows.Forms.ColumnHeader
    Friend WithEvents CurrentDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents RightClkContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents btnGroupsTog As System.Windows.Forms.Button
    Friend WithEvents ChangeGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CompletedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ErrorToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddToQueueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveFromListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents myTabControl As System.Windows.Forms.TabControl
    Friend WithEvents Torrents As System.Windows.Forms.TabPage
    Friend WithEvents Settings As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents NotifyChkBox As System.Windows.Forms.CheckBox
    Friend WithEvents AutoChkBox As System.Windows.Forms.CheckBox
    Friend WithEvents btnNewTors As System.Windows.Forms.Button
    Friend WithEvents DateTimeCreated As System.Windows.Forms.ColumnHeader
    Friend WithEvents lViewSetDir As System.Windows.Forms.ListView
    Friend WithEvents lViewSettings As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lViewSetAdv As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lViewAdvSettings As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents StartQueuedItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartWithWindowsStartupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartMinChkBox As System.Windows.Forms.CheckBox
    Friend WithEvents tTimer As System.Windows.Forms.Timer
    Friend WithEvents StopExtractingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OverRideChkBox As System.Windows.Forms.CheckBox
    Friend WithEvents ProcessAllChkBox As System.Windows.Forms.CheckBox
    Friend WithEvents Path As System.Windows.Forms.ColumnHeader
    Friend WithEvents PlayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenSourceDirectoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CleanUpChkBox As System.Windows.Forms.CheckBox

End Class
