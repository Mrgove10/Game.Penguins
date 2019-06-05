using Common.Logging;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using System.Collections.Generic;

namespace Game.Penguins.Core.Code.Helper
{
    public class EndGameHelper
    {
        private readonly ILog _log = LogManager.GetLogger<EndGameHelper>(); //http://netcommon.sourceforge.net/docs/2.1.0/reference/html/ch01.html#logging-usage

        /// <summary>
        /// Verifies end game
        /// </summary>
        public bool VerifyEndGame(NextActionType action, IList<IPlayer> players)
        {
            int playerAlive = 0;

            //TODO si penguin == 0;
            foreach (IPlayer player in players)
            {
                if (player.Penguins > 0)
                {
                    playerAlive += 1;
                }
            }

            if (playerAlive == 0)
            {
                //GAME-OVER
                action = NextActionType.Nothing;
                _log.Warn(" -- FIN DU JEU -- ");
                return true;
            }
            else
            {
                return false;
            }

            //Next actionType == nothing
        }
    }
}