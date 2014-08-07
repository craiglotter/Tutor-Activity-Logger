Imports System.IO

Public Class Activity_Log

    Public Log_ID As String
    Public Log_Tutor As String
    Public Log_Date As String
    Public Log_Duration As String
    Public Log_Time As String
    Public Log_User_Type As String
    Public Log_Personal_Details_ID As String
    Public Log_Personal_Details_Surname As String
    Public Log_Personal_Details_Firstname As String
    Public Log_Call_Type As String
    Public Log_Call_Sub_Type As String
    Public Log_Resolve_Status As String
    Public Log_Resolve_Date As String
    Public Log_Resolve_Time As String
    Public Log_Resolve_Tutor As String
    Public Log_Problem_Description As String
    Public Log_Problem_Resolution As String
    Public Log_Modify_Date As String
    Public Log_Modify_Time As String
    Public Log_Modify_Tutor As String

    Public Sub New()
        Try
            Log_ID = ""
            Log_Tutor = ""
            Log_Date = ""
            Log_Duration = ""
            Log_Time = ""
            Log_User_Type = ""
            Log_Personal_Details_ID = ""
        Log_Personal_Details_Surname = ""
            Log_Personal_Details_Firstname = ""
        Log_Call_Type = ""
        Log_Call_Sub_Type = ""
        Log_Resolve_Status = ""
        Log_Resolve_Date = ""
        Log_Resolve_Time = ""
        Log_Resolve_Tutor = ""
        Log_Problem_Description = ""
        Log_Problem_Resolution = ""
        Log_Modify_Date = ""
        Log_Modify_Time = ""
            Log_Modify_Tutor = ""
        Catch ex As Exception
            Error_Handler(ex, "New Activity Log Declaration")
        End Try
    End Sub

    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            If ex.Message.IndexOf("Thread was being aborted") < 0 Then
                Dim Display_Message1 As New Display_Message("The Application encountered the following problem: " & vbCrLf & identifier_msg & ":" & ex.ToString)
                Display_Message1.ShowDialog()
                Dim dir As DirectoryInfo = New DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
                If dir.Exists = False Then
                    dir.Create()
                End If
                Dim filewriter As StreamWriter = New StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
                filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ":" & ex.ToString)
                filewriter.Flush()
                filewriter.Close()
            End If
        Catch exc As Exception
            MsgBox("An error occurred in Tutor Activity Logger's error handling routine. The application will try to recover from this serious error.", MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub

    Protected Friend Function Generate_SQL_Insert() As String
        Dim result As String = ""
        Try
            result = "INSERT INTO [Log_Records] ([Log_ID], [Log_Tutor], [Log_Date], [Log_Time], [Log_Duration], [Log_User_Type], [Log_Personal_Details_ID], [Log_Personal_Details_Surname], [Log_Personal_Details_Firstname], [Log_Call_Type], [Log_Call_Sub_Type], [Log_Resolve_Date], [Log_Resolve_Time], [Log_Resolve_Tutor], [Log_Resolve_Status], [Log_Problem_Description], [Log_Problem_Resolution], [Log_Modify_Date], [Log_Modify_Time], [Log_Modify_Tutor]) VALUES ("
            result = result & "'" & Log_ID & "', "
            result = result & "'" & Log_Tutor & "', "
            result = result & "'" & Log_Date & "', "
            result = result & "'" & Log_Time & "', "
            result = result & "'" & Log_Duration & "', "
            result = result & "'" & Log_User_Type & "', "
            result = result & "'" & Log_Personal_Details_ID & "', "
            result = result & "'" & Log_Personal_Details_Surname & "', "
            result = result & "'" & Log_Personal_Details_Firstname & "', "
            result = result & "'" & Log_Call_Type & "', "
            result = result & "'" & Log_Call_Sub_Type & "', "
            result = result & "'" & Log_Resolve_Date & "', "
            result = result & "'" & Log_Resolve_Time & "', "
            result = result & "'" & Log_Resolve_Tutor & "', "
            result = result & "'" & Log_Resolve_Status & "', "
            result = result & "'" & Log_Problem_Description & "', "
            result = result & "'" & Log_Problem_Resolution & "', "
            result = result & "'" & Log_Modify_Date & "', "
            result = result & "'" & Log_Modify_Time & "', "
            result = result & "'" & Log_Modify_Tutor & "')"
        Catch ex As Exception
            Error_Handler(ex, "Clear Activity Log")
            result = "Fail"
        End Try
        Return result
    End Function

    Protected Friend Sub Clear_Data()
        Try
            Log_ID = ""
            Log_Tutor = ""
            Log_Date = ""
            Log_Duration = ""
            Log_Time = ""
            Log_User_Type = ""
            Log_Personal_Details_ID = ""
            Log_Personal_Details_Surname = ""
            Log_Personal_Details_Firstname = ""
            Log_Call_Type = ""
            Log_Call_Sub_Type = ""
            Log_Resolve_Status = ""
            Log_Resolve_Date = ""
            Log_Resolve_Time = ""
            Log_Resolve_Tutor = ""
            Log_Problem_Description = ""
            Log_Problem_Resolution = ""
            Log_Modify_Date = ""
            Log_Modify_Time = ""
            Log_Modify_Tutor = ""
        Catch ex As Exception
            Error_Handler(ex, "Clear Activity Log")
        End Try
    End Sub

    Protected Friend Function MessageBox() As String
        Try
            Dim result As String
            result = ""
            result = result & Log_ID & vbCrLf
            result = result & Log_Tutor & vbCrLf
            result = result & Log_Date & vbCrLf
            result = result & Log_Duration & vbCrLf
            result = result & Log_Time & vbCrLf
            result = result & Log_User_Type & vbCrLf
            result = result & Log_Personal_Details_ID & vbCrLf
            result = result & Log_Personal_Details_Surname & vbCrLf
            result = result & Log_Personal_Details_Firstname & vbCrLf
            result = result & Log_Call_Type & vbCrLf
            result = result & Log_Call_Sub_Type & vbCrLf
            result = result & Log_Resolve_Status & vbCrLf
            result = result & Log_Resolve_Date & vbCrLf
            result = result & Log_Resolve_Time & vbCrLf
            result = result & Log_Resolve_Tutor & vbCrLf
            result = result & Log_Problem_Description & vbCrLf
            result = result & Log_Problem_Resolution & vbCrLf
            result = result & Log_Modify_Date & vbCrLf
            result = result & Log_Modify_Time & vbCrLf
            result = result & Log_Modify_Tutor
            Return result
        Catch ex As Exception
            Error_Handler(ex, "Clear Activity Log")
            Return "Failed"
        End Try
    End Function
End Class


