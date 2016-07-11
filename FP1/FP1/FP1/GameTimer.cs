using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FP1
{
    public class GameTimer
    {
        public static List<GameTimer> StaticTimers;

        static GameTimer()
        {
            StaticTimers = new List<GameTimer>();
        }

        public delegate void TimeTrigger(GameTimer timer);

        TimeSpan start;
        TimeSpan target;
        bool isRunning = true;
        bool IsRunning { get { return isRunning; } }
        bool isDone = false;
        bool IsDone { get { return isDone; } }
        TimeTrigger onFinish;

        public GameTimer(GameTime current, TimeSpan duration)
        {
            isRunning = true;
            isDone = false;
            start = current.TotalGameTime;
            target = start + duration;
        }
        public GameTimer(GameTime current, TimeSpan duration, TimeTrigger onFinish)
        {
            isRunning = true;
            isDone = false;
            start = current.TotalGameTime;
            target = start + duration;
            this.onFinish += onFinish;
        }
        public void Update(GameTime current)
        {
            if (isRunning && current.TotalGameTime >= target)
            {
                isRunning = false;
                isDone = true;
                onFinish(this);
            }
        }

        public static void AddStaticTimer(GameTime current, TimeSpan duration, TimeTrigger onFinish)
        {
            onFinish += PruneTimer;
            StaticTimers.Add(new GameTimer(current, duration, onFinish));
        }

        static void PruneTimer(GameTimer gt)
        {
            StaticTimers.Remove(gt);
        }
    }
}
