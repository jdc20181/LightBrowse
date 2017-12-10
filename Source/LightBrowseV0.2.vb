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
        ToolStripProgressBar1.Maximum = e.MaximumProgress
        ToolStripProgressBar1.Value = e.MaximumProgress
        Me.Cursor = Cursors.AppStarting

    End Sub

    Private Sub Done(ByVal sender As Object, ByVal e As Gecko.Events.GeckoDocumentCompletedEventArgs)

        Me.Cursor = Cursors.Default
        ToolStripTextBox1.Text = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url.ToString

        Save_History()

        LightPage()

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



        ElseIf CheckURL(ToolStripTextBox1.Text) = False Then
            search()

        End If
    End Sub
    Public Sub search()
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

        ToolStripDropDownButton2.Alignment =
            System.Windows.Forms.ToolStripItemAlignment.Right
        LoadUpSettings()
   

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

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim tab As New TabPage
        Dim brws As New GeckoWebBrowser
        brws.Dock = DockStyle.Fill
        tab.Text = " New Tab"
        tab.Controls.Add(brws)
        Me.TabControl1.TabPages.Add(tab)
        Me.TabControl1.SelectedTab = tab
        brws.Navigate("http://bbstart.tk/lightpage/")
        AddHandler brws.ProgressChanged, AddressOf Loading
        AddHandler brws.DocumentCompleted, AddressOf Done
        int = int + 1
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
        Updates.Show()

    End Sub



    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        'My.Settings.Reset()

    End Sub

    Private Sub LicenseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LicenseToolStripMenuItem.Click
        LicenseViewer.Show()

    End Sub


'LightPage is in Testing phase and is not released, the following code is for demostration purposes only and is not entirely in functioning capacity
    Public Sub LightPage()
        If CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser).Url = New Uri("http://bbstart.tk/lightpage") Then
            Dim nameIndex, urlIndex As Integer
            'My.Settings.StartPageName1, My.Settings.StartPageName2, My.Settings.StartPageName3, My.Settings.StartPageName4, My.Settings.StartPageName5, My.Settings.StartPageName6, My.Settings.StartPageName7, My.Settings.StartPageName8, My.Settings.StartPageName9, My.Settings.StartPageName10
            'My.Settings.StartPage1, My.Settings.StartPage2, My.Settings.StartPage3, My.Settings.StartPage4, My.Settings.StartPage5, My.Settings.StartPage6, My.Settings.StartPage7, My.Settings.StartPage8, My.Settings.StartPage9, My.Settings.StartPage10
            Dim names() As String = {"Google"}
            Dim urls() As String = {"https://google.com"}
            Dim wb As GeckoWebBrowser = CType(TabControl1.SelectedTab.Controls.Item(0), GeckoWebBrowser)

            Dim siteNodes = wb.Document.GetElementsByClassName("url")
            For Each n As GeckoHtmlElement In siteNodes
                'n.TextContent = "Site"
                n.TextContent = names(nameIndex)
                nameIndex += 1
            Next

            Dim urlNodes = wb.Document.GetElementsByClassName("url")
            For Each n As GeckoHtmlElement In urlNodes
                n.TextContent = urls(urlIndex)
                n.SetAttribute("href", urls(urlIndex))
                urlIndex += 1
            Next
        End If
    End Sub



#End Region
End Class
