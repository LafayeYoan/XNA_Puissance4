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
        public const int NB_COLONNES = 7;
        public const int NB_LIGNES = 6;
        public const int TAILLE_BLOCK = 80;

        // 0 : case libre
        // 1 : case occupée par le joueur
        // 2 : case occupée par l'IA
        private int[,] map;
        private String direction;

        private Boolean isJoueurTurn;
        //cursorPosition représente le curseur du joueur pour choisir où placer son pion
        private int cursorPosition;

        private ObjetPuissance4 cadre;
        private ObjetPuissance4 pion_IA;
        private ObjetPuissance4 pion_Joueur;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Puissance4()
        {
            isJoueurTurn = true;
            cursorPosition = 0;

            map = new int[NB_LIGNES, NB_COLONNES]{
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0}
            };

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
 
            cadre = new ObjetPuissance4(Content.Load<Texture2D>("Images\\cadre"), new Vector2(0f, 0f), new Vector2(TAILLE_BLOCK, TAILLE_BLOCK));
            pion_IA = new ObjetPuissance4(Content.Load<Texture2D>("Images\\pion_IA"), new Vector2(0f, 0f), new Vector2(TAILLE_BLOCK, TAILLE_BLOCK));
            pion_Joueur = new ObjetPuissance4(Content.Load<Texture2D>("Images\\pion_Joueur"), new Vector2(0f, 0f), new Vector2(TAILLE_BLOCK, TAILLE_BLOCK));
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

            if (isJoueurTurn = true)
            {
                KeyboardState keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    direction = "Right";
                    if (cursorPosition < (NB_COLONNES - 1))
                    {
                        cursorPosition ++;
                    } 
                }
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    //TODO : vérifier que la variable direction sert réellement à quelque chose ?
                    direction = "Left";
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                    }
                }
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    direction = "Down";
                    //TODO : descendre le pion au bon endroit dans la matrice
                    isJoueurTurn = false;
                }
            }
            else
            {
                //Tour IA
                isJoueurTurn = true;
            }

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

            int offsetX = 40;
            int offsetY = 140;
            for (int x = 0; x < NB_LIGNES; x++)
            {
                for (int y = 0; y < NB_COLONNES; y++)
                {
                    if (map[x, y] == 0)
                    {
                        int xpos, ypos;
                        xpos = offsetX + x * TAILLE_BLOCK;
                        ypos = offsetY + y * TAILLE_BLOCK;
                        Vector2 pos = new Vector2(ypos, xpos);
                        spriteBatch.Draw (cadre.Texture, pos, Color.White);
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}