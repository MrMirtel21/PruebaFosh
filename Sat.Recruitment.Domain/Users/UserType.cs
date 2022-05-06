using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Domain.Users
{
    public class UserType
    {

        public string Name { get; private set; }

        public string Id { get; private set; }


        public static UserType Normal => new UserType("normal", "Normal");
        public static UserType SuperUser => new UserType("superUser", "SuperUser");
        public static UserType Premium => new UserType("premium", "Premium");


        private UserType(string id, string name) 
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<UserType> List() =>
                new[] { Normal, SuperUser, Premium };

        public static UserType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                ThrowInvalidTypeException();
            }

            return state;
        }

        public static UserType From(string id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                ThrowInvalidTypeException();
            }

            return state;
        }

        public static bool ExistName(string name)
        {
            try
            {
                FromName(name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        private static void ThrowInvalidTypeException()
        {
            throw new Exception($"Possible values for UserType: {String.Join(",", List().Select(s => s.Name))}");
        }

    }
}
