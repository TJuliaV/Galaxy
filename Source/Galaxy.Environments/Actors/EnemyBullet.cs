using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;

namespace Galaxy.Environments.Actors
{
    public class EnemyBullet : BaseActor
    {
    #region Constant

    private const int Speed = 3;

    #endregion

    #region Constructors

    public EnemyBullet(ILevelInfo info)
        : base(info)
    {
      Width = 5;
      Height = 5;
      ActorType = ActorType.Enemy;
    }

    #endregion

    #region Overrides

    public override void Load()
    {
      Load(@"Assets\enemybullet.png");
    }

    public override void Update()
    {
        base.Update();
        Position = new Point(Position.X, Position.Y + Speed);
    }

    #endregion
  }
}