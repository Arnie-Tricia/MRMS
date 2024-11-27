Imports MySql.Data.MySqlClient
Public Class Addpatient
    Dim con As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim dt As New DataTable
    Dim sql As String
    Dim sql1 As String
    Dim first As String
    Dim second As String
    Dim column
    Dim age As Integer
    Private Sub load_data()
        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            sql = "SELECT * FROM tbl_patientpi INNER JOIN tbl_patientmh ON tbl_patientpi.Student_ID = tbl_patientmh.Student_ID	
INNER JOIN tbl_patientacc ON tbl_patientmh.Student_ID = tbl_patientacc.Student_ID;"
            cmd = New MySqlCommand(sql, con)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()

    End Sub
    Private Sub Addpatient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_data()
        Panel2.Left = (Me.Width - Panel2.Width) / 15
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
            TabControl1.SelectedIndex += 1
        End If
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs)
        Me.Close()
        nurselogin.Show()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If TabControl1.SelectedIndex > 0 Then
            TabControl1.SelectedIndex -= 1
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If TabControl1.SelectedIndex < TabControl1.TabCount - 1 Then
            TabControl1.SelectedIndex += 1
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If TabControl1.SelectedIndex > 0 Then
            TabControl1.SelectedIndex -= 1
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        If String.IsNullOrEmpty(TextBox32.Text) OrElse String.IsNullOrEmpty(TextBox33.Text) OrElse String.IsNullOrEmpty(TextBox34.Text) Then
            MsgBox("Fill Up All Information", MsgBoxStyle.Information, "Fill Up All Information")
        Else
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            sql = "SELECT * FROM tbl_patientmh WHERE Student_ID = @id"
            cmd = New MySqlCommand(sql, con)
            cmd.Parameters.AddWithValue("@id", TextBox32.Text)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MsgBox("Student ID does not exist.", MsgBoxStyle.Critical, "Invalid Record")
            Else
                sql = "SELECT * FROM tbl_patientacc WHERE Student_ID = @id"
                cmd = New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@id", TextBox32.Text)
                da = New MySqlDataAdapter(cmd)
                dt = New DataTable()
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    MsgBox("Student ID already exists.", MsgBoxStyle.Critical, "Duplicate Record")
                ElseIf TextBox34.Text = TextBox1.Text Then
                    sql = "insert into tbl_patientacc(Student_ID,Username,Password) values (@StudentID, @Username, @Password)"
                    cmd = New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@StudentID", TextBox32.Text)
                    cmd.Parameters.AddWithValue("@Username", TextBox33.Text)
                    cmd.Parameters.AddWithValue("@Password", TextBox34.Text)
                    cmd.ExecuteNonQuery()

                    MsgBox("New Account added!", MsgBoxStyle.Information, "Successfully Added!")
                    TextBox32.Clear()
                    TextBox33.Clear()
                    TextBox34.Clear()
                    TextBox1.Clear()
                Else
                    MessageBox.Show("Password Not matched")
                End If
            End If
            con.Close()
            load_data()
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox32.Clear()
        TextBox33.Clear()
        TextBox34.Clear()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        '       con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        '       con.Open()

        '       If TextBox23.Text = "" Or TextBox25.Text = "" Or TextBox27.Text = "" Or ComboBox5.Text = "" Or TextBox30.Text = "" Or
        'DateTimePicker3.Text = "" Or TextBox31.Text = "" Or ComboBox6.Text = "" Or TextBox29.Text = "" Or
        'ComboBox1.Text = "" Or ComboBox2.Text = "" Or TextBox26.Text = "" Or TextBox24.Text = "" Or
        'TextBox22.Text = "" Then
        '           MsgBox("Please fill up all information.", MsgBoxStyle.Information, "Fill up all information")
        '       Else
        '           Dim sqlCheck As String = "SELECT COUNT(*) FROM tbl_patientpi WHERE Student_ID = @StudentID"
        '           Dim cmdCheck As New MySqlCommand(sqlCheck, con)
        '           cmdCheck.Parameters.AddWithValue("@StudentID", TextBox23.Text)
        '           Dim recordCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

        '           If recordCount > 0 Then
        '               MsgBox("Student ID already exists.", MsgBoxStyle.Information, "Duplicate Record")
        '           Else
        '               Dim sqlInsert As String = "INSERT INTO tbl_patientpi(Student_ID, Firstname, Lastname, Sex, Age, Birthday, Religion, Civil_Status, Contact_No, Course, Course_Dept, Guardian_Name, Relationship, G_contact_No) VALUES (@StudentID, @FirstName, @LastName, @Sex, @Age, @Birthday, @Religion, @CivilStatus, @ContactNo, @Course, @CourseDept, @GuardianName, @Relationship, @GContactNo)"
        '               Dim cmdInsert As New MySqlCommand(sqlInsert, con)
        '               cmdInsert.Parameters.AddWithValue("@StudentID", TextBox23.Text)
        '               cmdInsert.Parameters.AddWithValue("@FirstName", TextBox25.Text)
        '               cmdInsert.Parameters.AddWithValue("@LastName", TextBox27.Text)
        '               cmdInsert.Parameters.AddWithValue("@Sex", ComboBox5.Text)
        '               cmdInsert.Parameters.AddWithValue("@Age", TextBox30.Text)
        '               cmdInsert.Parameters.AddWithValue("@Birthday", DateTimePicker3.Text)
        '               cmdInsert.Parameters.AddWithValue("@Religion", TextBox31.Text)
        '               cmdInsert.Parameters.AddWithValue("@CivilStatus", ComboBox6.Text)
        '               cmdInsert.Parameters.AddWithValue("@ContactNo", TextBox29.Text)
        '               cmdInsert.Parameters.AddWithValue("@Course", ComboBox1.Text)
        '               cmdInsert.Parameters.AddWithValue("@CourseDept", ComboBox2.Text)
        '               cmdInsert.Parameters.AddWithValue("@GuardianName", TextBox26.Text)
        '               cmdInsert.Parameters.AddWithValue("@Relationship", TextBox24.Text)
        '               cmdInsert.Parameters.AddWithValue("@GContactNo", TextBox22.Text)
        '               cmdInsert.ExecuteNonQuery()

        '               MsgBox("New Patient Record added!", MsgBoxStyle.Information, "Success")
        '               TextBox23.Clear()
        '               TextBox25.Clear()
        '               TextBox27.Clear()
        '               TextBox30.Clear()
        '               TextBox31.Clear()
        '               TextBox29.Clear()
        '               TextBox26.Clear()
        '               TextBox24.Clear()
        '               TextBox22.Clear()
        '           End If
        '       End If

        '       con.Close()
        '       load_data()

        '     If String.IsNullOrEmpty(TextBox23.Text) OrElse String.IsNullOrEmpty(TextBox25.Text) OrElse
        'String.IsNullOrEmpty(TextBox27.Text) OrElse String.IsNullOrEmpty(ComboBox5.Text) OrElse
        'String.IsNullOrEmpty(TextBox30.Text) OrElse
        'String.IsNullOrEmpty(TextBox31.Text) OrElse String.IsNullOrEmpty(ComboBox6.Text) OrElse
        'String.IsNullOrEmpty(TextBox29.Text) OrElse String.IsNullOrEmpty(ComboBox1.Text) OrElse
        'String.IsNullOrEmpty(ComboBox2.Text) OrElse String.IsNullOrEmpty(TextBox26.Text) OrElse
        'String.IsNullOrEmpty(TextBox24.Text) OrElse String.IsNullOrEmpty(TextBox22.Text) Then

        '         MsgBox("Please fill up all information.", MsgBoxStyle.Information, "Fill up all information")
        '     ElseIf Not Integer.TryParse(TextBox30.Text, age) Then
        '         MsgBox("Invalid Age. Please enter a valid integer.", MsgBoxStyle.Information, "Invalid Input")
        '         Return
        '     Else
        '             con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        '         con.Open()

        '         Dim sqlCheck As String = "SELECT COUNT(*) FROM tbl_patientpi WHERE Student_ID = @StudentID"
        '         Dim cmdCheck As New MySqlCommand(sqlCheck, con)
        '         cmdCheck.Parameters.AddWithValue("@StudentID", TextBox23.Text)
        '         Dim recordCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

        '         If recordCount > 0 Then
        '             MsgBox("Student ID already exists.", MsgBoxStyle.Information, "Duplicate Record")
        '         Else

        '             Dim sqlInsert As String = "INSERT INTO tbl_patientpi(Student_ID, Firstname, Lastname, Sex, Age, Birthday, Religion, Civil_Status, Contact_No, Course, Course_Dept, Guardian_Name, Relationship, G_contact_No) VALUES (@StudentID, @FirstName, @LastName, @Sex, @Age, @Birthday, @Religion, @CivilStatus, @ContactNo, @Course, @CourseDept, @GuardianName, @Relationship, @GContactNo)"
        '             Dim cmdInsert As New MySqlCommand(sqlInsert, con)
        '             cmdInsert.Parameters.AddWithValue("@StudentID", TextBox23.Text)
        '             cmdInsert.Parameters.AddWithValue("@FirstName", TextBox25.Text)
        '             cmdInsert.Parameters.AddWithValue("@LastName", TextBox27.Text)
        '             cmdInsert.Parameters.AddWithValue("@Sex", ComboBox5.Text)
        '             cmdInsert.Parameters.AddWithValue("@Age", TextBox30.Text)
        '             cmdInsert.Parameters.AddWithValue("@Birthday", DateTimePicker3.Text)
        '             cmdInsert.Parameters.AddWithValue("@Religion", TextBox31.Text)
        '             cmdInsert.Parameters.AddWithValue("@CivilStatus", ComboBox6.Text)
        '             cmdInsert.Parameters.AddWithValue("@ContactNo", TextBox29.Text)
        '             cmdInsert.Parameters.AddWithValue("@Course", ComboBox1.Text)
        '             cmdInsert.Parameters.AddWithValue("@CourseDept", ComboBox2.Text)
        '             cmdInsert.Parameters.AddWithValue("@GuardianName", TextBox26.Text)
        '             cmdInsert.Parameters.AddWithValue("@Relationship", TextBox24.Text)
        '             cmdInsert.Parameters.AddWithValue("@GContactNo", TextBox22.Text)
        '             cmdInsert.ExecuteNonQuery()

        '             MsgBox("New Patient Record added!", MsgBoxStyle.Information, "Success")
        '             TextBox23.Clear()
        '             TextBox25.Clear()
        '             TextBox27.Clear()
        '             TextBox30.Clear()
        '             TextBox31.Clear()
        '             TextBox29.Clear()
        '             TextBox26.Clear()
        '             TextBox24.Clear()
        '             TextBox22.Clear()
        '         End If

        '         con.Close()
        '         load_data()
        '     End If




        If String.IsNullOrEmpty(TextBox23.Text) OrElse String.IsNullOrEmpty(TextBox25.Text) OrElse
   String.IsNullOrEmpty(TextBox27.Text) OrElse String.IsNullOrEmpty(ComboBox5.Text) OrElse
   String.IsNullOrEmpty(TextBox30.Text) OrElse
   String.IsNullOrEmpty(TextBox31.Text) OrElse String.IsNullOrEmpty(ComboBox6.Text) OrElse
   String.IsNullOrEmpty(TextBox29.Text) OrElse String.IsNullOrEmpty(ComboBox1.Text) OrElse
   String.IsNullOrEmpty(ComboBox2.Text) OrElse String.IsNullOrEmpty(TextBox26.Text) OrElse
   String.IsNullOrEmpty(TextBox24.Text) OrElse String.IsNullOrEmpty(TextBox22.Text) Then

            MsgBox("Please fill up all information.", MsgBoxStyle.Information, "Fill up all information")
        ElseIf Not Integer.TryParse(TextBox30.Text, age) Then
            MsgBox("Invalid Age. Please enter a valid integer.", MsgBoxStyle.Information, "Invalid Input")
            Return
        Else
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            Dim sqlCheck As String = "SELECT COUNT(*) FROM tbl_patientpi WHERE Student_ID = @StudentID"
            Dim cmdCheck As New MySqlCommand(sqlCheck, con)
            cmdCheck.Parameters.AddWithValue("@StudentID", TextBox23.Text)
            Dim recordCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

            If recordCount > 0 Then
                MsgBox("Student ID already exists.", MsgBoxStyle.Information, "Duplicate Record")
            Else

                Dim message As String = "Please confirm the following information:" & vbCrLf & vbCrLf
                message &= "Student ID: " & TextBox23.Text & vbCrLf
                message &= "First Name: " & TextBox25.Text & vbCrLf
                message &= "Last Name: " & TextBox27.Text & vbCrLf
                message &= "Sex: " & ComboBox5.Text & vbCrLf
                message &= "Age: " & TextBox30.Text & vbCrLf
                message &= "Birthday: " & DateTimePicker3.Text & vbCrLf
                message &= "Religion: " & TextBox31.Text & vbCrLf
                message &= "Civil Status: " & ComboBox6.Text & vbCrLf
                message &= "Contact No.: " & TextBox29.Text & vbCrLf
                message &= "Course: " & ComboBox1.Text & vbCrLf
                message &= "Course Dept.: " & ComboBox2.Text & vbCrLf
                message &= "Guardian Name: " & TextBox26.Text & vbCrLf
                message &= "Relationship: " & TextBox24.Text & vbCrLf
                message &= "G Contact No.: " & TextBox22.Text

                Dim confirmResult As DialogResult = MessageBox.Show(message, "Confirm Input", MessageBoxButtons.YesNo)

                ' If user confirms, add new record to database
                If confirmResult = DialogResult.Yes Then
                    Dim sqlInsert As String = "INSERT INTO tbl_patientpi(Student_ID, Firstname, Lastname, Sex, Age, Birthday, Religion, Civil_Status, Contact_No, Course, Course_Dept, Guardian_Name, Relationship, G_contact_No) VALUES (@StudentID, @FirstName, @LastName, @Sex, @Age, @Birthday, @Religion, @CivilStatus, @ContactNo, @Course, @CourseDept, @GuardianName, @Relationship, @GContactNo)"
                    Dim cmdInsert As New MySqlCommand(sqlInsert, con)
                    cmdInsert.Parameters.AddWithValue("@StudentID", TextBox23.Text)
                    cmdInsert.Parameters.AddWithValue("@FirstName", TextBox25.Text)
                    cmdInsert.Parameters.AddWithValue("@LastName", TextBox27.Text)
                    cmdInsert.Parameters.AddWithValue("@Sex", ComboBox5.Text)
                    cmdInsert.Parameters.AddWithValue("@Age", TextBox30.Text)
                    cmdInsert.Parameters.AddWithValue("@Birthday", DateTimePicker3.Text)
                    cmdInsert.Parameters.AddWithValue("@Religion", TextBox31.Text)
                    cmdInsert.Parameters.AddWithValue("@CivilStatus", ComboBox6.Text)
                    cmdInsert.Parameters.AddWithValue("@ContactNo", TextBox29.Text)
                    cmdInsert.Parameters.AddWithValue("@Course", ComboBox1.Text)
                    cmdInsert.Parameters.AddWithValue("@CourseDept", ComboBox2.Text)
                    cmdInsert.Parameters.AddWithValue("@GuardianName", TextBox26.Text)
                    cmdInsert.Parameters.AddWithValue("@Relationship", TextBox24.Text)
                    cmdInsert.Parameters.AddWithValue("@GContactNo", TextBox22.Text)
                    cmdInsert.ExecuteNonQuery()

                    MsgBox("New Patient Record added!", MsgBoxStyle.Information, "Success")
                    TextBox23.Clear()
                    TextBox25.Clear()
                    TextBox27.Clear()
                    TextBox30.Clear()
                    TextBox31.Clear()
                    TextBox29.Clear()
                    TextBox26.Clear()
                    TextBox24.Clear()
                    TextBox22.Clear()
                End If

                con.Close()
                load_data()
            End If
        End If

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        con.Open()

        sql = "SELECT * FROM tbl_patientpi WHERE Student_ID = @id"
        cmd = New MySqlCommand(sql, con)
        cmd.Parameters.AddWithValue("@id", TextBox3.Text)
        da = New MySqlDataAdapter(cmd)
        dt = New DataTable()
        da.Fill(dt)

        If TextBox3.Text = "" Or TextBox5.Text = "" Or TextBox12.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox16.Text = "" Or TextBox10.Text = "" Or TextBox8.Text = "" Or TextBox7.Text = "" Or TextBox9.Text = "" Or TextBox6.Text = "" Or TextBox17.Text = "" Then
            MsgBox("Fill Up All Information", MsgBoxStyle.Information, "Fill Up All Information")
        ElseIf dt.Rows.Count = 0 Then
            MsgBox("Student ID does not exist in the Patient Information Record.", MsgBoxStyle.Critical, "Invalid Record")
        Else
            sql = "SELECT * FROM tbl_patientmh WHERE Student_ID = @id"
            cmd = New MySqlCommand(sql, con)
            cmd.Parameters.AddWithValue("@id", TextBox3.Text)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                MsgBox("Student ID already exists.", MsgBoxStyle.Critical, "Duplicate Record")
            Else
                sql = "INSERT INTO tbl_patientmh(Student_ID, Diseases, Allergies, Meds, Hospitalization, Menstrual_Period, Height, Weight, BMI, BP, PR,TEMP, HEAD, EAR, EYES, NOSE, TEETH, CHEST_LUNGS, HEART, ABDOMEN, EXTREMITIES) VALUES (@StudentID, @Diseases, @Allergies, @Meds, @Hospitalization, @Menstrual_Period, @Height, @Weight, @BMI, @BP, @PR, @TEMP, @HEAD, @EAR, @EYES, @NOSE, @TEETH, @CHEST_LUNGS, @HEART, @ABDOMEN, @EXTREMITIES)"
                Dim cmdInsert As New MySqlCommand(sql, con)
                cmdInsert.Parameters.AddWithValue("@StudentID", TextBox3.Text)
                cmdInsert.Parameters.AddWithValue("@Diseases", TextBox5.Text)
                cmdInsert.Parameters.AddWithValue("@Allergies", TextBox12.Text)
                cmdInsert.Parameters.AddWithValue("@Meds", TextBox2.Text)
                cmdInsert.Parameters.AddWithValue("@Hospitalization", TextBox4.Text)
                cmdInsert.Parameters.AddWithValue("@Menstrual_Period", TextBox11.Text)
                cmdInsert.Parameters.AddWithValue("@Height", TextBox16.Text)
                cmdInsert.Parameters.AddWithValue("@Weight", TextBox10.Text)
                cmdInsert.Parameters.AddWithValue("@BMI", TextBox8.Text)
                cmdInsert.Parameters.AddWithValue("@BP", TextBox7.Text)
                cmdInsert.Parameters.AddWithValue("@PR", TextBox9.Text)
                cmdInsert.Parameters.AddWithValue("@TEMP", TextBox6.Text)
                cmdInsert.Parameters.AddWithValue("@HEAD", RichTextBox1.Text)
                cmdInsert.Parameters.AddWithValue("@EAR", RichTextBox2.Text)
                cmdInsert.Parameters.AddWithValue("@EYES", RichTextBox3.Text)
                cmdInsert.Parameters.AddWithValue("@NOSE", RichTextBox4.Text)
                cmdInsert.Parameters.AddWithValue("@TEETH", RichTextBox5.Text)
                cmdInsert.Parameters.AddWithValue("@CHEST_LUNGS", RichTextBox6.Text)
                cmdInsert.Parameters.AddWithValue("@HEART", RichTextBox7.Text)
                cmdInsert.Parameters.AddWithValue("@ABDOMEN", RichTextBox8.Text)
                cmdInsert.Parameters.AddWithValue("@EXTREMITIES", TextBox17.Text)

                cmdInsert.ExecuteNonQuery()
                MsgBox("Patient's Medical Record added!", MsgBoxStyle.Information, "Success")
                TextBox3.Clear()
                TextBox5.Clear()
                TextBox12.Clear()
                TextBox2.Clear()
                TextBox4.Clear()
                TextBox16.Clear()
                TextBox10.Clear()
                TextBox8.Clear()
                TextBox7.Clear()
                TextBox9.Clear()
                TextBox6.Clear()
                TextBox17.Clear()
                TextBox11.Clear()
                RichTextBox1.Text = "Normal"
                RichTextBox2.Text = "Normal"
                RichTextBox3.Text = "Normal"
                RichTextBox4.Text = "Normal"
                RichTextBox5.Text = "Normal"
                RichTextBox6.Text = "Normal"
                RichTextBox7.Text = "Normal"
                RichTextBox8.Text = "Normal"

            End If
        End If
        con.Close()
        load_data()


        'con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        'con.Open()

        'sql = "SELECT * FROM tbl_patientpi WHERE Student_ID = @id"
        'cmd = New MySqlCommand(sql, con)
        'cmd.Parameters.AddWithValue("@id", TextBox3.Text)
        'da = New MySqlDataAdapter(cmd)
        'dt = New DataTable()
        'da.Fill(dt)

        'If TextBox3.Text = "" Or TextBox5.Text = "" Or TextBox12.Text = "" Then
        '    MsgBox("Please fill up all information.", MsgBoxStyle.Information, "Fill up all information")
        'ElseIf dt.Rows.Count > 0 Then
        '    sql = "INSERT INTO tbl_patientmh(Student_ID, Diseases, Allergies, Meds, Hospitalization, Menstrual_Period, Height, Weight, BMI, BP, PR,TEMP, HEAD, EAR, EYES, NOSE, TEETH, CHEST_LUNGS, HEART, ABDOMEN, EXTREMITIES) VALUES (@StudentID, @Diseases, @Allergies, @Meds, @Hospitalization, @Menstrual_Period, @Height, @Weight, @BMI, @BP, @PR, @TEMP, @HEAD, @EAR, @EYES, @NOSE, @TEETH, @CHEST_LUNGS, @HEART, @ABDOMEN, @EXTREMITIES)"
        '    Dim cmdInsert As New MySqlCommand(sql, con)
        '    cmdInsert.Parameters.AddWithValue("@StudentID", TextBox3.Text)
        '    cmdInsert.Parameters.AddWithValue("@Diseases", TextBox5.Text)
        '    cmdInsert.Parameters.AddWithValue("@Allergies", TextBox12.Text)
        '    cmdInsert.Parameters.AddWithValue("@Meds", TextBox2.Text)
        '    cmdInsert.Parameters.AddWithValue("@Hospitalization", TextBox4.Text)
        '    cmdInsert.Parameters.AddWithValue("@Menstrual_Period", DateTimePicker1.Text)
        '    cmdInsert.Parameters.AddWithValue("@Height", TextBox16.Text)
        '    cmdInsert.Parameters.AddWithValue("@Weight", TextBox10.Text)
        '    cmdInsert.Parameters.AddWithValue("@BMI", TextBox8.Text)
        '    cmdInsert.Parameters.AddWithValue("@BP", TextBox7.Text)
        '    cmdInsert.Parameters.AddWithValue("@PR", TextBox9.Text)
        '    cmdInsert.Parameters.AddWithValue("@TEMP", TextBox6.Text)
        '    cmdInsert.Parameters.AddWithValue("@HEAD", ComboBox3.Text)
        '    cmdInsert.Parameters.AddWithValue("@EAR", ComboBox4.Text)
        '    cmdInsert.Parameters.AddWithValue("@EYES", ComboBox7.Text)
        '    cmdInsert.Parameters.AddWithValue("@NOSE", ComboBox8.Text)
        '    cmdInsert.Parameters.AddWithValue("@TEETH", ComboBox9.Text)
        '    cmdInsert.Parameters.AddWithValue("@CHEST_LUNGS", ComboBox10.Text)
        '    cmdInsert.Parameters.AddWithValue("@HEART", ComboBox11.Text)
        '    cmdInsert.Parameters.AddWithValue("@ABDOMEN", ComboBox12.Text)
        '    cmdInsert.Parameters.AddWithValue("@EXTREMITIES", TextBox17.Text)

        '    cmdInsert.ExecuteNonQuery()
        '    MsgBox("Patient's Medical Record added!", MsgBoxStyle.Information, "Success")
        '    TextBox3.Clear()
        '    TextBox5.Clear()
        '    TextBox12.Clear()
        '    TextBox2.Clear()
        '    TextBox4.Clear()
        '    TextBox16.Clear()
        '    TextBox10.Clear()
        '    TextBox8.Clear()
        '    TextBox7.Clear()
        '    TextBox9.Clear()
        '    TextBox6.Clear()
        '    ComboBox3.ResetText()
        '    ComboBox4.ResetText()
        '    ComboBox7.ResetText()
        '    ComboBox8.ResetText()
        '    ComboBox9.ResetText()
        '    ComboBox10.ResetText()
        '    ComboBox11.ResetText()
        '    ComboBox12.ResetText()
        '    TextBox17.Clear()

        'Else
        '    MessageBox.Show("Student ID not found.")
        'End If

        'con.Close()
        'load_data()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox23.Clear()
        TextBox25.Clear()
        TextBox27.Clear()
        TextBox30.Clear()
        TextBox31.Clear()
        TextBox29.Clear()
        TextBox26.Clear()
        TextBox24.Clear()
        TextBox22.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox3.Clear()
        TextBox5.Clear()
        TextBox12.Clear()
        TextBox2.Clear()
        TextBox4.Clear()
        TextBox16.Clear()
        TextBox10.Clear()
        TextBox8.Clear()
        TextBox7.Clear()
        TextBox9.Clear()
        TextBox17.Clear()
        RichTextBox1.Clear()
        RichTextBox2.Clear()
        RichTextBox3.Clear()
        RichTextBox4.Clear()
        RichTextBox5.Clear()
        RichTextBox6.Clear()
        RichTextBox7.Clear()
        RichTextBox8.Clear()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub
End Class