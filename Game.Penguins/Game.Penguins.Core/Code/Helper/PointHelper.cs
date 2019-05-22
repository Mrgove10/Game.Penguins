using Game.Penguins.Core.Code.Players;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Code.Helper
{
    public class PointHelper
    {
        /// <summary>
        /// Updates the player's points count
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public void UpdatePlayerPoints(IPlayer player, int pointToAdd)
        {
            Player cp = (Player)player;
            cp.Points += pointToAdd; //addes the points to the current player score
        }
    }
}