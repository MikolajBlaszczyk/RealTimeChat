import sys
from DataAccess import db

connectionString = sys.argv[0]

dataAccess = db(connectionString)

dataAccess.TerminateSessionInApi()

if sys.platform == 'linux':
    dataAccess.PerformBackup()
