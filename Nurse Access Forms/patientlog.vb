Imports MySql.Data.MySqlClient
Public Class patientlog
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
            dtg2.DataSource = dt
            'column = dtg2.Columns.Count - 1
            'dtg2.Columns(column).Visible = False
            'dtg2.Columns(0).Visible = False
            'dtg2.Columns(3).Visible = False
            Label2.Text = dt.Rows.Count


            For Each col As DataGridViewColumn In dtg2.Columns
                col.DefaultCellStyle.Padding = New Padding(5)
                'col.Width = 150
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                col.HeaderCell.Style.Font = New Font("Segoe UI Historic", 10, FontStyle.Bold)
                col.MinimumWidth = 50
                'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dtg2.DefaultCellStyle.BackColor = Color.White
                dtg2.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64)
                dtg2.DefaultCellStyle.Font = New Font("Segoe UI Historic", 10)
                dtg2.ColumnHeadersDefaultCellStyle.BackColor = Color.White
                dtg2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(45, 73, 112)
                dtg2.EnableHeadersVisualStyles = False
                'dtg2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                dtg2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 73, 112)

            Next

            dtg2.RowTemplate.Height = 35

            If dtg2.Columns.Count > 0 Then
                column = dtg2.Columns.Count - 1
                dtg2.Columns(column).Visible = True
                dtg2.Columns(0).Visible = False
                dtg2.Columns(3).Visible = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()

    End Sub
    Private Sub patientlog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_data()
    End Sub
    Private Sub patientlog_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (Me.Width - Panel1.Width) / 5
    End Sub
    Private Sub dtg2_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtg2.CellContentDoubleClick
        Try
            With dtg2.CurrentRow()
                patientpresc.TextBox3.Text = .Cells(0).Value
                patientpresc.TextBox4.Text = .Cells(1).Value
                'patientpresc.TextBox5.Text = .Cells(2).Value
                patientpresc.TextBox6.Text = .Cells(2).Value
                patientpresc.TextBox7.Text = .Cells(3).Value
                patientpresc.TextBox8.Text = .Cells(4).Value
                patientpresc.TextBox9.Text = .Cells(5).Value
                patientpresc.TextBox10.Text = .Cells(6).Value
                patientpresc.DTP2.Text = .Cells(7).Value

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim con As MySqlConnection = New MySqlConnection("server=localhost;user id=root;password=;database=mrmsdb;sslMode=none")
        Try
            con.Open()
            Dim sql As String = "SELECT * FROM tbl_patientlog WHERE Student_ID LIKE @search OR username LIKE @search OR Purpose LIKE @search OR P_Condition LIKE @search OR Prescribed_Meds LIKE @search OR P_DATE LIKE @search"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, con)
            cmd.Parameters.AddWithValue("@search", "%" & TextBox2.Text & "%")
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                dtg2.DataSource = dt
            Else
                MessageBox.Show("No record Found!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        load_data()
    End Sub

    'Private Sub SearchData(keyword As String)
    '    Dim dv As DataView = dt.DefaultView
    '    dv.RowFilter = String.Format("Username LIKE '%{0}%' OR Password LIKE '%{0}%' OR Purpose LIKE '%{0}%' OR P_Condition LIKE '%{0}%' OR Prescribe_meds LIKE '%{0}%' OR Time_in LIKE '%{0}%' OR Time_out LIKE '%{0}%' OR P_DATE LIKE '%{0}%'", keyword)

    '    If dv.Count = 0 Then
    '        MessageBox.Show("No records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Else
    '        dtg2.AutoGenerateColumns = False
    '        dtg2.DataSource = dv
    '    End If

    'End Sub
    'Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
    '    SearchData(TextBox2.Text)
    'End Sub

End Class