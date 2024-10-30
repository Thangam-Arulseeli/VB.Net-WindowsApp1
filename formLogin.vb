Public Class formLogin
    Private Sub formLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conn.ConnectionString = conString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtUseName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUserName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLogin_Click(sender, e)
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Static entryCount As Integer, str As String
        Dim entryForm As New frmMainEntryForm
        If Not (txtUserName.Text = "" Or txtPassword.Text = "") Then
            Try
                conn.Open()
                cmd.Connection = conn
                str = "select * from UserDetails where username='" + txtUserName.Text + "'  and password='" + txtPassword.Text + "'"
                cmd.CommandText = str
                Debug.Print(str)
                rd = cmd.ExecuteReader()
                If rd.Read() Then
                    entryForm.Show()
                    entryForm.Focus()
                    Me.Hide()
                Else
                    entryCount = entryCount + 1
                    MsgBox("You are not an authorized user; Please try again", MsgBoxStyle.Critical)
                    txtUserName.Text = ""
                    txtPassword.Text = ""
                    If entryCount = 3 Then
                        MsgBox("You entered wrong for 3 times; Try to login later")
                        End
                    End If
                    txtUserName.Focus()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                ' Close the connection if open
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End If
    End Sub
End Class