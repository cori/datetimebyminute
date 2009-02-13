@setlocal
@pushd.

@set PATH=C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN;%PATH% @cd %~dp0

stsadm.exe -o addsolution -filename ETC.Sharepoint.DateTimeByMinute.wsp
stsadm.exe -o deploysolution -name ETC.Sharepoint.DateTimeByMinute.wsp -immediate -url http://localhost:81 -allowGacDeployment

@pause
@popd
@endlocal