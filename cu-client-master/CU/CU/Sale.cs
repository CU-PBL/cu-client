using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace CU
{
    public partial class Sale : Form
    {
        public Sale()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            paymentoption pay = new paymentoption();
            pay.Show();

            // MessageBox.Show("결제 완료.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sum = 0;
            var text = int.Parse(textBox1.Text);
            int[] data = new int[100];
            if (text < 1 || text > 100)
                MessageBox.Show("없는 제품 번호");
            else
            {
                data[text - 1] += 1;

                for (int i = 0; i < 100; i++)
                {
                    if (data[i] != 0)
                    {
                        var client = new RestClient("http://106.10.42.112:8000/product/" + (i + 1));
                        var request = new RestRequest(Method.GET);
                        var response = client.Execute(request);
                        var result = Cson<ProductData>.DeParse(response.Content);
                        int setting = 0;
                        int set = 0;
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if ((int)dataGridView1.Rows[j].Cells["Column1"].Value == result.id)
                            {
                                setting = 1;
                                set = j;
                            }
                        }

                        if (setting == 1)
                        {
                            int g = (int)dataGridView1.Rows[set].Cells["Column4"].Value;
                            dataGridView1.Rows[set].Cells["Column4"].Value = g + 1;
                            dataGridView1.Rows[set].Cells["Column5"].Value = result.price * (g + 1);
                        }
                        else
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells["Column1"].Value = result.id;
                            dataGridView1.Rows[index].Cells["Column2"].Value = result.name;
                            dataGridView1.Rows[index].Cells["Column3"].Value = result.price;
                            dataGridView1.Rows[index].Cells["Column4"].Value = 1;
                            dataGridView1.Rows[index].Cells["Column5"].Value = result.price;
                        }
                    }
                }
            }

            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                sum += (int)dataGridView1.Rows[j].Cells["Column5"].Value;
            }

            textBox3.Text = "" + sum;
        }
    }
}
