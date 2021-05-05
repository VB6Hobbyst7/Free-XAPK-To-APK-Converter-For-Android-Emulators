Imports System.Runtime.InteropServices

Public Class frmAbout
    Private Const IDC_HAND As Integer = 32649

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function LoadCursor(ByVal hInstance As IntPtr, ByVal lpCursorName As Integer) As IntPtr
    End Function

    Private Shared ReadOnly SystemHandCursor As Cursor = New Cursor(LoadCursor(IntPtr.Zero, IDC_HAND))

    Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        picBlurDest.Cursor = SystemHandCursor
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles picBlurDest.Click
        Process.Start("https://github.com/ProFoxyfy")
    End Sub

    Private Sub picBlurDest_MouseEnter(sender As Object, e As EventArgs) Handles picBlurDest.MouseEnter
        picBlurDest.BackColor = SystemColors.Control
    End Sub

    Private Sub picBlurDest_MouseLeave(sender As Object, e As EventArgs) Handles picBlurDest.MouseLeave
        picBlurDest.BackColor = Color.White
    End Sub
End Class