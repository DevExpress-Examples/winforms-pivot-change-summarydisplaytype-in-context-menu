<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128581663/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2321)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# Pivot Grid for WinForms - How to Change SummaryDisplayType in the Context Menu

The following example shows how to customize the field header's context menu in the [PivotGridControl.PopupMenuShowing](https://docs.devexpress.com/WindowsForms/DevExpress.XtraPivotGrid.PivotGridControl.PopupMenuShowing) event handler. The event adds a new item to the _Number_ field header's context menu and allows you to change the [SummaryDisplayType](https://docs.devexpress.com/WindowsForms/1935/controls-and-libraries/pivot-grid/data-shaping/summarization/summaries/summary-display-modes) value.

![custom-context-menu](images/custom-context-menu.png)

Set the [PivotGridFieldOptions.AllowRunTimeSummaryChange](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPivotGrid.PivotGridFieldOptions.AllowRunTimeSummaryChange) property to **true** to allow users to change the data field’s summary type.

<!-- default file list -->
## Files to Review

* [Form1.cs](./CS/WindowsApplication34/Form1.cs) (VB: [Form1.vb](./VB/WindowsApplication34/Form1.vb))
* [Program.cs](./CS/WindowsApplication34/Program.cs) (VB: [Program.vb](./VB/WindowsApplication34/Program.vb))
<!-- default file list end -->

## Documentation 
- [PivotGridControl.PopupMenuShowing](https://docs.devexpress.com/WindowsForms/DevExpress.XtraPivotGrid.PivotGridControl.PopupMenuShowing)
- [Field Header Context Menu](https://docs.devexpress.com/WindowsForms/1726/controls-and-libraries/pivot-grid/ui-elements/field-header-context-menu)
## More Examples
- [Pivot Grid for WinForms - How to Customize the Popup Menu to Hide or Show the Totals](https://github.com/DevExpress-Examples/how-to-toggle-totals-visibility-at-runtime-e923)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-pivot-change-summarydisplaytype-in-context-menu&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-pivot-change-summarydisplaytype-in-context-menu&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
