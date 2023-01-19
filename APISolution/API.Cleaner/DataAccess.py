import pyodbc

driverAndConnectionString = '''
                            DRIVER={SQL Server};
                            Server=MIKOŁAJ\SQLEXPRESS;
                            Database=RealTimeChatAPI;
                            User Id=RealUser;
                            Password='RealPassword;
                            '''

queryToTerminateSession = 'DELETE FROM RealTimeChatAPI.dbo.Session'
queryToPerformBackup = 'exec sp_CreateBackup'

connection = pyodbc.connect(driverAndConnectionString)
cursor = connection.cursor()


def TerminateSessionInApi():
    cursor.execute(queryToTerminateSession)
    cursor.commit()


def PerformBackup():
    cursor.execute(queryToPerformBackup)
    cursor.commit()
