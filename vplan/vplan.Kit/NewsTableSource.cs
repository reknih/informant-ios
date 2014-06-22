using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using UntisExp;

namespace vplan
{
	public class NewsTableSource : UITableViewSource {
		List<News> tableItems;
		string cellIdentifier = "TableCell";
		public NewsTableSource (List<News> items)
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
			cell.TextLabel.Text = tableItems[indexPath.Row].Title;
			cell.DetailTextLabel.Text = Helpers.TruncateWithPreservation(tableItems[indexPath.Row].Content, 40) + " ...";
			cell.ImageView.Image = FromUrl (tableItems [indexPath.Row].Image);
			return cell;
		}
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//new UIAlertView("Row Selected", tableItems[indexPath.Row].Line1, null, "OK", null).Show();
			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}
		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
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
			return UITableViewCellEditingStyle.None; // this example doesn't suppport Insert
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
		public void CustomAdd (News d)
		{
			tableItems.Insert (tableItems.Count, d);
		}
		static UIImage FromUrl (string uri)
		{
			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url))
				return UIImage.LoadFromData (data);
		}
	}
}

