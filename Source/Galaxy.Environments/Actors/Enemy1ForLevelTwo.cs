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
    class Enemy1ForLevelTwo: BaseActor
    {
        #region Constant

        protected const int MaxSpeed = 1;
        protected const long StartFlyMs = 1000;
        protected const long SleepMs = 3000;

        #endregion

        #region Private fields

        private Stopwatch m_dethTimer;
        private bool m_isAlive;

        protected bool m_flying;
        protected Stopwatch m_flyTimer;


        public override bool IsAlive
        {
            get { return m_isAlive; }
            set
            {
                m_isAlive = value;
                if (!m_isAlive)
                {
                    m_dethTimer = new Stopwatch();
                    m_dethTimer.Start();
                }
            }
        }

        #endregion

        #region Constructors

        public Enemy1ForLevelTwo(ILevelInfo info)
              : base(info)
        {
          Width = 40;
          Height = 40;
          ActorType = ActorType.Enemy;
        }

        #endregion

        #region Overrides

        public EnemyBullet NewEnemyBullet(Enemy1ForLevelTwo spaceship)
        {
            EnemyBullet bullet = new EnemyBullet(Info);
            int positionY = spaceship.Position.Y + 30;
            int positionX = spaceship.Position.X + 15;
            bullet.Position = new Point(positionX, positionY);
            bullet.Load();
            return bullet;
        }

        public override void Update()
        {
          base.Update();

            if (!IsAlive)
            {
                h_checkSleep();
                return;
            }

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

        private void h_checkSleep()
        {
            if (m_dethTimer.ElapsedMilliseconds > SleepMs)
            {
                m_isAlive = true;
            }
        }

        #endregion

        #region Overrides

        public override void Load()
        {
            Load(@"Assets\enemy1ForLevelTwo.png");
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
            Position = new Point(Position.X - 1, (int)(Position.Y + 0.3* Math.Round(Math.Cos(Position.X / 50))));
        }

        #endregion
        }
}
