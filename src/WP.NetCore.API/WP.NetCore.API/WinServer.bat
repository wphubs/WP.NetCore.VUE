set nssm=nssm.exe
set AppName=WP.NetCore.API
sc create %AppName% binpath= "%~dp0%nssm%" start= auto
REG ADD "HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\services\%AppName%\Parameters" /v "AppDirectory" /t REG_SZ /d %~dp0 /f
REG ADD "HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\services\%AppName%\Parameters" /v "Application" /t REG_SZ /d "C:\Program Files\dotnet\dotnet.exe" /f
REG ADD "HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\services\%AppName%\Parameters" /v "AppParameters" /t REG_SZ /d "%AppName%.dll" /f
sc start %AppName%