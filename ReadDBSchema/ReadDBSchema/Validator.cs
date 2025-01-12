using System.Text.RegularExpressions;

namespace ReadDBSchema
{
    public static class Validator
    {
        public static bool IsValidUsername(string username)
        {
            const int maxLength = 32;
            const string pattern = @"^[a-zA-Z0-9_-]+$"; 

            if (string.IsNullOrEmpty(username) || username.Length > maxLength)
                return false;

            return Regex.IsMatch(username, pattern);
        }

        public static bool IsValidPassword(string password)
        {
            const int minLength = 8;

            if (string.IsNullOrEmpty(password) || password.Length < minLength)
                return false;

            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$";

            return Regex.IsMatch(password, pattern);
        }
    }
}
