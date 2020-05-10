# USB-Explorer
A tool that reads data stored under USBSTOR key in the system registry hive, representing information about connected USB storage devices. The work is based on Eric Zimmerman's [Registry Explorer](https://github.com/EricZimmerman/Registry). The result is presented in tabular format.

# Features
1. Provides serial number, first insertion, last insertion, and last removal dates.
2. Can process live system hive (requires admin privileges).

# Limitations
1. Development and tests were done on Windows 10 only, so it might not work with other Windows profiles.
2. Data about USB connections can be found in several locations other than USBSTOR. For now, this tool only reads USBSTOR.
3. The tool uses standard Windows grid from the Windows Forms control suite, so no fancy DevExpress grids or colors (Although might consider using them later).

# Screenshot
![alt text](https://github.com/karimabisaad/USB-Explorer/blob/master/screenshot.png)
