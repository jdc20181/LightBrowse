'MIT License

'Copyright(c) Jeffrey Crowder (LightBrowse) 2017, BeffsBrowser 2015-2017

'Permission Is hereby granted, free Of charge, to any person obtaining a copy
'of this software And associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
'copies of the Software, And to permit persons to whom the Software Is
'furnished to do so, subject to the following conditions:

'The above copyright notice And this permission notice shall be included In all
'copies Or substantial portions of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
'IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
'FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
'AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
'LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
'OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
'SOFTWARE.





Imports Gecko

Public Class LightBrowseMain

#Region "Browsing Declarations"
    Dim int As Integer = 0

  Private Sub Loading(ByVal sender As Object, ByVal e As Gecko.GeckoProgressEventArgs)
        TabControl1.SelectedTab.Text = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).DocumentTitle
        If TabControl1.SelectedTab.Text = Nothing Then
            TabControl1.SelectedTab.Text = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString

        End If
        If CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString.StartsWith("https://") Then
            ToolStripLabel3.ForeColor = Color.ForestGreen
            ToolStripLabel3.Text = "Secure Connection"

        ElseIf CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString.StartsWith("http://") Then
            ToolStripLabel3.ForeColor = Color.Red
            ToolStripLabel3.Text = "Insecure Connection"
        End If
        ToolStripProgressBar1.Maximum = e.MaximumProgress
        ToolStripProgressBar1.Value = e.MaximumProgress
        Me.Cursor = Cursors.AppStarting

    End Sub

    Private Sub Done(ByVal sender As Object, ByVal e As Gecko.Events.GeckoDocumentCompletedEventArgs)

        If CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString.StartsWith("https://") Then
            ToolStripLabel3.ForeColor = Color.ForestGreen
            ToolStripLabel3.Text = "Secure Connection"

        ElseIf CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString.StartsWith("http://") Then
            ToolStripLabel3.ForeColor = Color.Red
            ToolStripLabel3.Text = "Insecure Connection"
        End If

        Me.Cursor = Cursors.Default
        ToolStripTextBox1.Text = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString


        If My.Settings.SafeBrowsing = "True" Then


        ElseIf My.Settings.SafeBrowsing = "False" Then
        
            My.Settings.History2.Add(CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString)
         
        End If
   

    End Sub
#End Region

