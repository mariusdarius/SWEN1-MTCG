using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Xml.Linq;
using System.Security.Principal;
using MySql.Data.MySqlClient;
using CardGameDemoDB;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Google.Protobuf;
using System.Collections.Concurrent;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Collections;
using Org.BouncyCastle.Crypto;
using MySqlX.XDevAPI;
using System.Transactions;
using System.Xml;
using MySqlX.XDevAPI.Relational;
using NUnit.Framework;

namespace CardGameServer
{
    public class Program
    {
        static List<string> userTokens = new List<string>();    // Initialize an empty list to store tokens for the usernames

        public static void Main(String[] args)
        {
            IPAddress ip = IPAddress.Any;   // Set the IP address and port number for the server
            int port = 10001;

            TcpListener listener = new TcpListener(ip, port);   // Create a new TCP listener and start listening for incoming connections
            listener.Start();
            Console.WriteLine("Server started on " + ip + ":" + port);

            Battle battle = new Battle();   //already start listening for players 1 and 2
            battle.startListening();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();  // Wait for a client to connect
                Console.WriteLine("Client connected: " + client.Client.RemoteEndPoint);

                NetworkStream stream = client.GetStream();  // Start a new thread to handle incoming connections on the clientListener
                Thread clientThread = new Thread(() => HandleClient(client, battle));
                clientThread.Start();

                if (!client.Connected)      //if you do not remove the thread you will not be able to connect to the server again after you first connected and disconnected
                {                           //without it you cannot reconnect with the same client after you just disconnected
                    clientThread.Abort();   // IMPORTANT!!!!
                }

            }
        }

        public static void AddCardToAvailableCardsTable(string cardId, string userName, Database demodb)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM all_cards WHERE card_id = @cardId", demodb.Connection);
            command.Parameters.AddWithValue("@cardId", cardId);
            MySqlDataReader reader = command.ExecuteReader();
            string insertQuery;
            MySqlCommand insertCommand = new MySqlCommand();
            string cardName = null;
            int damage = 0;
            string cardType = null;
            string element = null;
            string species = null;
            string isUsed = null;


            if (reader.Read())
            {
                cardName = reader.GetString(reader.GetOrdinal("card_name"));
                damage = reader.GetInt32(reader.GetOrdinal("damage"));
                cardType = reader.GetString(reader.GetOrdinal("card_type"));
                element = reader.GetString(reader.GetOrdinal("element"));
                species = reader.GetString(reader.GetOrdinal("species"));
                isUsed = "false";

            }
            reader.Close();

            string selectQuery = "SELECT * FROM available_cards WHERE username = @username AND card_id = @cardId";
            MySqlCommand selectCommand = new MySqlCommand(selectQuery, demodb.Connection);
            selectCommand.Parameters.AddWithValue("@username", userName);
            selectCommand.Parameters.AddWithValue("@cardId", cardId);
            MySqlDataReader reader2 = selectCommand.ExecuteReader();
            bool combinationExists = reader2.HasRows;
            reader2.Close();

