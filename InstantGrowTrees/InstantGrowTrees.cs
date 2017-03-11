﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.TerrainFeatures;

namespace InstantGrowTrees
{
    /// <summary>The entry class called by SMAPI.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Properties
        *********/
        /// <summary>The mod configuration.</summary>
        private ModConfig Config;


        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();
            TimeEvents.AfterDayStarted += this.ReceiveAfterDayStarted;
        }


        /*********
        ** Private methods
        *********/
        /****
        ** Event handlers
        ****/
        /// <summary>The method called when the current day changes.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ReceiveAfterDayStarted(object sender, EventArgs e)
        {
            // When the player loads a saved game, or after the overnight save,
            // check for any trees that should be grown.
            this.GrowTrees();
        }

        /****
        ** Methods
        ****/
        /// <summary>Grow all trees eligible for growth.</summary>
        private void GrowTrees()
        {
            foreach (GameLocation location in Game1.locations)
            {
                foreach (KeyValuePair<Vector2, TerrainFeature> feature in location.terrainFeatures)
                {
                    if (this.Config.RegularTreesInstantGrow && feature.Value is Tree)
                        this.GrowTree((Tree)feature.Value, location, feature.Key);
                    if (this.Config.FruitTreesInstantGrow && feature.Value is FruitTree)
                        GrowFruitTree((FruitTree)feature.Value, location, feature.Key);
                }
            }
        }

        /// <summary>Grow a tree if it's eligible for growth.</summary>
        /// <param name="tree">The tree to grow.</param>
        /// <param name="location">The tree's location.</param>
        /// <param name="tile">The tree's tile position.</param>
        private void GrowTree(Tree tree, GameLocation location, Vector2 tile)
        {
            if (this.Config.RegularTreesGrowInWinter || !Game1.currentSeason.Equals("winter") || tree.treeType == 6)
            {
                // ignore trees on nospawn tiles
                string isNoSpawn = location.doesTileHaveProperty((int)tile.X, (int)tile.Y, "NoSpawn", "Back");
                if (isNoSpawn != null && (isNoSpawn.Equals("All") || isNoSpawn.Equals("Tree")))
                    return;

                // ignore fully-grown trees
                if (tree.growthStage >= 5)
                    return;

                // ignore blocked seeds
                if (tree.growthStage == 0 && location.objects.ContainsKey(tile))
                    return;

                // grow blocked trees to max
                Rectangle freeArea = new Rectangle((int)((tile.X - 1.0) * Game1.tileSize), (int)((tile.Y - 1.0) * Game1.tileSize), Game1.tileSize * 3, Game1.tileSize * 3);
                foreach (KeyValuePair<Vector2, TerrainFeature> pair in location.terrainFeatures)
                {
                    if (pair.Value is Tree && !pair.Value.Equals(this) && ((Tree)pair.Value).growthStage >= 5 && pair.Value.getBoundingBox(pair.Key).Intersects(freeArea))
                    {
                        tree.growthStage = 4;
                        return;
                    }
                }

                // grow tree
                tree.growthStage = 5;
            }
        }

        /// <summary>Grow a fruit tree if it's eligible for growth.</summary>
        /// <param name="tree">The tree to grow.</param>
        /// <param name="location">The tree's location.</param>
        /// <param name="tile">The tree's tile position.</param>
        private void GrowFruitTree(FruitTree tree, GameLocation location, Vector2 tile)
        {
            // ignore if tree blocked
            foreach (Vector2 adjacentTile in Utility.getSurroundingTileLocationsArray(tile))
            {
                if (location.isTileOccupied(adjacentTile) && (!location.terrainFeatures.ContainsKey(tile) || !(location.terrainFeatures[tile] is HoeDirt) || ((HoeDirt)location.terrainFeatures[tile]).crop == null))
                    return;
            }

            // grow tree
            tree.daysUntilMature = 0;
            tree.growthStage = 4;
        }
    }
}
