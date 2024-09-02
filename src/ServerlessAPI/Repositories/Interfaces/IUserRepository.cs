using ServerlessAPI.Entities;

namespace ServerlessAPI.Repositories;

/// <summary>
/// Sample DynamoDB Table user CRUD
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Include new user to the DynamoDB Table
    /// </summary>
    /// <param name="user">user to include</param>
    /// <returns>success/failure</returns>
    Task<bool> CreateAsync(User user);
    
    /// <summary>
    /// Remove existing user from DynamoDB Table
    /// </summary>
    /// <param name="user">user to remove</param>
    /// <returns></returns>
    Task<bool> DeleteAsync(User user);

    /// <summary>
    /// Get user by PK
    /// </summary>
    /// <param name="id">user`s PK</param>
    /// <returns>User object</returns>
    Task<User?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Update user content
    /// </summary>
    /// <param name="user">user to be updated</param>
    /// <returns></returns>
    Task<bool> UpdateAsync(User user));
}