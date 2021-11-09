Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Data.PivotGrid
Imports DevExpress.Utils.Menu

Namespace WindowsApplication34

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            pivotGridControl1.DataSource = CreateTable(20)
            pivotGridControl1.PopupMenuShowing += New PopupMenuShowingEventHandler(AddressOf pivotGridControl1_PopupMenuShowing)
            pivotGridControl1.Fields.Add("Type", DevExpress.XtraPivotGrid.PivotArea.RowArea)
            pivotGridControl1.Fields.Add("Product", DevExpress.XtraPivotGrid.PivotArea.RowArea)
            Dim fieldYear As PivotGridField = New PivotGridField("Date", PivotArea.ColumnArea)
            fieldYear.Name = "FieldYear"
            fieldYear.Caption = fieldYear.Name
            fieldYear.GroupInterval = PivotGroupInterval.DateYear
            pivotGridControl1.Fields.AddRange(New PivotGridField() {fieldYear})
            Dim dataField As PivotGridField = pivotGridControl1.Fields.Add("Number", DevExpress.XtraPivotGrid.PivotArea.DataArea)
            dataField.Options.AllowRunTimeSummaryChange = True
        End Sub

        Private Sub pivotGridControl1_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
            If e.MenuType Is PivotGridMenuType.HeaderSummaries Then
                Dim sdtItem As DXSubMenuItem = New DXSubMenuItem()
                sdtItem.Caption = "Summary Display Type"
                e.Menu.Items.Add(sdtItem)
                Dim curentSummaryDisplayType As String = [Enum].GetName(GetType(PivotSummaryDisplayType), e.Field.SummaryDisplayType)
                For Each str As String In [Enum].GetNames(GetType(PivotSummaryDisplayType))
                    Dim item As DXMenuCheckItem = New DXMenuCheckItem(str, Equals(curentSummaryDisplayType, str))
                     ''' Cannot convert AssignmentExpressionSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
'''                     item.Click += new System.EventHandler(this.ItemClick)
'''  item.Tag = e.Field
                    sdtItem.Items.Add(item)
                Next
            End If
        End Sub

        Private Sub ItemClick(ByVal sender As Object, ByVal e As EventArgs)
            Dim item As DXMenuItem = TryCast(sender, DXMenuItem)
            Dim field As PivotGridField = TryCast(item.Tag, PivotGridField)
            field.SummaryDisplayType = CType([Enum].Parse(GetType(PivotSummaryDisplayType), item.Caption), PivotSummaryDisplayType)
        End Sub

        Private Function CreateTable(ByVal RowCount As Integer) As DataTable
            Dim tbl As DataTable = New DataTable()
            tbl.Columns.Add("Type", GetType(String))
            tbl.Columns.Add("Product", GetType(String))
            tbl.Columns.Add("Date", GetType(Date))
            tbl.Columns.Add("Number", GetType(Integer))
            Dim r As Random = New Random()
            For i As Integer = 0 To RowCount - 1
                For j As Integer = 0 To RowCount - 1
                    tbl.Rows.Add(New Object() {String.Format("Type {0}", i Mod 2), String.Format("Product {0}", i Mod 3), Date.Now.AddYears(j Mod 3), r.Next(2)})
                Next
            Next

            Return tbl
        End Function
    End Class
End Namespace
