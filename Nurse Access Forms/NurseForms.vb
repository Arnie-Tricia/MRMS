Public Class NurseForms
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub imgback_Click(sender As Object, e As EventArgs)
        Me.Hide()
        Frontpage.Show()
    End Sub

    Private Sub btnPatientPI_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With PatientPI
            .TopLevel = False
            Panel1.Controls.Add(PatientPI)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub NurseForms_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (Me.Width - Panel1.Width) / 5
    End Sub
End Class
