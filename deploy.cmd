@echo Off
SETLOCAL
set config=%1

if "%config%" == "" (
  set config=debug
)


set EnableNuGetPackageRestore=true
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild Deploy.proj /p:Configuration="%config%" /p:Platform="Any CPU" /m /v:M /nr:false
ENDLOCAL