using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace CU
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var userData = new UserData {id = textBox1.Text, passwd = textBox2.Text};

            var result = Cson<UserData>.Parse(userData);
            var client = new RestClient(ConstantData.SERVER + "/login");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", result, ParameterType.RequestBody);
            var response = client.Execute(request);

            var loginResult = Cson<LoginData>.DeParse(response.Content);
//            Console.WriteLine($"로그인 결과: {loginResult.flag}");

            if (loginResult != null && loginResult.flag)
            {
                this.Hide();
                CU_Main main = new CU_Main();
                main.Show();
            }
            else
            {
                MessageBox.Show("로그인 정보가 일치하지 않습니다.", "사용자 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
    }
}