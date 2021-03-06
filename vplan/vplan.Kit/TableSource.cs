﻿using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using UntisExp;

namespace vplan
{
	public class TableSource : UITableViewSource {
		List<Data> tableItems;
		string cellIdentifier = "TableCell";
		public TableSource (List<Data> items)
		{
			tableItems = items;
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
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, cellIdentifier);
			try {
				cell.TextLabel.Text = tableItems[indexPath.Row].Line1;
				cell.DetailTextLabel.Text = tableItems[indexPath.Row].Line2;
			} catch {
			}
			return cell;
		}
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//new UIAlertView("Row Selected", tableItems[indexPath.Row].Line1, null, "OK", null).Show();
			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}
		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				// remove the item from the underlying data source
				string del;
				if (tableItems [indexPath.Row].AltFach != "" && tableItems [indexPath.Row].Lehrer != "") {
					del = tableItems [indexPath.Row].AltFach + "%" + tableItems [indexPath.Row].Lehrer;
				} else {
					del = tableItems [indexPath.Row].Fach + "%" + tableItems [indexPath.Row].Vertreter;
				}
				int there;
				var pm = new PrefManager ();
				try {
					there = pm.getInt ("ignoredCount");
					if (there == 0) {
						throw new Exception ();
					}
				} catch {
					pm.setInt ("ignoredCount", 0);
					there = 0;
				}
				there++;
				pm.setString ("ignored" + Convert.ToString(there), del);
				pm.setInt ("ignoredCount", there);
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
			try {
			if (tableItems [indexPath.Row].Head == true) {
				return false;
			}
			} catch {
			}
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}
		public override string TitleForDeleteConfirmation (UITableView tableView, NSIndexPath indexPath)
		{   // Optional - default text is 'Delete'
			string r;
			if (tableItems [indexPath.Row].AltFach != "" && tableItems [indexPath.Row].Lehrer != "") {
				r = "Ignoriere " + tableItems [indexPath.Row].AltFach + " bei " + tableItems [indexPath.Row].Lehrer;
			} else {
				r = "Ignoriere " + tableItems [indexPath.Row].Fach + " bei " + tableItems [indexPath.Row].Vertreter;			
			}
			return r;
		}
		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you don't allow re-ordering
		}
		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete; // this example doesn't suppport Insert
		}
		public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
		{
			var item = tableItems[sourceIndexPath.Row];
			int deleteAt = sourceIndexPath.Row;
			int insertAt = destinationIndexPath.Row;
			// are we inserting 
			if (destinationIndexPath.Row < sourceIndexPath.Row) {
				// add one to where we delete, because we're increasing the index by inserting
				deleteAt += 1;
			} else {
				// add one to where we insert, because we haven't deleted the original yet
				insertAt += 1;
			}
			tableItems.Insert (insertAt, item);
			tableItems.RemoveAt (deleteAt);
		}
		public void CustomAdd (Data d)
		{
			tableItems.Insert (tableItems.Count, d);
		}
	}
}

