using System;
using NUnit.Framework;

namespace LinqTask.Tests
{
    public class Tests
    {
        private BusinessLogic businessLogic = new BusinessLogic();

        [Test]
        public void TestGetUsersBySurname()
        {
            Assert.AreEqual("Broadhurst", businessLogic.GetUsersBySurname("Broadhurst")[0].Surname);
            Assert.AreEqual("Daniyal", businessLogic.GetUsersBySurname("Broadhurst")[1].Name);
            Assert.AreEqual(2, businessLogic.GetUsersBySurname("Broadhurst").Count);
            Assert.AreEqual(1, businessLogic.GetUsersBySurname("Ramsey").Count);
        }

        [Test]
        public void GetUserByID()
        {
            Assert.AreEqual(businessLogic.GetUser(0), businessLogic.GetUserByID(55823));
            Assert.AreEqual(businessLogic.GetUser(1), businessLogic.GetUserByID(29364));
            Assert.Throws<NotImplementedException>(() => businessLogic.GetUserByID(0));
            Assert.Throws<NotImplementedException>(() => businessLogic.GetUserByID(25314));
        }

        [Test]
        public void GetUsersBySubstring()
        {
            Assert.Contains(businessLogic.GetUser(3), businessLogic.GetUsersBySubstring("Broad"));
            Assert.Contains(businessLogic.GetUser(2), businessLogic.GetUsersBySubstring("Broad"));
            Assert.That(businessLogic.GetUsersBySubstring("Broad"), Has.No.Member(businessLogic.GetUser(1)));
        }

        [Test]
        public void GetAllUniqueNames()
        {
            Assert.AreEqual(4, businessLogic.GetAllUniqueNames().Count);
            Assert.AreEqual(5, businessLogic.GetCountOfUsers());
        }

        [Test]
        public void GetAllAuthors()
        {
            Assert.AreEqual(4, businessLogic.GetAllAuthors().Count);
            Assert.AreEqual(5, businessLogic.GetCountOfUsers());
        }

        [Test]
        public void GetUsersDictionary()
        {
            Assert.AreEqual(4, businessLogic.GetUsersDictionary().Count);
        }

        [Test]
        public void GetMaxID()
        {
            Assert.AreEqual(businessLogic.GetUser(4).ID, businessLogic.GetMaxID());
            Assert.AreNotEqual(businessLogic.GetUser(3).ID, businessLogic.GetMaxID());
            Assert.AreNotEqual(businessLogic.GetUser(2).ID, businessLogic.GetMaxID());
            Assert.AreNotEqual(businessLogic.GetUser(1).ID, businessLogic.GetMaxID());
            Assert.AreNotEqual(businessLogic.GetUser(0).ID, businessLogic.GetMaxID());
        }

        [Test]
        public void GetOrderedUsers()
        {
            Assert.AreEqual(businessLogic.GetUser(2), businessLogic.GetOrderedUsers()[0]);
            Assert.AreEqual(businessLogic.GetUser(3), businessLogic.GetOrderedUsers()[1]);
            Assert.AreEqual(businessLogic.GetUser(1), businessLogic.GetOrderedUsers()[2]);
            Assert.AreEqual(businessLogic.GetUser(0), businessLogic.GetOrderedUsers()[3]);
            Assert.AreEqual(businessLogic.GetUser(4), businessLogic.GetOrderedUsers()[4]);
        }

        [Test]
        public void GetDescendingOrderedUsers()
        {
            Assert.AreEqual(businessLogic.GetUser(4), businessLogic.GetDescendingOrderedUsers()[0]);
            Assert.AreEqual(businessLogic.GetUser(0), businessLogic.GetDescendingOrderedUsers()[1]);
            Assert.AreEqual(businessLogic.GetUser(1), businessLogic.GetDescendingOrderedUsers()[2]);
            Assert.AreEqual(businessLogic.GetUser(2), businessLogic.GetDescendingOrderedUsers()[3]);
            Assert.AreEqual(businessLogic.GetUser(3), businessLogic.GetDescendingOrderedUsers()[4]);
        }

        [Test]
        public void GetReversedUsers()
        {
            Assert.AreEqual(businessLogic.GetUser(4), businessLogic.GetReversedUsers()[0]);
            Assert.AreEqual(businessLogic.GetUser(3), businessLogic.GetReversedUsers()[1]);
            Assert.AreEqual(businessLogic.GetUser(2), businessLogic.GetReversedUsers()[2]);
            Assert.AreEqual(businessLogic.GetUser(1), businessLogic.GetReversedUsers()[3]);
            Assert.AreEqual(businessLogic.GetUser(0), businessLogic.GetReversedUsers()[4]);
        }

        [Test]
        public void GetUsersPage()
        {
            Assert.AreEqual(2, businessLogic.GetUsersPage(2, 3).Count);
            Assert.AreEqual(5, businessLogic.GetCountOfUsers());
            Assert.AreEqual(1, businessLogic.GetUsersPage(1, 4).Count);
            Assert.AreEqual(0, businessLogic.GetUsersPage(1, 5).Count);
        }
    }
}