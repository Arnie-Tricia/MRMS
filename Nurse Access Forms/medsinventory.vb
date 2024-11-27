Imports MySql.Data.MySqlClient
Public Class medsinventory
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

            sql = "Select * from tbl_meds"
            cmd = New MySqlCommand(sql, con)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)
            dtg.DataSource = dt
            column = dtg.Columns.Count - 5
            dtg.Columns(column).Visible = False
            dtg.Columns(0).Visible = False

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
                'dtg.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 209, 234)
            Next

            dtg.RowTemplate.Height = 35


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()

    End Sub
    Private Sub medsinventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        load_data()
        Panel1.Left = (Me.Width - Panel1.Width) / 5
    End Sub

    Private Sub dtg_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtg.CellContentDoubleClick
        Try
            With dtg.CurrentRow()
                Label1.Text = .Cells(0).Value
                TextBox3.Text = .Cells(1).Value
                NumericUpDown2.Text = .Cells(2).Value
                NumericUpDown1.Text = .Cells(3).Value
                DateTimePicker2.Text = .Cells(4).Value
            End With
            Button6.Enabled = True
            Button9.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            If TextBox3.Text = "" Or DateTimePicker2.Value = Nothing Then
                MsgBox("Please Select a medicine")
            Else
                sql = "UPDATE tbl_meds SET Medicine_Name = @name, Quantity = @quantity, Stock_Balance = @balance, Expiration_Date = @expiration WHERE ID_Meds = @id"
                cmd = New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@name", TextBox3.Text)
                cmd.Parameters.AddWithValue("@quantity", NumericUpDown2.Value)
                cmd.Parameters.AddWithValue("@balance", NumericUpDown1.Value)
                cmd.Parameters.AddWithValue("@expiration", DateTimePicker2.Value)
                cmd.Parameters.AddWithValue("@id", Label1.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Updated")
                TextBox3.Clear()
                NumericUpDown2.Value = 0
                NumericUpDown1.Value = 0
                DateTimePicker2.ResetText()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try
        load_data()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            If TextBox3.Text = "" Or NumericUpDown1.Text = "" Or NumericUpDown2.Text = "" Or DateTimePicker2.Text = "" Then
                MsgBox("Fill Up All Information", MsgBoxStyle.Information, "Fill Up All Information")
            Else
                sql = "SELECT COUNT(*) FROM tbl_meds WHERE Medicine_Name = @medicineName"
                cmd = New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@medicineName", TextBox3.Text)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                If count > 0 Then
                    MsgBox("Medicine already exists!", MsgBoxStyle.Exclamation, "Duplicate Record")
                Else
                    sql = "INSERT INTO tbl_meds (Medicine_Name, Quantity, Stock_Balance, Expiration_Date) VALUES (@medicineName, @quantity, @stockBalance, @expirationDate)"
                    cmd = New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@medicineName", TextBox3.Text)
                    cmd.Parameters.AddWithValue("@quantity", NumericUpDown2.Value)
                    cmd.Parameters.AddWithValue("@stockBalance", NumericUpDown1.Value)
                    cmd.Parameters.AddWithValue("@expirationDate", DateTimePicker2.Value)
                    cmd.ExecuteNonQuery()

                    MsgBox("New medicine added!", MsgBoxStyle.Information, "Successfully Added!")
                    TextBox3.Clear()
                    NumericUpDown2.Value = 0
                    NumericUpDown1.Value = 0
                    DateTimePicker2.ResetText()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
            load_data()
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            If TextBox3.Text = "" Or NumericUpDown2.Text = "" Or NumericUpDown1.Text = "" Or DateTimePicker2.Text = "" Then
                MsgBox("Please Select a medicine")
            Else
                Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    Dim sql As String = "DELETE FROM tbl_Meds WHERE ID_Meds = @idMeds"
                    Dim cmd As New MySqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@idMeds", Label1.Text)
                    cmd.ExecuteNonQuery()

                    MsgBox("Successfully Deleted!", MsgBoxStyle.Exclamation)
                    TextBox3.Clear()
                    NumericUpDown2.Value = 0
                    NumericUpDown1.Value = 0
                    DateTimePicker2.ResetText()
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox3.Clear()
        NumericUpDown2.Value = 0
        NumericUpDown1.Value = 0
        DateTimePicker2.ResetText()
        Button6.Enabled = False
        Button9.Enabled = False
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim con As MySqlConnection = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        Try
            con.Open()
            Dim sql As String = "SELECT * FROM tbl_meds WHERE Medicine_Name LIKE @search OR Quantity LIKE @search OR Stock_Balance LIKE @search OR Expiration_Date LIKE @search"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, con)
            cmd.Parameters.AddWithValue("@search", "%" & TextBox2.Text & "%")
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                dtg.DataSource = dt
            Else
                MessageBox.Show("No record Found!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try

    End Sub
End Class