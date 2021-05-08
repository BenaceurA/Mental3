using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
namespace Mental3
{
    public class KeyboardManager
    {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public static KeyboardState updateState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            return currentKeyState;
        }

        public static bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool TriggeredDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        public static bool TriggeredUp(Keys key)
        {
            return previousKeyState.IsKeyDown(key) && !currentKeyState.IsKeyDown(key);
        }
    }
}
