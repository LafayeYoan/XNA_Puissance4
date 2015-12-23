using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Puissance4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Puissance4 : Microsoft.Xna.Framework.Game
    {
       
        public const int TAILLE_BLOCK = 80;
        public const int OFFSET_X = 140;
        public const int OFFSET_Y = 140;

        public const int EMPTY_TOKEN = 0;
        public const int PLAYER1_TOKEN = 1;
        public const int PLAYER2_TOKEN = 2;

        /// <summary>
        /// Nombre de jeton pour gagner
        /// </summary>
        public const int TOKEN_IN_A_ROW_TO_WIN = 4;

        private Map map;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //ObjectPuissace4 
        public static ObjetPuissance4 cursor;
        public static ObjetPuissance4 pion_J1;
        public static ObjetPuissance4 pion_J2;
        public static ObjetPuissance4 cadre;
        

        public Puissance4()
        {
            map = new Map();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            new JoueurHumain(PLAYER1_TOKEN, map);
            new JoueurIA(PLAYER2_TOKEN, map);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 660;
            graphics.ApplyChanges();

            cursor = new ObjetPuissance4(Content.Load<Texture2D>("Images\\cursor"), new Vector2(0f, 0f), new Vector2(Puissance4.TAILLE_BLOCK, Puissance4.TAILLE_BLOCK));
            pion_J1 = new ObjetPuissance4(Content.Load<Texture2D>("Images\\pion_rouge"), new Vector2(0f, 0f), new Vector2(Puissance4.TAILLE_BLOCK, Puissance4.TAILLE_BLOCK));
            pion_J2 = new ObjetPuissance4(Content.Load<Texture2D>("Images\\pion_jaune"), new Vector2(0f, 0f), new Vector2(Puissance4.TAILLE_BLOCK, Puissance4.TAILLE_BLOCK));
            cadre = new ObjetPuissance4(Content.Load<Texture2D>("Images\\cadre"), new Vector2(0f, 0f), new Vector2(Puissance4.TAILLE_BLOCK, Puissance4.TAILLE_BLOCK));
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Joueur.getPlayer().Update(gameTime);

            
            //TODO : Mettre à jour l'affichage
            base.Update(gameTime);
        }

        

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            Joueur.getPlayer().Draw(gameTime, spriteBatch);

            map.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
}