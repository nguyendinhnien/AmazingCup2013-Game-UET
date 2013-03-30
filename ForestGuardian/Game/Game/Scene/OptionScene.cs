using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Data;
using Library;

namespace CustomGame
{
    public class OptionScene : GameScene
    {
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D loadingBlackTexture;
        private Rectangle loadingBlackTextureDestination;

        private Texture2D tickTexture;
        private Vector2 tickPosition;
        private Texture2D barTexture;
        private Vector2 musicBarPosition;
        private Vector2 soundBarPosition;

        private Button closeButton;
        private Button increMusicButton;
        private Button decreMusicButton;
        private Button increSoundButton;
        private Button decreSoundButton;

        public OptionScene()
            : base()
        {
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = SceneManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>(@"images\scene\OptionScene\dialog_options");
            loadingBlackTexture = content.Load<Texture2D>(@"images\scene\CommonButton\FadeScreen");

            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);
            loadingBlackTextureDestination = new Rectangle(viewport.X, viewport.Y,
                viewport.Width, viewport.Height);

            tickTexture = content.Load<Texture2D>(@"images\scene\OptionScene\tick_full_screen");
            tickPosition = backgroundPosition + new Vector2(78, 76);

            Texture2D texture = content.Load<Texture2D>(@"images\scene\OptionScene\b_close");
            Texture2D pressTexture = content.Load<Texture2D>(@"images\scene\OptionScene\b_close_clicked");
            Vector2 position = backgroundPosition + new Vector2(645, 15);
            closeButton = new Button(texture, null, pressTexture, position);
            closeButton.Clicked += CloseButtonClicked;

            texture = content.Load<Texture2D>(@"images\scene\OptionScene\decrease_volume");
            pressTexture = content.Load<Texture2D>(@"images\scene\OptionScene\decrease_volume_clicked");
            position = backgroundPosition + new Vector2(294, 179);//300 185
            decreMusicButton = new Button(texture, null, pressTexture, position);
            decreMusicButton.Clicked += DecreMusicButtonClicked;

            position = backgroundPosition + new Vector2(294, 247);
            decreSoundButton = new Button(texture, null, pressTexture, position);
            decreSoundButton.Clicked += DecreSoundButtonClicked;

            texture = content.Load<Texture2D>(@"images\scene\OptionScene\increase_volume");
            pressTexture = content.Load<Texture2D>(@"images\scene\OptionScene\increase_volume_clicked");
            position = backgroundPosition + new Vector2(640, 164); //650.176
            increMusicButton = new Button(texture, null, pressTexture, position);
            increMusicButton.Clicked += IncreMusicButtonClicked;

            position = backgroundPosition + new Vector2(640, 233);
            increSoundButton = new Button(texture, null, pressTexture, position);
            increSoundButton.Clicked += IncreSoundButtonClicked;

            barTexture = content.Load<Texture2D>(@"images\scene\OptionScene\sound_bar");
            musicBarPosition = backgroundPosition + new Vector2(338, 183);
            soundBarPosition = backgroundPosition + new Vector2(338, 251);
        }

        public override void Update(GameTime gameTime)
        {
            closeButton.Update(gameTime);
            increMusicButton.Update(gameTime);
            decreMusicButton.Update(gameTime);
            increSoundButton.Update(gameTime);
            decreSoundButton.Update(gameTime);

            if (InputManager.IsMouseJustReleased() && InputManager.IsMouseHittedRectangle(new Rectangle(
                (int)tickPosition.X, (int)tickPosition.Y, tickTexture.Width, tickTexture.Height)))
            {
                UserData.isFullScreen = !UserData.isFullScreen;
                AudioManager.soundBank.PlayCue("mouse_click");
                SceneManager.ToggleFullScreen();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(loadingBlackTexture, loadingBlackTextureDestination,
                Color.White);
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);

            closeButton.Draw(spriteBatch);
            increMusicButton.Draw(spriteBatch);
            decreMusicButton.Draw(spriteBatch);
            increSoundButton.Draw(spriteBatch);
            decreSoundButton.Draw(spriteBatch);

            spriteBatch.Draw(barTexture, musicBarPosition, new Rectangle(0, 0, (300 * UserData.setting.music_volume) / 100, 14), Color.White);
            spriteBatch.Draw(barTexture, soundBarPosition, new Rectangle(0, 0, (300 * UserData.setting.sound_volume) / 100, 14), Color.White);

            if (UserData.isFullScreen)
                spriteBatch.Draw(tickTexture, tickPosition, Color.White);

            spriteBatch.End();
        }

        private void DecreMusicButtonClicked(object sender, EventArgs e)
        {
            UserData.setting.music_volume -= 10;
            UserData.setting.music_volume = Math.Max(UserData.setting.music_volume, 0);
            AudioManager.SetMusicVolume(UserData.setting.music_volume);
        }

        private void IncreMusicButtonClicked(object sender, EventArgs e)
        {
            UserData.setting.music_volume += 10;
            UserData.setting.music_volume = Math.Min(UserData.setting.music_volume, 100);
            AudioManager.SetMusicVolume(UserData.setting.music_volume);
        }

        private void DecreSoundButtonClicked(object sender, EventArgs e)
        {
            UserData.setting.sound_volume -= 10;
            UserData.setting.sound_volume = Math.Max(UserData.setting.sound_volume, 0);
            AudioManager.SetSoundVolume(UserData.setting.sound_volume);
        }

        private void IncreSoundButtonClicked(object sender, EventArgs e)
        {
            UserData.setting.sound_volume += 10;
            UserData.setting.sound_volume = Math.Min(UserData.setting.sound_volume, 100);
            AudioManager.SetSoundVolume(UserData.setting.sound_volume);
        }

        private void CloseButtonClicked(object sender, EventArgs e)
        {
            DataSerializer.SaveData(UserData.setting, UserData.CustomSettingDirectory, UserData.CustomSettingFile);
            this.ExitScene();
        }
    }
}