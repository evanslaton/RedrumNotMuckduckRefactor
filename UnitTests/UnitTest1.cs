using System.Collections.Generic;
using Room = redrum_not_muckduck_game.Room;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class RoomTest
    {
        private string Name = "Classroom1";
        private string Description = "TLG Learning Center";
        private bool HasItem = false;
        public Dictionary<string, bool> HasEventHappened;
        public Dictionary<string, bool> ItemInRoom;
        public Dictionary<string, string> PersonsInRoom;
        public string Action { get; set; } = "";
        private Room RoomToTest;

        [SetUp] //Creates the room to be used in tests
        public void Setup()
        {
            HasEventHappened = new Dictionary<string, bool>();
            HasEventHappened.Add("shoe", false);

            ItemInRoom = new Dictionary<string, bool>();
            ItemInRoom.Add("chicken nuggets", false);

            PersonsInRoom = new Dictionary<string, string>();
            PersonsInRoom.Add("charley", "I'm here!!!");

            RoomToTest = new Room(Name, Description, ItemInRoom,
                PersonsInRoom, Action, HasItem);
        }

        [Test]
        public void RoomShouldHaveAName()
        {
            Assert.AreEqual(RoomToTest.Name, Name); 
        }

        [Test]
        public void RoomShouldHaveADescription()
        {
            Assert.AreEqual(RoomToTest.Description, Description);
        }

        [Test]
        public void RoomsCanHaveAnItem()
        {
            Assert.AreEqual(RoomToTest.ItemInRoom, ItemInRoom);
        }

        [Test]
        public void RoomsCanHavePeopleInThem()
        {
            Assert.AreEqual(RoomToTest.PersonsInRoom, PersonsInRoom);
        }

        [Test]
        public void RoomsKnowIfTheyHaveAnItem()
        {
            Assert.AreEqual(RoomToTest.HasItem, HasItem);
        }
    }
}