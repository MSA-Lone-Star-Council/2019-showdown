using System;
using Common.Common;
using System.Threading.Tasks;

namespace Client.Common
{
	public class SubscriptionManager
	{
		public const string TagStorageKey = "SUBSCRIBED_TAGS";

		IStorage storage;
		INotificationHub hub;

		public SubscriptionManager(IStorage storage, INotificationHub hub)
		{
			this.storage = storage;
			this.hub = hub;
		}

		public bool this[string subscriptionId]
		{
			get
			{
				var savedTags = storage.GetList(TagStorageKey);
				return savedTags.Contains(subscriptionId);
			}
		}

		public async Task ToggleSubscription(string subscriptionId)
		{
			var current = this[subscriptionId];

			if (current)
			{
				storage.RemoveFromList(TagStorageKey, subscriptionId);
			}
			else
			{
				storage.AddToList(TagStorageKey, subscriptionId);
			}
			await hub.SaveTags(storage.GetList(TagStorageKey));
		}

		public async Task SaveToHub()
		{
			var tags = storage.GetList(TagStorageKey);
			await hub.SaveTags(storage.GetList(TagStorageKey));
		}
	}
}
