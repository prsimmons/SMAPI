﻿using System;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI.Framework;
using StardewModdingAPI.Inheritance;
using StardewValley;

namespace StardewModdingAPI.Events
{
    /// <summary>Events raised when the player data changes.</summary>
    public static class PlayerEvents
    {
        /*********
        ** Events
        *********/
        /// <summary>Raised after the player loads a saved game.</summary>
        public static event EventHandler<EventArgsLoadedGameChanged> LoadedGame;

        /// <summary>Raised after the game assigns a new player character. This happens just before <see cref="LoadedGame"/>; it's unclear how this would happen any other time.</summary>
        public static event EventHandler<EventArgsFarmerChanged> FarmerChanged;

        /// <summary>Raised after the player's inventory changes in any way (added or removed item, sorted, etc).</summary>
        public static event EventHandler<EventArgsInventoryChanged> InventoryChanged;

        /// <summary> Raised after the player levels up a skill. This happens as soon as they level up, not when the game notifies the player after their character goes to bed.</summary>
        public static event EventHandler<EventArgsLevelUp> LeveledUp;


        /*********
        ** Internal methods
        *********/
        /// <summary>Raise a <see cref="LoadedGame"/> event.</summary>
        /// <param name="monitor">Encapsulates monitoring and logging.</param>
        /// <param name="loaded">Whether the save has been loaded. This is always true.</param>
        internal static void InvokeLoadedGame(IMonitor monitor, EventArgsLoadedGameChanged loaded)
        {
            monitor.SafelyRaiseGenericEvent($"{nameof(PlayerEvents)}.{nameof(PlayerEvents.LoadedGame)}", PlayerEvents.LoadedGame?.GetInvocationList(), null, loaded);
        }

        /// <summary>Raise a <see cref="FarmerChanged"/> event.</summary>
        /// <param name="monitor">Encapsulates monitoring and logging.</param>
        /// <param name="priorFarmer">The previous player character.</param>
        /// <param name="newFarmer">The new player character.</param>
        internal static void InvokeFarmerChanged(IMonitor monitor, Farmer priorFarmer, Farmer newFarmer)
        {
            monitor.SafelyRaiseGenericEvent($"{nameof(PlayerEvents)}.{nameof(PlayerEvents.FarmerChanged)}", PlayerEvents.FarmerChanged?.GetInvocationList(), null, new EventArgsFarmerChanged(priorFarmer, newFarmer));
        }

        /// <summary>Raise an <see cref="InventoryChanged"/> event.</summary>
        /// <param name="monitor">Encapsulates monitoring and logging.</param>
        /// <param name="inventory">The player's inventory.</param>
        /// <param name="changedItems">The inventory changes.</param>
        internal static void InvokeInventoryChanged(IMonitor monitor, List<Item> inventory, IEnumerable<ItemStackChange> changedItems)
        {
            monitor.SafelyRaiseGenericEvent($"{nameof(PlayerEvents)}.{nameof(PlayerEvents.InventoryChanged)}", PlayerEvents.InventoryChanged?.GetInvocationList(), null, new EventArgsInventoryChanged(inventory, changedItems.ToList()));
        }

        /// <summary>Rase a <see cref="LeveledUp"/> event.</summary>
        /// <param name="monitor">Encapsulates monitoring and logging.</param>
        /// <param name="type">The player skill that leveled up.</param>
        /// <param name="newLevel">The new skill level.</param>
        internal static void InvokeLeveledUp(IMonitor monitor, EventArgsLevelUp.LevelType type, int newLevel)
        {
            monitor.SafelyRaiseGenericEvent($"{nameof(PlayerEvents)}.{nameof(PlayerEvents.LeveledUp)}", PlayerEvents.LeveledUp?.GetInvocationList(), null, new EventArgsLevelUp(type, newLevel));
        }
    }
}
