Imports MySql.Data.MySqlClient
Public Class PatientPI
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

            sql = "SELECT * FROM tbl_patientpi"
            cmd = New MySqlCommand(sql, con)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()

            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)

            dtg.DataSource = dt

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

            dtg.RowTemplate.Height = 35
            If dtg.Columns.Count > 0 Then
                column = dtg.Columns.Count - 1
                dtg.Columns(column).Visible = True
                dtg.Columns(0).Visible = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try


    End Sub
    Private Sub PatientPI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_data()
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        If TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox9.Text = "" Then
            MsgBox("Please select a patient")
        Else
            viewpatient.Show()
            Try
                With dtg.CurrentRow()

                    viewpatient.TextBox23.Text = .Cells(1).Value
                    viewpatient.TextBox25.Text = .Cells(2).Value
                    viewpatient.TextBox27.Text = .Cells(3).Value
                    viewpatient.ComboBox5.Text = .Cells(4).Value
                    viewpatient.TextBox30.Text = .Cells(5).Value
                    viewpatient.DateTimePicker2.Text = .Cells(6).Value
                    viewpatient.TextBox31.Text = .Cells(7).Value
                    viewpatient.ComboBox6.Text = .Cells(8).Value
                    viewpatient.TextBox29.Text = .Cells(9).Value
                    viewpatient.ComboBox1.Text = .Cells(10).Value
                    viewpatient.ComboBox2.Text = .Cells(11).Value
                    viewpatient.TextBox26.Text = .Cells(12).Value
                    viewpatient.TextBox24.Text = .Cells(13).Value
                    viewpatient.TextBox22.Text = .Cells(14).Value
                End With
                Button8.Enabled = True
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        Try
            If String.IsNullOrEmpty(TextBox3.Text) OrElse String.IsNullOrEmpty(TextBox4.Text) OrElse String.IsNullOrEmpty(TextBox9.Text) Then
                MsgBox("Please select a patient")
            Else
                Dim confirmation As MsgBoxResult = MsgBox("Are you sure you want to delete ALL records for " & TextBox9.Text & ", including Personal Information, Medical History, Account Details, Prescriptions, and Logs?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Delete Confirmation")

                If confirmation = MsgBoxResult.Yes Then
                    Dim connectionString As String = "server=localhost;user id=root;password=;database=mrmsdb;sslMode=none"

                    Using con As New MySqlConnection(connectionString)
                        con.Open()

                        Dim transaction As MySqlTransaction = con.BeginTransaction()

                        Try
                            Dim sql As String = "DELETE FROM tbl_patientpi WHERE Student_ID = @id"
                            Dim cmd As New MySqlCommand(sql, con)
                            cmd.Parameters.AddWithValue("@id", TextBox3.Text)
                            cmd.ExecuteNonQuery()

                            sql = "DELETE FROM tbl_patientmh WHERE Student_ID = @id"
                            cmd = New MySqlCommand(sql, con)
                            cmd.Parameters.AddWithValue("@id", TextBox3.Text)
                            cmd.ExecuteNonQuery()

                            sql = "DELETE FROM tbl_patientlog WHERE Student_ID = @id"
                            cmd = New MySqlCommand(sql, con)
                            cmd.Parameters.AddWithValue("@id", TextBox3.Text)
                            cmd.ExecuteNonQuery()

                            sql = "DELETE FROM tbl_patientacc WHERE Student_ID = @id"
                            cmd = New MySqlCommand(sql, con)
                            cmd.Parameters.AddWithValue("@id", TextBox3.Text)
                            cmd.ExecuteNonQuery()

                            transaction.Commit()

                            MsgBox("Successfully Deleted!", MsgBoxStyle.Exclamation)
                            TextBox3.Clear()
                            TextBox4.Clear()
                            TextBox9.Clear()
                        Catch ex As Exception
                            transaction.Rollback()
                            MsgBox(ex.Message)
                        End Try

                        con.Close()
                        load_data()
                    End Using
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub dtg_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtg.CellContentDoubleClick
        Try
            With dtg.CurrentRow()
                TextBox3.Text = .Cells(1).Value
                TextBox4.Text = .Cells(2).Value
                TextBox9.Text = .Cells(3).Value
            End With
            Button8.Enabled = True
            Button9.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Panel1_Resize(sender As Object, e As EventArgs) Handles Panel1.Resize
        Panel1.Left = (Me.Width / Panel1.Width) / 1
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Try
            Using con As New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none"),
                  cmd As New MySqlCommand(),
                  da As New MySqlDataAdapter(),
                  dt As New DataTable()
                con.Open()
                cmd.Connection = con
                cmd.CommandText = "SELECT * FROM tbl_patientpi WHERE id LIKE @searchTerm OR Student_Id LIKE @searchTerm OR Firstname LIKE @searchTerm OR Lastname LIKE @searchTerm OR Sex LIKE @searchTerm OR Age LIKE @searchTerm OR Birthday LIKE @searchTerm OR Religion LIKE @searchTerm OR Civil_Status LIKE @searchTerm OR Contact_No LIKE @searchTerm OR Course LIKE @searchTerm OR Course_Dept LIKE @searchTerm OR Guardian_Name LIKE @searchTerm OR Relationship LIKE @searchTerm OR G_contact_No LIKE @searchTerm"
                cmd.Parameters.AddWithValue("@searchTerm", "%" & TextBox2.Text & "%")
                da.SelectCommand = cmd
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    dtg.DataSource = dt
                Else
                    MessageBox.Show("No record Found!")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        load_data()
    End Sub
End Class