using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FP1.Minigames;

namespace FP1.Screens
{
    /// <summary>
    /// Screen when playing a minigame
    /// </summary>
    class MinigameScreen : Screen
    {
        public static Minigame[] AllMinigames = { 
                                                    new Minigames.test.TestMinigame(),  // 0
                                                    new Minigames.LunchBreakMain(),     // 1
                                                    new Minigames.PipeDown(),   // 2
                                                    new Minigames.TugLifeMain() // 3
                                                };

        new public static void Load(ContentManager cm)
        {
            foreach (Minigame m in AllMinigames)
                m.Load(cm);
        }

        public static Minigame GetRandomGame()
        {
            return NCodeRiddian.GlobalRandom.RandomFrom<Minigame>(AllMinigames);
        }

        Minigame currentGame;
        Player[] players;

        public MinigameScreen(Minigame game, Player[] players)
        {
            currentGame = game;
            this.players = players;
        }

        public override void Start()
        {
            currentGame.Start(players);
        }

        public override void Update(GameTime time, GameScreenManager Manager)
        {
            foreach (Player p in players)
            {
                p.GamePad.Update();
                if (p.IsComputer)
                {
                    currentGame.RunAI(p, p.ComputerLevel);
                }
            }
            currentGame.Update(time, this);
        }

        public override void Draw(GameTime time, SpriteBatch sb)
        {
            currentGame.Draw(time, sb);
        }
    }
}
