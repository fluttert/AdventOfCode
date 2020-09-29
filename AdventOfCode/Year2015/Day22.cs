using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    internal class Day22 : IAoC
    {
        // Challenge can be found on: https://adventofcode.com/2015/day/22

        internal Dictionary<string, (int cost, int damage, int armor, int heals, int manaGained, int duration)> spells = new Dictionary<string, (int cost, int damage, int armor, int heals, int manaGained, int duration)> {
            { "Shield", (cost: 113, damage:0, armor: 7, heals:0, manaGained:0, duration:6) },
            { "Poison", (cost: 173, damage:3, armor: 0, heals:0, manaGained:0, duration:6) },
            { "Recharge", (cost: 229, damage:0, armor: 0, heals:0, manaGained:101, duration:5) },
            { "Magic Missile", (cost: 53, damage:4, armor: 0, heals:0, manaGained:0, duration:0) },
            { "Drain", (cost: 73, damage:2, armor: 0, heals:2, manaGained:0, duration:0) }
        };

        public string SolvePart1(string input)
        {
            return SolveGame(hardMode: false);
        }

        public string SolvePart2(string input)
        {
            return SolveGame(hardMode: true);
        }

        public string SolveGame(bool hardMode = false)
        {
            int minimumManaSpent = int.MaxValue;
            var games = new Queue<List<string>>();
            foreach (var spell in spells)
            {
                games.Enqueue(new List<string>() { spell.Key });
            }

            while (games.Count > 0)
            {
                var game = games.Dequeue();

                var (gameValid, gameEnds, playerWins, manaSpent) = SimulateGame(game, hardMode, minimumManaSpent);

                // discard invalid & expensive games
                if (gameValid is false)
                {
                    continue;
                }

                if (gameEnds && playerWins)
                {
                    if (manaSpent < minimumManaSpent)
                    {
                        minimumManaSpent = manaSpent;
                        //break;
                    }
                }

                // when the match is not yet over, add a random spell
                if (gameEnds is false)
                {
                    foreach (var spell in spells)
                    {
                        var newGame = CopyList(game);
                        newGame.Add(spell.Key);
                        games.Enqueue(newGame);
                    }
                }
            }
            return minimumManaSpent.ToString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0042:Deconstruct variable declaration", Justification = "<Pending>")]
        public (bool gameValid, bool gameEnds, bool playerWins, int manaSpent) SimulateGame(List<string> moves, bool hardMode, int manaLimit)
        {
            var gameResult = (gameValid: true, gameEnds: true, playerWins: false, manaSpent: 0);
            var player = (hitpoints: 50, damage: 0, armor: 0, manaPoints: 500, manaSpent: 0);
            var boss = (hitpoints: 51, damage: 9);
            bool playerTurn = true;
            int playerMove = 0;

            var activeEffects = new Dictionary<string, int>() {
                { "Shield", 0 }, { "Poison", 0 }, { "Recharge", 0 }
            };
            while (player.hitpoints > 0 && boss.hitpoints > 0)
            {
                // apply hardmode
                if (playerTurn && hardMode)
                {
                    player.hitpoints -= 1;
                    if (player.hitpoints <= 0) { break; }
                }
                // apply effects
                if (activeEffects["Poison"] > 0)
                {
                    boss.hitpoints -= spells["Poison"].damage;
                    activeEffects["Poison"]--;
                }
                if (activeEffects["Recharge"] > 0)
                {
                    player.manaPoints += spells["Recharge"].manaGained;
                    activeEffects["Recharge"]--;
                }
                if (activeEffects["Shield"] > 0)
                {
                    player.armor = spells["Shield"].armor;
                    activeEffects["Shield"]--;
                }
                else { player.armor = 0; }

                // check state!
                if (boss.hitpoints <= 0 && player.hitpoints > 0)
                {
                    gameResult.playerWins = true;
                    break;
                }

                // apply move
                if (playerTurn)
                {
                    // check if there are any moves left, otherwise extend the game
                    if (playerMove >= moves.Count) { gameResult.gameEnds = false; break; }

                    var spell = moves[playerMove];

                    // Yes! let's go :)
                    player.manaSpent += spells[spell].cost;
                    if (spell == "Magic Missile")
                    {
                        boss.hitpoints -= spells["Magic Missile"].damage;
                    }
                    if (spell == "Drain")
                    {
                        boss.hitpoints -= spells["Drain"].damage;
                        player.hitpoints += spells["Drain"].heals;
                    }
                    if (spell == "Shield")
                    {
                        if (activeEffects["Shield"] > 0) { gameResult.gameValid = false; break; }
                        activeEffects["Shield"] = spells["Shield"].duration;
                    }
                    if (spell == "Poison")
                    {
                        if (activeEffects["Poison"] > 0) { gameResult.gameValid = false; break; }
                        activeEffects["Poison"] = spells["Poison"].duration;
                    }
                    if (spell == "Recharge")
                    {
                        if (activeEffects["Recharge"] > 0) { gameResult.gameValid = false; break; }
                        activeEffects["Recharge"] = spells["Recharge"].duration;
                    }

                    // can we even cast the spell?
                    if (player.manaPoints < spells[spell].cost) { gameResult.gameValid = false; break; }
                    player.manaPoints -= spells[spell].cost;

                    // update costs
                    gameResult.manaSpent = player.manaSpent;
                    if (player.manaSpent > manaLimit) { gameResult.gameValid = true; break; }

                    playerMove++;
                }
                else
                {
                    // the boss always attacks! with a minimum of 1 attack
                    player.hitpoints -= Math.Max((boss.damage - player.armor), 1);
                }

                // check state again!
                if (boss.hitpoints <= 0 && player.hitpoints > 0)
                {
                    gameResult.playerWins = true;
                    break;
                }

                // next turn
                playerTurn = !playerTurn;
            }
            return gameResult;
        }

        // deep copy the list, no references to the original list pleaseeeeee
        private List<string> CopyList(List<string> list)
        {
            var output = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                output.Add(list[i]);
            }
            return output;
        }

        public string GetInput()
        {
            return @"Hit Points: 51
Damage: 9";
        }
    }
}