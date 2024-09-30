using Ninja.Models.TigerVNC;
using Ninja.Settings;

namespace Ninja.Profiles.Application;

using Models.TigerVNC;
using Settings;

public static class TigerVNC
{
    public static TigerVNCSessionInfo CreateSessionInfo(ProfileInfo profile)
    {
        // Get group info
        var group = ProfileManager.GetGroup(profile.Group);

        return new TigerVNCSessionInfo
        {
            Host = profile.TigerVNC_Host,

            Port = profile.TigerVNC_OverridePort
                ? profile.TigerVNC_Port
                : group.TigerVNC_OverridePort
                    ? group.TigerVNC_Port
                    : SettingsManager.Current.TigerVNC_Port
        };
    }
}