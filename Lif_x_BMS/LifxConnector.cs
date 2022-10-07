using LifxCloud.NET;
using LifxCloud.NET.Models;
using System;
using System.Linq;

namespace Lif_x_BMS
{
    internal class LifxConnector
    {
        public static async void lifxConnector(string key)
        {
            try
            {
                var client = await LifxCloudClient.CreateAsync(key);
                var lights = await client.ListGroups(Selector.All);
                var result = await client.PulseEffect(lights.First(),
                    new PulseEffectRequest()
                    {
                        power_on = true,
                        period = 1,
                        cycles = 3,
                        persist = false,
                        color = LifxColor.BuildRGB(255, 0, 0)
                    });
            }
            catch (Exception ex)
            {
                Program.Log("Lifx Connector Failed");
                Program.Log(ex.ToString());
            }
        }
        public static async void lifxConnectorCS(string key)
        {
            try
            {
                var client = await LifxCloudClient.CreateAsync(key);
                var lights = await client.ListGroups(Selector.All);
                var result = await client.PulseEffect(lights.First(),
                    new PulseEffectRequest()
                    {
                        power_on = true,
                        period = 1,
                        cycles = 3,
                        persist = false,
                        color = LifxColor.BuildRGB(0, 255, 0)
                    });
            }
            catch (Exception ex)
            {
                Program.Log("Lifx Connector Failed");
                Program.Log(ex.ToString());
            }
        }
    }
}
