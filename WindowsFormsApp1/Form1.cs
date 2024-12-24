using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void LoadDataGridView()
        {
            string link = "https://localhost:44315/api/sanpham";

            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dataGridView1.DataSource = arr;
        }
        public void LoadComboBox()
        {
            string link = "https://localhost:44315/api/danhmuc";
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(DanhMuc[]));
            object data = js.ReadObject(response.GetResponseStream());
            DanhMuc[] arr1 = data as DanhMuc[];
            comboDanhMuc.DataSource = arr1;
            comboDanhMuc.ValueMember = "MaDM";
            comboDanhMuc.DisplayMember = "TenDM";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            LoadComboBox();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string madm = textMaDM.Text;
            string link = "http://localhost:90/hocrestful/api/sanpham?madm=" + madm;

            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dataGridView1.DataSource = arr;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?masp={0}&tensp={1}&gia={2}&madm={3}", textMaSP.Text
                , textTenSP.Text, textGia.Text, comboDanhMuc.SelectedValue);
            string link = "http://localhost:90/hocrestful/api/sanpham/" + postString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "POST";
            Stream dataStream = request.GetRequestStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                LoadDataGridView();
                MessageBox.Show("Thêm sản phẩm mới thành công");

            }
            else
            {
                MessageBox.Show("Thêm sản phẩm mới thất bại");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            textMaSP.Text = dataGridView1.Rows[d].Cells[0].Value.ToString();
            textTenSP.Text = dataGridView1.Rows[d].Cells[1].Value.ToString();
            textGia.Text = dataGridView1.Rows[d].Cells[2].Value.ToString();
            comboDanhMuc.Text = dataGridView1.Rows[d].Cells[3].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string putstring = string.Format("?masp={0}&tensp={1}&gia={2}&madm={3}",textMaSP.Text,textTenSP.Text,textGia.Text,comboDanhMuc.SelectedValue);
            string link = "http://localhost:90/hocrestful/api/sanpham/" + putstring;
            HttpWebRequest request = WebRequest.CreateHttp(link);
            request.Method = "PUT";
            Stream dataStream = request.GetRequestStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                LoadDataGridView();
                MessageBox.Show("Sửa sản phẩm mới thành công");
            }
            else
            {
                MessageBox.Show("Sửa sản phẩm mới thất bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string deleteString = string.Format("?masp={0}", textMaSP.Text);
            string link = "http://localhost:90/hocrestful/api/sanpham/" + deleteString;
            HttpWebRequest request = WebRequest.CreateHttp(link);
            request.Method = "DELETE";
            DataContractJsonSerializer js = new DataContractJsonSerializer (typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                LoadDataGridView();
                MessageBox.Show("Xóa sản phẩm mới thành công");
            }
            else
            {
                MessageBox.Show("Xóa sản phẩm mới thất bại");

            }
        }
    }
}
