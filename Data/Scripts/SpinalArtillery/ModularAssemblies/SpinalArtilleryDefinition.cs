using System.Collections.Generic;
using VRageMath;
using static SpinalArtillery.ModularAssemblies.Communication.DefinitionDefs;

namespace SpinalArtillery.ModularAssemblies
{
    /* Hey there modders!
     *
     * This file is a *template*. Make sure to keep up-to-date with the latest version, which can be found at https://github.com/StarCoreSE/Modular-Assemblies-Client-Mod-Template.
     *
     * If you're just here for the API, head on over to https://github.com/StarCoreSE/Modular-Assemblies/wiki/The-Modular-API for a (semi) comprehensive guide.
     *
     */
    internal partial class ModularDefinition
    {
        // You can declare functions in here, and they are shared between all other ModularDefinition files.
        // However, for all but the simplest of assemblies it would be wise to have a separate utilities class.

        // This is the important bit.
        internal ModularPhysicalDefinition SpinalArtilleryDefinition => new ModularPhysicalDefinition
        {
            // Unique name of the definition.
            Name = "SpinalArtilleryDefinition",

            // Triggers whenever a new part is added to an assembly.
            OnPartAdd = (assemblyId, block, isBasePart) =>
            {
                if (isBasePart)
                    MasterSession.I.RegisterWeapon(assemblyId, block);
                else
                    MasterSession.I.OnPartAdd(assemblyId, block);
            },

            // Triggers whenever a part is removed from an assembly.
            OnPartRemove = (assemblyId, block, isBasePart) =>
            {
                if (isBasePart)
                    MasterSession.I.UnregisterWeapon(assemblyId);
                else
                    MasterSession.I.OnPartRemove(assemblyId, block);
            },

            // Triggers whenever a part is destroyed, just after OnPartRemove.
            OnPartDestroy = (assemblyId, block, isBasePart) =>
            {
                MasterSession.I.OnPartDestroy(assemblyId, block);
            },

            // Optional - if this is set, an assembly will not be created until a baseblock exists.
            BaseBlockSubtype = "SpinalBreech",

            // All SubtypeIds that can be part of this assembly.
            AllowedBlockSubtypes = new[]
            {
                // Barrel Parts
                "SpinalBarrelBlock",
                "SpinalCoilPart",

                // Breech/Misc
                "SpinalBreech",
                
                // Loader Stack
                "SpinalStackExtender",
                "SpinalExplosivePacker",
                "SpinalAutoloader",
                "SpinalMagazine",
                "SpinalGunpowderPacker",
            },

            // Allowed connection directions & whitelists, measured in blocks.
            // If an allowed SubtypeId is not included here, connections are allowed on all sides.
            // If the connection type whitelist is empty, all allowed subtypes may connect on that side.
            AllowedConnections = new Dictionary<string, Dictionary<Vector3I, string[]>>
            {
                // Barrel Parts
                ["SpinalBarrelBlock"] = new Dictionary<Vector3I, string[]>
                {
                    [Vector3I.Forward] = new[]
                    {
                        "SpinalBarrelBlock",
                        "SpinalBreech"
                    },
                    [Vector3I.Backward] = new[]
                    {
                        "SpinalBarrelBlock",
                        "SpinalBreech"
                    },
                },
                ["SpinalCoilPart"] = new Dictionary<Vector3I, string[]>
                {
                    [Vector3I.Forward] = new[]
                    {
                        "SpinalCoilPart",
                    },
                    [Vector3I.Backward] = new[]
                    {
                        "SpinalCoilPart",
                    },
                    [Vector3I.Right] = new[]
                    {
                        "SpinalBreech",
                    },
                },

                // Breech/Misc
                ["SpinalBreech"] = new Dictionary<Vector3I, string[]>
                {
                    [Vector3I.Up] = new[]
                    {
                        "SpinalCoilPart",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalAutoloader",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Down] = new[]
                    {
                        "SpinalCoilPart",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalAutoloader",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Right] = new[]
                    {
                        "SpinalCoilPart",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalAutoloader",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Left] = new[]
                    {
                        "SpinalCoilPart",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalAutoloader",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Forward] = new[]
                    {
                        "SpinalBarrelBlock",
                    },
                    [Vector3I.Backward] = new[]
                    {
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalAutoloader",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                },

                // Loader Stack
                ["SpinalStackExtender"] = new Dictionary<Vector3I, string[]>
                {
                    [Vector3I.Forward] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Backward] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Up] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Down] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Left] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                    [Vector3I.Right] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalExplosivePacker",
                        "SpinalMagazine",
                        "SpinalGunpowderPacker",
                    },
                },
                ["SpinalExplosivePacker"] = new Dictionary<Vector3I, string[]>
                {
                    [Vector3I.Forward] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                    },
                },
                ["SpinalAutoloader"] = new Dictionary<Vector3I, string[]>
                {
                    [Vector3I.Forward] = new[]
                    {
                        "SpinalMagazine",
                    },
                },
                ["SpinalMagazine"] = new Dictionary<Vector3I, string[]>
                {
                    [Vector3I.Up] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalAutoloader",
                    },
                    [Vector3I.Down] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalAutoloader",
                    },
                    [Vector3I.Right] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalAutoloader",
                    },
                    [Vector3I.Left] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalAutoloader",
                    },
                    [Vector3I.Forward*2] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalAutoloader",
                    },
                    [Vector3I.Backward*2] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                        "SpinalAutoloader",
                    },
                },
                ["SpinalGunpowderPacker"] = new Dictionary<Vector3I, string[]>
                {
                    [Vector3I.Forward] = new[]
                    {
                        "SpinalBreech",
                        "SpinalStackExtender",
                    },
                },
            },
        };
    }
}
