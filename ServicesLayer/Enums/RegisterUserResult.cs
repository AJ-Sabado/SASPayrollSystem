namespace ServicesLayer.Enums
{
    public enum RegisterUserResult
    {
        Success,
        UserAlreadyExists,
        InvalidEmail,
        PasswordMismatch,
        WeakPassword,
        UnknownError
    }
}
