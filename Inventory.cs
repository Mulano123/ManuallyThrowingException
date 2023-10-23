using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManuallyThrowingException
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellingPrice;

  
        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource(); 
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListofCategory = new String[]
            {
                "Berevages",
                "Bread/Bakery",
                "Canned/Jared Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other",
            };

            foreach(string foods in ListofCategory)
            {
                cbCategory.Items.Add(foods);
            }
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
                throw new StringFormatException("Invalid Input!! Please enter the Product Name.");
            return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
                throw new NumberFormatException("Invalid Input!! Please enter the Quantity.");
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException("Invalid Input!! Please enter the Selling Price.");
            return Convert.ToDouble(price);
        }

        class NumberFormatException : Exception
        {
            public NumberFormatException(string numberExcept) : base(numberExcept) { }
        }

        class StringFormatException : Exception
        {
            public StringFormatException(string stringExcept) : base(stringExcept) { }
        }

        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string currencyExcept) : base(currencyExcept) { }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductName.Text) || (string.IsNullOrEmpty(txtQuantity.Text) || 
                    (string.IsNullOrEmpty(txtSellPrice.Text) || (string.IsNullOrEmpty(richTxtDescription.Text) ||
                    (cbCategory.SelectedIndex == -1)))))
                {
                    MessageBox.Show("Please fill up the form to add product!!");
                }
                else
                {
                    _ProductName = Product_Name(txtProductName.Text);
                    _Category = cbCategory.Text;
                    _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                    _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                    _Description = richTxtDescription.Text;
                    _Quantity = Quantity(txtQuantity.Text);
                    _SellingPrice = SellingPrice(txtSellPrice.Text);
                    showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellingPrice, _Quantity, _Description));
                    gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    gridViewProductList.DataSource = showProductList;
                }
            }
            catch (StringFormatException sfe)
            {
                MessageBox.Show("Message: " + sfe.Message);
            }
            catch (NumberFormatException nfe)
            {
                MessageBox.Show("Message: " + nfe.Message);
            }
            catch (CurrencyFormatException cfe)
            {
                MessageBox.Show("Message: " + cfe.Message);
            }

        }
    }
}

