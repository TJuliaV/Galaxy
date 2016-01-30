﻿using System;
using System.Collections.Generic;
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

    #region Constructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="LevelOne" /> class.
    /// </summary>
    public LevelTwo()
    {
      // Backgrounds
      FileName = @"Assets\LevelTwo.png";

      // Enemies
      for (int i = 0; i < 5; i++)
      {
          var ship = new Enemy1ForLevelTwo(this);
        int positionY = ship.Height + 100;
        int positionX = 150 + i * (ship.Width + 70);

        ship.Position = new Point(positionX, positionY);

        Actors.Add(ship);
      }
      for (int i = 0; i < 7; i++)
      {
          var spaceship = new Enemy2ForLevelTwo(this);
          int positionY = spaceship.Height + 50;
          int positionX = 110 + i * (spaceship.Width + 55);

          spaceship.Position = new Point(positionX, positionY);

          Actors.Add(spaceship);
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

      IEnumerable<BaseActor> killedActors = CollisionChecher.GetAllCollisions(Actors);

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
