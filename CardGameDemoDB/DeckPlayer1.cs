using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CardGameDemoDB
{
    public class DeckPlayer1
    {
        string username;
        string card_id;
        string card_name;
        int damage;
        string card_type;
        string element;
        string species;

        public string Username { get { return username; } set { username = value; } }
        public string Card_id { get { return card_id; } set { card_id = value; } }
        public string Card_name { get { return card_name; } set { card_name = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
        public string Card_type { get { return card_type; } set { card_type = value; } }
        public string Element { get { return element; } set { element = value; } }
        public string Species { get { return species; } set { species = value; } }

        public DeckPlayer1 AddToDeck1(DeckPlayer2 card)
        {
            //DeckPlayer1 newCard= new DeckPlayer1();
            this.card_id = card.Card_id;
            this.card_name = card.Card_name;
            this.damage = card.Damage;
            this.card_type = card.Card_type;
            this.element = card.Element;
            this.species = card.Species;
            return this;
        }

            public void GetCardInfo(DeckPlayer1 deckPlayer1, string id, Database demodb)
        {

            deckPlayer1.Card_id = id;

            string getCard = "SELECT card_name, damage, card_type, element, species FROM all_cards WHERE card_id = @id";
            MySqlCommand command = new MySqlCommand(getCard, demodb.Connection);
            command.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                // Store the values of each column in separate variables
                deckPlayer1.Card_name = reader.GetString(0);
                deckPlayer1.Damage = reader.GetInt32(1);
                deckPlayer1.Card_type = reader.GetString(2);
                deckPlayer1.Element = reader.GetString(3);
                deckPlayer1.Species = reader.GetString(4);

            }
            reader.Close();

            Console.WriteLine("Id: " + deckPlayer1.Card_id);
            Console.WriteLine("Cardname: " + deckPlayer1.Card_name);
            Console.WriteLine("Damage: " + deckPlayer1.Damage);
            Console.WriteLine("Card type: " + deckPlayer1.Card_type);
            Console.WriteLine("Element: " + deckPlayer1.Element);
            Console.WriteLine("Species: " + deckPlayer1.Species);

        }
    }
}
