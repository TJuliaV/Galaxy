#region using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Galaxy.Core.Actors;
using Galaxy.Core.Collision;
using Galaxy.Core.Environment;
using Galaxy.Environments.Actors;

#endregion

namespace Galaxy.Environments
{
  /// <summary>
  ///   The level class for Open Mario.  This will be the first level that the player interacts with.
  /// </summary>
  public class LevelOne : BaseLevel
  {
    private int m_frameCount;

    #region Constructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="LevelOne" /> class.
    /// </summary>
    public LevelOne()
    {
      // Backgrounds
      FileName = @"Assets\LevelOne.png";

      // Enemies
      for (int i = 0; i < 5; i++)
      {
        var ship = new Ship(this);
        int positionY = ship.Height + 100;
        int positionX = 150 + i * (ship.Width + 70);

        ship.Position = new Point(positionX, positionY);

          if (i%2 == 0)
          {
              ship.m_styleoffly = StyleOfFly.Sin;
          }
          else
          {
              ship.m_styleoffly = StyleOfFly.Cos;
          }

        Actors.Add(ship);
      }
      //for (int i = 0; i < 7; i++)
      //{
      //    var spaceship = new Spaceship(this);
      //    int positionY = spaceship.Height + 50;
      //    int positionX = 110 + i * (spaceship.Width + 55);

      //    spaceship.Position = new Point(positionX, positionY);

      //    spaceship.m_styleoffly = StyleOfFly.Vector;

      //    Actors.Add(spaceship);
      //}

      //SuperEnemy
      var lightning = new Lightning(this);
      int posY = lightning.Height + 10;
      int posX = lightning.Width + 50;

      lightning.Position = new Point(posX, posY);

      Actors.Add(lightning);

      // Player
      Player = new PlayerShip(this);
      int playerPositionX = Size.Width / 2 - Player.Width / 2;
      int playerPositionY = Size.Height - Player.Height - 50;
      Player.Position = new Point(playerPositionX, playerPositionY);
      Actors.Add(Player);
    }

    #endregion

    #region Overrides

      private void WorkWithEnemyBullet()
      {
          //пули создаются
          Spaceship[] spaceship = Actors.Where(actor => actor is Spaceship).Cast<Spaceship>().ToArray();
          var time = DateTime.Now.Millisecond;
          if (time%33 == 0)
          {
              foreach (var ship in spaceship)
              {
                  Actors.Add(ship.NewEnemyBullet(ship));
              }
          }
          
          //пули, долетевшие до низа, уничтожаются
          EnemyBullet[] bullets = Actors.Where(actor => actor is EnemyBullet).Cast<EnemyBullet>().ToArray();
          foreach (var bul in bullets)
          {
              if (bul.Position.Y >= BaseLevel.DefaultHeight)
              {
                  Actors.Remove(bul);
              }
          }
      }

    private void h_dispatchKey()
    {
      if (!IsPressed(VirtualKeyStates.Space)) return;

      if(m_frameCount % 10 != 0) return;

      Bullet bullet = new Bullet(this)
      {
        Position = Player.Position
      };

      bullet.Load();
      Actors.Add(bullet);
    }

    public override BaseLevel NextLevel()
    {
      //возвращает на мой уровень
      return new LevelTwo();
    }

    public override void Update()
    {
      m_frameCount++;
      h_dispatchKey();

        base.Update();
        WorkWithEnemyBullet();

      List<BaseActor> lst = Actors.Where(o => o.IsAlive).ToList();

      IEnumerable<BaseActor> killedActors = CollisionChecher.GetAllCollisions(lst);

      foreach (BaseActor killedActor in killedActors)
      {
        if (killedActor.IsAlive)
          killedActor.IsAlive = false;
      }

      List<BaseActor> toRemove = Actors.Where(actor => actor.CanDrop).ToList();
      BaseActor[] actors = new BaseActor[toRemove.Count()];
      toRemove.CopyTo(actors);

      foreach (BaseActor actor in actors.Where(actor => actor.CanDrop))
      {
        Actors.Remove(actor);
      }

      if (Player.CanDrop)
        Failed = true;

      //has no enemy
      if (Actors.All(actor => actor.ActorType != ActorType.Enemy))
        Success = true;
    }

    #endregion
  }
}
