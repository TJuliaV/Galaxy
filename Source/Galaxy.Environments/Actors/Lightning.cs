using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;

namespace Galaxy.Environments.Actors
{
    public class Lightning : BaseActor
    {
    #region Private fields

    protected bool m_flying;
    protected Stopwatch m_flyTimer;

    #endregion

    #region Constructors

    public Lightning(ILevelInfo info)
        : base(info)
    {
      Width = 30;
      Height = 35;
      ActorType = ActorType.SuperEnemy;
    }

    #endregion

    #region Overrides

    public override void Update()
    {
      base.Update();
      h_changePosition();
    }

    #endregion

    #region Overrides

    public override void Load()
    {
        Load(@"Assets\lightning.png");
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
        Position = new Point(Position.X - 1,Position.Y - 2);
    }

    #endregion
    }
}