#Region "Navigation Dependents"
    Public Sub UrlNavigate()
        If CheckURL(ToolStripTextBox1.Text) = True Then
            Dim brws As New GeckoWebBrowser

            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done
            int = int + 1

            CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(ToolStripTextBox1.Text)
            '   RestoreSave()


        ElseIf CheckURL(ToolStripTextBox1.Text) = False Then
            search()

        End If
    End Sub
    Public Sub Search()
        Dim brws As New GeckoWebBrowser
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 0.5
        CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate(My.Settings.SearchEngine & ToolStripTextBox1.Text)
    End Sub
    Public Function CheckURL(ByVal urltocheck As String)

        Dim url As New System.Uri("http://" & urltocheck)
        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)
        Dim resp As System.Net.WebResponse
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            Return True
        Catch ex As Exception
            req = Nothing
            Return False
        End Try
    End Function
    Public Sub LoadUpSettings()


        ToolStripTextBox1.ShortcutsEnabled = False
        Try
            Dim tab As New TabPage
            Dim brws As New GeckoWebBrowser
            brws.Dock = DockStyle.Fill
            tab.Text = " New Tab"
            tab.Controls.Add(brws)
            Me.TabControl1.TabPages.Add(tab)
            Me.TabControl1.SelectedTab = tab
            brws.Navigate(My.Settings.HomeSpace)

            AddHandler brws.ProgressChanged, AddressOf Loading
            AddHandler brws.DocumentCompleted, AddressOf Done

            'Test line below

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Navigation Events"

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click


        Dim s As String = ToolStripTextBox1.Text
        Dim fHasSpace As Boolean = s.Contains(" ")

        If fHasSpace = True Then
            search()
        ElseIf fHasSpace = False Then
            UrlNavigate()
        End If


    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click

        Dim brws As New GeckoWebBrowser
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 1
        CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GoBack()
    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click

        Dim brws As New GeckoWebBrowser
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 1
        CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GoForward()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim brws As New GeckoWebBrowser

        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 0.5
        CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Reload()
    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click

        Dim tab As New TabPage
        Dim brws As New GeckoWebBrowser
        brws.Dock = DockStyle.Fill
        tab.Text = " New Tab"
        tab.Controls.Add(brws)
        Me.TabControl1.TabPages.Add(tab)
        Me.TabControl1.SelectedTab = tab
        brws.Navigate(My.Settings.HomeSpace)
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 1

    End Sub

    Private Sub LightBrowseMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    If My.Settings.SafeBrowsing = "True" Then
            ToolStripStatusLabel2.Text = "Safe-Mode: Enabled"
            ToolStripStatusLabel2.ForeColor = Color.Green
        ElseIf My.Settings.SafeBrowsing = "False" Then
            ToolStripStatusLabel2.Text = "Safe-Mode: Disabled"
            ToolStripStatusLabel2.ForeColor = Color.Red

        End If
        ToolStripDropDownButton2.Alignment =
            System.Windows.Forms.ToolStripItemAlignment.Right
        LoadUpSettings()
        If My.Settings.LicenseAknowledge = "True" Then
            'Load up normally
        ElseIf My.Settings.LicenseAknowledge = "False" Then
            LicenseAknowledge.Show()
        End If


        If My.Settings.SafeBrowsing = "True" Then
            SafeBrowsingToolStripMenuItem.Checked = True
        ElseIf My.Settings.SafeBrowsing = "False" Then
            SafeBrowsingToolStripMenuItem.Checked = False

        End If
        ToolStripTextBox1.Control.ContextMenuStrip = ContextMenuStrip1

    End Sub
    Public Sub MakeFullScreen()
        Me.SetVisibleCore(False)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        Me.SetVisibleCore(True)
    End Sub

    Private Sub Fullscreen_Click(sender As Object, e As EventArgs) Handles Fullscreen.Click
        MakeFullScreen()
        Fullscreen.Visible = False
        ExitFullScreen.Visible = True
    End Sub

    Private Sub ExitFullScreen_Click(sender As Object, e As EventArgs) Handles ExitFullScreen.Click
        Me.WindowState = FormWindowState.Normal
        Me.Size = New Drawing.Size(1446, 772)  ' MaximumSize
        FormBorderStyle = FormBorderStyle.Sizable
        Fullscreen.Visible = True
        ControlBox = True
        Me.TopMost = False
        ExitFullScreen.Visible = False
    End Sub
    Public Sub Save_History()
        Dim HistoryPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) &
                         "\.\LightBrowseData" &
                         My.Settings.History
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(HistoryPath, True)

        file.WriteLine(CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString())

        file.Close()
    End Sub



    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Dim tab As New TabPage
        Dim brws As New GeckoWebBrowser
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 0.5
        brws.Dock = DockStyle.Fill
        tab.Text = " New Tab"
        tab.Controls.Add(brws)
        Me.TabControl1.TabPages.Add(tab)
        Me.TabControl1.SelectedTab = tab
        brws.Navigate(My.Settings.HomeSpace)
        My.Settings.Save()
        My.Settings.Reload()
    End Sub

    Private Sub ToolStripTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox1.KeyDown

        If (e.KeyCode = Keys.Enter) Then

            Dim s As String = ToolStripTextBox1.Text
            Dim fHasSpace As Boolean = s.Contains(" ")

            If fHasSpace = True Then
                search()
            ElseIf fHasSpace = False Then
                UrlNavigate()
            End If
        End If

    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Settings.Show()
    End Sub

    Private Sub NewWindowToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewWindowToolStripMenuItem1.Click
        Dim d As New LightBrowseMain
        d.Show()

    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Navigate("javascript:print()")

    End Sub

    Private Sub HistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HistoryToolStripMenuItem.Click
        History.Show()

    End Sub

    Private Sub UpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdatesToolStripMenuItem.Click
           Dim proc As New System.Diagnostics.Process()
 
        proc = Process.Start("C:\Program Files (x86)\BeffsBrowser\LightBrowse Beta\updater.exe", "")

    End Sub

    Private Sub LicenseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LicenseToolStripMenuItem.Click
        LicenseViewer.Show()

    End Sub



    Private Sub ToolStripLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripLabel2.Click
        Updates.Show()

    End Sub

    Private Sub SafeBrowsingToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles SafeBrowsingToolStripMenuItem.CheckedChanged
       If SafeBrowsingToolStripMenuItem.Checked = True Then
            My.Settings.SafeBrowsing = "True"
            ToolStripStatusLabel2.Text = "Safe-Mode: Enabled"
            ToolStripStatusLabel2.ForeColor = Color.ForestGreen
        ElseIf SafeBrowsingToolStripMenuItem.Checked = False Then
            My.Settings.SafeBrowsing = "False"
            ToolStripStatusLabel2.Text = "Safe-Mode: Disabled"
            ToolStripStatusLabel2.ForeColor = Color.Red
        End If
    End Sub

    Private Sub LightBrowseMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim shouldWarn = (My.Settings.TabCloseWarning = "Yes")
        Dim hasTabs = (TabControl1.TabCount >= 2)

        If shouldWarn AndAlso hasTabs Then
            Dim shouldCloseResult = MessageBox.Show("You have 2 or more tabs open. Are you sure you wanna exit?" & vbNewLine & "A Total of" & " " & TabControl1.TabCount & " " & "Tabs will be closed", "Closing Multi-Tabbed Window", MessageBoxButtons.YesNo)

            If shouldCloseResult = DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click

    End Sub

    Private Sub FullScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullScreenToolStripMenuItem.Click
        Fullscreen.PerformClick()
    End Sub

    Private Sub ExitFullScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitFullScreenToolStripMenuItem.Click
        ExitFullScreen.PerformClick()

    End Sub

    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        ToolStripButton9.PerformClick()

    End Sub

    Private Sub ForwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForwardToolStripMenuItem.Click
        ToolStripButton11.PerformClick()

    End Sub

    Private Sub NewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTabToolStripMenuItem.Click
        ToolStripButton10.PerformClick()

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        ToolStripButton1.PerformClick()

    End Sub

  

    Private Sub TabControl1_ControlAdded(sender As Object, e As ControlEventArgs) Handles TabControl1.ControlAdded
        If TabControl1.TabCount <= 50 Then
            ToolStripLabel1.ForeColor = Color.ForestGreen
        ElseIf TabControl1.TabCount >= 50 Then
            ToolStripLabel1.ForeColor = Color.Gold
        ElseIf TabControl1.TabCount <= 250 Then
            ToolStripLabel1.ForeColor = Color.Red
        ElseIf TabControl1.TabCount >= 349 Then
            ToolStripLabel1.ForeColor = Color.Red
            ToolStripButton10.Enabled = False
            ToolStripButton10.Visible = False

        End If
        ToolStripLabel1.Text = TabControl1.TabCount & " " & "Tab(s) Opened"

    End Sub

    Private Sub TabControl1_ControlRemoved(sender As Object, e As ControlEventArgs) Handles TabControl1.ControlRemoved
        ToolStripLabel1.Text = TabControl1.TabCount - 1 & " " & "Tab(s) Opened"
        If TabControl1.TabCount <= 50 Then
            ToolStripLabel1.ForeColor = Color.ForestGreen
        ElseIf TabControl1.TabCount >= 50 Then
            ToolStripLabel1.ForeColor = Color.Gold
        ElseIf TabControl1.TabCount <= 348 Then
            ToolStripLabel1.ForeColor = Color.Red
        ElseIf TabControl1.TabCount >= 349 Then
            ToolStripLabel1.ForeColor = Color.Red
            ToolStripButton10.Visible = False

        End If
    End Sub

    Private Sub ToolStripTextBox1_MouseHover(sender As Object, e As EventArgs) Handles ToolStripTextBox1.MouseHover

        ToolStripTextBox1.TextBox.Width = 700
    End Sub

    Private Sub ToolStripTextBox1_MouseLeave(sender As Object, e As EventArgs) Handles ToolStripTextBox1.MouseLeave
        ToolStripTextBox1.TextBox.Width = 650
    End Sub


