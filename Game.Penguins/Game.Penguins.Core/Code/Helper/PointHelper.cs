using Common.Logging;
using Game.Penguins.Core.Code.Players;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Code.Helper
{
    public class PointHelper
    {
        private readonly ILog _log = LogManager.GetLogger<PointHelper>();

        /// <summary>
        /// Updates the player's points count
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pointToAdd"></param>
        public void UpdatePlayerPoints(IPlayer player, int pointToAdd)
        {
            _log.Debug("Current player has " + player.Points + " adding  " + pointToAdd);
            Player cp = (Player)player;
            cp.Points += pointToAdd; //adds the points to the current player score
            _log.Debug("Player now has " + player.Points);
        }
    }
}