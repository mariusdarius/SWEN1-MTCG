//using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardGameDemoDB;

namespace UnitTest
{

    [TestFixture]

    public class Tests
    {

        //[Test]

        /*public void Test1()
        {
            Assert.Pass();
        }*/

        /*[Test]



        public void TestCalculator()
        {
            // Arrange
            int x = 2;
            int y = 3;

            // Act  
            int result = CardGameServer.Program.Calculator(x, y);

            // Assert
            Assert.AreEqual(5, result);
        }*/


        //[Test]
        /*public void TestStartListening()
        {
            TcpListener listener = null;
            int port = 1234;
            string player1;
            string player2;


            // Arrange
            Battle battle = new Battle();
            int expectedPort = 1234;
            string expectedMessage = "Waiting for connections...";

            // Act
            battle.startListening();

            // Assert
            //Assert.IsNotNull(battle.Player1);
            //Assert.IsNotNull(battle.Player2);
            Assert.AreEqual(expectedPort, battle.Port);
            Assert.AreEqual(expectedMessage, Console.ReadLine());

        }*/

        [Test]
        public void TestConnect()
        {
            // Arrange
            Database database = new Database();

            // Act
            database.Connect();

            // Assert
            Assert.AreEqual(System.Data.ConnectionState.Open, database.Connection.State);
        }

        [Test]
        public void TestDisconnect()
        {
            // Arrange
            Database database = new Database();

            // Act
            database.Connect();
            database.Disconnect();

            // Assert
            Assert.AreEqual(System.Data.ConnectionState.Closed, database.Connection.State);
        }

        //Testing battle

        [Test]
        public void TestCheckInstantWinGoblinDragon()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "goblin", Card_name = "WaterGoblin" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "dragon", Card_name = "Dragon" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinDragonGoblin()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "dragon", Card_name = "Dragon" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "goblin", Card_name = "WaterGoblin" };
            string expected = "player1";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void TestCheckInstantWinDragonWizzard() {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "dragon", Card_name = "Dragon" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "wizzard", Card_name = "Wizzard" };
            string expected = "tie";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinOrkWizzard()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "ork", Card_name = "Ork" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "wizzard", Card_name = "Wizzard" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinWizzardOrk()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "wizzard", Card_name = "Wizzard" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "ork", Card_name = "Ork" };
            string expected = "player1";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinKnightWaterSpell()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "knight", Card_name = "Knight", Element = "normal" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "magic", Card_name = "WaterSpell", Element = "water" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinWaterSpellKnight()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "magic", Card_name = "WaterSpell", Element = "water" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "knight", Card_name = "Knight", Element = "normal" };
            string expected = "player1";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinFireSpellKnight()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "magic", Card_name = "FireSpell", Element = "fire" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "knight", Card_name = "Knight", Element = "normal" };
            string expected = "tie";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinFireSpellKraken()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "magic", Card_name = "FireSpell" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "kraken", Card_name = "Kraken" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinKrakenWaterSpell()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "kraken", Card_name = "Kraken" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "magic", Card_name = "WaterSpell" };
            string expected = "player1";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinFireElfDragon()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "fireelf", Card_name = "FireElf" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "dragon", Card_name = "Dragon" };
            string expected = "player1";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinDragonFireElf()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "dragon", Card_name = "Dragon" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "fireelf", Card_name = "FireElf" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinKrakenFireelf()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "kraken", Card_name = "Kraken" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "fireelf", Card_name = "FireElf" };
            string expected = "tie";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCheckInstantWinFireSpellFireelf()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Species = "magic", Card_name = "FireSpell" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Species = "fireelf", Card_name = "FireElf" };
            string expected = "tie";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string actual = battleLogic.CheckInstantWin(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CheckDamageWaterGoblinOrk()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 {Card_name = "WaterGoblin", Damage = 10, Card_type = "monster", Element = "water"};
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "Ork", Damage = 15, Card_type = "monster", Element = "normal" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageDragonKraken()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "Dragon", Damage = 70, Card_type = "monster", Element = "normal" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "Kraken", Damage = 85, Card_type = "monster", Element = "normal" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageGoblinGoblin()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "Goblin", Damage = 17, Card_type = "monster", Element = "normal" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "Goblin", Damage = 17, Card_type = "monster", Element = "normal" };
            string expected = "tie";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageWizzardWaterSpell()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "Wizzard", Damage = 45, Card_type = "monster", Element = "normal" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "WaterSpell", Damage = 55, Card_type = "spell", Element = "water" };
            string expected = "player1";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageWaterSpellWizzard()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "Wizzard", Damage = 10, Card_type = "monster", Element = "normal" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "WaterSpell", Damage = 50, Card_type = "spell", Element = "water" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageFireSpellDragon()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "FireSpell", Damage = 60, Card_type = "spell", Element = "fire" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "Dragon", Damage = 80, Card_type = "monster", Element = "normal" };
            string expected = "player1";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageDragonFireSpell()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "FireDragon", Damage = 60, Card_type = "monster", Element = "fire" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "FireSpell", Damage = 80, Card_type = "spell", Element = "fire" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageRegularSpellFireSpell()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "RegularSpell", Damage = 40, Card_type = "spell", Element = "normal" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "FireSpell", Damage = 20, Card_type = "spell", Element = "fire" };
            string expected = "player2";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageRegularSpellWaterSpell()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "RegularSpell", Damage = 30, Card_type = "spell", Element = "normal" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "WaterSpell", Damage = 40, Card_type = "spell", Element = "water" };
            string expected = "player1";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDamageWaterSpellWaterSpell()
        {
            // Arrange
            DeckPlayer1 player1Card = new DeckPlayer1 { Card_name = "WaterSpell", Damage = 20, Card_type = "spell", Element = "water" };
            DeckPlayer2 player2Card = new DeckPlayer2 { Card_name = "WaterSpell", Damage = 20, Card_type = "spell", Element = "water" };
            string expected = "tie";
            BattleLogic battleLogic = new BattleLogic();

            // Act
            string result = battleLogic.CheckDamage(player1Card, player2Card);

            // Assert
            Assert.AreEqual(expected, result);
        }












    }

}


