using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGameServer.Program;

namespace CardGameDemoDB
{
    public class BattleLogic
    {
        
        public BattleLogic() { }

        public string CheckInstantWin(DeckPlayer1 player1Card, DeckPlayer2 player2Card) 
        {
            // Check if both players have the same card
            if (player1Card.Species == player2Card.Species)
            {
                return "tie";
            }

            // Check for instant win conditions
            if (player1Card.Species == "goblin" && player2Card.Species == "dragon")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                Console.WriteLine("Goblins are too afraid of Dragons to attack. The Dragon wins");
                return "player2";
            }
            else if (player1Card.Species == "dragon" && player2Card.Species == "goblin")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                Console.WriteLine("Goblins are too afraid of Dragons to attack. The Dragon wins");
                return "player1";
            }
            else if (player1Card.Species == "wizzard" && player2Card.Species == "ork")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                Console.WriteLine("Wizzards can control Orks so they are not able to damage them. The Wizzard wins");
                return "player1";
            }
            else if (player1Card.Species == "ork" && player2Card.Species == "wizzard")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                Console.WriteLine("Wizzards can control Orks so they are not able to damage them. The Wizzard wins");
                return "player2";
            }
            else if (player1Card.Species == "knight" && player2Card.Species == "magic")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                if (player2Card.Element == "water") {
                    Console.WriteLine("The armor of Knights is so heavy that WaterSpells make them drown instantly. The WaterSpell wins.");
                    return "player2";
                }
                else {
                    return "tie";
                }
                
            }
            else if (player1Card.Species == "magic" && player2Card.Species == "knight")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                if (player1Card.Element == "water")
                {
                    Console.WriteLine("The armor of Knights is so heavy that WaterSpells make them drown instantly. The WaterSpell wins.");
                    return "player1";
                }
                else
                {
                    return "tie";
                }
            }
            else if (player1Card.Species == "kraken" && player2Card.Species == "magic")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                Console.WriteLine("The Kraken is immune against spells. The Kraken wins.");
                return "player1";
            }
            else if (player1Card.Species == "magic" && player2Card.Species == "kraken")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                Console.WriteLine("The Kraken is immune against spells. The Kraken wins.");
                return "player2";
            }
            else if (player1Card.Species == "fireelf" && player2Card.Species == "dragon")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                Console.WriteLine("The FireElves know Dragons since they were little and can evade their attacks. The FireElf wins.");
                return "player1";
            }
            else if (player1Card.Species == "dragon" && player2Card.Species == "fireelf")
            {
                Console.WriteLine(player1Card.Card_name + " VS " + player2Card.Card_name);
                Console.WriteLine("The FireElves know Dragons since they were little and can evade their attacks. The FireElf wins.");
                return "player2";
            }

            return "tie";

        }

        public string CheckDamage(DeckPlayer1 player1Card, DeckPlayer2 player2Card) 
        {
            float damagePlayer1;
            float damagePlayer2;
            string winner = "";

            Console.WriteLine(player1Card.Card_name + " with damage " + player1Card.Damage + " VS " + player2Card.Card_name + " with damage " + player2Card.Damage);
            Console.WriteLine(player1Card.Card_type + " with element " + player1Card.Element + " VS " + player2Card.Card_type + " with damage " + player2Card.Element);

            // Check if both players have the same card type
            if (player1Card.Card_type == "monster" && player2Card.Card_type == "monster")
            {
                //Console.WriteLine(player1Card.Card_name + " with damage " + player1Card.Damage + " VS " + player2Card.Card_name + " with damage " + player2Card.Damage);
                if(player1Card.Damage > player2Card.Damage) {
                    Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                    winner = "player1";
                } else if(player1Card.Damage < player2Card.Damage) {
                    Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                    winner = "player2";
                }
                else {
                    Console.WriteLine("It's a draw. Neither player loses their card this round.");
                    winner = "tie";
                }
 
            }


            if(player1Card.Card_type == "monster" && player2Card.Card_type == "spell" || player1Card.Card_type == "spell" && player2Card.Card_type == "monster" || player1Card.Card_type == "spell" && player2Card.Card_type == "spell") {

                //Console.WriteLine(player1Card.Card_name + " with damage " + player1Card.Damage + " VS " + player2Card.Card_name + " with damage " + player2Card.Damage);

                switch (player1Card.Element + player2Card.Element)
                {
                    case "waterwater":
                        {
                            if (player1Card.Damage > player2Card.Damage)
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            }
                            else if (player1Card.Damage < player2Card.Damage)
                            {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                                winner = "tie";
                            }
                            break;
                        }
                    case "waterfire": {
                            damagePlayer1 = (float)player1Card.Damage * 2;
                            damagePlayer2 = (float)player2Card.Damage / 2;
                            if (damagePlayer1 > damagePlayer2) 
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            } 
                            else if (damagePlayer1 < damagePlayer2) {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else 
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                                
                            }
                            break;
                        }
                    case "waternormal":
                        {
                            damagePlayer1 = (float)player1Card.Damage / 2;
                            damagePlayer2 = (float)player2Card.Damage * 2;
                            if (damagePlayer1 > damagePlayer2)
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            }
                            else if (damagePlayer1 < damagePlayer2)
                            {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                            }
                            break;
                        }
                    case "firefire": 
                    {
                            if (player1Card.Damage > player2Card.Damage)
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            }
                            else if (player1Card.Damage < player2Card.Damage)
                            {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                                winner = "tie";
                            }
                            break;
                        }
                    case "firewater": 
                    {
                            damagePlayer1 = (float)player1Card.Damage / 2;
                            damagePlayer2 = (float)player2Card.Damage * 2;
                            if (damagePlayer1 > damagePlayer2)
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            }
                            else if (damagePlayer1 < damagePlayer2)
                            {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                            }
                            break;
                    }
                    case "firenormal":
                    {
                            damagePlayer1 = (float)player1Card.Damage * 2;
                            damagePlayer2 = (float)player2Card.Damage / 2;
                            if (damagePlayer1 > damagePlayer2)
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            }
                            else if (damagePlayer1 < damagePlayer2)
                            {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                            }
                            break;
                    }
                    case "normalnormal":
                    {
                            if (player1Card.Damage > player2Card.Damage)
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            }
                            else if (player1Card.Damage < player2Card.Damage)
                            {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                                winner = "tie";
                            }
                            break;
                    }
                    case "normalwater":
                    {
                            damagePlayer1 = (float)player1Card.Damage * 2;
                            damagePlayer2 = (float)player2Card.Damage / 2;
                            if (damagePlayer1 > damagePlayer2)
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            }
                            else if (damagePlayer1 < damagePlayer2)
                            {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                            }
                            break;
                    }
                    case "normalfire": 
                    {
                            damagePlayer1 = (float)player1Card.Damage / 2;
                            damagePlayer2 = (float)player2Card.Damage * 2;
                            if (damagePlayer1 > damagePlayer2)
                            {
                                Console.WriteLine("Player 1 " + player1Card.Card_name + " wins.");
                                winner = "player1";
                            }
                            else if (damagePlayer1 < damagePlayer2)
                            {
                                Console.WriteLine("Player 2 " + player2Card.Card_name + " wins.");
                                winner = "player2";
                            }
                            else
                            {
                                Console.WriteLine("It's a draw. Neither player loses their card this round.");
                            }
                            break;
                        }

                }
                
            }

            return winner;
        }


        public string StartBattle(List<DeckPlayer1> player1Cards, List<DeckPlayer2> player2Cards) 
        {
            //BattleLogic battleLogic = new BattleLogic();
            Console.WriteLine("BATTLE START!");

            string winner1 = "";
            string winner2 = "";
            string champion = "";
            Random random = new Random();   // create a new instance of the Random class
            DeckPlayer1 randomCard1 = new DeckPlayer1();
            DeckPlayer2 randomCard2 = new DeckPlayer2();
            //DeckPlayer1 cardToAdd1 = new DeckPlayer1();
            //DeckPlayer2 cardToAdd2 = new DeckPlayer2();

            int rounds = 1;
            while (true) 
            {
                DeckPlayer1 cardToAdd1 = new DeckPlayer1(); // Initialize cardToAdd1 inside the loop
                DeckPlayer2 cardToAdd2 = new DeckPlayer2();

                if (player1Cards.Count == 0)
                {
                    Console.WriteLine("Player 1 has no cards left. The winner is Player 2");
                    champion = "player2";
                    break;
                }
                if (player2Cards.Count == 0)
                {
                    Console.WriteLine("Player 2 has no cards left. The winner is Player 1");
                    champion = "player1";
                    break;
                }

                if (rounds > 50)
                {
                    Console.WriteLine("There have been too many ties. The match will end in a draw");
                    champion = "tie";
                    break;
                }

                Console.WriteLine("Round: " + rounds);
                int index1 = random.Next(player1Cards.Count);    // generate a random index for the deck to draw a random card during battle
                int index2 = random.Next(player2Cards.Count);

                randomCard1 = player1Cards[index1];
                randomCard2 = player2Cards[index2];

                winner1 = CheckInstantWin(randomCard1, randomCard2);

                if(winner1 == "player1") {                                          // for instant wins
                    Console.WriteLine("Round " + rounds + " goes to Player 1");
                    cardToAdd1.AddToDeck1(randomCard2); // add the loser's card to the winner's side, create a new instance
                    player1Cards.Add(cardToAdd1);       // now add the card to the list/deck
                    player2Cards.Remove(randomCard2); // remove the card from player2Cards

                } else if(winner1 == "player2") {                                   //for instant wins            
                    Console.WriteLine("Round " + rounds + " goes to Player 2");
                    cardToAdd2.AddToDeck2(randomCard1); // add the loser's card to the winner's side, create a new instance
                    player2Cards.Add(cardToAdd2);       // now add the card to the list/deck
                    player1Cards.Remove(randomCard1); // remove the card from player2Cards

                } else if(winner1 == "tie") {                                       //if not a instant win then continue
                    winner2 = CheckDamage(randomCard1, randomCard2);
                    if(winner2 == "player1") {
                        Console.WriteLine("Round " + rounds + " goes to Player 1");
                        cardToAdd1.AddToDeck1(randomCard2); // add the loser's card to the winner's side, create a new instance
                        player1Cards.Add(cardToAdd1);       // now add the card to the list/deck
                        player2Cards.Remove(randomCard2); // remove the card from player2Cards

                    } else if(winner2 == "player2") {
                        Console.WriteLine("Round " + rounds + " goes to Player 2");
                        cardToAdd2.AddToDeck2(randomCard1); // add the loser's card to the winner's side, create a new instance
                        player2Cards.Add(cardToAdd2);       // now add the card to the list/deck
                        player1Cards.Remove(randomCard1); // remove the card from player2Cards

                    } else if(winner2 == "tie") {
                        Console.WriteLine("Round " + rounds + " goes to no one. The battle continues.");
                    }

                }

                rounds++;

                Console.WriteLine("\n///////////////////////////////////////Player 1's remaining cards:");
                foreach (var card in player1Cards)
                {
                    Console.WriteLine("Name: " + card.Card_name + ", Damage: " + card.Damage);
                }

                Console.WriteLine("\n///////////////////////////////////////Player 2's remaining cards:");
                foreach (var card in player2Cards)
                {
                    Console.WriteLine("Name: " + card.Card_name + ", Damage: " + card.Damage);
                }
                Console.WriteLine("///////////////////////////////////////");


            }

            return champion;
    
        }
    }
}
