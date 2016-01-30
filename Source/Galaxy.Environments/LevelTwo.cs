using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Core.Actors;
using Galaxy.Core.Collision;
using Galaxy.Core.Environment;
using Galaxy.Environments.Actors;

namespace Galaxy.Environments
{
    public class LevelTwo: BaseLevel
    {
        private int m_frameCount;
        private long m_gameTime = 60000;
        private Stopwatch m_gameTimer;
    #region Constructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="LevelOne" /> class.
    /// </summary>
    public LevelTwo()
    {
      // Backgrounds
      FileName = @"Assets\LevelOne.png";
        m_gameTimer = new Stopwatch();
        m_gameTimer.Start();

      // Enemies
      for (int i = 0; i < 4; i++)
      {
          var ship = new Enemy1ForLevelTwo(this);
        int positionY = ship.Height + 30;
        int positionX = 150 + i * (ship.Width + 80);

        ship.Position = new Point(positionX, positionY);

        Actors.Add(ship);
      }

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
        Enemy1ForLevelTwo[] spaceship = Actors.Where(actor => actor is Enemy1ForLevelTwo && actor.IsAlive).Cast<Enemy1ForLevelTwo>().ToArray();
        var time = DateTime.Now.Millisecond;
        if (time % 59 == 0)
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
      return new StartScreen();
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
        
      if (Player.CanDrop)
          Failed = true;
        
      if (m_gameTimer.ElapsedMilliseconds > m_gameTime)
        Success = true;
    }

    #endregion
    }
}
