Imports MySql.Data.MySqlClient
Public Class viewpatient
    Dim con As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim dt As New DataTable
    Dim sql As String
    Dim first As String
    Dim second As String
    Dim column As String

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim connectionString As String = "server=localhost;user id=root;password=;database=mrmsdb;sslMode=none"
        Dim query As String = "SELECT * FROM tbl_patientmh WHERE Student_ID = @id"
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@id", TextBox23.Text)
                Using adapter As New MySqlDataAdapter(command)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    If dataTable.Rows.Count > 0 Then
                        Dim row As DataRow = dataTable.Rows(0)
                        TextBox3.Text = row("Student_ID").ToString()
                        TextBox5.Text = row("Diseases").ToString()
                        TextBox12.Text = row("Allergies").ToString()
                        TextBox2.Text = row("Meds").ToString()
                        TextBox4.Text = row("Hospitalization").ToString()
                        TextBox13.Text = row("Menstrual_Period").ToString()
                        TextBox16.Text = row("Height").ToString()
                        TextBox10.Text = row("Weight").ToString()
                        TextBox8.Text = row("BMI").ToString()
                        TextBox7.Text = row("BP").ToString()
                        TextBox9.Text = row("PR").ToString()
                        TextBox6.Text = row("TEMP").ToString()
                        RichTextBox1.Text = row("HEAD").ToString()
                        RichTextBox2.Text = row("EAR").ToString()
                        RichTextBox3.Text = row("EYES").ToString()
                        RichTextBox4.Text = row("NOSE").ToString()
                        RichTextBox5.Text = row("TEETH").ToString()
                        RichTextBox6.Text = row("CHEST_LUNGS").ToString()
                        RichTextBox7.Text = row("HEART").ToString()
                        RichTextBox8.Text = row("ABDOMEN").ToString()
                        TextBox17.Text = row("EXTREMITIES").ToString()
                        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
                            TabControl1.SelectedIndex += 1
                        End If
                    Else
                        MessageBox.Show("Record not found.")
                        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
                            TabControl1.SelectedIndex += 1
                        End If
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Using con As New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            Dim sql As String = "SELECT * FROM tbl_patientacc WHERE Student_ID = @id"
            Using cmd As New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@id", TextBox23.Text)

                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        TextBox32.Text = row("Student_ID").ToString()
                        TextBox33.Text = row("username").ToString()
                        TextBox34.Text = row("password").ToString()

                        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
                            TabControl1.SelectedIndex += 1
                        End If
                    Else
                        MessageBox.Show("Record not found.")
                        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
                            TabControl1.SelectedIndex += 1
                        End If
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If TabControl1.SelectedIndex > 0 Then
            TabControl1.SelectedIndex -= 1
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If TabControl1.SelectedIndex > 0 Then
            TabControl1.SelectedIndex -= 1
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        Panel1.Left = (Me.Width / Panel1.Width) / 5
    End Sub

    Private Sub viewpatient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_data()
        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            sql = "SELECT * FROM tbl_patientpi
        INNER JOIN tbl_patientmh ON tbl_patientpi.Student_ID = tbl_patientmh.Student_ID
        INNER JOIN tbl_patientacc ON tbl_patientmh.Student_ID = tbl_patientacc.Student_ID
        INNER JOIN tbl_patientlog ON tbl_patientacc.Student_ID = tbl_patientlog.Student_ID;"
            cmd = New MySqlCommand(sql, con)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()


    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Using con As New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslmode=none")
                con.Open()

                If String.IsNullOrEmpty(TextBox23.Text) OrElse String.IsNullOrEmpty(TextBox25.Text) OrElse
                   String.IsNullOrEmpty(TextBox27.Text) OrElse String.IsNullOrEmpty(ComboBox5.Text) OrElse
                   String.IsNullOrEmpty(TextBox30.Text) OrElse String.IsNullOrEmpty(DateTimePicker2.Text) OrElse
                   String.IsNullOrEmpty(TextBox31.Text) OrElse String.IsNullOrEmpty(ComboBox6.Text) OrElse
                   String.IsNullOrEmpty(TextBox29.Text) OrElse String.IsNullOrEmpty(ComboBox1.Text) OrElse
                   String.IsNullOrEmpty(ComboBox2.Text) OrElse String.IsNullOrEmpty(TextBox26.Text) OrElse
                   String.IsNullOrEmpty(TextBox24.Text) OrElse String.IsNullOrEmpty(TextBox22.Text) Then

                    MsgBox("fill all information")

                Else
                    Dim sql As String = "update tbl_patientpi set firstname = @firstname, lastname = @lastname, " &
                                        "sex = @sex, age = @age, birthday = @birthday, religion = @religion, " &
                                        "civil_status = @civil_status, contact_no = @contact_no, course = @course, " &
                                        "course_dept = @course_dept, guardian_name = @guardian_name, relationship = @relationship, " &
                                        "g_contact_no = @g_contact_no where student_id = @student_id"

                    Using cmd As New MySqlCommand(sql, con)
                        cmd.Parameters.AddWithValue("@firstname", TextBox25.Text)
                        cmd.Parameters.AddWithValue("@lastname", TextBox27.Text)
                        cmd.Parameters.AddWithValue("@sex", ComboBox5.Text)
                        cmd.Parameters.AddWithValue("@age", TextBox30.Text)
                        cmd.Parameters.AddWithValue("@birthday", DateTimePicker2.Value)
                        cmd.Parameters.AddWithValue("@religion", TextBox31.Text)
                        cmd.Parameters.AddWithValue("@civil_status", ComboBox6.Text)
                        cmd.Parameters.AddWithValue("@contact_no", TextBox29.Text)
                        cmd.Parameters.AddWithValue("@course", ComboBox1.Text)
                        cmd.Parameters.AddWithValue("@course_dept", ComboBox2.Text)
                        cmd.Parameters.AddWithValue("@guardian_name", TextBox26.Text)
                        cmd.Parameters.AddWithValue("@relationship", TextBox24.Text)
                        cmd.Parameters.AddWithValue("@g_contact_no", TextBox22.Text)
                        cmd.Parameters.AddWithValue("@student_id", TextBox23.Text)


                        cmd.ExecuteNonQuery()
                        MsgBox("successfully updated!", MsgBoxStyle.Information, "updated")
                    End Using

                End If
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Call viewpatient_Load(sender, e)

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            If String.IsNullOrEmpty(TextBox3.Text) OrElse String.IsNullOrEmpty(TextBox5.Text) OrElse
   String.IsNullOrEmpty(TextBox12.Text) OrElse String.IsNullOrEmpty(TextBox2.Text) OrElse
   String.IsNullOrEmpty(TextBox4.Text) OrElse String.IsNullOrEmpty(TextBox13.Text) OrElse
   String.IsNullOrEmpty(TextBox16.Text) OrElse String.IsNullOrEmpty(TextBox10.Text) OrElse
   String.IsNullOrEmpty(TextBox8.Text) OrElse String.IsNullOrEmpty(TextBox7.Text) OrElse
   String.IsNullOrEmpty(TextBox9.Text) OrElse String.IsNullOrEmpty(TextBox2.Text) OrElse
   String.IsNullOrEmpty(TextBox6.Text) OrElse String.IsNullOrEmpty(RichTextBox1.Text) OrElse
   String.IsNullOrEmpty(RichTextBox2.Text) OrElse String.IsNullOrEmpty(RichTextBox3.Text) OrElse
   String.IsNullOrEmpty(RichTextBox4.Text) OrElse String.IsNullOrEmpty(RichTextBox5.Text) OrElse
   String.IsNullOrEmpty(RichTextBox6.Text) OrElse String.IsNullOrEmpty(RichTextBox7.Text) OrElse
   String.IsNullOrEmpty(RichTextBox8.Text) OrElse String.IsNullOrEmpty(TextBox17.Text) Then

                MsgBox("Fill all information")
            Else
                sql = "UPDATE tbl_patientmh SET Diseases = @Diseases, Allergies = @Allergies, Meds = @Meds,
            Hospitalization = @Hospitalization, Menstrual_Period = @Menstrual_Period, Height = @Height, Weight = @Weight,
            BMI = @BMI, BP = @BP, PR = @PR, TEMP = @TEMP, HEAD = @HEAD, EAR = @EAR, EYES = @EYES, NOSE = @NOSE,
            TEETH = @TEETH, CHEST_LUNGS = @CHEST_LUNGS, HEART = @HEART, ABDOMEN = @ABDOMEN, EXTREMITIES = @EXTREMITIES
           WHERE Student_Id = @Student_Id"

                cmd = New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@Diseases", TextBox5.Text)
                cmd.Parameters.AddWithValue("@Allergies", TextBox12.Text)
                cmd.Parameters.AddWithValue("@Meds", TextBox2.Text)
                cmd.Parameters.AddWithValue("@Hospitalization", TextBox4.Text)
                cmd.Parameters.AddWithValue("@Menstrual_Period", TextBox13.Text)
                cmd.Parameters.AddWithValue("@Height", TextBox16.Text)
                cmd.Parameters.AddWithValue("@Weight", TextBox10.Text)
                cmd.Parameters.AddWithValue("@BMI", TextBox7.Text)
                cmd.Parameters.AddWithValue("@BP", TextBox9.Text)
                cmd.Parameters.AddWithValue("@PR", TextBox2.Text)
                cmd.Parameters.AddWithValue("@TEMP", TextBox6.Text)
                cmd.Parameters.AddWithValue("@HEAD", RichTextBox1.Text)
                cmd.Parameters.AddWithValue("@EAR", RichTextBox2.Text)
                cmd.Parameters.AddWithValue("@EYES", RichTextBox3.Text)
                cmd.Parameters.AddWithValue("@NOSE", RichTextBox4.Text)
                cmd.Parameters.AddWithValue("@TEETH", RichTextBox5.Text)
                cmd.Parameters.AddWithValue("@CHEST_LUNGS", RichTextBox6.Text)
                cmd.Parameters.AddWithValue("@HEART", RichTextBox7.Text)
                cmd.Parameters.AddWithValue("@ABDOMEN", RichTextBox8.Text)
                cmd.Parameters.AddWithValue("@EXTREMITIES", TextBox17.Text)
                cmd.Parameters.AddWithValue("@Student_Id", TextBox23.Text)

                cmd.ExecuteNonQuery()
                MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Updated")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Call viewpatient_Load(sender, e)
        con.Close()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Try
            Dim connectionString As String = "server=localhost;user id=root;password=;database=mrmsdb;sslMode=none"
            Using con As MySqlConnection = New MySqlConnection(connectionString)
                con.Open()

                If String.IsNullOrEmpty(TextBox32.Text) OrElse String.IsNullOrEmpty(TextBox33.Text) OrElse String.IsNullOrEmpty(TextBox34.Text) Then
                    MsgBox("Fill all information")
                ElseIf TextBox34.Text = TextBox11.Text Then
                    Dim sql As String = "UPDATE tbl_patientacc SET username = @username, password = @password WHERE Student_Id = @studentId"
                    Using cmd As MySqlCommand = New MySqlCommand(sql, con)
                        cmd.Parameters.AddWithValue("@username", TextBox33.Text)
                        cmd.Parameters.AddWithValue("@password", TextBox34.Text)
                        cmd.Parameters.AddWithValue("@studentId", TextBox32.Text)
                        cmd.ExecuteNonQuery()
                    End Using
                    MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Updated")
                Else
                    MsgBox("Password not matched!", MsgBoxStyle.Information, "Not matched")
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Call viewpatient_Load(sender, e)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TabControl1.SelectedIndex > 0 Then
            TabControl1.SelectedIndex -= 1
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim connectionString As String = "server=localhost;user id=root;password=;database=mrmsdb;sslMode=none"
        Dim query As String = "SELECT * FROM tbl_patientlog WHERE Student_ID = @id"
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@id", TextBox23.Text)
                Using adapter As New MySqlDataAdapter(command)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    If dataTable.Rows.Count > 0 Then
                        Dim row As DataRow = dataTable.Rows(0)
                        TextBox1.Text = row("Student_ID").ToString()
                        Load_data()
                        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
                            TabControl1.SelectedIndex += 1
                        End If
                    Else
                        MessageBox.Show("No Record found.")
                        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
                            TabControl1.SelectedIndex += 1
                        End If
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub Load_data()

        Try
            Dim connectionString As String = "server=localhost;user id=root;password=;database=mrmsdb;sslMode=none"

            Using con As New MySqlConnection(connectionString),
              cmd As New MySqlCommand("SELECT * FROM tbl_patientlog WHERE Student_ID = @id", con)
                cmd.Parameters.AddWithValue("@id", TextBox23.Text)
                con.Open()
                Dim dt As New DataTable()
                Dim da As New MySqlDataAdapter(cmd)
                da.Fill(dt)
                dtg2.DataSource = dt
                If dtg2 IsNot Nothing Then
                    column = dtg2.Columns.Count
                    'MessageBox.Show("dtg2.Columns.Count: " & dtg2.Columns.Count)
                    column = dtg2.Columns.Count - 1
                    dtg2.Columns(0).Visible = False
                    dtg2.Columns(3).Visible = False
                    Label6.Text = dt.Rows.Count
                End If

                For Each col As DataGridViewColumn In dtg2.Columns
                    col.DefaultCellStyle.Padding = New Padding(5)
                    col.Width = 150
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    col.HeaderCell.Style.Font = New Font("Segoe UI Historic", 10, FontStyle.Bold)
                    col.MinimumWidth = 50
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dtg2.DefaultCellStyle.BackColor = Color.White
                    dtg2.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64)
                    dtg2.DefaultCellStyle.Font = New Font("Segoe UI Historic", 10)
                    dtg2.ColumnHeadersDefaultCellStyle.BackColor = Color.White
                    dtg2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(45, 73, 112)
                    dtg2.EnableHeadersVisualStyles = False
                    dtg2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 73, 112)
                Next

                dtg2.RowTemplate.Height = 35


            End Using
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click
        Dim connectionString As String = "server=localhost;user id=root;password=;database=mrmsdb;sslMode=none"
        Dim query As String = "SELECT * FROM tbl_patientmh WHERE Student_ID = @id"
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@id", TextBox23.Text)
                Using adapter As New MySqlDataAdapter(command)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    If dataTable.Rows.Count > 0 Then
                        Dim row As DataRow = dataTable.Rows(0)
                        TextBox3.Text = row("Student_ID").ToString()
                        TextBox5.Text = row("Diseases").ToString()
                        TextBox12.Text = row("Allergies").ToString()
                        TextBox2.Text = row("Meds").ToString()
                        TextBox4.Text = row("Hospitalization").ToString()
                        TextBox13.Text = row("Menstrual_Period").ToString()
                        TextBox16.Text = row("Height").ToString()
                        TextBox10.Text = row("Weight").ToString()
                        TextBox8.Text = row("BMI").ToString()
                        TextBox7.Text = row("BP").ToString()
                        TextBox9.Text = row("PR").ToString()
                        TextBox6.Text = row("TEMP").ToString()
                        RichTextBox1.Text = row("HEAD").ToString()
                        RichTextBox2.Text = row("EAR").ToString()
                        RichTextBox3.Text = row("EYES").ToString()
                        RichTextBox4.Text = row("NOSE").ToString()
                        RichTextBox5.Text = row("TEETH").ToString()
                        RichTextBox6.Text = row("CHEST_LUNGS").ToString()
                        RichTextBox7.Text = row("HEART").ToString()
                        RichTextBox8.Text = row("ABDOMEN").ToString()
                        TextBox17.Text = row("EXTREMITIES").ToString()
                        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
                            TabControl1.SelectedIndex += 1
                        End If
                    Else
                        MessageBox.Show("Record not found.")
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub Label35_Click(sender As Object, e As EventArgs) Handles Label35.Click

    End Sub
End Class