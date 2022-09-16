namespace AccountService.Strings
{
    public static class AuthResponseStrings
    {
        public const string AccountCannotbeCreated = "You can't create an account.";
        public const string MessageAfterCreatingAccount = "Account created.";
       

        public static string IncorrectDataMessage(string data)
        {
            return $"Incorrect username / password.";
        }

        public static string UserNotFoundMessage(string dataToMessage)
        {
            return $"{dataToMessage} not found.";
        }
    }
}
