using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class Program
    {
        public struct Carte
        {
            public int poids;               // le poids pour la comparaison
            public EnumSymbole symbole;     // carreau, trefle, pique et coeur
            public EnumFigure figure;       // 2 -> 10 et valet, dame, roi, As
        }

        public enum EnumSymbole
        {
            Carreau, Trefle, Pique, Coeur
        }

        public enum EnumFigure
        {
            Deux, Trois, Quatre, Cinq, Six, Sept, Huit, Neuf, Dix, Valet, Dame, Roi, As
        }


        public static Stack<Carte> JoueurA = new Stack<Carte>();
        public static Stack<Carte> JoueurB = new Stack<Carte>();


        public static Stack<Carte> TaGainA = new Stack<Carte>();
        public static Stack<Carte> TaGainB = new Stack<Carte>();


        static void Main(string[] args)
        {

            // Mon jeux de carte

            List<Carte> MesCartes = new List<Carte>();

            GenereLeJeux(MesCartes);

            string Reponse;
            do
            {

            //Melanger les cartes

            Melanger(MesCartes);



            //Distribuer des cartes

            Distribuer(MesCartes);


            
            //Le Jeu commence

            Jouer();


            Console.WriteLine("\n" + "Voulez vous jouer une nouvelle partie? Oui / Non");
            Reponse = Console.ReadLine();
            }
            while (Reponse == "Oui");




            Console.ReadLine();
        }

  

        public static void Jouer()
        {
            
            Console.WriteLine("================");
            Console.WriteLine("==Début de Jeu==");
            Console.WriteLine("================" + "\n");

            while (JoueurA.Count + TaGainA.Count > 0 && JoueurB.Count + TaGainB.Count > 0)
            {
                    //on prend une carte de chaque tas

                    Carte CA = JoueurA.Pop();
                    Carte CB = JoueurB.Pop();

                    Console.WriteLine("Carte JoueurA: {0}, {1}", CA.figure, CA.symbole);
                    Console.WriteLine("Carte JoueurB: {0}, {1}", CB.figure, CB.symbole);

                    if (CA.poids > CB.poids)
                    {
                        Console.WriteLine("---------------JoueurA gagne la main");
                        //Joueur A gagne
                        TaGainA.Push(CB);
                        TaGainA.Push(CA);
                    }
                    else if (CA.poids < CB.poids)
                    {
                        Console.WriteLine("---------------JoueurB gagne la main");
                        //Joueur B Gagne
                        TaGainB.Push(CA);
                        TaGainB.Push(CB);
                    }

                    //Bataille
                    else
                    {
                        Console.WriteLine("\n" + "=== Bataille ===");
                        Stack<Carte> CartesJouées1 = new Stack<Carte>();
                        Stack<Carte> CartesJouées2 = new Stack<Carte>();
                        Console.WriteLine("Carte JoueurA: {0}, {1}", CA.figure, CA.symbole);
                        Console.WriteLine("Carte JoueurB: {0}, {1}", CB.figure, CB.symbole);
                        CartesJouées1.Push(CA);
                        CartesJouées2.Push(CB);
                        Carte bataille1 = default(Carte);
                        Carte bataille2 = default(Carte);
                        while (bataille1.poids == bataille2.poids)
                        {
                            while (bataille1.poids == bataille2.poids && JoueurA.Count != 0 && JoueurB.Count != 0)
                            {
                                bataille1 = JoueurA.Pop();
                                CartesJouées1.Push(bataille1);
                                Console.WriteLine("Carte BatailleJoueurA: {0}, {1}", bataille1.figure, bataille1.symbole);
                                bataille2 = JoueurB.Pop();
                                CartesJouées2.Push(bataille2);
                                Console.WriteLine("Carte BatailleJoueurB: {0}, {1}", bataille2.figure, bataille2.symbole);

                                Test();
                            }

                            Console.WriteLine("==Fin Bataille==");
                            if (bataille1.poids > bataille2.poids)
                            {
                                Console.WriteLine("---------------JoueurA gagne la bataille" + "\n");
                                while (CartesJouées1.Count != 0)
                                {
                                    Carte finbataille1 = CartesJouées1.Pop();
                                    TaGainA.Push(finbataille1);
                                }
                                while (CartesJouées2.Count != 0)
                                {
                                    Carte finbataille2 = CartesJouées2.Pop();
                                    TaGainA.Push(finbataille2);
                                }
                            }
                            else if (bataille1.poids < bataille2.poids)
                            {
                                Console.WriteLine("---------------JoueurB gagne la bataille" + "\n");
                                while (CartesJouées1.Count != 0)
                                {
                                    Carte finbataille1 = CartesJouées1.Pop();
                                    TaGainB.Push(finbataille1);
                                }
                                while (CartesJouées2.Count != 0)
                                {
                                    Carte finbataille2 = CartesJouées2.Pop();
                                    TaGainB.Push(finbataille2);
                                }
                            }

                            Test();
                        }

                        Test();
                    }

                    Test();

                }


            Console.WriteLine("\n" + "================");
            Console.WriteLine("===Fin de Jeu ==");
            Console.WriteLine("================" + "\n");

            if (JoueurA.Count == 0)
            {
                Console.WriteLine("--------------------------JoueurA n'a plus des Cartes.");
                Console.WriteLine("--------------------------JoueurB a gagné!");
            }
            else
            {
                Console.WriteLine("--------------------------JoueurB n'a plus des cartes.");
                Console.WriteLine("--------------------------JoueurA a gagné!");
            }

        } 




        public static void Test()
        {
            //Test si les joueurs ont encore des cartes dans leur tas de jeux

            //JoueurA
            if (JoueurA.Count == 0)
            {
                while (TaGainA.Count > 0)
                {
                    JoueurA.Push(TaGainA.Pop());
                }
            }


            //JoueurB
            if (JoueurB.Count == 0)
            {
                while (TaGainB.Count > 0)
                {
                    JoueurB.Push(TaGainB.Pop());
                }
            }
        }


        public static void Distribuer(List<Carte> MesCartes)
        {
            //for (int a = 0; a < 26; a++)
            //{
            //    Carte AMettre1 = MesCartes[a];
            //    JoueurA.Push(AMettre1);
            //    a++;
            //    Carte AMettre2 = MesCartes[a];
            //    JoueurB.Push(AMettre2);
            //}

            bool tofirst = true;
            foreach (Carte item in MesCartes)
            {
                if (tofirst)
                {
                    JoueurA.Push(item);
                }
                else
                {
                    JoueurB.Push(item);
                }
                tofirst = !tofirst;
            }
        }


        public static Random MonRandom = new Random();
        public static void Melanger (List<Carte> MesCartes)
        {
            for (int v = 0; v < 1000; v++)
            {
                //un nombre aléatoire 0-51
                int index = MonRandom.Next(0, 51);

                //je vais chercher l'élément
                Carte ADeplacer = MesCartes[index];

                //je sauvegarde la première carte
                Carte Prems = MesCartes[0];

                //je place la carte à déplacer dans la première position
                MesCartes[0] = ADeplacer;

                //je place la premiére carte à la place de la carte déplacée
                MesCartes[index] = Prems;
            }
        }


        public static void GenereLeJeux(List<Carte> MesCartes)
        {
            for (int i = 0; i < 4; i++)
            {
                EnumSymbole leSymbole = default(EnumSymbole);
                switch (i)
                {
                    case 0: leSymbole = EnumSymbole.Carreau; break;
                    case 1: leSymbole = EnumSymbole.Trefle; break;
                    case 2: leSymbole = EnumSymbole.Pique; break;
                    case 3: leSymbole = EnumSymbole.Coeur; break;
                }

                List <Carte> lesfigures = GenereLesFigures(leSymbole);
                MesCartes.AddRange(lesfigures);
            }            
        }


        public static List<Carte> GenereLesFigures(EnumSymbole Symbole)
        {
            List<Carte> PetitTas = new List<Carte>();
            int calculPoids = 2;
            foreach (string item in Enum.GetNames(typeof(EnumFigure)))
            {
                //Créer la carte
                Carte c = new Carte();
                c.symbole = Symbole;
                c.figure = (EnumFigure)Enum.Parse(typeof(EnumFigure), item);
                c.poids = calculPoids;
                calculPoids++;
                PetitTas.Add(c);
            }
            return PetitTas;
        }



    }
}
