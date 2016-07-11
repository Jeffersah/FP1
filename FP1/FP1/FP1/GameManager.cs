using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FP1.Screens;
using FP1.Minigames;
using NCodeRiddian;

namespace FP1
{
    abstract class GameManager
    {
        public static Player[] Players;
        public static int[] Points;
        public static int POINTS_TO_WIN = 10;
        public static GameSelectionMode SelectMode;

        public static void Setup(Player[] Players)
        {
            GameManager.Players = Players;
            Points = new int[]{0,0,0,0};
            GlobalRandom.RandomFrom<Player>(Players).isP1 = true;
        }

        public static void ChangeP1(Player newP1, int points)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i].isP1 = (Players[i] == newP1))
                {
                    Points[i] += points;
                }
            }
        }
    }

    enum GameSelectionMode : byte
    {
        Random,
        Vote,
        WinnerChoses,
        LoserChoses
    }
}
