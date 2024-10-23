@echo off
REM Set variables for directories relative to src
set OUT_DIR=../out
set LIB_DIR=../libs

REM Clean up old compiled files
echo Cleaning up old compiled files...
if exist "%OUT_DIR%" rd /s /q "%OUT_DIR%"
if exist "%LIB_DIR%" rd /s /q "%LIB_DIR%"

REM Recreate directories
mkdir "%OUT_DIR%"
mkdir "%LIB_DIR%"

REM Compile all Java files into the out directory, one folder at a time
echo Compiling Java files...

javac -d "%OUT_DIR%" libinterfacecbbbase/*.java
javac -d "%OUT_DIR%" libinterfacesofcolors/*.java
javac -d "%OUT_DIR%" libinterfacesofshapes/*.java
javac -d "%OUT_DIR%" libshapecircle/*.java
javac -d "%OUT_DIR%" libshaperectangle/*.java
javac -d "%OUT_DIR%" libcolorblue/*.java
javac -d "%OUT_DIR%" libcolorred/*.java
javac -d "%OUT_DIR%" cbbbuilder/*.java
javac -d "%OUT_DIR%" drawer/*.java

REM Check if compilation was successful
if errorlevel 1 (
    echo Compilation failed. Exiting...
    exit /b 1
)

REM Create JAR files for each package
echo Creating JAR files...
jar cvf "%LIB_DIR%/libinterfacecbbbase.jar" -C "%OUT_DIR%" libinterfacecbbbase
jar cvf "%LIB_DIR%/libinterfacesofcolors.jar" -C "%OUT_DIR%" libinterfacesofcolors
jar cvf "%LIB_DIR%/libinterfacesofshapes.jar" -C "%OUT_DIR%" libinterfacesofshapes
jar cvf "%LIB_DIR%/libshapecircle.jar" -C "%OUT_DIR%" libshapecircle
jar cvf "%LIB_DIR%/libshaperectangle.jar" -C "%OUT_DIR%" libshaperectangle
jar cvf "%LIB_DIR%/libcolorblue.jar" -C "%OUT_DIR%" libcolorblue
jar cvf "%LIB_DIR%/libcolorred.jar" -C "%OUT_DIR%" libcolorred
jar cvf "%LIB_DIR%/cbbbuilder.jar" -C "%OUT_DIR%" libcolorred

REM Check if JAR creation was successful
if errorlevel 1 (
    echo JAR creation failed. Exiting...
    exit /b 1
)

REM Ensure drawer\libs directory exists
if not exist "drawer\libs" (
    mkdir "drawer\libs"
)

REM Copy JAR files to the lib directory inside drawer
echo Copying JAR files to the drawer libs directory...
copy "%LIB_DIR%\*.jar" "drawer\libs\"

REM Indicate success
echo Build and JAR creation successful. JAR files are ready to use in /drawer/libs/.
pause
