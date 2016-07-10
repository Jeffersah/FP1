using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FP1.Screens;
using FP1.Minigames;

namespace FP1
{
    abstract class GameManager
    {
        public static Player[] Players;
        public static int[] Points;
        public static int POINTS_TO_WIN = 10;
        public static void Setup(Player[] Players)
        {
            GameManager.Players = Players;
            Points = new int[]{0,0,0,0};
        }
    }
}
