#include <APIConstants.au3>
#include <GUIConstantsEx.au3>
#include <WinAPI.au3>

; Credits to guinness for coming up with this script


Global $sidebarWidth

If $CmdLine[0] = 0 Then
;MsgBox(64, "Result", "No variable was found.")
$sidebarWidth = 0;
Else
;MsgBox(64, "Variable", $CmdLine[1])
$sidebarWidth = $CmdLine[1];
EndIf

Example()


Func Example()
    ; Set the working area of the Desktop, in this case 120px to the left and retaining the same height and width.

    Local $aWorkingArea = _WorkingArea(0, Default, @DeskTopWidth-$sidebarWidth, Default)
	;width, height; x; y

    ; Create the GUI.
	; $WS_EX_LAYERED
    Local $hGUI = GUICreate('', $sidebarWidth, $aWorkingArea[1], @DeskTopWidth-$sidebarWidth, $aWorkingArea[3], $WS_POPUP, $WS_EX_LAYERED)
    GUISetState(@SW_SHOW, $hGUI)
    Exit
    While 1
        Switch GUIGetMsg()
            Case $GUI_EVENT_CLOSE
                ExitLoop

        EndSwitch
    WEnd

    ; Delete the GUI.
    GUIDelete($hGUI)

    ; Reset the working area to the previous values.
    _WorkingArea()

 EndFunc   ;==>Example



Func _WorkingArea($iLeft = Default, $iTop = Default, $iWidth = Default, $iHeight = Default)
    Local Static $tWorkArea = 0
    If IsDllStruct($tWorkArea) Then
        _WinAPI_SystemParametersInfo($SPI_SETWORKAREA, 0, DllStructGetPtr($tWorkArea), $SPIF_SENDCHANGE)
        $tWorkArea = 0
    Else
        $tWorkArea = DllStructCreate($tagRECT)
        _WinAPI_SystemParametersInfo($SPI_GETWORKAREA, 0, DllStructGetPtr($tWorkArea))

        Local $tCurrentArea = DllStructCreate($tagRECT)
        Local $aArray[4] = [$iLeft, $iTop, $iWidth, $iHeight]
        For $i = 0 To 3
            If $aArray[$i] = Default Or $aArray[$i] < 0 Then
                $aArray[$i] = DllStructGetData($tWorkArea, $i + 1)
            EndIf
            DllStructSetData($tCurrentArea, $i + 1, $aArray[$i])
            $aArray[$i] = DllStructGetData($tWorkArea, $i + 1)
        Next
        _WinAPI_SystemParametersInfo($SPI_SETWORKAREA, 0, DllStructGetPtr($tCurrentArea), $SPIF_SENDCHANGE)
        $aArray[2] -= $aArray[0]
        $aArray[3] -= $aArray[1]
        Local $aReturn[4] = [$aArray[2], $aArray[3], $aArray[0], $aArray[1]]
        Return $aReturn
    EndIf
EndFunc   ;==>_WorkingArea