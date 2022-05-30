Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Data.PivotGrid
Imports DevExpress.Utils.Menu

Namespace WindowsApplication34
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			pivotGridControl1.DataSource = CreateTable(20)
			AddHandler pivotGridControl1.PopupMenuShowing, AddressOf pivotGridControl1_PopupMenuShowing

			pivotGridControl1.Fields.Add("Type", DevExpress.XtraPivotGrid.PivotArea.RowArea)
			pivotGridControl1.Fields.Add("Product", DevExpress.XtraPivotGrid.PivotArea.RowArea)
			Dim fieldYear As New PivotGridField("Date", PivotArea.ColumnArea)
			fieldYear.Name = "FieldYear"
			fieldYear.Caption = fieldYear.Name
			fieldYear.GroupInterval = PivotGroupInterval.DateYear
			pivotGridControl1.Fields.AddRange(New PivotGridField() { fieldYear })

            Dim dataField As PivotGridField = pivotGridControl1.Fields.AddDataSourceColumn("Number", DevExpress.XtraPivotGrid.PivotArea.DataArea)
            dataField.Caption = "Number"
            dataField.Tag = PivotSummaryDisplayType.Default

        End Sub

		Private Sub pivotGridControl1_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
            If e.MenuType = PivotGridMenuType.Header AndAlso e.HitInfo.HeaderField.Area = PivotArea.DataArea Then
                Dim sdtItem As New DXSubMenuItem()
                sdtItem.Caption = "Summary Display Type"
                e.Menu.Items.Add(sdtItem)

                Dim curentSummaryDisplayType As String = System.Enum.GetName(GetType(PivotSummaryDisplayType), e.Field.Tag)
                For Each str As String In System.Enum.GetNames(GetType(PivotSummaryDisplayType))
                    Dim item As New DXMenuCheckItem(str, curentSummaryDisplayType = str)
                    AddHandler item.Click, AddressOf ItemClick
                    item.Tag = e.Field
                    sdtItem.Items.Add(item)
                Next str
            End If
        End Sub

        Private Sub ItemClick(ByVal sender As Object, ByVal e As EventArgs)
            Dim item As DXMenuItem = TryCast(sender, DXMenuItem)
            Dim field As PivotGridField = TryCast(item.Tag, PivotGridField)
            Dim sourceBinding As New DataSourceColumnBinding("Number")
            Dim newValue As PivotSummaryDisplayType = DirectCast(System.Enum.Parse(GetType(PivotSummaryDisplayType), item.Caption), PivotSummaryDisplayType)
            Select Case newValue
                Case PivotSummaryDisplayType.AbsoluteVariation
                    field.DataBinding = New DifferenceBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, CalculationDirection.DownThenAcross, DifferenceTarget.Previous, DifferenceType.Absolute)
                Case PivotSummaryDisplayType.PercentVariation
                    field.DataBinding = New DifferenceBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, CalculationDirection.DownThenAcross, DifferenceTarget.Previous, DifferenceType.Percentage)
                Case PivotSummaryDisplayType.PercentOfColumn
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValueAndRowParentValue)
                Case PivotSummaryDisplayType.PercentOfRow
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.RowValueAndColumnParentValue)
                Case PivotSummaryDisplayType.PercentOfColumnGrandTotal
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue)
                Case PivotSummaryDisplayType.PercentOfRowGrandTotal
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.RowValue)
                Case PivotSummaryDisplayType.PercentOfGrandTotal
                    field.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.None)
                Case PivotSummaryDisplayType.RankInColumnLargestToSmallest
                    field.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, PivotSortOrder.Descending)
                Case PivotSummaryDisplayType.RankInColumnSmallestToLargest
                    field.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, PivotSortOrder.Ascending)
                Case PivotSummaryDisplayType.RankInRowLargestToSmallest
                    field.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, RankType.Dense, PivotSortOrder.Descending)
                Case PivotSummaryDisplayType.RankInRowSmallestToLargest
                    field.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, PivotSortOrder.Ascending)
                Case Else
                    field.DataBinding = sourceBinding
            End Select
            field.Tag = newValue
        End Sub


        Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Type", GetType(String))
			tbl.Columns.Add("Product", GetType(String))
			tbl.Columns.Add("Date", GetType(DateTime))
			tbl.Columns.Add("Number", GetType(Integer))

			Dim r As New Random()
			For i As Integer = 0 To RowCount - 1

				For j As Integer = 0 To RowCount - 1
					tbl.Rows.Add(New Object() { String.Format("Type {0}", i Mod 2), String.Format("Product {0}", i Mod 3), DateTime.Now.AddYears(j Mod 3), r.Next(2) })
				Next j
			Next i
			Return tbl
		End Function

	End Class
End Namespace