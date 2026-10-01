using Alta;
using Alta.Caves;
using MelonLoader;
using System.Reflection;

[assembly: MelonInfo(typeof(CustomDistributionAPI.Core), "CustomDistributionAPI", "1.0.0", "CGNik", null)]
[assembly: MelonGame("Alta", "A Township Tale")]

namespace CustomDistributionAPI
{
    public class Core : MelonMod
    {
        public static void AddToDistribution(Distribution distribution, UnityEngine.Object topic, float baseValue, float noAttributeValue, AttributeCurveRange[] multipliers)
        {
            List<Distribution.Item> items = (List<Distribution.Item>)distribution.GetType().GetField("items", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(distribution);
            Distribution.Item item = new Distribution.Item();
            typeof(Distribution.BaseItem).GetField("topic", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item, topic);
            typeof(Distribution.BaseItem).GetField("baseValue", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item, baseValue);
            typeof(Distribution.BaseItem).GetField("noAttributeValue", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item, noAttributeValue);
            typeof(Distribution.BaseItem).GetField("multipliers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item, multipliers);
            items.Add(item);
        }

        public static void RegisterDistribution(Distribution distribution)
        {
            Distribution.CheckItems();
            Dictionary<uint, Distribution> items = (Dictionary<uint, Distribution>)typeof(HashedGeneralValue<Distribution>).GetField("items", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            items.Add(distribution.Hash, distribution);
        }

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }
}