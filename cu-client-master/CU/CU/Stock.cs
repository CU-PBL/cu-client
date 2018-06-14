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
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
            Console.WriteLine("요청 보내는중...");
            var client = new RestClient($"http://106.10.42.112:8000/stock/list");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var productList = Cson<StockData>.ArrParse(response.Content);

            var cnt = 0;

            productList.ForEach(product =>
            {
                SaleView.Rows.Add();
                Console.WriteLine(product.id + product.name);
                SaleView.Rows[cnt].Cells["Column1"].Value = product.id;
                SaleView.Rows[cnt].Cells["Column2"].Value = product.name;
                SaleView.Rows[cnt].Cells["Column3"].Value = product.price;
                SaleView.Rows[cnt].Cells["Column4"].Value = product.stock;

                ++cnt;
            });
                
        }
    }
}
