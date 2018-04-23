using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils.Menu;

namespace WindowsApplication34
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pivotGridControl1.DataSource = CreateTable(20);
            pivotGridControl1.ShowMenu += new PivotGridMenuEventHandler(pivotGridControl1_ShowMenu);

            pivotGridControl1.Fields.Add("Type", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            pivotGridControl1.Fields.Add("Product", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            PivotGridField fieldYear = new PivotGridField("Date", PivotArea.ColumnArea);
            fieldYear.Name = "FieldYear";
            fieldYear.Caption = fieldYear.Name;
            fieldYear.GroupInterval = PivotGroupInterval.DateYear;
            pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldYear });

            PivotGridField dataField = pivotGridControl1.Fields.Add("Number", DevExpress.XtraPivotGrid.PivotArea.DataArea);
            dataField.Options.AllowRunTimeSummaryChange = true;

        }

        void pivotGridControl1_ShowMenu(object sender, PivotGridMenuEventArgs e)
        {
            if (e.MenuType == PivotGridMenuType.HeaderSummaries)
            {
                e.Menu.Items.Clear();
                string curentSummaryDisplayType = Enum.GetName(typeof(PivotSummaryDisplayType), e.Field.SummaryDisplayType);
                foreach (string str in Enum.GetNames( typeof(PivotSummaryDisplayType)) )
                {
                    DXMenuCheckItem item = new DXMenuCheckItem(str, curentSummaryDisplayType == str);
                    item.Click += new EventHandler(ItemClick);
                    item.Tag = e.Field;
                    e.Menu.Items.Add(item);
                }
            }                
        }

        void ItemClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            PivotGridField field = item.Tag as PivotGridField;
            field.SummaryDisplayType = (PivotSummaryDisplayType)Enum.Parse(typeof(PivotSummaryDisplayType), item.Caption);
        }


        private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Type", typeof(string));
            tbl.Columns.Add("Product", typeof(string));
            tbl.Columns.Add("Date", typeof(DateTime));
            tbl.Columns.Add("Number", typeof(int));

            Random r = new Random();
            for (int i = 0; i < RowCount; i++)

                for (int j = 0; j < RowCount; j++)
                    tbl.Rows.Add(new object[] { String.Format("Type {0}", i % 2), String.Format("Product {0}", i % 3), DateTime.Now.AddYears(j % 3), r.Next(2) });
            return tbl;
        }

    }
}