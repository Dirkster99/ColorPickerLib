MLib
----

MLib is a set of WPF theming libraries based on MahApps.Metro, MUI,
and Infragistics Themes For Microsoft Controls.

This set of theming libraries is used to power several Windows WPF Dektop app projects:

- Edi
  https://github.com/Dirkster99/Edi

- File System Controls (TestExplorerMLib and ThemedExplorer projects)
  https://github.com/Dirkster99/fsc/wiki/FSC-Themeable-Explorer-(Clone)

MLib supports styling of different Window controls (MainWindow, ContentDialog, or Dialog)
but does not implement this function in the core theming library.

1) The Window and Dialog implementation for MLib is implemented in a seperate NuGet
   package MWindowLib (comes without ContentDialog support)

2) The ContentDialog implementation is available in a seperate NuGet library package:
   MWindowDialogLib (Includes MWindowLib and ContentDialog support)


Features
--------

All styles are available with a Light and Dark theme.

The framework supports a dialog service that supports ContentDialogs and
Modal Dialogs using one seemless API:
https://www.codeproject.com/Articles/1170500/A-ContentDialog-in-a-WPF-Desktop-Application

Supported OS
------------

This framework is designed with Windows 10 UI guidelines in mind but it should
also work for Windows 7 or 8.