@echo off
echo ��ʼ���Ƶ���ĿĿ¼
xcopy ExportFiles\binary\*.dat C:\MySoftware\UnityProjects\BravePunishing\Assets\Sources\Config /y
xcopy ExportFiles\cs\*.cs C:\MySoftware\UnityProjects\BravePunishing\Assets\Sources\Code\HotUpdate\Config /y
xcopy ExportJson\*.json C:\MySoftware\UnityProjects\BravePunishing\Assets\Editor\ConfigJson /y
echo �������
pause