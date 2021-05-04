Imports System.IO.Compression
Public Class frmMain
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub OpenFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFolderToolStripMenuItem.Click
        OpenApkFolder()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ConvertToApk()
    End Sub

    Private Sub ConvertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertToolStripMenuItem.Click
        ConvertToApk()
    End Sub
    Private Sub ConvertToApk()
        If Not My.Computer.FileSystem.DirectoryExists("C:\Converted XAPK's\Current\") Then
            My.Computer.FileSystem.CreateDirectory("C:\Converted XAPK's\")
            My.Computer.FileSystem.CreateDirectory("C:\Converted XAPK's\Current\")
        End If
        ZipFile.ExtractToDirectory(TextBox1.Text, "C:\Converted XAPK's\Current\")
        MsgBox("Extraction Completed! Thank you for using Free XAPK To APK Converter!")
    End Sub
    Private Sub OpenApkFolder()
        Process.Start("explorer.exe", ControlChars.Quote + "C:\Converted XAPK's\Current\" + ControlChars.Quote)
    End Sub
End Class
