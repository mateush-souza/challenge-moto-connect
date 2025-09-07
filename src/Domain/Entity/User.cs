using challenge_moto_connect.Domain.Interfaces;
using challenge_moto_connect.Domain.ValueObjects;

namespace challenge_moto_connect.Domain.Entity
{
    public class User : ICancel
    {
        #region ICancel Properties
        public Guid UserCancelID { get; set; }
        public bool IsCancel { get; set; }
        #endregion

        public Guid UserID { get; set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public UserType Type { get; set; }

        public User() { }

        public User(Guid id, Email email, Password password, UserType type)
        {
            UserID = id;
            Email = email;
            Password = password;
            Type = type;
            IsCancel = false;
            UserCancelID = Guid.Empty;
        }

        public void UpdateEmail(Email newEmail)
        {
            Email = newEmail;
        }

        public void UpdatePassword(Password newPassword)
        {
            Password = newPassword;
        }
    }
}


