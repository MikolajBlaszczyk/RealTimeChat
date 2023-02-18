RESTORE DATABASE [RealTimeChatApi] FROM DISK = '/tmp/RTC.bak'
WITH FILE = 1,
MOVE 'RealTimeChatAPI' TO '/var/opt/mssql/data/RTC.mdf',
MOVE 'RealTimeChatAPI_log' TO '/var/opt/mssql/data/RTC.ldf',
NOUNLOAD, REPLACE, STATS = 5
GO 
