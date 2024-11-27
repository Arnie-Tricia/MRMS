Imports MySql.Data.MySqlClient
Public Class manageuser
    Dim con As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim dt As New DataTable
    Dim sql As String
    Dim first As String
    Dim second As String
    Dim column
    Private Sub load_data()
        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            sql = "Select * from tbl_nurselist"
            cmd = New MySqlCommand(sql, con)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)
            dtg.DataSource = dt
            column = dtg.Columns.Count - 5
            dtg.Columns(column).Visible = False
            dtg.Columns(0).Visible = False
            dtg.Columns(4).Visible = False

            Label2.Text = dt.Rows.Count

            For Each col As DataGridViewColumn In dtg.Columns
                col.DefaultCellStyle.Padding = New Padding(5)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                col.HeaderCell.Style.Font = New Font("Segoe UI Historic", 10, FontStyle.Bold)
                dtg.DefaultCellStyle.BackColor = Color.White
                dtg.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64)
                dtg.DefaultCellStyle.Font = New Font("Segoe UI Historic", 10)
                dtg.ColumnHeadersDefaultCellStyle.BackColor = Color.White
                dtg.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(45, 73, 112)
                dtg.EnableHeadersVisualStyles = False
                dtg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                dtg.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 73, 112)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()

    End Sub
    Private Sub manageuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_data()
    End Sub

    Private Sub dtg_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtg.CellContentDoubleClick
        Try
            With dtg.CurrentRow()
                TextBox1.Text = .Cells(0).Value
                TextBox3.Text = .Cells(1).Value
                TextBox4.Text = .Cells(2).Value
                TextBox5.Text = .Cells(3).Value
                TextBox6.Text = .Cells(4).Value

            End With
            Button8.Enabled = True
            Button2.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            If TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox2.Text = "" Then
                MsgBox("Fill Up All Information", MsgBoxStyle.Information, "Fill Up All Information")
            ElseIf TextBox6.Text = TextBox2.Text Then
                sql = "UPDATE tbl_nurselist SET FirstName = @firstName, LastName = @lastName, Username = @username, Password = @password WHERE ID_Nurse = @idNurse"
                cmd = New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@firstName", TextBox3.Text)
                cmd.Parameters.AddWithValue("@lastName", TextBox4.Text)
                cmd.Parameters.AddWithValue("@username", TextBox5.Text)
                cmd.Parameters.AddWithValue("@password", TextBox6.Text)
                cmd.Parameters.AddWithValue("@idNurse", TextBox1.Text)
                cmd.ExecuteNonQuery()

                MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Updated")
                TextBox1.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox5.Clear()
                TextBox6.Clear()
                TextBox2.Clear()
            Else
                MessageBox.Show("Password not matched")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
        load_data()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            Using con As New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
                con.Open()

                If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox5.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox6.Text) Then
                    MsgBox("Fill Up All Information", MsgBoxStyle.Information, "Fill Up All Information")
                ElseIf TextBox6.Text = TextBox2.Text Then
                    Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        Dim sql As String = "DELETE FROM tbl_nurselist WHERE ID_Nurse = @ID_Nurse;"
                        Using cmd As New MySqlCommand(sql, con)
                            cmd.Parameters.AddWithValue("@ID_Nurse", TextBox1.Text)
                            cmd.ExecuteNonQuery()
                        End Using

                        MsgBox("Successfully Deleted!", MsgBoxStyle.Exclamation)
                        TextBox1.Clear()
                        TextBox3.Clear()
                        TextBox4.Clear()
                        TextBox5.Clear()
                        TextBox6.Clear()
                        TextBox2.Clear()
                    End If
                Else
                    MessageBox.Show("Password not matched")
                End If
            End Using
            load_data()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            If TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox2.Text = "" Then
                MsgBox("Fill Up All Information", MsgBoxStyle.Information, "Fill Up All Information")
            Else
                If TextBox6.Text = TextBox2.Text Then
                    Dim sql As String = "SELECT COUNT(*) FROM tbl_nurselist WHERE Username = @username"
                    Dim cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@username", TextBox5.Text)
                    Dim count As Integer = CInt(cmd.ExecuteScalar())

                    If count > 0 Then
                        MsgBox("Username already exists", MsgBoxStyle.Information, "Username Exists")
                    Else
                        sql = "INSERT INTO tbl_nurselist(FirstName, LastName, Username, Password) VALUES (@firstName, @lastName, @username, @password)"
                        cmd = New MySqlCommand(sql, con)
                        cmd.Parameters.AddWithValue("@firstName", TextBox3.Text)
                        cmd.Parameters.AddWithValue("@lastName", TextBox4.Text)
                        cmd.Parameters.AddWithValue("@username", TextBox5.Text)
                        cmd.Parameters.AddWithValue("@password", TextBox6.Text)
                        cmd.ExecuteNonQuery()

                        MsgBox("New user added!", MsgBoxStyle.Information, "Successfully Added!")
                        TextBox1.Clear()
                        TextBox3.Clear()
                        TextBox4.Clear()
                        TextBox5.Clear()
                        TextBox6.Clear()
                        TextBox2.Clear()
                    End If
                Else
                    MessageBox.Show("Password not matched")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

        load_data()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox2.Clear()
        Button8.Enabled = False
        Button2.Enabled = False
    End Sub
End Class