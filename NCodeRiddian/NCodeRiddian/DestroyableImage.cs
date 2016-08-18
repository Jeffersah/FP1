using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace NCodeRiddian
{
    public class DestroyableImage
    {
        Texture2D image;
        bool[,] livemap;
        Color[] data;
        Vector2 com;
        int liveCount;

        public int LivePixelCount
        {
            get
            {
                return liveCount;
            }
        }

        public Vector2 CenterOfMass
        {
            get
            {
                return com;
            }
        }

        public DestroyableImage(Image over)
        {
            data = new Color[over.getTexture().Width * over.getTexture().Height];
            over.getTexture().GetData<Color>(data);
            image = new Texture2D(over.getTexture().GraphicsDevice, over.getTexture().Width, over.getTexture().Height);
            image.SetData<Color>(data);
            liveCount = 0;
            livemap = new bool[over.getTexture().Width, over.getTexture().Height];
            CalculateLiveMap();
            CalculateCOM();
        }

        public DestroyableImage(Texture2D over)
        {
            data = new Color[over.Width * over.Height];
            image = over;
            image.GetData<Color>(data);
            liveCount = 0;
            livemap = new bool[over.Width, over.Height];
            CalculateLiveMap();
            CalculateCOM();
        }

        public void CalculateLiveMap()
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (data[GetI(x, y, image.Width)].A != 0)
                    {
                        livemap[x, y] = true;
                    }
                    else
                        livemap[x, y] = false;
                }
            }
        }

        public void CalculateCOM()
        {
            float pxCount = 0;
            int tX = 0;
            int tY = 0;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (livemap[x, y])
                    {
                        pxCount++;
                        tX += x;
                        tY += y;
                    }
                }
            }
            liveCount = (int)pxCount;
            com = new Vector2(tX / pxCount, tY / pxCount);
        }

        /// <summary>
        /// Destroys pixels that are set false in the livemap
        /// </summary>
        protected void KillFromLiveMap()
        {
            bool anyChanged = false;
            for(int x = 0; x < livemap.GetLength(0); x++)
            {
                for (int y = 0; y < livemap.GetLength(1); y++)
                {
                    if(!livemap[x,y])
                    {
                        if(data[GetI(x, y, image.Width)].A != 0)
                        {
                            data[GetI(x, y, image.Width)] = new Color(0,0,0,0);
                            anyChanged = true;
                        }
                    }
                }
            }
            if(anyChanged)
            { 
                image.SetData(data);
                CalculateCOM();
            }
        }

        public int GetX(int i, int width)
        {
            return i % width;
        }
        public int GetY(int i, int width)
        {
            return (int)(i / width);
        }
        public int GetI(int x, int y, int width)
        {
            return x + y * width;
        }

        public Vector2[] RelativeLine(Vector2[] GlobalLine, Vector2 Position, float CurrentRotation)
        {
            return new Vector2[] {  LocationManager.TransformAround(Position, GlobalLine[0], CurrentRotation) - Position,
                                    LocationManager.TransformAround(Position, GlobalLine[1], CurrentRotation) - Position };
        }

        /// <summary>
        /// finds the first "Impact point" casting a line toward this object
        /// </summary>
        /// <returns>A relative point, or null for no intersect</returns>
        public Point? CastTo(Vector2[] RelativeLine)
        {
            foreach(Point p in LocationManager.AllPointsThroughGrid(RelativeLine, 1))
            {
                if(p.X >= 0 && p.X < livemap.GetLength(0) &&
                   p.Y >= 0 && p.Y < livemap.GetLength(1) && livemap[p.X, p.Y])
                {
                    return p;
                }
            }
            return null;
        }
        
        public void DestroyPixelsWhere(Func<Point, bool> condition)
        {
            bool anyBroken = false;
            for(int x = 0; x < livemap.GetLength(0); x++)
            {
                for (int y = 0; y < livemap.GetLength(1); y++)
                {
                    Point p = new Point(x, y);
                    if(livemap[x,y] && condition(p))
                    {
                        // destroy;
                        anyBroken = true;
                        livemap[x, y] = false;
                    }
                }
            }
            if (anyBroken)
                KillFromLiveMap();
        }

        public Image getImage()
        {
            return new Image(image);
        }

        public Texture2D getTexture()
        {
            return image;
        }
    }
}
