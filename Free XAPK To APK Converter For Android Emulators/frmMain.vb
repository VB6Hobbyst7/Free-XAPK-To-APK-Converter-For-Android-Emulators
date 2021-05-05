Imports System.IO.Compression
Imports System.Runtime.InteropServices

Public Class frmMain
    Private AeroEnabled As Boolean
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
    Private Const IDC_HAND As Integer = 32649
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function LoadCursor(ByVal hInstance As IntPtr, ByVal lpCursorName As Integer) As IntPtr
    End Function

    Private Shared ReadOnly SystemHandCursor As Cursor = New Cursor(LoadCursor(IntPtr.Zero, IDC_HAND))
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button2.Cursor = SystemHandCursor
        Button3.Cursor = SystemHandCursor
        Button1.Cursor = SystemHandCursor
        Button4.Cursor = SystemHandCursor
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Me.Location = mousePos
        End If
    End Sub
    Private Sub CheckAeroEnabled()
        If Environment.OSVersion.Version.Major >= 6 Then
            Dim enabled As Integer = 0
            Dim response As Integer = NativeMethods.DwmIsCompositionEnabled(enabled)
            AeroEnabled = (enabled = 1)
        Else
            AeroEnabled = False
        End If
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            CheckAeroEnabled()
            Dim cp As CreateParams = MyBase.CreateParams
            If Not aeroEnabled Then
                cp.ClassStyle = cp.ClassStyle Or NativeConstants.CS_DROPSHADOW
                Return cp
            Else
                Return cp
            End If
        End Get
    End Property
    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case NativeConstants.WM_NCPAINT
                Dim val = 2
                If aeroEnabled Then
                    NativeMethods.DwmSetWindowAttribute(Handle, 2, val, 4)
                    Dim bla As New NativeStructs.MARGINS()
                    With bla
                        .bottomHeight = 1
                        .leftWidth = 0
                        .rightWidth = 0
                        .topHeight = 0
                    End With
                    NativeMethods.DwmExtendFrameIntoClientArea(Handle, bla)
                End If
                Exit Select
        End Select
        MyBase.WndProc(m)
    End Sub
End Class
Public Class NativeStructs
    Public Structure MARGINS
        Public leftWidth As Integer
        Public rightWidth As Integer
        Public topHeight As Integer
        Public bottomHeight As Integer
    End Structure
End Class
Public Class NativeMethods
    <DllImport("dwmapi")>
    Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As NativeStructs.MARGINS) As Integer
    End Function
    <DllImport("dwmapi")>
    Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
    End Function
    <DllImport("dwmapi.dll")>
    Public Shared Function DwmIsCompositionEnabled(ByRef pfEnabled As Integer) As Integer
    End Function
End Class
Public Class NativeConstants
    Public Const CS_DROPSHADOW As Integer = &H20000
    Public Const WM_NCPAINT As Integer = &H85
End Class
