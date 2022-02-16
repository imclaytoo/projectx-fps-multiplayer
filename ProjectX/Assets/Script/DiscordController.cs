using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;

    public string sDetails, sState, sLargeImage, sLargeText, sSmallImage, sSmallText;

    // Start is called before the first frame update
    void Start()
    {
        discord = new Discord.Discord(943540113659555911, (System.UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            Details = sDetails,
            State = sState,
            Assets = 
            {
                LargeImage = sLargeImage,
                LargeText = sLargeText,
                SmallImage = sSmallImage,
                SmallText = sSmallText
            },
        };
        activityManager.UpdateActivity(activity, (res) => {
            if (res == Discord.Result.Ok)
                Debug.Log("Discord status set!");
            else
                Debug.LogError("Discord status failed!");
        });
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }

    /*
    static void UpdatePresence()
    {
        DiscordRichPresence discordPresence;
        memset(&discordPresence, 0, sizeof(discordPresence));
        discordPresence.state = "Main Menu";
        discordPresence.details = "In-Game";
        discordPresence.startTimestamp = 1507665886;
        discordPresence.endTimestamp = 1507665886;
        discordPresence.largeImageText = "ProjectX";
        discordPresence.smallImageText = "ProjectX";
        discordPresence.partyId = "ae488379-351d-4a4f-ad32-2b9b01c91657";
        discordPresence.joinSecret = "MTI4NzM0OjFpMmhuZToxMjMxMjM= ";
        Discord_UpdatePresence(&discordPresence);
    }
    */
}
