using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;

namespace XNBTextureInfo
{
    public class DecoyGame : Game
    {
        private GraphicsDeviceManager graphics;

        public DecoyGame()
        {
            Content.RootDirectory = @"Content\Images";
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 160,
                PreferredBackBufferHeight = 120
            };
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();

            Exit();
        }

        protected override void LoadContent()
        {
            DirectoryInfo dir = new DirectoryInfo(Content.RootDirectory);
            string[] textures = dir.GetFiles("Item_*.xnb").Select(info => info.Name.Remove(info.Name.LastIndexOf('.'))).ToArray();

            using (StreamWriter writer = new StreamWriter(new FileStream("ItemTexture.json", FileMode.Create, FileAccess.Write, FileShare.Write)))
            {
                string[] textureData = new string[textures.Length];

                writer.Write("[");

                for (int i = 0; i < textures.Length; i++)
                {
                    string name = textures[i];
                    Texture2D texture = Content.Load<Texture2D>(name);
                    textureData[i] = $"{{\"Id\":{name.Substring(name.IndexOf('_') + 1)},\"Width\":{texture.Width},\"Height\":{texture.Height}}}";
                }
                writer.Write(string.Join(",", textureData));

                writer.Write("]");
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
        }
    }
}
