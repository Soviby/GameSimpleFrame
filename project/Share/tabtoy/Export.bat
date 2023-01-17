.\tabtoy.exe ^
--mode=v2 ^
--csharp_out=..\..\Assets\Scripts\Data\Config.cs ^
--binary_out=..\..\Assets\Resources\DB\Config.bytes ^
--combinename=Config ^
--lan=zh_cn ^
.\xlsx\Globals.xlsx ^
.\xlsx\BattlePlayer.xlsx ^
.\xlsx\Sample.xlsx

@IF %ERRORLEVEL% NEQ 0 pause