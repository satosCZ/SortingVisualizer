using SortingVisualizer.Sorters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingVisualizer
{
    public partial class Form1 : Form
    {
        int[] array;
        Graphics g;
        BackgroundWorker bw = null;
        bool paused = false;
        int maxValue = 0;
        int width = 0;

        public Form1()
        {
            InitializeComponent();
            PopulateDropDown();
        }

        private void PopulateDropDown()
        {
            List<string> classList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(ISortEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(x => x.Name).ToList();
            classList.Sort();
            foreach (string entry in classList)
            {
                dcbSortMode.Items.Add(entry);
            }
            dcbSortMode.SelectedIndex = 0;
        }

        private void PrepareArray(Graphics g)
        {
            array = new int[pbDisplay.Width];
            maxValue = pbDisplay.Height;
            width = pbDisplay.Width;
            double step = (double)maxValue / (double)width;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (int)(step * i);
                DrawLines(g, i);
            }
        }

        private void ShuffleArray(Graphics g)
        {
            Random rnd = new Random();
            maxValue = pbDisplay.Height;
            width = pbDisplay.Width;
            for (int i = 0; i < array.Length; i++)
            {
                int j = rnd.Next(i, array.Length);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                DrawLines(g, i);
                DrawLines(g, j);
            }
        }

        private void DrawLines(Graphics g, int position)
        {
            g.DrawLine(Pens.Black, position, 0, position, maxValue);
            g.DrawLine(Pens.Orange, position, maxValue - array[position], position, maxValue);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (array == null)
            {
                btnReset_Click(null, null);
            }
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            //lblTime.Text = stopwatch.Elapsed.ToString();
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            btnStartStop.Enabled = false;
            bw.RunWorkerAsync(argument: dcbSortMode.SelectedItem);

            stopwatch.Stop();
            lblTime.Text = stopwatch.Elapsed.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (array == null || array.Length != width)
            {
                PrepareArray(pbDisplay.CreateGraphics());
                btnReset.Text = "Shuffle";
                return;
            }
            else
            {
                if (array == null)
                {
                    btnReset_Click(null, null);
                }
                g = pbDisplay.CreateGraphics();
                ShuffleArray(g);
                lblSorted.Text = "0";
                lblUnsorted.Text = array.Length.ToString();
                btnStartStop.Enabled = true;
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            
            
            BackgroundWorker bgw = sender as BackgroundWorker;
            string SortEngineName = (string)e.Argument;
            Type type = Type.GetType("SortingVisualizer.Sorters." + SortEngineName);
            var ctors = type.GetConstructors();
            try
            {
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { array, g, pbDisplay.Height});
                while(!se.IsSorted() && (!bgw.CancellationPending))
                {
                    se.NextStep(cbSlow.Checked);
                }
                SortCompleted();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void SortCompleted()
        {
            Brush redBrush = new SolidBrush(Color.Red);
            Brush blackBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < array.Length; i++)
            {
                g.FillRectangle(redBrush, i, pbDisplay.Height - array[i], 2, pbDisplay.Height);
                if (i % 10 == 0)
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            }
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (array != null)
            {
                btnStartStop.Enabled = false;
                PrepareArray(pbDisplay.CreateGraphics());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrepareArray(pbDisplay.CreateGraphics());
            btnReset.Text = "Shuffle";
        }
    }
}
