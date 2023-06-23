using System.Diagnostics;

namespace DatgetlQuestion
{
    public partial class Form1 : Form
    {
        List<int> xList = new List<int>();
        List<int> yList = new List<int>();

        int y2;
        int y1;

        int x2;
        int x1;

        bool toStop;

        public Form1()
        {
            InitializeComponent();
            InitCoordinates();
        }

        private void InitCoordinates()
        {

            xList.Add(38);
            xList.Add(44);
            xList.Add(48);
            xList.Add(56);
            xList.Add(80);
            xList.Add(95);

            yList.Add(94);
            yList.Add(85);
            yList.Add(92);
            yList.Add(99);
            yList.Add(110);
            yList.Add(105);

            lblCoordinates.Text += Environment.NewLine;

            for (int i = 0; i < xList.Count; i++)
            {

                lblCoordinates.Text += xList[i];

                lblCoordinates.Text += " , " + yList[i];

                lblCoordinates.Text += Environment.NewLine;
            }

        }

        private void DetermineCoordinatesToUse()
        {
            if (toStop) return;

            if (txtX.Text == "")
            {
                //calculate x with y
                for (int i = 0; i < yList.Count; i++)
                {
                    if (
                        (int.Parse(txtY.Text) <= yList[i] && int.Parse(txtY.Text) >= yList[i + 1])
                      || (int.Parse(txtY.Text) >= yList[i] && int.Parse(txtY.Text) <= yList[i + 1])
                        )
                    {
                        x2 = xList[i + 1];
                        x1 = xList[i];

                        y2 = yList[i + 1];
                        y1 = yList[i];


                        Debug.WriteLine(x1 + " " + y1 + " " + x2 + " " + y2);
                        break;
                    }
                }

            }
            else if (txtY.Text == "")
            {
                //calculate y with x
                for (int i = 0; i < xList.Count; i++)
                {
                    if (int.Parse(txtX.Text) < xList[i])
                    {
                        x2 = xList[i];
                        x1 = xList[i - 1];

                        y2 = yList[i];
                        y1 = yList[i - 1];


                        Debug.WriteLine(x1 + " " + y1 + " " + x2 + " " + y2);
                        break;

                    }
                }
            }

        }

        private void LineFormula()
        {
            if (toStop) return;

            decimal m = (decimal)(y2 - y1) / (x2 - x1);
            Debug.WriteLine(m);

            decimal c = y1 - (m * x1);
            Debug.WriteLine(c);

            //y = mx + c
            if (txtY.Text == "") txtY.Text = (m * int.Parse(txtX.Text) + c).ToString();
            else if (txtX.Text == "") txtX.Text = ((int.Parse(txtY.Text) - c) / m).ToString();
        }

        private void CheckIfLineIntersects()
        {
            if (txtX.Text == "" && txtY.Text == "")
            {
                toStop = true;
                MessageBox.Show("No Values input");

            }
            else if (txtX.Text == "")
            {
                //check Y
                if (int.Parse(txtY.Text) < 85 || int.Parse(txtY.Text) > 110)
                {
                    toStop = true;

                    MessageBox.Show("line does not intersect");

                }
            }
            else if (txtY.Text == "")
            {
                //check X
                if (int.Parse(txtX.Text) < 38 || int.Parse(txtX.Text) > 95)
                {
                    toStop = true;

                    MessageBox.Show("line does not intersect");

                }
            }
            else //both x and y has values
            {
                CheckIfCoordinatesOnLine(Convert.ToInt32(txtX.Text), Convert.ToInt32(txtY.Text));

            }
        }

        private void CheckIfCoordinatesOnLine(int x, int y)
        {
            bool onPolyLine = false;

            for (int i = 0; i < xList.Count - 1; i++)
            {
                x2 = xList[i + 1];
                x1 = xList[i];

                y2 = yList[i + 1];
                y1 = yList[i];


                decimal m = (decimal)(y2 - y1) / (x2 - x1);
                Debug.WriteLine(m);

                decimal c = y1 - (m * x1);
                Debug.WriteLine(c);

                // y = mx + c
                if (y == (int)(m * x + c)) { MessageBox.Show("point is on the polyline"); onPolyLine = true; break; }

            }

            if(!onPolyLine) MessageBox.Show("point is NOT on the polyline");

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            toStop = false;
            CheckIfLineIntersects();
            DetermineCoordinatesToUse();
            LineFormula();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}