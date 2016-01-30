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
   public class Spaceship: Ship
  {
    #region Constructors

    public Spaceship(ILevelInfo info)
        : base(info)
    {
      Width = 40;
      Height = 40;
      ActorType = ActorType.Enemy;
    }

    #endregion

       public EnemyBullet NewEnemyBullet(Spaceship spaceship)
       {
           EnemyBullet bullet = new EnemyBullet(Info);
           int positionY = spaceship.Position.Y + 30;
           int positionX = spaceship.Position.X + 15;
           bullet.Position = new Point(positionX, positionY);
           bullet.Load();
           return bullet;
       }

    public override void Load()
    {
        base.Load();
        Load(@"Assets\spaceship.png");
    }
  }
}
