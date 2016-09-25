namespace Identity.Core.Features.User.Services.Interfaces
{
    public interface IEncryptionService
    {
        string CreateSaltKey(int size);
        string CreatePasswordHash(string password, string saltkey);
    }
}
