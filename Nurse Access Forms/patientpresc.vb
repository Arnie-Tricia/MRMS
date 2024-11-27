Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class patientpresc
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

            sql = "Select * from tbl_patientlog"
            cmd = New MySqlCommand(sql, con)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            da.Fill(dt)
            dtg.DataSource = dt
            column = dtg.Columns.Count - 1
            dtg.Columns(column).visible = True
            dtg.Columns(0).Visible = False
            dtg.Columns(3).Visible = False

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
                dtg.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 73, 112)
                dtg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Next

            'dtg.RowTemplate.Height = 35

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()

    End Sub

    Private Sub dtg_cellcontentdoubleclick(sender As Object, e As DataGridViewCellEventArgs) Handles dtg.CellContentDoubleClick
        Try
            With dtg.CurrentRow()
                TextBox1.Text = .Cells(0).Value
                TextBox3.Text = .Cells(1).Value
                TextBox4.Text = .Cells(2).Value
                TextBox6.Text = .Cells(4).Value
                TextBox7.Text = .Cells(5).Value
                TextBox8.Text = .Cells(6).Value
                TextBox9.Text = .Cells(7).Value
                TextBox10.Text = .Cells(9).Value
                DTP2.Text = .Cells(10).Value
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub patientpresc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_data()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        'con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        'con.Open()
        'sql = "SELECT * FROM tbl_patientlog WHERE ID_log LIKE '" & TextBox2.Text & "%' OR username LIKE '" & TextBox2.Text & "%' OR 
        'Purpose LIKE '" & TextBox2.Text & "%' OR  
        'P_Condition LIKE '" & TextBox2.Text & "%' OR  
        'Prescribed_Meds LIKE '" & TextBox2.Text & "%' OR  
        'P_DATE LIKE '" & TextBox2.Text & "%'"
        'cmd = New MySqlCommand(sql, con)
        'da = New MySqlDataAdapter(cmd)
        'dt = New DataTable()
        'da.Fill(dt)
        'If dtg.Rows.Count > 0 Then
        '    dtg.DataSource = dt
        'Else
        '    MessageBox.Show("No record Found!")
        'End If

        con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        con.Open()
        sql = "SELECT * FROM tbl_patientlog WHERE Student_ID LIKE @Student_ID OR username LIKE @username OR Purpose LIKE @Purpose OR P_Condition LIKE @P_Condition OR Prescribed_Meds LIKE @Prescribed_Meds OR P_DATE LIKE @P_DATE"
        cmd = New MySqlCommand(sql, con)
        cmd.Parameters.AddWithValue("@Student_ID", "%" & TextBox2.Text & "%")
        cmd.Parameters.AddWithValue("@username", "%" & TextBox2.Text & "%")
        cmd.Parameters.AddWithValue("@Purpose", "%" & TextBox2.Text & "%")
        cmd.Parameters.AddWithValue("@P_Condition", "%" & TextBox2.Text & "%")
        cmd.Parameters.AddWithValue("@Prescribed_Meds", "%" & TextBox2.Text & "%")
        cmd.Parameters.AddWithValue("@P_DATE", "%" & TextBox2.Text & "%")
        da = New MySqlDataAdapter(cmd)
        dt = New DataTable()
        da.Fill(dt)
        If dtg.Rows.Count > 0 Then
            dtg.DataSource = dt
        Else
            MessageBox.Show("No record Found!")
        End If


    End Sub

    Private Sub patientpresc_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (Me.Width - Panel1.Width) / 30

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        DTP2.ResetText()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click


        Try
            con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
            con.Open()

            If TextBox4.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Then
                MsgBox("Fill all information")
            Else
                sql = "UPDATE tbl_patientlog SET username = @username, Purpose = @Purpose, P_Condition = @P_Condition, Prescribed_Meds = @Prescribed_Meds, P_DATE = @P_DATE WHERE ID_log = @ID_log"
                cmd = New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@ID_log", TextBox1.Text)
                cmd.Parameters.AddWithValue("@username", TextBox4.Text)
                cmd.Parameters.AddWithValue("@Purpose", TextBox6.Text)
                cmd.Parameters.AddWithValue("@P_Condition", TextBox7.Text)
                cmd.Parameters.AddWithValue("@Prescribed_Meds", TextBox8.Text)
                cmd.Parameters.AddWithValue("@P_DATE", DTP2.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Updated")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try

        Call patientpresc_Load(sender, e)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click



        Try
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                con = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
                con.Open()
                sql = "DELETE FROM tbl_patientlog WHERE ID_log = @ID_log"
                cmd = New MySqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@ID_log", TextBox1.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Deleted!", MsgBoxStyle.Exclamation)
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox6.Clear()
                TextBox7.Clear()
                TextBox8.Clear()
                TextBox9.Clear()
                TextBox10.Clear()
                DTP2.ResetText()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
            load_data()
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        load_data()
    End Sub

End Class