Imports System.Data.SqlClient

Module MyMainModule
    Public conn As New SqlConnection
    Public cmd As New SqlCommand
    Public rd As SqlDataReader
    Public ad As New SqlDataAdapter
    Public ds As New DataSet
    Public dt As New DataTable
    ' Public conString As String = "Server=DESKTOP-ECHNAQT\SQL2019EXP;Database=VB_Dot_Net_SampleData;User Id=;Password=;"
    Public conString As String = "Data Source=DESKTOP-ECHNAQT\SQL2019EXP;Initial Catalog=VB_Dot_Net_SampleData;Integrated Security=True;"
End Module

