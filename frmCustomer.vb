Imports System.Data.SqlClient
Imports System.Runtime.InteropServices.ComTypes



Public Class frmCustomer
    Public CmdText As String, loading As Integer
    Private Sub cboCustomerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomerType.SelectedIndexChanged
        LoadDateInGrid()
    End Sub

    Private Sub frmCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cboCustomerType.Items.Clear()
        loading = 0
        'Using Statement
        Using connection As New SqlConnection(conString)
            connection.Open()
            Dim query As String = "select distinct CustomerType from Customer"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        cboCustomerType.Items.Add(reader("CustomerType").ToString())
                    End While
                End Using ' Disposes SqlDataReader
            End Using ' Disposes SqlCommand
        End Using ' Disposes SqlConnection


        'Try
        '    conn.Open()
        '    cmd.CommandText = "select distinct CustomerType from Customer"
        '    rd = cmd.ExecuteReader
        '    While rd.Read()
        '        cboCustomerType.Items.Add(rd.GetString(0))
        '    End While
        '    rd.Close()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'Finally
        '    If conn.State = ConnectionState.Open Then
        '        conn.Close()
        '    End If
        'End Try
    End Sub

    Private Sub dgvCustomer_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCustomer.CellContentClick
        ' Check to ensure the click is on a row (not a header or invalid cell)
        If e.RowIndex >= 0 Then
            ' Get the current row
            Dim selectedRow As DataGridViewRow = dgvCustomer.Rows(e.RowIndex)

            ' Retrieve cell values from the selected row
            txtCustomerId.Text = selectedRow.Cells("CustomerId").Value.ToString()
            txtCustomerName.Text = selectedRow.Cells("CustomerName").Value.ToString()
            dtpDateOfBirth.Text = selectedRow.Cells("DateOfBirth").Value.ToString()
            txtAddress.Text = selectedRow.Cells("Address").Value.ToString()
            txtPhone.Text = selectedRow.Cells("Mobile").Value.ToString()
            txtMailID.Text = selectedRow.Cells("eMailID").Value.ToString()
        End If
    End Sub

    Private Sub Clear()
        txtCustomerId.Text = ""
        txtCustomerName.Text = ""
        dtpDateOfBirth.Text = ""
        txtAge.Text = ""
        txtAddress.Text = ""
        txtPhone.Text = ""
        txtMailID.Text = ""
        cboCustType.Text = ""
        cboCustomerType.Text = ""
        loading = 0
    End Sub

    Private Sub dtpDateOfBirth_ValueChanged(sender As Object, e As EventArgs) Handles dtpDateOfBirth.ValueChanged
        txtAge.Text = DateDiff(DateInterval.Year, CDate(dtpDateOfBirth.Text), Today)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim query As String = "INSERT INTO Customer (CustomerName, DateOfBirth, Address, Mobile, eMailId, CustomerType) " &
                                " VALUES (@CName, @DOB, @Address, @Mobile, @MailId, @CType)"

        ' Create a new connection and command
        Using connection As New SqlConnection(conString)
            Using command As New SqlCommand(query, connection)
                ' Define parameters to avoid SQL injection
                command.Parameters.AddWithValue("@CName", txtCustomerName.Text)
                command.Parameters.AddWithValue("@DOB", dtpDateOfBirth.Value)
                command.Parameters.AddWithValue("@Address", txtAddress.Text)
                command.Parameters.AddWithValue("@Mobile", txtPhone.Text)
                command.Parameters.AddWithValue("@MailId", txtMailID.Text)
                command.Parameters.AddWithValue("@CType", cboCustType.Text)

                MsgBox(query)
                Debug.Print(query)
                ' Open the connection
                connection.Open()
                command.ExecuteNonQuery()

                MessageBox.Show("Record inserted successfully!")

                ' Execute the query and check if insertion was successful
                'Dim rowsAffected As Integer = command.ExecuteNonQuery()
                'If rowsAffected > 0 Then
                '  MessageBox.Show("Record inserted successfully!")
                'Else
                'MessageBox.Show("Insertion failed.")
                'End If
                loading = 1
                CmdText = "select CustomerId, CustomerName, FORMAT(dateofbirth,'dd-MM-yyyy') AS DateOfBirth, CustomerType, Address, Mobile, eMailId from Customer"
                LoadDateInGrid()
            End Using
        End Using
    End Sub

    Private Sub LoadDateInGrid()
        ' SQL query to fetch data
        If loading = 0 Then
            CmdText = "select CustomerId, CustomerName, FORMAT(dateofbirth,'dd-MM-yyyy') AS DateOfBirth, CustomerType, Address, Mobile, eMailId from Customer where customertype='" & cboCustomerType.Text & "'"
        End If
        ' Create a new connection and command
        Using connection As New SqlConnection(conString)
            Using command As New SqlCommand(CmdText, connection)

                ' Open the connection
                connection.Open()

                ' Use a DataAdapter to fill a DataTable
                Dim adapter As New SqlDataAdapter(command)
                Dim table As New DataTable()
                adapter.Fill(table)

                ' Bind the DataTable to the DataGridVifew
                dgvCustomer.DataSource = table
                loading = 0
            End Using  ' Disposes SqlCommand
        End Using     ' Disposes SqlConnection
    End Sub
End Class