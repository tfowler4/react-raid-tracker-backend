using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ServerlessAPI.Entities;

namespace ServerlessAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDynamoDBContext context;
    private readonly ILogger<UserRepository> logger;

    public UserRepository(IDynamoDBContext context, ILogger<UserRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<bool> CreateAsync(User user)
    {
        try
        {
            user.Id = Guid.NewGuid();
            await context.SaveAsync(user);
            logger.LogInformation("User {} is added", user.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "fail to persist to DynamoDb Table");
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteAsync(User user)
    {
        bool result;
        try
        {
            // Delete the user.
            await context.DeleteAsync<User>(user.Id);
            // Try to retrieve deleted user. It should return null.
            User deletedUser = await context.LoadAsync<User>(user.Id, new DynamoDBContextConfig
            {
                ConsistentRead = true
            });

            result = deletedUser == null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "fail to delete user from DynamoDb Table");
            result = false;
        }

        if (result) logger.LogInformation("User {Id} is deleted", user);

        return result;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        if (user == null) return false;

        try
        {
            await context.SaveAsync(user);
            logger.LogInformation("User {Id} is updated", user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "fail to update user from DynamoDb Table");
            return false;
        }

        return true;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        try
        {
            return await context.LoadAsync<User>(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "fail to update user from DynamoDb Table");
            return null;
        }
    }
}