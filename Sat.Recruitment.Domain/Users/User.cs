using Sat.Recruitment.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Users
{
    public  class User : EntityBase
    {
        public User(string name,string email, string address, string phone, UserType type) 
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            Type = type;           
        }
        public User() 
        {
        
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType Type { get; set; }
        public decimal Money { get; private set; }

        public void SetMoney(decimal money) 
        {
            Money = money;
        }

        public override string ToString()
        {
            return $"{Name},{Email},{Address},{Phone},{Type},{Money.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is User))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            var item = (User)obj;

            return item.Email == this.Email ||
                   item.Phone == this.Phone ||
                   item.Name == this.Name && item.Address == this.Address;
        }


        public static bool operator == (User left, User right)
        {
            if (Object.Equals(left, null))
            {
                return (Object.Equals(right, null)) ? true : false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(User left, User right)
        {
            return !(left == right);
        }
    }
}
