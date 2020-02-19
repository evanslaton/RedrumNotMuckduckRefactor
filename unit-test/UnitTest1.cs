using Microsoft.VisualStudio.TestTools.UnitTesting;
using redrum_not_muckduck_game;

namespace unit_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGameSetUp()
        {
            Game g = new Game();
            Assert.AreEqual(Game.AllRooms.Count, 7);
            Assert.AreEqual(Game.CurrentRoom.Name, "Accounting");
        }

        [TestMethod]
        public void TestGetColumn()
        {
            Assert.AreEqual(Solution.GetColumn(), 80);
        }
    }
}
