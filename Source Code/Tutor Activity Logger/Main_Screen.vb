Imports Microsoft.Win32
Imports System.IO
Imports System.Data.Sqlclient

Public Class Main_Screen
    Inherits System.Windows.Forms.Form

    Dim WithEvents Worker1 As Worker
    Public Delegate Sub WorkerhHandler(ByVal Result As String)
    Public Delegate Sub WorkerProgresshHandler()
    Public Delegate Sub WorkerFileCreatedhHandler()
    Public Delegate Sub WorkerErrorEncountered(ByVal ex As Exception)
    Public Delegate Sub WorkerMessageExtractedhHandler(ByVal str As String)

    Private application_exit As Boolean = False
    Private shutting_down As Boolean = False
    Private splash_loader As Splash_Screen
    Public dataloaded As Boolean = False
    Private testingconnection As Boolean = False
    Private loginaudited As Boolean = False
    Private error_reporting_level

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Worker1 = New Worker
        AddHandler Worker1.WorkerComplete, AddressOf WorkerHandler
        '   AddHandler Worker1.WorkerFolderCount, AddressOf WorkerFolderCountHandler
        'AddHandler Worker1.WorkerFileCount, AddressOf WorkerFileCountHandler
        'AddHandler Worker1.WorkerMessageExtracted, AddressOf WorkerMessageExtractedHandler
        'AddHandler Worker1.WorkerErrorEncountered, AddressOf WorkerErrorEncounteredHandler
    End Sub

    Public Sub New(ByVal splash As Splash_Screen, Optional ByVal currentuser As String = "Unknown User")
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Current_User.Text = currentuser.ToUpper

        splash_loader = splash
        Worker1 = New Worker
        AddHandler Worker1.WorkerComplete, AddressOf WorkerHandler
        'AddHandler Worker1.WorkerFolderCount, AddressOf WorkerFolderCountHandler
        ' AddHandler Worker1.WorkerFileCount, AddressOf WorkerFileCountHandler
        ' AddHandler Worker1.WorkerMessageExtracted, AddressOf WorkerMessageExtractedHandler
        AddHandler Worker1.WorkerErrorEncountered, AddressOf WorkerErrorEncounteredHandler
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents Current_User As System.Windows.Forms.Label
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblYearCalls As System.Windows.Forms.Label
    Friend WithEvents lblMonthCalls As System.Windows.Forms.Label
    Friend WithEvents lblTotalMonthCalls As System.Windows.Forms.Label
    Friend WithEvents lblTotalYearCalls As System.Windows.Forms.Label
    Friend WithEvents YearCalls As System.Windows.Forms.Label
    Friend WithEvents MonthCalls As System.Windows.Forms.Label
    Friend WithEvents TotalYearCalls As System.Windows.Forms.Label
    Friend WithEvents TotalMonthCalls As System.Windows.Forms.Label
    Friend WithEvents lstCustomerType As MTGCComboBox
    Friend WithEvents lstCustomerTypeID As System.Windows.Forms.ComboBox
    Friend WithEvents lstCustomerTypePDText As System.Windows.Forms.ComboBox
    Friend WithEvents PersonalDetailsDescriptor As System.Windows.Forms.Label
    Friend WithEvents lstCustomerTypeCover As System.Windows.Forms.Label
    Friend WithEvents lstProblemTypeCover As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lstProblemType As MTGCComboBox
    Friend WithEvents lstProblemSubTypeCover As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lstProblemSubType As MTGCComboBox
    Friend WithEvents lstProblemTypeID As System.Windows.Forms.ComboBox
    Friend WithEvents lstProblemSubTypeID As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lstResolveStatusCover As System.Windows.Forms.Label
    Friend WithEvents lstResolveStatus As MTGCComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblStats As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents lstResolveTime As System.Windows.Forms.TextBox
    Friend WithEvents lstResolveDate As System.Windows.Forms.TextBox
    Friend WithEvents lstResolveTutor As System.Windows.Forms.TextBox
    Friend WithEvents lstLogTime As System.Windows.Forms.TextBox
    Friend WithEvents lstLogDate As System.Windows.Forms.TextBox
    Friend WithEvents lstLogTutor As System.Windows.Forms.TextBox
    Friend WithEvents lstLogID As System.Windows.Forms.TextBox
    Friend WithEvents txtProblemDuration As System.Windows.Forms.TextBox
    Friend WithEvents txtProblemResolution As System.Windows.Forms.TextBox
    Friend WithEvents txtProblemDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerDetailsSurname As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerDetailsFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerDetailsID As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lstLogModifyTime As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lstLogModifyDate As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lstLogModifyTutor As System.Windows.Forms.TextBox
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar As System.Windows.Forms.Label
    Friend WithEvents ConnectionTester As System.Windows.Forms.Timer
    Friend WithEvents ServerStatus As System.Windows.Forms.Label
    Friend WithEvents ConnectionTesterCounter As System.Windows.Forms.Label
    Friend WithEvents btnNewLog As System.Windows.Forms.Button
    Friend WithEvents btnSaveLog As System.Windows.Forms.Button
    Friend WithEvents lstCallList As System.Windows.Forms.ListBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnMyCalls As System.Windows.Forms.Button
    Friend WithEvents btnAllCalls As System.Windows.Forms.Button
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents ArrowImageList As System.Windows.Forms.ImageList
    Friend WithEvents displayrecordcount As System.Windows.Forms.Label
    Friend WithEvents lblSegment As System.Windows.Forms.Label
    Friend WithEvents lblSegmentNext As System.Windows.Forms.Label
    Friend WithEvents lblSegmentPrevious As System.Windows.Forms.Label
    Friend WithEvents btnAllOpenCalls As System.Windows.Forms.Button
    Friend WithEvents btnMyOpenCalls As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Main_Screen))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Label8 = New System.Windows.Forms.Label
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label9 = New System.Windows.Forms.Label
        Me.ServerStatus = New System.Windows.Forms.Label
        Me.ConnectionTesterCounter = New System.Windows.Forms.Label
        Me.lblSegmentPrevious = New System.Windows.Forms.Label
        Me.lblSegmentNext = New System.Windows.Forms.Label
        Me.lblSegment = New System.Windows.Forms.Label
        Me.displayrecordcount = New System.Windows.Forms.Label
        Me.lstCallList = New System.Windows.Forms.ListBox
        Me.btnNewLog = New System.Windows.Forms.Button
        Me.btnSaveLog = New System.Windows.Forms.Button
        Me.btnAllOpenCalls = New System.Windows.Forms.Button
        Me.btnMyOpenCalls = New System.Windows.Forms.Button
        Me.btnAllCalls = New System.Windows.Forms.Button
        Me.btnMyCalls = New System.Windows.Forms.Button
        Me.Current_User = New System.Windows.Forms.Label
        Me.StatusBar = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox5 = New System.Windows.Forms.PictureBox
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox7 = New System.Windows.Forms.PictureBox
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.lstLogModifyTime = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.lstLogModifyDate = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.lstLogModifyTutor = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.lstLogID = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.lstLogTime = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.lstLogDate = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.lstLogTutor = New System.Windows.Forms.TextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblStats = New System.Windows.Forms.Label
        Me.YearCalls = New System.Windows.Forms.Label
        Me.MonthCalls = New System.Windows.Forms.Label
        Me.TotalYearCalls = New System.Windows.Forms.Label
        Me.TotalMonthCalls = New System.Windows.Forms.Label
        Me.lblYearCalls = New System.Windows.Forms.Label
        Me.lblMonthCalls = New System.Windows.Forms.Label
        Me.lblTotalYearCalls = New System.Windows.Forms.Label
        Me.lblTotalMonthCalls = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.lstResolveTime = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.lstResolveDate = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.lstResolveTutor = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.lstResolveStatusCover = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lstResolveStatus = New MTGCComboBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtProblemDuration = New System.Windows.Forms.TextBox
        Me.lstProblemSubTypeCover = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.lstProblemSubType = New MTGCComboBox
        Me.lstProblemTypeCover = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.lstProblemType = New MTGCComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtProblemResolution = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtProblemDescription = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.lstProblemTypeID = New System.Windows.Forms.ComboBox
        Me.lstProblemSubTypeID = New System.Windows.Forms.ComboBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lstCustomerTypeCover = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtCustomerDetailsSurname = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCustomerDetailsFirstName = New System.Windows.Forms.TextBox
        Me.PersonalDetailsDescriptor = New System.Windows.Forms.Label
        Me.txtCustomerDetailsID = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lstCustomerType = New MTGCComboBox
        Me.lstCustomerTypeID = New System.Windows.Forms.ComboBox
        Me.lstCustomerTypePDText = New System.Windows.Forms.ComboBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.MenuItem8 = New System.Windows.Forms.MenuItem
        Me.Label2 = New System.Windows.Forms.Label
        Me.ConnectionTester = New System.Windows.Forms.Timer(Me.components)
        Me.ArrowImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DimGray
        Me.Label8.Location = New System.Drawing.Point(688, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(152, 16)
        Me.Label8.TabIndex = 33
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label8, "Current System Time")
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenu = Me.ContextMenu1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Resting..."
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Display Main Screen"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "Exit Application"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DimGray
        Me.Label9.Location = New System.Drawing.Point(816, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 16)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "BUILD 20060203.2"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label9, "Application Build Version")
        '
        'ServerStatus
        '
        Me.ServerStatus.BackColor = System.Drawing.Color.Transparent
        Me.ServerStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServerStatus.ForeColor = System.Drawing.Color.DimGray
        Me.ServerStatus.Location = New System.Drawing.Point(688, 16)
        Me.ServerStatus.Name = "ServerStatus"
        Me.ServerStatus.Size = New System.Drawing.Size(224, 16)
        Me.ServerStatus.TabIndex = 72
        Me.ServerStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.ServerStatus, "SQL Server Status")
        '
        'ConnectionTesterCounter
        '
        Me.ConnectionTesterCounter.BackColor = System.Drawing.Color.Transparent
        Me.ConnectionTesterCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConnectionTesterCounter.ForeColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.ConnectionTesterCounter.Location = New System.Drawing.Point(911, 16)
        Me.ConnectionTesterCounter.Name = "ConnectionTesterCounter"
        Me.ConnectionTesterCounter.Size = New System.Drawing.Size(14, 16)
        Me.ConnectionTesterCounter.TabIndex = 73
        Me.ConnectionTesterCounter.Text = "30"
        Me.ConnectionTesterCounter.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.ConnectionTesterCounter, "Countdown to next Server Status Check")
        '
        'lblSegmentPrevious
        '
        Me.lblSegmentPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSegmentPrevious.Location = New System.Drawing.Point(897, 24)
        Me.lblSegmentPrevious.Name = "lblSegmentPrevious"
        Me.lblSegmentPrevious.Size = New System.Drawing.Size(24, 16)
        Me.lblSegmentPrevious.TabIndex = 91
        Me.lblSegmentPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblSegmentPrevious, "Previous Page Number")
        '
        'lblSegmentNext
        '
        Me.lblSegmentNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSegmentNext.Location = New System.Drawing.Point(897, 480)
        Me.lblSegmentNext.Name = "lblSegmentNext"
        Me.lblSegmentNext.Size = New System.Drawing.Size(24, 16)
        Me.lblSegmentNext.TabIndex = 90
        Me.lblSegmentNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblSegmentNext, "Next Page Number")
        '
        'lblSegment
        '
        Me.lblSegment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSegment.Location = New System.Drawing.Point(897, 248)
        Me.lblSegment.Name = "lblSegment"
        Me.lblSegment.Size = New System.Drawing.Size(24, 16)
        Me.lblSegment.TabIndex = 89
        Me.lblSegment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblSegment, "Current Page Number")
        '
        'displayrecordcount
        '
        Me.displayrecordcount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.displayrecordcount.Location = New System.Drawing.Point(808, 504)
        Me.displayrecordcount.Name = "displayrecordcount"
        Me.displayrecordcount.Size = New System.Drawing.Size(88, 16)
        Me.displayrecordcount.TabIndex = 88
        Me.displayrecordcount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.displayrecordcount, "Indicates how many logs are currently being displayed of the total found from the" & _
        " List Request")
        '
        'lstCallList
        '
        Me.lstCallList.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstCallList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstCallList.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstCallList.Location = New System.Drawing.Point(808, 33)
        Me.lstCallList.Name = "lstCallList"
        Me.lstCallList.Size = New System.Drawing.Size(88, 455)
        Me.lstCallList.TabIndex = 84
        Me.lstCallList.TabStop = False
        Me.ToolTip1.SetToolTip(Me.lstCallList, "Recorded Activity Logs. Left-Click on any log to view its details.")
        '
        'btnNewLog
        '
        Me.btnNewLog.BackColor = System.Drawing.Color.AliceBlue
        Me.btnNewLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewLog.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnNewLog.Location = New System.Drawing.Point(680, 496)
        Me.btnNewLog.Name = "btnNewLog"
        Me.btnNewLog.TabIndex = 83
        Me.btnNewLog.Text = "New Log"
        Me.ToolTip1.SetToolTip(Me.btnNewLog, "Create New Activity Log")
        '
        'btnSaveLog
        '
        Me.btnSaveLog.BackColor = System.Drawing.Color.AliceBlue
        Me.btnSaveLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveLog.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnSaveLog.Location = New System.Drawing.Point(600, 496)
        Me.btnSaveLog.Name = "btnSaveLog"
        Me.btnSaveLog.Size = New System.Drawing.Size(72, 23)
        Me.btnSaveLog.TabIndex = 82
        Me.btnSaveLog.Text = "Save Log"
        Me.ToolTip1.SetToolTip(Me.btnSaveLog, "Save Activity Log")
        '
        'btnAllOpenCalls
        '
        Me.btnAllOpenCalls.BackColor = System.Drawing.Color.AliceBlue
        Me.btnAllOpenCalls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllOpenCalls.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllOpenCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnAllOpenCalls.Location = New System.Drawing.Point(280, 88)
        Me.btnAllOpenCalls.Name = "btnAllOpenCalls"
        Me.btnAllOpenCalls.Size = New System.Drawing.Size(48, 18)
        Me.btnAllOpenCalls.TabIndex = 87
        Me.btnAllOpenCalls.TabStop = False
        Me.btnAllOpenCalls.Text = "OPEN"
        Me.ToolTip1.SetToolTip(Me.btnAllOpenCalls, "Show all unresolved calls logged on the system")
        '
        'btnMyOpenCalls
        '
        Me.btnMyOpenCalls.BackColor = System.Drawing.Color.AliceBlue
        Me.btnMyOpenCalls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMyOpenCalls.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMyOpenCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnMyOpenCalls.Location = New System.Drawing.Point(280, 32)
        Me.btnMyOpenCalls.Name = "btnMyOpenCalls"
        Me.btnMyOpenCalls.Size = New System.Drawing.Size(48, 18)
        Me.btnMyOpenCalls.TabIndex = 86
        Me.btnMyOpenCalls.TabStop = False
        Me.btnMyOpenCalls.Text = "OPEN"
        Me.ToolTip1.SetToolTip(Me.btnMyOpenCalls, "List all Unresolved Calls that I've logged")
        '
        'btnAllCalls
        '
        Me.btnAllCalls.BackColor = System.Drawing.Color.AliceBlue
        Me.btnAllCalls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllCalls.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnAllCalls.Location = New System.Drawing.Point(256, 64)
        Me.btnAllCalls.Name = "btnAllCalls"
        Me.btnAllCalls.Size = New System.Drawing.Size(75, 18)
        Me.btnAllCalls.TabIndex = 85
        Me.btnAllCalls.TabStop = False
        Me.btnAllCalls.Text = "ALL CALLS"
        Me.ToolTip1.SetToolTip(Me.btnAllCalls, "Show all calls logged on the system")
        '
        'btnMyCalls
        '
        Me.btnMyCalls.BackColor = System.Drawing.Color.AliceBlue
        Me.btnMyCalls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMyCalls.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMyCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.btnMyCalls.Location = New System.Drawing.Point(256, 8)
        Me.btnMyCalls.Name = "btnMyCalls"
        Me.btnMyCalls.Size = New System.Drawing.Size(75, 18)
        Me.btnMyCalls.TabIndex = 84
        Me.btnMyCalls.TabStop = False
        Me.btnMyCalls.Text = "MY CALLS"
        Me.ToolTip1.SetToolTip(Me.btnMyCalls, "List all of my Calls logged")
        '
        'Current_User
        '
        Me.Current_User.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_User.Location = New System.Drawing.Point(80, 8)
        Me.Current_User.Name = "Current_User"
        Me.Current_User.Size = New System.Drawing.Size(192, 16)
        Me.Current_User.TabIndex = 69
        Me.Current_User.Text = "Unknown User"
        Me.ToolTip1.SetToolTip(Me.Current_User, "User currently logged in")
        '
        'StatusBar
        '
        Me.StatusBar.BackColor = System.Drawing.Color.White
        Me.StatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.ForeColor = System.Drawing.Color.LimeGreen
        Me.StatusBar.Location = New System.Drawing.Point(16, 568)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(624, 16)
        Me.StatusBar.TabIndex = 71
        Me.StatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.StatusBar, "Status Bar")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.LimeGreen
        Me.Label1.Location = New System.Drawing.Point(864, 568)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 16)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "Waiting"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox5
        '
        Me.PictureBox5.BackgroundImage = CType(resources.GetObject("PictureBox5.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(840, 568)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox5.TabIndex = 66
        Me.PictureBox5.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(824, 568)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox4.TabIndex = 65
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(808, 568)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox3.TabIndex = 64
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(792, 568)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 63
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(776, 568)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 62
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SteelBlue
        Me.Panel1.Controls.Add(Me.lblSegmentPrevious)
        Me.Panel1.Controls.Add(Me.lblSegmentNext)
        Me.Panel1.Controls.Add(Me.lblSegment)
        Me.Panel1.Controls.Add(Me.displayrecordcount)
        Me.Panel1.Controls.Add(Me.PictureBox7)
        Me.Panel1.Controls.Add(Me.PictureBox6)
        Me.Panel1.Controls.Add(Me.lstCallList)
        Me.Panel1.Controls.Add(Me.btnNewLog)
        Me.Panel1.Controls.Add(Me.btnSaveLog)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(0, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(968, 528)
        Me.Panel1.TabIndex = 68
        '
        'PictureBox7
        '
        Me.PictureBox7.BackColor = System.Drawing.Color.SteelBlue
        Me.PictureBox7.Location = New System.Drawing.Point(897, 448)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox7.TabIndex = 87
        Me.PictureBox7.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.SteelBlue
        Me.PictureBox6.Location = New System.Drawing.Point(897, 40)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox6.TabIndex = 86
        Me.PictureBox6.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label28)
        Me.GroupBox4.Controls.Add(Me.lstLogModifyTime)
        Me.GroupBox4.Controls.Add(Me.Label29)
        Me.GroupBox4.Controls.Add(Me.lstLogModifyDate)
        Me.GroupBox4.Controls.Add(Me.Label30)
        Me.GroupBox4.Controls.Add(Me.lstLogModifyTutor)
        Me.GroupBox4.Controls.Add(Me.Label27)
        Me.GroupBox4.Controls.Add(Me.lstLogID)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.lstLogTime)
        Me.GroupBox4.Controls.Add(Me.Label25)
        Me.GroupBox4.Controls.Add(Me.lstLogDate)
        Me.GroupBox4.Controls.Add(Me.Label26)
        Me.GroupBox4.Controls.Add(Me.lstLogTutor)
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.AliceBlue
        Me.GroupBox4.Location = New System.Drawing.Point(464, 280)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(296, 208)
        Me.GroupBox4.TabIndex = 81
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Activity Log Idenifiers"
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(16, 168)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(96, 16)
        Me.Label28.TabIndex = 101
        Me.Label28.Text = "Modified Time:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstLogModifyTime
        '
        Me.lstLogModifyTime.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstLogModifyTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstLogModifyTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstLogModifyTime.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstLogModifyTime.Location = New System.Drawing.Point(112, 168)
        Me.lstLogModifyTime.Name = "lstLogModifyTime"
        Me.lstLogModifyTime.ReadOnly = True
        Me.lstLogModifyTime.Size = New System.Drawing.Size(152, 20)
        Me.lstLogModifyTime.TabIndex = 100
        Me.lstLogModifyTime.TabStop = False
        Me.lstLogModifyTime.Text = ""
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(16, 144)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(96, 16)
        Me.Label29.TabIndex = 99
        Me.Label29.Text = "Modified Date:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstLogModifyDate
        '
        Me.lstLogModifyDate.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstLogModifyDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstLogModifyDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstLogModifyDate.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstLogModifyDate.Location = New System.Drawing.Point(112, 144)
        Me.lstLogModifyDate.Name = "lstLogModifyDate"
        Me.lstLogModifyDate.ReadOnly = True
        Me.lstLogModifyDate.Size = New System.Drawing.Size(152, 20)
        Me.lstLogModifyDate.TabIndex = 98
        Me.lstLogModifyDate.TabStop = False
        Me.lstLogModifyDate.Text = ""
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(16, 120)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(96, 16)
        Me.Label30.TabIndex = 97
        Me.Label30.Text = "Modified By:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstLogModifyTutor
        '
        Me.lstLogModifyTutor.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstLogModifyTutor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstLogModifyTutor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstLogModifyTutor.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstLogModifyTutor.Location = New System.Drawing.Point(112, 120)
        Me.lstLogModifyTutor.Name = "lstLogModifyTutor"
        Me.lstLogModifyTutor.ReadOnly = True
        Me.lstLogModifyTutor.Size = New System.Drawing.Size(152, 20)
        Me.lstLogModifyTutor.TabIndex = 96
        Me.lstLogModifyTutor.TabStop = False
        Me.lstLogModifyTutor.Text = ""
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(16, 24)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(96, 16)
        Me.Label27.TabIndex = 95
        Me.Label27.Text = "Log ID:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstLogID
        '
        Me.lstLogID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstLogID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstLogID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstLogID.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstLogID.Location = New System.Drawing.Point(112, 24)
        Me.lstLogID.Name = "lstLogID"
        Me.lstLogID.ReadOnly = True
        Me.lstLogID.Size = New System.Drawing.Size(152, 20)
        Me.lstLogID.TabIndex = 94
        Me.lstLogID.TabStop = False
        Me.lstLogID.Text = "Unassigned"
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(16, 96)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(96, 16)
        Me.Label24.TabIndex = 93
        Me.Label24.Text = "Time Stamp:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstLogTime
        '
        Me.lstLogTime.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstLogTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstLogTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstLogTime.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstLogTime.Location = New System.Drawing.Point(112, 96)
        Me.lstLogTime.Name = "lstLogTime"
        Me.lstLogTime.ReadOnly = True
        Me.lstLogTime.Size = New System.Drawing.Size(152, 20)
        Me.lstLogTime.TabIndex = 92
        Me.lstLogTime.TabStop = False
        Me.lstLogTime.Text = ""
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(16, 72)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(96, 16)
        Me.Label25.TabIndex = 91
        Me.Label25.Text = "Date Stamp:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstLogDate
        '
        Me.lstLogDate.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstLogDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstLogDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstLogDate.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstLogDate.Location = New System.Drawing.Point(112, 72)
        Me.lstLogDate.Name = "lstLogDate"
        Me.lstLogDate.ReadOnly = True
        Me.lstLogDate.Size = New System.Drawing.Size(152, 20)
        Me.lstLogDate.TabIndex = 90
        Me.lstLogDate.TabStop = False
        Me.lstLogDate.Text = ""
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(16, 48)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(96, 16)
        Me.Label26.TabIndex = 89
        Me.Label26.Text = "Tutor Stamp:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstLogTutor
        '
        Me.lstLogTutor.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstLogTutor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstLogTutor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstLogTutor.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstLogTutor.Location = New System.Drawing.Point(112, 48)
        Me.lstLogTutor.Name = "lstLogTutor"
        Me.lstLogTutor.ReadOnly = True
        Me.lstLogTutor.Size = New System.Drawing.Size(152, 20)
        Me.lstLogTutor.TabIndex = 88
        Me.lstLogTutor.TabStop = False
        Me.lstLogTutor.Text = ""
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnAllOpenCalls)
        Me.Panel2.Controls.Add(Me.btnMyOpenCalls)
        Me.Panel2.Controls.Add(Me.btnAllCalls)
        Me.Panel2.Controls.Add(Me.btnMyCalls)
        Me.Panel2.Controls.Add(Me.lblStats)
        Me.Panel2.Controls.Add(Me.YearCalls)
        Me.Panel2.Controls.Add(Me.MonthCalls)
        Me.Panel2.Controls.Add(Me.TotalYearCalls)
        Me.Panel2.Controls.Add(Me.TotalMonthCalls)
        Me.Panel2.Controls.Add(Me.lblYearCalls)
        Me.Panel2.Controls.Add(Me.lblMonthCalls)
        Me.Panel2.Controls.Add(Me.lblTotalYearCalls)
        Me.Panel2.Controls.Add(Me.lblTotalMonthCalls)
        Me.Panel2.Location = New System.Drawing.Point(465, 23)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(432, 114)
        Me.Panel2.TabIndex = 80
        '
        'lblStats
        '
        Me.lblStats.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStats.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblStats.Location = New System.Drawing.Point(8, 8)
        Me.lblStats.Name = "lblStats"
        Me.lblStats.Size = New System.Drawing.Size(224, 16)
        Me.lblStats.TabIndex = 80
        Me.lblStats.Text = "Quick Stats for"
        '
        'YearCalls
        '
        Me.YearCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.YearCalls.Location = New System.Drawing.Point(168, 48)
        Me.YearCalls.Name = "YearCalls"
        Me.YearCalls.Size = New System.Drawing.Size(72, 16)
        Me.YearCalls.TabIndex = 76
        Me.YearCalls.Text = "0"
        '
        'MonthCalls
        '
        Me.MonthCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.MonthCalls.Location = New System.Drawing.Point(168, 32)
        Me.MonthCalls.Name = "MonthCalls"
        Me.MonthCalls.Size = New System.Drawing.Size(72, 16)
        Me.MonthCalls.TabIndex = 77
        Me.MonthCalls.Text = "0"
        '
        'TotalYearCalls
        '
        Me.TotalYearCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.TotalYearCalls.Location = New System.Drawing.Point(168, 88)
        Me.TotalYearCalls.Name = "TotalYearCalls"
        Me.TotalYearCalls.Size = New System.Drawing.Size(72, 16)
        Me.TotalYearCalls.TabIndex = 78
        Me.TotalYearCalls.Text = "0"
        '
        'TotalMonthCalls
        '
        Me.TotalMonthCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.TotalMonthCalls.Location = New System.Drawing.Point(168, 72)
        Me.TotalMonthCalls.Name = "TotalMonthCalls"
        Me.TotalMonthCalls.Size = New System.Drawing.Size(72, 16)
        Me.TotalMonthCalls.TabIndex = 79
        Me.TotalMonthCalls.Text = "0"
        '
        'lblYearCalls
        '
        Me.lblYearCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblYearCalls.Location = New System.Drawing.Point(8, 48)
        Me.lblYearCalls.Name = "lblYearCalls"
        Me.lblYearCalls.Size = New System.Drawing.Size(176, 16)
        Me.lblYearCalls.TabIndex = 72
        Me.lblYearCalls.Text = "User Calls for:"
        '
        'lblMonthCalls
        '
        Me.lblMonthCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblMonthCalls.Location = New System.Drawing.Point(8, 32)
        Me.lblMonthCalls.Name = "lblMonthCalls"
        Me.lblMonthCalls.Size = New System.Drawing.Size(176, 16)
        Me.lblMonthCalls.TabIndex = 73
        Me.lblMonthCalls.Text = "User Calls for:"
        '
        'lblTotalYearCalls
        '
        Me.lblTotalYearCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblTotalYearCalls.Location = New System.Drawing.Point(8, 88)
        Me.lblTotalYearCalls.Name = "lblTotalYearCalls"
        Me.lblTotalYearCalls.Size = New System.Drawing.Size(176, 16)
        Me.lblTotalYearCalls.TabIndex = 74
        Me.lblTotalYearCalls.Text = "Total Calls for:"
        '
        'lblTotalMonthCalls
        '
        Me.lblTotalMonthCalls.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblTotalMonthCalls.Location = New System.Drawing.Point(8, 72)
        Me.lblTotalMonthCalls.Name = "lblTotalMonthCalls"
        Me.lblTotalMonthCalls.Size = New System.Drawing.Size(176, 16)
        Me.lblTotalMonthCalls.TabIndex = 75
        Me.lblTotalMonthCalls.Text = "Total Calls for:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.lstResolveTime)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.lstResolveDate)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.lstResolveTutor)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.lstResolveStatusCover)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.lstResolveStatus)
        Me.GroupBox3.Controls.Add(Me.ComboBox1)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.AliceBlue
        Me.GroupBox3.Location = New System.Drawing.Point(464, 144)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(296, 128)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Resolution Status"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(16, 96)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 16)
        Me.Label19.TabIndex = 93
        Me.Label19.Text = "Time:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstResolveTime
        '
        Me.lstResolveTime.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstResolveTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstResolveTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstResolveTime.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstResolveTime.Location = New System.Drawing.Point(112, 96)
        Me.lstResolveTime.Name = "lstResolveTime"
        Me.lstResolveTime.ReadOnly = True
        Me.lstResolveTime.Size = New System.Drawing.Size(152, 20)
        Me.lstResolveTime.TabIndex = 92
        Me.lstResolveTime.TabStop = False
        Me.lstResolveTime.Text = ""
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(16, 72)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(96, 16)
        Me.Label22.TabIndex = 91
        Me.Label22.Text = "Date:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstResolveDate
        '
        Me.lstResolveDate.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstResolveDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstResolveDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstResolveDate.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstResolveDate.Location = New System.Drawing.Point(112, 72)
        Me.lstResolveDate.Name = "lstResolveDate"
        Me.lstResolveDate.ReadOnly = True
        Me.lstResolveDate.Size = New System.Drawing.Size(152, 20)
        Me.lstResolveDate.TabIndex = 90
        Me.lstResolveDate.TabStop = False
        Me.lstResolveDate.Text = ""
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(16, 48)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(96, 16)
        Me.Label23.TabIndex = 89
        Me.Label23.Text = "Tutor:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstResolveTutor
        '
        Me.lstResolveTutor.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lstResolveTutor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstResolveTutor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstResolveTutor.ForeColor = System.Drawing.Color.AliceBlue
        Me.lstResolveTutor.Location = New System.Drawing.Point(112, 48)
        Me.lstResolveTutor.Name = "lstResolveTutor"
        Me.lstResolveTutor.ReadOnly = True
        Me.lstResolveTutor.Size = New System.Drawing.Size(152, 20)
        Me.lstResolveTutor.TabIndex = 88
        Me.lstResolveTutor.TabStop = False
        Me.lstResolveTutor.Text = ""
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(16, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 16)
        Me.Label21.TabIndex = 87
        Me.Label21.Text = "Call Status:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstResolveStatusCover
        '
        Me.lstResolveStatusCover.BackColor = System.Drawing.Color.AliceBlue
        Me.lstResolveStatusCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstResolveStatusCover.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstResolveStatusCover.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstResolveStatusCover.Location = New System.Drawing.Point(112, 24)
        Me.lstResolveStatusCover.Name = "lstResolveStatusCover"
        Me.lstResolveStatusCover.Size = New System.Drawing.Size(133, 21)
        Me.lstResolveStatusCover.TabIndex = 86
        Me.lstResolveStatusCover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Red
        Me.Label20.Location = New System.Drawing.Point(264, 24)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(16, 16)
        Me.Label20.TabIndex = 84
        Me.Label20.Text = "*"
        '
        'lstResolveStatus
        '
        Me.lstResolveStatus.BackColor = System.Drawing.Color.AliceBlue
        Me.lstResolveStatus.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
        Me.lstResolveStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.lstResolveStatus.ColumnNum = 1
        Me.lstResolveStatus.ColumnWidth = "121"
        Me.lstResolveStatus.DisplayMember = "Text"
        Me.lstResolveStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstResolveStatus.DropDownArrowBackColor = System.Drawing.Color.FromArgb(CType(136, Byte), CType(169, Byte), CType(223, Byte))
        Me.lstResolveStatus.DropDownBackColor = System.Drawing.Color.FromArgb(CType(193, Byte), CType(210, Byte), CType(238, Byte))
        Me.lstResolveStatus.DropDownForeColor = System.Drawing.Color.Black
        Me.lstResolveStatus.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.lstResolveStatus.DropDownWidth = 141
        Me.lstResolveStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstResolveStatus.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstResolveStatus.GridLineColor = System.Drawing.Color.Transparent
        Me.lstResolveStatus.GridLineHorizontal = False
        Me.lstResolveStatus.GridLineVertical = False
        Me.lstResolveStatus.HighlightBorderColor = System.Drawing.Color.Black
        Me.lstResolveStatus.HighlightBorderOnMouseEvents = True
        Me.lstResolveStatus.Items.AddRange(New Object() {""})
        Me.lstResolveStatus.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.lstResolveStatus.Location = New System.Drawing.Point(112, 24)
        Me.lstResolveStatus.ManagingFastMouseMoving = True
        Me.lstResolveStatus.ManagingFastMouseMovingInterval = 30
        Me.lstResolveStatus.Name = "lstResolveStatus"
        Me.lstResolveStatus.NormalBorderColor = System.Drawing.Color.Black
        Me.lstResolveStatus.Size = New System.Drawing.Size(152, 21)
        Me.lstResolveStatus.TabIndex = 83
        Me.lstResolveStatus.TabStop = False
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(168, 24)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(8, 21)
        Me.ComboBox1.TabIndex = 85
        Me.ComboBox1.Text = "ComboBox1"
        Me.ComboBox1.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtProblemDuration)
        Me.GroupBox2.Controls.Add(Me.lstProblemSubTypeCover)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.lstProblemSubType)
        Me.GroupBox2.Controls.Add(Me.lstProblemTypeCover)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.lstProblemType)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtProblemResolution)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtProblemDescription)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.lstProblemTypeID)
        Me.GroupBox2.Controls.Add(Me.lstProblemSubTypeID)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.AliceBlue
        Me.GroupBox2.Location = New System.Drawing.Point(32, 160)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(424, 328)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Activity Log"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(184, 296)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 16)
        Me.Label13.TabIndex = 93
        Me.Label13.Text = "minutes"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 296)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 92
        Me.Label3.Text = "Problem Duration:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtProblemDuration
        '
        Me.txtProblemDuration.BackColor = System.Drawing.Color.AliceBlue
        Me.txtProblemDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProblemDuration.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblemDuration.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtProblemDuration.Location = New System.Drawing.Point(144, 296)
        Me.txtProblemDuration.Name = "txtProblemDuration"
        Me.txtProblemDuration.Size = New System.Drawing.Size(40, 20)
        Me.txtProblemDuration.TabIndex = 91
        Me.txtProblemDuration.Text = ""
        '
        'lstProblemSubTypeCover
        '
        Me.lstProblemSubTypeCover.BackColor = System.Drawing.Color.AliceBlue
        Me.lstProblemSubTypeCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstProblemSubTypeCover.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProblemSubTypeCover.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstProblemSubTypeCover.Location = New System.Drawing.Point(144, 48)
        Me.lstProblemSubTypeCover.Name = "lstProblemSubTypeCover"
        Me.lstProblemSubTypeCover.Size = New System.Drawing.Size(237, 21)
        Me.lstProblemSubTypeCover.TabIndex = 88
        Me.lstProblemSubTypeCover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(400, 48)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(16, 16)
        Me.Label16.TabIndex = 87
        Me.Label16.Text = "*"
        '
        'lstProblemSubType
        '
        Me.lstProblemSubType.BackColor = System.Drawing.Color.AliceBlue
        Me.lstProblemSubType.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
        Me.lstProblemSubType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.lstProblemSubType.ColumnNum = 1
        Me.lstProblemSubType.ColumnWidth = "121"
        Me.lstProblemSubType.DisplayMember = "Text"
        Me.lstProblemSubType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstProblemSubType.DropDownArrowBackColor = System.Drawing.Color.FromArgb(CType(136, Byte), CType(169, Byte), CType(223, Byte))
        Me.lstProblemSubType.DropDownBackColor = System.Drawing.Color.FromArgb(CType(193, Byte), CType(210, Byte), CType(238, Byte))
        Me.lstProblemSubType.DropDownForeColor = System.Drawing.Color.Black
        Me.lstProblemSubType.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.lstProblemSubType.DropDownWidth = 141
        Me.lstProblemSubType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProblemSubType.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstProblemSubType.GridLineColor = System.Drawing.Color.Transparent
        Me.lstProblemSubType.GridLineHorizontal = False
        Me.lstProblemSubType.GridLineVertical = False
        Me.lstProblemSubType.HighlightBorderColor = System.Drawing.Color.Black
        Me.lstProblemSubType.HighlightBorderOnMouseEvents = True
        Me.lstProblemSubType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.lstProblemSubType.Location = New System.Drawing.Point(144, 48)
        Me.lstProblemSubType.ManagingFastMouseMoving = True
        Me.lstProblemSubType.ManagingFastMouseMovingInterval = 30
        Me.lstProblemSubType.Name = "lstProblemSubType"
        Me.lstProblemSubType.NormalBorderColor = System.Drawing.Color.Black
        Me.lstProblemSubType.Size = New System.Drawing.Size(256, 21)
        Me.lstProblemSubType.TabIndex = 86
        Me.lstProblemSubType.TabStop = False
        '
        'lstProblemTypeCover
        '
        Me.lstProblemTypeCover.BackColor = System.Drawing.Color.AliceBlue
        Me.lstProblemTypeCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstProblemTypeCover.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProblemTypeCover.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstProblemTypeCover.Location = New System.Drawing.Point(144, 24)
        Me.lstProblemTypeCover.Name = "lstProblemTypeCover"
        Me.lstProblemTypeCover.Size = New System.Drawing.Size(237, 21)
        Me.lstProblemTypeCover.TabIndex = 85
        Me.lstProblemTypeCover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(400, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(16, 16)
        Me.Label18.TabIndex = 84
        Me.Label18.Text = "*"
        '
        'lstProblemType
        '
        Me.lstProblemType.BackColor = System.Drawing.Color.AliceBlue
        Me.lstProblemType.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
        Me.lstProblemType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.lstProblemType.ColumnNum = 1
        Me.lstProblemType.ColumnWidth = "121"
        Me.lstProblemType.DisplayMember = "Text"
        Me.lstProblemType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstProblemType.DropDownArrowBackColor = System.Drawing.Color.FromArgb(CType(136, Byte), CType(169, Byte), CType(223, Byte))
        Me.lstProblemType.DropDownBackColor = System.Drawing.Color.FromArgb(CType(193, Byte), CType(210, Byte), CType(238, Byte))
        Me.lstProblemType.DropDownForeColor = System.Drawing.Color.Black
        Me.lstProblemType.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.lstProblemType.DropDownWidth = 141
        Me.lstProblemType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProblemType.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstProblemType.GridLineColor = System.Drawing.Color.Transparent
        Me.lstProblemType.GridLineHorizontal = False
        Me.lstProblemType.GridLineVertical = False
        Me.lstProblemType.HighlightBorderColor = System.Drawing.Color.Black
        Me.lstProblemType.HighlightBorderOnMouseEvents = True
        Me.lstProblemType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.lstProblemType.Location = New System.Drawing.Point(144, 24)
        Me.lstProblemType.ManagingFastMouseMoving = True
        Me.lstProblemType.ManagingFastMouseMovingInterval = 30
        Me.lstProblemType.Name = "lstProblemType"
        Me.lstProblemType.NormalBorderColor = System.Drawing.Color.Black
        Me.lstProblemType.Size = New System.Drawing.Size(256, 21)
        Me.lstProblemType.TabIndex = 83
        Me.lstProblemType.TabStop = False
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(16, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(104, 16)
        Me.Label17.TabIndex = 18
        Me.Label17.Text = "Problem Sub Type:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(16, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(88, 16)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "Problem Type:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(400, 184)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(16, 16)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "*"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(16, 184)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 16)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Problem Resolution:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtProblemResolution
        '
        Me.txtProblemResolution.BackColor = System.Drawing.Color.AliceBlue
        Me.txtProblemResolution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProblemResolution.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblemResolution.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtProblemResolution.Location = New System.Drawing.Point(144, 184)
        Me.txtProblemResolution.Multiline = True
        Me.txtProblemResolution.Name = "txtProblemResolution"
        Me.txtProblemResolution.Size = New System.Drawing.Size(256, 108)
        Me.txtProblemResolution.TabIndex = 11
        Me.txtProblemResolution.Text = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(400, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(16, 16)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "*"
        '
        'txtProblemDescription
        '
        Me.txtProblemDescription.BackColor = System.Drawing.Color.AliceBlue
        Me.txtProblemDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProblemDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblemDescription.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtProblemDescription.Location = New System.Drawing.Point(144, 72)
        Me.txtProblemDescription.Multiline = True
        Me.txtProblemDescription.Name = "txtProblemDescription"
        Me.txtProblemDescription.Size = New System.Drawing.Size(256, 108)
        Me.txtProblemDescription.TabIndex = 0
        Me.txtProblemDescription.Text = ""
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(16, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 16)
        Me.Label14.TabIndex = 3
        Me.Label14.Text = "Problem Description:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstProblemTypeID
        '
        Me.lstProblemTypeID.Location = New System.Drawing.Point(144, 24)
        Me.lstProblemTypeID.Name = "lstProblemTypeID"
        Me.lstProblemTypeID.Size = New System.Drawing.Size(8, 21)
        Me.lstProblemTypeID.TabIndex = 89
        Me.lstProblemTypeID.Text = "ComboBox1"
        Me.lstProblemTypeID.Visible = False
        '
        'lstProblemSubTypeID
        '
        Me.lstProblemSubTypeID.Location = New System.Drawing.Point(144, 48)
        Me.lstProblemSubTypeID.Name = "lstProblemSubTypeID"
        Me.lstProblemSubTypeID.Size = New System.Drawing.Size(8, 21)
        Me.lstProblemSubTypeID.TabIndex = 90
        Me.lstProblemSubTypeID.Text = "ComboBox1"
        Me.lstProblemSubTypeID.Visible = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Red
        Me.Button2.Enabled = False
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button2.Location = New System.Drawing.Point(376, 312)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(16, 8)
        Me.Button2.TabIndex = 126
        Me.Button2.TabStop = False
        Me.Button2.Text = "Button2"
        Me.Button2.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstCustomerTypeCover)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtCustomerDetailsSurname)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtCustomerDetailsFirstName)
        Me.GroupBox1.Controls.Add(Me.PersonalDetailsDescriptor)
        Me.GroupBox1.Controls.Add(Me.txtCustomerDetailsID)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lstCustomerType)
        Me.GroupBox1.Controls.Add(Me.lstCustomerTypeID)
        Me.GroupBox1.Controls.Add(Me.lstCustomerTypePDText)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.AliceBlue
        Me.GroupBox1.Location = New System.Drawing.Point(32, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(424, 136)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Customer Details"
        '
        'lstCustomerTypeCover
        '
        Me.lstCustomerTypeCover.BackColor = System.Drawing.Color.AliceBlue
        Me.lstCustomerTypeCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstCustomerTypeCover.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCustomerTypeCover.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstCustomerTypeCover.Location = New System.Drawing.Point(144, 24)
        Me.lstCustomerTypeCover.Name = "lstCustomerTypeCover"
        Me.lstCustomerTypeCover.Size = New System.Drawing.Size(133, 21)
        Me.lstCustomerTypeCover.TabIndex = 1
        Me.lstCustomerTypeCover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(296, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(16, 16)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "*"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 16)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Surname:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCustomerDetailsSurname
        '
        Me.txtCustomerDetailsSurname.BackColor = System.Drawing.Color.AliceBlue
        Me.txtCustomerDetailsSurname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerDetailsSurname.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerDetailsSurname.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtCustomerDetailsSurname.Location = New System.Drawing.Point(144, 96)
        Me.txtCustomerDetailsSurname.Name = "txtCustomerDetailsSurname"
        Me.txtCustomerDetailsSurname.Size = New System.Drawing.Size(256, 20)
        Me.txtCustomerDetailsSurname.TabIndex = 8
        Me.txtCustomerDetailsSurname.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 16)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "First Name:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCustomerDetailsFirstName
        '
        Me.txtCustomerDetailsFirstName.BackColor = System.Drawing.Color.AliceBlue
        Me.txtCustomerDetailsFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerDetailsFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerDetailsFirstName.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtCustomerDetailsFirstName.Location = New System.Drawing.Point(144, 72)
        Me.txtCustomerDetailsFirstName.Name = "txtCustomerDetailsFirstName"
        Me.txtCustomerDetailsFirstName.Size = New System.Drawing.Size(256, 20)
        Me.txtCustomerDetailsFirstName.TabIndex = 6
        Me.txtCustomerDetailsFirstName.Text = ""
        '
        'PersonalDetailsDescriptor
        '
        Me.PersonalDetailsDescriptor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PersonalDetailsDescriptor.Location = New System.Drawing.Point(16, 48)
        Me.PersonalDetailsDescriptor.Name = "PersonalDetailsDescriptor"
        Me.PersonalDetailsDescriptor.Size = New System.Drawing.Size(128, 16)
        Me.PersonalDetailsDescriptor.TabIndex = 5
        Me.PersonalDetailsDescriptor.Text = "Student Number:"
        Me.PersonalDetailsDescriptor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCustomerDetailsID
        '
        Me.txtCustomerDetailsID.BackColor = System.Drawing.Color.AliceBlue
        Me.txtCustomerDetailsID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerDetailsID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerDetailsID.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtCustomerDetailsID.Location = New System.Drawing.Point(144, 48)
        Me.txtCustomerDetailsID.Name = "txtCustomerDetailsID"
        Me.txtCustomerDetailsID.Size = New System.Drawing.Size(256, 20)
        Me.txtCustomerDetailsID.TabIndex = 4
        Me.txtCustomerDetailsID.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Customer Type:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstCustomerType
        '
        Me.lstCustomerType.BackColor = System.Drawing.Color.AliceBlue
        Me.lstCustomerType.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
        Me.lstCustomerType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.lstCustomerType.ColumnNum = 1
        Me.lstCustomerType.ColumnWidth = "121"
        Me.lstCustomerType.DisplayMember = "Text"
        Me.lstCustomerType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstCustomerType.DropDownArrowBackColor = System.Drawing.Color.FromArgb(CType(136, Byte), CType(169, Byte), CType(223, Byte))
        Me.lstCustomerType.DropDownBackColor = System.Drawing.Color.FromArgb(CType(193, Byte), CType(210, Byte), CType(238, Byte))
        Me.lstCustomerType.DropDownForeColor = System.Drawing.Color.Black
        Me.lstCustomerType.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.lstCustomerType.DropDownWidth = 141
        Me.lstCustomerType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCustomerType.ForeColor = System.Drawing.Color.SteelBlue
        Me.lstCustomerType.GridLineColor = System.Drawing.Color.Transparent
        Me.lstCustomerType.GridLineHorizontal = False
        Me.lstCustomerType.GridLineVertical = False
        Me.lstCustomerType.HighlightBorderColor = System.Drawing.Color.Black
        Me.lstCustomerType.HighlightBorderOnMouseEvents = True
        Me.lstCustomerType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.lstCustomerType.Location = New System.Drawing.Point(144, 24)
        Me.lstCustomerType.ManagingFastMouseMoving = True
        Me.lstCustomerType.ManagingFastMouseMovingInterval = 30
        Me.lstCustomerType.Name = "lstCustomerType"
        Me.lstCustomerType.NormalBorderColor = System.Drawing.Color.Black
        Me.lstCustomerType.Size = New System.Drawing.Size(152, 21)
        Me.lstCustomerType.TabIndex = 4
        Me.lstCustomerType.TabStop = False
        '
        'lstCustomerTypeID
        '
        Me.lstCustomerTypeID.Location = New System.Drawing.Point(144, 24)
        Me.lstCustomerTypeID.Name = "lstCustomerTypeID"
        Me.lstCustomerTypeID.Size = New System.Drawing.Size(8, 21)
        Me.lstCustomerTypeID.TabIndex = 80
        Me.lstCustomerTypeID.Text = "ComboBox1"
        Me.lstCustomerTypeID.Visible = False
        '
        'lstCustomerTypePDText
        '
        Me.lstCustomerTypePDText.Location = New System.Drawing.Point(200, 24)
        Me.lstCustomerTypePDText.Name = "lstCustomerTypePDText"
        Me.lstCustomerTypePDText.Size = New System.Drawing.Size(8, 21)
        Me.lstCustomerTypePDText.TabIndex = 81
        Me.lstCustomerTypePDText.Text = "ComboBox1"
        Me.lstCustomerTypePDText.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(807, 23)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(90, 474)
        Me.Panel3.TabIndex = 85
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem4, Me.MenuItem6})
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 0
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem5, Me.MenuItem9})
        Me.MenuItem4.Text = "Tasks"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 0
        Me.MenuItem5.Text = "Log Off"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 1
        Me.MenuItem9.Text = "Exit"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 1
        Me.MenuItem6.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem7, Me.MenuItem8})
        Me.MenuItem6.Text = "Logs"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 0
        Me.MenuItem7.Text = "New Log"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 1
        Me.MenuItem8.Text = "Save Log"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "Current User:"
        '
        'ConnectionTester
        '
        Me.ConnectionTester.Enabled = True
        Me.ConnectionTester.Interval = 10000
        '
        'ArrowImageList
        '
        Me.ArrowImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ArrowImageList.ImageSize = New System.Drawing.Size(25, 25)
        Me.ArrowImageList.ImageStream = CType(resources.GetObject("ArrowImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ArrowImageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'Main_Screen
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(930, 610)
        Me.Controls.Add(Me.ConnectionTesterCounter)
        Me.Controls.Add(Me.ServerStatus)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Current_User)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label9)
        Me.ForeColor = System.Drawing.Color.SteelBlue
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(936, 642)
        Me.Menu = Me.MainMenu1
        Me.MinimumSize = New System.Drawing.Size(936, 642)
        Me.Name = "Main_Screen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tutor Activity Logger 1.0"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private current_light As Integer = 0
    Private current_colour As Integer = 0
    Private currently_working As Boolean = False




    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            If ex.Message.IndexOf("Thread was being aborted") < 0 Then
                If error_reporting_level = "minimal" Then
                    Dim Display_Message1 As New Display_Message("The Application encountered the following problem: " & vbCrLf & ex.Message.ToString)
                    Display_Message1.ShowDialog()
                Else
                    Dim Display_Message1 As New Display_Message("The Application encountered the following problem: " & vbCrLf & identifier_msg & ":" & ex.ToString)
                    Display_Message1.ShowDialog()
                End If
                Dim dir As DirectoryInfo = New DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
                If dir.Exists = False Then
                    dir.Create()
                End If
                Dim filewriter As StreamWriter = New StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
                filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ":" & ex.ToString)
                filewriter.Flush()
                filewriter.Close()
            End If
        Catch exc As Exception
            MsgBox("An error occurred in Tutor Activity Logger's error handling routine. The application will try to recover from this serious error.", MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub

    Private Sub run_green_lights()
        If shutting_down = False Then

            Try
                Label1.ForeColor = Color.LimeGreen
                Label1.Text = "Waiting"


                current_light = current_light - 1
                If current_light < 1 Then
                    current_light = 5
                End If
                current_colour = 0



                PictureBox1.Image = ImageList1.Images(1)
                PictureBox2.Image = ImageList1.Images(1)
                PictureBox3.Image = ImageList1.Images(1)
                PictureBox4.Image = ImageList1.Images(1)
                PictureBox5.Image = ImageList1.Images(1)
           


            Select Case current_light
                Case 0

                    PictureBox1.Image = ImageList1.Images(0)
                Case 1

                    PictureBox2.Image = ImageList1.Images(0)
                Case 2

                    PictureBox3.Image = ImageList1.Images(0)
                Case 3

                    PictureBox4.Image = ImageList1.Images(0)
                Case 4

                    PictureBox5.Image = ImageList1.Images(0)
                Case 5

                    PictureBox1.Image = ImageList1.Images(0)
            End Select

            current_light = current_light + 1
            If current_light > 5 Then
                current_light = 1
            End If
            Catch err As System.InvalidOperationException
                current_light = current_light
            Catch ex As Exception
                Error_Handler(ex, "Running Lights")
            End Try
        End If
    End Sub

    Private Sub run_orange_lights()
        If shutting_down = False Then


            Try
                Label1.ForeColor = Color.DarkOrange
                Label1.Text = "Working"

                current_light = current_light - 1
                If current_light < 1 Then
                    current_light = 5
                End If
                current_colour = 1



                PictureBox1.Image = ImageList1.Images(3)
                PictureBox2.Image = ImageList1.Images(3)
                PictureBox3.Image = ImageList1.Images(3)
                PictureBox4.Image = ImageList1.Images(3)
                PictureBox5.Image = ImageList1.Images(3)
          
            Select Case current_light
                Case 0
                    PictureBox1.Image = ImageList1.Images(2)
                Case 1
                    PictureBox2.Image = ImageList1.Images(2)
                Case 2
                    PictureBox3.Image = ImageList1.Images(2)
                Case 3
                    PictureBox4.Image = ImageList1.Images(2)
                Case 4
                    PictureBox5.Image = ImageList1.Images(2)
                Case 5
                    PictureBox1.Image = ImageList1.Images(2)
            End Select

            current_light = current_light + 1
            If current_light > 5 Then
                current_light = 1
                End If
            Catch err As System.InvalidOperationException
                current_light = current_light
            Catch ex As Exception
                Error_Handler(ex, "Running Lights")
            End Try
        End If
    End Sub

    Private Sub run_lights()
        If shutting_down = False Then


            Try
                If current_colour = 1 Then
                    Select Case current_light
                        Case 0
                            PictureBox5.Image = ImageList1.Images(3)
                            PictureBox1.Image = ImageList1.Images(2)
                        Case 1
                            PictureBox1.Image = ImageList1.Images(3)
                            PictureBox2.Image = ImageList1.Images(2)
                        Case 2
                            PictureBox2.Image = ImageList1.Images(3)
                            PictureBox3.Image = ImageList1.Images(2)
                        Case 3
                            PictureBox3.Image = ImageList1.Images(3)
                            PictureBox4.Image = ImageList1.Images(2)
                        Case 4
                            PictureBox4.Image = ImageList1.Images(3)
                            PictureBox5.Image = ImageList1.Images(2)
                        Case 5
                            PictureBox5.Image = ImageList1.Images(3)
                            PictureBox1.Image = ImageList1.Images(2)
                    End Select
                Else
                    Select Case current_light
                        Case 0
                            PictureBox5.Image = ImageList1.Images(1)
                            PictureBox1.Image = ImageList1.Images(0)
                        Case 1
                            PictureBox1.Image = ImageList1.Images(1)
                            PictureBox2.Image = ImageList1.Images(0)
                        Case 2
                            PictureBox2.Image = ImageList1.Images(1)
                            PictureBox3.Image = ImageList1.Images(0)
                        Case 3
                            PictureBox3.Image = ImageList1.Images(1)
                            PictureBox4.Image = ImageList1.Images(0)
                        Case 4
                            PictureBox4.Image = ImageList1.Images(1)
                            PictureBox5.Image = ImageList1.Images(0)
                        Case 5
                            PictureBox5.Image = ImageList1.Images(1)
                            PictureBox1.Image = ImageList1.Images(0)
                    End Select

                End If

                current_light = current_light + 1
                If current_light > 5 Then
                    current_light = 1
                End If
            Catch err As System.InvalidOperationException
                current_light = current_light
            Catch ex As Exception
                Error_Handler(ex, "Running Lights")
            End Try
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            run_lights()
            Label8.Text = Format(Now(), "dd/MM/yyyy HH:mm:ss")

            ConnectionTesterCounter.ForeColor = ServerStatus.ForeColor
            If testingconnection = False Then


                ConnectionTesterCounter.Text = (CInt(ConnectionTesterCounter.Text) - 1).ToString
                If ConnectionTesterCounter.Text.Length < 2 Then
                    ConnectionTesterCounter.Text = "0" & ConnectionTesterCounter.Text
                End If

                If CInt(ConnectionTesterCounter.Text) = -1 Then
                    ConnectionTesterCounter.Text = 10
                End If

            End If

            If application_exit = True Then
                Me.Close()
            End If
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Private Sub Main_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            PictureBox6.Image = ArrowImageList.Images(0)
            PictureBox7.Image = ArrowImageList.Images(2)
            If File.Exists((Application.StartupPath & "\").Replace("\\", "\") & "config.ini") = False Then
                If File.Exists((Application.StartupPath & "\").Replace("\\", "\") & "default_config.ini") = True Then
                    File.Copy((Application.StartupPath & "\").Replace("\\", "\") & "default_config.ini", (Application.StartupPath & "\").Replace("\\", "\") & "config.ini")
                End If
            End If

            If File.Exists((Application.StartupPath & "\").Replace("\\", "\") & "config.ini") = True Then
                Dim server As String = ""
                Dim table As String = ""
                Dim user As String = ""

                Try


                    Dim filereader As IO.StreamReader = New StreamReader((Application.StartupPath & "\").Replace("\\", "\") & "config.ini")

                    server = filereader.ReadLine.Replace("Server:", "").Trim
                    table = filereader.ReadLine.Replace("Table:", "").Trim
                    user = filereader.ReadLine.Replace("User:", "").Trim
                    error_reporting_level = filereader.ReadLine.Replace("Errors:", "").Trim
                    Worker1.dbserver = server
                    Worker1.dbuser = user
                    Worker1.dbtable = table

                    filereader.Close()
                Catch ex As Exception
                    Dim displ As Display_Message = New Display_Message("No valid config.ini file could be located in the application startup folder. This application will no be forced to shutdown.")
                    displ.ShowDialog()
                    dataloaded = True
                    splash_loader.Visible = False
                    application_exit = True
                    Label8.Text = Format(Now(), "dd/MM/yyyy HH:mm:ss")
                    Timer2.Start()
                End Try
                If application_exit = False Then


                    Label8.Text = Format(Now(), "dd/MM/yyyy HH:mm:ss")
                    Timer2.Start()
                    ConnectionTester.Start()
                    force_check(3)


                    If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
                        Clear_Inputs(True)
                        colourbuttons(btnMyCalls)
                        Load_Calls(Current_User.Text.Trim, False, 1, "False")
                    End If
                    dataloaded = True
                    splash_loader.Visible = False

                    Try
                        Dim ApplicationName As String
                        ApplicationName = "Tutor Activity Logger"
                        Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
                        Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)
                        If Process.GetProcessesByName(aProcName).Length > 1 Then
                            Me.Hide()
                            Dim Display_Message1 As New Display_Message("Another Instance of " & ApplicationName & " is already running. Only one instance of " & ApplicationName & " is allowed to run at any time. This instance of the program will now commence shut down operations.")
                            Display_Message1.ShowDialog()
                            application_exit = True
                        End If
                    Catch ex As Exception
                        Error_Handler(ex, "Checking Multiple Application Instances")
                    End Try
                End If
            Else
                Dim displ As Display_Message = New Display_Message("No required config.ini file could be located in the application startup folder. This application will no be forced to shutdown.")
                displ.ShowDialog()
                dataloaded = True
                splash_loader.Visible = False
                application_exit = True
                Label8.Text = Format(Now(), "dd/MM/yyyy HH:mm:ss")
                Timer2.Start()
            End If




        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Private Sub exit_application()
        Try
            If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
                Dim result As String
                Try

                    Dim conn1 As OleDb.OleDbConnection = Worker1.Get_Connection()

                    conn1.Open()
                    Try
                        Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                        sql = New OleDb.OleDbCommand
                        sql.CommandText = "INSERT INTO [Audit_Logins] ([Audit_Tutor],[Audit_Date],[Audit_Time]) values ('" & Current_User.Text.Trim & " [LOGOUT]','" & Format(Now(), "yyyy/MM/dd") & "','" & Format(Now(), "HH:mm:ss") & "')"
                        sql.Connection = conn1
                        result = sql.ExecuteNonQuery().ToString & " Insert Succeeded"
                        sql.Dispose()
                        result = "Login Tracking Audit Succeeded"
                    Catch ex As Exception
                        Error_Handler(ex)
                        result = "Login Tracking Audit Failed"
                    Finally
                        conn1.Close()
                        conn1.Dispose()
                    End Try
                Catch ex As Exception
                    Error_Handler(ex)
                    result = "Login Tracking Audit Failed"
                Finally
                    StatusBar.Text = result
                    If StatusBar.Text = "Login Tracking Audit Failed" Then
                        StatusBar.ForeColor = Color.Red
                    Else
                        StatusBar.ForeColor = Color.LimeGreen
                        loginaudited = True
                    End If
                End Try
            End If

            shutting_down = True
            Timer2.Stop()
            ConnectionTester.Stop()
            If Worker1.WorkerThread Is Nothing = False Then
                Worker1.WorkerThread.Abort()
                Worker1.Dispose()
            End If
            NotifyIcon1.Dispose()
            Application.Exit()
        Catch ex As Exception
            Error_Handler(ex, "Shutting Down Application")
        Finally
            Application.Exit()
        End Try
    End Sub

    Private Sub Main_Screen_closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            exit_application()
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub


    Public Sub WorkerHandler(ByVal Result As String)
        Try
            If Result.StartsWith("Server") = False Then
                If Result.IndexOf("Retrieval") = -1 Then



                If CInt(Result.Split(" ")(0)) > 0 Then
                    StatusBar.Text = Worker1.ActivityLog.Log_ID & " Activity Log Successfully Created"
                    StatusBar.ForeColor = Color.LimeGreen
                    lstLogID.Text = Worker1.ActivityLog.Log_ID
                    lstLogID.ForeColor = Color.Green
                        Clear_Inputs(True)
                        Load_Calls(displayrecordcount.Tag, True, CInt(lblSegment.Text), lblSegment.Tag)
                Else
                    StatusBar.Text = Worker1.ActivityLog.Log_ID & " Activity Log Creation Failed"
                    StatusBar.ForeColor = Color.Red
                    lstLogID.Text = "Unassigned"
                    lstLogID.ForeColor = Color.Red
                    End If
                Else
                    If CInt(Result.Split(" ")(0)) > 0 Then
                        StatusBar.Text = Worker1.ActivityLog.Log_ID & " Activity Log Successfully Retrieved"
                        StatusBar.ForeColor = Color.LimeGreen
                        lstLogID.ForeColor = Color.Green
                        lstLogID.Text = Worker1.ActivityLog.Log_ID
                        lstLogTutor.Text = Worker1.ActivityLog.Log_Tutor
                        lstLogDate.Text = Worker1.ActivityLog.Log_Date
                        lstLogTime.Text = Worker1.ActivityLog.Log_Time
                        txtProblemDuration.Text = Worker1.ActivityLog.Log_Duration

                        Dim r As IEnumerator = lstCustomerTypeID.Items.GetEnumerator()
                        Dim counter As Integer = 0
                        While r.MoveNext = True
                            Dim obj As MTGCComboBoxItem = r.Current
                            If obj.Text = Worker1.ActivityLog.Log_User_Type.Trim Then
                                lstCustomerTypeID.SelectedIndex = counter
                                Exit While
                            End If
                            counter = counter + 1
                        End While
                        r = Nothing



                        txtCustomerDetailsID.Text = Worker1.ActivityLog.Log_Personal_Details_ID
                        txtCustomerDetailsSurname.Text = Worker1.ActivityLog.Log_Personal_Details_Surname
                        txtCustomerDetailsFirstName.Text = Worker1.ActivityLog.Log_Personal_Details_Firstname



                        r = lstProblemTypeID.Items.GetEnumerator()
                        counter = 0
                        While r.MoveNext = True
                            Dim obj As MTGCComboBoxItem = r.Current
                            If obj.Text = Worker1.ActivityLog.Log_Call_Type.Trim Then
                                lstProblemTypeID.SelectedIndex = counter
                                lstProblemType.SelectedIndex = lstProblemTypeID.SelectedIndex
                                Exit While
                            End If
                            counter = counter + 1
                        End While
                        r = Nothing

                        r = lstProblemSubTypeID.Items.GetEnumerator()
                        counter = 0
                        While r.MoveNext = True
                            Dim obj As MTGCComboBoxItem = r.Current
                            If obj.Text = Worker1.ActivityLog.Log_Call_Sub_Type.Trim Then
                                lstProblemSubTypeID.SelectedIndex = counter
                                lstProblemSubType.SelectedIndex = lstProblemSubTypeID.SelectedIndex
                                Exit While
                            End If
                            counter = counter + 1
                        End While
                        r = Nothing
                        Select Case Worker1.ActivityLog.Log_Resolve_Status
                            Case "Unresolved"
                                lstResolveStatus.SelectedIndex = 0
                            Case "Resolved"
                                lstResolveStatus.SelectedIndex = 1
                        End Select


                        lstResolveDate.Text = Worker1.ActivityLog.Log_Resolve_Date
                        lstResolveTime.Text = Worker1.ActivityLog.Log_Resolve_Time
                        lstResolveTutor.Text = Worker1.ActivityLog.Log_Resolve_Tutor
                        txtProblemDescription.Text = Worker1.ActivityLog.Log_Problem_Description
                        txtProblemResolution.Text = Worker1.ActivityLog.Log_Problem_Resolution

                        lstLogModifyDate.Text = Worker1.ActivityLog.Log_Modify_Date
                        lstLogModifyTime.Text = Worker1.ActivityLog.Log_Modify_Time
                        lstLogModifyTutor.Text = Worker1.ActivityLog.Log_Modify_Tutor

                    Else
                        StatusBar.Text = Worker1.ActivityLog.Log_ID & " Activity Log Retrieval Failed"
                        StatusBar.ForeColor = Color.Red
                        lstLogID.ForeColor = Color.Red
                    End If
                End If
            Else
                testingconnection = False
                ConnectionTesterCounter.Text = 10
                'ConnectionTester.Start()
                ServerStatus.Text = Worker1.serverstatus.Text
                ServerStatus.ForeColor = Worker1.serverstatus.ForeColor
                ConnectionTesterCounter.ForeColor = Worker1.serverstatus.ForeColor
                ServerStatus.Refresh()
                If ServerStatus.Text = "SQL Server Status: Online" Then
                    LockControls(True)
                    If lstCustomerType.Items.Count < 1 Then
                        Clear_Inputs(True)
                        colourbuttons(btnMyCalls)
                        Load_Calls(Current_User.Text.Trim, True, 1, "False")
                    End If
                End If

            End If
          
        Catch ex As Exception
            Error_Handler(ex)
        Finally
            currently_working = False
            NotifyIcon1.Text = "Resting... "
            run_green_lights()
        End Try
    End Sub

 

  

 
    Public Sub WorkerErrorEncounteredHandler(ByVal ex As Exception)
        Try
            Error_Handler(ex, "Worker Error Encountered")
        Catch exc As Exception
            Error_Handler(exc)
        End Try
    End Sub

    Private Sub run_worker(Optional ByVal threadselect As Integer = 1)
        run_orange_lights()

        Select Case threadselect
            Case 1
                Worker1.ChooseThreads(1)
            Case 2
            

                    lstLogModifyTutor.Text = Current_User.Text.Trim
                lstLogModifyDate.Text = Format(Now(), "yyyy/MM/dd")
                lstLogModifyTime.Text = Format(Now(), "HH:mm:ss")
                Dim cinfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")

                Worker1.ActivityLog.Clear_Data()
                Worker1.ActivityLog.Log_ID = lstLogID.Text.Trim
                If lstLogTutor.Text = "" Then
                    Worker1.ActivityLog.Log_Tutor = Current_User.Text.Trim
                Else
                    Worker1.ActivityLog.Log_Tutor = lstLogTutor.Text
                End If
                If lstLogDate.Text = "" Then
                    Worker1.ActivityLog.Log_Date = Format(Now(), "yyyy/MM/dd")
                Else
                    Worker1.ActivityLog.Log_Date = lstLogDate.Text
                End If
                If lstLogTime.Text = "" Then
                    Worker1.ActivityLog.Log_Time = Format(Now(), "HH:mm:ss")
                Else
                    Worker1.ActivityLog.Log_Time = lstLogTime.Text
                End If



                Worker1.ActivityLog.Log_Duration = txtProblemDuration.Text.Trim

                Worker1.ActivityLog.Log_User_Type = lstCustomerTypeID.SelectedItem.text
                Worker1.ActivityLog.Log_Personal_Details_ID = txtCustomerDetailsID.Text.Trim.ToUpper
                Worker1.ActivityLog.Log_Personal_Details_Surname = cinfo.TextInfo.ToTitleCase(txtCustomerDetailsSurname.Text.Trim.Replace("  ", " "))
                Worker1.ActivityLog.Log_Personal_Details_Firstname = cinfo.TextInfo.ToTitleCase(txtCustomerDetailsFirstName.Text.Trim.Replace("  ", " "))
                Worker1.ActivityLog.Log_Call_Type = lstProblemTypeID.SelectedItem.text
                Worker1.ActivityLog.Log_Call_Sub_Type = lstProblemSubTypeID.SelectedItem.text
                Worker1.ActivityLog.Log_Resolve_Status = lstResolveStatus.SelectedItem.text
                Worker1.ActivityLog.Log_Resolve_Date = lstResolveDate.Text
                Worker1.ActivityLog.Log_Resolve_Time = lstResolveTime.Text
                Worker1.ActivityLog.Log_Resolve_Tutor = lstResolveTutor.Text
                Worker1.ActivityLog.Log_Problem_Description = txtProblemDescription.Text.Trim.Replace("'", "''")
                Worker1.ActivityLog.Log_Problem_Resolution = txtProblemResolution.Text.Trim.Replace("'", "''")
                Worker1.ActivityLog.Log_Modify_Date = lstLogModifyDate.Text
                Worker1.ActivityLog.Log_Modify_Time = lstLogModifyTime.Text
                Worker1.ActivityLog.Log_Modify_Tutor = lstLogModifyTutor.Text
                StatusBar.Text = ""
                Worker1.ChooseThreads(2)
            Case 3

                If testingconnection = False Then
                    ConnectionTesterCounter.Text = 0
                    'ConnectionTester.Stop()
                    ServerStatus.ForeColor = Color.Orange
                    ServerStatus.Text = "SQL Server Status: Checking"
                    ToolTip1.SetToolTip(ServerStatus, "SQL Server connection status on " & Worker1.dbserver & " (user: " & Worker1.dbuser & ")")
                    ServerStatus.Refresh()
                    testingconnection = True
                    LockControls(False)
                    Worker1.ChooseThreads(3)
                End If

        End Select

        currently_working = True
    End Sub



    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        Try
            Me.Close()
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Protected Friend Sub show_application()
        Try
            Me.Opacity = 1

            Me.BringToFront()
            Me.Refresh()
            Me.WindowState = FormWindowState.Normal

        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Private Sub NotifyIcon1_dblclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        show_application()
    End Sub
    Private Sub NotifyIcon1_snglclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        show_application()
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        show_application()
    End Sub

    Private Sub Main_Screen_resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Try

            If Me.WindowState = FormWindowState.Minimized Then
                NotifyIcon1.Visible = True
                Me.Opacity = 0
            End If
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub

    Private Sub force_check(Optional ByVal threadselect As Integer = 1)
        Try
            NotifyIcon1.Text = "Processing Request..."
            If currently_working = False Then

                Select Case threadselect
                    Case 1
                        run_worker(threadselect)
                    Case 2
                        If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
                            If txtProblemDescription.Text.Trim = "" Or txtProblemResolution.Text.Trim = "" Then
                                Dim errorl As String = ""
                                If txtProblemDescription.Text.Trim = "" Then
                                    errorl = errorl & vbCrLf & "- Problem Description is currently blank"
                                End If
                                If txtProblemResolution.Text.Trim = "" Then
                                    errorl = errorl & vbCrLf & "- Problem Resolution is currently blank"
                                End If
                                MsgBox("You need to ensure that all inputs marked with a red asterisk are filled in before saving an Activity Log." & vbCrLf & errorl, MsgBoxStyle.Information, "Save Request Rejected")
                            Else
                                run_worker(threadselect)
                            End If
                        End If
                    Case 3

                        run_worker(threadselect)

                End Select



            End If
        Catch ex As Exception
            Error_Handler(ex)
        End Try
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        force_check()
    End Sub


    Private Function DosShellCommand(ByVal AppToRun As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process

            myProcess.StartInfo.FileName = "cmd.exe"
            myProcess.StartInfo.UseShellExecute = False



            myProcess.StartInfo.CreateNoWindow = True

            myProcess.StartInfo.RedirectStandardInput = False
            myProcess.StartInfo.RedirectStandardOutput = False
            myProcess.StartInfo.RedirectStandardError = False

            myProcess.StartInfo.FileName = AppToRun

            myProcess.Start()

        Catch ex As Exception
            Error_Handler(ex)
        End Try
        Return s
    End Function


    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Try
            Dim apptorun As String
            apptorun = """" & (Application.StartupPath & "\Tutor Activity Logger.exe").Replace("\\", "\") & """"
            DosShellCommand(apptorun)
            exit_application()
        Catch ex As Exception
            Error_Handler(ex, "Logging Off")
        End Try
    End Sub



    Private Sub fill_in_data()

        If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
            Try



                If loginaudited = False And Worker1.serverstatus.Text = "SQL Server Status: Online" Then
                    Dim result As String
                    Try

                        Dim conn1 As OleDb.OleDbConnection = Worker1.Get_Connection()

                        conn1.Open()
                        Try
                            Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                            sql = New OleDb.OleDbCommand
                            sql.CommandText = "INSERT INTO [Audit_Logins] ([Audit_Tutor],[Audit_Date],[Audit_Time]) values ('" & Current_User.Text.Trim & "','" & Format(Now(), "yyyy/MM/dd") & "','" & Format(Now(), "HH:mm:ss") & "')"
                            sql.Connection = conn1
                            result = sql.ExecuteNonQuery().ToString & " Insert Succeeded"
                            sql.Dispose()
                            result = "Login Tracking Audit Succeeded"
                        Catch ex As Exception
                            Error_Handler(ex)
                            result = "Login Tracking Audit Failed"
                        Finally
                            conn1.Close()
                            conn1.Dispose()
                        End Try
                    Catch ex As Exception
                        Error_Handler(ex)
                        result = "Login Tracking Audit Failed"
                    Finally
                        StatusBar.Text = result
                        If StatusBar.Text = "Login Tracking Audit Failed" Then
                            StatusBar.ForeColor = Color.Red
                        Else
                            StatusBar.ForeColor = Color.LimeGreen
                            loginaudited = True
                        End If
                    End Try
                End If

                Dim conn As OleDb.OleDbConnection = Worker1.Get_Connection()
                conn.Open()
                Try
                    lblStats.Text = "Quick Stats for " & Current_User.Text
                    lblYearCalls.Text = "User Calls for " & Format(Now, "yyyy") & ":"
                    lblMonthCalls.Text = "User Calls for " & Format(Now, "MMMM yyyy") & ":"
                    lblTotalYearCalls.Text = "Total Calls for " & Format(Now, "yyyy") & ":"
                    lblTotalMonthCalls.Text = "Total Calls for " & Format(Now, "MMMM yyyy") & ":"



                    Dim stats(3) As String
                    stats(0) = "Select count([Log_ID]) as Total_Calls from [Log_Records] where [Log_Tutor] = '" & Current_User.Text.Trim.ToUpper & "' and [Log_Date] like '%/" & Year(Now) & "%'"
                    stats(1) = "Select count([Log_ID]) as Total_Calls from [Log_Records] where [Log_Tutor] = '" & Current_User.Text.Trim.ToUpper & "' and [Log_Date] like '%/" & Month(Now) & "/" & Year(Now) & "%'"
                    stats(2) = "Select count([Log_ID]) as Total_Calls from [Log_Records] where [Log_Date] like '%/" & Year(Now) & "%'"
                    stats(3) = "Select count([Log_ID]) as Total_Calls from [Log_Records] where [Log_Date] like '%/" & Month(Now) & "/" & Year(Now) & "%'"
                    Dim controls(3) As Control
                    controls(0) = YearCalls
                    controls(1) = MonthCalls
                    controls(2) = TotalYearCalls
                    controls(3) = TotalMonthCalls
                    Dim counter As Integer = 0
                    Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                    Dim datareader As OleDb.OleDbDataReader
                    For Each str As String In stats
                        sql = New OleDb.OleDbCommand
                        sql.CommandText = str
                        sql.Connection = conn

                        datareader = sql.ExecuteReader(CommandBehavior.Default)
                        If datareader.HasRows = True Then
                            While datareader.Read = True
                                controls(counter).Text = (datareader.Item("Total_Calls"))
                            End While
                        End If
                        datareader.Close()
                        sql.Dispose()
                        counter = counter + 1
                    Next


                    lstCustomerType.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
                    lstCustomerType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
                    lstCustomerType.ColumnNum = 1
                    lstCustomerType.ColumnWidth = "80"

                    If lstCustomerType.Items.Count > 0 Then
                        lstCustomerType.Items.Clear()
                        lstCustomerTypeID.Items.Clear()
                        lstCustomerTypePDText.Items.Clear()
                    End If
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "Select [User_ID],[User_Description], [User_Personal_Details_ID_Descriptor] from [User_Type] order by [User_ID] asc"
                    sql.Connection = conn

                    datareader = sql.ExecuteReader(CommandBehavior.Default)
                    If datareader.HasRows = True Then
                        While datareader.Read = True
                            lstCustomerType.Items.Add(New MTGCComboBoxItem(datareader.Item("User_Description").ToString.Trim))
                            lstCustomerTypeID.Items.Add(New MTGCComboBoxItem(datareader.Item("User_ID").ToString.Trim))
                            lstCustomerTypePDText.Items.Add(New MTGCComboBoxItem(datareader.Item("User_Personal_Details_ID_Descriptor").ToString.Trim))
                        End While
                    End If
                    datareader.Close()
                    sql.Dispose()
                    If lstCustomerType.Items.Count > 0 Then
                        lstCustomerType.SelectedIndex = 0
                        lstCustomerTypeID.SelectedIndex = 0
                        lstCustomerTypePDText.SelectedIndex = 0
                    End If

                    If lstProblemType.Items.Count > 0 Then
                        lstProblemType.Items.Clear()
                        lstProblemTypeID.Items.Clear()
                    End If
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "Select [Call_ID],[Call_Description] from [Call_Type] order by [Call_ID] asc"
                    sql.Connection = conn

                    datareader = sql.ExecuteReader(CommandBehavior.Default)
                    If datareader.HasRows = True Then
                        While datareader.Read = True
                            lstProblemType.Items.Add(New MTGCComboBoxItem(datareader.Item("Call_Description").ToString.Trim))
                            lstProblemTypeID.Items.Add(New MTGCComboBoxItem(datareader.Item("Call_ID").ToString.Trim))
                        End While
                    End If
                    datareader.Close()
                    sql.Dispose()
                    If lstProblemType.Items.Count > 0 Then
                        lstProblemType.SelectedIndex = 0
                        lstProblemTypeID.SelectedIndex = 0
                    End If


                    If lstResolveStatus.Items.Count > 0 Then
                        lstResolveStatus.Items.Clear()
                    End If
                    lstResolveStatus.Items.Add(New MTGCComboBoxItem("Unresolved"))
                    lstResolveStatus.Items.Add(New MTGCComboBoxItem("Resolved"))
                    If lstResolveStatus.Items.Count > 0 Then
                        lstResolveStatus.SelectedIndex = 0
                    End If
                    lstCustomerTypeCover.Select()
                Catch ex As Exception
                    Error_Handler(ex, "Autofill Main Screen Data")
                Finally
                    conn.Close()
                    conn.Dispose()
                End Try
            Catch ex As Exception
                Error_Handler(ex, "Autofill Main Screen Data")
            End Try
        End If
    End Sub

    Private Sub lstCustomerTypeID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCustomerTypeID.SelectedIndexChanged
        Try
            lstCustomerType.SelectedIndex = lstCustomerTypeID.SelectedIndex
            lstCustomerTypeCover.Text = lstCustomerType.Text
            lstCustomerTypePDText.SelectedIndex = lstCustomerTypeID.SelectedIndex
            PersonalDetailsDescriptor.Text = lstCustomerTypePDText.SelectedItem.text & ":"
            txtCustomerDetailsID.Select()
        Catch ex As Exception
            Error_Handler(ex, "Changing User Type")
        End Try
    End Sub

    Private Sub lstCustomerType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCustomerType.SelectedIndexChanged
        Try
            lstCustomerTypeCover.Text = lstCustomerType.Text
            lstCustomerTypeID.SelectedIndex = lstCustomerType.SelectedIndex
            lstCustomerTypePDText.SelectedIndex = lstCustomerType.SelectedIndex
            PersonalDetailsDescriptor.Text = lstCustomerTypePDText.SelectedItem.text & ":"
            txtCustomerDetailsID.Select()
        Catch ex As Exception
            Error_Handler(ex, "Changing User Type")
        End Try
    End Sub

    Private Sub lstProblemType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProblemType.SelectedIndexChanged

        If Worker1.serverstatus.Text = "SQL Server Status: Online" Then
            Try

                lstProblemTypeCover.Text = lstProblemType.Text
                lstProblemTypeID.SelectedIndex = lstProblemType.SelectedIndex
                txtProblemDescription.Select()


                If lstProblemSubType.Items.Count > 0 Then
                    lstProblemSubType.Items.Clear()
                    lstProblemSubTypeID.Items.Clear()
                End If
                Dim conn As OleDb.OleDbConnection = Worker1.Get_Connection()
                conn.Open()
                Try
                    Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                    Dim datareader As OleDb.OleDbDataReader
                    sql = New OleDb.OleDbCommand
                    sql.CommandText = "Select [Call_Sub_ID],[Call_Sub_Description] from [Call_Sub_Type] where [Call_Sub_Call_ID] = '" & lstProblemTypeID.SelectedItem.Text & "' order by [Call_Sub_ID] asc"
                    sql.Connection = conn

                    datareader = sql.ExecuteReader(CommandBehavior.Default)
                    If datareader.HasRows = True Then
                        While datareader.Read = True
                            lstProblemSubType.Items.Add(New MTGCComboBoxItem(datareader.Item("Call_Sub_Description").ToString.Trim))
                            lstProblemSubTypeID.Items.Add(New MTGCComboBoxItem(datareader.Item("Call_Sub_ID").ToString.Trim))
                        End While
                    End If
                    datareader.Close()
                    sql.Dispose()
                    If lstProblemSubType.Items.Count > 0 Then
                        lstProblemSubType.SelectedIndex = 0
                        lstProblemSubTypeID.SelectedIndex = 0
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "Changing Problem Type")
                Finally
                    conn.Close()
                    conn.Dispose()
                End Try

            Catch ex As Exception
                Error_Handler(ex, "Changing Problem Type")
            End Try
        End If
    End Sub



    Private Sub lstProblemSubType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProblemSubType.SelectedIndexChanged
        Try
            lstProblemSubTypeCover.Text = lstProblemSubType.Text
            lstProblemSubTypeID.SelectedIndex = lstProblemSubType.SelectedIndex
            txtProblemDescription.Select()
        Catch ex As Exception
            Error_Handler(ex, "Changing Problem Sub Type")
        End Try
    End Sub

    Private Sub lstResolveStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstResolveStatus.SelectedIndexChanged
        Try
            lstResolveStatusCover.Text = lstResolveStatus.Text
            If lstResolveStatusCover.Text = "Resolved" Then
                lstResolveTutor.Text = Current_User.Text
                lstResolveDate.Text = Format(Now(), "yyyy/MM/dd")
                lstResolveTime.Text = Format(Now(), "HH:mm:ss")
            Else
                lstResolveTutor.Text = ""
                lstResolveDate.Text = ""
                lstResolveTime.Text = ""
            End If
        Catch ex As Exception
            Error_Handler(ex, "Changing Resolution")
        End Try
    End Sub

    Private Function Check_For_Dirty() As Boolean
        Dim result As Boolean = False
        Try

            Dim ctrl As Object
            For Each ctrl In GroupBox1.Controls
                If (ctrl.GetType.ToString).IndexOf("TextBox") <> -1 Then
                    If ctrl.text.length > 0 And ctrl.name.startswith("txt") = True Then

                        result = True
                    End If
                End If
            Next
            For Each ctrl In GroupBox2.Controls
                If ctrl.text.length > 0 And ctrl.name.startswith("txt") = True Then

                    result = True
                End If
            Next
        Catch ex As Exception
            Error_Handler(ex, "New Log")
        End Try
        Return result
    End Function

    Private Sub Clear_Inputs(Optional ByVal ForceClear As Boolean = False)
        Try

            Dim result As DialogResult = DialogResult.Cancel
            If ForceClear = False Then
                If Check_For_Dirty() = True Then
                    result = MsgBox("Are you sure you wish to abandon the current activity log being recorded and create a new log?", MsgBoxStyle.OKCancel, "Abandon Changes")
                Else
                    fill_in_data()
                    lstLogID.Text = "Unassigned"
                    lstLogID.ForeColor = Color.AliceBlue
                    lstLogTutor.Text = Current_User.Text
                    lstLogDate.Text = Format(Now(), "yyyy/MM/dd")
                    lstLogTime.Text = Format(Now(), "HH:mm:ss")
                    lstCustomerType.SelectedIndex = 0
                    lstProblemType.SelectedIndex = 0
                    lstProblemSubType.SelectedIndex = 0
                End If
            End If
            If result = DialogResult.OK Or ForceClear = True Then
                fill_in_data()
                Dim ctrl As Object
                For Each ctrl In GroupBox1.Controls
                    If (ctrl.GetType.ToString).IndexOf("TextBox") <> -1 Then
                        ctrl.text = ""
                    End If
                Next
                For Each ctrl In GroupBox2.Controls
                    If (ctrl.GetType.ToString).IndexOf("TextBox") <> -1 Then
                        ctrl.text = ""
                    End If
                Next
                For Each ctrl In GroupBox3.Controls
                    If (ctrl.GetType.ToString).IndexOf("TextBox") <> -1 Then
                        ctrl.text = ""
                    End If
                Next
                For Each ctrl In GroupBox4.Controls
                    If (ctrl.GetType.ToString).IndexOf("TextBox") <> -1 Then
                        ctrl.text = ""
                    End If
                Next
                lstLogID.Text = "Unassigned"
                lstLogID.ForeColor = Color.AliceBlue
                lstLogTutor.Text = Current_User.Text
                lstLogDate.Text = Format(Now(), "yyyy/MM/dd")
                lstLogTime.Text = Format(Now(), "HH:mm:ss")
                lstCustomerType.SelectedIndex = 0
                lstProblemType.SelectedIndex = 0
                lstProblemSubType.SelectedIndex = 0

                lstCustomerTypeCover.Select()
            End If
        Catch ex As Exception
            Error_Handler(ex, "New Log")
        End Try
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewLog.Click
        Clear_Inputs()
    End Sub

    Private Sub MenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem7.Click
        Clear_Inputs()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveLog.Click

        force_check(2)

    End Sub

    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem8.Click
        force_check(2)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        lstCustomerType.SelectedIndex = 0
        lstProblemType.SelectedIndex = 1
        lstProblemSubType.SelectedIndex = 0
        txtCustomerDetailsID.Text = "LTTCLA002"
        txtCustomerDetailsFirstName.Text = "Claire ANN"
        txtCustomerDetailsSurname.Text = "Lotter"
        txtProblemDescription.Text = "A nasty problem was encountered"
        txtProblemResolution.Text = "I solved the horrible problem" & vbCrLf & "Period."
    End Sub

    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        Try
            exit_application()
        Catch ex As Exception
            Error_Handler(ex, "Exiting Application")
        End Try
    End Sub


    Private Sub LockControls(ByVal unlocked As Boolean)
        Try
            lstCallList.Enabled = unlocked
            btnMyCalls.Enabled = unlocked
            btnAllCalls.Enabled = unlocked
            btnMyOpenCalls.Enabled = unlocked
            btnAllOpenCalls.Enabled = unlocked
            btnSaveLog.Enabled = unlocked
            btnNewLog.Enabled = unlocked
            MenuItem6.Enabled = unlocked
            lstCustomerType.Enabled = unlocked
            lstProblemType.Enabled = unlocked
            lstProblemSubType.Enabled = unlocked
        Catch ex As Exception
            Error_Handler(ex, "Locking Controls")
        End Try
    End Sub

    Private Sub ConnectionTester_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectionTester.Tick
        force_check(3)
    End Sub




    Private Sub Load_Calls(Optional ByVal Tutor As String = "All", Optional ByVal force As Boolean = False, Optional ByVal Segment As Integer = 1, Optional ByVal OpenCalls As String = "False")
        Try
            If (testingconnection = False And currently_working = False And Worker1.serverstatus.Text = "SQL Server Status: Online") Or force = True Then
                lstCallList.Items.Clear()
                Dim conn As OleDb.OleDbConnection = Worker1.Get_Connection
                conn.Open()
                Dim sql As OleDb.OleDbCommand = New OleDb.OleDbCommand
                Dim datareader As OleDb.OleDbDataReader
                sql = New OleDb.OleDbCommand
                Dim filler As String = ""
                If OpenCalls = "True" Then
                    If Tutor = "All" Then
                        filler = " where [Log_Resolve_Status] = 'Unresolved' "
                    Else
                        filler = " and [Log_Resolve_Status] = 'Unresolved' "
                    End If

                End If
                If Tutor = "All" Then
                    sql.CommandText = "Select [Log_ID] from [Log_Records]" & filler & "order by [Log_ID] desc"
                Else
                    sql.CommandText = "Select [Log_ID] from [Log_Records] where [Log_Tutor] = '" & Current_User.Text & "'" & filler & " order by [Log_ID] desc"
                End If

                sql.Connection = conn
                Dim segmentend, segmentstart As Integer
                segmentend = 35 * Segment
                segmentstart = 35 * (Segment - 1)
                datareader = sql.ExecuteReader(CommandBehavior.Default)
                Dim runner As Integer = 1
                If datareader.HasRows = True Then
                    While datareader.Read = True

                        If runner > segmentstart And runner <= segmentend Then
                            lstCallList.Items.Add("   " & datareader.Item("Log_ID").ToString.Trim)
                        End If
                        runner = runner + 1
                    End While

                End If
                runner = runner - 1
                lblSegment.Text = Segment
                lblSegment.Tag = OpenCalls
                lblSegmentNext.Text = Segment + 1
                lblSegmentPrevious.Text = Segment - 1
                If (Segment - 1) < 1 Then
                    PictureBox6.Visible = False
                    lblSegmentPrevious.Visible = False
                    PictureBox6.Enabled = False
                Else
                    PictureBox6.Visible = True
                    lblSegmentPrevious.Visible = True
                    PictureBox6.Enabled = True
                End If
                If Not (Segment * 35) < runner Then
                    PictureBox7.Visible = False
                    lblSegmentNext.Visible = False
                    PictureBox7.Enabled = False
                Else
                    PictureBox7.Visible = True
                    lblSegmentNext.Visible = True
                    PictureBox7.Enabled = True
                End If
                displayrecordcount.Text = "(" & lstCallList.Items.Count & " of " & runner & ")"
                displayrecordcount.Tag = Tutor
                datareader.Close()
                sql.Dispose()


                conn.Close()
            End If
        Catch ex As Exception
            Error_Handler(ex, "Loading My Calls")
        End Try
    End Sub




    Private Sub lstCallList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCallList.SelectedIndexChanged
        Try
            Worker1.ActivityLog.Clear_Data()
            Worker1.ActivityLog.Log_ID = lstCallList.SelectedItem.ToString.Trim
            Worker1.ChooseThreads(4)
        Catch ex As Exception
            Error_Handler(ex, "Loading Specific Call from List")
        End Try
    End Sub

    Private Sub colourbuttons(ByVal inputbutton As Button)
        Try
            Dim choice() As Button = {btnMyCalls, btnAllCalls, btnMyOpenCalls, btnAllOpenCalls}



            Dim u As Button
            For Each u In choice
                u.BackColor = Color.AliceBlue
            Next
            Dim result As Integer = (choice.IndexOf(choice, inputbutton))
            If result <> -1 Then
                choice(result).BackColor = Color.PowderBlue
            End If
        Catch ex As Exception
            Error_Handler(ex, "Colour Buttons")
        End Try
    End Sub

    Private Sub btnMyCalls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMyCalls.Click
        Try
            colourbuttons(sender)
            Load_Calls(Current_User.Text.Trim, False, 1)
        Catch ex As Exception
            Error_Handler(ex, "Display Records Button")
        End Try
    End Sub

    Private Sub btnAllCalls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllCalls.Click
        Try
            colourbuttons(sender)
            Load_Calls("All", False, 1)
        Catch ex As Exception
            Error_Handler(ex, "Display Records Button")
        End Try
    End Sub

    Private Sub PictureBox7_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.MouseHover
        Try
            PictureBox7.Image = ArrowImageList.Images(3)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Overlay")
        End Try
    End Sub
    Private Sub PictureBox7_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.MouseLeave
        Try
            PictureBox7.Image = ArrowImageList.Images(2)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Overlay")
        End Try
    End Sub
    Private Sub PictureBox6_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.MouseHover
        Try
            PictureBox6.Image = ArrowImageList.Images(1)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Overlay")
        End Try
    End Sub
    Private Sub PictureBox6_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.MouseLeave
        Try
            PictureBox6.Image = ArrowImageList.Images(0)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Overlay")
        End Try
    End Sub


    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        Try
            Load_Calls(displayrecordcount.Tag.ToString(), False, CInt(lblSegmentNext.Text), lblSegment.Tag)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Click")
        End Try
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Try
            Load_Calls(displayrecordcount.Tag.ToString(), False, CInt(lblSegmentPrevious.Text), lblSegment.Tag)
        Catch ex As Exception
            Error_Handler(ex, "Changing Arrow Click")
        End Try
    End Sub

    Private Sub btnMyOpenCalls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMyOpenCalls.Click
        Try
            colourbuttons(sender)
            Load_Calls(Current_User.Text.Trim, False, 1, "True")
        Catch ex As Exception
            Error_Handler(ex, "Display Records Button")
        End Try
    End Sub

    Private Sub btnAllOpenCalls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllOpenCalls.Click
        Try
            colourbuttons(sender)
            Load_Calls("All", False, 1, "True")
        Catch ex As Exception
            Error_Handler(ex, "Display Records Button")
        End Try
    End Sub
End Class
