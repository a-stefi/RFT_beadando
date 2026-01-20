using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace ATMApp
{
    internal class UserRepository
    {
        public class userRepository

        {

            private const string FilePath = "users.json";



            public List<User> LoadUsers()

            {

                if (!File.Exists(FilePath))

                    return new List<User>();

                var json = File.ReadAllText(FilePath);

                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

            }



            public void SaveUsers(List<User> users)

            {

                var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(FilePath, json);

            }



            public void SaveUser(User user)

            {

                var users = LoadUsers();

                var index = users.FindIndex(u => u.AccountNumber == user.AccountNumber);

                if (index != -1)

                {

                    users[index] = user;

                    SaveUsers(users);

                }

            }

        }
    }
}
