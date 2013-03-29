using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Library;
namespace CustomGame
{
    public class VictoryScene : EndGameScene
    {
        private Label VictoryLabel;

        private string UnlockedMapName;
        private Vector2 UnlockedMapPos = new Vector2(350,360);
        private string UnlockedTowerName;
        private Vector2 UnlockedTowerPos = new Vector2(350,420);
        
        private SpriteFont unlock_font;
        private SpriteFont normal_font;

        public VictoryScene(int total_point, int total_kill, string map_name)
            :base(total_point,total_kill, map_name){
                int new_map_unlock = UserData.UnlockNewMap();
                if (new_map_unlock >= 0){
                    UnlockedMapName = MapLoadManager.getMap(new_map_unlock).Name;
                }
                int new_tower_unlock = UserData.UnlockNewTower();
                if (new_tower_unlock >= 0)
                {
                    UnlockedTowerName = Enum.GetNames(typeof(TowerType))[new_tower_unlock];
                }
        }

        public override void LoadContent()
        {
            ContentManager Content = sceneManager.Game.Content;
            Texture2D texture = Content.Load<Texture2D>(@"images\scene\EndGameScene\victory_label");
            VictoryLabel = new Label();
            VictoryLabel.Texture = texture;
            VictoryLabel.Center = new Vector2(512, 100);
            unlock_font = Content.Load<SpriteFont>(@"fonts\VictoryScene\unlock_font");
            normal_font = Content.Load<SpriteFont>(@"fonts\VictoryScene\normal_font");

            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch,gameTime);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            VictoryLabel.Draw(spriteBatch);
            
            Vector2 normal_position;
            if (UnlockedMapName != null)
            {
                spriteBatch.DrawString(unlock_font, UnlockedMapName, UnlockedMapPos, Color.Pink);

                normal_position = UnlockedMapPos + new Vector2(unlock_font.MeasureString(UnlockedMapName).X, 4);
                spriteBatch.DrawString(normal_font, " is unlocked", normal_position, Color.White);
            }
            else
            {
                spriteBatch.DrawString(normal_font, "No map is unlocked", UnlockedMapPos, Color.White);
            }

            if (UnlockedTowerName != null)
            {
                spriteBatch.DrawString(unlock_font, UnlockedTowerName, UnlockedTowerPos, Color.Green);
                
                normal_position = UnlockedTowerPos + new Vector2(unlock_font.MeasureString(UnlockedTowerName).X, 4);
                spriteBatch.DrawString(normal_font, " is unlocked", normal_position, Color.White);
            }
            else
            {
                spriteBatch.DrawString(normal_font, "No tower is unlocked", UnlockedTowerPos, Color.White);
            }

            spriteBatch.End();
        }
    }
}
