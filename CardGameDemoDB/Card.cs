using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameDemoDB
{
    public class Card
    {
        string _id;
        string _name;
        float _damage;
        string _cardtype;
        string _element;
        string _species;
        public string Id { get; set; }
        public string Name { get; set; }
        public float Damage { get; set; }
        public string Cardtype { get; set; }
        public string Element { get; set; }
        public string Species { get; set; }
    }
}
