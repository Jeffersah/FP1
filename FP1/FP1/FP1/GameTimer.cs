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
        public delegate void TimeTriggerParam(GameTimer timer, object[] param);

        TimeSpan start;
        TimeSpan lastUpdate;
        TimeSpan target;
        bool isRunning = true;
        bool IsRunning { get { return isRunning; } }
        bool isDone = false;
        bool IsDone { get { return isDone; } }
        TimeTrigger onFinish;
        TimeTriggerParam paramfinish;
        object[] param;

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
        public GameTimer(GameTime current, TimeSpan duration, TimeTriggerParam onFinish, object[] param)
        {
            isRunning = true;
            isDone = false;
            start = current.TotalGameTime;
            target = start + duration;
            this.paramfinish += onFinish;
            this.param = param;
        }
        public void Update(GameTime current)
        {
            if(isRunning)
                lastUpdate = current.TotalGameTime;
            if (isRunning && current.TotalGameTime >= target)
            {
                isRunning = false;
                isDone = true;
                if(onFinish != null)
                    onFinish(this);
                if(paramfinish != null)
                    paramfinish(this, param);
            }
        }

        public void Stop()
        {
            isRunning = false;
            isDone = true;
        }

        public TimeSpan RemainingTime(GameTime gt)
        {
            return target - gt.TotalGameTime;
        }
        public TimeSpan RemainingTime()
        {
            return target - lastUpdate;
        }

        public static void AddStaticTimer(GameTime current, TimeSpan duration, TimeTrigger onFinish)
        {
            onFinish += PruneTimer;
            StaticTimers.Add(new GameTimer(current, duration, onFinish));
        }
        public static void AddStaticTimer(GameTime current, TimeSpan duration, TimeTriggerParam onFinish, object[] param)
        {
            onFinish += PruneTimer;
            StaticTimers.Add(new GameTimer(current, duration, onFinish, param));
        }

        static void PruneTimer(GameTimer gt)
        {
            StaticTimers.Remove(gt);
        }
        static void PruneTimer(GameTimer gt, object [] param)
        {
            StaticTimers.Remove(gt);
        }
    }
}
