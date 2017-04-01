using System;
using Common.Common.Models;

namespace Admin.Common
{
	public interface IAnnouncementEditorView
	{
		void GoBack();

		Announcement Announcement { get; }

}
}
