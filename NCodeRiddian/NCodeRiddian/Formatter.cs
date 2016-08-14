using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace NCodeRiddian
{
    public abstract class Formatter
    {
        public static void Center(ref Rectangle inner, Rectangle frame)
        {
            inner.X = frame.Center.X - inner.Width / 2;
            inner.Y = frame.Center.Y - inner.Height / 2;
        }
        public static Rectangle Center(Rectangle inner, Rectangle frame)
        {
            return new Rectangle(frame.Center.X - inner.Width / 2, frame.Center.Y - inner.Height / 2, inner.Width, inner.Height);
        }
        public static Rectangle GridCell(Rectangle frame, int cellX, int cellY, int index)
        {
            int cellsPerRow = frame.Width / cellX;
            int row = index / cellsPerRow;
            return new Rectangle(frame.X + cellX * (index % cellsPerRow),frame.Y + cellY * row, cellX, cellY);
        }
        public static Rectangle PaddedGridCell(Rectangle frame, int cellX, int cellY, int index)
        {
            return PaddedGridCell(frame, cellX, cellY, index, 1);
        }
        public static Rectangle PaddedGridCell(Rectangle frame, int cellX, int cellY, int index, int minPadding)
        {
            int cellsPerRow = frame.Width / cellX;
            while (frame.Width - (cellsPerRow * cellX) < minPadding)
                cellsPerRow--;

            if (cellsPerRow <= 0)
                return new Rectangle(0, 0, 0, 0);

            int padding = (frame.Width - (cellsPerRow * cellX)) / (cellsPerRow + 1);
            int row = index / cellsPerRow;
            return new Rectangle(frame.X + ((cellX + padding) * (index % cellsPerRow)) + padding, frame.Y + ((cellY + padding) * row) + padding, cellX, cellY);
        }
        public static Rectangle PaddedGridCell_fixcount(Rectangle frame, int cellX, int cellY, int index, int perrow)
        {
            int cellsPerRow = perrow;

            int padding = (frame.Width - (cellsPerRow * cellX)) / (cellsPerRow + 1);
            int row = index / cellsPerRow;
            return new Rectangle(frame.X + ((cellX + padding) * (index % cellsPerRow)) + padding, frame.Y + ((cellY + padding) * row) + padding, cellX, cellY);
        }
    }
}
