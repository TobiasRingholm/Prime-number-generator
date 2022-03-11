using System.Diagnostics;

namespace CompulsoryAssignment
{
    public partial class PrimeNumberGenerator : Form
    {
        public PrimeNumberGenerator()
        {
            InitializeComponent();
        }
        private List<long> list1;
        private List<long> list2;
        private object myLock = new();

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            if (long.TryParse(textBox1.Text + textBox2.Text, out long l))
            {
                long first = long.Parse(textBox1.Text);
                long last = long.Parse(textBox2.Text);
                var stopwatch = new Stopwatch();
                label6.Text = "Calculating primes in parallel";
                label6.Visible = true;
                label11.Visible = false;

                stopwatch.Restart();
                await Task.Run(() =>
                {

                    var tasks = new[]
                        {
                        Task.Run(() => list1 = Program.GetPrimesParallel(first, last)),
                        };

                    Task.WaitAll(tasks);


                });
                stopwatch.Stop();

                listBox1.DataSource = list1;

                label6.Text = "Done in " + stopwatch.Elapsed + " time units";
                label6.Visible = true;

                if (listBox2.Items.Count != 0)
                {
                    if (Enumerable.SequenceEqual(list1, list2))
                    {
                        label8.Visible = true;
                        label8.Text = "The lists are equal!";
                    }
                    else
                    {
                        label8.Text = "The lists are not equal!";
                        label8.Visible = true;
                    }

                }
            }
            else
            {
                label11.Text = "Please use a long integer value";
                label11.Visible = true;
            }
            button2.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (long.TryParse(textBox1.Text + textBox2.Text, out long l))
            {
                long first = long.Parse(textBox1.Text);
                long last = long.Parse(textBox2.Text);
                var stopwatch = new Stopwatch();
                label7.Text = "Calculating primes Sequentially";
                label7.Visible = true;
                label11.Visible = false;

                stopwatch.Restart();
                await Task.Run(() =>
                {
                    var tasks = new[]
                        {
                        Task.Run(() => list2 = Program.GetPrimesSequential(first, last)),
                        };

                    Task.WaitAll(tasks);

                });
                stopwatch.Stop();

                listBox2.DataSource = list2;
                label7.Text = "Done in " + stopwatch.Elapsed + " time units";
                label7.Visible = true;
                if (listBox1.Items.Count != 0)
                {
                    if (Enumerable.SequenceEqual(list1, list2))
                    {
                        label8.Visible = true;
                        label8.Text = "The lists are equal!";
                    }
                    else
                    {
                        label8.Text = "The lists are not equal!";
                        label8.Visible = true;
                    }

                }
            }
            else
            {
                label11.Text = "Please use a long integer value";
                label11.Visible = true;
            }
            button1.Enabled = true;
        }
    }
}
