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
    public class BusinessLogic
    {
        private List<User> users = new List<User>();
        private List<Record> records = new List<Record>();

        private string Surname { get; set; }

        /// <summary>
        /// Filling both collections with test data
        /// </summary>
        public BusinessLogic()
        {
            users.Add(new User(55823, "Nikola", "Ramsey"));
            records.Add(new Record(users[0], $"Hello, my name is {users[0].Name} {users[0].Surname}"));
            users.Add(new User(29364, "Jannah", "Cohen"));
            records.Add(new Record(users[1], null));
            users.Add(new User(12424, "Krisha", "Broadhurst"));
            records.Add(new Record(users[2], $"Hello, my name is {users[2].Name} {users[2].Surname}"));
            users.Add(new User(12424, "Daniyal", "Broadhurst"));
            records.Add(new Record(users[3], $"Hello, my name is {users[3].Name} {users[3].Surname}"));
            users.Add(new User(92573, "Daniyal", "Akhtar"));
            records.Add(new Record(users[4], $"Hello, my name is {users[4].Name} {users[4].Surname}"));
            Console.WriteLine($"{records[0].Author}\nMessage: {records[0].Message}\n");
            Console.WriteLine($"{records[1].Author}\nMessage: {records[1].Message}\n");
            Console.WriteLine($"{records[2].Author}\nMessage: {records[2].Message}\n");
            Console.WriteLine($"{records[3].Author}\nMessage: {records[3].Message}\n");
            Console.WriteLine($"{records[4].Author}\nMessage: {records[4].Message}\n");
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="i">User</param>
        /// <returns></returns>
        public User GetUser(int i)
        {
            return users[i];
        }

        /// <summary>
        /// Get count of users
        /// </summary>
        /// <returns>Count of users</returns>
        public int GetCountOfUsers()
        {
            return users.Count;
        }


        /// <summary>
        /// Get users by surname
        /// </summary>
        /// <param name="surname">Surname of user</param>
        /// <returns>List of users by surname</returns>
        public List<User> GetUsersBySurname(String surname)
        {
            var element = (from user in users
                           where user.Surname == surname
                           select user).ToList();
            return element;
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>User by ID</returns>
        public User GetUserByID(int id)
        {
            try
            {
                var userByID = (from user in users
                              where user.ID == id
                              select user).ToList();
                return userByID[0];
            }
            catch (ArgumentOutOfRangeException)
            {
                Single();
                return null;
            }
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
            var usersBySubstring = (from user in users
                                    where user.Surname.Contains(substring)
                                    select user).ToList();
            return usersBySubstring;
        }

        /// <summary>
        /// Get all unique names
        /// </summary>
        /// <returns>List of unique Names</returns>
        public List<String> GetAllUniqueNames()
        {
            List<string> nameList = (from user in users
                                     select user.Name).ToList();
            IEnumerable<string> unique = nameList.Distinct();
            return unique.ToList();
        }

        /// <summary>
        /// Get all authors with massage
        /// </summary>
        /// <returns>List users by message</returns>
        public List<User> GetAllAuthors()
        {
            var allAuthors = (from record in records
                              where record.Message != null
                              select record.Author).ToList();
            return allAuthors;
        }

        /// <summary>
        /// Get users dictionary
        /// </summary>
        /// <returns>Users dictionary</returns>
        public Dictionary<string, User> GetUsersDictionary()
        {
            Dictionary<string, User> dictionary = new Dictionary<string, User>();
            var people = users
                .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
            return people;
        }

        /// <summary>
        /// GetMaxID
        /// </summary>
        /// <returns>Max ID</returns>
        public int GetMaxID()
        {
            int maxValue = users[0].ID;
            foreach (var user in users.Where(user => maxValue < user.ID))
            {
                maxValue = user.ID;
            }
            return maxValue;
        }

        /// <summary>
        /// Get ordered users
        /// </summary>
        /// <returns>List of ordered users</returns>
        public List<User> GetOrderedUsers()
        {
            var sortedUsers = (from i in users
                               orderby i.ID
                               select i).ToList();
            return sortedUsers;
        }

        /// <summary>
        /// Get descending ordered users
        /// </summary>
        /// <returns>List of descending ordered users</returns>
        public List<User> GetDescendingOrderedUsers()
        {
            var sortedUsers = (from i in users
                               orderby i.ID descending
                               select i).ToList();
            return sortedUsers;
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
            var result = (users.Skip(pageIndex).Take(pageSize)).ToList();
            return result;
        }
    }
}