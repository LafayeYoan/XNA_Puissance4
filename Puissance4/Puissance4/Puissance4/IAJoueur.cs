using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puissance4
{
    class IAJoueur
    {
        public const int PROFONDEUR = 8;

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

            //Test un ajout simple
            for (int i = 0; i < Map.NB_COLONNES; i++)
            {
                //Si le gagnant est L'IA
                if (map.addToken(i, Puissance4.IA_TOKEN) == Puissance4.IA_TOKEN)
                {
                    return i;
                }

                //On retire le token pour ne pas interferer dans les tests
                map.removeLastInsertedToken(i);

            }

            //appel du recursif
            for (int i = 0; i < Map.NB_COLONNES; i++)
            {
                Map copie = map.getCopy();
                //on ajoute le Token de l'IA
                copie.addToken(i, Puissance4.IA_TOKEN);


                //On ajoute le token du joueur, puis on relance
                for (int j = 0; j < Map.NB_COLONNES; j++)
                {
                    if (copie.addToken(i, Puissance4.PLAYER_TOKEN) == Puissance4.PLAYER_TOKEN)
                    {
                        return -1;
                    }
                    return recursiveTest(copie, profondeur - 1);
                }
                    
            }
            return -1;
        }
    }
}
