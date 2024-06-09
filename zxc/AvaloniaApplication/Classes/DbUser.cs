using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication.Classes
{
    /// <summary>
    /// Модель для пользователя
    /// </summary>
    public class DbUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateBirhday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? BankCardNumber { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? StreetHouseApartament { get; set; }
        public string? PostalCode { get; set; }
    }
}
