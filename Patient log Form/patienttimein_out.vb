Public Class patienttimein_out
    Private Sub btnstudent_Click(sender As Object, e As EventArgs) Handles btnstimein.Click
        Me.Hide()
        patientlogin.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btntimeout.Click
        Me.Hide()
        patientlogout.Show()
    End Sub
    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Me.Hide()
        Frontpage.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub
End Class