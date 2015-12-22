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
        public const int PLAYER_TOKEN = 1;
        public const int IA_TOKEN = 2;

        /// <summary>
        /// Nombre de jeton pour gagner
        /// </summary>
        public const int TOKEN_IN_A_ROW_TO_WIN = 4;

        private Map map;
        private String direction;

        private Boolean isJoueurTurn;
        private Boolean lockKey;
        //cursorPosition représente le curseur du joueur pour choisir où placer son pion
        private int cursorPosition;

        private ObjetPuissance4 cadre;
        private ObjetPuissance4 cursor;
        private ObjetPuissance4 pion_IA;
        private ObjetPuissance4 pion_Joueur;

        private int gagnant = -1;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Puissance4()
        {
            isJoueurTurn = false;
            lockKey = false;
            cursorPosition = 0;

            map = new Map();
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
            cursor = new ObjetPuissance4(Content.Load<Texture2D>("Images\\cursor"), new Vector2(0f, 0f), new Vector2(TAILLE_BLOCK, TAILLE_BLOCK));
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

            if (isJoueurTurn == true)
            {
                KeyboardState keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Keys.Right) && !lockKey)
                {
                    direction = "Right";
                    if (cursorPosition < (Map.NB_COLONNES - 1))
                    {
                        cursorPosition ++;
                    }
                    lockKey = true;
                }
                if (keyboard.IsKeyDown(Keys.Left) && !lockKey)
                {
                    direction = "Left";
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                    }
                    lockKey = true;
                }
                if (keyboard.IsKeyDown(Keys.Down) && !lockKey)
                {
                    direction = "Down";
                    if (map.columnHaveFreeSpace(cursorPosition))
                    {
                        gagnant = map.addToken(cursorPosition, PLAYER_TOKEN);
                        isJoueurTurn = false;
                    }
                    //TODO : Transparence du curseur
                    lockKey = true;
                }

                switch (direction)
                {
                    case("Right"):
                        if (keyboard.IsKeyUp(Keys.Right))
                            lockKey = false;
                        break;
                    case ("Left"):
                        if (keyboard.IsKeyUp(Keys.Left))
                            lockKey = false;
                        break;
                    case ("Down"):
                        if (keyboard.IsKeyUp(Keys.Down))
                            lockKey = false;
                        break;
                }
                
            }
            else
            {
                //Tour IA
                gagnant = map.addToken(IAJoueur.getBestColumn(map), Puissance4.IA_TOKEN);
                isJoueurTurn = true;
            }

            
            //TODO : Mettre à jour l'affichage
            base.Update(gameTime);
        }

        private void DrawCursor(){
            
            int xpos, ypos;
            xpos = OFFSET_X - TAILLE_BLOCK;
            ypos = OFFSET_Y + cursorPosition * TAILLE_BLOCK;
            Vector2 pos = new Vector2(ypos, xpos);
            spriteBatch.Draw(cursor.Texture, pos, Color.White);
            
        }

        private void DrawBlock(ObjetPuissance4 obj, int x, int y)
        {
            int xpos, ypos;
            xpos = OFFSET_X + x * TAILLE_BLOCK;
            ypos = OFFSET_Y + y * TAILLE_BLOCK;
            Vector2 pos = new Vector2(ypos, xpos);
            spriteBatch.Draw(obj.Texture, pos, Color.White);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            for (int x = 0; x < Map.NB_LIGNES; x++)
            {
                for (int y = 0; y < Map.NB_COLONNES; y++)
                {
                    if (map.getValue(x, y) == EMPTY_TOKEN)
                    {
                        DrawBlock(cadre,  x, y);
                    }

                    if (map.getValue(x, y) == PLAYER_TOKEN)
                    {
                        DrawBlock(pion_Joueur, x, y);
                    }

                    if (map.getValue(x, y) == IA_TOKEN)
                    {
                        DrawBlock(pion_IA,  x, y);
                    }
                }
            }
            DrawCursor();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}