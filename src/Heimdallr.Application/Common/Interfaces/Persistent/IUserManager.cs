namespace Heimdallr.Application.Common.Interfaces.Persistent;

public interface IUserManager
{
    Task<IUser?> FindUserByIdAsync(Guid userId);
}
