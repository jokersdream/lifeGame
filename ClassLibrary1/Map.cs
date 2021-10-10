using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Map
    {
        public int row, column, generation = 0;
        public List<List<int>> map = new List<List<int>>();
        public HashSet<int> setLive = new HashSet<int>();
        public HashSet<int> setDie = new HashSet<int>();

        public void receive_map(ref List<List<int>> map)
        {
            this.map.Clear();

            this.map = map;
            this.row = map.Count;
            this.column = map[0].Count;
        }

        public void initialize(int rows, int columns)
        {
            this.map.Clear();

            this.generation = 0;
            this.row = rows;
            this.column = columns;

            for (int i = 0; i != rows; ++i)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j != columns; ++j)
                {
                    temp.Add(0);
                }
                map.Add(temp);
            }

            Random random = new Random();
            for (int i = rows / 3; i != rows * 2 / 3; ++i)
            {
                for (int j = columns / 3; j != columns * 2 / 3; ++j)
                {
                    map[i][j] = random.Next(0, 13) / 10;
                }
            }
        }

        public int live_count(int i, int j)
        {
            int count = 0;
            int row_max = row - 1;
            int col_max = column - 1;

            if (j != 0 && i != 0 && map[i - 1][j - 1] == 1)
                count++;
            if (i != 0 && map[i - 1][j] == 1)
                count++;
            if (j != col_max && i != 0 && map[i - 1][j + 1] == 1)
                count++;
            if (j != 0 && map[i][j - 1] == 1)
                count++;
            if (j != col_max && map[i][j + 1] == 1)
                count++;
            if (j != 0 && i != row_max && map[i + 1][j - 1] == 1)
                count++;
            if (i != row_max && map[i + 1][j] == 1)
                count++;
            if (j != col_max && i != row_max && map[i + 1][j + 1] == 1)
                count++;

            return count;
        }

        public void refresh()   ///refresh map
        {
            List<List<int>> res = new List<List<int>>();
            for (int i = 0; i != this.row; ++i)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j != this.column; ++j)
                {
                    int count = live_count(i, j);

                    if (count > 3 || count < 2)  ///towards death
                    {
                        temp.Add(0);
                        if(map[i][j] != 0)
                            setDie.Add(i * row + j);
                    }
                    else if (count == 3)  ///towards reborn
                    {
                        temp.Add(1);
                        if(map[i][j] != 1)
                            setLive.Add(i * row + j);
                    }
                    else   ///keep current, no need to change anything
                    {
                        temp.Add(map[i][j]);
                    }
                }
                res.Add(temp);
            }
            this.map = res;
            this.generation++;
        }
    }
}
