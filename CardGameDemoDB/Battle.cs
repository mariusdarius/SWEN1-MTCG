using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Reflection.PortableExecutable;

namespace CardGameDemoDB
{
    public class Battle
    {
        private TcpListener listener = null;
        private int port = 1234;
        private string player1;
        private string player2;


        public string Player1{ get{ return player1; } set{ player1 = value; } }
        public string Player2{ get{ return player2;} set{ player2 = value; } }
        public int Port{ get{ return port; } set{ port = value; } }
        public void TcpServer(int port)
        {
            this.port = port;
        }

        public void startListening() {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
        }

        public void startBattle()
        {
            Database demodb= new Database();
            HandleMessages message = new HandleMessages();
            BattleLogic battleLogic = new BattleLogic();

            TcpClient client1 = listener.AcceptTcpClient();
            Console.WriteLine("Player 1 connected!");
            //message.SendMessage(client, "Player 1 connected!");
            Player1 = message.ReceiveMessage(client1);
            Console.WriteLine("Welcome " + player1);
            //message.SendMessage(client, "Welcome player 1.");

            Console.WriteLine("Waiting for second player...");

            TcpClient client2 = listener.AcceptTcpClient();
            Console.WriteLine("Player 2 connected!");
            //message.SendMessage(client, "Player 1 connected!");
            Player2 = message.ReceiveMessage(client2);
            Console.WriteLine("Welcome " + player2);
            //message.SendMessage(client1, "Welcome player 2.");

            demodb.Connect();

            string selectCardsFromDeck = "SELECT card_id1, card_id2, card_id3, card_id4 FROM deck WHERE username=@username";
            MySqlCommand command = new MySqlCommand(selectCardsFromDeck, demodb.Connection);
            command.Parameters.AddWithValue("@username", player1);

            string p1card_id1 = "";
            string p1card_id2 = "";
            string p1card_id3 = "";
            string p1card_id4 = "";

            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read()) {
                p1card_id1 = reader.GetString(0);
                p1card_id2 = reader.GetString(1);
                p1card_id3 = reader.GetString(2);
                p1card_id4 = reader.GetString(3);
            }
            reader.Close();

            command = new MySqlCommand(selectCardsFromDeck, demodb.Connection);
            command.Parameters.AddWithValue("@username", player2);

            string p2card_id1 = "";
            string p2card_id2 = "";
            string p2card_id3 = "";
            string p2card_id4 = "";

            reader = command.ExecuteReader();
            if (reader.Read())
            {
                p2card_id1 = reader.GetString(0);
                p2card_id2 = reader.GetString(1);
                p2card_id3 = reader.GetString(2);
                p2card_id4 = reader.GetString(3);
            }
            reader.Close();
            

            DeckPlayer1 P1card1 = new DeckPlayer1();
            P1card1.GetCardInfo(P1card1, p1card_id1, demodb);
            DeckPlayer1 P1card2 = new DeckPlayer1();
            P1card2.GetCardInfo(P1card2, p1card_id2, demodb);
            DeckPlayer1 P1card3 = new DeckPlayer1();
            P1card3.GetCardInfo(P1card3, p1card_id3, demodb);
            DeckPlayer1 P1card4 = new DeckPlayer1();
            P1card4.GetCardInfo(P1card4, p1card_id4, demodb);

            DeckPlayer2 P2card1 = new DeckPlayer2();
            P2card1.GetCardInfo(P2card1, p2card_id1, demodb);
            DeckPlayer2 P2card2 = new DeckPlayer2();
            P2card2.GetCardInfo(P2card2, p2card_id2, demodb);
            DeckPlayer2 P2card3 = new DeckPlayer2();
            P2card3.GetCardInfo(P2card3, p2card_id3, demodb);
            DeckPlayer2 P2card4 = new DeckPlayer2();
            P2card4.GetCardInfo(P2card4, p2card_id4, demodb);

            List<DeckPlayer1> player1Cards = new List<DeckPlayer1> { P1card1, P1card2, P1card3, P1card4 };
            List<DeckPlayer2> player2Cards = new List<DeckPlayer2> { P2card1, P2card2, P2card3, P2card4 };

            string winner = battleLogic.StartBattle(player1Cards, player2Cards);
            if(winner == "player1") {
                Console.WriteLine("THE WINNER IS " + player1 + "!!! CONGRATULATIONS!!!");
                message.SendMessageAndParameter(client1, "THE WINNER IS", player1);
            } else if(winner == "player2") {
                Console.WriteLine("THE WINNER IS " + player2 + "!!! CONGRATULATIONS!!!");
                message.SendMessageAndParameter(client2, "THE WINNER IS", player2);
            }
            else if(winner == "tie") {
                Console.WriteLine("There is no winner. It's a draw.");
                message.SendMessage(client1, "There is no winner. It's a draw.");

            }
            else {
                Console.WriteLine("There was an error during battle. Returning to menu...");
                message.SendMessage(client1, "There is no winner. It's a draw.");
                return;
            }

            int stats1 = 0;
            int stats2 = 0;

            string getStats = "SELECT stats FROM client WHERE username = @player1";
            command = new MySqlCommand(getStats, demodb.Connection);
            command.Parameters.AddWithValue("@player1", player1);

            reader = command.ExecuteReader();
            if (reader.Read())
            {
                stats1 = reader.GetInt32(0);
            }
            reader.Close();

            getStats = "SELECT stats FROM client WHERE username = @player2";
            command = new MySqlCommand(getStats, demodb.Connection);
            command.Parameters.AddWithValue("@player2", player2);

            reader = command.ExecuteReader();
            if (reader.Read())
            {
                stats2 = reader.GetInt32(0);
            }
            reader.Close();

            if(winner == "player1") 
            {
                stats1 += 3;
                stats2 -= 5;   
            } 
            else if(winner == "player2") {
                stats1 -= 5;
                stats2 += 3;
            }

            command = new MySqlCommand("UPDATE client SET stats = @stats WHERE username = @player1", demodb.Connection);
            command.Parameters.AddWithValue("@stats", stats1);
            command.Parameters.AddWithValue("@player1", player1);
            command.ExecuteNonQuery();

            command = new MySqlCommand("UPDATE client SET stats = @stats WHERE username = @player1", demodb.Connection);
            command.Parameters.AddWithValue("@stats", stats2);
            command.Parameters.AddWithValue("@player1", player2);
            command.ExecuteNonQuery();
            Console.WriteLine("Scoreboard has been updated.");
            message.SendMessage(client1, "Scoreboard has been updated.");

            command = new MySqlCommand("UPDATE scoreboard SET score = @stats WHERE username = @player1", demodb.Connection);
            command.Parameters.AddWithValue("@stats", stats1);
            command.Parameters.AddWithValue("@player1", player1);
            command.ExecuteNonQuery();

            command = new MySqlCommand("UPDATE scoreboard SET score = @stats WHERE username = @player2", demodb.Connection);
            command.Parameters.AddWithValue("@stats", stats2);
            command.Parameters.AddWithValue("@player2", player2);
            command.ExecuteNonQuery();


            demodb.Disconnect();


        }


    }
}
