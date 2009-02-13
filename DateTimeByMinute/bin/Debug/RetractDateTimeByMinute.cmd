@setlocal
@pushd.

@set PATH=C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN;%PATH% @cd %~dp0

stsadm.exe -o retractsolution -name ETC.Sharepoint.DateTimeByMinute.wsp -immediate -url http://localhost:81
stsadm.exe -o deletesolution -name ETC.Sharepoint.DateTimeByMinute.wsp

@pause
@popd
@endlocal