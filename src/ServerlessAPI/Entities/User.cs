using Amazon.DynamoDBv2.DataModel;

namespace ServerlessAPI.Entities;

// <summary>
/// Map the Book Class to DynamoDb Table
/// To learn more visit https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DeclarativeTagsList.html
/// </summary>
[DynamoDBTable("raid-tracker-UserTable")]
public class User
{
    ///<summary>
    /// Map c# types to DynamoDb Columns 
    /// to learn more visit https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/MidLevelAPILimitations.SupportedTypes.html
    /// <summary>
    [DynamoDBHashKey] //Partition key
    public Guid Id { get; set; } = Guid.Empty;

    [DynamoDBProperty]
    public string Email { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string? Password { get; set; }

    [DynamoDBProperty]
    public bool? Enabled { get; set; }
}
