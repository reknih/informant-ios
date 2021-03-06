﻿using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using UntisExp;

namespace vplan
{
	public class GroupTableSource : UITableViewSource {
		List<Group> tableItems;
		string cellIdentifier = "TableCell";
		GroupController _sv;
		public GroupTableSource (List<Group> items, GroupController sv)
		{
			tableItems = items;
			_sv = sv;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
			cell.TextLabel.Text = tableItems[indexPath.Row].ClassName;
			return cell;
		}
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var pm = new PrefManager ();
			pm.setInt ("group", indexPath.Row + 1);
			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the grey-ish highlight
			_sv.changeView ();
			if (UIDevice.CurrentDevice.UserInterfaceIdiom != UIUserInterfaceIdiom.Phone) {
				((VplanSuperViewController)_sv.SplitViewController).highwayToHell ();
			}
		}
		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				// remove the item from the underlying data source
				tableItems.RemoveAt(indexPath.Row);
				// delete the row from the table
				tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				break;
			case UITableViewCellEditingStyle.None:
				Console.WriteLine ("CommitEditingStyle:None called");
				break;
			}
		}
		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return false; // return false if you wish to disable editing for a specific indexPath or for all rows
		}
		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return false; // return false if you don't allow re-ordering
		}
		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete; // this example doesn't suppport Insert
		}
	}
}

