using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using challenge_moto_connect.Domain.Interfaces;

namespace challenge_moto_connect.Domain.Entity
{
    public class User : ICancel
    {
        #region ICancel Properties
        public Guid UserCancelID { get; set; }
        public bool IsCancel { get; set; }
        #endregion

        public Guid UserID { get; set; }

        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set
            {
                if (!IsValidEmail(value))
                    throw new ArgumentException("Email inválido.");
                _email = value;
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (!IsValidPassword(value))
                    throw new ArgumentException("Senha inválida. A senha deve conter pelo menos 8 caracteres, incluindo letra maiúscula, minúscula, número e caractere especial.");
                _password = value;
            }
        }

        public UserType Type { get; set; }

        public User() { }

        public User(Guid id, string email, string password, UserType type)
        {
            UserID = id;
            Email = email; // Usa propriedade que valida
            Password = password;
            Type = type;
            IsCancel = false;
            UserCancelID = Guid.Empty;
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
