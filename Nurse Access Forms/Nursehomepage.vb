Public Class Nursehomepage

    Private Sub Nursehomepage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.Text = Date.Now.ToString("f")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        With Addpatient
            .TopLevel = False
            Panelcontainer.Controls.Add(Addpatient)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With PatientPI
            .TopLevel = False
            Panelcontainer.Controls.Add(PatientPI)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With patientlog
            .TopLevel = False
            Panelcontainer.Controls.Add(patientlog)
            .BringToFront()
            .Show()
        End With

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With medsinventory
            .TopLevel = False
            Panelcontainer.Controls.Add(medsinventory)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With manageuser
            .TopLevel = False
            Panelcontainer.Controls.Add(manageuser)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        With patientpresc
            .TopLevel = False
            Panelcontainer.Controls.Add(patientpresc)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Panelcontainer_Paint(sender As Object, e As PaintEventArgs) Handles Panelcontainer.Paint
        With title
            .TopLevel = False
            Panelcontainer.Controls.Add(title)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MessageBox.Show("Are you sure you want to logout?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Hide()
            nurselogin.Show()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        With title
            .TopLevel = False
            Panelcontainer.Controls.Add(title)
            .BringToFront()
            .Show()
        End With
    End Sub

    'Private Sub PictureBox6_Click(sender As Object, e As EventArgs)
    '    If MessageBox.Show("Are you sure you want to exit the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
    '        Application.Exit()
    '    End If
    'End Sub
End Class