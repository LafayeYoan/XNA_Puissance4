using System;
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
    /// <summary>
    /// Represente le plateau de jeu
    /// </summary>
    class Map
    {
        /// <summary>
        /// Nombre de collones
        /// </summary>
        public const int NB_COLONNES = 7;

        /// <summary>
        /// Nombre de Lignes
        /// </summary>
        public const int NB_LIGNES = 6;

        /// <summary>
        /// Stockage de la carte
        /// </summary>
        private int[,] map;


        /// <summary>
        /// Constructeur
        /// </summary>
        public Map()
        {
            this.map = new int[NB_LIGNES, NB_COLONNES];
            for (int i = 0; i < NB_LIGNES; i++)
            {
                for (int j = 0; j < NB_COLONNES; j++)
                {
                    map[i, j] = Puissance4.EMPTY_TOKEN;
                }
            }
        }

        /// <summary>
        /// Return la valeur de la case
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public int getValue(int ligne, int colonne)
        {
            return this.map[ligne, colonne];
        }


        /// <summary>
        /// Verifie si il reste de la place dans la colonne.
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool columnHaveFreeSpace(int col)
        {
            if (map[0, col] == Puissance4.EMPTY_TOKEN)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Verifie si la colonne est vide
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool columnIsEmpty(int col)
        {
            for (var i = 0; i < NB_LIGNES; i++)
            {
                if (map[i, col] != Puissance4.EMPTY_TOKEN)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Ajoute un jeton dans la colonne
        /// </summary>
        /// <param name="col"></param>
        /// <param name="tokenVal"></param>
        /// <returns>Token du gagnant</returns>
        public int addToken(int col, int tokenVal)
        {
            if (!columnHaveFreeSpace(col))
            {
                return -1;
            }

            if (columnIsEmpty(col))
            {
                map[NB_LIGNES - 1, col] = tokenVal;
                return getGagnant(NB_LIGNES - 1, col);
            }

            for (var i = 0; i < NB_LIGNES; i++)
            {
                if (map[i, col] != Puissance4.EMPTY_TOKEN)
                {
                    map[i - 1, col] = tokenVal;
                    //test si ajout ok


                    return getGagnant(i - 1, col);
                }
            }

            return -1;

        }

        /// <summary>
        /// Retire le dernier jeton inséré
        /// </summary>
        /// <param name="col"></param>
        public void removeLastInsertedToken(int col)
        {
            for (int i = 0; i < NB_LIGNES; i++)
            {
                if (map[i, col] != Puissance4.EMPTY_TOKEN)
                {
                    map[i, col] = Puissance4.EMPTY_TOKEN;
                    break;
                }
            }
        }

        /// <summary>
        /// Retourne une copie de la carte (Pour les calculs d'IA)
        /// </summary>
        /// <returns></returns>
        public Map getCopy()
        {
            Map copy = new Map();
            for (int x = 0; x < NB_LIGNES; x++)
            {
                for (int y = 0; y < NB_COLONNES; y++)
                {
                    copy.map[x, y] = map[x, y];
                }
            }
            return copy;
        }

        /// <summary>
        /// tes si le jeton entré permet d'etre gagnant et retourne le ganant (-1 si aucun)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public int getGagnant(int ligne, int colonne)
        {
            int tokenAct = map[ligne, colonne];
            if (tokenAct == Puissance4.EMPTY_TOKEN)
            {
                return -1;
            }
            int gagnant = getWinner(getVerticalLine(ligne, colonne));
            if (gagnant != -1)
            {
                return gagnant;
            }
            gagnant = getWinner(getHorizontalLine(ligne, colonne));
            if (gagnant != -1)
            {
                return gagnant;
            }
            gagnant = getWinner(getDroiteLine(ligne, colonne));
            if (gagnant != -1)
            {
                return gagnant;
            }
            gagnant = getWinner(getGaucheLine(ligne, colonne));
            if (gagnant != -1)
            {
                return gagnant;
            }
            return -1;
        }

        /// <summary>
        /// Test de 4 valeurs dans le sens Vertical (|)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public List<int> getVerticalLine(int ligne, int colonne)
        {
            List<int> line = new List<int>();
            for (var i = 0; i < NB_LIGNES; i++)
            {
                line.Add(map[i, colonne]);
            }
            return line;
        }

        /// <summary>
        /// Test de 4 valeur dans le sens horizontale (--)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        private List<int> getHorizontalLine(int ligne, int colonne)
        {
            List<int> line = new List<int>();

            for (var i = 0; i < NB_COLONNES; i++)
            {
                line.Add(map[ligne, i]);
            }
            return line;
        }

        /// <summary>
        /// Test de 4 valeur dans le sens gauche (\)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public List<int> getGaucheLine(int ligne, int colonne)
        {

            int nbVal = NB_LIGNES < NB_COLONNES ? NB_LIGNES - ligne : NB_COLONNES - colonne;
            int startLine = 0;
            if (ligne - colonne >= 0)
            {
                startLine = ligne - colonne;
            }
            int startColonne = 0;
            if (colonne - ligne >= 0)
            {
                startColonne = colonne - ligne;
            }

            List<int> line = new List<int>();

            for (var i = 0; i < (NB_LIGNES < NB_COLONNES ? NB_LIGNES : NB_COLONNES); i++)
            {
                if (startLine + i < NB_LIGNES && startColonne + i < NB_COLONNES)
                {
                    line.Add(map[startLine + i, startColonne + i]);
                }
                else
                {
                    break;
                }
            }
            return line;
        }

        /// <summary>
        /// Test de 4 valeur dans le sens droit (/)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public List<int> getDroiteLine(int ligne, int colonne)
        {
            int startLine = NB_LIGNES - 1;
            if (ligne + colonne < NB_LIGNES)
            {
                startLine = ligne + colonne;
            }
            int startColonne = 0;
            if (colonne - ligne >= 0)
            {
                startColonne = colonne - ligne;
            }
            else
            {
                startColonne = colonne;
            }

            List<int> line = new List<int>();

            for (var i = 0; i < (NB_LIGNES < NB_COLONNES ? NB_LIGNES : NB_COLONNES); i++)
            {
                if (startLine - i >= 0 && startColonne + i < NB_COLONNES)
                {
                    line.Add(map[startLine - i, startColonne + i]);
                }
                else
                {
                    break;
                }
            }
            return line;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns>return -1 on not found else return actual token</returns>
        private int getWinner(List<int> line)
        {
            String str = "";
            String strPlayer = "";
            String strIA = "";
            for (int i = 0; i < line.Count; i++)
            {
                str += line[i];
            }

            for (int i = 0; i < Puissance4.TOKEN_IN_A_ROW_TO_WIN; i++)
            {
                strPlayer += Puissance4.PLAYER1_TOKEN;
                strIA += Puissance4.PLAYER2_TOKEN;
            }

            if (str.Contains(strPlayer))
            {
                return Puissance4.PLAYER1_TOKEN;
            }
            else if (str.Contains(strIA))
            {
                return Puissance4.PLAYER2_TOKEN;
            }
            return -1;
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Map.NB_LIGNES; x++)
            {
                for (int y = 0; y < Map.NB_COLONNES; y++)
                {
                    if (this.getValue(x, y) == Puissance4.EMPTY_TOKEN)
                    {
                        DrawBlock(spriteBatch,Puissance4.cadre, x, y);
                    }

                    if (this.getValue(x, y) == Puissance4.PLAYER1_TOKEN)
                    {
                        DrawBlock(spriteBatch, Puissance4.pion_J1, x, y);
                    }

                    if (this.getValue(x, y) == Puissance4.PLAYER2_TOKEN)
                    {
                        DrawBlock(spriteBatch, Puissance4.pion_J2, x, y);
                    }
                }
            }

        }
        private void DrawBlock(SpriteBatch spriteBatch, ObjetPuissance4 obj, int x, int y)
        {
            int xpos, ypos;
            xpos = Puissance4.OFFSET_X + x * Puissance4.TAILLE_BLOCK;
            ypos = Puissance4.OFFSET_Y + y * Puissance4.TAILLE_BLOCK;
            Vector2 pos = new Vector2(ypos, xpos);
            spriteBatch.Draw(obj.Texture, pos, Color.White);
        }
    }
}
