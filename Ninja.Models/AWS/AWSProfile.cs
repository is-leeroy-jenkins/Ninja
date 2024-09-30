using System.Collections.Generic;

namespace Ninja.Models.AWS;

public static class AWSProfile
{
    public static List<AWSProfileInfo> GetDefaultList()
    {
        return new List<AWSProfileInfo>
        {
            new(false, "default", "eu-central-1"),
            new(false, "default", "us-east-1")
        };
    }
}