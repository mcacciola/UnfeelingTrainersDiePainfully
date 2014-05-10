using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UTDP;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Describes the state of the button.
/// </summary>
public enum ButtonStatus
{
    Normal,
    MouseOver,
    Pressed,
    Selected,
}

/// <summary>
/// Stores the appearance and functionality of a button.
/// </summary>
public class Button : Sprite
{
    // Store the MouseState of the last frame.
    private MouseState previousState;

    // The the different state textures.
    private Texture2D hoverTexture;
    private Texture2D pressedTexture;

    // A rectangle that covers the button.
    private Rectangle bounds;

    // Store the current state of the button.
    private ButtonStatus status = ButtonStatus.Normal;

    private Tower tower;

    private SpriteFont font;

    // Gets fired when the button is pressed.
    public event EventHandler Clicked;

    /// <summary>
    /// Constructs a new button.
    /// </summary>
    /// <param name="texture">The normal texture for the button.</param>
    /// <param name="hoverTexture">The texture drawn when the mouse is over the button.</param>
    /// <param name="pressedTexture">The texture drawn when the button has been pressed.</param>
    /// <param name="position">The position where the button will be drawn.</param>
    public Button(Texture2D texture, Texture2D hoverTexture, Texture2D pressedTexture, Vector2 position, Tower tower, SpriteFont font)
        : base(texture, position)
    {
        this.hoverTexture = hoverTexture;
        this.pressedTexture = pressedTexture;

        this.bounds = new Rectangle((int)position.X, (int)position.Y,
            texture.Width, texture.Height);

        this.tower = tower;
        this.font = font;
    }

    /// <summary>
    /// Updates the buttons status.
    /// </summary>
    /// <param name="gameTime">The current game time.</param>
    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();

        int mouseX = mouseState.X;
        int mouseY = mouseState.Y;

        bool isMouseOver = bounds.Contains(mouseX, mouseY);
        if (status != ButtonStatus.Selected)
        {
            if (isMouseOver && status != ButtonStatus.Pressed)
            {
                status = ButtonStatus.MouseOver;
            }
            else if (isMouseOver == false && status != ButtonStatus.Pressed)
            {
                status = ButtonStatus.Normal;
            }

            // Check if the player holds down the button.
            if (mouseState.LeftButton == ButtonState.Pressed &&
             previousState.LeftButton == ButtonState.Released)
            {
                if (isMouseOver == true)
                {
                    // Update the button state.
                    status = ButtonStatus.Pressed;
                }
            }

            // Check if the player releases the button.
            if (mouseState.LeftButton == ButtonState.Released &&
             previousState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    // update the button state.
                    status = ButtonStatus.MouseOver;
                    if (Clicked != null)
                    {
                        // Fire the clicked event.
                        Clicked(this, EventArgs.Empty);
                    }
                }

                else if (status == ButtonStatus.Pressed)
                {
                    status = ButtonStatus.Normal;
                }
            }

            previousState = mouseState;
        }
    }

    public void SetNormal()
    {
        status = ButtonStatus.Normal;
    }

    public void SetSelected()
    {
        status = ButtonStatus.Selected;
    }

    //Draws the button based on its status
    public override void Draw(SpriteBatch spriteBatch)
    {
        switch (status)
        {
            case ButtonStatus.Normal:
                spriteBatch.Draw(texture, bounds, Color.White);
                break;
            case ButtonStatus.MouseOver:
                spriteBatch.Draw(hoverTexture, bounds, Color.White);
                break;
            case ButtonStatus.Pressed:
                spriteBatch.Draw(pressedTexture, bounds, Color.White);
                break;
            case ButtonStatus.Selected:
                spriteBatch.Draw(pressedTexture, bounds, Color.Cyan);
                break;
            default:
                spriteBatch.Draw(texture, bounds, Color.White);
                break;
        }
        string text = this.tower.Cost.ToString();
        Vector2 textSize = font.MeasureString(text);
        Vector2 textPosition = new Vector2((this.position.X + 16) - (textSize.X / 2), this.position.Y + 32);
        spriteBatch.DrawString(font, text, textPosition, Color.White);

    }
}
