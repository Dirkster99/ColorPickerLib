@ECHO OFF
pushd "%~dp0"
ECHO.
ECHO.
ECHO.
ECHO This script deletes all temporary build files in their
ECHO corresponding BIN and OBJ Folder contained in the following projects
ECHO.
ECHO ColorPickerDemo
ECHO ColorPickerDemoLib
ECHO ColorPickerLib
ECHO.
ECHO MLibTest\ThemedDemo
ECHO MLibTest\Components\ServiceLocator
ECHO MLibTest\Components\Settings\Settings
ECHO MLibTest\Components\Settings\SettingsModel
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
ECHO Deleting BIN and OBJ Folders in Demos_Tests\ColorPicker\ColorPickerDemoLib project folder
ECHO.
RMDIR /S /Q ColorPickerDemoLib\bin
RMDIR /S /Q ColorPickerDemoLib\obj
ECHO.
ECHO 

ECHO.
ECHO Deleting BIN and OBJ Folders in Demos_Tests\ColorPicker\ColorPickerLib project folder
ECHO.
RMDIR /S /Q ColorPickerLib\bin
RMDIR /S /Q ColorPickerLib\obj
ECHO.
ECHO 

ECHO.
ECHO Deleting BIN and OBJ Folders in MLibTest\ThemedDemo project folder
ECHO.
RMDIR /S /Q MLibTest\ThemedDemo\bin
RMDIR /S /Q MLibTest\ThemedDemo\obj
ECHO.

ECHO.
ECHO Deleting BIN and OBJ Folders in MLibTest\Components\ServiceLocator project folder
ECHO.
RMDIR /S /Q MLibTest\Components\ServiceLocator\bin
RMDIR /S /Q MLibTest\Components\ServiceLocator\obj
ECHO.

ECHO.
ECHO Deleting BIN and OBJ Folders in MLibTest\Components\Settings\Settings project folder
ECHO.
RMDIR /S /Q MLibTest\Components\Settings\Settings\bin
RMDIR /S /Q MLibTest\Components\Settings\Settings\obj
ECHO.

ECHO.
ECHO Deleting BIN and OBJ Folders in MLibTest\Components\Settings\SettingsModel project folder
ECHO.
RMDIR /S /Q MLibTest\Components\Settings\SettingsModel\bin
RMDIR /S /Q MLibTest\Components\Settings\SettingsModel\obj
ECHO.

PAUSE