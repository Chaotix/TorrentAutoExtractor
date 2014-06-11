Public Class MsgBoxTimer
    Dim MsgBoxAnswer As Boolean = False, TimeUp As Boolean
    Dim Countdown As Integer = 0
    Dim ButtonTimer As String

    Public Sub ShowMsg(ByVal BoxInCenterOrParent As String, ByVal questionStr As String, Optional ByVal Timer As Integer = 0, Optional ByVal TimerOnYesOrNo As String = "", Optional ByVal TitleBar As String = "")
        TimeUp = False

        If BoxInCenterOrParent.ToUpper = "PARENT" Then
            Me.StartPosition = FormStartPosition.CenterParent
        Else
            Me.StartPosition = FormStartPosition.CenterScreen
        End If
        btnYes.Text = "Yes"
        btnNo.Text = "No"
        Me.Text = TitleBar
        lblQuestion.Text = questionStr
        If Timer > 0 And (TimerOnYesOrNo.ToUpper = "YES" Or TimerOnYesOrNo.ToUpper = "NO") Then
            Countdown = Timer
            ButtonTimer = TimerOnYesOrNo.ToUpper
            btnTimer.Enabled = True
            If ButtonTimer = "YES" Then btnYes.Text = "Yes (" & Countdown & ")"
            If ButtonTimer = "NO" Then btnNo.Text = "No (" & Countdown & ")"
        End If
        Me.ShowDialog()
    End Sub

    Private Sub MsgBoxTimer_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If TimeUp Then
            btnTimer.Enabled = False
            If ButtonTimer = "YES" Then MsgBoxAnswer = True Else MsgBoxAnswer = False
        End If
    End Sub

    Public ReadOnly Property MsgAnswer() As Boolean
        Get
            Return MsgBoxAnswer
        End Get
    End Property

    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        MsgBoxAnswer = True
        Me.Close()
    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        MsgBoxAnswer = False
        Me.Close()
    End Sub

    Private Sub btntimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimer.Tick
        Countdown -= 1
        If ButtonTimer = "YES" Then btnYes.Text = "Yes (" & Countdown & ")"
        If ButtonTimer = "NO" Then btnNo.Text = "No (" & Countdown & ")"
        If Countdown <= 0 Then Me.Close()
    End Sub

End Class