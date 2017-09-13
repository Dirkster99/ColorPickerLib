@ECHO OFF
pushd "%~dp0"
ECHO.
ECHO.
ECHO.
ECHO This script deletes all temporary build files in their
ECHO corresponding BIN and OBJ Folder contained in the following projects
ECHO.
ECHO ColorPickerDemo
ECHO ColorPickerLib
ECHO.
REM Ask the user if hes really sure to continue beyond this point XXXXXXXX
set /p choice=Are you sure to continue (Y/N)?
if not '%choice%'=='Y' Goto EndOfBatch
REM Script does not continue unless user types 'Y' in upper case letter
ECHO.
ECHO XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
ECHO.
ECHO XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

RMDIR /S /Q .\.vs

ECHO.
ECHO Deleting BIN and OBJ Folders in Demos_Tests\ColorPicker\ColorPickerDemo project folder
ECHO.
RMDIR /S /Q ColorPickerDemo\bin
RMDIR /S /Q ColorPickerDemo\obj
ECHO.
ECHO 


ECHO.
ECHO Deleting BIN and OBJ Folders in Demos_Tests\ColorPicker\ColorPickerLib project folder
ECHO.
RMDIR /S /Q ColorPickerLib\bin
RMDIR /S /Q ColorPickerLib\obj
ECHO.
ECHO 
