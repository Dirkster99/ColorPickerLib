[![Build status](https://ci.appveyor.com/api/projects/status/0e19uif3f40tsppi?svg=true)](https://ci.appveyor.com/project/Dirkster99/colorpickerlib)
[![Release](https://img.shields.io/github/release/Dirkster99/colorpickerlib.svg)](https://github.com/Dirkster99/colorpickerlib/releases/latest)
[![NuGet](https://img.shields.io/nuget/dt/Dirkster.colorpickerlib.svg)](http://nuget.org/packages/Dirkster.colorpickerlib)
# ColorPickerLib
A WPF/MVVM implementation of a themeable color picker control.

<table>
<tr>
<td>
<img src="https://github.com/Dirkster99/Docu/blob/master/ColorPickerLib/DemoProgramm_Screenshot.png">
</td>
<td>
<img src="https://github.com/Dirkster99/Docu/blob/master/ColorPickerLib/DemoProgramm_Screenshot_PopUp1.png">
</td>
</tr>
</table>

![](https://github.com/Dirkster99/Docu/blob/master/ColorPickerLib/DemoProgramm_Screenshot_PopUp1.png)

# Project Description

This project implements a Color Picker control using MVVM/Windows Presentation Foundation (WPF) pattern and technology. These controls  can be themed in dark and light themes and are localized.

You can test these controls with the demo application provided in this repository.

# Features

This version of the control implements:

* a few bug fixes (eg.: you cannot enter letters or more than 3 digits in a channel input element).

Supports localization for:
* Chinese (Simplified) and Chinese (Traditional)
* Dutch
* English
* French
* German
* Hindi
* Indonesian
* Italian
* Japanese
* Spanish

These 4 styles have to be included to avoid a transparent background in the pop-up control:

```XAML
    <!-- Popup Background -->
    <SolidColorBrush x:Key="PopupBackgroundBrush"  Color="#FF252526" />
    
    <!-- Popup Border -->
    <SolidColorBrush x:Key="PopupDarkBorderBrush" Color="#FFABADB3" />
    
    <!-- =============================================================================== -->
    <!-- ColorCanvas, ColorPicker                                                        -->
    <!-- =============================================================================== -->
    
    <DrawingBrush  x:Key="CheckerBrush" Viewport="0,0,10,10" ViewportUnits="Absolute" TileMode="Tile">
        <DrawingBrush.Drawing>
            <DrawingGroup>
                <GeometryDrawing Brush="White">
                    <GeometryDrawing.Geometry>
                        <RectangleGeometry Rect="0,0 100,100" />
                    </GeometryDrawing.Geometry>
                </GeometryDrawing>
                <GeometryDrawing Brush="LightGray">
                    <GeometryDrawing.Geometry>
                        <GeometryGroup>
                            <RectangleGeometry Rect="0,0 50,50" />
                            <RectangleGeometry Rect="50,50 50,50" />
                        </GeometryGroup>
                    </GeometryDrawing.Geometry>
                </GeometryDrawing>
            </DrawingGroup>
        </DrawingBrush.Drawing>
    </DrawingBrush>
    
    <SolidColorBrush x:Key="ColorPickerDarkBorderBrush" Color="Black" />
```

## Theming

Load *Light* or *Dark* brush resources in you resource dictionary to take advantage of existing definitions.

```XAML
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="/ColorPickerLib;component/Themes/DarkBrushs.xaml" />
    </ResourceDictionary.MergedDictionaries>
```

```XAML
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="/ColorPickerLib;component/Themes/LightBrushs.xaml" />
    </ResourceDictionary.MergedDictionaries>
```

These definitions do not theme all controls used within this library. You should use a standard theming library, such as:
- [MahApps.Metro](https://github.com/MahApps/MahApps.Metro),
- [MLib](https://github.com/Dirkster99/MLib), or
- [MUI](https://github.com/firstfloorsoftware/mui)

to also theme standard elements, such as, button and textblock etc.

# References

This project implements a WPF Color Picker control.

The project is based on:
* The color picker control contained in the Extended WPF Toolkit™ Community Edition: https://github.com/xceedsoftware/wpftoolkit from Xceed

* See also [WPF Color Picker Control](https://wpfcolorpickercontrol.codeplex.com/) on [Codeplex](https://codeplex.com/).
