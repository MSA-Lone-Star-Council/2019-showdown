using System;
using Admin.Common;
using Admin.Common.API.Entities;
using UIKit;

namespace Admin.iOS
{
	public partial class GameEditorViewController : UIViewController, IGameEditorView
	{
		GameEditorPresenter _presenter;

		public GameEditorViewController(Game game)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			_presenter = new GameEditorPresenter(game, appDelegate.BackendClient);
		}

		partial void AdditionalSetup()
		{
			eventPicker.Model = new EventPickerModel(_presenter, eventField);
			awayTeamPicker.Model = new SchoolPickerModel(_presenter, awayTeamField);
			homeTeamPicker.Model = new SchoolPickerModel(_presenter, homeTeamField);
			scorekeeperPicker.Model = new UserPickerModel(_presenter, scorekeeperField);

			saveButton.TouchUpInside += async (sender, e) => _presenter.Save();
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			_presenter.TakeView(this);
			await _presenter.OnBegin();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			_presenter.RemoveView();
		}

		Game IGameEditorView.Game
		{
			get
			{
				return new Game()
				{
					Title = titleField.Text,
					// Scorekeeper will be set by presenter
					// Away team will be set by presenter
					// Home team will be set by presenter
					InProgress = inProgressSwitch.On
				};
			}

			set
			{
				titleField.Text = value.Title;
				// Scorekeeper will be set by presenter
				// Away team will be set by presenter
				// Home team will be set by presenter
				inProgressSwitch.On = value.InProgress;
			}
		}

		int IGameEditorView.SelectedEventIndex
		{
			get { return (int)eventPicker.SelectedRowInComponent(0); }

			set
			{
				if (value == -1) return;
				eventPicker.Select(value, 0, true);
				eventField.Text = eventPicker.Model.GetTitle(eventPicker, value, 0);
			}
		}

		int IGameEditorView.SelectedScorekeeperIndex
		{
			get { return (int)scorekeeperPicker.SelectedRowInComponent(0); }

			set
			{
				if (value == -1) return;
				scorekeeperPicker.Select(value, 0, true);
				scorekeeperField.Text = scorekeeperPicker.Model.GetTitle(scorekeeperPicker, value, 0);
			}
		}

		int IGameEditorView.SelectedAwayTeamIndex
		{
			get { return (int)awayTeamPicker.SelectedRowInComponent(0); }

			set
			{
				if (value == -1) return;
				awayTeamPicker.Select(value, 0, true);
				awayTeamField.Text = awayTeamPicker.Model.GetTitle(scorekeeperPicker, value, 0);
			}
		}

		int IGameEditorView.SelectedHomeTeamIndex
		{
			get { return (int)homeTeamPicker.SelectedRowInComponent(0);}

			set
			{
				if (value == -1) return;
				homeTeamPicker.Select(value, 0, true);
				homeTeamField.Text = homeTeamPicker.Model.GetTitle(homeTeamPicker, value, 0);
			}
		}

		bool IGameEditorView.FetchingValues
		{
			set
			{
				var enabled = !value;
				eventField.Enabled = enabled;
				scorekeeperField.Enabled = enabled;
				awayTeamField.Enabled = enabled;
				homeTeamField.Enabled = enabled;

				eventPicker.ReloadAllComponents();
				scorekeeperPicker.ReloadAllComponents();
				awayTeamPicker.ReloadAllComponents();
				homeTeamPicker.ReloadAllComponents();

				saveButton.BackgroundColor = enabled ? UIColor.Green : UIColor.Gray;
				saveButton.Enabled = enabled;
			}
		}

		public class EventPickerModel : UIPickerViewModel
		{
			UITextField _owner;
			GameEditorPresenter _presenter;

			public EventPickerModel(GameEditorPresenter presenter, UITextField owner)
			{
				_presenter = presenter;
				_owner = owner;
			}

			public override nint GetComponentCount(UIPickerView pickerView)
			{
				return 1;
			}

			public override string GetTitle(UIPickerView pickerView, nint row, nint component)
			{
				return _presenter.GetEventName((int)row);
			}

			public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
			{
				return _presenter.GetEventCount();
			}

			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				_owner.Text = GetTitle(pickerView, row, component);
			}
		}

		public class SchoolPickerModel : UIPickerViewModel
		{
			UITextField _owner;
			GameEditorPresenter _presenter;

			public SchoolPickerModel(GameEditorPresenter presenter, UITextField owner)
			{
				_presenter = presenter;
				_owner = owner;
			}

			public override nint GetComponentCount(UIPickerView pickerView)
			{
				return 1;
			}

			public override string GetTitle(UIPickerView pickerView, nint row, nint component)
			{
				return _presenter.GetSchoolName((int)row);
			}

			public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
			{
				return _presenter.GetSchoolCount();
			}

			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				_owner.Text = GetTitle(pickerView, row, component);
			}
		}

		public class UserPickerModel : UIPickerViewModel
		{
			UITextField _owner;
			GameEditorPresenter _presenter;

			public UserPickerModel(GameEditorPresenter presenter, UITextField owner)
			{
				_presenter = presenter;
				_owner = owner;
			}

			public override nint GetComponentCount(UIPickerView pickerView)
			{
				return 1;
			}

			public override string GetTitle(UIPickerView pickerView, nint row, nint component)
			{
				return _presenter.GetUserName((int)row);
			}

			public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
			{
				return _presenter.GetUserCount();
			}

			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				_owner.Text = GetTitle(pickerView, row, component);
			}
		}
	}
}