#Region "URL Box shortcuts"
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If ToolStripTextBox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(ToolStripTextBox1.SelectedText)

        Else


        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
     
        ToolStripTextBox1.SelectedText = ""
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If My.Computer.Clipboard.ContainsText Then
            ToolStripTextBox1.Paste()
        End If
    End Sub

    Private Sub PasteAndGoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteAndGoToolStripMenuItem.Click
        If My.Computer.Clipboard.ContainsText Then
            ToolStripTextBox1.Paste()
            Dim s As String = ToolStripTextBox1.Text
            Dim fHasSpace As Boolean = s.Contains(" ")

            If fHasSpace = True Then
                search()
            ElseIf fHasSpace = False Then
                UrlNavigate()

            End If
        End If
    End Sub



    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        ToolStripTextBox1.SelectAll()

    End Sub

    Private Sub ZoomInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomInToolStripMenuItem.Click
        CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GetDocShellAttribute.GetContentViewerAttribute.SetFullZoomAttribute(CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GetDocShellAttribute.GetContentViewerAttribute.GetFullZoomAttribute + CSng(0.1))

    End Sub

    Private Sub ZoomOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomOutToolStripMenuItem.Click
        CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GetDocShellAttribute.GetContentViewerAttribute.SetFullZoomAttribute(CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GetDocShellAttribute.GetContentViewerAttribute.GetFullZoomAttribute - CSng(0.1))

    End Sub

    Private Sub ResetZoomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetZoomToolStripMenuItem.Click
        CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).GetDocShellAttribute.GetContentViewerAttribute.SetFullZoomAttribute(1)

    End Sub

 
#End Region






#End Region
End Class

