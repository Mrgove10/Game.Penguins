using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Code.Helper
{
    class PlayOrder
    {
        /// <summary>
        /// Randomises the Player turns
        /// </summary>
        /// <returns></returns>
       /* private IList<IPlayer> GeneratePlayOrder()
        {
            IList<IPlayer> CopyStartList = new List<IPlayer>(Players); //local copy only for thsi function
            List<IPlayer> RandomList = new List<IPlayer>();
            Random r = new Random();
            int randomIndex = 0;
            while (CopyStartList.Count > 0)
            {
                randomIndex = r.Next(0, CopyStartList.Count);
                RandomList.Add(CopyStartList[randomIndex]);
                CopyStartList.RemoveAt(randomIndex);
            }

            return RandomList;
        }*/
    }
}
