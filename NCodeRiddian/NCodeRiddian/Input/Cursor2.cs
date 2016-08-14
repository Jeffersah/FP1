using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NCodeRiddian
{
    public abstract class Cursor2
    {
        private static Toggleable<MouseState> State;

        static Cursor2()
        {
            State = new Toggleable<MouseState>(Mouse.GetState(), Mouse.GetState());
        }

        /// <summary>
        /// Get the current MouseState
        /// </summary>
        public static MouseState CurrentState
        {
            get
            {
                return State.Current;
            }
        }

        /// <summary>
        /// Get the MouseState from the previous frame
        /// </summary>
        public static MouseState PreviousState
        {
            get
            {
                return State.Other;
            }
        }

        /// <summary>
        /// Should be called every update. Gets the new mouse state and sets the old mouse state
        /// </summary>
        public static void Update()
        {
            State.Toggle();
            State.Current = Mouse.GetState();
        }

        /// <summary>
        /// Gets the current XY screen coordinates of the mouse
        /// </summary>
        public static Point ScreenPosition
        {
            get
            {
                return new Point(CurrentState.X, CurrentState.Y);
            }
        }

        /// <summary>
        /// Gets the current XY coordinates of the mouse using the current Camera as a reference frame
        /// </summary>
        public static Point WorldPosition
        {
            get
            {
                return new Point(Camera.getlocation().X + ((int)Math.Round(((float)CurrentState.X - (float)Camera.getOrigin().X) / Camera.getZoom())), Camera.getlocation().Y + ((int)Math.Round(((float)CurrentState.Y - (float)Camera.getOrigin().Y) / Camera.getZoom())));
            }
        }

        /// <summary>
        /// Gets the current XY screen coordinates of the mouse from the previous frame
        /// </summary>
        public static Point PreviousScreenPosition
        {
            get
            {
                return new Point(CurrentState.X, CurrentState.Y);
            }
        }

        /// <summary>
        /// Gets the current XY coordinates of the mouse using the current Camera as a reference frame from the previous frame
        /// </summary>
        public static Point PreviousWorldPosition
        {
            get
            {
                return new Point(Camera.getlocation().X + ((int)Math.Round(((float)PreviousState.X - (float)Camera.getOrigin().X) / Camera.getZoom())), Camera.getlocation().Y + ((int)Math.Round(((float)PreviousState.Y - (float)Camera.getOrigin().Y) / Camera.getZoom())));
            }
        }

        /// <summary>
        /// Gets the current XY screen coordinates of the mouse
        /// </summary>
        public static Vector2 ScreenVector
        {
            get
            {
                return new Vector2(CurrentState.X, CurrentState.Y);
            }
        }

        /// <summary>
        /// Gets the current XY coordinates of the mouse using the current Camera as a reference frame
        /// </summary>
        public static Vector2 WorldVector
        {
            get
            {
                return new Vector2(Camera.getlocation().X + (((float)CurrentState.X - (float)Camera.getOrigin().X) / Camera.getZoom()), Camera.getlocation().Y + (((float)CurrentState.Y - (float)Camera.getOrigin().Y) / Camera.getZoom()));
            }
        }

        /// <summary>
        /// Gets the current XY screen coordinates of the mouse from the previous frame
        /// </summary>
        public static Vector2 PreviousScreenVector
        {
            get
            {
                return new Vector2(PreviousState.X, PreviousState.Y);
            }
        }

        /// <summary>
        /// Gets the current XY coordinates of the mouse using the current Camera as a reference frame from the previous frame
        /// </summary>
        public static Vector2 PreviousWorldVector
        {
            get
            {
                return new Vector2(Camera.getlocation().X + (((float)PreviousState.X - (float)Camera.getOrigin().X) / Camera.getZoom()), Camera.getlocation().Y + (((float)PreviousState.Y - (float)Camera.getOrigin().Y) / Camera.getZoom()));
            }
        }

        /// <summary>
        /// Gets the change in mouse position that occurred between last frame and this frame relative to the screen
        /// </summary>
        public static Point DeltaScreenPosition
        {
            get
            {
                return new Point(ScreenPosition.X - PreviousScreenPosition.X, ScreenPosition.Y - PreviousScreenPosition.Y);
            }
        }

        /// <summary>
        /// Gets the change in mouse position that occurred between last frame and this frame relative to the screen
        /// </summary>
        public static Vector2 DeltaScreenVector
        {
            get
            {
                return ScreenVector - PreviousScreenVector;
            }
        }

        /// <summary>
        /// Gets the change in mouse position that occurred between last frame and this frame relative to the Camera
        /// </summary>
        public static Point DeltaWorldPosition
        {
            get
            {
                return new Point(WorldPosition.X - PreviousWorldPosition.X, WorldPosition.Y - PreviousWorldPosition.Y);
            }
        }

        /// <summary>
        /// Gets the change in mouse position that occurred between last frame and this frame relative to the Camera
        /// </summary>
        public static Vector2 DeltaWorldVector
        {
            get
            {
                return WorldVector - PreviousWorldVector;
            }
        }

        /// <summary>
        /// True if the left button is down
        /// </summary>
        public static bool LeftDown
        {
            get
            {
                return CurrentState.LeftButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// True on the first frame the left button is pressed
        /// </summary>
        public static bool LeftPressed
        {
            get
            {
                return CurrentState.LeftButton == ButtonState.Pressed && PreviousState.LeftButton == ButtonState.Released;
            }
        }

        /// <summary>
        /// True if left button is up
        /// </summary>
        public static bool LeftUp
        {
            get
            {
                return CurrentState.LeftButton == ButtonState.Released;
            }
        }

        /// <summary>
        /// True on the first frame the left button is released
        /// </summary>
        public static bool LeftReleased
        {
            get
            {
                return CurrentState.LeftButton == ButtonState.Released && PreviousState.LeftButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// True if the right button is down
        /// </summary>
        public static bool RightDown
        {
            get
            {
                return CurrentState.RightButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// True on the first frame the right button is pressed
        /// </summary>
        public static bool RightPressed
        {
            get
            {
                return CurrentState.RightButton == ButtonState.Pressed && PreviousState.RightButton == ButtonState.Released;
            }
        }

        /// <summary>
        /// True if the right button is up
        /// </summary>
        public static bool RightUp
        {
            get
            {
                return CurrentState.RightButton == ButtonState.Released;
            }
        }

        /// <summary>
        /// True on the first frame the right button is up
        /// </summary>
        public static bool RightReleased
        {
            get
            {
                return CurrentState.RightButton == ButtonState.Released && PreviousState.RightButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// True if the middle button is down
        /// </summary>
        public static bool MiddleDown
        {
            get
            {
                return CurrentState.MiddleButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// True on the first frame the middle button is pressed
        /// </summary>
        public static bool MiddlePressed
        {
            get
            {
                return CurrentState.MiddleButton == ButtonState.Pressed && PreviousState.MiddleButton == ButtonState.Released;
            }
        }

        /// <summary>
        /// True if the middle button is up
        /// </summary>
        public static bool MiddleUp
        {
            get
            {
                return CurrentState.MiddleButton == ButtonState.Released;
            }
        }

        /// <summary>
        /// True on the first frame the middle button is up
        /// </summary>
        public static bool MiddleReleased
        {
            get
            {
                return CurrentState.MiddleButton == ButtonState.Released && PreviousState.MiddleButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Returns the cumulitive scroll of the mouse wheel since the start of the game
        /// </summary>
        public static int Scroll
        {
            get
            {
                return CurrentState.ScrollWheelValue;
            }
        }

        /// <summary>
        /// Returns the change in scroll since the last frame.
        /// </summary>
        public static int DeltaScroll
        {
            get
            {
                return CurrentState.ScrollWheelValue - PreviousState.ScrollWheelValue;
            }
        }

        /// <summary>
        /// Checks if the mouse is in an area using the mouses world position
        /// </summary>
        /// <param name="r">Area to check</param>
        /// <returns>TRUE if the mouse is in this area (inclusive), FALSE otherwise</returns>
        public static bool MouseIn(Rectangle r)
        {
            Point WP = WorldPosition;
            return WP.X >= r.X && WP.X <= r.X + r.Width && WP.Y >= r.Y && WP.Y < r.Y + r.Height;
        }

        /// <summary>
        /// Checks if the mouse entered an area on this update using the mouses world position
        /// </summary>
        /// <param name="r">Area to check</param>
        /// <returns>TRUE if the mouse moved into this area (inclusive) this frame, FALSE otherwise</returns>
        public static bool MouseEntered(Rectangle r)
        {
            Point WP = WorldPosition;
            Point PP = PreviousWorldPosition;
            return (WP.X >= r.X && WP.X <= r.X + r.Width && WP.Y >= r.Y && WP.Y < r.Y + r.Height) && (PP.X < r.X || PP.X > r.X + r.Width || PP.Y < r.Y || PP.Y > r.Y + r.Height);
        }

        /// <summary>
        /// Checks if the mouse exited an area on this update using the mouses world position
        /// </summary>
        /// <param name="r">Area to check</param>
        /// <returns>TRUE if the mouse moved out of this area (inclusive) this frame, FALSE otherwise</returns>
        public static bool MouseExited(Rectangle r)
        {
            Point PP = WorldPosition;
            Point WP = PreviousWorldPosition;
            return (WP.X >= r.X && WP.X <= r.X + r.Width && WP.Y >= r.Y && WP.Y < r.Y + r.Height) && (PP.X < r.X || PP.X > r.X + r.Width || PP.Y < r.Y || PP.Y > r.Y + r.Height);
        }
    }
}
