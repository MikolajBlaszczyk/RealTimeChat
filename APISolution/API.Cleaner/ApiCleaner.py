import sys
from DataAccess import db

connectionString = sys.argv[1]

dataAccess = db(connectionString)

dataAccess.TerminateSessionInApi()

if sys.platform == 'linux':
    dataAccess.PerformBackup()
