Imports MySql.Data.MySqlClient
Public Class patientlogout
    Dim con As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim dt As New DataTable
    Dim sql As String
    Dim first As String
    Dim second As String
    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Me.Hide()
        patienttimein_out.Show()
        'TextBox1.Text = "Student ID"
        'tbuname.Text = "Username"
        'tbpass.Text = "Password"
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If MessageBox.Show("Are you sure you want to exit the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub patientlogout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        con.Open()
        Timer1.Enabled = True
        'TextBox1.Text = "Student ID"
        'tbuname.Text = "Username"
        'tbpass.Text = "Password"
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = DateTime.Now.ToString("yyyy-MM-dd")
        Label2.Text = DateTime.Now.ToString("t")
    End Sub
    Private Sub tbuname_MouseClick(sender As Object, e As MouseEventArgs) Handles tbuname.MouseClick
        'If tbuname.Text = "Username" Then
        '    tbuname.Clear()
        'End If
    End Sub

    Private Sub tbpass_MouseClick(sender As Object, e As MouseEventArgs) Handles tbpass.MouseClick
        'If tbpass.Text = "Password" Then
        '    tbpass.Clear()
        '    tbpass.PasswordChar = "*"
        'End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrEmpty(TextBox1.Text) OrElse String.IsNullOrEmpty(tbuname.Text) OrElse String.IsNullOrEmpty(tbpass.Text) Then
            MsgBox("Please enter all the required information.", MsgBoxStyle.Exclamation)
        End If

        Try
            sql = "Select * from tbl_patientacc where Student_ID = '" & TextBox1.Text & "' and username = '" & tbuname.Text & "' and password = '" & tbpass.Text & "'"
            cmd = New MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)


            If dt.Rows.Count > 0 Then
                first = dt.Rows(0).Item(1)
                second = dt.Rows(0).Item(2)

                sql = "update tbl_patientlog set time_out = '" & TimeOfDay & "' WHERE P_Date= '" & Label5.Text & "' and Student_ID = '" & TextBox1.Text & "'"
                cmd = New MySqlCommand(sql, con)
                da = New MySqlDataAdapter(cmd)
                dt = New DataTable()
                da.Fill(dt)

                MsgBox("Time Out Successfully!", MsgBoxStyle.Information)
                Me.Hide()
                TextBox1.Clear()
                tbuname.Clear()
                tbpass.Clear()
                Frontpage.Show()
            Else
                MsgBox("Incorrect Student ID, Username or Password", MsgBoxStyle.Critical)
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()
        Call patientlogout_Load(sender, e)
    End Sub
    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        'If TextBox1.Text = "Student ID" Then
        '    TextBox1.Clear()
        'End If
    End Sub
End Class