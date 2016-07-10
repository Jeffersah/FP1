using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NCodeRiddian;
using FP1.Screens;

namespace FP1
{
    /// <summary>
    /// Used on the main menu for setup purposes
    /// </summary>
    class TemporaryPlayer : Player
    {
        public int COL_NAME = 8;

        public static SpriteFont NameFont;
        public static void LOAD()
        {
            NameFont = TextureManager.getFont("namefont");
        }

        public bool hasPickedName = true;
        public bool isInNameSelect = false;
        public bool isSpellingName = false;

        int currentHighlight = 0;

        public TemporaryPlayer(string Name, Difficulty diff, PlayerIndex PlayerIdx)
            : base(Name, diff, PlayerIdx)
        {
            hasPickedName = true;
            if (Name == "")
            {
                hasPickedName = false;
                isInNameSelect = true;
            }
        }

        public void MenuUpdate(MainMenu menu)
        {
            GamePad.Update();
            if (!hasPickedName)
            {
                if (isInNameSelect)
                {
                    if (GamePad.DeltaLeftStick().Y <= -.5f && GamePad.BasePrevious().LeftStick.Y >= -.1f)
                    {
                        currentHighlight++;
                        if (currentHighlight > Settings.PrvNames.Count)
                        {
                            currentHighlight--;
                        }
                    }
                    else if (GamePad.DeltaLeftStick().Y >= .5f && GamePad.BasePrevious().LeftStick.Y <= .1f)
                    {
                        currentHighlight--;
                        if (currentHighlight < 0)
                            currentHighlight = 0;
                    }
                    if (GamePad.IsButtonDown(ControllerButton.A))
                    {
                        if (currentHighlight < Settings.PrvNames.Count)
                        {
                            isSpellingName = false;
                            isInNameSelect = false;
                            hasPickedName = true;
                            base.Name = Settings.PrvNames[currentHighlight];
                        }
                        else
                        {
                            isSpellingName = true;
                            isInNameSelect = false;
                            currentHighlight = 0;
                        }
                    }
                }
                else if (isSpellingName)
                {
                    if (GamePad.DeltaLeftStick().X >= .5f && GamePad.BasePrevious().LeftStick.X >= 0)
                    {
                        currentHighlight++;
                    }
                    if (GamePad.DeltaLeftStick().X <= -.5f && GamePad.BasePrevious().LeftStick.X <= 0)
                    {
                        currentHighlight += 25;
                    }
                    if (GamePad.DeltaLeftStick().Y >= .5f && GamePad.BasePrevious().LeftStick.Y >= 0)
                    {
                        currentHighlight += 26 - COL_NAME;
                    }
                    if (GamePad.DeltaLeftStick().Y <= -.5f && GamePad.BasePrevious().LeftStick.Y <= 0)
                    {
                        currentHighlight += COL_NAME;
                    }
                    if (GamePad.IsButtonPressed(ControllerButton.A))
                    {
                        if (Name.Length < Settings.NAMELEN)
                            Name += (char)('A' + currentHighlight);
                    }
                    if (GamePad.IsButtonPressed(ControllerButton.B))
                    {
                        if (Name.Length != 0)
                            Name = Name.Substring(0, Name.Length - 1);
                    }
                    if (GamePad.IsButtonPressed(ControllerButton.Start))
                    {
                        if (Name.Length != 0)
                        {
                            isSpellingName = false;
                            hasPickedName = true;
                            Settings.PrvNames.Add(Name);
                        }
                    }
                    currentHighlight %= 26;
                }
            }
        }

        public void MenuDraw(SpriteBatch sb, Rectangle area, MainMenu menu)
        {
            Camera.drawGeneric(sb, area, Color.Gray);
            foreach (Vector2[] line in LocationManager.RectangleEdges(area))
            {
                Camera.drawLineGeneric(line[0], line[1], sb, PlayerColor);
            }
            if (hasPickedName)
            {
                Camera.drawString(sb, NameFont, Name, new Vector2(area.X + (area.Width / 2) - (NameFont.MeasureString(Name).X / 2), area.Y + 5), Color.Black, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
            else if (isInNameSelect)
            {
                for (int i = 0; i < 10; i++)
                {
                    int yh = (int)NameFont.MeasureString("ABC").Y;

                    string name = "";

                    if (currentHighlight + i < Settings.PrvNames.Count)
                    {
                        name = Settings.PrvNames[currentHighlight + i];
                    }
                    else if (currentHighlight + i == Settings.PrvNames.Count)
                    {
                        name = "--NEW--";
                    }

                    if (name != "")
                    {
                        Camera.drawString(sb, NameFont, name, new Vector2(area.X + (area.Width / 2) - (NameFont.MeasureString(name).X / 2), area.Y + 5 + (i * yh)), i == 0 ? PlayerColor : Color.Black, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                }
            }
            else
            {
                Camera.drawString(sb, NameFont, fillTo5(Name), new Vector2(area.X + (area.Width / 2) - (NameFont.MeasureString(fillTo5(Name)).X / 2), area.Y + 5), Color.Black, 0, Vector2.Zero, SpriteEffects.None, 0);
                int XSpacing = area.X / (COL_NAME);
                int loffset = (int)(NameFont.MeasureString("W").X / 2);
                int x = 0;
                int y = 0;
                int i = 0;
                for (char c = 'A'; c <= 'Z'; c++)
                {
                    if (i == currentHighlight)
                    {
                        Camera.drawGeneric(sb, new Rectangle(
                            (XSpacing * 2) + (XSpacing * 4 * x)+ area.X, 
                            (y * 50) + 50 + area.Y,
                            loffset * 3,
                            loffset * 3
                            ), PlayerColor);
                    }
                    Camera.drawString(sb, NameFont, c + "", new Vector2((XSpacing*2) + (XSpacing * 4 * x) + area.X,(y * 50) + 50 + area.Y), Color.Black, 0, Vector2.Zero, SpriteEffects.None, 0);
                    x++;
                    if (x == COL_NAME)
                    {
                        x = 0; 
                        y++;
                    }
                    i++;
                }
            }
        }

        string fillTo5(string s)
        {
            string tmp = s;
            while (tmp.Length < 5)
                tmp += "_";
            return tmp;
        }

        public Player GetFinalizedPlayer()
        {
            // TODO
            return null;
        }
    }
}
