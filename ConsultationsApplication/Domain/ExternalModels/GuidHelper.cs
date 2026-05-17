using System.Security.Cryptography;
using System.Text;

namespace Domain.ExternalModels;

public class GuidHelper
{
    public static Guid FromLegacyId(string entityType, int legacyId)
    {
        var input = $"{entityType}:{legacyId}";
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return new Guid(hash);
    }
}