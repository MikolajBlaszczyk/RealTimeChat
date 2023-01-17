using RealTimeChat.API.DataAccess.KeyDataAccess;

namespace RealTimeChat.BusinessLogic
{
    public class ClosureHandler
    {
        private readonly DatabaseClosureManager DbClosureManager;

        public ClosureHandler(DatabaseClosureManager dbClosureManager)
        {
            DbClosureManager = dbClosureManager;
        }

        public async Task PerformNecessaryDataAccessAction()
        {
            await DbClosureManager.PerformBackup();
        }
    }
}
