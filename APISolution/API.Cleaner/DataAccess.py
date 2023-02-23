import pyodbc


class db:
    def __init__(self, connectionString):
        self.driverAndConnectionString = f'''
                                            DRIVER={'{SQL Server}'};
                                            {connectionString}
                                            '''
        self.queryToTerminateSession = 'DELETE FROM RealTimeChatAPI.dbo.Session'
        self.queryToPerformBackup = 'exec sp_CreateBackup'

        connection = pyodbc.connect(self.driverAndConnectionString)
        self.cursor = connection.cursor()

    def TerminateSessionInApi(self):
        self.cursor.execute(self.queryToTerminateSession)
        self.cursor.commit()

    def PerformBackup(self):
        self.cursor.execute(self.queryToPerformBackup)
        self.cursor.commit()
