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
    class Enemy2ForLevelTwo: BaseActor
    {
            #region Constant

    protected const int MaxSpeed = 1;
    protected const long StartFlyMs = 1000;

    #endregion

    #region Private fields

    protected bool m_flying;
    protected Stopwatch m_flyTimer;

    #endregion

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

    public override void Update()
    {
      base.Update();

      if (!IsAlive)
        return;

      if (!m_flying)
      {
        if (m_flyTimer.ElapsedMilliseconds <= StartFlyMs) return;

        m_flyTimer.Stop();
        m_flyTimer = null;
        h_changePosition();
        m_flying = true;
      }
      else
      {
        h_changePosition();
      }
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

    #region Private methods

    private void h_changePosition()
    {
            Position = new Point(Position.X - 1, (int)(Position.Y + Math.Round(Math.Cos(Position.X / 50))));
    }

    #endregion
    }
}
