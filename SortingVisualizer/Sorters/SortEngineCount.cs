using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingVisualizer.Sorters
{
    class SortEngineCount : ISortEngine
    {
        private bool doSlow = false;

        private int[] _array;
        private int _maxValue;
        private Graphics _g;

        Brush lineBrush = new SolidBrush(Color.Orange);
        Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));
        private Brush movingBrush = new SolidBrush(Color.Green);

        public SortEngineCount(int[] array, Graphics g, int maxValue)
        {
            _array = array;
            _g = g;
            _maxValue = maxValue;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < _array.Length-1; i++)
            {
                if (_array[i] > _array[i + 1]) return false;
            }
            return true;
        }

        public void NextStep(bool slow = false)
        {
            doSlow = slow;

            int[] sortedValues = new int[_array.Length];

            int[] counts = new int[_maxValue];

            foreach (var item in _array)
            {
                counts[item]++;
            }


            int numItemsBefore = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                int tmp = counts[i];
                counts[i] = numItemsBefore;
                numItemsBefore += tmp;
            }

            foreach (var item in _array)
            {
                sortedValues[counts[item]] = item;
                DrawBar(counts[item], sortedValues[counts[item]]);
                counts[item] += 1;
            }

            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = sortedValues[i];

            }
        }

        public void DrawBar(params int[] i)
        {
            if (doSlow)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            _g.FillRectangle(backBrush, i[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, i[0], _maxValue - i[1], 1, _maxValue);
        }
    }
}
