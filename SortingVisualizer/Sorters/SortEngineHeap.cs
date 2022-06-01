using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingVisualizer.Sorters
{
    public class SortEngineHeap : ISortEngine
    {
        private int[] _array;
        private Graphics _g;
        private int _maxValue;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush blackBrush = new SolidBrush(Color.Black);

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        private bool doSlow = false;

        public SortEngineHeap(int[] array, Graphics g, int maxValue)
        {
            _array = array;
            _g = g;
            _maxValue = maxValue;
        }

        public void NextStep(bool slow = false)
        {
            doSlow = slow;
            int n = _array.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(_array, n, i);
            }
            for (int i = n-1; i >= 0; i--)
            {
                int temp = _array[0];
                _array[0] = _array[i];
                _array[i] = temp;
                DrawBar(i);
                Heapify(_array, i, 0);
            }
        }

        private void WriteText(string text)
        {
            _g.FillRectangle(blackBrush, 10, 10, 80, 15);
            _g.DrawString(text, new Font(new FontFamily("Arial"),9f), lineBrush, new RectangleF(10, 10, 80, 15));
        }

        public void DrawBar(params int[] i)
        {
            if(doSlow)
                System.Threading.Thread.Sleep(TimeSpan.FromTicks(10000));
            _g.FillRectangle(blackBrush, i[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, i[0], _maxValue - _array[i[0]], 1, _maxValue);
            if (i.Length>1)
            {
                _g.FillRectangle(blackBrush, i[1], 0, 1, _maxValue);
                _g.FillRectangle(lineBrush, i[1], _maxValue - _array[i[1]], 1, _maxValue);
            }
        }

        private void Heapify(int[] array, int n, int i)
        {
            sw.Start();
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && array[left] > array[largest])
                largest = left;
            if (right < n && array[right] > array[largest])
                largest = right;
            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;
                DrawBar(i, largest);
                sw.Stop();
                //WriteText(sw.ElapsedTicks.ToString());
                sw.Reset();
                Heapify(array, n, largest);
            }
        }

        public bool IsSorted()
        {
            for (int i = 0; i < _array.Length - 1; i++)
            {
                if (_array[i] > _array[i + 1])
                    return false;
            }
            return true;
        }
    }
}
