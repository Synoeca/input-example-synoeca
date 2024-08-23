﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InputExample
{
	public class InputManager
	{
		KeyboardState currentKeyboardState;
		KeyboardState priorKeyboardState;
		MouseState currentMouseState;
		MouseState priorMouseState;
		GamePadState currentGamePadState;
		GamePadState priorGamePadState;

		/// <summary>
		/// The current direction
		/// </summary>
		public Vector2 Direction { get; private set; }

		public MouseState MousePosition { get; private set; }

		/// <summary>
		///  If the warp functionality has been requested
		/// </summary>
		public bool Warp { get; private set; } = false;

		/// <summary>
		/// If the user has requested the game end
		/// </summary>
		public bool Exit { get; private set; } = false;

		public void Update(GameTime gameTime)
		{
			#region Updating Input State

			priorKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

			priorMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			priorGamePadState = currentGamePadState;
			currentGamePadState = GamePad.GetState(0);

			#endregion

			#region Direction Updating

			// Get position from GamePad
			Direction = currentGamePadState.ThumbSticks.Right * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

			MousePosition = currentMouseState;

			// Get direction from Keyboard
			if (currentKeyboardState.IsKeyDown(Keys.Left) ||
				currentKeyboardState.IsKeyDown(Keys.A))
			{
				Direction += new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
			}

			if (currentKeyboardState.IsKeyDown(Keys.Right) ||
				currentKeyboardState.IsKeyDown(Keys.D))
			{
				Direction += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
			}

			if (currentKeyboardState.IsKeyDown(Keys.Up) ||
				currentKeyboardState.IsKeyDown(Keys.W))
			{
				Direction += new Vector2(0, -100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
			}

			if (currentKeyboardState.IsKeyDown(Keys.Down) ||
				currentKeyboardState.IsKeyDown(Keys.S))
			{
				Direction += new Vector2(0, 100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
			}

			#endregion

			#region Warp Input

			Warp = false;

			if (priorKeyboardState.IsKeyUp(Keys.Space) &&
				currentKeyboardState.IsKeyDown(Keys.Space))
			{
				Warp = true;
			}

			if (priorMouseState.LeftButton == ButtonState.Released &&
				currentMouseState.LeftButton == ButtonState.Pressed)
			{
				Warp = true;
			}

			if (priorGamePadState.IsButtonUp(Buttons.A) &&
				currentGamePadState.IsButtonDown(Buttons.A))
			{
				Warp = true;
			}

			#endregion

			#region Exit Input
			if (currentGamePadState.Buttons.Back == ButtonState.Pressed ||
				currentKeyboardState.IsKeyDown(Keys.Escape))
			{
				Exit = true;
			}
			#endregion


		}
	}
}
