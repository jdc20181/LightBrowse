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

Public Class Settings
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.BackColor = ColorTranslator.FromHtml("#1f92d6")
        TextBox1.Text = My.Settings.HomeSpace
        If My.Settings.SearchEngine = "http://www.bing.com/search?q=" Then
            CheckBox2.Checked = True
            CheckBox1.Enabled = False
            CheckBox8.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
        ElseIf My.Settings.SearchEngine = "https://www.google.com/?gws_rd=ssl#q=" Then
            CheckBox1.Checked = True
            CheckBox2.Enabled = False
            CheckBox8.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
        ElseIf My.Settings.SearchEngine = "http://www.aolsearch.com/search?s_it=sb-home&v_t=na&q=" Then
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox8.Enabled = False
            CheckBox4.Checked = True
            CheckBox5.Enabled = False
        ElseIf My.Settings.SearchEngine = "http://www.ask.com/web?q=" Then
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox8.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Checked = True
        ElseIf My.Settings.SearchEngine = "https://duckduckgo.com/?q=" Then
            CheckBox3.Checked = True
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox8.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
        ElseIf My.Settings.SearchEngine = "https://search.yahoo.com/search;_ylt=AwrBT7q_XaRZLU8AXCel87UF;_ylc=X1MDOTU4MTA0NjkEX3IDMgRmcgN0ZXN0BGdwcmlkAy5wMzRHblUuU1BhbkF0TjM3S2dQTkEEbl9yc2x0AzAEbl9zdWdnAzEwBG9yaWdpbgNzZWFyY2gueWFob28uY29tBHBvcwMxBHBxc3RyA3QEcHFzdHJsAzEEcXN0cmwDNARxdWVyeQN0ZXN0BHRfc3RtcAMxNTAzOTQ0MTMx?p=" Then
            CheckBox8.Checked = True
            CheckBox3.Enabled = False
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False

            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            My.Settings.SearchEngine = "http://www.aolsearch.com/search?s_it=sb-home&v_t=na&q="
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox3.Enabled = False
            CheckBox8.Enabled = False

            CheckBox5.Enabled = False
            My.Settings.Save()
        ElseIf CheckBox4.Checked = False Then
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
            CheckBox8.Enabled = True

            CheckBox5.Enabled = True
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            My.Settings.SearchEngine = "http://www.ask.com/web?q="
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox3.Enabled = False
            CheckBox8.Enabled = False

            CheckBox4.Enabled = False
            My.Settings.Save()

        ElseIf CheckBox5.Checked = False Then
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
            CheckBox8.Enabled = True

            CheckBox4.Enabled = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            My.Settings.SearchEngine = "https://www.google.com/?gws_rd=ssl#q="
            My.Settings.Save()

            CheckBox2.Enabled = False
            CheckBox3.Enabled = False
            CheckBox8.Enabled = False

            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
        ElseIf CheckBox1.Checked = False Then
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
            CheckBox8.Enabled = True

            CheckBox4.Enabled = True
            CheckBox5.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            My.Settings.SearchEngine = "http://www.bing.com/search?q="
            My.Settings.Save()

            CheckBox1.Enabled = False
            CheckBox3.Enabled = False
            CheckBox8.Enabled = False

            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
        ElseIf CheckBox2.Checked = False Then
            CheckBox1.Enabled = True
            CheckBox3.Enabled = True
            CheckBox8.Enabled = True
            CheckBox4.Enabled = True
            CheckBox5.Enabled = True
        End If
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
            CheckBox8.Enabled = False
            My.Settings.SearchEngine = "https://duckduckgo.com/?q="
            My.Settings.Save()

        ElseIf CheckBox3.Checked = False Then
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            CheckBox4.Enabled = True
            CheckBox5.Enabled = True
            CheckBox8.Enabled = True
        End If
    End Sub
    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
            CheckBox3.Enabled = False
            My.Settings.SearchEngine = "https://search.yahoo.com/search;_ylt=A0LEVzenXaRZSaMA2Dil87UF;_ylc=X1MDOTU4MTA0NjkEX3IDMgRmcgMEZ3ByaWQDbXp3eG9nWnJRUU9OcDhDTzR6RlhCQQRuX3JzbHQDMARuX3N1Z2cDOQRvcmlnaW4Dc2VhcmNoLnlhaG9vLmNvbQRwb3MDMARwcXN0cgMEcHFzdHJsAwRxc3RybAM0BHF1ZXJ5A3Rlc3QEdF9zdG1wAzE1MDM5NDQxMDg-?p="
            My.Settings.Save()

        ElseIf CheckBox8.Checked = False Then
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            CheckBox4.Enabled = True
            CheckBox5.Enabled = True
            CheckBox3.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.HomeSpace = TextBox1.Text
        My.Settings.Save()

    End Sub
End Class
