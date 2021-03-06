﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Puissance4
{
    class JoueurIA:Joueur
    {
        public const int PROFONDEUR = 8;

        public JoueurIA(int playerToken, Map map)
            : base(playerToken, map)
        {
            base.NomJoueur = "Ordinateur";
        }


        public override void Update(GameTime gametime)
        {
            Joueur.gagnant = map.addToken(getBestColumn(map), this.playerToken);
            Joueur.nextPlayer();
        }
        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            //Pas de draw specific
        }

        /// <summary>
        /// Retourne la colone gagnante pour L'IA
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static int getBestColumn(Map map){
            Map copy = map.getCopy();
            int colonne = recursiveTest(copy, PROFONDEUR);
            //si aucune colonne a été trouvée , on ajoute de maniere aléatoire
            while (colonne == -1)
            {
                int colTest = new Random().Next(0, Map.NB_COLONNES);
                if (map.columnHaveFreeSpace(colTest))
                {
                    colonne = colTest;
                }
            }
            return colonne;
        }

        /// <summary>
        /// Retourne la colonne si gagnante trouvée ou -1 sinon
        /// </summary>
        /// <param name="map"></param>
        /// <param name="profondeur"></param>
        /// <returns></returns>
        private static int recursiveTest(Map map, int profondeur){

            // Sortie en cas de profondeur == 0
            if (profondeur <= 0)
            {
                return -1;
            }

            //On verifie si le joueur gagne en jouant a la position, si oui on retourne cette colonne a jouer
            for (int i = 0; i < Map.NB_COLONNES; i++)
            {
                if (map.addToken(i, Puissance4.PLAYER1_TOKEN) == Puissance4.PLAYER1_TOKEN)
                {
                    map.removeLastInsertedToken(i);
                    return i;
                }
                //netoie le dernier movement
                map.removeLastInsertedToken(i);
            }
            
            //On test les places, si on gagne, on retourne cette colonne
            for (int i = 0; i < Map.NB_COLONNES; i++)
            {
                if (map.addToken(i, Puissance4.PLAYER2_TOKEN) == Puissance4.PLAYER2_TOKEN)
                {
                    map.removeLastInsertedToken(i);
                    return i;
                }
                //netoie le dernier movement
                map.removeLastInsertedToken(i);
            }

            //On creé des copies pour chaque colone possible du joueur
            for (int i = 0; i < Map.NB_COLONNES; i++)
            {
                //Si on peut ajouter un jeton
                if (map.columnHaveFreeSpace(i))
                {
                    //On créé une copie
                    Map copie = map.getCopy();

                    //On ajoute un token Joueur
                    map.addToken(i, Puissance4.PLAYER1_TOKEN);

                    //on appelle le recursif
                    return recursiveTest(copie, profondeur - 1);
                }
            }
            return -1;
        }

        private bool hasMaxMinusOneInARow(List<int> ligne){
            int nbInARow = Puissance4.TOKEN_IN_A_ROW_TO_WIN -1;
            String str = "";
            String strToTest = "" + Puissance4.EMPTY_TOKEN;
            for (int i = 0; i < ligne.Count; i++ )
            {
                str += ligne[i];             
            }

            for (int i = 0; i < nbInARow; i++)
            {
                strToTest += Puissance4.PLAYER1_TOKEN;
            }
            strToTest += Puissance4.EMPTY_TOKEN;
            return str.Contains(strToTest);
        }
    }
}
