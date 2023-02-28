using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameDemoDB
{
    public class User       // used when 'var account = JsonConvert.DeserializeObject<User>(requestLines[6]);'
    {                       //is used to store data from the user input temporarily in a User object, eg. for registration or login
        string _username;
        string _password;
        int _coins;
        int _stats;
        string _bio;
        string _image;
        string _name;
        public string Username { get; set; }
        public string Password { get; set; }
        public int Coins { get; set; }
        public int Stats { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
    }
}
