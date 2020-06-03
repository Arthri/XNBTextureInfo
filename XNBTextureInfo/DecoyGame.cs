using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;

namespace XNBTextureInfo
{
    public class DecoyGame : Game
    {
        private GraphicsDeviceManager graphics;

        public Texture2D[] ItemTextures { get; private set; }

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

            SerializeContent();

            Exit();
        }

        protected override void LoadContent()
        {
            DirectoryInfo dir = new DirectoryInfo(Content.RootDirectory);
            string[] textures = dir.GetFiles("Item_*.xnb").Select(info => info.Name.Remove(info.Name.LastIndexOf('.'))).ToArray();
            ItemTextures = new Texture2D[textures.Length];
            for (int i = 0; i < textures.Length; i++)
            {
                ItemTextures[i] = Content.Load<Texture2D>(textures[i]);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        public void SerializeContent()
        {
            using (StreamWriter writer = new StreamWriter(new FileStream("ItemTexture.json", FileMode.Create, FileAccess.Write, FileShare.Write)))
            {
                string[] textureData = new string[ItemTextures.Length];

                writer.Write("[");

                for (int i = 0; i < ItemTextures.Length; i++)
                {
                    textureData[i] = $"{{\"Id\":{i},\"Width\":{ItemTextures[i].Width},\"Height\":{ItemTextures[i].Height}}}";
                }
                writer.Write(string.Join(",", textureData));

                writer.Write("]");
            }
        }
    }
}
