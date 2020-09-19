using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Global namespace
/// </summary>
namespace LinqTask
{
    /// <summary>
    /// Business logic class for data processing
    /// </summary>
    class BusinessLogic
    {
        private List<User> users = new List<User>();
        private List<Record> records = new List<Record>();

        /// <summary>
        /// Get random ID
        /// </summary>
        /// <returns>Random ID</returns>
        static int GetRandomID()
        {
            Random rnd = new Random();
            int id = rnd.Next(10000, 99999);

            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        public BusinessLogic()
        {
            users.Add(new User(GetRandomID(), "Nikola", "Ramsey"));
            records.Add(new Record(users[0], $"Hello, my name is {users[0].Name} {users[0].Surname}"));
            users.Add(new User(GetRandomID(), "Jannah", "Cohen"));
            records.Add(new Record(users[1], null));
            users.Add(new User(GetRandomID(), "Krisha", "Broadhurst"));
            records.Add(new Record(users[2], $"Hello, my name is {users[2].Name} {users[2].Surname}"));
            Console.WriteLine($"{records[0].Author}\nMessage: {records[0].Message}\n");
            Console.WriteLine($"{records[1].Author}\nMessage: {records[1].Message}\n");
            Console.WriteLine($"{records[2].Author}\nMessage: {records[2].Message}\n");
        }

        /// <summary>
        /// Get users by surname
        /// </summary>
        /// <param name="surname">Surname of user</param>
        /// <returns>List of users by surname</returns>
        public List<User> GetUsersBySurname(String surname)
        {
            var element = from user in users
                          where user.Equals(surname)
                          select user;
            return element.ToList();
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>User by ID</returns>
        public User GetUserByID(int id)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].ID == id)
                {
                    return users[i];
                }
            }
            Single();
            return null;
        }

        /// <summary>
        /// Throw NotImplementedException
        /// </summary>
        private void Single()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get users by substring
        /// </summary>
        /// <param name="substring">substring to contains</param>
        /// <returns>List of users by substring</returns>
        public List<User> GetUsersBySubstring(String substring)
        {
            List<User> usersBySubstring = new List<User>();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Surname.Contains(substring))
                {
                    usersBySubstring.Add(users[i]);
                }
            }
            if (usersBySubstring.Count > 1)
            {
                return usersBySubstring;
            }
            else
            {
                Single();
                return null;
            }
        }

        /// <summary>
        /// Get all unique names
        /// </summary>
        /// <returns>List of unique Names</returns>
        public List<String> GetAllUniqueNames()
        {
            List<String> usersUniqueNames = new List<String>();
            String[] uniqueNames = new string[users.Count];
            for (int i = 0; i < users.Count; i++)
            {
                uniqueNames[i] = users[i].Name;
            }
            IEnumerable<string> distinctNames = uniqueNames;
            foreach (string name in distinctNames)
            {
                usersUniqueNames.Add(name);
            }
            if (usersUniqueNames.Count > 1)
            {
                return usersUniqueNames;
            }
            else
            {
                Single();
                return null;
            }
        }

        /// <summary>
        /// Get all authors with massage
        /// </summary>
        /// <returns>List users by message</returns>
        public List<User> GetAllAuthors()
        {
            List<User> usersByMessage = new List<User>();

            for (int i = 0; i < users.Count; i++)
            {
                if (records[i].Message != null)
                {
                    usersByMessage.Add(users[i]);
                }
            }
            if (usersByMessage.Count > 1)
            {
                return usersByMessage;
            }
            else
            {
                Single();
                return null;
            }
        }

        /// <summary>
        /// Get users dictionary
        /// </summary>
        /// <returns>Users dictionary</returns>
        public Dictionary<int, User> GetUsersDictionary()
        {
            Dictionary<int, User> dictionary = users.ToDictionary(p => p.ID);
            return dictionary;
        }

        /// <summary>
        /// GetMaxID
        /// </summary>
        /// <returns>Max ID</returns>
        public int GetMaxID()
        {
            int maxValue = users[0].ID;

            for (int i = 1; i < users.Count; i++)
            {
                if (maxValue < users[i].ID)
                {
                    maxValue = users[i].ID;
                }
            }
            return maxValue;
        }

        /// <summary>
        /// Get ordered users
        /// </summary>
        /// <returns>List of ordered users</returns>
        public List<User> GetOrderedUsers()
        {
            List<User> orderedUsers = new List<User>();

            var sortedUsers = from i in users
                              orderby i.ID
                              select i;
            foreach (User i in sortedUsers)
            {
                orderedUsers.Add(i);
            }
            return orderedUsers;
        }

        /// <summary>
        /// Get descending ordered users
        /// </summary>
        /// <returns>List of descending ordered users</returns>
        public List<User> GetDescendingOrderedUsers()
        {
            List<User> orderedUsers = new List<User>();

            var sortedUsers = from i in users
                              orderby i.ID descending
                              select i;
            foreach (User i in sortedUsers)
            {
                orderedUsers.Add(i);
            }
            return orderedUsers;
        }

        /// <summary>
        /// Get reversed users
        /// </summary>
        /// <returns>Reversed list of users</returns>
        public List<User> GetReversedUsers()
        {
            users.Reverse();
            return users;
        }

        /// <summary>
        /// Get users page
        /// </summary>
        /// <param name="pageSize">The maximum possible number of elements on one page</param>
        /// <param name="pageIndex">Number of elements from the current position</param>
        /// <returns></returns>
        public List<User> GetUsersPage(int pageSize, int pageIndex)
        {
            List<User> usersPage = new List<User>();
            var result = users.Skip(pageIndex).Take(pageSize);
            foreach (User i in result)
            {
                usersPage.Add(i);
            }
            return usersPage;
        }
    }
}