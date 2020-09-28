using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    internal class Day21 : IAoC
    {
        // Challenge can be found on: https://adventofcode.com/2015/day/21

        internal Dictionary<string, (int cost, int damage, int armor)> weapons = new Dictionary<string, (int cost, int damage, int armor)> {
            { "Dagger", (cost: 8, damage: 4, armor: 0) },
            { "Shortsword", (cost: 10, damage: 5, armor: 0)},
            { "Warhammer", (cost: 25, damage: 6, armor: 0)},
            { "Longsword", (cost: 40, damage: 7, armor: 0)},
            { "Greataxe", (cost: 74, damage: 8, armor: 0)}
        };

        internal Dictionary<string, (int cost, int damage, int armor)> armor = new Dictionary<string, (int cost, int damage, int armor)> {
            {"Leather", (cost: 13, damage: 0, armor: 1) },
            {"Chainmail", (cost: 31, damage: 0, armor: 2)},
            {"Splintmail", (cost: 53, damage: 0, armor: 3)},
            {"Bandedmail", (cost: 75, damage: 0, armor: 4)},
            {"Platemail", (cost: 102, damage: 0, armor: 5)}
        };

        internal Dictionary<string, (int cost, int damage, int armor)> rings = new Dictionary<string, (int cost, int damage, int armor)> {
            {"Damage +1", (cost: 25, damage: 1, armor: 0) },
            {"Damage +2", (cost: 50, damage: 2, armor: 0)},
            {"Damage +3", (cost: 100, damage: 3, armor: 0)},
            {"Defense +1", (cost: 20, damage: 0, armor: 1)},
            {"Defense +2", (cost: 40, damage: 0, armor: 2)},
            {"Defense +3", (cost: 80, damage: 0, armor: 3) }
        };

        public string SolvePart1(string input)
        {
            int cheapestOutfit = int.MaxValue;

            // our outfits
            var queue = new Queue<(string weapon, string armor, string ring1, string ring2)>();
            foreach (var weapon in weapons) { queue.Enqueue((weapon: weapon.Key, armor: null, ring1: null, ring2: null)); }

            while (queue.Count > 0)
            {
                var outfit = queue.Dequeue();
                int outfitCost = CalculateOutfitPrice(outfit);

                // no bother to fight if it aint the cheapest winnable outfit
                if (outfitCost > cheapestOutfit) { continue; }

                if (PlayerWinsFromBoss(outfit))
                {
                    cheapestOutfit = Math.Min(cheapestOutfit, outfitCost);
                }

                // empty slot? please variate!
                if (outfit.armor == null)
                {
                    foreach (var armor in armor)
                    {
                        queue.Enqueue((outfit.weapon, armor.Key, outfit.ring1, outfit.ring2));
                    }
                }
                if (outfit.ring1 == null)
                {
                    foreach (var ring in rings)
                    {
                        queue.Enqueue((outfit.weapon, outfit.armor, ring.Key, outfit.ring2));
                    }
                }
                if (outfit.ring1 != null && outfit.ring2 == null)
                {
                    foreach (var ring in rings)
                    {
                        if (outfit.ring1 == ring.Key) { continue; }
                        queue.Enqueue((outfit.weapon, outfit.armor, outfit.ring1, ring.Key));
                    }
                }
            }

            return cheapestOutfit.ToString();
        }

        internal int CalculateOutfitPrice((string weapon, string armor, string ring1, string ring2) outfit)
        {
            int cost = weapons[outfit.weapon].cost;
            if (outfit.armor != null) { cost += armor[outfit.armor].cost; }
            if (outfit.ring1 != null) { cost += rings[outfit.ring1].cost; }
            if (outfit.ring2 != null && outfit.ring1 != outfit.ring2) { cost += rings[outfit.ring2].cost; }
            return cost;
        }

        internal bool PlayerWinsFromBoss((string weapon, string armor, string ring1, string ring2) outfit)
        {
            var boss = (hp: 100, attack: 8, armor: 2);
            var player = (hp: 100, attack: 0, armor: 0);
            player.attack = weapons[outfit.weapon].damage;
            if (outfit.armor != null)
            {
                player.armor = armor[outfit.armor].armor;
            }
            if (outfit.ring1 != null)
            {
                player.attack += rings[outfit.ring1].damage;
                player.armor += rings[outfit.ring1].armor;
            }
            if (outfit.ring2 != null && outfit.ring2 != outfit.ring1)
            {
                player.attack += rings[outfit.ring2].damage;
                player.armor += rings[outfit.ring2].armor;
            }

            // now FIGHT!
            bool playerTurn = true;
            int playerDamage = Math.Max((player.attack - boss.armor), 1);
            int bossDamage = Math.Max((boss.attack - player.armor), 1);
            while (player.hp > 0 && boss.hp > 0)
            {
                if (playerTurn) { boss.hp -= playerDamage; }
                else { player.hp -= bossDamage; }

                // switch
                playerTurn = !playerTurn;
            }

            return player.hp > boss.hp;
        }

        public string SolvePart2(string input)
        {
            int overpricedOutfit = int.MinValue;

            // our outfits
            var queue = new Queue<(string weapon, string armor, string ring1, string ring2)>();
            foreach (var weapon in weapons) { queue.Enqueue((weapon: weapon.Key, armor: null, ring1: null, ring2: null)); }

            while (queue.Count > 0)
            {
                var outfit = queue.Dequeue();
                int outfitCost = CalculateOutfitPrice(outfit);

                if (PlayerWinsFromBoss(outfit) == false)
                {
                    overpricedOutfit = Math.Max(overpricedOutfit, outfitCost);
                }

                // empty slot? please variate!
                if (outfit.armor == null)
                {
                    foreach (var armor in armor)
                    {
                        queue.Enqueue((outfit.weapon, armor.Key, outfit.ring1, outfit.ring2));
                    }
                }
                if (outfit.ring1 == null)
                {
                    foreach (var ring in rings)
                    {
                        queue.Enqueue((outfit.weapon, outfit.armor, ring.Key, outfit.ring2));
                    }
                }
                if (outfit.ring1 != null && outfit.ring2 == null)
                {
                    foreach (var ring in rings)
                    {
                        if (outfit.ring1 == ring.Key) { continue; }
                        queue.Enqueue((outfit.weapon, outfit.armor, outfit.ring1, ring.Key));
                    }
                }
            }

            return overpricedOutfit.ToString();
        }

        public string GetInput()
        {
            return @"Hit Points: 100
Damage: 8
Armor: 2";
        }
    }
}