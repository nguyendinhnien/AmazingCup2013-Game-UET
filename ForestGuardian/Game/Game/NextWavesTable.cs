using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Library;
using Data;
namespace CustomGame
{
    public class NextWavesTable
    {
        private List<Data.Wave> Waves;
        private int[] next_wave_index = new int[3];
        private Texture2D[] next_wave_texture = new Texture2D[3];
        private int[] next_wave_number = new int[3];

        private Label NextWavesLabel;
        //private Vector2 HiddenNextWavesPos = new Vector2(975, 100);
        //private Vector2 ShowNextWavesPos = new Vector2(740, 100);

        private Button ArrowButton;
        private Vector2 ArrowOffset = new Vector2(25, 120);
        
        private Vector2 FirstIndexOffset = new Vector2(93, 90);
        private Vector2 FirstTextureOffset = new Vector2(132, 80);
        private Vector2 FirstNumberOffset = new Vector2(240, 95);

        private SpriteFont font;

        private bool active = false;
        private bool show = false;
        private bool open = false;

        public NextWavesTable()
        {}

        public void SetWaves(List<Data.Wave> waves){ Waves = waves; }

        public void LoadContent(ContentManager Content){
            Texture2D texture, hoverTexture;
            texture = Content.Load<Texture2D>(@"images\gameplay\buttons\next_waves_label");
            NextWavesLabel = new Label(texture, new Vector2(975,100));

            texture = Content.Load<Texture2D>(@"images\gameplay\buttons\arrow_normal_but");
            hoverTexture = Content.Load<Texture2D>(@"images\gameplay\buttons\arrow_hover_but");
            ArrowButton = new Button(texture, hoverTexture, null, new Vector2(975,100) + ArrowOffset);
            ArrowButton.Hovered += ArrowButton_Hovered;

            font = Content.Load<SpriteFont>(@"fonts\gameplay\next_wave");
        }

        private void ArrowButton_Hovered(object sender, EventArgs e)
        {
            if (!active) { active = true; }
        }

        public void Update(GameTime gameTime, int current_wave_index){
            ArrowButton.Update(gameTime);           
            if (active)
            {
                if (open) { TransitionOff(); }
                else { show = true; TransitionOn(); }
            }
            ArrowButton.Position = NextWavesLabel.Position + ArrowOffset;

            if (show)
            {
                for (int i = 0; i < 3; i++)
                {
                    next_wave_index[i] = current_wave_index + i;
                    if (next_wave_index[i] <= Waves.Count)
                    {
                        next_wave_number[i] = Waves[next_wave_index[i] - 1].EnemyNumber;
                        switch (Waves[next_wave_index[i] - 1].EnemyType)
                        {
                            case EnemyType.AXE_MAN:
                                next_wave_texture[i] = AxeMan.TEXTURE;
                                break;
                            case EnemyType.SAW_MAN:
                                next_wave_texture[i] = SawMan.TEXTURE;
                                break;
                            case EnemyType.DOZER:
                                next_wave_texture[i] = Dozer.TEXTURE;
                                break;
                        }
                    }
                }
            }
        }

        private void TransitionOn()
        {
            NextWavesLabel.PositionX -= 5;
            if (NextWavesLabel.PositionX < 740)
            {
                NextWavesLabel.PositionX = 740; active = false; open = true;
            }
        }

        private void TransitionOff()
        {
            NextWavesLabel.PositionX += 5;
            if (NextWavesLabel.PositionX > 975)
            {
                NextWavesLabel.PositionX = 975; open = false; active = false; show = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (show)
            {
                float layerDepth = NextWavesLabel.LayerDepth - 0.05f;
                Vector2 index_position, texture_position, number_position;

                index_position = NextWavesLabel.Position + FirstIndexOffset;
                index_position.X -= (font.MeasureString(next_wave_index[0].ToString())).X * 0.7f / 2;
                
                texture_position = NextWavesLabel.Position + FirstTextureOffset;
                
                number_position = NextWavesLabel.Position + FirstNumberOffset;
                number_position.X -= (font.MeasureString(next_wave_number[0].ToString())).X * 0.5f / 2;

                for (int i = 0; i < 3; i++)
                {
                    if (next_wave_index[i] <= Waves.Count)
                    {
                        spriteBatch.DrawString(font, next_wave_index[i].ToString(), index_position, Color.Orange, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, layerDepth);
                        spriteBatch.Draw(next_wave_texture[i], texture_position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, layerDepth);
                        spriteBatch.DrawString(font, next_wave_number[i].ToString(), number_position, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, layerDepth);

                        index_position.Y += 77; texture_position.Y += 77; number_position.Y += 77;
                    }
                }
            }
            NextWavesLabel.Draw(spriteBatch);
            ArrowButton.Draw(spriteBatch);
        }      
    }
}
