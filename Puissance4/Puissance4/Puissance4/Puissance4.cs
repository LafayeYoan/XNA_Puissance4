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
    public class Puissance4 : Microsoft.Xna.Framework.Game
    {
        //Jeu en cours (pour appel de la methode exit)
        public static Puissance4 game;

        //Taille des blocks
        public const int TAILLE_BLOCK = 80;

        //Offsets
        public const int OFFSET_X = 140;
        public const int OFFSET_Y = 140;

        //Taille fenetre
        public const int GAME_HEIGHT = 640;
        public const int GAME_WIDTH = 850;

        //Valeurs des jetons
        public const int EMPTY_TOKEN = 0;
        public const int PLAYER1_TOKEN = 1;
        public const int PLAYER2_TOKEN = 2;

        /// <summary>
        /// Nombre de jeton pour gagner
        /// </summary>
        public const int TOKEN_IN_A_ROW_TO_WIN = 4;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Objets pour le jeu
        public static ObjetPuissance4 cursor;
        public static ObjetPuissance4 pion_J1;
        public static ObjetPuissance4 pion_J2;
        public static ObjetPuissance4 cadre;

        //Objets pour le menu
        public static SpriteFont titleFont;
        public static SpriteFont textFont;

        public Puissance4()
        {
            game = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Phase.setPhase(new PhaseMenu());
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

            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.ApplyChanges();

            cursor = new ObjetPuissance4(Content.Load<Texture2D>("Images\\cursor"), new Vector2(0f, 0f), new Vector2(Puissance4.TAILLE_BLOCK, Puissance4.TAILLE_BLOCK));
            pion_J1 = new ObjetPuissance4(Content.Load<Texture2D>("Images\\pion_rouge"), new Vector2(0f, 0f), new Vector2(Puissance4.TAILLE_BLOCK, Puissance4.TAILLE_BLOCK));
            pion_J2 = new ObjetPuissance4(Content.Load<Texture2D>("Images\\pion_jaune"), new Vector2(0f, 0f), new Vector2(Puissance4.TAILLE_BLOCK, Puissance4.TAILLE_BLOCK));
            cadre = new ObjetPuissance4(Content.Load<Texture2D>("Images\\cadre"), new Vector2(0f, 0f), new Vector2(Puissance4.TAILLE_BLOCK, Puissance4.TAILLE_BLOCK));

            titleFont = Content.Load<SpriteFont>("Font\\ArialTitle");
            textFont = Content.Load<SpriteFont>("Font\\ArialText");
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

            Phase.getPhase().Update(gameTime);

            
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

            Phase.getPhase().Draw(gameTime, spriteBatch);            
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}