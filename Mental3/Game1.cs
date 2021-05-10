using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mental3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Map map;
        Player player;
        public static Vector2 gameScaleRatio;
        public static Vector2 gameNativeResolution;
        private RenderTarget2D _nativeRenderTarget;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            Window.IsBorderless = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            _nativeRenderTarget = new RenderTarget2D(GraphicsDevice, 512, 288);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new Map("map", Content);
            player = new Player(map, Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardManager.updateState();
            player.update();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();          
            map.draw(_spriteBatch);
            player.draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState:SamplerState.PointClamp);
            _spriteBatch.Draw(_nativeRenderTarget,new Rectangle(0,0,_graphics.PreferredBackBufferWidth,_graphics.PreferredBackBufferHeight),Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
