namespace Payments.Backend.Challenge.Domain.Services;

public class UserUniquesPolicy
{
    public void EnsureCpfIsUnique(bool cpfAlreadyExist)
    {
        if (cpfAlreadyExist)
            throw new InvalidOperationException("Duplicated cpf is not allowed.");
    }
    public void EnsureEmailIsUnique(bool emailAlreadyExist)
    {
        if (emailAlreadyExist)
            throw new InvalidOperationException("Duplicated email is not allowed.");
    }
}