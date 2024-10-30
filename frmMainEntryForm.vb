Public Class frmMainEntryForm
    Private Sub frmMainEntryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MsgBox("Main")
    End Sub

    Private Sub CustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerToolStripMenuItem.Click
        frmCustomer.Show()
    End Sub
End Class