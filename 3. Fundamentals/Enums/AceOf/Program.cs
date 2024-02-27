using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceOf
{
    internal class Program
    {
        enum Suit {
            Heart,
            Spade,
            Diamond,
            Club
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Mission 1
            DrawAce(Suit.Heart);

            // Bonus Mission
            DrawCard(Suit.Diamond, 1);
            DrawCard(Suit.Spade, 10);
            DrawCard(Suit.Club, 2);
            DrawCard(Suit.Heart, 13);

            // to keep console open
            Console.ReadLine();

            // Mission 1
            void DrawAce(Suit suit)
            {
                string chosenSuit = "";
                switch (suit)
                {
                    case Suit.Heart:
                        chosenSuit = "♥";
                        break;
                    case Suit.Spade:
                        chosenSuit = "♠";
                        break;
                    case Suit.Diamond:
                        chosenSuit = "♦";
                        break;
                    case Suit.Club:
                        chosenSuit = "♣";
                        break;
                }
                List<string> ace = ChooseRank(1);
                foreach (string cardRow in ace)
                {
                    Console.WriteLine(cardRow.Replace("!", chosenSuit));
                }
            }

            // Bonus Mission
            void DrawCard(Suit suit, int rank)
            {
                string chosenSuit = "";
                switch (suit)
                {
                    case Suit.Heart:
                        chosenSuit = "♥";
                        break;
                    case Suit.Spade:
                        chosenSuit = "♠";
                        break;
                    case Suit.Diamond:
                        chosenSuit = "♦";
                        break;
                    case Suit.Club:
                        chosenSuit = "♣";
                        break;
                }

                List<string> chosenCard = ChooseRank(rank);

                foreach (string cardRow in chosenCard)
                {
                    Console.WriteLine(cardRow.Replace("!", chosenSuit));
                }
            }

            List<string> ChooseRank(int rank)
            {
                List<List<string>> cardRowsList = new List<List<string>> {
                    new List<string> { "╭─────────╮", "│A        │", "│!        │", "│         │", "│    !    │", "│         │", "│        !│", "│        A│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│2   !    │", "│!        │", "│         │", "│         │", "│         │", "│        !│", "│    !   2│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│3   !    │", "│!        │", "│         │", "│    !    │", "│         │", "│        !│", "│    !   3│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│4 !   !  │", "│!        │", "│         │", "│         │", "│         │", "│        !│", "│  !   ! 4│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│5 !   !  │", "│!        │", "│         │", "│    !    │", "│         │", "│        !│", "│  !   ! 5│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│6 !   !  │", "│!        │", "│         │", "│  !   !  │", "│         │", "│        !│", "│  !   ! 6│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│7 !   !  │", "│!        │", "│    !    │", "│  !   !  │", "│         │", "│        !│", "│  !   ! 7│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│8 !   !  │", "│!        │", "│    !    │", "│  !   !  │", "│    !    │", "│        !│", "│  !   ! 8│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│9 !   !  │", "│!        │", "│  !   !  │", "│    !    │", "│  !   !  │", "│        !│", "│  !   ! 9│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│10!   !  │", "│!   !    │", "│  !   !  │", "│         │", "│  !   !  │", "│    !   !│", "│  !   !10│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│J┌─────┐ │", "│!│!\\__/│ │", "│ │|(_/|│ │", "│ │} / {│ │", "│ │|/_)|│ │", "│ │/  \\!│!│", "│ └─────┘J│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│Q┌─────┐ │", "│!│!(_(/│ │", "│ │  )/❀│ │", "│ │{ / }│ │", "│ │❀/(  │ │", "│ │/) )!│!│", "│ └─────┘Q│", "╰─────────╯" },
                    new List<string> { "╭─────────╮", "│K┌─────┐ │", "│!│!\\__/│ │", "│ │ (_/|│ │", "│ │+ / +│ │", "│ │|/_) │ │", "│ │/  \\!│!│", "│ └─────┘K│", "╰─────────╯" },
                };


                return cardRowsList[rank - 1];
            }
        }
    }
}