            if (combinationExists == true)
            {
                Console.WriteLine($"Too bad, {userName} received a duplicate: {cardName} with damage {damage}.");
            }
            else
            {
                Console.WriteLine($"{userName} received: {cardName} with damage {damage}");
                insertQuery = "INSERT INTO available_cards (username, card_id, card_name, damage, card_type, element, species, used) VALUES (@username, @cardId, @cardName, @damage, @cardType, @element, @species, @used)";
                insertCommand = new MySqlCommand(insertQuery, demodb.Connection);
                insertCommand.Parameters.AddWithValue("@username", userName);
                insertCommand.Parameters.AddWithValue("@cardId", cardId);
                insertCommand.Parameters.AddWithValue("@cardName", cardName);
                insertCommand.Parameters.AddWithValue("@damage", damage);
                insertCommand.Parameters.AddWithValue("@cardType", cardType);
                insertCommand.Parameters.AddWithValue("@element", element);
                insertCommand.Parameters.AddWithValue("@species", species);
                insertCommand.Parameters.AddWithValue("@used", isUsed);
                insertCommand.ExecuteNonQuery();
            }

        }
        
        public static void HandleClient(TcpClient client, Battle battle)
        {
            Database demodb = new Database();
            HandleMessages messages = new HandleMessages();

            NetworkStream stream = client.GetStream();  // Get the network stream for the client connection

            while (client.Connected)
            {
                string request = messages.ReceiveMessage(client);   //Receive the curl script command
                string[] requestLines = request.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                string[] requestLineParts = requestLines[0].Split(" ");
                string method = requestLineParts[0];
                string endpoint = requestLineParts[1];
                string protocolVersion = requestLineParts[2];

                string mtcgToken = string.Empty;

                Dictionary<string, string> headers = new Dictionary<string, string>();

                Console.WriteLine("Method: " + method);
                Console.WriteLine("Endpoint: " + endpoint);
                Console.WriteLine("ProtocolVersion: " + protocolVersion);

                for (int i = 1; i < requestLines.Length; i++)
                {
                    string line = requestLines[i];
                    Console.WriteLine("The line is: " + line);

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        break;
                    }

                    int separatorIndex = line.IndexOf(":");
                    if (separatorIndex >= 0)
                    {
                        string headerName = line.Substring(0, separatorIndex).Trim();
                        string headerValue = line.Substring(separatorIndex + 1).Trim();

                        headers.Add(headerName, headerValue);
                    }
                }

                switch (method)
                {
                    case "POST":
                        {
                            try
                            {
                                switch (endpoint)
                                {
                                    case "/users":
                                        {
                                            demodb.Connect();
                                            var account = JsonConvert.DeserializeObject<User>(requestLines[6]);
                                            string checkSql = "SELECT COUNT(*) FROM client WHERE username = @Username";
                                            MySqlCommand command = new MySqlCommand(checkSql, demodb.Connection);
                                            command.Parameters.AddWithValue("@Username", account.Username);

                                            long count = (long)command.ExecuteScalar();

                                            try {
                                                if (count > 0)
                                                {
                                                    Console.WriteLine($"Username {account.Username} already exists in the database.");
                                                    messages.SendMessage(client, "Username already exists in the database\n");
                                                    demodb.Disconnect();
                                                    break;
                                                }
                                                else
                                                {
                                                    string insertSql = "INSERT INTO client (username, password, coins, stats, bio, image) VALUES (@username, @password, 20, 100, \" \", \" \")";
                                                    command = new MySqlCommand(insertSql, demodb.Connection);
                                                    // Set the parameter values for the INSERT statement
                                                    command.Parameters.AddWithValue("@username", account.Username);
                                                    command.Parameters.AddWithValue("@password", account.Password);
                                                    command.ExecuteNonQuery();
                                                    Console.WriteLine($"Username {account.Username} was successfully registered.");
                                                    messages.SendMessage(client, "User was successfully registered.\n");

                                                    string insertScore = "INSERT INTO scoreboard (username, score) VALUES (@user, @userscore)";
                                                    command = new MySqlCommand(insertScore, demodb.Connection);
                                                    command.Parameters.AddWithValue("@user", account.Username);
                                                    command.Parameters.AddWithValue("@userscore", 100);
                                                    command.ExecuteNonQuery();
                                                    Console.WriteLine($"Username {account.Username} was successfully added to the scoreboard.");
                                                    messages.SendMessage(client, "User was successfully added to the scoreboard.\n");

                                                    demodb.Disconnect();

                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"Error: {ex.Message}");
                                                demodb.Disconnect();
                                            }
                                            break;
                                        }
                                    case "/sessions":
                                        {
                                            demodb.Connect();
                                            var account = JsonConvert.DeserializeObject<User>(requestLines[6]);
                                            string requestBody = request.Substring(request.IndexOf("\r\n\r\n") + 4);
                                            string loginSql = "SELECT COUNT(*) FROM client WHERE username = @Username AND password = @Password";
                                            MySqlCommand command = new MySqlCommand(loginSql, demodb.Connection);
                                            command.Parameters.AddWithValue("@username", account.Username);
                                            command.Parameters.AddWithValue("@password", account.Password);
                                            //command.ExecuteNonQuery();

                                            long count = (long)command.ExecuteScalar();

                                            if (count > 0)
                                            {
                                                // When a user logs in, add their username to the list
                                                userTokens.Add($"{account.Username}-mtcgToken");
                                                foreach (string token in userTokens)
                                                {
                                                    Console.WriteLine($"Token: {token}");
                                                }
                                                Console.WriteLine($"Welcome {account.Username}. You are now logged in.");
                                                messages.SendMessageAndParameter(client, "Welcome. You are now logged in", account.Username);
                                                demodb.Disconnect();
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Login failed. Wrong username or password.");
                                                messages.SendMessage(client, "Login failed. Wrong username or password.\n");
                                                demodb.Disconnect();
                                                break;
                                            }
                                        }
                                    case "/packages":
                                        {
                                            string authorizationHeader = requestLines[5];
                                            if (authorizationHeader.StartsWith("Authorization: "))
                                            {
                                                string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                                if (authorizationHeaderParts.Length > 1)
                                                {
                                                    mtcgToken = authorizationHeaderParts[1];
                                                }
                                            }
                                            Console.WriteLine("Token: " + mtcgToken);
                                            if (userTokens.Contains(mtcgToken))
                                            {
                                                Console.WriteLine("Creating packages...");
                                                messages.SendMessage(client, "Creating packages...\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Creating packages failed. You need to login first.");
                                                messages.SendMessage(client, "Creating packages failed. You need to login first.\n");
                                                break;
                                            }

                                            // Define the variables to hold the card IDs outside the switch statement
                                            string firstId = null;
                                            string secondId = null;
                                            string thirdId = null;
                                            string fourthId = null;
                                            string fifthId = null;
                                            int breakCounter = 0;
                                            // Deserialize the JSON data into a list of Card objects
                                            List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(requestLines[7]);  //when deserializing JSON into an object, the property names in the JSON must match the property names in the object's class
                                            foreach (Card card in cards)
                                            {
                                                Console.WriteLine("Id: " + card.Id);
                                                Console.WriteLine("Name: " + card.Name);
                                                Console.WriteLine("Damage: " + card.Damage);
                                                Console.WriteLine();
                                            }

                                            foreach (Card card in cards)
                                            {
                                                if (card.Name.ToLower().Contains("spell"))
                                                {
                                                    card.Cardtype = "spell";
                                                    card.Species = "magic";

                                                    var match = Regex.Match(card.Name.ToLower(), "(fire|water)");
                                                    // If there is a match, assign the element name to card.Element
                                                    if (match.Success)
                                                    {
                                                        card.Element = match.Value;
                                                    }
                                                    else
                                                    {
                                                        card.Element = "normal";
                                                    }
                                                }
                                                else
                                                {
                                                    card.Cardtype = "monster";
                                                }
                                                if (card.Cardtype == "monster")
                                                {
                                                    var match2 = Regex.Match(card.Name.ToLower(), "(goblin|dragon|ork|wizzard|kraken|knight|fireelf)");
                                                    // If there is a match, assign the species name to card.Species
                                                    if (match2.Success)
                                                    {
                                                        card.Species = match2.Value;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("The given species is unknown. Unable to create package.");
                                                        messages.SendMessage(client, "The given species is unknown. Unable to create package.\n");
                                                        breakCounter++;

                                                    }
                                                    match2 = Regex.Match(card.Name.ToLower(), "(fire|water)");
                                                    if (match2.Success)
                                                    {
                                                        card.Element = match2.Value;
                                                    }
                                                    else
                                                    {
                                                        card.Element = "normal";
                                                    }
                                                }

                                            }
                                            if (breakCounter > 0)
                                            {
                                                break;
                                            }
                                            demodb.Connect();
                                            int currentCardNumber = 0;
                                            foreach (Card card in cards)
                                            {
                                                currentCardNumber++;
                                                string checkStorage = "SELECT COUNT(*) FROM all_cards WHERE card_id = @Id";
                                                MySqlCommand command = new MySqlCommand(checkStorage, demodb.Connection);
                                                command.Parameters.AddWithValue("@Id", card.Id);
                                                long count = (long)command.ExecuteScalar();

                                                if (count > 0)
                                                {
                                                    Console.WriteLine("Card with Id {0} already exists in the database.", card.Id);
                                                    messages.SendMessageAndParameter(client, "Card with this Id already exists in the database:", card.Id);
                                                }
                                                else
                                                {
                                                    // card is successfully inserted into the database if it does not already exist
                                                    //Console.WriteLine("Card with Id {0} does not exist in the database.", card.Id);
                                                    string insertQuery = "INSERT INTO all_cards (card_id, card_name, damage, card_type, element, species) VALUES (@Id, @Name, @Damage, @Type, @Element, @Species)";
                                                    command = new MySqlCommand(insertQuery, demodb.Connection);
                                                    command.Parameters.AddWithValue("@Id", card.Id);
                                                    command.Parameters.AddWithValue("@Name", card.Name);
                                                    command.Parameters.AddWithValue("@Type", card.Cardtype);
                                                    command.Parameters.AddWithValue("@Damage", card.Damage);
                                                    command.Parameters.AddWithValue("@Element", card.Element);
                                                    command.Parameters.AddWithValue("@Species", card.Species);
                                                    command.ExecuteNonQuery();
                                                    Console.WriteLine("A new card was created. The card with Id {0} was added to the game.", card.Id);
                                                    messages.SendMessageAndParameter(client, "A new card was created. The card with this Id was added to the game:", card.Id);
                                                }
                                                
                                                switch (currentCardNumber)
                                                {
                                                    case 1:
                                                        {
                                                            firstId = card.Id;
                                                            break;
                                                        }
                                                    case 2:
                                                        {
                                                            secondId = card.Id;
                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            thirdId = card.Id;
                                                            break;
                                                        }
                                                    case 4:
                                                        {
                                                            fourthId = card.Id;
                                                            break;
                                                        }
                                                    case 5:
                                                        {
                                                            fifthId = card.Id;
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Something went wrong.");
                                                            messages.SendMessage(client, "Something went wrong\n");
                                                            break;
                                                        }
                                                }

                                                //Console.WriteLine("Id: {0}, Name: {1}, Damage: {2}, Cardtype: {3}, Element: {4}, Species: {5}", card.Id, card.Name, card.Damage, card.Cardtype, card.Element, card.Species);
                                            }

                                            /*Console.WriteLine("the id is: " + firstId);
                                            Console.WriteLine("the id is: " + secondId);
                                            Console.WriteLine("the id is: " + thirdId);
                                            Console.WriteLine("the id is: " + fourthId);
                                            Console.WriteLine("the id is: " + fifthId);*/

                                            string insertPackageSql = "INSERT INTO packages (card_id1, card_id2, card_id3, card_id4, card_id5) " + "VALUES (@CardId1, @CardId2, @CardId3, @CardId4, @CardId5)";
                                            MySqlCommand command2 = new MySqlCommand(insertPackageSql, demodb.Connection);
                                            command2.Parameters.AddWithValue("@CardId1", firstId);
                                            command2.Parameters.AddWithValue("@CardId2", secondId);
                                            command2.Parameters.AddWithValue("@CardId3", thirdId);
                                            command2.Parameters.AddWithValue("@CardId4", fourthId);
                                            command2.Parameters.AddWithValue("@CardId5", fifthId);
                                            command2.ExecuteNonQuery();
                                            Console.WriteLine("Package has been created.");
                                            messages.SendMessage(client, "Package has been created.\n");
                                            demodb.Disconnect();
                                            break;
                                        }
                                    case "/transactions/packages":
                                        {
                                            string authorizationHeader = requestLines[5];
                                            if (authorizationHeader.StartsWith("Authorization: "))
                                            {
                                                string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                                if (authorizationHeaderParts.Length > 1)
                                                {
                                                    mtcgToken = authorizationHeaderParts[1];
                                                }
                                            }
                                            Console.WriteLine("Token: " + mtcgToken);
                                            if (userTokens.Contains(mtcgToken))
                                            {
                                                Console.WriteLine("Welcome to the card shop.");
                                                messages.SendMessage(client, "Welcome to the card shop.\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("You cannot enter the card shop. You need to login first.");
                                                messages.SendMessage(client, "You cannot enter the card shop. You need to login first.}n");
                                                break;
                                            }

                                            int index = mtcgToken.IndexOf("-mtcgToken");
                                            string userName;
                                            if (index >= 0)
                                            {
                                                userName = mtcgToken.Substring(0, index);
                                                //var account = JsonConvert.DeserializeObject<User>(userName);
                                                //Console.WriteLine(mtcgToken);
                                                //Console.WriteLine("Hello " + userName + ".");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Token could not be found.");
                                                messages.SendMessage(client, "Token could not be found.\n");
                                                break;
                                            }

                                            demodb.Connect();

                                            string checkSql = "SELECT coins FROM client WHERE username = @Username";
                                            MySqlCommand command = new MySqlCommand(checkSql, demodb.Connection);
                                            command.Parameters.AddWithValue("@Username", userName);
                                            int coins;
                                            using (MySqlDataReader reader = command.ExecuteReader())    //'using' statement automatically calls the Dispose method on the MySqlDataReader object
                                            {
                                                if (reader.Read())
                                                {
                                                    coins = reader.GetInt32("Coins");
                                                    //Console.WriteLine("Coins for user {0}: {1}", userName, coins);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("User not found: {0}", userName);
                                                    messages.SendMessage(client, "User not found.\n");
                                                    break;
                                                }
                                            }


                                            //ReaderOptions.Close();

                                            if (coins < 5)
                                            {
                                                Console.WriteLine("Not enough coins.");
                                                messages.SendMessage(client, "Not enough coins.\n");
                                                break;
                                            }

                                            command = new MySqlCommand("SELECT * FROM packages ORDER BY RAND() LIMIT 1", demodb.Connection);
                                            MySqlDataReader reader2 = command.ExecuteReader();

                                            string packageId;
                                            string cardId1;
                                            string cardId2;
                                            string cardId3;
                                            string cardId4;
                                            string cardId5;

                                            if (reader2.Read())
                                            {
                                                packageId = reader2.GetString(reader2.GetOrdinal("package_id"));
                                                cardId1 = reader2.GetString(reader2.GetOrdinal("card_id1"));
                                                cardId2 = reader2.GetString(reader2.GetOrdinal("card_id2"));
                                                cardId3 = reader2.GetString(reader2.GetOrdinal("card_id3"));
                                                cardId4 = reader2.GetString(reader2.GetOrdinal("card_id4"));
                                                cardId5 = reader2.GetString(reader2.GetOrdinal("card_id5"));

                                                /*Console.WriteLine(packageId);
                                                Console.WriteLine(cardId1);
                                                Console.WriteLine(cardId2);
                                                Console.WriteLine(cardId3);
                                                Console.WriteLine(cardId4);
                                                Console.WriteLine(cardId5);*/
                                            }
                                            else
                                            {
                                                Console.WriteLine("Sorry, all the packages have been sold out. Try again later.");
                                                messages.SendMessage(client, "Sorry, all the packages have been sold out. Try again later.\n");
                                                break;
                                            }

                                            reader2.Close();

                                            AddCardToAvailableCardsTable(cardId1, userName, demodb);
                                            AddCardToAvailableCardsTable(cardId2, userName, demodb);
                                            AddCardToAvailableCardsTable(cardId3, userName, demodb);
                                            AddCardToAvailableCardsTable(cardId4, userName, demodb);
                                            AddCardToAvailableCardsTable(cardId5, userName, demodb);

                                            command = new MySqlCommand("SELECT COUNT(*) FROM packages WHERE package_id = @id", demodb.Connection);
                                            command.Parameters.AddWithValue("@id", packageId);
                                            long count = (long)command.ExecuteScalar();

                                            if (count > 0)
                                            {
                                                string deleteQuery = "DELETE FROM packages WHERE package_id = @packageId";
                                                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, demodb.Connection);
                                                deleteCommand.Parameters.AddWithValue("@packageId", packageId);
                                                deleteCommand.ExecuteNonQuery();
                                                Console.WriteLine("You have successfully bought the package.");
                                                messages.SendMessage(client, "You have successfully bought the package.\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("There is no entry with package_id: " + packageId);
                                                messages.SendMessage(client, "There is no entry with this package_id.\n");
                                            }
                                            coins = coins - 5;
                                            string takeCoins = "UPDATE client SET coins = @coins WHERE username = @username";
                                            command = new MySqlCommand(takeCoins, demodb.Connection);
                                            command.Parameters.AddWithValue("@coins", coins);
                                            command.Parameters.AddWithValue("@username", userName);
                                            command.ExecuteNonQuery();
                                            Console.WriteLine("You paid 5 coins");
                                            messages.SendMessage(client, "You paid 5 coins.\n");

                                            demodb.Disconnect();
                                            break;
                                        }
                                    case "/battles":
                                        {
                                            string authorizationHeader = requestLines[4];
                                            Console.WriteLine(requestLines[4]);
                                            if (authorizationHeader.StartsWith("Authorization: "))
                                            {
                                                string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                                if (authorizationHeaderParts.Length > 1)
                                                {
                                                    mtcgToken = authorizationHeaderParts[1];
                                                }
                                            }
                                            Console.WriteLine("Token: " + mtcgToken);
                                            if (userTokens.Contains(mtcgToken))
                                            {
                                                Console.WriteLine("Starting battle...");
                                                messages.SendMessage(client, "Starting battle...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Battle cannot be started. You need to login first.");
                                                messages.SendMessage(client, "Battle cannot be started. You need to login first.\n");
                                                break;
                                            }

                                            int index = mtcgToken.IndexOf("-mtcgToken");
                                            string userName;
                                            if (index >= 0)
                                            {
                                                userName = mtcgToken.Substring(0, index);
                                                //Console.WriteLine(mtcgToken);
                                                //Console.WriteLine("Hello " + userName + ".");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Token could not be found.");
                                                messages.SendMessage(client, "Token could not be found.\n");
                                                break;
                                            }



                                            Thread battleThread = new Thread(battle.startBattle);
                                            //Thread battleThread = new Thread(() => battle.startBattle(client));
                                            battleThread.Start();

                                            // Other code can go here
                                            TcpClient clientBattle = new TcpClient();
                                            clientBattle.Connect(IPAddress.Parse("127.0.0.1"), 1234);
                                            messages.SendMessage(clientBattle, userName);




                                            



                                            break;
                                        }
                                    case "/tradings":
                                        {
                                            break;
                                        }
                                }
                            } catch (Exception ex) {
                                // Handle the exception
                                Console.WriteLine("An error occurred while executing the curl command: " + ex.Message);
                            }

                            break;
                        }
                    case "GET":
                        {
                            if (endpoint != "/cards" && endpoint != "/deck" && endpoint != "/deck?format=plain" && endpoint != "/stats" && endpoint != "/score")
                            {
                                int secondSlashIndex = endpoint.IndexOf('/', endpoint.IndexOf('/') + 1);
                                endpoint = endpoint.Substring(0, secondSlashIndex + 1);
                                //Console.WriteLine(endpoint);
                            }

                            switch (endpoint) {
                                case "/cards": {
                                        try {
                                            string authorizationHeader = requestLines[4];
                                            Console.WriteLine(requestLines[4]);
                                            if (authorizationHeader.StartsWith("Authorization: "))
                                            {
                                                string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                                if (authorizationHeaderParts.Length > 1)
                                                {
                                                    mtcgToken = authorizationHeaderParts[1];
                                                }
                                            }
                                            //Console.WriteLine("Token: " + mtcgToken);
                                            if (userTokens.Contains(mtcgToken))
                                            {
                                                Console.WriteLine("Showing acquired cards...");
                                                messages.SendMessage(client, "Showing acquired cards...\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Acquired cards cannot be shown. You need to login first.");
                                                messages.SendMessage(client, "Acquired cards cannot be shown. You need to login first.\n");
                                                break;
                                            }

                                            int index = mtcgToken.IndexOf("-mtcgToken");
                                            string userName;
                                            if (index >= 0)
                                            {
                                                userName = mtcgToken.Substring(0, index);
                                                //Console.WriteLine(mtcgToken);
                                                //Console.WriteLine("Hello " + userName + ".");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Token could not be found.");
                                                messages.SendMessage(client, "Token could not be found.\n");
                                                break;
                                            }

                                            demodb.Connect();

                                            string showCards = "SELECT card_name, damage FROM available_cards WHERE username = @username";
                                            MySqlCommand command = new MySqlCommand(showCards, demodb.Connection);
                                            command.Parameters.AddWithValue("@username", userName);
                                            command.ExecuteNonQuery();

                                            MySqlDataReader reader = command.ExecuteReader();

                                            if (reader.HasRows)
                                            {
                                                // Loop through the results and print the card names
                                                while (reader.Read())
                                                {
                                                    string cardName = reader.GetString(0);
                                                    int damage = reader.GetInt32(1);
                                                    Console.WriteLine(cardName + ": " + damage);
                                                    messages.SendMessageAndNumber(client, cardName, damage);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("You have no cards. Go buy some in the shop.");
                                                messages.SendMessage(client, "You have no cards. Go buy some in the shop.\n");
                                            }

                                            reader.Close();
                                            demodb.Disconnect();
                                        }
                                        catch (Exception ex)
                                        {
                                            // Handle the exception
                                            Console.WriteLine("An error occurred while executing the curl command: " + ex.Message);
                                        }

                                        break;
                                    }
                                case "/deck": {
                                        try
                                        {
                                            string authorizationHeader = requestLines[4];
                                            Console.WriteLine(requestLines[4]);
                                            if (authorizationHeader.StartsWith("Authorization: "))
                                            {
                                                string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                                if (authorizationHeaderParts.Length > 1)
                                                {
                                                    mtcgToken = authorizationHeaderParts[1];
                                                }
                                            }
                                            Console.WriteLine("Token: " + mtcgToken);
                                            if (userTokens.Contains(mtcgToken))
                                            {
                                                Console.WriteLine("Showing acquired cards...");
                                                messages.SendMessage(client, "Showing acquired cards...\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Acquired cards cannot be shown. You need to login first.");
                                                messages.SendMessage(client, "Acquired cards cannot be shown. You need to login first.\n");
                                                break;
                                            }

                                            int index = mtcgToken.IndexOf("-mtcgToken");
                                            string userName;
                                            if (index >= 0)
                                            {
                                                userName = mtcgToken.Substring(0, index);
                                                //Console.WriteLine(mtcgToken);
                                                //Console.WriteLine("Hello " + userName + ".");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Token could not be found.");
                                                messages.SendMessage(client, "Token could not be found.\n");
                                                break;
                                            }

                                            demodb.Connect();

                                            string checkDeck = "SELECT * FROM deck WHERE username = @username";
                                            MySqlCommand command = new MySqlCommand(checkDeck, demodb.Connection);
                                            command.Parameters.AddWithValue("@username", userName);
                                            command.ExecuteNonQuery();

                                            MySqlDataReader reader = command.ExecuteReader();
                                            string storeCardId1 = "";
                                            string storeCardId2 = "";
                                            string storeCardId3 = "";
                                            string storeCardId4 = "";

                                            if (!reader.HasRows)
                                            {
                                                Console.WriteLine("You have no cards in your deck. Go and edit your deck.");
                                                messages.SendMessage(client, "You have no cards in your deck. Go and edit your deck.\n");
                                                reader.Close();
                                                demodb.Disconnect();
                                                break;

                                            }
                                            reader.Close();

                                            Console.WriteLine("Showing deck...");
                                            messages.SendMessage(client, "Showing deck...\n");
                                            string getCardId = "SELECT card_id1, card_id2, card_id3, card_id4 FROM deck WHERE username = @username";
                                            command = new MySqlCommand(getCardId, demodb.Connection);
                                            command.Parameters.AddWithValue("@username", userName);
                                            command.ExecuteNonQuery();

                                            MySqlDataReader reader2 = command.ExecuteReader();
                                            while (reader2.Read())
                                            {
                                                storeCardId1 = reader2.GetString("card_id1");
                                                storeCardId2 = reader2.GetString("card_id2");
                                                storeCardId3 = reader2.GetString("card_id3");
                                                storeCardId4 = reader2.GetString("card_id4");
                                                //Console.WriteLine("Get card_id from deck: " + storeCardId1);
                                                //Console.WriteLine("Get card_id from deck: " + storeCardId2);
                                                //Console.WriteLine("Get card_id from deck: " + storeCardId3);
                                                //Console.WriteLine("Get card_id from deck: " + storeCardId4);
                                            }
                                            reader2.Close();

                                            string cardName;
                                            int damage;

                                            Console.WriteLine("Your deck consists of: ");
                                            messages.SendMessage(client, "Your deck consists of: \n");
                                            // SQL SELECT statement to retrieve card_name and damage for card_id1, 2,3 and 4

                                            //first card
                                            string selectQuery1 = "SELECT card_name, damage FROM all_cards WHERE card_id = @card_id1";
                                            MySqlCommand command1 = new MySqlCommand(selectQuery1, demodb.Connection);
                                            command1.Parameters.AddWithValue("@card_id1", storeCardId1);
                                            MySqlDataReader reader_1 = command1.ExecuteReader();
                                            if(reader_1.Read()) {
                                                cardName = reader_1.GetString(0);
                                                damage = reader_1.GetInt32(1);
                                                Console.WriteLine("Card ID: {0}, Name: {1}, Damage: {2}", storeCardId1, cardName, damage);
                                                messages.SendMessageAndNumber(client, cardName, damage);
                                            }
                                            reader_1.Close();

                                            //second card
                                            string selectQuery2 = "SELECT card_name, damage FROM all_cards WHERE card_id = @card_id2";
                                            MySqlCommand command2 = new MySqlCommand(selectQuery2, demodb.Connection);
                                            command2.Parameters.AddWithValue("@card_id2", storeCardId2);
                                            MySqlDataReader reader_2 = command2.ExecuteReader();
                                            if (reader_2.Read())
                                            {
                                                cardName = reader_2.GetString(0);
                                                damage = reader_2.GetInt32(1);
                                                Console.WriteLine("Card ID: {0}, Name: {1}, Damage: {2}", storeCardId2, cardName, damage);
                                                messages.SendMessageAndNumber(client, cardName, damage);
                                            }
                                            reader_2.Close();

                                            //third card
                                            string selectQuery3 = "SELECT card_name, damage FROM all_cards WHERE card_id = @card_id3";
                                            MySqlCommand command3 = new MySqlCommand(selectQuery3, demodb.Connection);
                                            command3.Parameters.AddWithValue("@card_id3", storeCardId3);
                                            MySqlDataReader reader_3 = command3.ExecuteReader();
                                            if (reader_3.Read())
                                            {
                                                cardName = reader_3.GetString(0);
                                                damage = reader_3.GetInt32(1);
                                                Console.WriteLine("Card ID: {0}, Name: {1}, Damage: {2}", storeCardId3, cardName, damage);
                                                messages.SendMessageAndNumber(client, cardName, damage);
                                            }
                                            reader_3.Close();

                                            //fourth card
                                            string selectQuery4 = "SELECT card_name, damage FROM all_cards WHERE card_id = @card_id4";
                                            MySqlCommand command4 = new MySqlCommand(selectQuery4, demodb.Connection);
                                            command4.Parameters.AddWithValue("@card_id4", storeCardId4);
                                            MySqlDataReader reader_4 = command4.ExecuteReader();
                                            if (reader_4.Read())
                                            {
                                                cardName = reader_4.GetString(0);
                                                damage = reader_4.GetInt32(1);
                                                Console.WriteLine("Card ID: {0}, Name: {1}, Damage: {2}", storeCardId4, cardName, damage);
                                                messages.SendMessageAndNumber(client, cardName, damage);
                                            }
                                            reader_4.Close();
                                            demodb.Disconnect();
                                        }
                                        catch (Exception ex)
                                        {
                                            // Handle the exception
                                            Console.WriteLine("An error occurred while executing the curl command: " + ex.Message);
                                            demodb.Disconnect();
                                        }

                                        break;
                                    }
                                case "/deck?format=plain": {
                                        try
                                        {
                                            string authorizationHeader = requestLines[4];
                                            Console.WriteLine(requestLines[4]);
                                            if (authorizationHeader.StartsWith("Authorization: "))
                                            {
                                                string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                                if (authorizationHeaderParts.Length > 1)
                                                {
                                                    mtcgToken = authorizationHeaderParts[1];
                                                }
                                            }
                                            //Console.WriteLine("Token: " + mtcgToken);
                                            if (userTokens.Contains(mtcgToken))
                                            {
                                                Console.WriteLine("Showing acquired cards...");
                                                messages.SendMessage(client, "Showing acquired cards...\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Acquired cards cannot be shown. You need to login first.");
                                                messages.SendMessage(client, "Acquired cards cannot be shown. You need to login first.\n");
                                                break;
                                            }

                                            int index = mtcgToken.IndexOf("-mtcgToken");
                                            string userName;
                                            if (index >= 0)
                                            {
                                                userName = mtcgToken.Substring(0, index);
                                                //Console.WriteLine(mtcgToken);
                                                //Console.WriteLine("Hello " + userName + ".");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Token could not be found.");
                                                messages.SendMessage(client, "Token could not be found.\n");
                                                break;
                                            }

                                            demodb.Connect();

                                            string checkDeck = "SELECT * FROM deck WHERE username = @username";
                                            MySqlCommand command = new MySqlCommand(checkDeck, demodb.Connection);
                                            command.Parameters.AddWithValue("@username", userName);
                                            command.ExecuteNonQuery();

                                            MySqlDataReader reader = command.ExecuteReader();
                                            string storeCardId1 = "";
                                            string storeCardId2 = "";
                                            string storeCardId3 = "";
                                            string storeCardId4 = "";

                                            if (!reader.HasRows)
                                            {
                                                Console.WriteLine("You have no cards in your deck. Go and edit your deck.");
                                                messages.SendMessage(client, "You have no cards in your deck. Go and edit your deck.\n");
                                                reader.Close();
                                                demodb.Disconnect();
                                                break;

                                            }
                                            reader.Close();

                                            Console.WriteLine("Showing deck...");
                                            messages.SendMessage(client, "Showing deck...\n");
                                            string getCardId = "SELECT card_id1, card_id2, card_id3, card_id4 FROM deck WHERE username = @username";
                                            command = new MySqlCommand(getCardId, demodb.Connection);
                                            command.Parameters.AddWithValue("@username", userName);
                                            command.ExecuteNonQuery();

                                            MySqlDataReader reader2 = command.ExecuteReader();
                                            while (reader2.Read())
                                            {
                                                storeCardId1 = reader2.GetString("card_id1");
                                                storeCardId2 = reader2.GetString("card_id2");
                                                storeCardId3 = reader2.GetString("card_id3");
                                                storeCardId4 = reader2.GetString("card_id4");
                                                /*Console.WriteLine("Get card_id from deck: " + storeCardId1);
                                                Console.WriteLine("Get card_id from deck: " + storeCardId2);
                                                Console.WriteLine("Get card_id from deck: " + storeCardId3);
                                                Console.WriteLine("Get card_id from deck: " + storeCardId4);*/
                                            }
                                            reader2.Close();

                                            string cardName;
                                            int damage;

                                            Console.WriteLine("Your deck consist of: ");
                                            messages.SendMessage(client, "Your deck consists of: \n");
                                            // SQL SELECT statement to retrieve card_name and damage for card_id1, 2,3 and 4

                                            //first card
                                            string selectQuery1 = "SELECT card_name, damage FROM all_cards WHERE card_id = @card_id1";
                                            MySqlCommand command1 = new MySqlCommand(selectQuery1, demodb.Connection);
                                            command1.Parameters.AddWithValue("@card_id1", storeCardId1);
                                            MySqlDataReader reader_1 = command1.ExecuteReader();
                                            if (reader_1.Read())
                                            {
                                                cardName = reader_1.GetString(0);
                                                damage = reader_1.GetInt32(1);
                                                Console.WriteLine("Card ID: {0}, Name: {1}, Damage: {2}", storeCardId1, cardName, damage);
                                                messages.SendMessageAndNumber(client, cardName, damage);
                                            }
                                            reader_1.Close();

                                            //second card
                                            string selectQuery2 = "SELECT card_name, damage FROM all_cards WHERE card_id = @card_id2";
                                            MySqlCommand command2 = new MySqlCommand(selectQuery2, demodb.Connection);
                                            command2.Parameters.AddWithValue("@card_id2", storeCardId2);
                                            MySqlDataReader reader_2 = command2.ExecuteReader();
                                            if (reader_2.Read())
                                            {
                                                cardName = reader_2.GetString(0);
                                                damage = reader_2.GetInt32(1);
                                                Console.WriteLine("Card ID: {0}, Name: {1}, Damage: {2}", storeCardId2, cardName, damage);
                                                messages.SendMessageAndNumber(client, cardName, damage);
                                            }
                                            reader_2.Close();

                                            //third card
                                            string selectQuery3 = "SELECT card_name, damage FROM all_cards WHERE card_id = @card_id3";
                                            MySqlCommand command3 = new MySqlCommand(selectQuery3, demodb.Connection);
                                            command3.Parameters.AddWithValue("@card_id3", storeCardId3);
                                            MySqlDataReader reader_3 = command3.ExecuteReader();
                                            if (reader_3.Read())
                                            {
                                                cardName = reader_3.GetString(0);
                                                damage = reader_3.GetInt32(1);
                                                Console.WriteLine("Card ID: {0}, Name: {1}, Damage: {2}", storeCardId3, cardName, damage);
                                                messages.SendMessageAndNumber(client, cardName, damage);
                                            }
                                            reader_3.Close();

                                            //fourth card
                                            string selectQuery4 = "SELECT card_name, damage FROM all_cards WHERE card_id = @card_id4";
                                            MySqlCommand command4 = new MySqlCommand(selectQuery4, demodb.Connection);
                                            command4.Parameters.AddWithValue("@card_id4", storeCardId4);
                                            MySqlDataReader reader_4 = command4.ExecuteReader();
                                            if (reader_4.Read())
                                            {
                                                cardName = reader_4.GetString(0);
                                                damage = reader_4.GetInt32(1);
                                                Console.WriteLine("Card ID: {0}, Name: {1}, Damage: {2}", storeCardId4, cardName, damage);
                                                messages.SendMessageAndNumber(client, cardName, damage);
                                            }
                                            reader_4.Close();
                                            demodb.Disconnect();
                                        }
                                        catch (Exception ex)
                                        {
                                            // Handle the exception
                                            Console.WriteLine("An error occurred while executing the curl command: " + ex.Message);
                                            demodb.Disconnect();
                                        }
                                        break;
                                }
                                case "/users/": {
                                        string authorizationHeader = requestLines[4];
                                        Console.WriteLine(requestLines[4]);
                                        if (authorizationHeader.StartsWith("Authorization: "))
                                        {
                                            string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                            if (authorizationHeaderParts.Length > 1)
                                            {
                                                mtcgToken = authorizationHeaderParts[1];
                                            }
                                        }
                                        //Console.WriteLine("Token: " + mtcgToken);
                                        if (userTokens.Contains(mtcgToken))
                                        {
                                            Console.WriteLine("Loading...");
                                            messages.SendMessage(client, "Loading...\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("User data cannot be shown. You need to login first.");
                                            messages.SendMessage(client, "User data cannot be shown. You need to login first.\n");
                                            break;
                                        }

                                        int index = mtcgToken.IndexOf("-mtcgToken");
                                        string userName;
                                        if (index >= 0)
                                        {
                                            userName = mtcgToken.Substring(0, index);
                                            //Console.WriteLine(mtcgToken);
                                            //Console.WriteLine("Hello " + userName + ".");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Token could not be found.");
                                            messages.SendMessage(client, "Token could not be found.\n");
                                            break;
                                        }

                                        demodb.Connect();

                                        string bio = "";
                                        string image = "";

                                        string selectBioImage = "SELECT bio, image FROM client WHERE username = @username";
                                        
                                        MySqlCommand command = new MySqlCommand(selectBioImage, demodb.Connection);
                                        command.Parameters.AddWithValue("@username", userName);

                                        MySqlDataReader reader = command.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            bio = reader.GetString(0);
                                            image = reader.GetString(1);
                                        }
                                        reader.Close();
                                        messages.SendMessage(client, "User data has been modified.\n");
                                        //Console.WriteLine("Name: " + userName + ", Bio: " + bio + ", Image: " + image);
                                        demodb.Disconnect();
                                        break;
                                }
                                case "/stats": {
                                        string authorizationHeader = requestLines[4];
                                        Console.WriteLine(requestLines[4]);
                                        if (authorizationHeader.StartsWith("Authorization: "))
                                        {
                                            string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                            if (authorizationHeaderParts.Length > 1)
                                            {
                                                mtcgToken = authorizationHeaderParts[1];
                                            }
                                        }
                                        //Console.WriteLine("Token: " + mtcgToken);
                                        if (userTokens.Contains(mtcgToken))
                                        {
                                            Console.WriteLine("Loading stats...");
                                            messages.SendMessage(client, "Loading stats...\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Stats cannot be shown. You need to login first.");
                                            messages.SendMessage(client, "Stats cannot be shown. You need to login first.\n");
                                            break;
                                        }

                                        int index = mtcgToken.IndexOf("-mtcgToken");
                                        string userName;
                                        if (index >= 0)
                                        {
                                            userName = mtcgToken.Substring(0, index);
                                            //Console.WriteLine(mtcgToken);
                                            //Console.WriteLine("Hello " + userName + ".");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Token could not be found.");
                                            messages.SendMessage(client, "Token could not be found.\n");
                                            break;
                                        }

                                        demodb.Connect();


                                        string selectStats = "SELECT username, stats FROM client WHERE username = @username";
                                        
                                        MySqlCommand command = new MySqlCommand(selectStats, demodb.Connection);
                                        command.Parameters.AddWithValue("@username", userName);

                                        MySqlDataReader reader = command.ExecuteReader();
                                        if (reader.HasRows)
                                        {
                                            while (reader.Read())
                                            {
                                                string username = reader.GetString(0);
                                                int stats = reader.GetInt32(1);

                                                Console.WriteLine("Stats: " + stats + " " + username);
                                                messages.SendMessageAndNumber(client, username, stats);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No data found.");
                                            messages.SendMessage(client, "No data found.\n");
                                        }

                                        reader.Close();
                                        demodb.Disconnect();

                                        break;
                                }
                                case "/score": {
                                        demodb.Connect();

                                        string selectScore = "SELECT username, score FROM scoreboard ORDER BY score DESC";
                                        MySqlCommand command = new MySqlCommand(selectScore, demodb.Connection);

                                        MySqlDataReader reader = command.ExecuteReader();
                                        Console.WriteLine("Scoreboard:");
                                        messages.SendMessage(client, "Scoreboard:\n");
                                        while (reader.Read())
                                        {
                                            string username = reader.GetString(0);
                                            int score = reader.GetInt32(1);
                                            Console.WriteLine(username + ": " + score);
                                            messages.SendMessageAndNumber(client, username, score);
                                        }

                                        reader.Close();
                                        demodb.Disconnect();

                                        break;
                                }
                            }
                            break;
                        }
                    case "PUT":
                        {
                            try
                            {
                                if (endpoint == "/deck")
                                {
                                    string authorizationHeader = requestLines[5];
                                    Console.WriteLine(requestLines[5]);
                                    if (authorizationHeader.StartsWith("Authorization: "))
                                    {
                                        string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                        if (authorizationHeaderParts.Length > 1)
                                        {
                                            mtcgToken = authorizationHeaderParts[1];
                                        }
                                    }
                                    //Console.WriteLine("Token: " + mtcgToken);
                                    if (userTokens.Contains(mtcgToken))
                                    {
                                        Console.WriteLine("Editing deck...");
                                        messages.SendMessage(client, "Editing deck.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Editing deck failed. You need to login first.");
                                        messages.SendMessage(client, "Editing deck failed. You need to login first.\n");
                                        break;
                                    }

                                    int index = mtcgToken.IndexOf("-mtcgToken");
                                    string userName;
                                    if (index >= 0)
                                    {
                                        userName = mtcgToken.Substring(0, index);
                                        //Console.WriteLine(mtcgToken);
                                        //Console.WriteLine("Hello " + userName + ".");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Token could not be found.");
                                        messages.SendMessage(client, "Token could not be found.\n");
                                        break;
                                    }
                                    demodb.Connect();

                                    string[] ids = requestLines[7].Split(',');
                                    string[] validIds = new string[4];
                                    int validCount = 0;

                                    string id1;
                                    string id2;
                                    string id3;
                                    string id4;

                                    if (ids.Length == 4)
                                    {
                                        id1 = ids[0].Trim(new char[] { '[', ' ', '"' });
                                        id2 = ids[1].Trim(new char[] { ' ', '"' });
                                        id3 = ids[2].Trim(new char[] { ' ', '"' });
                                        id4 = ids[3].Trim(new char[] { ' ', '"', ']' });
                                        Console.WriteLine(id1);
                                        Console.WriteLine(id2);
                                        Console.WriteLine(id3);
                                        Console.WriteLine(id4);

                                        foreach (string id in ids)
                                        {
                                            string trimmedId = id.Trim(new char[] { ' ', '"', '[', ']' });
                                            string query = "SELECT COUNT(*) FROM available_cards WHERE username = @username AND card_id = @id";
                                            using (MySqlCommand command1 = new MySqlCommand(query, demodb.Connection))
                                            {
                                                command1.Parameters.AddWithValue("@username", userName);
                                                command1.Parameters.AddWithValue("@id", trimmedId);
                                                long count = (long)command1.ExecuteScalar();
                                                if (count > 0)
                                                {
                                                    validIds[validCount] = trimmedId;
                                                    validCount++;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Editing deck failed. Check your available cards or input.");
                                        messages.SendMessage(client, "Editing deck failed. Check your available cards or input.\n");
                                        break;
                                    }
                                    
                                    if (validCount == 4)
                                    {
                                        messages.SendMessage(client, "Valid.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to insert cards.");
                                        messages.SendMessage(client, "Failed to insert cards.\n");
                                        break;
                                    }

                                    bool deckEmpty;
                                    string checkDeck = "SELECT * FROM deck WHERE username = @username";
                                    using (MySqlCommand command = new MySqlCommand(checkDeck, demodb.Connection))
                                    {
                                        command.Parameters.AddWithValue("@username", userName);
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            deckEmpty = !reader.HasRows;
                                            
                                        }
                                    }


                                    if (deckEmpty)
                                    {
                                        string insertIntoDeck = "INSERT INTO deck (username, card_id1, card_id2, card_id3, card_id4) VALUES (@username, @id1, @id2, @id3, @id4)";
                                        using (MySqlCommand command2 = new MySqlCommand(insertIntoDeck, demodb.Connection))
                                        {
                                            command2.Parameters.AddWithValue("@username", userName);
                                            command2.Parameters.AddWithValue("@id1", validIds[0]);
                                            command2.Parameters.AddWithValue("@id2", validIds[1]);
                                            command2.Parameters.AddWithValue("@id3", validIds[2]);
                                            command2.Parameters.AddWithValue("@id4", validIds[3]);
                                            int rowsAffected = command2.ExecuteNonQuery();
                                            if (rowsAffected == 1)
                                            {
                                                Console.WriteLine("Deck was successfully edited.");
                                                messages.SendMessage(client, "Deck was successfully edited.\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Editing deck failed.");
                                                messages.SendMessage(client, "Editing deck failed.\n");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string insertIntoDeck = "UPDATE deck SET card_id1 = @id1, card_id2 = @id2, card_id3 = @id3, card_id4 = @id4 WHERE username = @username";
                                        using (MySqlCommand command2 = new MySqlCommand(insertIntoDeck, demodb.Connection))
                                        {
                                            command2.Parameters.AddWithValue("@username", userName);
                                            command2.Parameters.AddWithValue("@id1", validIds[0]);
                                            command2.Parameters.AddWithValue("@id2", validIds[1]);
                                            command2.Parameters.AddWithValue("@id3", validIds[2]);
                                            command2.Parameters.AddWithValue("@id4", validIds[3]);
                                            int rowsAffected = command2.ExecuteNonQuery();
                                            if (rowsAffected == 1)
                                            {
                                                Console.WriteLine("Deck was successfully edited.");
                                                messages.SendMessage(client, "Deck was successfully edited.\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Editing deck failed.");
                                                messages.SendMessage(client, "Editing deck failed.\n");
                                            }
                                        }
                                    }

                                    demodb.Disconnect();
                                }
                                else
                                {
                                    string authorizationHeader = requestLines[5];
                                    Console.WriteLine(requestLines[5]);
                                    if (authorizationHeader.StartsWith("Authorization: "))
                                    {
                                        string[] authorizationHeaderParts = authorizationHeader.Substring(20).Split(' ');
                                        if (authorizationHeaderParts.Length > 1)
                                        {
                                            mtcgToken = authorizationHeaderParts[1];
                                        }
                                    }
                                    //Console.WriteLine("Token: " + mtcgToken);
                                    if (userTokens.Contains(mtcgToken))
                                    {
                                        Console.WriteLine("Editing user data...");
                                        messages.SendMessage(client, "Editing user data failed.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Editing user data failed. You need to login first.");
                                        messages.SendMessage(client, "Editing user data failed. You need to login first.\n");
                                        break;
                                    }

                                    int index = mtcgToken.IndexOf("-mtcgToken");
                                    string userName;
                                    if (index >= 0)
                                    {
                                        userName = mtcgToken.Substring(0, index);
                                        //Console.WriteLine(mtcgToken);
                                        //Console.WriteLine("Hello " + userName + ".");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Token could not be found.");
                                        messages.SendMessage(client, "Token could not be found.\n");
                                        break;
                                    }
                                    demodb.Connect();


                                    var account = JsonConvert.DeserializeObject<User>(requestLines[7]);
                                    
                                    string updateDataSql = "UPDATE client SET username = @Name, bio = @bio, image = @image WHERE username = @userName";

                                    // Create a MySqlCommand object and set its parameters
                                    using MySqlCommand command = new MySqlCommand(updateDataSql, demodb.Connection);
                                    command.Parameters.AddWithValue("@Name", account.Name);
                                    command.Parameters.AddWithValue("@bio", account.Bio);
                                    command.Parameters.AddWithValue("@image", account.Image);
                                    command.Parameters.AddWithValue("@userName", userName);
                                    command.ExecuteNonQuery();

                                    messages.SendMessage(client, "User data was modified.\n");

                                    demodb.Disconnect();
                                    break;
                                   
                                }
                            } catch (Exception ex) {
                                Console.WriteLine("An error occurred while executing the curl command: " + ex.Message);
                            }
                            break;
                        }
                    case "DELETE":
                        {

                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Method not found.");
                            messages.SendMessage(client, "Method not found.");
                            break;
                        }
                }

                client.Close();
            }
        }
    


    }
}
