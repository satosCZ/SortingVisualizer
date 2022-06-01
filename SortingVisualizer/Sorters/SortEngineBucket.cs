using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineBucket : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineBucket(int[] arr, Graphics g, int maxValue)
        {
            _arr = arr;
            _g = g;
            _maxValue = maxValue;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < _arr.Length - 1; i++)
            {
                if (_arr[i] > _arr[i + 1]) return false;
            }
            return true;
        }

        public void DrawBar(params int[] position)
        {
            if (doSlow && position[0] % 5 == 0)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            _g.FillRectangle(backBrush, position[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, position[0], _maxValue - _arr[position[0]], 1, _maxValue);
        }

        public void NextStep(bool slow)
        {
            doSlow = slow;
            int n = 16;
            int range = (_arr.Max() - _arr.Min()) + 1;
            List<int>[] buckets = new List<int>[range];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }

            for (int i = 0; i < _arr.Length; i++)
            {
                buckets[_arr[i] - _arr.Min()].Add(_arr[i]);
            }

            int index = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i].Count>0)
                    for (int j = 0; j < buckets[i].Count; j++)
                    {
                        _arr[index] = buckets[i][j];
                        DrawBar(index++);
                    }
            }

        }
    }
}
