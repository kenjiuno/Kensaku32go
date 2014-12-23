call "H:\Vs11\VC\vcvarsall.bat" x86
msbuild Kensaku32go.sln /p:Configuration=Debug /p:Platform=x86
pause
msbuild Kensaku32go.sln /p:Configuration=Debug /p:Platform=x64
pause
"C:\Program Files (x86)\NSIS\makensis.exe" Setup_Kensaku32go.nsi
pause