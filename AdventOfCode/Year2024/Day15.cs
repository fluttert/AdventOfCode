using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day15 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/15

        private char[][] grid;          // track grid
        private (int x, int y) robot;   // track robot
        private List<Box> boxes;        // all wide boxes

        public string SolvePart1(string input)
        {
            long result = 0;
            string[] parts = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            grid = CreateGridAndRobot(parts[0]);
            string[] robotMoves = parts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // moves
            for (int i = 0; i < robotMoves.Length; i++)
            {
                for (int j = 0; j < robotMoves[i].Length; j++)
                {
                    ProcessMove(robotMoves[i][j]);
                    //PrintGrid();
                }
            }

            // result
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 'O') { result += (100 * i) + j; }
                }
            }

            return "" + result;
        }

        private void PrintGrid()
        {
            Console.WriteLine();
            for (int i = 0; i < grid.Length; i++)
            {
                Console.WriteLine(grid[i]);
            }
        }

        private char[][] CreateGridAndRobot(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int dimRows = lines.Length;             // how many ROWS    (X)
            int dimCols = lines[0].Length;          // how many COLUMNS (Y)
            char[][] grid = new char[dimRows][];    // init rows
            for (int i = 0; i < dimRows; i++)
            {
                grid[i] = lines[i].ToCharArray();   // direct convert from String to char array
                int robot = lines[i].IndexOf('@');
                if (robot is not -1)
                {
                    this.robot = (i, robot);
                }
            }
            return grid;
        }

        private void ProcessMove(char move)
        {
            (int x, int y) v = (0, 0);  // vector
            switch (move)
            {
                case '^': v = (-1, 0); break;
                case 'v': v = (1, 0); break;
                case '<': v = (0, -1); break;
                case '>': v = (0, 1); break;
                default: throw new ArgumentException("Unknown move: " + move);
            }
            // assume the robot CAN move, will put robot back otherwise
            grid[robot.x][robot.y] = '.';
            // position available, just swap place
            if (grid[robot.x + v.x][robot.y + v.y] == '.')
            {
                grid[robot.x + v.x][robot.y + v.y] = '@';
                robot.x += v.x; robot.y += v.y;
                return;
            }

            // otherwise determine PUSH
            bool canPush = true;
            (int x, int y) swap = (robot.x + v.x, robot.y + v.y);
            while (grid[swap.x][swap.y] is not '.')         // search for an empty place
            {
                if (grid[swap.x][swap.y] == '#') { canPush = false; break; }                // no move
                if (grid[swap.x][swap.y] == 'O') { swap = (swap.x + v.x, swap.y + v.y); }   // continue search
            }
            // swap 1 box to the open position, and move the robot to the next one
            if (canPush) { grid[swap.x][swap.y] = 'O'; grid[robot.x + v.x][robot.y + v.y] = '@'; robot.x += v.x; robot.y += v.y; }
            else { grid[robot.x][robot.y] = '@'; }  // robot will not move
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            string[] parts = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            grid = CreateGridAndRobotWideVersion(parts[0]);
            string[] robotMoves = parts[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            PrintGrid();

            //moves
            for (int i = 0; i < robotMoves.Length; i++)
            {
                for (int j = 0; j < robotMoves[i].Length; j++)
                {
                    ProcessWideMove(robotMoves[i][j]);
                    //printgrid();
                }
            }

            // result
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 'O') { result += (100 * i) + j; }
                }
            }

            return "" + result;
        }

        private char[][] CreateGridAndRobotWideVersion(string input)
        {
            // grid will be only consisting of the robot + walls
            
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int dimRows = lines.Length;             // how many ROWS    (X)
            int dimCols = lines[0].Length * 2;          // how many COLUMNS (Y)
            char[][] grid = new char[dimRows][];    // init rows
            this.boxes = new();
            int boxId = 0;
            for (int i = 0; i < dimRows; i++)
            {
                grid[i] = new char[dimCols];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    switch (lines[i][j])
                    {
                        case '#': grid[i][j * 2] = '#'; grid[i][(j * 2) + 1] = '#'; break;
                        case 'O': grid[i][j * 2] = '.'; grid[i][(j * 2) + 1] = '.'; this.boxes.Add(new Box(i, j * 2, boxId)); boxId++; break;
                        //case 'O': grid[i][j * 2] = '['; grid[i][(j * 2) + 1] = ']'; this.boxes.Add(new Box(i, j * 2, boxId)); boxId++; break;
                        case '.': grid[i][j * 2] = '.'; grid[i][(j * 2) + 1] = '.'; break;
                        case '@': grid[i][j * 2] = '@'; grid[i][(j * 2) + 1] = '.'; this.robot = (i, j * 2); break;
                        default:
                            throw new ArgumentException("Unknown input: " + lines[i][j]);
                            break;
                    }
                }
            }
            return grid;
        }

        private void ProcessWideMove(char move)
        {
            (int x, int y) v = (0, 0);  // vector
            switch (move)
            {
                case '^': v = (-1, 0); break;
                case 'v': v = (1, 0); break;
                case '<': v = (0, -1); break;
                case '>': v = (0, 1); break;
                default: throw new ArgumentException("Unknown move: " + move);
            }
            // assume the robot CAN move, will put robot back otherwise
            grid[robot.x][robot.y] = '.';
            // position available, just swap place
            if (grid[robot.x + v.x][robot.y + v.y] == '.')
            {
                grid[robot.x + v.x][robot.y + v.y] = '@';
                robot.x += v.x; robot.y += v.y;
                return;
            }

            // otherwise determine PUSH
            bool canPush = true;
            (int x, int y) swap = (robot.x + v.x, robot.y + v.y);
            while (grid[swap.x][swap.y] is not '.')         // search for an empty place
            {
                if (grid[swap.x][swap.y] == '#') { canPush = false; break; }                // no move
                if (grid[swap.x][swap.y] == 'O') { swap = (swap.x + v.x, swap.y + v.y); }   // continue search
            }
            // swap 1 box to the open position, and move the robot to the next one
            if (canPush) { grid[swap.x][swap.y] = 'O'; grid[robot.x + v.x][robot.y + v.y] = '@'; robot.x += v.x; robot.y += v.y; }
            else { grid[robot.x][robot.y] = '@'; }  // robot will not move
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }

    public class Box
    {
        private int row, col, colEnd, id;

        public Box(int row, int col, int id = -1)
        {
            this.row = row;
            this.col = col;
            colEnd = col + 1;
            this.id = id;
        }

        public List<Box> ConnectedBoxes(List<Box> boxes)
        {
            var adjecentBoxes = new List<Box>();
            for (int i = 0; i < boxes.Count; i++)
            {
                // .??..
                // ?[]?.
                // .??..
                // if questionmark == '[' or ']' then it's adjecent!
            }
            return boxes;
        }

        public bool CanMove(char[][] grid, char move)
        {
            // if move does not hit a wall ( '#' ), then you can move ( box or empty space is considered good)
            //case '^': v = (-1, 0); break;
            //case 'v': v = (1, 0); break;
            //case '<': v = (0, -1); break;
            //case '>': v = (0, 1); break;
            bool moveable = true;
            if (move == '<' && grid[this.row][this.col-1] == '#') { moveable = false;  }
            if (move == '>' && grid[this.row][this.colEnd+1] == '#') { moveable = false;  }
            if (move == '^' && (grid[this.row-1][this.col] == '#' || grid[this.row - 1][this.colEnd] == '#')) { moveable = false;  }
            if (move == 'v' && (grid[this.row+1][this.col] == '#' || grid[this.row + 1][this.colEnd] == '#')) { moveable = false;  }
            return moveable;
        }

        public void Move(char move) {
            if (move == '<') { col += -1; colEnd += -1; }
            if (move == '>') { col += 1; colEnd +=-1; }
            if (move == '^') { row += -1; }
            if (move == 'v') { row += 1; }

        }
    }
}