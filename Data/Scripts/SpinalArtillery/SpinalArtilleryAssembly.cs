using System.Collections.Generic;
using System.Linq;
using Sandbox.ModAPI;
using SpinalArtillery.API;
using SpinalArtillery.ModularAssemblies.Communication;
using VRage.Game.Entity;
using VRage.Game.ModAPI;

namespace SpinalArtillery
{
    public class SpinalArtilleryAssembly
    {
        public static ModularDefinitionApi ModularApi => ModularAssemblies.ModularDefinition.ModularApi;
        public static WcApi WcApi => MasterSession.I.WcApi;


        public readonly int AssemblyId;
        public readonly IMyCubeBlock BaseBlock;

        private readonly Dictionary<string, int> _blockCounts = new Dictionary<string, int>()
        {
            // Barrel Parts
            ["SpinalBarrelBlock"] = 0,
            ["SpinalCoilPart"] = 0,
            
            // Breech/Misc
            ["SpinalBreech"] = 1,
            
            // Loader Stack
            ["SpinalStackExtender"] = 0,
            ["SpinalExplosivePacker"] = 0,
            ["SpinalAutoloader"] = 0,
            ["SpinalMagazine"] = 0,
            ["SpinalGunpowderPacker"] = 0,
        };

        private readonly string[] LoaderStackSubtypes = {
            "SpinalStackExtender",
            "SpinalExplosivePacker",
            "SpinalAutoloader",
            "SpinalMagazine",
            "SpinalGunpowderPacker",
        };

        internal int LoaderStackSize => _blockCounts.Sum(kvp => LoaderStackSubtypes.Contains(kvp.Key) ? kvp.Value : 0);
        internal float StackSizeMultiplier => 1 - 0.01f * LoaderStackSize;



        #region Multipliers

        // SpinalAutoloader
        internal float RoF => _blockCounts["SpinalAutoloader"] * StackSizeMultiplier;

        // SpinalExplosivePacker
        internal float AreaDmg => _blockCounts["SpinalExplosivePacker"] * StackSizeMultiplier * 1000;
        internal float AreaRadius => _blockCounts["SpinalExplosivePacker"] * StackSizeMultiplier;

        #endregion



        public SpinalArtilleryAssembly(int assemblyId, IMyCubeBlock baseBlock)
        {
            AssemblyId = assemblyId;
            BaseBlock = baseBlock;
            UpdateStats();
        }

        public void UpdateAfterSimulation()
        {
            MyAPIGateway.Utilities.ShowNotification("Stack Size: " + LoaderStackSize, 1000/60);
            MyAPIGateway.Utilities.ShowNotification("AreaDmg: " + WcApi.GetAreaDmgMultiplier((MyEntity) BaseBlock), 1000/60);
            MyAPIGateway.Utilities.ShowNotification("AreaRadius: " + WcApi.GetAreaRadiusMultiplier((MyEntity) BaseBlock), 1000/60);
            //foreach (var blocktype in _blockCounts.Where(kvp => kvp.Value != 0))
            //    MyAPIGateway.Utilities.ShowNotification($"{blocktype.Key}: {blocktype.Value}", 1000/60);

            UpdateStats();
        }

        public void Close()
        {

        }

        public void OnPartAdd(IMyCubeBlock block)
        {
            _blockCounts[block.BlockDefinition.SubtypeId]++;
            UpdateStats(block.BlockDefinition.SubtypeId);
        }
        
        public void OnPartRemove(IMyCubeBlock block)
        {
            _blockCounts[block.BlockDefinition.SubtypeId]--;
            UpdateStats(block.BlockDefinition.SubtypeId);
        }

        public void OnPartDestroy(IMyCubeBlock block)
        {
            /*
             * TODO: Make the following blocks explode upon damage:
             * - SpinalExplosivePacker
             * - SpinalMagazine
             * - SpinalGunpowderPacker
             */ 
        }

        private void UpdateStats(string blockType = null)
        {
            if (blockType == null)
            {
                foreach (var key in _blockCounts.Keys)
                    UpdateStats(key);
                return;
            }

            switch (blockType)
            {
                case "SpinalBarrelBlock":
                    break;
                case "SpinalCoilPart":
                    break;
                case "SpinalBreech":
                    // TODO: Show error message and "break" the weapon when multiple breeches are added.
                    // Alternatively, split the stats in half?
                    break;
                case "SpinalStackExtender":
                    break;
                case "SpinalExplosivePacker":
                    WcApi.SetAreaDmgMultiplier((MyEntity) BaseBlock, AreaDmg);
                    WcApi.SetAreaRadiusMultiplier((MyEntity) BaseBlock, AreaRadius);
                    break;
                case "SpinalAutoloader":
                    WcApi.SetRofMultiplier((MyEntity) BaseBlock, RoF);
                    break;
                case "SpinalMagazine":
                    break;
                case "SpinalGunpowderPacker":
                    break;
            }
        }
    }
}
