; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

Unicode true

SetCompressor /solid lzma

!include "MUI2.nsh"

!define APP "Kensaku32go"
!define TTL "検索32号"

; The name of the installer
Name "${TTL}"

; The file to write
OutFile "Setup_${APP}.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel user

XPStyle on

!define MUI_ABORTWARNING

;--------------------------------

; Pages

!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_INSTFILES

;--------------------------------

!insertmacro MUI_LANGUAGE "Japanese"

!include "LogicLib.nsh"
!include "x64.nsh"

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there

SectionEnd ; end the section

Function Shtcut
  StrCpy $1 "$DESKTOP"
  CreateShortCut "$1\検索32号.lnk" "$INSTDIR\${APP}.exe"

  StrCpy $1 "$SMPROGRAMS\検索32号"
  CreateDirectory $1
  CreateShortCut "$1\検索32号.lnk" "$INSTDIR\${APP}.exe"
  CreateShortCut "$1\けんさく32ごう.lnk" "$INSTDIR\${APP}.exe"
  CreateShortCut "$1\Kensaku32go.lnk" "$INSTDIR\${APP}.exe"

  WriteRegStr HKCU "Software\Classes\.Kensaku32go" "" "Kensaku32go"
  WriteRegStr HKCU "Software\Classes\.Kensaku32go\ShellNew" "ItemName" "検索32号の辞書"
  WriteRegStr HKCU "Software\Classes\.Kensaku32go\ShellNew" "NullFile" ""

  WriteRegStr HKCU "Software\Classes\Kensaku32go" "" "検索32号"
  WriteRegStr HKCU "Software\Classes\Kensaku32go" "NeverShowExt" ""
  WriteRegStr HKCU "Software\Classes\Kensaku32go\DefaultIcon" "" "$INSTDIR\${APP}.exe"
  WriteRegStr HKCU "Software\Classes\Kensaku32go\shell\open\command" "" '"$INSTDIR\${APP}.exe" "%1"'

  System::Call 'Shell32::SHChangeNotify(i 0x8000000, i 0, i 0, i 0)'
FunctionEnd

Section /o  "x86" o86
  SetOutPath "$INSTDIR"
  File /r /x "*.vshost.*" "bin\x86\DEBUG\*.*"
  File                        "x86\*.*"

  StrCpy $2 "32"
  Call Shtcut
SectionEnd

Section /o  "x64" o64
  SetOutPath "$INSTDIR"
  File /r /x "*.vshost.*" "bin\x64\DEBUG\*.*"
  File                        "x64\*.*"

  StrCpy $2 "64"
  Call Shtcut
SectionEnd

Function .onInit
  ${If} ${RunningX64}
    SectionSetFlags ${o86} 0
    SectionSetFlags ${o64} ${SF_SELECTED}
  ${Else}
    SectionSetFlags ${o64} 0
    SectionSetFlags ${o86} ${SF_SELECTED}
  ${EndIf}
FunctionEnd
