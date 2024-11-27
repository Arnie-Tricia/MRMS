Imports MySql.Data.MySqlClient

Public Class patientlogin

    Private con As MySqlConnection
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private dt As DataTable
    Private sql As String
    Private first As String
    Private second As String

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Hide()
        Frontpage.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If MessageBox.Show("Are you sure you want to exit the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub patientlogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        con.Open()
        Timer1.Enabled = True
        'TextBox1.Text = "Student ID"
        'tbuname.Text = "Username"
        'tbpass.Text = "Password"
        ''tbuname.Focus()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = DateTime.Now.ToString("yyyy-MM-dd")
        Label3.Text = DateTime.Now.ToString("t")
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Me.Hide()
        patienttimein_out.Show()
        'TextBox1.Text = "Student ID"
        'tbuname.Text = "Username"
        'tbpass.Text = "Password"
        RichTextBox1.Text = ""
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

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(tbuname.Text) Or String.IsNullOrWhiteSpace(tbpass.Text) Or String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MsgBox("Please enter all the required information", MsgBoxStyle.Critical)
            Return
        End If

        Try
            sql = "Select * from tbl_patientacc where Student_ID = @student_id and username = @username and password = @password"
            cmd = New MySqlCommand(sql, con)
            cmd.Parameters.AddWithValue("@student_id", TextBox1.Text)
            cmd.Parameters.AddWithValue("@username", tbuname.Text)
            cmd.Parameters.AddWithValue("@password", tbpass.Text)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                first = dt.Rows(0).Item(1)
                second = dt.Rows(0).Item(2)
                sql = "insert into tbl_patientlog(Student_ID, username, password, purpose, time_in, Time_InStatus, P_DATE) 
                       values (@student_id,@username, @password, @purpose, @timeIn, 'Time In', @pDate)"
                cmd = New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@student_id", TextBox1.Text)
                cmd.Parameters.AddWithValue("@username", tbuname.Text)
                cmd.Parameters.AddWithValue("@password", tbpass.Text)
                cmd.Parameters.AddWithValue("@purpose", RichTextBox1.Text)
                cmd.Parameters.AddWithValue("@timeIn", DateTime.Now.ToString("hh:mm:ss tt"))
                cmd.Parameters.AddWithValue("@pDate", DateTime.Now.ToString("yyyy-MM-dd"))
                cmd.ExecuteNonQuery()
                MsgBox("Time In Successfully!", MsgBoxStyle.Information)
                Me.Hide()
                TextBox1.Clear()
                tbuname.Clear()
                tbpass.Clear()
                RichTextBox1.Clear()
                Frontpage.Show()
            Else
                MsgBox("Incorrect Student ID, Username or Password", MsgBoxStyle.Critical)
            End If

        Catch ex As MySqlException
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try

        Call patientlogin_Load(sender, e)
    End Sub

    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        'If TextBox1.Text = "Student ID" Then
        '    TextBox1.Clear()
        'End If
    End Sub
End Class
