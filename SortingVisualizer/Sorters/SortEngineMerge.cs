using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineMerge : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineMerge(int[] arr, Graphics g, int maxValue)
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

        public void NextStep(bool slow = false)
        {
            doSlow = slow;

            MergeSort(0, _arr.Length -1);
        }

        private void MergeSort(int left, int right)
        {
            if (left < right && left != right)
            {
                int middle = (left + right) / 2;
                MergeSort(left, middle);
                MergeSort(middle + 1, right);
                Merge(left, middle, right);
            }
        }

        private void Merge(int left, int middle, int right)
        {
            int[] leftArr = new int[middle - left + 1];
            int[] rightArr = new int[right - middle];

            Array.Copy(_arr, left, leftArr, 0, middle - left + 1);
            Array.Copy(_arr, middle + 1, rightArr, 0, right - middle);

            int i, j;
            i = j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArr.Length)
                {
                    _arr[k] = rightArr[j];
                    j++;
                    DrawBar(k);
                }
                else if (j == rightArr.Length)
                {
                    _arr[k] = leftArr[i];
                    i++;
                    DrawBar(k);
                }
                else if (leftArr[i] <= rightArr[j])
                {
                    _arr[k] = leftArr[i];
                    i++;
                    DrawBar(k);
                }
                else
                {
                    _arr[k] = rightArr[j];
                    j++;
                    DrawBar(k);
                }
            }
        }

        public void DrawBar(params int[] position)
        {
            if (doSlow && position[0] % 8 == 0)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            _g.FillRectangle(backBrush, position[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, position[0], _maxValue - _arr[position[0]], 1, _maxValue);
        }
    }
}
