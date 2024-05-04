@echo off
echo 开始复制到项目目录
xcopy ExportFiles\binary\*.dat F:\UnityProjects\BravePunishing\Assets\Sources\Config /y
xcopy ExportFiles\cs\*.cs F:\UnityProjects\BravePunishing\Assets\Sources\Code\HotUpdate\Config /y
echo 复制完成
pause