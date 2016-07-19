using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using NCodeRiddian;
using DataLoader;

namespace FP1.Minigames
{
    class TugLifeMain : Minigame
    {

        Rectangle GAMESPACE = new Rectangle(200, 350, 1200, 500);

        Image ropeSprite;
        Image muscleManSprite;
        Image aButtonSprite;
        Image bButtonSprite;
        Image xButtonSprite;
        Image yButtonSprite;
        Image beachBackground;
        SpriteFont myFont;

        Random rando = new Random();

        Player[] players;
        Rectangle ropePosition;
        Rectangle winBox;   // unplug ya controller

        List<Tugger> team;
        Tugger p1;
        const int P1_STRENGTH = 15;
        const int TEAM_STRENGTH = -50;
        const int ACTIVATE_CHANCE = 100;

        //const int SEASY_HIT_CHANCE = 

        List<ControllerButton> buttons;
        ControllerButton currentButton;
        Dictionary<ControllerButton, Image> buttonSprites;

        bool canActivate;
        bool isStunned;
        int frameCount;

        public TugLifeMain() : base("Tug Life", "Tug 'Em!"){ }

        public override void Load(ContentManager cm)
        {

            ropeSprite = new Image("Minigames\\TugLife\\rope");
            muscleManSprite = new Image("Minigames\\TugLife\\muscleMan");
            aButtonSprite = new Image("ControllerImages\\A");
            bButtonSprite = new Image("ControllerImages\\B");
            xButtonSprite = new Image("ControllerImages\\X");
            yButtonSprite = new Image("ControllerImages\\Y");
            beachBackground = new Image("Minigames\\TugLife\\beachBackground");

            myFont = TextureManager.getFont("Minigames\\TugLife\\tugFont");

        }

        public override void Start(Player[] InGame)
        {

            players = InGame;
            ropePosition = new Rectangle(((GAMESPACE.Width - ropeSprite.getTexture().Width) / 2) + GAMESPACE.Left, (GAMESPACE.Height / 2) + GAMESPACE.Top, ropeSprite.getTexture().Width, ropeSprite.getTexture().Height);
            canActivate = true;
            isStunned = false;
            frameCount = 0;

            winBox = new Rectangle(((ropePosition.Width / 2) + ropePosition.Left) - 100, ropePosition.Top, 200, ropePosition.Height);

            team = new List<Tugger>();
            for (int x = 1; x < players.Length; x++)
            {
                team.Add(new Tugger(players[x],
                    ropePosition.Left + (50 * (x-2)),
                    ropePosition.Top - (muscleManSprite.getTexture().Height / 2)
                    ));
            }

            p1 = new Tugger(players[0], (GAMESPACE.Right - 50) - muscleManSprite.getTexture().Width, ropePosition.Top - (muscleManSprite.getTexture().Height / 2));

            buttons = new List<ControllerButton>();
            buttons.Add(ControllerButton.A);
            buttons.Add(ControllerButton.X);
            buttons.Add(ControllerButton.Y);
            buttons.Add(ControllerButton.B);
            currentButton = ControllerButton.A;

            buttonSprites = new Dictionary<ControllerButton, Image>();
            buttonSprites.Add(ControllerButton.A, aButtonSprite);
            buttonSprites.Add(ControllerButton.B, bButtonSprite);
            buttonSprites.Add(ControllerButton.X, xButtonSprite);
            buttonSprites.Add(ControllerButton.Y, yButtonSprite);

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gt, Screens.MinigameScreen parentScreen)
        {

            if (frameCount >= 120)
                isStunned = false;

            // ACTIVATING TEAMMATES
            foreach (Tugger teammate in team)
            {
                if (canActivate)
                {
                    if (rando.Next(1, ACTIVATE_CHANCE) == (ACTIVATE_CHANCE-1))
                    {
                        teammate.activate();
                        canActivate = false;

                        currentButton = buttons.ElementAt(rando.Next(0, buttons.Count));
                    }
                }
            }

            /*
            // TEST OTHER PLAYERS
            bool canPull = false;
            Tugger activator = null;
            foreach (Tugger teammate in team)
            {
                if (teammate.isActive)
                {
                    canPull = true;
                    activator = teammate;
                }
            }
            if (canPull)
            {
                for (int x = 0; x < buttons.Count; x++)
                {
                    if (players[0].GamePad.IsButtonPressed(currentButton))
                    {
                        bigTug(activator);
                        canActivate = true;
                    }
                }
            }
            */ 
            
            // OTHER PLAYERS
            if (!isStunned)
            {
                for (int x = 1; x < players.Length; x++)
                {
                    for (int y = 0; y < buttons.Count; y++)
                    {
                        if (players[x].GamePad.IsButtonPressed(buttons.ElementAt(y)))
                        {
                            if (buttons.ElementAt(y).Equals(currentButton))
                            {
                                if (team.ElementAt(x - 1).isActive)
                                {
                                    bigTug(team.ElementAt(x - 1));
                                }
                                else
                                {
                                    isStunned = true;
                                    frameCount = 0;
                                }
                            }
                            else
                            {
                                isStunned = true;
                                frameCount = 0;
                            }
                        }
                    }
                }
            }

            // PLAYER ONE
            if (players[0].GamePad.IsButtonPressed(ControllerButton.A))
            {
                ropePosition.Offset(P1_STRENGTH, 0);

                foreach (Tugger teammate in team)
                {
                    teammate.move(P1_STRENGTH);
                }
                p1.move(P1_STRENGTH);

            }

            frameCount++;

        }

