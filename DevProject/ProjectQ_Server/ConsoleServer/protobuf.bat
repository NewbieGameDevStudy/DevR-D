@echo off

set protoListPath=%CD%\Packet\Protobuf\
set protocPath=%CD%\packages\Google.Protobuf.Tools.3.5.1\tools\windows_x64\
set rootPath=%CD%
set SRC_DIR=%protoListPath%
set DST_DIR=%protoListPath%

cd %protoListPath%

del /q "*.cs"

dir /b >protoList.txt

cd %protocPath%


set protoListFile=%protoListPath%protoList.txt

for /f "delims=" %%i in (%protoListFile%) do (

 if %%i == protoList.txt goto END

 protoc.exe -I=%SRC_DIR% --csharp_out=%DST_DIR% %SRC_DIR%/%%i
)

:END

cd %protoListPath%



pause