using System.Collections.Generic;
using System.Linq;
using Sandbox.ModAPI;
using SpinalArtillery.ModularAssemblies.Communication;
using VRage.Game.ModAPI;

namespace SpinalArtillery
{
    public class SpinalArtilleryAssembly
    {
        public static ModularDefinitionApi ModularApi => ModularAssemblies.ModularDefinition.ModularApi;

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

        public SpinalArtilleryAssembly(int assemblyId, IMyCubeBlock baseBlock)
        {
            AssemblyId = assemblyId;
            BaseBlock = baseBlock;
        }

        public void UpdateAfterSimulation()
        {
            foreach (var blocktype in _blockCounts.Where(kvp => kvp.Value != 0))
                MyAPIGateway.Utilities.ShowNotification($"{blocktype.Key}: {blocktype.Value}", 1000/60);
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
                    break;
                case "SpinalAutoloader":
                    break;
                case "SpinalMagazine":
                    break;
                case "SpinalGunpowderPacker":
                    break;
            }
        }
    }
}