        public void bigTug(Tugger t)
        {
            ropePosition.Offset(TEAM_STRENGTH, 0);
            foreach (Tugger teammate in team)
            {
                teammate.move(TEAM_STRENGTH);
            }
            p1.move(TEAM_STRENGTH);
            t.deactivate();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {

            // Background
            Camera.draw(sb, beachBackground, new Rectangle(0, 0, beachBackground.getTexture().Width, beachBackground.getTexture().Height));

            Camera.draw(sb, ropeSprite, ropePosition);

            for (int x = 0; x < team.Count; x++)
            {
                Camera.draw(sb, muscleManSprite, team.ElementAt(x).getSrc(), team.ElementAt(x).getColor());
            }

            Camera.draw(sb, muscleManSprite, p1.getSrc(), p1.getColor(), null, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 1);

            Camera.drawGeneric(sb, winBox, Color.Red);

            foreach (Tugger teammate in team)
            {
                if (teammate.isActive)
                {
                    Camera.draw(sb, buttonSprites[currentButton],
                        new Rectangle
                            (teammate.getSrc().Left,
                            teammate.getSrc().Top - buttonSprites[currentButton].getTexture().Height,
                            buttonSprites[currentButton].getTexture().Width,
                            buttonSprites[currentButton].getTexture().Height));
                }

            }

        }

        public override void RunAI(Player p, Difficulty difficulty)
        {

            int decision;
            bool active = false;
            foreach (Tugger teammate in team)
            {
                if (teammate.getPlayer().Equals(p))
                    active = teammate.isActive;
            }

            switch (difficulty)
            {

                case Difficulty.SEasy :

                    if (active)
                    {

                        decision = rando.Next(1, 101);

                        if (decision <= 15)
                        {
                            p.GamePad.GetSimState().SetButtonDown(currentButton);
                        }
                        else if (decision <= 50)
                        {
                            // Don't hit
                        }
                        else
                        {

                            ControllerButton choice = buttons.ElementAt(rando.Next(0, buttons.Count));

                            while (choice == currentButton)
                            {
                                choice = buttons.ElementAt(rando.Next(0, buttons.Count));
                            }

                            p.GamePad.GetSimState().SetButtonDown(choice);

                        }

                    }
                    else
                    {
                        if (rando.Next(1, 1001) <= 50)
                        {

                            p.GamePad.GetSimState().SetButtonDown(currentButton);

                        }
                    }

                    break;

                case Difficulty.Easy :

                    if (active)
                    {

                        decision = rando.Next(1, 101);

                        if (decision <= 25)
                        {
                            p.GamePad.GetSimState().SetButtonDown(currentButton);
                        }
                        else if (decision <= 60)
                        {
                            // Don't hit
                        }
                        else
                        {

                            ControllerButton choice = buttons.ElementAt(rando.Next(0, buttons.Count));

                            while (choice == currentButton)
                            {
                                choice = buttons.ElementAt(rando.Next(0, buttons.Count));
                            }

                            p.GamePad.GetSimState().SetButtonDown(choice);

                        }

                    }
                    else
                    {
                        if (rando.Next(1, 1001) <= 30)
                        {

                            p.GamePad.GetSimState().SetButtonDown(currentButton);

                        }
                    }

                    break;

                case Difficulty.Medium :

                    if (active)
                    {

                        decision = rando.Next(1, 101);

                        if (decision <= 40)
                        {
                            p.GamePad.GetSimState().SetButtonDown(currentButton);
                        }
                        else if (decision <= 70)
                        {
                            // Don't hit
                        }
                        else
                        {

                            ControllerButton choice = buttons.ElementAt(rando.Next(0, buttons.Count));

                            while (choice == currentButton)
                            {
                                choice = buttons.ElementAt(rando.Next(0, buttons.Count));
                            }

                            p.GamePad.GetSimState().SetButtonDown(choice);

                        }

                    }
                    else
                    {
                        if (rando.Next(1, 1001) <= 10)
                        {

                            p.GamePad.GetSimState().SetButtonDown(currentButton);

                        }
                    }

                    break;

                case Difficulty.Hard :

                    if (active)
                    {

                        decision = rando.Next(1, 101);

                        if (decision <= 50)
                        {
                            p.GamePad.GetSimState().SetButtonDown(currentButton);
                        }
                        else if (decision <= 83)
                        {
                            // Don't hit
                        }
                        else
                        {

                            ControllerButton choice = buttons.ElementAt(rando.Next(0, buttons.Count));

                            while (choice == currentButton)
                            {
                                choice = buttons.ElementAt(rando.Next(0, buttons.Count));
                            }

                            p.GamePad.GetSimState().SetButtonDown(choice);

                        }

                    }

                    break;

                case Difficulty.SHard :

                    if (active)
                    {

                        decision = rando.Next(1, 101);

                        if (decision <= 70)
                        {
                            p.GamePad.GetSimState().SetButtonDown(currentButton);
                        }
                        else if (decision <= 90)
                        {
                            // Don't hit
                        }
                        else
                        {

                            ControllerButton choice = buttons.ElementAt(rando.Next(0, buttons.Count));

                            while (choice == currentButton)
                            {
                                choice = buttons.ElementAt(rando.Next(0, buttons.Count));
                            }

                            p.GamePad.GetSimState().SetButtonDown(choice);

                        }

                    }

                    break;

                default :
                    return;

            }

        }

    }
}
