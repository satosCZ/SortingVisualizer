using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineShell : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineShell(int[] arr, Graphics g, int maxValue)
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
            if (doSlow && position[0] % 10 == 0)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            _g.FillRectangle(backBrush, position[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, position[0], _maxValue - _arr[position[0]], 1, _maxValue);
        }

        public void NextStep(bool slow = false)
        {
            doSlow = slow;
            int[] interval = GenerateInterval();
            IntArrayShellSort(interval);
        }

        private void IntArrayShellSort(int[] interval)
        {
            for (int k = interval.Length-1; k >=0 ; k--)
            {
                int inter = interval[k];
                for (int m = 0; m < inter; m++)
                {
                    for (int j = m+inter; j < _arr.Length; j+=inter)
                    {
                        for (int i = j; i >= inter && _arr[i] < _arr[i-inter]; i-=inter)
                        {
                            int temp = _arr[i];
                            _arr[i] = _arr[i - 1];
                            _arr[i - 1] = temp;
                            DrawBar(i);
                            DrawBar(i - 1);
                        }
                    }
                }
            }

        }

        private int[] GenerateInterval()
        {
            int n = _arr.Length;

            if (n < 2) return new int[0];

            int t = Math.Max(1, (int)Math.Log(n, 3) - 1);
            int[] intervals = new int[t];
            intervals[0] = 1;
            for (int i = 1; i < t; i++)
            {
                intervals[i] = 3 * intervals[i - 1] + 1;
            }
            return intervals;
        }
    }
}
