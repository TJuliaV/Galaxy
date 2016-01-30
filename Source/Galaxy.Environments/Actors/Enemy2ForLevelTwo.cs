using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Galaxy.Environments.Actors
{
    class Enemy2ForLevelTwo : Enemy1ForLevelTwo
    {

        #region Constructors

          public Enemy2ForLevelTwo(ILevelInfo info)
              : base(info)
        {
          Width = 30;
          Height = 30;
          ActorType = ActorType.Enemy;
        }

        #endregion

        #region Overrides

        public override void Load()
        {
          Load(@"Assets\spaceship.png");
          if (m_flyTimer == null)
          {
            m_flyTimer = new Stopwatch();
            m_flyTimer.Start();
          }
        }

        #endregion
    }
}
