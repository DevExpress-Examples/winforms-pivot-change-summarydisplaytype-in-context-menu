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

namespace WindowsApplication34 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            pivotGridControl1.DataSource = CreateTable(20);
            pivotGridControl1.PopupMenuShowing +=new PopupMenuShowingEventHandler(pivotGridControl1_PopupMenuShowing);

            pivotGridControl1.Fields.AddDataSourceColumn("Type", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            pivotGridControl1.Fields.AddDataSourceColumn("Product", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            PivotGridField fieldYear = new PivotGridField("", PivotArea.ColumnArea);
            fieldYear.Name = "FieldYear";
            fieldYear.DataBinding = new DataSourceColumnBinding("Date", PivotGroupInterval.DateYear);
            fieldYear.Caption = fieldYear.Name;
            pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldYear });

            PivotGridField dataField = pivotGridControl1.Fields.AddDataSourceColumn("Number", DevExpress.XtraPivotGrid.PivotArea.DataArea);
            dataField.Caption = "Number";
            dataField.Tag = PivotSummaryDisplayType.Default;

        }

        void pivotGridControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {         
            if (e.MenuType == PivotGridMenuType.Header && e.HitInfo.HeaderField.Area == PivotArea.DataArea) {
                DXSubMenuItem sdtItem = new DXSubMenuItem();
                sdtItem.Caption = "Summary Display Type";
                e.Menu.Items.Add(sdtItem);

                string curentSummaryDisplayType = Enum.GetName(typeof(PivotSummaryDisplayType), e.Field.Tag);
                foreach (string str in Enum.GetNames(typeof(PivotSummaryDisplayType))) {
                    DXMenuCheckItem item = new DXMenuCheckItem(str, curentSummaryDisplayType == str);
                    item.Click += new EventHandler(ItemClick);
                    item.Tag = e.Field;
                    sdtItem.Items.Add(item);
                }
            }                
        }

        void ItemClick(object sender, EventArgs e) {
            DXMenuItem item = sender as DXMenuItem;
            PivotGridField field = item.Tag as PivotGridField;
            DataSourceColumnBinding sourceBinding = new DataSourceColumnBinding("Number");
            PivotSummaryDisplayType newValue = (PivotSummaryDisplayType)Enum.Parse(typeof(PivotSummaryDisplayType), item.Caption);
            switch(newValue) {
                case PivotSummaryDisplayType.AbsoluteVariation:
                    field.DataBinding = new DifferenceBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        CalculationDirection.DownThenAcross,
                        DifferenceTarget.Previous,
                        DifferenceType.Absolute);
                    break;
                case PivotSummaryDisplayType.PercentVariation:
                    field.DataBinding = new DifferenceBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        CalculationDirection.DownThenAcross,
                        DifferenceTarget.Previous,
                        DifferenceType.Percentage);
                    break;
                case PivotSummaryDisplayType.PercentOfColumn:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValueAndRowParentValue);
                    break;
                case PivotSummaryDisplayType.PercentOfRow:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValueAndColumnParentValue);
                    break;
                case PivotSummaryDisplayType.PercentOfColumnGrandTotal:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue);
                    break;
                case PivotSummaryDisplayType.PercentOfRowGrandTotal:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue);
                    break;
                case PivotSummaryDisplayType.PercentOfGrandTotal:
                    field.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.None);
                    break;
                case PivotSummaryDisplayType.RankInColumnLargestToSmallest:
                    field.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, PivotSortOrder.Descending);
                    break;
                case PivotSummaryDisplayType.RankInColumnSmallestToLargest:
                    field.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, PivotSortOrder.Ascending);
                    break;
                case PivotSummaryDisplayType.RankInRowLargestToSmallest:
                    field.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        RankType.Dense, PivotSortOrder.Descending);
                    break;
                case PivotSummaryDisplayType.RankInRowSmallestToLargest:
                    field.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, PivotSortOrder.Ascending);
                    break;
                default:
                    field.DataBinding = sourceBinding;
                    break;
            }
            field.Tag = newValue;

        }

        private DataTable CreateTable(int RowCount) {
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
