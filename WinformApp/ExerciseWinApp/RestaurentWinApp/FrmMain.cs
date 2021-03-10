using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurentWinApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            CboRestaurant.Items.AddRange(new string[] { 
                "히노아지","스시야마","퍼지네이블","테마의 식당","홍콩반점","롤링파스타"
            });   
        }

        private void CboRestaurant_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selItem = sender as ComboBox;
            LblResult.Text = $"이번 주 모임장소 : {selItem.SelectedItem}";

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (CboRestaurant.Text != "")
            {
                CboRestaurant.Items.Add(CboRestaurant.Text);
                LblResult.Text = $"{CboRestaurant.Text} 아이템 추가";
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (CboRestaurant.SelectedIndex > -1)
            {
                LblResult.Text = $"{CboRestaurant.Text} 아이템 삭제";
                CboRestaurant.Items.Remove(CboRestaurant.Text);
            }
        }
    }
}
