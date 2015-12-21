using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puissance4
{
    class Map
    {
        public const int NB_COLONNES = 7;
        public const int NB_LIGNES = 6;
        private int[,] map;


        public Map()
        {
             this.map = new int[NB_LIGNES, NB_COLONNES]{
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0}
            };
        }

        public int getValue(int ligne, int colonne){
            return this.map[ligne,colonne];
        }

        public bool columnHaveFreeSpace(int col)
        {
            if(map[0,col]==0){
                return true;
            }
            return false;
        }

        public bool columnIsEmpty(int col)
        {
            for(var i = 0; i < NB_LIGNES ; i++){
                if (map[i, col] != 0)
                {
                   return false;
                }
            }
            return true;
        }

        public int addToken(int col, int tokenVal)
        {
            if(!columnHaveFreeSpace(col)){
                return -1;
            }

            if (columnIsEmpty(col))
            {
                map[NB_LIGNES-1, col] = tokenVal;
                return getGagnant(NB_LIGNES - 1, col);
            }

            for(var i = 0; i < NB_LIGNES ; i++){
                if (map[i, col] != 0)
                {
                    map[i-1,col]=tokenVal;
                    //test si ajout ok


                    return getGagnant(i-1,col);
                }
            }

            return -1;
            
        }

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

        public int has4InARow()
        {
            return 0;
        }

        /// <summary>
        /// tes si le jeton entré permet d'etre gagnant et retourne le ganant (-1 si aucun)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        private int getGagnant(int ligne, int colonne)
        {
            int gagnant = test4Vertical(ligne, colonne);
            if (gagnant != -1)
            {
                return gagnant;
            }
            gagnant = test4Horizontal(ligne, colonne);
            if (gagnant != -1)
            {
                return gagnant;
            }
            gagnant = test4Droite(ligne, colonne);
            if (gagnant != -1)
            {
                return gagnant;
            }
            gagnant = test4Gauche(ligne, colonne);
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
        private int test4Vertical(int ligne, int colonne)
        {
            int tokenAct = map[ligne, colonne];
            if (tokenAct == Puissance4.EMPTY_TOKEN)
            {
                return -1;
            }

            int[] line = new int[NB_LIGNES];

            for (var i = 0; i < NB_LIGNES; i++)
            {
                line[i] = map[i, colonne];
            }
            return test4Array(line);
        }

        /// <summary>
        /// Test de 4 valeur dans le sens horizontale (--)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        private int test4Horizontal(int ligne, int colonne)
        {

            int tokenAct = map[ligne, colonne];
            if (tokenAct == Puissance4.EMPTY_TOKEN)
            {
                return -1;
            }

            int[] line = new int[NB_COLONNES];

            for (var i = 0; i < NB_COLONNES; i++)
            {
                line[i] = map[ligne , i];
            }
            return test4Array(line);
        }

        /// <summary>
        /// Test de 4 valeur dans le sens gauche (\)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        private int test4Gauche(int ligne, int colonne)
        {
            int tokenAct = map[ligne, colonne];
            if (tokenAct == Puissance4.EMPTY_TOKEN)
            {
                return -1;
            }

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

            int[] line = new int[nbVal];

            for (var i = 0; i < nbVal; i++)
            {
                line[i] = map[startLine + i,startColonne + i];
            }
            return test4Array(line);
        }

        /// <summary>
        /// Test de 4 valeur dans le sens droit (/)
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <returns></returns>
        private int test4Droite(int ligne, int colonne)
        {
            int tokenAct = map[ligne, colonne];
            if (tokenAct == Puissance4.EMPTY_TOKEN)
            {
                return -1;
            }

            int nbVal = NB_LIGNES < NB_COLONNES ? NB_LIGNES - ligne : NB_COLONNES - colonne;
            int startLine = NB_LIGNES-1;
            if (ligne + colonne < NB_LIGNES)
            {
                startLine = ligne + colonne;
            }
            int startColonne = 0;
            if (colonne - ligne >= 0)
            {
                startColonne = colonne - ligne;
            }

            int[] line = new int[nbVal];

            for (var i = 0; i < nbVal; i++)
            {
                line[i] = map[startLine - i, startColonne + i];
            }
            return test4Array(line);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns>return -1 on not found else return actual token</returns>
        private int test4Array(int[] line)
        {
            int actualToken = -1;
            int count = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (actualToken == 0)
                {
                    actualToken = -1;
                }
                if (line[i] != actualToken)
                {                    
                    actualToken = line[i];
                    count = 0;
                }
                else
                {
                    count++;
                }

                if (count == 3 && actualToken!=0)
                {
                    return actualToken;
                }
            }

            return -1;
            
        }
    }
}
