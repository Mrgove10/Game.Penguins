using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Code.Penguin
{
    class Penguin : IPenguin
    {
        public IPlayer Player => throw new NotImplementedException();
    }
}
