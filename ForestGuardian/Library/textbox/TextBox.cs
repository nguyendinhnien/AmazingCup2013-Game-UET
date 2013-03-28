﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Library
{
    public delegate void TextBoxEvent(TextBox sender);

    public class TextBox : IKeyboardSubscriber
    {
        private Texture2D _textBoxTexture;
        private Texture2D _caretTexture;
        private SpriteFont _font;

        private Vector2 _position;
        private Vector2 _textOffset;
        MouseState _previousMouse;

        //public int X { get; set; }
        //public int Y { get; set; }

        //Dat Width cho text
        public int Width { get; set; }
        //Height duoc set boi font
        public int Height { get; private set; }
        public bool Highlighted { get; set; }
        public bool PasswordBox { get; set; }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public Vector2 Center
        {
            get { return new Vector2(_position.X + _textBoxTexture.Bounds.Width / 2, _position.Y + _textBoxTexture.Bounds.Height / 2); }
            set { _position.X = value.X - _textBoxTexture.Bounds.Width / 2; _position.Y = value.Y - _textBoxTexture.Bounds.Height / 2; }

        }
        public Vector2 TextOffset
        {
            set { _textOffset = value; }
        }

        public event TextBoxEvent Clicked;

        string _text = "";
        public String Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                if (_text == null)
                    _text = "";

                if (_text != "")
                {
                    //if you attempt to display a character that is not in your font
                    //you will get an exception, so we filter the characters
                    String filtered = "";
                    foreach (char c in value)
                    {
                        if (_font.Characters.Contains(c))
                            filtered += c;
                    }

                    _text = filtered;

                    if (_font.MeasureString(_text).X > Width)
                    {
                        //recursion to ensure that text cannot be larger than the box
                        Text = _text.Substring(0, _text.Length - 1);
                    }
                }
            }
        }

        public TextBox(Texture2D textBoxTexture, Texture2D caretTexture, SpriteFont font)
            : this(textBoxTexture, Vector2.Zero, caretTexture, font) { }
        public TextBox(Texture2D textBoxTexture, Vector2 position, Texture2D caretTexture, SpriteFont font)
        {
            _textBoxTexture = textBoxTexture;
            _position = position;
            _caretTexture = caretTexture;
            _font = font;
            _previousMouse = Mouse.GetState();
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            Point mousePoint = new Point(mouse.X, mouse.Y);

            Rectangle position = new Rectangle((int)_textOffset.X, (int)_textOffset.Y, Width, Height);
            if (position.Contains(mousePoint))
            {
                Highlighted = true;
                if (_previousMouse.LeftButton == ButtonState.Released && mouse.LeftButton == ButtonState.Pressed)
                {
                    if (Clicked != null)
                        Clicked(this);
                }
            }
            else
            {
                Highlighted = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            bool caretVisible = true;
            if ((gameTime.TotalGameTime.TotalMilliseconds % 1000) < 500)
                caretVisible = false;
            else
                caretVisible = true;

            String toDraw = Text;

            if (PasswordBox)
            {
                toDraw = "";
                for (int i = 0; i < Text.Length; i++)
                    toDraw += (char)0x2022; //bullet character (make sure you include it in the font!!!!)
            }
            //my texture was split vertically in 2 parts, upper was unhighlighted, lower was highlighted version of the box
            //spriteBatch.Draw(_textBoxTexture, new Rectangle(X, Y, Width, Height), new Rectangle(0, (Highlighted ? (_textBoxTexture.Height / 2) : 0), _textBoxTexture.Width, _textBoxTexture.Height / 2), Color.White);
            spriteBatch.Draw(_textBoxTexture, _position, Color.White);

            Vector2 size = _font.MeasureString(toDraw);
            Vector2 text_position = _position + _textOffset;
            Vector2 caret_position = text_position + new Vector2(size.X + 2, 2);

            if (caretVisible && Selected)
            {
                //Draw caret
                spriteBatch.Draw(_caretTexture, caret_position, Color.White); //my caret texture was a simple vertical line, 4 pixels smaller than font size.Y
            }
            //shadow first, then the actual text
            spriteBatch.DrawString(_font, toDraw, text_position + Vector2.One, Color.Black);
            spriteBatch.DrawString(_font, toDraw, text_position, Color.White);
        }


        public void RecieveTextInput(char inputChar)
        {
            Text = Text + inputChar;
        }
        public void RecieveTextInput(string text)
        {
            Text = Text + text;
        }
        public void RecieveCommandInput(char command)
        {
            switch (command)
            {
                case '\b': //backspace
                    if (Text.Length > 0)
                        Text = Text.Substring(0, Text.Length - 1);
                    break;
                case '\r': //return
                    if (OnEnterPressed != null)
                        OnEnterPressed(this);
                    break;
                case '\t': //tab
                    if (OnTabPressed != null)
                        OnTabPressed(this);
                    break;
                default:
                    break;
            }
        }
        public void RecieveSpecialInput(Keys key)
        { }

        public event TextBoxEvent OnEnterPressed;
        public event TextBoxEvent OnTabPressed;

        public bool Selected
        {
            get;
            set;
        }
    }
}
