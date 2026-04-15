namespace Heimdallr.Application.Common.Interfaces.Persistent;

public interface IUserManager
{
    IQueryable<IUser> ApplicationUsers { get; }
}
